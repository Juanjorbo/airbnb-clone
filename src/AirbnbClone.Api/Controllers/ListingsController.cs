using AirbnbClone.Api.Contracts;
using AirbnbClone.Api.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AirbnbClone.Api.Domain;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;


namespace AirbnbClone.Api.Controllers;

[ApiController]
[Route("listings")]
public class ListingsController : ControllerBase
{
    private readonly AppDbContext _db;

    public ListingsController(AppDbContext db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<IActionResult> Search([FromQuery] ListingsSearchQuery query)
    {
        
        var q = _db.Listings.AsNoTracking().AsQueryable();

        // Filtro por ciudad
        if (!string.IsNullOrWhiteSpace(query.City))
        {
            var city = query.City.Trim();
            q = q.Where(x => x.City == city);
        }

        // Filtro por precio mínimo
        if (query.MinPrice.HasValue)
        {
            q = q.Where(x => x.PricePerNight >= query.MinPrice.Value);
        }

        // Filtro por precio máximo
        if (query.MaxPrice.HasValue)
        {
            q = q.Where(x => x.PricePerNight <= query.MaxPrice.Value);
        }

        // Filtro por huéspedes
        if (query.Guests.HasValue)
        {
            q = q.Where(x => x.MaxGuests >= query.Guests.Value);
        }

        // Filtro por orden
        q = query.Sort switch
        {
            "priceAsc" => q.OrderBy(x => x.PricePerNight),
            "priceDesc" => q.OrderByDescending(x => x.PricePerNight),
            "newest" => q.OrderByDescending(x => x.CreatedAtUtc),
            _ => q.OrderByDescending(x => x.CreatedAtUtc)
        };

        var page = query.Page < 1 ? 1 : query.Page;
        var pageSize = query.PageSize is < 1 or > 100 ? 20 : query.PageSize;

        var total = await q.CountAsync();

        var items = await q
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(x => new
            {
                x.Id,
                x.Title,
                x.City,
                x.PricePerNight,
                x.MaxGuests
            })
            .ToListAsync();

        return Ok(new
        {
            total,
            page,
            pageSize,
            items
        });
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateListingRequest request)
    {
        var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userIdString is null)
            return Unauthorized();

        var hostId = Guid.Parse(userIdString);

        if (string.IsNullOrWhiteSpace(request.Title) ||
            string.IsNullOrWhiteSpace(request.City) ||
            request.PricePerNight <= 0 ||
            request.MaxGuests <= 0)
        {
            return BadRequest(new { message = "Invalid listing data." });
        }

        var listing = new Listing
        {
            HostId = hostId,
            Title = request.Title.Trim(),
            City = request.City.Trim(),
            PricePerNight = request.PricePerNight,
            MaxGuests = request.MaxGuests,
            CreatedAtUtc = DateTime.UtcNow
        };

        _db.Listings.Add(listing);
        await _db.SaveChangesAsync();

        return Created($"/listings/{listing.Id}", new
        {
            listing.Id,
            listing.Title,
            listing.City,
            listing.PricePerNight,
            listing.MaxGuests
        });
    }

    [Authorize]
    [HttpGet("me")]
    public async Task<IActionResult> GetMyListings()
    {
        var hostIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (hostIdString is null)
            return Unauthorized();

        var hostId = Guid.Parse(hostIdString);

        var items = await _db.Listings
            .AsNoTracking()
            .Where(x => x.HostId == hostId)
            .OrderByDescending(x => x.CreatedAtUtc)
            .Select(x => new
            {
                x.Id,
                x.Title,
                x.City,
                x.PricePerNight,
                x.MaxGuests,
                x.CreatedAtUtc
            })
            .ToListAsync();

        return Ok(items);
    }

}
using AirbnbClone.Api.Contracts;
using AirbnbClone.Api.Data;
using AirbnbClone.Api.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace AirbnbClone.Api.Controllers;

[ApiController]
[Route("bookings")]
public class BookingsController : ControllerBase
{
    private readonly AppDbContext _db;

    public BookingsController(AppDbContext db)
    {
        _db = db;
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateBookingRequest request)
    {
        var guestIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (guestIdString is null)
            return Unauthorized();

        var guestId = Guid.Parse(guestIdString);

        if (request.ListingId == Guid.Empty)
            return BadRequest(new { message = "ListingId is required." });

        if (request.CheckIn >= request.CheckOut)
            return BadRequest(new { message = "CheckIn must be before CheckOut." });

        var listing = await _db.Listings
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.Id == request.ListingId);

        if (listing is null)
            return NotFound(new { message = "Listing not found." });

        if (listing.HostId == guestId)
            return BadRequest(new { message = "You cannot book your own listing." });

        var hasOverlap = await _db.Bookings.AnyAsync(b =>
            b.ListingId == request.ListingId &&
            b.Status != BookingStatus.Cancelled &&
            b.CheckIn < request.CheckOut &&
            request.CheckIn < b.CheckOut
        );

        if (hasOverlap)
            return Conflict(new { message = "Listing is not available for those dates." });

        var booking = new Booking
        {
            ListingId = request.ListingId,
            GuestId = guestId,
            CheckIn = request.CheckIn,
            CheckOut = request.CheckOut,
            Status = BookingStatus.Pending,
            CreatedAtUtc = DateTime.UtcNow
        };

        _db.Bookings.Add(booking);
        await _db.SaveChangesAsync();

        return Created($"/bookings/{booking.Id}", new
        {
            booking.Id,
            booking.ListingId,
            booking.GuestId,
            booking.CheckIn,
            booking.CheckOut,
            booking.Status
        });
    }

    [Authorize]
    [HttpGet("me")]
    public async Task<IActionResult> GetMyBookings()
    {
        var guestIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (guestIdString is null)
            return Unauthorized();

        var guestId = Guid.Parse(guestIdString);

        var bookings = await _db.Bookings
            .AsNoTracking()
            .Where(b => b.GuestId == guestId)
            .OrderByDescending(b => b.CreatedAtUtc)
            .Select(b => new
            {
                b.Id,
                b.ListingId,
                b.CheckIn,
                b.CheckOut,
                b.Status,
                b.CreatedAtUtc
            })
            .ToListAsync();

        return Ok(bookings);
    }

}

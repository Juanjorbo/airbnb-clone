using AirbnbClone.Api.Data;
using AirbnbClone.Api.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace AirbnbClone.Api.Controllers;

[ApiController]
[Route("favorites")]
[Authorize]
public class FavoritesController : ControllerBase
{
    private readonly AppDbContext _db;

    public FavoritesController(AppDbContext db)
    {
        _db = db;
    }

    // Toggle: si existe -> lo borra, si no existe -> lo crea
    [HttpPost("{listingId:guid}")]
    public async Task<IActionResult> Toggle(Guid listingId)
    {
        var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userIdString is null) return Unauthorized();

        if (!Guid.TryParse(userIdString, out var userId))
            return Unauthorized();

        var listingExists = await _db.Listings.AsNoTracking().AnyAsync(x => x.Id == listingId);
        if (!listingExists)
            return NotFound(new { message = "Listing not found." });

        var existing = await _db.Favorites
            .SingleOrDefaultAsync(f => f.UserId == userId && f.ListingId == listingId);

        if (existing is not null)
        {
            _db.Favorites.Remove(existing);
            await _db.SaveChangesAsync();
            return Ok(new { favorited = false });
        }

        var fav = new Favorite
        {
            UserId = userId,
            ListingId = listingId,
            CreatedAtUtc = DateTime.UtcNow
        };

        _db.Favorites.Add(fav);
        await _db.SaveChangesAsync();

        return Ok(new { favorited = true });
    }

    // Devuelve IDs de listings favoritos del usuario
    [HttpGet("me")]
    public async Task<IActionResult> GetMyFavorites()
    {
        var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userIdString is null) return Unauthorized();

        if (!Guid.TryParse(userIdString, out var userId))
            return Unauthorized();

        var listingIds = await _db.Favorites
            .AsNoTracking()
            .Where(f => f.UserId == userId)
            .OrderByDescending(f => f.CreatedAtUtc)
            .Select(f => f.ListingId)
            .ToListAsync();

        return Ok(new { listingIds });
    }

    // Borrar explícito (por si lo necesitas)
    [HttpDelete("{listingId:guid}")]
    public async Task<IActionResult> Remove(Guid listingId)
    {
        var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userIdString is null) return Unauthorized();

        if (!Guid.TryParse(userIdString, out var userId))
            return Unauthorized();

        var existing = await _db.Favorites
            .SingleOrDefaultAsync(f => f.UserId == userId && f.ListingId == listingId);

        if (existing is null)
            return NotFound(new { message = "Favorite not found." });

        _db.Favorites.Remove(existing);
        await _db.SaveChangesAsync();

        return Ok(new { removed = true });
    }
}
using AirbnbClone.Api.Data;
using AirbnbClone.Api.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AirbnbClone.Api.Controllers;

[ApiController]
[Route("dev")]
public class DevController : ControllerBase
{
    private readonly AppDbContext _db;
    private readonly IWebHostEnvironment _env;

    public DevController(AppDbContext db, IWebHostEnvironment env)
    {
        _db = db;
        _env = env;
    }

    [HttpPost("seed-cartoon")]
    public async Task<IActionResult> SeedCartoon()
    {
        if (!_env.IsDevelopment())
            return NotFound();

        var hostId = Guid.Parse("11111111-1111-1111-1111-111111111111");

        var seed = new List<Listing>
        {
            new() { HostId = hostId, Title = "Piña de Bob Esponja", City = "Fondo de Bikini", PricePerNight = 89, MaxGuests = 2, ImageUrl = "frontend/public/listings/spongebob.jpg" },
            new() { HostId = hostId, Title = "Casa de Patricio (Roca Deluxe)", City = "Fondo de Bikini", PricePerNight = 59, MaxGuests = 2 },
            new() { HostId = hostId, Title = "Casa Árbol (Hora de Aventuras)", City = "Ooo", PricePerNight = 140, MaxGuests = 4 },
            new() { HostId = hostId, Title = "Castillo de Peach", City = "Reino Champiñón", PricePerNight = 220, MaxGuests = 6 },
            new() { HostId = hostId, Title = "Mansión de Scooby-Doo", City = "Crystal Cove", PricePerNight = 175, MaxGuests = 6 },
            new() { HostId = hostId, Title = "Dojo Tortugas Ninja (Alcantarillas)", City = "New York", PricePerNight = 110, MaxGuests = 5 },
            new() { HostId = hostId, Title = "Casa de los Simpson", City = "Springfield", PricePerNight = 95, MaxGuests = 5 },
            new() { HostId = hostId, Title = "Casa de Coraje", City = "Nowhere", PricePerNight = 80, MaxGuests = 3 },
            new() { HostId = hostId, Title = "Laboratorio de Dexter (Habitación secreta)", City = "Dexter’s Lab", PricePerNight = 160, MaxGuests = 2 },
            new() { HostId = hostId, Title = "Planet Express (Futurama)", City = "Nueva Nueva York", PricePerNight = 190, MaxGuests = 4 },
            new() { HostId = hostId, Title = "Casa de Gumball", City = "Elmore", PricePerNight = 85, MaxGuests = 5 },
            new() { HostId = hostId, Title = "Castillo de Hyrule (Zelda)", City = "Hyrule", PricePerNight = 240, MaxGuests = 6 },
        };

        var existing = await _db.Listings
            .AsNoTracking()
            .Select(x => new { x.Title, x.City })
            .ToListAsync();

        var existingSet = new HashSet<string>(existing.Select(x => $"{x.Title}||{x.City}"));

        var toAdd = seed
            .Where(x => !existingSet.Contains($"{x.Title}||{x.City}"))
            .Select(x =>
            {
                x.CreatedAtUtc = DateTime.UtcNow;
                return x;
            })
            .ToList();

        if (toAdd.Count == 0)
            return Ok(new { message = "Already seeded.", added = 0 });

        _db.Listings.AddRange(toAdd);
        await _db.SaveChangesAsync();

        return Ok(new { message = "Seeded cartoon listings.", added = toAdd.Count });
    }

    [HttpDelete("clear-listings")]
    public async Task<IActionResult> ClearListings()
    {
        if (!_env.IsDevelopment())
            return NotFound();

        await _db.Database.ExecuteSqlRawAsync(@"DELETE FROM ""Listings"";");
        return Ok(new { message = "Listings cleared." });
    }
}
namespace AirbnbClone.Api.Domain;

public class Listing
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid HostId { get; set; }

    public string Title { get; set; } = string.Empty;

    public string City { get; set; } = string.Empty;

    public decimal PricePerNight { get; set; }

    public int MaxGuests { get; set; }

    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;

    public string ImageUrl { get; set; } = string.Empty;
}

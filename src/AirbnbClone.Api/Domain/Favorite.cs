namespace AirbnbClone.Api.Domain;

public class Favorite
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid UserId { get; set; }

    public Guid ListingId { get; set; }

    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
}
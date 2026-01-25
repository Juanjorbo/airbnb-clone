namespace AirbnbClone.Api.Contracts;

public sealed class CreateBookingRequest
{
    public Guid ListingId { get; set; }

    // Formato: "2026-01-25"
    public DateOnly CheckIn { get; set; }
    public DateOnly CheckOut { get; set; }
}

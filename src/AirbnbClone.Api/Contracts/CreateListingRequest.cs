namespace AirbnbClone.Api.Contracts;

public sealed class CreateListingRequest
{
    public string Title { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public decimal PricePerNight { get; set; }
    public int MaxGuests { get; set; }
    public string ImageUrl { get; set; } = string.Empty;
}

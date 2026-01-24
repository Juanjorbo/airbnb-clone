namespace AirbnbClone.Api.Contracts;

public sealed class ListingsSearchQuery
{
    public string? City { get; set; }
    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }
    public int? Guests { get; set; }

    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 20;

    public string? Sort { get; set; } // priceAsc, priceDesc, newest
}

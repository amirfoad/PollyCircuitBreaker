namespace PricingApi.Models;

public class PricingDetails
{
    public PricingDetails(Guid productId,
        string currencyCode,
        decimal price)
    {
        ProductId = productId;
        CurrencyCode = currencyCode;
        Price = price;
    }
    public Guid ProductId { get; set; }
    public string CurrencyCode { get; set; }
    public decimal Price { get; set; }
}
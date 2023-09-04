namespace ProductApi.Models
{
    public record PricingDetails
    {
        public Guid ProductId { get; set; }
        public string CurrencyCode { get; set; }
        public decimal Price { get; set; }
    }
}
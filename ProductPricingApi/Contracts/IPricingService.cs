using ProductPricingApi.Models;

namespace ProductPricingApi.Contracts;

public interface IPricingService
{
    Task<PricingDetails> GetPricingForProductAsync(Guid productId, string currencyCode);
}
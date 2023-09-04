using PricingApi.Models;

namespace PricingApi.Contracts;

public interface IPricingService
{
    Task<PricingDetails> GetPricingForProductAsync(Guid productId,string currencyCode);
}
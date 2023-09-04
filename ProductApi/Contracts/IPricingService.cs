using ProductApi.Models;

namespace ProductApi.Contracts
{
    public interface IPricingService
    {
        Task<PricingDetails> GetPricingForProductAsync(Guid productId, string currencyCode);
    }
}
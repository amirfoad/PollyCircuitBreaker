using PricingApi.Contracts;
using PricingApi.Models;

namespace PricingApi.Services;

public class PricingService : IPricingService
{
    private DateTime _recoveryTime = DateTime.UtcNow;
    private static readonly Random random = new();

    public Task<PricingDetails> GetPricingForProductAsync(Guid productId, string currencyCode)
    {
        if (_recoveryTime > DateTime.UtcNow)
        {
            throw new Exception("Something went wrong");
        }

        if (_recoveryTime < DateTime.UtcNow && random.Next(1, 4) == 1)
        {
            _recoveryTime = DateTime.UtcNow.AddSeconds(30);
        }

        return Task.FromResult(new PricingDetails(productId, currencyCode, 10.99m));

    }
}
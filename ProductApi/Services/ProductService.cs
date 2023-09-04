using ProductApi.Contracts;
using ProductApi.Models;

namespace ProductApi.Services
{
    public class ProductService : IProductService
    {
        public Task<ProductDetails> GetProduct(Guid productId)
        {
            return Task.FromResult(new ProductDetails(productId,
                "Macbook Pro"));
        }
    }
}
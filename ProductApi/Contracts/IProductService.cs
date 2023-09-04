using ProductApi.Models;

namespace ProductApi.Contracts
{
    public interface IProductService
    {
        Task<ProductDetails> GetProduct(Guid productId);
    }
}
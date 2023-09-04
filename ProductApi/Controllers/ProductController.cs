using Microsoft.AspNetCore.Mvc;
using ProductApi.Contracts;
using ProductApi.Models;

namespace ProductApi.Controllers
{
    public class ProductController : ControllerBase
    {
        private readonly IPricingService _pricingService;
        private readonly IProductService _productService;

        public ProductController(IPricingService pricingService,
            IProductService productService)
        {
            _pricingService = pricingService;
            _productService = productService;
        }

        [HttpGet("products/{productId}/currencies/{currencyCode}")]
        public async Task<IActionResult> GetProduct([FromRoute] Guid productId,
            [FromRoute] string currencyCode)
        {
            #region Product

            var product = await _productService.GetProduct(productId);
            if (product is null)
                return NotFound();

            #endregion Product

            var pricing = await _pricingService.GetPricingForProductAsync(productId, currencyCode);
            return Ok(new ProductResponse(productId,
                product.Name,
                pricing.Price,
                currencyCode));
        }
    }
}
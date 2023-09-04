using Microsoft.AspNetCore.Mvc;
using ProductPricingApi.Contracts;

namespace ProductPricingApi.Controllers
{
    public class PricingController : ControllerBase
    {
        private readonly IPricingService _pricingService;

        public PricingController(IPricingService pricingService)
        {
            _pricingService = pricingService;
        }

        [HttpGet("products/{productId}/currencies/{currencyCode}")]
        public async Task<IActionResult> GetPricingForProduct([FromRoute] Guid productId,
            [FromRoute] string currencyCode)
        {
            try
            {
                var pricingDetails = await _pricingService.GetPricingForProductAsync(productId, currencyCode);
                return Ok(pricingDetails);
            }
            catch
            {
                return StatusCode(503);
            }
        }
    }
}
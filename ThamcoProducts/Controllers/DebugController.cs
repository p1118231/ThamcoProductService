using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using ThamcoProducts.Services.ProductRespository;
using ThamcoProducts.Services.Undercutters;


namespace ThamcoProducts.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DebugController : ControllerBase
    {
        // GET: api/weather
        private readonly ILogger<DebugController> _logger;
        private IUndercuttersService _undercuttersService;

        private IProductSerivce _productService;

        public DebugController(ILogger<DebugController> logger, IUndercuttersService undercuttersService, IProductSerivce productSerivce)
        {
            _logger = logger;
            _undercuttersService = undercuttersService;
            _productService = productSerivce;
        }

         [HttpGet("Undercutters")]
       
        public async Task<IActionResult> UnderCutters()
        {
            IEnumerable<ProductDto> products = null!;

            try{

                products = await _undercuttersService.GetProductsAsync();

            }
            catch{

                _logger.LogWarning("failure to access undercutters service ");
                products= Array.Empty<ProductDto>();

            }

            return Ok(products.ToList());

        }

        [HttpGet("FakeProducts")]
       
        public async Task<IActionResult> FakeProducts()
        {
            IEnumerable<Product> products = null!;

            try{

                products = await _productService.GetProductsAsync();

            }
            catch{

                _logger.LogWarning("failure to access products service ");
                products= Array.Empty<Product>();

            }

            return Ok(products.ToList());

        }
    }
}

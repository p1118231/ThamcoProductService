using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
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

        public DebugController(ILogger<DebugController> logger, IUndercuttersService undercuttersService)
        {
            _logger = logger;
            _undercuttersService = undercuttersService;
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
    }
}

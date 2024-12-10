using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace ThamcoProducts.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        // GET: api/weather
        [HttpGet]
        [Authorize]
        public IActionResult GetWeather()
        {
            var weather = new
            {
                Temperature = 22.5,
                Condition = "Sunny",
                City = "Sample City"
            };

            return Ok(weather);
        }

        // GET: api/weather/{city}
        [HttpGet("{city}")]
        public IActionResult GetWeatherByCity(string city)
        {
            var weather = new
            {
                Temperature = 22.5,
                Condition = "Sunny",
                City = city
            };

            return Ok(weather);
        }
    }
}

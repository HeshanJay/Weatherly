using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WeatherApp.API.Services;

namespace WeatherApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        private readonly WeatherService _weatherService;
        private readonly ILogger<WeatherController> _logger;

        public WeatherController(WeatherService weatherService, ILogger<WeatherController> logger)
        {
            _weatherService = weatherService;
            _logger = logger;
        }

        [HttpGet("{cityId:int}")]
        [Authorize]
        public async Task<IActionResult> GetWeather(int cityId)
        {
            var data = await _weatherService.GetWeatherByCityCodeAsync(cityId);
            return data == null ? NotFound() : Ok(data);
        }

        [HttpGet("all")]
        [Authorize]
        public async Task<IActionResult> GetAllWeather()
        {
            var jsonPath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "cities.json");
            var result = await _weatherService.GetWeatherForAllCitiesAsync(jsonPath);
            return Ok(result);
        }
    }
}

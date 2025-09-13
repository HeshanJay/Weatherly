using Microsoft.AspNetCore.Mvc;
using WeatherApp.API.Services;

namespace WeatherApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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
        public async Task<IActionResult> GetWeather(int cityId)
        {
            var data = await _weatherService.GetWeatherByCityCodeAsync(cityId);
            return data == null ? NotFound() : Ok(data);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllWeather()
        {
            var jsonPath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "cities.json");
            var result = await _weatherService.GetWeatherForAllCitiesAsync(jsonPath);
            return Ok(result);
        }
    }
}

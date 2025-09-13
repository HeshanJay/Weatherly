using Microsoft.Extensions.Caching.Memory;
using System.Text.Json;
using Weatherly.API.Models.DTO;
using Weatherly.API.Models.Input;

namespace WeatherApp.API.Services
{
    public class WeatherService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;
        private readonly IMemoryCache _memoryCache;
        private readonly ILogger<WeatherService> _logger;


        public WeatherService(HttpClient httpClient, IConfiguration config, IMemoryCache memoryCache, ILogger<WeatherService> logger)
        {
            _httpClient = httpClient;
            _config = config;
            _memoryCache = memoryCache;
            _logger = logger;
        }

        // Get single city
        private async Task<WeatherResponseDto?> GetCityWeatherAsync(int cityId)
        {
            var apiKey = _config["OpenWeatherMap:ApiKey"];
            if (string.IsNullOrWhiteSpace(apiKey))
                throw new InvalidOperationException("OpenWeatherMap:ApiKey is not configured in appsettings.json");

            var url = $"https://api.openweathermap.org/data/2.5/weather?id={cityId}&appid={apiKey}&units=metric";


            var response = await _httpClient.GetFromJsonAsync<JsonElement?>(url);
            if (response == null || response.Value.ValueKind == JsonValueKind.Undefined)
                return null;

            var json = response.Value;

            try
            {
                var cityName = json.GetProperty("name").GetString() ?? string.Empty;

                string status = "";
                if (json.TryGetProperty("weather", out JsonElement weatherArr) && weatherArr.GetArrayLength() > 0)
                {
                    var first = weatherArr[0];
                    status = first.GetProperty("main").GetString() ?? "";
                }

                double tempDouble = 0;
                if (json.TryGetProperty("main", out JsonElement main) && main.TryGetProperty("temp", out JsonElement tempEl))
                {
                    if (tempEl.ValueKind == JsonValueKind.Number && tempEl.TryGetDouble(out double t))
                        tempDouble = t;
                }

                return new WeatherResponseDto
                {
                    CityCode = cityId.ToString(),
                    CityName = cityName,
                    Temp = tempDouble.ToString("0.0"),
                    Status = status
                };
            }
            catch
            {
                return null;
            }
        }

        public async Task<WeatherResponseDto?> GetWeatherByCityCodeAsync(int cityId)
        {
            return await GetCityWeatherAsync(cityId);
        }

        //public async Task<List<WeatherResponseDto>> GetWeatherForAllCitiesAsync(string jsonFilePath)
        //{
        //    if (!File.Exists(jsonFilePath))
        //        throw new FileNotFoundException("City JSON file not found.");

        //    var json = await File.ReadAllTextAsync(jsonFilePath);
        //    var cityList = JsonSerializer.Deserialize<CityListWrapper>(json);

        //    if (cityList?.List == null || !cityList.List.Any())
        //        return new List<WeatherResponseDto>();

        //    var tasks = cityList.List
        //        .Select(entry => int.TryParse(entry.CityCode, out int cityId)
        //            ? GetCityWeatherAsync(cityId)
        //            : Task.FromResult<WeatherResponseDto?>(null))
        //        .ToList();

        //    var results = await Task.WhenAll(tasks);
        //    return results.Where(r => r != null).ToList()!;
        //}

        public async Task<List<WeatherResponseDto>> GetWeatherForAllCitiesAsync(string jsonFilePath)
        {
            string cacheKey = "weather_all";

            if (_memoryCache.TryGetValue(cacheKey, out List<WeatherResponseDto> cachedList))
            {
                _logger.LogInformation("Cache hit for weather_all");
                return cachedList;
            }

            _logger.LogInformation("Cache miss for weather_all");

            if (!File.Exists(jsonFilePath))
                throw new FileNotFoundException("City JSON file not found.");

            var json = await File.ReadAllTextAsync(jsonFilePath);
            var cityList = JsonSerializer.Deserialize<CityListWrapper>(json);

            if (cityList?.List == null || !cityList.List.Any())
                return new List<WeatherResponseDto>();

            var tasks = cityList.List
                .Select(entry => int.TryParse(entry.CityCode, out int cityId)
                    ? GetCityWeatherAsync(cityId)
                    : Task.FromResult<WeatherResponseDto?>(null))
                .ToList();

            var results = await Task.WhenAll(tasks);
            var finalList = results.Where(r => r != null).ToList()!;

            _memoryCache.Set(cacheKey, finalList, TimeSpan.FromMinutes(5));

            return finalList;
        }
    }
}

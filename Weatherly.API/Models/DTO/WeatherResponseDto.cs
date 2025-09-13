namespace Weatherly.API.Models.DTO
{
    public class WeatherResponseDto
    {
        public string CityCode { get; set; }
        public string CityName { get; set; }
        public string Temp { get; set; }
        public string Status { get; set; }
    }
}

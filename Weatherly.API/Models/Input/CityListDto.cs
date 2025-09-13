namespace Weatherly.API.Models.Input
{
    public class CityListWrapper
    {
        public List<CityCodeEntry> List { get; set; } = new();
    }

    public class CityCodeEntry
    {
        public string CityCode { get; set; } = string.Empty;
    }
}

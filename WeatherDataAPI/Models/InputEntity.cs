using Newtonsoft.Json;

namespace WeatherDataAPI.Models
{
    public class InputEntity
    {
        [JsonProperty(PropertyName = "id")]
        public string CityId { get; set; }

        [JsonProperty(PropertyName = "City")]
        public string CityName { get; set; }
    }

}
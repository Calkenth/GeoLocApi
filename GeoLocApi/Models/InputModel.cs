using Newtonsoft.Json;

namespace GeoLocApi.Models
{
    public class InputModel
    {
        [JsonProperty("ip")]
        [JsonRequired]
        public string ip { get; set; }
    }
}

using Newtonsoft.Json;

namespace GeoLocApi.Models
{
    public class GeoLocation
    {
        public int ID { get; set; }

        public GeoLocation(string ip, string city, string country, string continent)
        {
            Ip = ip;
            City = city;
            Country = country;
            Continent = continent;
        }

        [JsonProperty("ip")]
        public string Ip { get; set; }
        [JsonProperty("city")]
        public string City { get; set; }
        [JsonProperty("country_name")]
        public string Country { get; set; }
        [JsonProperty("continent_name")]
        public string Continent { get; set; }
    }
}

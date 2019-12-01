using GeoLocApi.Constants;
using GeoLocApi.Models;
using Newtonsoft.Json;
using System.Net;

namespace GeoLocApi.Controllers
{
    internal class CheckIP : IpStackConts
    {
        private readonly string ip;

        internal CheckIP(string ip)
        {
            this.ip = ip;
        }

        internal GeoLocation CreateGeoLoc()
        {
            string url = $@"{IpStackUrl}{this.ip}{AccessKey}{AccessKeyValue}{IpStackFormat}";
            try
            {
                WebClient webClient = new WebClient();

                var jsonString = webClient.DownloadString(url);
                webClient.Dispose();

                GeoLocation geoLocation = JsonConvert.DeserializeObject<GeoLocation>(jsonString);

                return geoLocation;
            }
            catch(WebException ex)
            {
                throw ex;
            }
        }
    }
}
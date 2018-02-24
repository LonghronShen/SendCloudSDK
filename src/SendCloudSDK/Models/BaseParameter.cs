using Newtonsoft.Json;

namespace SendCloudSDK.Models
{
    public class BaseParameter
    {
        [JsonProperty("api_user")]
        public string ApiUser { get; set; }
        [JsonProperty("api_key")]
        public string ApiKey { get; set; }

    }
}

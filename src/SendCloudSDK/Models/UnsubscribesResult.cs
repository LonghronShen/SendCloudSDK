using System.Collections.Generic;
using Newtonsoft.Json;

namespace SendCloudSDK.Models
{
    public class UnsubscribesResult : BaseResult
    {
        [JsonProperty("unsubscribes")]
        public List<Unsubscribe> Unsubscribes { get; set; }
        [JsonProperty("del_count")]
        public int Count { get; set; }

    }

    public class Unsubscribe
    {
        [JsonProperty("email")]
        public string Email { get; set; }
        [JsonProperty("domain")]
        public string Domain { get; set; }
        [JsonProperty("labelId")]
        public int LabelId { get; set; }
        [JsonProperty("apiUser")]
        public string ApiUser { get; set; }
        [JsonProperty("create_at")]
        public string CreateTime { get; set; }
    }
}

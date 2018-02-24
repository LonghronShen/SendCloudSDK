using System.Collections.Generic;
using Newtonsoft.Json;

namespace SendCloudSDK.Models
{
    public class BaseResult
    {
        [JsonProperty("message")]
        public string Message { get; set; }
        [JsonProperty("errors")]
        public List<string> Errors { get; set; }
    }

    public class Errors
    {

    }
}

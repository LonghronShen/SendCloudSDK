using System;
using Newtonsoft.Json;

namespace SendCloudSDK.Models
{
    public class StatsParameter : BaseParameter
    {
        [JsonProperty("days")]
        public int Days { get; set; }
        [JsonProperty("start_date")]
        public DateTime? StartDate { get; set; }
        [JsonProperty("end_date")]
        public DateTime? EndDate { get; set; }
        [JsonProperty("api_user_list")]
        public string UserList { get; set; }
        [JsonProperty("label_id_list")]
        public string LabelList { get; set; }
        [JsonProperty("domain_list")]
        public string DomainList { get; set; }
        [JsonProperty("aggregate")]
        public bool Aggregate { get; set; }
    }
}

using Newtonsoft.Json;

namespace SendCloudSDK.Models
{
    public class LabelsParameter : BaseParameter
    {
        [JsonProperty("start")]
        public int? Start { get; set; }
        [JsonProperty("limit")]
        public int? Limit { get; set; }
    }

    public class LabelParameter : BaseParameter
    {
        [JsonProperty("labelId")]
        public string LabelId { get; set; }
        [JsonProperty("labelName")]
        public string LabelName { get; set; }
    }
}

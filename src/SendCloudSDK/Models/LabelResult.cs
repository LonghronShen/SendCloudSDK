using System.Collections.Generic;
using Newtonsoft.Json;

namespace SendCloudSDK.Models
{
    public class LabelsResult : BaseResult
    {
        [JsonProperty("list")]
        public List<Label> List { get; set; }
        [JsonProperty("totalCount")]
        public int TotalCount { get; set; }
    }

    public class LabelResult : BaseResult
    {
        [JsonProperty("label")]
        public Label Label { get; set; }
        [JsonProperty("updateCount")]
        public int UpdateCount { get; set; }
        [JsonProperty("deleteCount")]
        public int DeleteCount { get; set; }
    }

    public class Label
    {
        public int LabelId { get; set; }
        public string LabelName { get; set; }
    }
}

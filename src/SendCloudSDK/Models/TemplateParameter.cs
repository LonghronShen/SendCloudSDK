using Newtonsoft.Json;

namespace SendCloudSDK.Models
{
    public enum EmailTemplateType
    {
        Trigger = 0,
        Batch = 1
    }

    public class TemplateParameter : BaseParameter
    {
        [JsonProperty("invoke_name")]
        public string InvokeName { get; set; } //	邮件模板调用名称
        [JsonProperty("name")]
        public string Name { get; set; }//邮件模板名称
        [JsonProperty("html")]
        public string Html { get; set; }//html格式内容
        [JsonProperty("text")]
        public string Text { get; set; }//text格式内容
        [JsonProperty("subject")]
        public string Subject { get; set; }//模板标题
        [JsonProperty("email_type")]
        public int? EmailTemplateType { get; set; }//	模板类型
    }

    public class DeleteTemplateParameter : BaseParameter
    {
        [JsonProperty("invoke_name")]
        public string Invoke { get; set; }
    }

    public class TemplateGetParameter : BaseParameter
    {
        [JsonProperty("invoke_name")]
        public string InvokeName { get; set; } //	邮件模板调用名称
        [JsonProperty("start")]
        public int? Start { get; set; } //查询起始位置, 取值区间 [0-], 默认为 0
        [JsonProperty("limit")]
        public int? Limit { get; set; } //查询个数, 取值区间 [0-100], 默认为 100
    }
}

using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace SendCloudSDK.Models
{
    public class TemplateResult : BaseResult
    {
        [JsonProperty("templateList")]
        public List<TemplateGet> TemplateList { get; set; }
        [JsonProperty("addCount")]
        public int AddCount { get; set; }
        [JsonProperty("delCount")]
        public int DelCount { get; set; }
        [JsonProperty("updateCount")]
        public int UpdateCount { get; set; }
    }

    public class TemplateGet
    {
        [JsonProperty("invoke_name")]
        public string InvokeName { get; set; } //邮件模板调用名称
        [JsonProperty("name")]
        public string Name { get; set; } 	//邮件模板名称
        [JsonProperty("html")]
        public string Html { get; set; } 	//模板内容
        [JsonProperty("subject")]
        public string Subject { get; set; } 	//模板标题
        [JsonProperty("email_type")]
        public int EmailType { get; set; } //模板类型
        [JsonProperty("is_verify")]
        public bool IsVerify { get; set; } //审核状态
        [JsonProperty("gmt_created")]
        public DateTime CreateTime { get; set; } //邮件模板创建时间
        [JsonProperty("gmt_modified")]
        public DateTime UpdateTime { get; set; } 	//邮件模板更新时间
    }
}

using Newtonsoft.Json;

namespace SendCloudSDK.Models
{
    public class BaseSendParameter : BaseParameter
    {
        [JsonProperty("from")]
        public string From { get; set; }
        [JsonProperty("substitution_vars")]
        public string Vars { get; set; }
        [JsonProperty("to")]
        public string To { get; set; }
        [JsonProperty("subject")]
        public string Subject { get; set; }
        [JsonProperty("fromname")]
        public string FromName { get; set; }
        [JsonProperty("replyto")]
        public string ReplyTo { get; set; }
        [JsonProperty("label")]
        public int? Label { get; set; }
        [JsonProperty("headers")]
        public string Headers { get; set; }
        [JsonProperty("files")]
        public string FilePath { get; set; }
        [JsonProperty("resp_email_id")]
        public BoolEnum? IsReturnEid { get; set; }
        [JsonProperty("use_maillist")]
        public BoolEnum? EnableAddressList { get; set; }
        [JsonProperty("gzip_compress")]
        public BoolEnum? EnableCompress { get; set; }

    }

    public class SendParameter : BaseSendParameter
    {
        //抄送和密送只适合于普通发送
        [JsonProperty("bcc")]
        public string Bcc { get; set; }
        [JsonProperty("cc")]
        public string Cc { get; set; }
        [JsonProperty("html")]
        public string Content { get; set; }
        [JsonProperty("x_smtpapi")]
        public string XSmtpApi { get; set; }
    }

    public class SendTemplateParameter : SendParameter
    {
        [JsonProperty("template_invoke_name")]
        public string TemplateName { get; set; }
    }

    public enum BoolEnum
    {
        False = 0,
        True = 1
    }
}

using System;
using Newtonsoft.Json;

namespace SendCloudSDK.Models
{
    public class StatsBase
    {
        [JsonProperty("sendDate")]
        public DateTime SendDate { get; set; } //发送日期
        [JsonProperty("apiUser")]
        public string ApiUser { get; set; } //子账号
        [JsonProperty("labelId")]
        public int LabelId { get; set; } //标签ID
        [JsonProperty("labelName")]
        public string LabelName { get; set; } //标签名称
        [JsonProperty("domain")]
        public string Domian { get; set; } //域名
    }


    public class StatsResult : BaseResult
    {
        [JsonProperty("stats")]
        public Stats Stats { get; set; }
    }

    public class InvalidStatsResult : BaseResult
    {
        [JsonProperty("stats")]
        public InvalidStats Stats { get; set; }

    }

    public class InvalidStats : StatsBase
    {
        [JsonProperty("sendcloudBlacklist")]
        public int BlackNum { get; set; } //在sendcloud黑名单的个数
        [JsonProperty("ubsubscribe")]
        public int Ubsubscribe { get; set; } //在取消订阅列表的个数
        [JsonProperty("espUnvaliable")]
        public int EspUnvaliable { get; set; } //esp服务器不可达的个数
        [JsonProperty("recipientAddressError")]
        public int RecipientAddressError { get; set; } //收件人格式错误的个数
        [JsonProperty("ipOrDomainRejected")]
        public int IpOrDomainRejected { get; set; } //ip或者域名被拒个数
        [JsonProperty("recipientNotFound")]
        public int RecipientNotFound { get; set; } //收件人不存在个数
        [JsonProperty("spamEmails")]
        public int SpamEmails { get; set; } //垃圾邮件个数
        [JsonProperty("senderOrRecipientRejected")]
        public int SenderOrRecipientRejected { get; set; } //发信人或者收件人被拒个数
        [JsonProperty("others")]
        public int Others { get; set; } //其他
    }

    public class Stats : StatsBase
    {
        [JsonProperty("request")]
        public int Request { get; set; }//  请求总数
        [JsonProperty("deliveredNum")]
        public string DeliveredNum { get; set; }// 送达数量
        [JsonProperty("clickNum")]
        public long ClickNum { get; set; } //点击数量
        [JsonProperty("openNum")]
        public long OpenNum { get; set; } //打开数量
        [JsonProperty("bounceNum")]
        public long BounceNum { get; set; } //弹回数量
        [JsonProperty("spamReportedNum")]
        public long SpamReportedNum { get; set; } //垃圾举报数量
        [JsonProperty("unsubscribeNum")]
        public long UnsubscribeNum { get; set; } //取消订阅数量
        [JsonProperty("uniqueOpensNum")]
        public long UniqueOpensNum { get; set; } //独立打开数量
        [JsonProperty("uniqueClicksNum")]
        public long UniqueClicksNum { get; set; } //独立点击数量
        [JsonProperty("invalidEmailsNum")]
        public long InvalidEmailsNum { get; set; } 	//无效邮件数量
        [JsonProperty("delivered_percent")]
        public float DeliveredPercent { get; set; } 	//送达比例
        [JsonProperty("click_percent")]
        public float ClickPercent { get; set; } 	//点击比例
        [JsonProperty("open_percent")]
        public float OpenPercent { get; set; } //打开比例
        [JsonProperty("bounce_percent")]
        public float BouncePercent { get; set; } 	//弹回比例
        [JsonProperty("spamReported_percent")]
        public float SpamReportedPercent { get; set; } 	//垃圾举报比例
        [JsonProperty("unsubscribe_percent")]
        public float UnsubscribePercent { get; set; } 	//取消订阅比例
        [JsonProperty("uniqueOpens_percent")]
        public float UniqueOpensPercent { get; set; } 	//独立打开比例
        [JsonProperty("uniqueClicks_percent")]
        public float UniqueClicksPercent { get; set; } 	//独立点击比例
        [JsonProperty("invalidEmails_percent")]
        public float InvalidEmailsPercent { get; set; } 	//无效邮件比例
    }

}

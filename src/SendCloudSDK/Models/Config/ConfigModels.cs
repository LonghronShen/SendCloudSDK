namespace SendCloudSDK.Models.Config
{
    public class SendCloudApiConfig
    {
        public string Host { get; set; }
        public string ApiUser { get; set; }
        public string ApiKey { get; set; }
    }

    public class SendConfig
    {
        public string MailSend { get; set; }
        public string MailSendTemplate { get; set; }

    }

    public class TemplateConfig
    {
        public string TemplateGet { get; set; }
        public string TemplateCreate { get; set; }
        public string TemplateDelete { get; set; }
        public string TemplateUpdate { get; set; }
    }

    public class AddressListConfig
    {

    }

    public class LabelConfig
    {
        public string LabelGetList { get; set; }
        public string LabelGet { get; set; }
        public string LabelCreate { get; set; }
        public string LabelDelete { get; set; }
        public string LabelUpdate { get; set; }

    }

    public class StatusConfig
    {
    }

    public class DataStatisticsConfig
    {
        public string StatiGet { get; set; }
        public string StatiGetHour { get; set; }
        public string StatiGetInvalid { get; set; }

    }

    public class UserInfoConfig
    {
    }

    public class BouncesConfig
    {
    }

    public class UnsubscribesConfig
    {
        public string UnsubscribesGet { get; set; }
        public string UnsubscribesCreate { get; set; }
        public string UnsubscribesDelete { get; set; }

    }

    public class SpamReportedConfig
    {
    }

    public class WebHookConfig
    {
        public string Site { get; set; }

    }
}

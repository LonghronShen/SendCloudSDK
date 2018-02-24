using SendCloudSDK.Models.Config;

namespace SendCloudSDK
{
    internal class SendCloudConfig
    {
        public SendCloudApiConfig ApiConfig { get; set; }
        public SendConfig SendConfig { get; set; }
        public TemplateConfig TemplateConfig { get; set; }
        public AddressListConfig AddressListConfig { get; set; }
        public LabelConfig LabelConfig { get; set; }
        public StatusConfig StatusConfig { get; set; }
        public DataStatisticsConfig DataStatisticsConfig { get; set; }
        public UserInfoConfig UserInfoConfig { get; set; }
        public BouncesConfig BouncesConfig { get; set; }
        public UnsubscribesConfig UnsubscribesConfig { get; set; }
        public SpamReportedConfig SpamReportedConfig { get; set; }
        public WebHookConfig WebHookConfig { get; set; }
    }
}

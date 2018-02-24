/******************************************************************* 
 * FileName: SendCloudManager.cs
 * Author   : Qiang Kong
 * Date : 2015-09-09 09:52:09
 * Desc : 
 * 
 * 
 * *******************************************************************/ 
using System;
using System.Collections.Concurrent;
using System.Configuration;
using SendCloudSDK.Common;
using SendCloudSDK;
using SendCloudSDK.Models.Config;
using SendCloudSDK.Utis;
using SendCloudSDK.WebApi.EMailApi;
using SendCloudSDK.WebApi.Interface;
using SendCloudSDK.WebApi.SMSApi;

namespace SendCloudSDK
{
    public sealed class SendCloudManager
    {

        private static readonly object SyncObj = new object();
        private static SendCloudManager _instance;
        public static SendCloudManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (SyncObj)
                    {
                        if (_instance == null)
                        {
                            _instance = new SendCloudManager();
                        }
                    }
                }
                return _instance;
            }
        }

        private readonly ISendCloudWebApi _webApi;
        public ISendCloudWebApi WebApi
        {
            get { return _webApi; }
        }

        private SendCloudManager()
        {
            Logger.CreateImpl();
            ConfigHelper.CreateImpl();
            //初始化配置模型
            var config = InitConfig();
            if (config == null)
            {
                throw new ApiException("配置初始化异常，请检查SendCloud.config");
            }

            _webApi = new SendCloudWebApi(config);
        }

        private SendCloudConfig InitConfig()
        {
            try
            {
                var result = new SendCloudConfig()
                {
                    ApiConfig = ConfigHelper.GetInstance().GetSection<SendCloudApiConfig>(typeof(SendCloudApiConfig).Name),
                    SendConfig = ConfigHelper.GetInstance().GetSection<SendConfig>(typeof(SendConfig).Name, Constants.SectionGroupName),
                    TemplateConfig = ConfigHelper.GetInstance().GetSection<TemplateConfig>(typeof(TemplateConfig).Name, Constants.SectionGroupName),
                    AddressListConfig = ConfigHelper.GetInstance().GetSection<AddressListConfig>(typeof(AddressListConfig).Name, Constants.SectionGroupName),
                    LabelConfig = ConfigHelper.GetInstance().GetSection<LabelConfig>(typeof(LabelConfig).Name, Constants.SectionGroupName),
                    StatusConfig = ConfigHelper.GetInstance().GetSection<StatusConfig>(typeof(StatusConfig).Name, Constants.SectionGroupName),
                    DataStatisticsConfig = ConfigHelper.GetInstance().GetSection<DataStatisticsConfig>(typeof(DataStatisticsConfig).Name, Constants.SectionGroupName),
                    UserInfoConfig = ConfigHelper.GetInstance().GetSection<UserInfoConfig>(typeof(UserInfoConfig).Name, Constants.SectionGroupName),
                    BouncesConfig = ConfigHelper.GetInstance().GetSection<BouncesConfig>(typeof(BouncesConfig).Name, Constants.SectionGroupName),
                    UnsubscribesConfig = ConfigHelper.GetInstance().GetSection<UnsubscribesConfig>(typeof(UnsubscribesConfig).Name, Constants.SectionGroupName),
                    SpamReportedConfig = ConfigHelper.GetInstance().GetSection<SpamReportedConfig>(typeof(SpamReportedConfig).Name, Constants.SectionGroupName),
                    WebHookConfig = ConfigHelper.GetInstance().GetSection<WebHookConfig>(typeof(WebHookConfig).Name, Constants.SectionGroupName),
                };
                return result;
            }
            catch (Exception ex)
            {
                Logger.Instance.Write(ex, MessageType.Error);
                return null;
            }


        }
    }

    internal class SendCloudWebApi : ISendCloudWebApi
    {
        private readonly IScEmailWebApi _emailWebApi;
        public IScEmailWebApi EmailWebApi
        {
            get { return _emailWebApi; }
        }

        private readonly IScSmsWebApi _smsWebApi;
        public IScSmsWebApi SmsWebApi
        {
            get { return _smsWebApi; }
        }

        public SendCloudWebApi(SendCloudConfig config)
        {
            //实例化邮件 短信对象
            _emailWebApi = new EmailWebApi(config);
            _smsWebApi = new SmsWebApi(config);
        }
    }
}

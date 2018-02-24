/******************************************************************* 
 * FileName: EmailWebApi.cs
 * Author   : Qiang Kong
 * Date : 2015-09-09 09:52:58
 * Desc : 
 * 
 * 
 * *******************************************************************/
using System;
using System.Collections.Generic;
using CodeScales.Http.Entity;
using CodeScales.Http.Methods;
using SendCloudSDK.Models;
using SendCloudSDK.Utis;
using SendCloudSDK.WebApi.Interface;

namespace SendCloudSDK.WebApi.EMailApi
{
    internal class EmailWebApi : BaseWebApi, IScEmailWebApi
    {
        public EmailWebApi(SendCloudConfig config)
            : base(config)
        {
        }

        public SendResult SendEmail(SendParameter parameter)
        {
            return Execute<SendResult, SendParameter>(Config.SendConfig.MailSend, parameter, true);
        }

        public SendResult SendEmail(SendParameter parameter, string attachmentName, string attachmentPath)
        {
            string json = base.CallApi(Config.SendConfig.MailSend, parameter, attachmentName, attachmentPath);
            return JsonNet.DeserializeToString<SendResult>(json);
        }

        public SendResult SendTemplateEmail(SendTemplateParameter parameter)
        {
            return Execute<SendResult, SendTemplateParameter>(Config.SendConfig.MailSendTemplate, parameter, true);
        }

        public SendResult SendTemplateEmail(SendTemplateParameter parameter, string attachmentName, string attachmentPath)
        {
            string json = base.CallApi(Config.SendConfig.MailSendTemplate, parameter, attachmentName, attachmentPath);
            return JsonNet.DeserializeToString<SendResult>(json);
        }

        public TemplateResult GetTemplates()
        {
            return Execute<TemplateResult, TemplateGetParameter>(Config.SendConfig.MailSendTemplate, new TemplateGetParameter());
        }

        public TemplateResult GetFirstTemplate(string templateName)
        {
            return Execute<TemplateResult, TemplateGetParameter>(Config.TemplateConfig.TemplateGet, new TemplateGetParameter() { InvokeName = templateName });
        }

        public TemplateResult CreateTemplate(TemplateParameter parameter)
        {
            return Execute<TemplateResult, TemplateParameter>(Config.TemplateConfig.TemplateCreate, parameter);
        }

        public TemplateResult UpdateTemplate(TemplateParameter parameter)
        {
            return Execute<TemplateResult, TemplateParameter>(Config.TemplateConfig.TemplateUpdate, parameter);
        }

        public TemplateResult DeleteTemplate(DeleteTemplateParameter parameter)
        {
            return Execute<TemplateResult, DeleteTemplateParameter>(Config.TemplateConfig.TemplateDelete, parameter);
        }

        public LabelsResult GetLabels(LabelsParameter parameter)
        {
            string result = base.CallApi(Config.LabelConfig.LabelGetList, parameter);
            return JsonNet.DeserializeToString<LabelsResult>(result);
        }

        public LabelsResult GetLabel(LabelParameter parameter)
        {
            string result = base.CallApi(Config.LabelConfig.LabelGet, parameter);
            return JsonNet.DeserializeToString<LabelsResult>(result);
        }

        public LabelResult CreateLabel(LabelParameter parameter)
        {
            string result = base.CallApi(Config.LabelConfig.LabelCreate, parameter);
            return JsonNet.DeserializeToString<LabelResult>(result);
        }

        public LabelResult UpdateLabel(LabelParameter parameter)
        {
            string result = base.CallApi(Config.LabelConfig.LabelUpdate, parameter);
            return JsonNet.DeserializeToString<LabelResult>(result);
        }

        public LabelResult DeleteLabel(LabelParameter parameter)
        {
            string result = base.CallApi(Config.LabelConfig.LabelDelete, parameter);
            return JsonNet.DeserializeToString<LabelResult>(result);
        }

        public StatsResult GetByDays(StatsParameter parameter)
        {
            string result = base.CallApi(Config.DataStatisticsConfig.StatiGet, parameter);
            return JsonNet.DeserializeToString<StatsResult>(result);
        }

        public StatsResult GetByHours(StatsParameter parameter)
        {
            string result = base.CallApi(Config.DataStatisticsConfig.StatiGetHour, parameter);
            return JsonNet.DeserializeToString<StatsResult>(result);
        }

        public InvalidStatsResult GetinInvalids(StatsParameter parameter)
        {
            string result = base.CallApi(Config.DataStatisticsConfig.StatiGetInvalid, parameter);
            return JsonNet.DeserializeToString<InvalidStatsResult>(result);
        }

        public UnsubscribesResult GetUnsubscribes(UnsubscribesGetParameter parameter)
        {
            string result = base.CallApi(Config.UnsubscribesConfig.UnsubscribesGet, parameter);
            return JsonNet.DeserializeToString<UnsubscribesResult>(result);
        }

        public UnsubscribesResult CreateUnsubscribe(AddUnsubscribeParameter parameter)
        {
            string result = base.CallApi(Config.UnsubscribesConfig.UnsubscribesCreate, parameter);
            return JsonNet.DeserializeToString<UnsubscribesResult>(result);
        }

        public UnsubscribesResult DeleteUnsubscribe(DelUnsubscribeParameter parameter)
        {
            string result = base.CallApi(Config.UnsubscribesConfig.UnsubscribesDelete, parameter);
            return JsonNet.DeserializeToString<UnsubscribesResult>(result);
        }

    }
}

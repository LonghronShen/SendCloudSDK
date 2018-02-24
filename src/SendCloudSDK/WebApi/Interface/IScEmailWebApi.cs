/******************************************************************* 
 * FileName: IScEmailWebApi.cs
 * Author   : Qiang Kong
 * Date : 2015-09-09 09:53:15
 * Desc : 
 * 
 * 
 * *******************************************************************/
using SendCloudSDK.Models;

namespace SendCloudSDK.WebApi.Interface
{
    public interface IScEmailWebApi
    {
        SendResult SendEmail(SendParameter parameter);
        SendResult SendEmail(SendParameter parameter, string attachmentName, string attachmentPath);
        SendResult SendTemplateEmail(SendTemplateParameter parameter);
        SendResult SendTemplateEmail(SendTemplateParameter parameter, string attachmentName, string attachmentPath);

        TemplateResult GetTemplates();
        TemplateResult GetFirstTemplate(string invokeName);
        TemplateResult CreateTemplate(TemplateParameter parameter);
        TemplateResult UpdateTemplate(TemplateParameter parameter);
        TemplateResult DeleteTemplate(DeleteTemplateParameter parameter);

        LabelsResult GetLabels(LabelsParameter parameter);
        LabelsResult GetLabel(LabelParameter parameter);
        LabelResult CreateLabel(LabelParameter parameter);
        LabelResult UpdateLabel(LabelParameter parameter);
        LabelResult DeleteLabel(LabelParameter parameter);

        StatsResult GetByDays(StatsParameter parameter);
        StatsResult GetByHours(StatsParameter parameter);
        InvalidStatsResult GetinInvalids(StatsParameter parameter);

        UnsubscribesResult GetUnsubscribes(UnsubscribesGetParameter parameter);
        UnsubscribesResult CreateUnsubscribe(AddUnsubscribeParameter parameter);
        UnsubscribesResult DeleteUnsubscribe(DelUnsubscribeParameter parameter);


    }
}

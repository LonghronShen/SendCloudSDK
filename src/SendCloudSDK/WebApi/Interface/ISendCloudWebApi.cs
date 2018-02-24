/******************************************************************* 
 * FileName: ISendCloudWebApi.cs
 * Author   : Qiang Kong
 * Date : 2015-09-09 09:53:25
 * Desc : 
 * 
 * 
 * *******************************************************************/ 
namespace SendCloudSDK.WebApi.Interface
{
    public interface ISendCloudWebApi
    {
        IScEmailWebApi EmailWebApi { get; }
        IScSmsWebApi SmsWebApi { get; }
    }
}

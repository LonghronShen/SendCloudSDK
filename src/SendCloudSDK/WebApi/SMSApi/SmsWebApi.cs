/******************************************************************* 
 * FileName: SmsWebApi.cs
 * Author   : Qiang Kong
 * Date : 2015-09-09 09:52:47
 * Desc : 
 * 
 * 
 * *******************************************************************/ 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SendCloudSDK.WebApi.Interface;

namespace SendCloudSDK.WebApi.SMSApi
{
    internal class SmsWebApi : BaseWebApi, IScSmsWebApi
    {
        public SmsWebApi(SendCloudConfig config)
            : base(config)
        {
        }
    }
}

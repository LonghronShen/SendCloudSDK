using Newtonsoft.Json;
using SendCloudSDK.Config;
using SendCloudSDK.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace SendCloudSDK.Models
{

    public class SendCloudSms
    {

        [JsonProperty("templateId")]
        public int? TemplateId { get; set; }

        [JsonProperty("msgType")]
        public int? MsgType { get; set; } = 0;

        [JsonProperty("phone")]
        public List<String> Phone { get; set; } = new List<string>();

        [JsonProperty("vars")]
        public Dictionary<string, string> Vars { get; set; } = new Dictionary<string, string>();

        public string GetPhoneString()
        {
            return string.Join(",", this.Phone);
        }

        public string GetVarsString()
        {
            return this.Vars.ToJson();
        }

        public bool Validate()
        {
            if (this.TemplateId == null)
            {
                throw new SmsException("模版为空");
            }
            var phoneCount = this.Phone?.Count;
            if (phoneCount == 0)
            {
                throw new SmsException("收信人为空");
            }
            if (phoneCount > Configuration.MaxReceivers)
            {
                throw new SmsException("收信人超过限制");
            }
            return true;
        }

    }

}
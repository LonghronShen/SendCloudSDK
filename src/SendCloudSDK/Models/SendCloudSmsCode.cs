using Newtonsoft.Json;
using SendCloudSDK.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace SendCloudSDK.Models
{

    public class SendCloudSmsCode
    {

        [JsonProperty("phone")]
        public string Phone { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("labelId")]
        public int? LabelId { get; set; }

        public bool Validate()
        {
            if (string.IsNullOrWhiteSpace(this.Phone))
            {
                throw new SmsException("收信人为空");
            }
            if (string.IsNullOrWhiteSpace(this.Code))
            {
                throw new SmsException("验证码为空");
            }
            return true;
        }

    }

}
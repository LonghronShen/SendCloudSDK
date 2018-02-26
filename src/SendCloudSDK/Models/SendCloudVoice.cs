using Newtonsoft.Json;
using SendCloudSDK.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace SendCloudSDK.Models
{

    public class SendCloudVoice
    {

        [JsonProperty("phone")]
        public string Phone { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }

        public bool Validate()
        {
            if (string.IsNullOrEmpty(this.Phone))
            {
                throw new VoiceException("收信人为空");
            }
            if (string.IsNullOrEmpty(this.Code))
            {
                throw new VoiceException("验证码为空");
            }
            return true;
        }

    }

}
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace SendCloudSDK.Models
{

    /// <summary>
    /// {"phone":"1312222","vars":{},"message":"手机号格式错误"}
    /// </summary>
    public class SmsSendFailureItem
    {

        [JsonProperty("phone")]
        public string Phone { get; set; }

        [JsonProperty("vars")]
        public JObject Vars { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

    }

}
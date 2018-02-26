using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace SendCloudSDK.Models
{

    /// <summary>
    /// {
    ///   "successCount":1,
    ///   "failedCount":1,
    ///   "items":[{"phone":"1312222","vars":{},"message":"手机号格式错误"}],
    ///   "smsIds":["1458113381893_15_3_11_1ainnq$131112345678"]}
    /// }
    /// </summary>
    public class SendSmsResult
    {

        [JsonProperty("successCount")]
        public int SuccessCount { get; set; }

        [JsonProperty("failedCount")]
        public int FailedCount { get; set; }

        [JsonProperty("items")]
        public SmsSendFailureItem[] Items { get; set; }

        [JsonProperty("smsIds")]
        public string[] SmsIds { get; set; }

    }

}
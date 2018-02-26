using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace SendCloudSDK.Models
{

    public class TimeStampResult
    {

        [JsonProperty("timestamp")]
        public long TimeStamp { get; set; }

    }

}
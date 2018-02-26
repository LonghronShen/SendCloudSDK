using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace SendCloudSDK.Utils
{

    public class ResponseData<TResult>
    {

        [JsonProperty("result")]
        public bool Result { get; set; }

        [JsonProperty("statusCode")]
        public int StatusCode { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("info")]
        public TResult Info { get; set; }

        public override string ToString()
        {
            return this.ToJson();
        }

    }

}

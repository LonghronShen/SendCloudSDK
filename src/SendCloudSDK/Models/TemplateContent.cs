using Newtonsoft.Json;
using SendCloudSDK.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace SendCloudSDK.Models
{

    public class TemplateContent
        : IContent
    {

        /// <summary>
        /// 模版名称
        /// </summary>
        [JsonProperty("templateInvokeName")]
        public string TemplateInvokeName { get; set; }

        [JsonProperty("useTemplate")]
        public bool UseTemplate => true;

        public bool Validate()
        {
            if (string.IsNullOrEmpty(this.TemplateInvokeName))
            {
                throw new ContentException("模版为空");
            }
            return true;
        }

    }

}
using Newtonsoft.Json;
using SendCloudSDK.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace SendCloudSDK.Models
{

    public class TextContent
        : IContent
    {

        /// <summary>
        /// * <pre>
        /// * 邮件格式：text/html或者text/plain
        /// *
        /// * 默认text/html
        /// * </pre>
        /// </summary>
        public enum ScContentType
        {
            Html,
            Plain
        }

        [JsonProperty("useTemplate")]
        public bool UseTemplate => false;

        /// <summary>
        /// 邮件内容
        /// </summary>
        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("content_type")]
        public ScContentType ContentType { get; set; } = ScContentType.Html;

        public bool Validate()
        {
            if (string.IsNullOrWhiteSpace(this.Text))
            {
                throw new ContentException("邮件内容为空");
            }
            if (this.Text.Length > Config.Config.MaxContentSize)
            {
                throw new ContentException("邮件内容过长");
            }
            return true;
        }

    }

}
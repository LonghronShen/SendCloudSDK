using Newtonsoft.Json;
using SendCloudSDK.Exceptions;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Text;

namespace SendCloudSDK.Models
{

    public class MailBody
    {

        /// <summary>
        /// 发件人地址
        /// </summary>
        [JsonProperty("from")]
        public string From { get; set; }

        /// <summary>
        /// 邮件主题
        /// </summary>
        [JsonProperty("subject")]
        public string Subject { get; set; }

        /// <summary>
        /// 发件人名称
        /// </summary>
        [JsonProperty("fromName")]
        public string FromName { get; set; }

        /// <summary>
        /// 用户默认的回复邮件地址
        /// </summary>
        [JsonProperty("fromName")]
        public string ReplyTo { get; set; }

        /// <summary>
        /// 本次发送所使用的标签ID
        /// </summary>
        [JsonProperty("labelId")]
        public int? LabelId { get; set; }

        /// <summary>
        /// 邮件头部信息
        /// </summary>
        [JsonProperty("headers")]
        public Dictionary<string, string> Headers { get; protected set; } = new Dictionary<string, string>();

        /// <summary>
        /// 邮件附件
        /// </summary>
        [JsonProperty("attachments")]
        public List<Attachment> Attachments { get; protected set; } = new List<Attachment>();

        /// <summary>
        /// * <pre>
        /// * SMTP 扩展字段 X-SMTPAPI 是 SendCloud 为开发者提供的邮件个性化定制的处理方式, 开发者通过这个特殊的 信头扩展字段,
        /// * 可以设置邮件处理方式的很多参数.
        /// *
        /// * SMTP 调用时, 开发者可以在邮件中自行插入各种头域信息, 这是 SMTP 协议所允许的.而 SendCloud 会检索 key 为
        /// * X-SMTPAPI 的头域信息, 如果发现含有此头域, 则其 value 的值可以被解析, 用来改变邮件的处理方式.
        /// * </pre>
        /// </summary>
        [JsonProperty("xsmtpapi")]
        public Dictionary<string, object> XSmtpApi { get; protected set; } = new Dictionary<string, object>();

        /// <summary>
        /// 添加邮件头部信息
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void AddHeader(string key, string value)
        {
            this.Headers.Add(key, value);
        }

        /// <summary>
        /// 添加附件
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="name"></param>
        public void AddAttachments(Stream stream, string name)
        {
            this.Attachments.Add(new Attachment(stream, name));
        }

        /// <summary>
        /// 添加附件
        /// </summary>
        /// <param name="data"></param>
        /// <param name="name"></param>
        public void AddAttachments(byte[] data, string name)
        {
            this.Attachments.Add(new Attachment(new MemoryStream(data), name));
        }

        /// <summary>
        /// 添加附件
        /// </summary>
        /// <param name="fileInfo"></param>
        /// <param name="name"></param>
        public void AddAttachments(FileInfo fileInfo, string name)
        {
            this.Attachments.Add(new Attachment(fileInfo.OpenRead(), name));
        }

        /// <summary>
        /// 添加xsmtpapi
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void AddXSmtpapi(string key, object value)
        {
            if (this.XSmtpApi == null)
            {
                this.XSmtpApi = new Dictionary<string, object>();
            }
            this.XSmtpApi.Add(key, value);
        }

        public bool Validate()
        {
            if (string.IsNullOrWhiteSpace(this.From))
            {
                throw new BodyException("发件人为空");
            }
            if (string.IsNullOrWhiteSpace(this.Subject))
            {
                throw new BodyException("邮件主题为空");
            }
            return true;
        }

    }

}
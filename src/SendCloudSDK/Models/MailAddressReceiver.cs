using Newtonsoft.Json;
using SendCloudSDK.Config;
using SendCloudSDK.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SendCloudSDK.Models
{

    public class MailAddressReceiver
        : IReceiver
    {

        /// <summary>
        /// 广播发送(收件人会全部显示)
        /// </summary>
        [JsonProperty("broadcastSend")]
        public bool IsBroadcastSend { get; set; } = true;

        /// <summary>
        /// 收件人
        /// </summary>
        [JsonProperty("to")]
        public List<string> To { get; protected set; } = new List<string>();

        /// <summary>
        /// 抄送
        /// </summary>
        [JsonProperty("cc")]
        public List<string> Cc { get; protected set; } = new List<string>();

        /// <summary>
        /// 密送
        /// </summary>
        [JsonProperty("bcc")]
        public List<string> Bcc { get; protected set; } = new List<string>();

        [JsonProperty("useAddressList")]
        public bool UseAddressList => true;

        public bool Validate()
        {
            var receivers = this.To?.Count;
            if (receivers == 0)
            {
                throw new ReceiverException("收件人为空");
            }
            if (this.Cc?.Count > 0)
            {
                receivers += this.Cc?.Count;
            }
            if (this.Bcc?.Count > 0)
            {
                receivers += this.Bcc?.Count;
            }
            if (receivers > Configuration.MaxReceivers)
            {
                throw new ReceiverException("收件人超出上限");
            }
            return true;
        }

        public string GetCcString()
        {
            if (this.Cc?.Count == 0) return null;
            return string.Join(";", this.Cc.Where(x => !string.IsNullOrEmpty(x)));
        }

        public string GetBccString()
        {
            if (this.Bcc?.Count == 0) return null;
            return string.Join(";", this.Bcc.Where(x => !string.IsNullOrEmpty(x)));
        }

        public override string ToString()
        {
            return string.Join(";", this.To.Where(x => !string.IsNullOrEmpty(x)));
        }

    }

}
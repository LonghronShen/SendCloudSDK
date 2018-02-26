using Newtonsoft.Json;
using SendCloudSDK.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace SendCloudSDK.Models
{

    public class AddressListReceiver
        : IReceiver
    {

        [JsonProperty("useAddressList")]
        public bool UseAddressList => true;

        [JsonProperty("invokeNames")]
        public List<string> InvokeNames { get; } = new List<string>();

        public void AddTo(string to)
        {
            this.InvokeNames.Add(to);
        }

        public bool Validate()
        {
            var count = this.InvokeNames?.Count;
            if (count == 0)
            {
                throw new ReceiverException("地址列表为空");
            }
            if (count > Config.Config.MaxMailList)
            {
                throw new ReceiverException("地址列表超过上限");
            }
            return true;
        }

    }

}
using System;
using System.Collections.Generic;
using System.Text;

namespace SendCloudSDK.Config
{

    public class Credential
    {

        public string ApiUser { get; set; }

        public string ApiKey { get; set; }

        public Credential(string apiUser, string apiKey)
        {
            this.ApiUser = apiUser;
            this.ApiKey = apiKey;
        }

    }

}

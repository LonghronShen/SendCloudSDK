using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Text;

namespace SendCloudSDK.Models
{

    public class SendCloudMail
    {

        public MailBody Body { get; set; }

        public IReceiver To { get; set; }

        public IContent Content { get; set; }

        public void Validate()
        {
            Contract.Assert(this.Body != null);
            this.Body.Validate();
            Contract.Assert(this.Content != null);
            this.Content.Validate();
            this.To?.Validate();
        }

    }

}
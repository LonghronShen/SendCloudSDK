using System;
using System.Collections.Generic;
using System.Text;

namespace SendCloudSDK.Exceptions
{

    public class ContentException
        : SCException
    {

        public ContentException(string message)
            : base(message, 1L, 302)
        {
        }

    }

}
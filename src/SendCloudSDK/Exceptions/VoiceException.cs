using System;
using System.Collections.Generic;
using System.Text;

namespace SendCloudSDK.Exceptions
{

    public class VoiceException
        : SCException
    {

        public VoiceException(string message)
            : base(message, 1L, 402)
        {
        }

    }

}
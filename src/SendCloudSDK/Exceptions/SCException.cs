using System;
using System.Collections.Generic;
using System.Text;

namespace SendCloudSDK.Exceptions
{

    public abstract class SCException
        : Exception, ISCException
    {

        public long SerialVersionUID { get; protected set; }

        public int ErrorCode { get; protected set; }

        public SCException(string message, long serialVersionUID, int errorCode)
            : base(message)
        {
            this.SerialVersionUID = serialVersionUID;
            this.ErrorCode = errorCode;
        }

        public override string ToString()
        {
            return $"code:{this.ErrorCode},message:{this.Message}";
        }

    }

}
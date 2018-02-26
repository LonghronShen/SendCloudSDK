using System;
using System.Collections.Generic;
using System.Text;

namespace SendCloudSDK.Models
{

    public enum SmsHookStatusCodes
    {
        PhoneNumberBlockedInGlobalBlacklist = 410,
        PhoneNumberBlockedInLocalBlacklist = 420,
        SubscriptionCanceled = 430,
        BlockedBecauseOfBadKeyword = 440,
        VariableInjectionError = 450,
        SmsLengthExceed500Chars = 460,
        EmptyPhoneNumber = 500,
        PhoneNumberStopped = 510,
        TemplateBeenComplained = 550,
        PhoneShutdown = 580,
        FailedWithOtherReasons = 590
    }

}
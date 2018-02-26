using System;
using System.Collections.Generic;
using System.Text;

namespace SendCloudSDK.Models
{

    public enum ApiReturnCodes
    {
        Success = 200,
        SmsPartiallySent = 301,
        AllFailed = 312,
        PhoneNumberNull = 411,
        WrongPhoneNumberFormat = 412,
        DuplicatedPhoneNumberFound = 413,
        SignatureParamError = 421,
        SignatureVerificationFailed = 422,
        TemplateNotExist = 431,
        TemplateNotCommitedOrChecked = 432,
        NullTemplateId = 433,
        InvalidVariableFormat = 441,
        InvalidTimeFormat = 451,
        TimeEarlierThanServer = 452,
        InvalidTimeStamp = 461,
        SmsUserNotExist = 471,
        NullSmsUser = 472,
        FreeUserInsufficientPrivilege = 473,
        UserNotExist = 474,
        NullPhoneNumberAndVariable = 481,
        WrongPhoneNumberAndVariableFormat = 482,
        VariableExceed32Chars = 483,
        OutOfCredit = 499,
        ServerError = 501,
        NotAuthorized = 601
    }

}
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SendCloudSDK.Config;
using SendCloudSDK.Models;
using SendCloudSDK.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Net.Http;
using System.Threading.Tasks;

namespace SendCloudSDK.Core
{

    public class SendCloud
    {

        #region Properties
        public Configuration Configuration { get; set; }

        public Action<HttpResponseMessage, Exception> DefaultExceptionHandler { get; set; }
        #endregion

        #region Constructors
        public SendCloud(Configuration config, Action<HttpResponseMessage, Exception> defaultExceptionHandler = null)
        {
            this.Configuration = config;
            this.DefaultExceptionHandler = defaultExceptionHandler;
        }

        public SendCloud(string apiUser, string apiKey, string smsUser, string smsKey, Action<HttpResponseMessage, Exception> defaultExceptionHandler = null)
            : this(Configuration.LoadDefaultConfiguration(apiUser, apiKey, smsUser, smsKey))
        {
            this.DefaultExceptionHandler = defaultExceptionHandler;
        }
        #endregion

        #region APIS
        /// <summary>
        /// Get current server timestamp.
        /// </summary>
        /// <returns></returns>
        public async Task<ResponseData<TimeStampResult>> GetServerTimeStampAsync(bool fromLocal = false)
        {
            if (fromLocal)
            {
                return await Task.FromResult(new ResponseData<TimeStampResult>()
                {
                    Info = new TimeStampResult()
                    {
                        TimeStamp = DateTime.Now.ToUnixTimeStamp()
                    }
                });
            }
            else
            {
                using (var client = new HttpClient())
                {
                    var response = await client.GetAsync(this.Configuration.TimeStampAPI);
                    return await this.ValidateAsync<TimeStampResult>(response);
                }
            }
        }

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="mail"></param>
        /// <returns></returns>
        public async Task<ResponseData<dynamic>> SendMailAsync(SendCloudMail mail)
        {
            if (mail == null) throw new ArgumentNullException(nameof(mail));
            if (string.IsNullOrWhiteSpace(this.Configuration.ApiUser)) throw new ArgumentNullException(nameof(this.Configuration.ApiUser));
            if (string.IsNullOrWhiteSpace(this.Configuration.ApiKey)) throw new ArgumentNullException(nameof(this.Configuration.ApiKey));
            Contract.EndContractBlock();

            mail.Validate();

            var credential = new Credential(this.Configuration.ApiUser, this.Configuration.ApiKey);
            if (mail?.Body?.Attachments?.Count == 0)
            {
                return await this.SendEmailAsync(credential, mail);
            }
            else
            {
                return await this.SendMultipartEmailAsync(credential, mail);
            }
        }

        /// <summary>
        /// 发送短信
        /// </summary>
        /// <param name="sms"></param>
        /// <returns></returns>
        public async Task<ResponseData<SendSmsResult>> SendSmsAsync(SendCloudSms sms)
        {
            if (sms == null) throw new ArgumentNullException(nameof(sms));
            if (string.IsNullOrWhiteSpace(this.Configuration.SmsUser)) throw new ArgumentNullException(nameof(this.Configuration.SmsUser));
            if (string.IsNullOrWhiteSpace(this.Configuration.SmsKey)) throw new ArgumentNullException(nameof(this.Configuration.SmsKey));
            Contract.EndContractBlock();

            sms.Validate();

            var timestampResponse = await this.GetServerTimeStampAsync();
            var timestamp = timestampResponse.Info.TimeStamp;

            var credential = new Credential(this.Configuration.SmsUser, this.Configuration.SmsKey);
            var treeMap = new SortedDictionary<string, string>();
            treeMap.Add("smsUser", credential.ApiUser);
            treeMap.Add("msgType", sms.MsgType.ToString());
            treeMap.Add("phone", sms.GetPhoneString());
            treeMap.Add("templateId", sms.TemplateId.ToString());
            treeMap.Add("timestamp", timestamp.ToString());
            if (sms.Vars?.Count > 0)
            {
                treeMap.Add("vars", sms.GetVarsString());
            }

            var signature = Md5Utils.MD5Signature(treeMap, credential.ApiKey);
            treeMap.Add("signature", signature);

            using (var client = new HttpClient())
            {
                var response = await client.PostAsync(this.Configuration.SendSmsApi, new FormUrlEncodedContent(treeMap));
                return await this.ValidateAsync<SendSmsResult>(response);
            }
        }

        /// <summary>
        /// 发送短信验证码
        /// </summary>
        /// <param name="smsCode"></param>
        /// <returns></returns>
        public async Task<ResponseData<SendSmsResult>> SendSmsCodeAsync(SendCloudSmsCode smsCode)
        {
            if (smsCode == null) throw new ArgumentNullException(nameof(smsCode));
            if (string.IsNullOrWhiteSpace(this.Configuration.SmsUser)) throw new ArgumentNullException(nameof(this.Configuration.SmsUser));
            if (string.IsNullOrWhiteSpace(this.Configuration.SmsKey)) throw new ArgumentNullException(nameof(this.Configuration.SmsKey));
            Contract.EndContractBlock();

            smsCode.Validate();

            var timestampResponse = await this.GetServerTimeStampAsync();
            var timestamp = timestampResponse.Info.TimeStamp;

            var credential = new Credential(this.Configuration.SmsUser, this.Configuration.SmsKey);
            var treeMap = new SortedDictionary<string, string>();
            treeMap.Add("smsUser", credential.ApiUser);
            treeMap.Add("code", smsCode.Code);
            treeMap.Add("phone", smsCode.Phone);
            treeMap.Add("timestamp", timestamp.ToString());
            if (smsCode.LabelId != null)
            {
                treeMap.Add("labelId", smsCode.LabelId.Value.ToString());
            }

            var signature = Md5Utils.MD5Signature(treeMap, credential.ApiKey);
            treeMap.Add("signature", signature);

            using (var client = new HttpClient())
            {
                var response = await client.PostAsync(this.Configuration.SendSmsApi, new FormUrlEncodedContent(treeMap));
                return await this.ValidateAsync<SendSmsResult>(response);
            }
        }

        /// <summary>
        /// 发送语音
        /// </summary>
        /// <param name="voice"></param>
        /// <returns></returns>
        public async Task<ResponseData<dynamic>> SendVoiceAsync(SendCloudVoice voice)
        {
            if (voice == null) throw new ArgumentNullException(nameof(voice));
            if (string.IsNullOrWhiteSpace(this.Configuration.SmsUser)) throw new ArgumentNullException(nameof(this.Configuration.SmsUser));
            if (string.IsNullOrWhiteSpace(this.Configuration.SmsKey)) throw new ArgumentNullException(nameof(this.Configuration.SmsKey));
            Contract.EndContractBlock();

            voice.Validate();

            var timestampResponse = await this.GetServerTimeStampAsync();
            var timestamp = timestampResponse.Info.TimeStamp;

            var credential = new Credential(this.Configuration.SmsUser, this.Configuration.SmsKey);
            var treeMap = new SortedDictionary<string, string>();
            treeMap.Add("smsUser", credential.ApiUser);
            treeMap.Add("phone", voice.Phone);
            treeMap.Add("code", voice.Code);
            treeMap.Add("timestamp", timestamp.ToString());

            var signature = Md5Utils.MD5Signature(treeMap, credential.ApiKey);
            treeMap.Add("signature", signature);

            using (var client = new HttpClient())
            {
                var response = await client.PostAsync(this.Configuration.SendVoiceApi, new FormUrlEncodedContent(treeMap));
                return await this.ValidateAsync(response);
            }
        }
        #endregion

        #region Internal
        /// <summary>
        /// 普通方式发送
        /// </summary>
        /// <param name="credential"></param>
        /// <param name="mail"></param>
        /// <returns></returns>
        protected virtual async Task<ResponseData<dynamic>> SendEmailAsync(Credential credential, SendCloudMail mail)
        {
            using (var client = new HttpClient())
            {
                var @params = new Dictionary<string, string>();

                @params.Add("apiUser", credential.ApiUser);
                @params.Add("apiKey", credential.ApiKey);
                @params.Add("from", mail.Body.From);
                @params.Add("fromName", mail.Body.FromName);
                @params.Add("subject", mail.Body.Subject);
                @params.Add("replyTo", mail.Body.ReplyTo);

                if (mail.Body.LabelId != null)
                {
                    @params.Add("replyTo", mail.Body.LabelId.Value.ToString());
                }

                if (mail.Content.UseTemplate)
                {
                    var content = (TemplateContent)mail.Content;
                    @params.Add("templateInvokeName", content.TemplateInvokeName);
                }
                else
                {
                    var content = (TextContent)mail.Content;
                    @params.Add(content.ContentType == TextContent.ScContentType.Html ? "html" : "plain", content.Text);
                }

                if (mail.To != null)
                {
                    if (mail.To.UseAddressList)
                    {
                        @params.Add("useAddressList", "true");
                        @params.Add("to", mail.To.ToString());
                    }
                    else
                    {
                        var receiver = (MailAddressReceiver)mail.To;
                        if (!mail.Content.UseTemplate && receiver.IsBroadcastSend)
                        {
                            @params.Add("to", receiver.ToString());
                            @params.Add("cc", receiver.GetCcString());
                            @params.Add("bcc", receiver.GetBccString());
                        }
                        else
                        {
                            if (mail.Body.XSmtpApi != null && !mail.Body.XSmtpApi.ContainsKey("to"))
                            {
                                mail.Body.AddXSmtpapi("to", receiver.To.ToJson());
                            }
                        }
                    }
                }

                if (mail.Body.Headers?.Count > 0)
                {
                    @params.Add("headers", mail.Body.Headers.ToJson());
                }

                if (mail.Body.XSmtpApi?.Count > 0)
                {
                    @params.Add("xsmtpapi", mail.Body.XSmtpApi.ToJson());
                }

                @params.Add("respEmailId", "true");
                @params.Add("useNotification", "false");

                var url = mail.Content.UseTemplate ? this.Configuration.SendTemplateApi : this.Configuration.SendEmailApi;
                var response = await client.PostAsync(url, new FormUrlEncodedContent(@params));
                return await this.ValidateAsync(response);
            }
        }

        /// <summary>
        /// multipart方式发送
        /// </summary>
        /// <param name="credential"></param>
        /// <param name="mail"></param>
        /// <returns></returns>
        protected virtual async Task<ResponseData<dynamic>> SendMultipartEmailAsync(Credential credential, SendCloudMail mail)
        {
            using (var multiPartContent = new MultipartFormDataContent())
            {
                multiPartContent.Add(new StringContent(credential.ApiUser), "apiUser");
                multiPartContent.Add(new StringContent(credential.ApiKey), "apiKey");
                multiPartContent.Add(new StringContent(mail.Body.From), "from");
                if (!string.IsNullOrWhiteSpace(mail.Body.FromName))
                {
                    multiPartContent.Add(new StringContent(mail.Body.FromName), "from");
                }
                multiPartContent.Add(new StringContent(mail.Body.Subject), "subject");
                if (!string.IsNullOrWhiteSpace(mail.Body.ReplyTo))
                {
                    multiPartContent.Add(new StringContent(mail.Body.ReplyTo), "replyTo");
                }
                if (mail.Body.LabelId != null)
                {
                    multiPartContent.Add(new StringContent(mail.Body.LabelId.Value.ToString()), "labelId");
                }

                // 是否使用模版发送
                if (mail.Content.UseTemplate)
                {
                    var content = (TemplateContent)mail.Content;
                    multiPartContent.Add(new StringContent(content.TemplateInvokeName), "templateInvokeName");
                }
                else
                {
                    var content = (TextContent)mail.Content;
                    if (content.ContentType == TextContent.ScContentType.Html)
                    {
                        multiPartContent.Add(new StringContent(content.Text), "html");
                    }
                    else
                    {
                        multiPartContent.Add(new StringContent(content.Text), "plain");
                    }
                }

                // 是否使用地址列表
                if (mail.To != null)
                {
                    if (mail.To.UseAddressList)
                    {
                        multiPartContent.Add(new StringContent("true"), "useAddressList");
                        multiPartContent.Add(new StringContent(mail.To.ToString()), "useAddressList");
                    }
                    else
                    {
                        var receiver = (MailAddressReceiver)mail.To;
                        if (!mail.Content.UseTemplate && receiver.IsBroadcastSend)
                        {
                            multiPartContent.Add(new StringContent(receiver.ToString()), "to");
                            var ccString = receiver.GetCcString();
                            if (!string.IsNullOrWhiteSpace(ccString))
                            {
                                multiPartContent.Add(new StringContent(ccString), "cc");
                            }
                            var bccString = receiver.GetBccString();
                            if (!string.IsNullOrWhiteSpace(bccString))
                            {
                                multiPartContent.Add(new StringContent(bccString), "bcc");
                            }
                        }
                        else
                        {
                            if (mail.Body.XSmtpApi?.Count == 0 || !mail.Body.XSmtpApi.ContainsKey("to"))
                            {
                                mail.Body.AddXSmtpapi("to", receiver.To.ToJson());
                            }
                        }
                    }
                }

                if (mail.Body.Headers?.Count > 0)
                {
                    multiPartContent.Add(new StringContent(mail.Body.Headers.ToJson()), "headers");
                }

                if (mail.Body.XSmtpApi?.Count > 0)
                {
                    multiPartContent.Add(new StringContent(mail.Body.XSmtpApi.ToJson()), "xsmtpapi");
                }

                multiPartContent.Add(new StringContent("true"), "respEmailId");
                multiPartContent.Add(new StringContent("false"), "useNotification");

                foreach (var item in mail.Body.Attachments)
                {
                    // application/octet-stream
                    multiPartContent.Add(new StreamContent(item.Content), item.Name);
                }

                var url = mail.Content.UseTemplate ? this.Configuration.SendTemplateApi : this.Configuration.SendEmailApi;
                using (var client = new HttpClient())
                {
                    var response = await client.PostAsync(url, multiPartContent);
                    return await this.ValidateAsync(response);
                }
            }
        }

        protected virtual async Task<ResponseData<dynamic>> ValidateAsync(HttpResponseMessage response)
        {
            return await this.ValidateAsync<dynamic>(response);
        }

        /// <summary>
        /// 解析返回结果
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        protected virtual async Task<ResponseData<TResult>> ValidateAsync<TResult>(HttpResponseMessage response)
        {
            var s = await response.Content.ReadAsStringAsync();
            try
            {
                var json = JObject.Parse(s);
                if (json.Property("statusCode") != null)
                {
                    return json.ToObject<ResponseData<TResult>>();
                }
                else
                {
                    return new ResponseData<TResult>()
                    {
                        StatusCode = 500,
                        Message = s
                    };
                }
            }
            catch (Exception ex)
            {
                this.OnError(response, ex);
            }
            return new ResponseData<TResult>()
            {
                StatusCode = (int)response.StatusCode,
                Message = "发送失败",
                Result = false
            };
        }

        protected virtual void OnError(HttpResponseMessage response, Exception ex)
        {
            if (this.DefaultExceptionHandler == null)
            {
                Debug.WriteLine(JsonConvert.SerializeObject(new
                {
                    Exception = ex.Message + Environment.NewLine + ex.StackTrace,
                    Response = response
                }));
            }
            this.DefaultExceptionHandler.Invoke(response, ex);
        }
        #endregion

    }

}
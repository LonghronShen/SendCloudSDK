using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SendCloudSDK.Config
{

    public class Configuration
    {

        #region Constants
        /// <summary>
        /// Default character set.
        /// </summary>
        public const string CharSet = "utf-8";

        /// <summary>
        /// 最大收件人数
        /// </summary>
        public const int MaxReceivers = 100;

        /// <summary>
        /// 最大地址列表数
        /// </summary>
        public const int MaxMailList = 5;

        /// <summary>
        /// 邮件内容大小
        /// </summary>
        public const int MaxContentSize = 1024 * 1024;
        #endregion

        #region Properties
        /// <summary>
        /// Server host
        /// </summary>
        public string Server { get; set; }

        /// <summary>
        /// 普通邮件发送
        /// </summary>
        public string SendEmailApi { get; set; }

        /// <summary>
        /// 地址列表发送
        /// </summary>
        public string SendTemplateApi { get; set; }

        /// <summary>
        /// 短信发送
        /// </summary>
        public string SendSmsApi { get; set; }

        /// <summary>
        /// 语音发送
        /// </summary>
        public string SendVoiceApi { get; set; }

        /// <summary>
        /// TimeStamp Api.
        /// </summary>
        public string TimeStampAPI { get; set; }

        /// <summary>
        /// 邮件user
        /// </summary>
        public string ApiUser { get; set; }

        /// <summary>
        /// 邮件key
        /// </summary>
        public string ApiKey { get; set; }

        /// <summary>
        /// 短信user
        /// </summary>
        public string SmsUser { get; set; }

        /// <summary>
        /// 短信key
        /// </summary>
        public string SmsKey { get; set; }
        #endregion

        public static Configuration LoadDefaultConfiguration()
        {
            try
            {
                var asm = typeof(Configuration).Assembly;
                var embededResourceNames = asm.GetManifestResourceNames();
                foreach (var item in embededResourceNames)
                {
                    if (item.EndsWith("DefaultConfig.json"))
                    {
                        using (var stream = asm.GetManifestResourceStream(item))
                        {
                            using (TextReader tr = new StreamReader(stream))
                            {
                                var configFile = tr.ReadToEnd();
                                return JsonConvert.DeserializeObject<Configuration>(configFile);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + Environment.NewLine + ex.StackTrace);
            }
            return null;
        }

        public static Configuration LoadConfigurationFromString(string fileContent)
        {
            try
            {
                return JsonConvert.DeserializeObject<Configuration>(fileContent);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + Environment.NewLine + ex.StackTrace);
            }
            return null;
        }

    }

}

using Newtonsoft.Json;
using SendCloudSDK.Config;
using SendCloudSDK.Core;
using SendCloudSDK.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using Xunit;

namespace SendCloudSDK.Test
{

    public class SendCloudTests
    {

        private readonly SendCloud _sendCloud;

        public SendCloudTests()
        {
            var configuration = File.ReadAllText("SendCloud.Development.json");
            this._sendCloud = new SendCloud(Configuration.LoadConfigurationFromString(configuration), (response, ex) =>
            {
                Debug.WriteLine(ex.Message + Environment.NewLine + ex.StackTrace);
            });
        }

        [Fact]
        public void ImplTest()
        {
            Assert.NotNull(this._sendCloud);
        }

        [Fact]
        public async void SendEmailTest()
        {
            var response = await this._sendCloud.SendMailAsync(new SendCloudMail()
            {

            });
            Assert.True(response.StatusCode == 200);
        }

        [Fact]
        public void SendTemplateEmailTest()
        {

        }

        [Fact]
        public async void SendSmsTest()
        {
            var response = await this._sendCloud.SendSmsAsync(new SendCloudSms()
            {
                Phone = new List<string>() { "15262916011" },
                TemplateId = 12570,
                Vars = new Dictionary<string, string>()
                {
                    { "cluster", "021-上海第一人民医院" },
                    { "service", "DB" },
                    { "time", DateTime.Now.ToString() },
                }
            });
            Assert.True(response.StatusCode == 200, JsonConvert.SerializeObject(response));
        }

        [Fact]
        public void SendSmsCodeTest()
        {

        }

        [Fact]
        public void SendVoiceTest()
        {

        }

        [Fact]
        public void CreateLabelTest()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public void GetSendStatusTest()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public void GetTemplateTest()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public void CreateTemplateTest()
        {
            throw new NotImplementedException();
        }

    }

}
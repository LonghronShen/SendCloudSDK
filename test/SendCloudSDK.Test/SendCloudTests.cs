using SendCloudSDK.Core;
using SendCloudSDK.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

            this._sendCloud = new SendCloud("", "", "", "", (response, ex) =>
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
        public void SendSmsTest()
        {

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
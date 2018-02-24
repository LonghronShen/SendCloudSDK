using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CodeScales.Http;
using CodeScales.Http.Common;
using CodeScales.Http.Entity;
using CodeScales.Http.Methods;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SendCloudSDK.Models;
using SendCloudSDK.WebApi.Interface;

namespace SendCloudSDK.Test
{
    [TestClass]
    public class ScEmailTest
    {
        private readonly IScEmailWebApi _emailApi;

        public ScEmailTest()
        {
            _emailApi = SendCloudManager.Instance.WebApi.EmailWebApi;
        }

        [TestMethod]
        public void ImplTest()
        {
            var result = SendCloudManager.Instance;
            Assert.IsTrue(true);
        }


        [TestMethod]
        public void Send()
        {
            HttpClient client = new HttpClient();
            HttpPost postMethod = new HttpPost(new Uri("http://sendcloud.sohu.com/webapi/label.create.json"));

            //MultipartEntity multipartEntity = new MultipartEntity();
            //multipartEntity.AddBody(new StringBody(Encoding.UTF8, "api_user", ""));
            //multipartEntity.AddBody(new StringBody(Encoding.UTF8, "api_key", ""));
            //multipartEntity.AddBody(new StringBody(Encoding.UTF8, "labelName", "hhLabel"));


            UrlEncodedFormEntity urlFormEntity = new UrlEncodedFormEntity(new List<NameValuePair>()
            {
                new NameValuePair(){Name = "api_user",Value = ""},
                new NameValuePair(){Name = "api_key",Value = ""},
                new NameValuePair(){Name = "labelName",Value = ""}
            }, Encoding.UTF8);

            postMethod.Entity = urlFormEntity;

            HttpResponse response = client.Execute(postMethod);

            Console.WriteLine("Response Code: " + response.ResponseCode);
            Console.WriteLine("Response Content: " + EntityUtils.ToString(response.Entity));
        }


        [TestMethod]
        public void SendTemplateEmail()
        {
            var result = _emailApi.SendTemplateEmail(new SendTemplateParameter()
            {
                TemplateName = "test_template_active",

                Vars = "{\"to\":[\"khadron@163.com\"],\"sub\":{\"%url%\":[\"<a href='www.sfaessentials.com'>https://github.com/Khadron/</a>\"]}}",
                From = "khadron@163.com",
                FromName = "khadron",
                Cc = "不支持",
                Subject = "Test Eamil"
            });

            Assert.AreEqual(result.Message, "success");
        }

        [TestMethod]
        public void SendEmail()
        {
            var result = _emailApi.SendEmail(new SendParameter()
            {
                Subject = "Test Email",
                Content = "",
                From = "khadron@163.com",
                FromName = "Dev",
                To = "khadron@163.com",
                Bcc = "不支持",

            });
            Assert.AreEqual(result.Message, "success");
        }


        [TestMethod]
        public void CreateLabel()
        {
            var result = _emailApi.CreateLabel(new LabelParameter()
            {
                LabelName = "Test-firstLabel"
            });
            Assert.AreEqual(result.Message, "success");
        }

        [TestMethod]
        public void GetSendStatus()
        {

        }

        [TestMethod]
        public void GetTemplate()
        {
            var result = _emailApi.GetFirstTemplate("firstApi");

            Assert.AreEqual(result.Message, "success");
        }

        [TestMethod]
        public void CreateTemplate()
        {
            #region html

            const string html = "<html><head></head><body></body></html>";

            #endregion

            var result = _emailApi.CreateTemplate(new TemplateParameter() { EmailTemplateType = (int)EmailTemplateType.Batch, Html = html, InvokeName = "", Name = "", Subject = "" });

            Assert.AreEqual(result.Message, "success");
        }
    }
}

/******************************************************************* 
 * FileName: BaseWebApi.cs
 * Author   : Qiang Kong
 * Date : 2015-09-09 09:52:39
 * Desc : 
 * 
 * 
 * *******************************************************************/
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using CodeScales.Http;
using CodeScales.Http.Common;
using CodeScales.Http.Entity;
using CodeScales.Http.Entity.Mime;
using CodeScales.Http.Methods;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using SendCloudSDK.Models;
using SendCloudSDK.Models.Http;
using SendCloudSDK.Utis;

namespace SendCloudSDK.WebApi
{
    internal abstract class BaseWebApi
    {
        protected HttpClient Client;
        protected HttpPost PostMethod;
        protected HttpGet GetMethod;
        protected HttpResponse Response;
        protected SendCloudConfig Config;

        protected BaseWebApi(SendCloudConfig config)
        {
            Config = config;
            Client = new HttpClient();
            PostMethod = new HttpPost();
            GetMethod = new HttpGet();
            Response = new HttpResponse();
        }

        protected MultipartEntity CreateMultipartBody<T>(T entity) where T : BaseParameter
        {
            return CreateMultipartBody<T>(entity, null, null);
        }

        protected MultipartEntity CreateMultipartBody<T>(T entity, string attachmentName, string attachmentPath) where T : BaseParameter
        {
            MultipartEntity result = new MultipartEntity();

            var kvs = ResolveParameter(entity);
            foreach (var kv in kvs)
            {
                result.AddBody(new StringBody(Encoding.UTF8, kv.Key, kv.Value));
            }

            if (!string.IsNullOrEmpty(attachmentPath))
            {
                FileInfo fileInfo = new FileInfo(attachmentPath);
                var fileName = Path.GetFileName(attachmentPath);
                if (string.IsNullOrEmpty(attachmentName))
                {
                    attachmentName = fileName;
                }
                UTF8FileBody fileBody = new UTF8FileBody(attachmentName, fileName, fileInfo);
                result.AddBody(fileBody);
            }

            return result;
        }


        protected UrlEncodedFormEntity CreateFormBody<T>(T entity) where T : BaseParameter
        {
            var kvc = ResolveParameter(entity);
            var result = new UrlEncodedFormEntity(kvc.Select(r => new NameValuePair(r.Key, r.Value)).ToList(), Encoding.Default);
            return result;
        }

        protected Uri BuildUri(string relativeUri)
        {
            var uri = string.Format("{0}{1}", Config.ApiConfig.Host, relativeUri);
            return new Uri(uri);
        }

        protected string CallApi<T>(string relativeUri, T parameter) where T : BaseParameter
        {
            return CallApi(relativeUri, parameter, false);
        }

        protected string CallApi<T>(string relativeUri, T parameter, bool multipart) where T : BaseParameter
        {
            return CallApi(relativeUri, parameter, multipart, null, null);
        }

        protected string CallApi<T>(string relativeUri, T parameter, string attachmentName, string attachmentPath) where T : BaseParameter
        {
            return CallApi(relativeUri, parameter, true, attachmentName, attachmentPath);
        }

        protected string CallApi<T>(string relativeUri, T parameter, bool multipart, string attachmentName, string attachmentPath) where T : BaseParameter
        {
            try
            {
                ReplenishApiInfo(parameter);
                if (!VerifyParameter(parameter))
                {
                    return BuildErrorInfo("不合法参数");
                }

                PostMethod.Uri = BuildUri(relativeUri);

                if (multipart)
                {
                    PostMethod.Entity = CreateMultipartBody(parameter, attachmentName, attachmentPath);
                }
                else
                {
                    PostMethod.Entity = CreateFormBody(parameter);
                }

                //PostMethod.Entity = multipart ? CreateMultipartBody<T>(parameter) : CreateFormBody<T>(parameter);
                HttpResponse response = Client.Execute(PostMethod);
                string result = EntityUtils.ToString(response.Entity);
                return result;
            }
            catch (Exception ex)
            {
                Logger.Instance.Write(ex, MessageType.Error);
                return BuildErrorInfo(ex.Message);
            }
        }

        protected virtual bool VerifyParameter(BaseParameter parameter)
        {
            return true;
        }

        protected void ReplenishApiInfo(BaseParameter parameter)
        {
            if (string.IsNullOrEmpty(parameter.ApiKey))
            {
                parameter.ApiKey = Config.ApiConfig.ApiKey;
            }

            if (string.IsNullOrEmpty(parameter.ApiUser))
            {
                parameter.ApiUser = Config.ApiConfig.ApiUser;
            }
        }

        protected static string BuildErrorInfo(string errorInfo)
        {
            return "{\"message\":\"error\",\"errors\":[" + errorInfo + "]}";
        }

        protected TR Execute<TR, TP>(string url, TP parameter, bool multipart = false)
            where TR : BaseResult, new()
            where TP : BaseParameter, new()
        {
            string json = CallApi(url, parameter, multipart);
            var result = JsonNet.DeserializeToString<TR>(json);
            if (result == null)
            {
                return new TR { Errors = new List<string> { BuildErrorInfo(json) } };
            }
            return result;
        }

        private static List<KeyValuePair<string, string>> ResolveParameter<T>(T entity) where T : BaseParameter
        {
            Type type = typeof(T);
            var propertys = type.GetProperties();

            var result = new List<KeyValuePair<string, string>>();
            foreach (var property in propertys)
            {
                object obj = property.GetValue(entity, null);
                if (obj == null)
                    continue;
                string val;
                if (obj is bool)
                {
                    val = ((bool)obj) ? "1" : "0";
                }
                else if (obj is BoolEnum)
                {
                    val = ((BoolEnum)obj).ToString().ToLower();
                }
                else
                {
                    val = obj.ToString();
                }


                object[] attributes = property.GetCustomAttributes(true);
                if (attributes.Length > 0)
                {
                    foreach (var attribute in attributes)
                    {
                        if (attribute is JsonPropertyAttribute)
                        {
                            var jp = attribute as JsonPropertyAttribute;
                            result.Add(new KeyValuePair<string, string>(jp.PropertyName, val));
                        }
                    }
                }
                else
                {
                    result.Add(new KeyValuePair<string, string>(property.Name, val));
                }
            }
            return result;
        }

    }
}

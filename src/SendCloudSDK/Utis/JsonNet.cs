using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace SendCloudSDK.Utis
{
    public static class JsonNet
    {

        public static string SerializeToEntity(object item)
        {
            try
            {
                return JsonConvert.SerializeObject(item, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            }
            catch (Exception ex)
            {
                Logger.Instance.Write(ex, MessageType.Error);
                return null;
            }
        }

        public static T DeserializeToString<T>(string jsonString)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(jsonString);

            }
            catch (Exception ex)
            {
                Logger.Instance.Write(ex, MessageType.Error);
                return default(T);
            }
        }
    }

    #region Converter

    public class ChinaDateTimeConverter : DateTimeConverterBase
    {
        private static readonly IsoDateTimeConverter DtConverter = new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd" };

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {


            return DtConverter.ReadJson(reader, objectType, existingValue, serializer);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {

            DtConverter.WriteJson(writer, value, serializer);
        }
    }


    public class StringAndBoolConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value is string)
            {
                if (value.Equals("Y"))
                {
                    writer.WriteValue(true);
                }
                else if (value.Equals("N"))
                {
                    writer.WriteValue(false);
                }
                else
                {
                    throw new ArgumentException(String.Format("StringAndBoolConverter WriteJson ArgumentFormateException:{0}", value));
                }
            }
            else if (value is bool)
            {
                writer.WriteValue(value);
            }
            else
            {
                throw new ArgumentException(String.Format("StringAndBoolConverter WriteJson ArgumentException:{0}", value.GetType()));
            }
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType != JsonToken.Boolean && reader.TokenType != JsonToken.String && reader.TokenType != JsonToken.Integer)
            {
                throw new ArgumentException(String.Format("StringAndBoolConverter ReadJson ArgumentException:{0}", reader.TokenType));
            }

            if (reader.Value == null)
            {
                return null;
            }

            if (reader.Value.ToString().ToLower().Equals("true") || reader.Value.ToString().ToLower().Equals("y") || reader.Value.ToString().ToLower().Equals("1"))
            {
                return true;
            }
            else if (reader.Value.ToString().ToLower().Equals("false") || reader.Value.ToString().ToLower().Equals("n") || reader.Value.ToString().ToLower().Equals("0"))
            {
                return false;
            }
            else
            {
                throw new ArgumentNullException("StringAndBoolConverter value unknow");
            }
        }

        public override bool CanConvert(Type objectType)
        {
            if (objectType == typeof(string))
            {
                return true;
            }
            if (objectType == typeof(bool))
            {
                return true;
            }
            return false;
        }
    }


    /// <summary>
    /// 动态序列化
    /// </summary>
    public class LimitPropsContractResolver : DefaultContractResolver
    {
        readonly string[] _props = null;

        public LimitPropsContractResolver(string[] props)
        {
            //指定要序列化属性的清单
            this._props = props;
        }

        protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
        {
            IList<JsonProperty> list = base.CreateProperties(type, memberSerialization);
            //只保留清单有列出的属性
            return list.Where(p => _props.Contains(p.PropertyName)).ToList();
        }
    }
    #endregion
}

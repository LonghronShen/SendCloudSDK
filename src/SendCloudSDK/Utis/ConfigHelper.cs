/******************************************************************* 
 * FileName: ConfigHelper.cs
 * Author   : Qiang Kong
 * Date : 2015-11-10 14:54:39
 * Desc : 
 * 
 * 强类型操作配置文件
 * *******************************************************************/

using System;
using System.Configuration;
using System.IO;
using System.Reflection;
using System.Xml;

namespace SendCloudSDK.Utis
{
    internal class ConfigHelper
    {
        private const string ValFlag = "value";
        private const string Epilogue = ">";

        private static ConfigHelper _instance;

        public static bool CreateImpl(string configPath = null)
        {
            var instance = GetInstance(configPath);
            return instance != null;
        }

        public static ConfigHelper GetInstance()
        {
            return GetInstance(null);
        }

        public static ConfigHelper GetInstance(string configPath)
        {
            return _instance ?? (_instance = new ConfigHelper(configPath));
        }

        private readonly ExeConfigurationFileMap _exeConfigMap;

        private ConfigHelper(string configPath)
        {
            if (string.IsNullOrWhiteSpace(configPath))
            {
                configPath = String.Format("{0}\\{1}", AppDomain.CurrentDomain.BaseDirectory.TrimEnd('/', '\\'), "SendCloud.config");

                if (!File.Exists(configPath))
                {
                    string[] filePaths = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "SendCloud.config", SearchOption.AllDirectories);
                    if (filePaths.Length == 0)
                    {
                        throw new Exception("未找到SendCloud.config");
                    }

                    configPath = filePaths[0];
                }

            }

            _exeConfigMap = new ExeConfigurationFileMap { ExeConfigFilename = configPath };
        }

        public T GetSection<T>(string sectionName) where T : class,new()
        {
            return GetSection<T>(sectionName, null);
        }

        public T GetSection<T>(string sectionName, string sectionGroup) where T : class, new()
        {
            T result = new T();
            var section = GetSection(sectionName, sectionGroup);
            if (section != null)
            {
                Type t = typeof(T);
                foreach (KeyValueConfigurationElement kv in section)
                {
                    t.GetProperty(kv.Key).SetValue(result, kv.Value, null);
                }
            }
            return result;
        }

        public void ModifySection<T>(string sectionName, T entity) where T : class, new()
        {
            ModifySection<T>(sectionName, null, entity);
        }

        public void ModifySection<T>(string sectionName, string sectionGroupName, T entity) where T : class,new()
        {
            try
            {


                var configuration = ConfigurationManager.OpenMappedExeConfiguration(_exeConfigMap, ConfigurationUserLevel.None);

                ConfigurationSection section;
                if (string.IsNullOrWhiteSpace(sectionGroupName))
                {
                    section = configuration.GetSection(sectionName);
                }
                else
                {

                    var group = configuration.GetSectionGroup(sectionGroupName);
                    if (group == null)
                    {
                        throw new Exception("SectionGroup为空");
                    }
                    section = group.Sections[sectionName];
                }


                if (section != null)
                {

                    var info = section.SectionInformation;
                    var dicAssemblyName = typeof(DictionarySectionHandler).FullName;
                    if (!info.Type.Contains(dicAssemblyName))
                    {
                        throw new Exception("暂时支持DictionarySectionHandler的节点");
                    }

                    var xmlStr = info.GetRawXml();

                    Type st = entity.GetType();
                    PropertyInfo[] spis = st.GetProperties();

                    int curIndex = 0;
                    foreach (var item in spis)
                    {
                        var key = item.Name;

                        curIndex = xmlStr.IndexOf(string.Format("key=\"{0}\"", key), curIndex, StringComparison.Ordinal);
                        if (curIndex != -1)
                        {
                            var normalStr = item.GetValue(entity, null)
                                .ToString()
                                .Replace("<", "&lt;")
                                .Replace("&", "&amp;")
                                .Replace(">", "&gt;").Replace("'", "&apos;").Replace("\"", "&quot;");
                            var tagStr = string.Format("key=\"{0}\" {3}=\"{1}\" /{2}", key, normalStr, Epilogue, ValFlag);
                            int endIndex = xmlStr.IndexOf(Epilogue, curIndex, StringComparison.Ordinal);
                            var ss = xmlStr.Substring(curIndex, endIndex - curIndex + 1);
                            xmlStr = xmlStr.Replace(ss, tagStr);
                            curIndex = endIndex;
                        }
                        else
                        {
                            curIndex = 0;
                        }

                    }

                    info.SetRawXml(xmlStr);
                    info.Type = dicAssemblyName;

                    configuration.Save(ConfigurationSaveMode.Modified);
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public string GetVal(string key, string sectionName)
        {
            string result = string.Empty;
            var kvc = GetSection(sectionName);

            foreach (KeyValueConfigurationElement kv in kvc)
            {
                if (key.Equals(kv.Key))
                {
                    result = kv.Value;
                    break;
                }
            }

            return result;
        }

        public string SetVal(string key, string val, string sectionName)
        {
            return "";
        }

        private KeyValueConfigurationCollection GetSection(string sectionName, string sectionGroupName = null)
        {
            var result = new KeyValueConfigurationCollection();

            if (string.IsNullOrEmpty(sectionName))
            {
                return result;
            }

            var configuration = ConfigurationManager.OpenMappedExeConfiguration(_exeConfigMap,
                ConfigurationUserLevel.None);

            ConfigurationSection section;
            if (string.IsNullOrWhiteSpace(sectionGroupName))
            {
                section = configuration.GetSection(sectionName);
            }
            else
            {

                var group = configuration.GetSectionGroup(sectionGroupName);
                if (group == null)
                {
                    throw new Exception("sectionGroup为空");
                }
                section = group.Sections[sectionName];
            }

            if (section != null)
            {

                ConfigXmlDocument vv = new ConfigXmlDocument();
                vv.LoadXml(section.SectionInformation.GetRawXml());

                var items = vv.GetElementsByTagName("add");
                XmlAttributeCollection attributes;
                foreach (XmlNode item in items)
                {
                    attributes = item.Attributes;
                    if (attributes != null && attributes.Count == 2)
                    {
                        result.Add(attributes["key"].Value, attributes["value"].Value);
                    }
                }
            }
            return result;
        }

        private string GetSectionXml(string sectionName)
        {
            string xmlStr = string.Empty;
            var configuration = ConfigurationManager.OpenMappedExeConfiguration(_exeConfigMap, ConfigurationUserLevel.None);
            var section = configuration.GetSection(sectionName);
            if (section != null)
            {
                xmlStr = section.SectionInformation.GetRawXml();
            }

            return xmlStr;
        }
    }
}

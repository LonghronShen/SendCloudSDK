using System.Collections.Generic;
using System.IO;
using System.Text;
using CodeScales.Http.Entity.Mime;

namespace SendCloudSDK.Models.Http
{
    internal class UTF8FileBody : Body
    {
        private readonly string _name;
        private readonly string _fileName;
        private readonly byte[] _content;
        private readonly string _mimeType;
        private const string StringParam = "Content-Disposition: form-data; name=\"";


        public UTF8FileBody(string name, string fileName, FileInfo fileInfom, string mimeType)
        {
            this._mimeType = mimeType;
        }

        public UTF8FileBody(string name, string fileName, FileInfo fileInfo)
        {
            this._name = name;
            this._fileName = fileName;
            this._content = null;

            if (fileInfo == null)
            {
                this._content = new byte[0];
            }
            else
            {
                using (BinaryReader reader = new BinaryReader(fileInfo.OpenRead()))
                {
                    this._content = reader.ReadBytes((int)reader.BaseStream.Length);
                }
            }
        }

        public byte[] GetContent(string boundry)
        {
            List<byte> bytes = new List<byte>();
            if (this._content.Length == 0 || this._mimeType == null || this._mimeType.Equals(string.Empty))
            {
                bytes.AddRange(Encoding.UTF8.GetBytes(AddPostParametersFile(this._name, this._fileName, boundry, "application/octet-stream")));
            }
            else
            {
                bytes.AddRange(Encoding.UTF8.GetBytes(AddPostParametersFile(this._name, this._fileName, boundry, this._mimeType)));
            }
            bytes.AddRange(this._content);
            bytes.AddRange(Encoding.ASCII.GetBytes("\r\n"));
            return bytes.ToArray();
        }

        private static string AddPostParametersFile(string name, string fileName, string boundry, string contentType)
        {
            if (name == null)
            {
                name = string.Empty;
            }
            if (fileName == null)
            {
                fileName = string.Empty;
            }

            StringBuilder builder = new StringBuilder();
            string paramBoundry = "--" + boundry + "\r\n";
            string paramEnd = "\"; filename=\"" + fileName + "\"\r\nContent-Type: " + contentType + "\r\n\r\n";
            builder.Append(paramBoundry);
            builder.Append(StringParam + name + paramEnd);
            return builder.ToString();
        }
    }
}

using SendCloudSDK.Common;

namespace SendCloudSDK.Models.Http
{
    public class EmailAttachment
    {
        public string AttachmentName { get; set; }
        public string AttachmentPath { get; set; }
        public MimeType MimeType { get; set; }
    }


}

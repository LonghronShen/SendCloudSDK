using System.Collections.Generic;
using Newtonsoft.Json;

namespace SendCloudSDK.Models
{
    public class SendResult : BaseResult
    {
        [JsonProperty("email_id_list")]
        public List<string> EmailIdList { get; set; }
        [JsonProperty("mail_list_task_id_list")]
        public List<string> EmailTaskIdList { get; set; }
    }
}

using System;
using Newtonsoft.Json;

namespace SendCloudSDK.Models
{
    public class UnsubscribesGetParameter : BaseParameter
    {
        [JsonProperty("days")]
        public int Days { get; set; } //过去 days 天内的统计数据 (days=1表示今天)
        [JsonProperty("start_date")]
        public string StartDate { get; set; } //开始日期, 格式为yyyy-MM-dd
        [JsonProperty("end_date")]
        public DateTime EndDate { get; set; } //	结束日期, 格式为yyyy-MM-dd
        [JsonProperty("email")]
        public string Email { get; set; } //	查询该地址在取消订阅列表中的详情
        [JsonProperty("api_user_list")]
        public string UserList { get; set; } //	获取指定 API_USER 的统计数据, 多个 API_USER 用;分开, 如:api_user_list=a;b;c
        [JsonProperty("label_id_list")]
        public string LabelIdList { get; set; } //	获取指定标签下的统计数据, 多个标签用;分开, 如:label_id_list=a;b;c
        [JsonProperty("start")]
        public int? Start { get; set; } //	查询起始位置, 取值区间 [0-], 默认为 0
        [JsonProperty("limit")]
        public int? Limit { get; set; } //	查询个数, 取值区间 [0-100], 默认为 100
    }

    public class AddUnsubscribeParameter : BaseParameter
    {
        [JsonProperty("email")]
        public string Email { get; set; }
    }

    public class DelUnsubscribeParameter : BaseParameter
    {
        [JsonProperty("start_date")]
        public DateTime StartDate { get; set; }
        [JsonProperty("end_date")]
        public DateTime EndDate { get; set; }
        [JsonProperty("email")]
        public string Email { get; set; }
    }

}

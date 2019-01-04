using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BAP_Model
{
    public class WechatTemplateMessage
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string touser { get; set; } //wechat openID
        public string template_id { get; set; } //模板Id
        public WechatTemplateMessageData data { get; set; } 
    }

    public class WechatTemplateMessageData
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public DataColumnProperties first { get; set; }
        public DataColumnProperties keyword1 { get; set; }
        public DataColumnProperties keyword2 { get; set; }
        public DataColumnProperties keyword3 { get; set; }
        public DataColumnProperties keyword4 { get; set; }
        public DataColumnProperties keyword5 { get; set; }
        public DataColumnProperties remark { get; set; } 
    }

    public class DataColumnProperties
    {
        public string value;
    }
}

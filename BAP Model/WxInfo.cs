using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BAP_Model.Model
{
    public class WxInfo
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]

        public string UserId { get; set; }

        public string Eid { get; set; }

        public string WxOpenId { get; set; } 

        public int Sex { get; set; }

        public string NickName { get; set; }

        public string HeadImg { get; set; }

        public DateTime CreatedTime { get; set; }

        public DateTime LastModifiedTime { get; set; }
    }
}

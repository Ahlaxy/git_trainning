using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BAP_Model
{
    public class modelForTest
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string EID { get; set; }

        public string WxOpenId { get; set; }

        public int Sex { get; set; }

        public string NickName { get; set; }

        public string HeadImg { get; set; }

        public DateTime CreatedTime { get; set; }

        public DateTime LastModifiedTime { get; set; } 

        public string EShow { get; set; }

        public string MobilePhone { get; set; }
    }
}

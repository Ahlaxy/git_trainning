using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BAP_WeChart
{
    public class WXModel
    {
        public string openid { get; set; }
        public string nickname { get; set; }
        public string sex { get; set; }
        public string headimgurl { get; set; }
        public bool isexistence { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string EmployeesID { get; set; }
    }
    public class WXGetAccesstokenHelp
    {
        public string openid { get; set; }
        public string accesstoken { get; set; }
    }

    public class ResultModel
    {
        public string code { get; set; }
        public string mes { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public WXModel wxmodel { get; set; }

      
    }
}
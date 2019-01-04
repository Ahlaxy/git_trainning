using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BAP_Model
{
    public class TestRequestDataWithSignature
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]

        public string UserId { get; set; }

        public string Timespan { get; set; }

        public string Nonce { get; set; } //随机数

        public string Signature { get; set; }

        public string RequestedData { get; set; }
    }
}

 


 
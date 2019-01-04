
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
namespace BAP_Model
{
    public class AccompanyType
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Code { get; set; }
        public string TypeName { get; set; } 
    }
}


 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BAP_Model
{
    public class NosocomialArea
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string NosocomialAreaId { get; set; }
        public string NosocomialAreaName { get; set; }
        public string EX3 { get; set; }
        public string IsDefaultDept { get; set; } 
    }
}
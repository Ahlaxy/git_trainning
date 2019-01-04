using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BAP_Model
{
    public class EquipmentInspectionItem
    { 
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string ItemName { get; set; }
        public string ItemCode { get; set; }
        public string Prop { get; set; }
        public string ItemValue { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BAP_Model
{
    public class InspectionDetails
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Phone { get; set; }
        public string EquipmentStatus { get; set; }
        public string InspectionDescription { get; set; }
        public string InspectionImgUrl { get; set; } 

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<InspectionItemDetails> InspectionItemList = new List<InspectionItemDetails>(); //巡检项目列表
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BAP_Model
{
    public class AccompanyRegDetails
    { 
        public string ACode { get; set; }
        public string CardId { get; set; }
        public string PName { get; set; }
        public string BedNo { get; set; } 
        public string DeptName { get; set; }
        public string PredictAccompanyDateFrom { get; set; }
        public string PredictAccompanyDateTo { get; set; }
        public string ToolName { get; set; } 

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<AccompanyType> CheckTypeList = new List<AccompanyType>(); //检查类型列表


    }
}

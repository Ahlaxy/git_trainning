using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BAP_Model
{
    public class InspectionTask
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string EquipName { get; set; }
        public string EquipCode { get; set; }
        public string FinishedTaskCount { get; set; }
        public string TotalTaskCount { get; set; }
        public string TaskId { get; set; }
        public string BillStaus { get; set; }
        public string BaseDateType { get; set; }
        public string RNum { get; set; }//频率
    }
}

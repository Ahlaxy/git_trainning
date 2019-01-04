using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BAP_Model
{
    public class SimulateGetHisListItem
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string BAPKey { get; set; }
        public string ID { get; set; }
        public string DeptName { get; set; }  
        public string SickbedNo { get; set; }  
        public string PID { get; set; }
        public string PatientIDNo { get; set; } 
        public string PatientName { get; set; } 
        public string RegisterNo { get; set; } 
        public string InpatientNo { get; set; }  
        public string PatientMobile { get; set; } 
 
    }
}



 
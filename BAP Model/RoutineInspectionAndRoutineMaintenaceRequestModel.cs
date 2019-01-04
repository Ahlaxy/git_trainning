

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json; 

namespace BAP_Model
{
    public class RoutineInspectionAndRoutineMaintenaceRequestModel
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string EID { get; set; }
        public string InspectionOrMaintanenaceFlag { get; set; } 
        public string DeptCode { get; set; }
        public string EmpPhone { get; set; }  
        public string DeviceCode { get; set; }   
        public string DeviceStatus { get; set; }   
        public string InspectionOrMaintanenaceDatetime { get; set; }   
        public string InspectionOrMaintanenaceDescription { get; set; }   
        public string UploadedImgUrl { get; set; }    
    }
}

 

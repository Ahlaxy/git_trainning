using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BAP_Model
{
    public class MaintainListItem
    {
         [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string empRepairsCode { get; set; }
        public string orderId { get; set; }
        public string deptName { get; set; }
        public string deviceCode { get; set; }
        public string deptCode { get; set; }
        public string createDate { get; set; } 
        public string dispatchFromdate { get; set; }
        public string workTiem { get; set; }
        public string orderStatus { get; set; }
        public string IsTakeOrderPersonFlag { get; set; } //是否是接单人
        public string ApplicantName { get; set; }  //报修人
        public string Description { get; set; }  //报修描述
        public string Location { get; set; }  // 报修位置
        public string ApplicantPhone { get; set; }   //报修人联系电话 
        public string MaintainDispatchEID { get; set; } // 派工单表EID

    }
}

 
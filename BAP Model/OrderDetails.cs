using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BAP_Model
{
    public class OrderDetails
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)] 
        public string orderId { get; set; }
        public string orderStatus { get; set; }
        public string orderCreateDate { get; set; }
        public string orderApplicant { get; set; }
        public string orderDepartment { get; set; }
        public string orderApplicantMobliePhone { get; set; }
        public string orderDeviceCode { get; set; }
        public string orderMaintainLocation { get; set; }
        public string orderDescription { get; set; }
        public string orderMaintainer { get; set; }
        public string orderMaintainerEID { get; set; }
        public string orderMaintainerMobilePhone { get; set; }
        public string dispatchFromDate { get; set; }
        public string orderFinishTime { get; set; }
        public string finishRemark { get; set; }    // 完成维修时的维修情况描述（点击完成维修按钮时）
        public string Remark { get; set; }          // 维修情况描述（点击维修描述按钮时）
        public string ApplicantRemark { get; set; } // 备注（申请报修时）
        public string orderGrade { get; set; }
        public string orderEvaluate { get; set; }
        public string uploadedImgUrl { get; set; }
        public string uploadedVideoUrl { get; set; }
        public string uploadedVideoScreenshotUrl { get; set; }
        public string uploadedVoiceUrl { get; set; }

        public string BeforeMaintenanceImgUrl { get; set; }
        public string AfterMaintenanceImgUrl { get; set; }
        public string MaterialCostAmount { get; set; } //维修费用总和

        public string SignatureImgUrl { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<MaterialCostItem> MaterialCostList = new List<MaterialCostItem>(); //维修费用列表


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using BAP_Model;
using BAP_Model.Model;
using BAP_WeChart;


namespace BAP_Model
{
    public class ReturnCollectionModel
    {
        public ResultModel resutlModel = new ResultModel();

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<DepartmentModel> deptList = new List<DepartmentModel>();

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<MaintainListItem> maintainList = new List<MaintainListItem>();

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public BAP_Model.Model.Employees employee = new BAP_Model.Model.Employees();

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public BAP_Model.OrderDetails orderDetails = new OrderDetails();

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public BAP_Model.InspectionDetails inspectionDetails = new InspectionDetails();

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public BAP_Model.Model.WxInfo wxInfo = new BAP_Model.Model.WxInfo();

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public BAP_Model.AccompanyRegDetails accompanyRegDetails = new AccompanyRegDetails();


        /// <summary>
        /// 物资申请详情
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public BAP_Model.GoodsAndMaterialsDetails goodsAndMaterialsDetails = new GoodsAndMaterialsDetails();

        public ReturnCollectionModel()
        { 
            this.employee = null;
            this.orderDetails = null;
            this.inspectionDetails = null;
            this.wxInfo = null;
            this.accompanyRegDetails = null;
            this.goodsAndMaterialsDetails = null;
        }
    }
}


 
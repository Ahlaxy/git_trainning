using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using BAP_Model;
using BAP_WeChart;

namespace BAP_Model
{
    public class OrderMealReturnCollectionModel
    {
        public ResultModel resutlModel = new ResultModel();

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<MealCategory> MealCategoryList = new List<MealCategory>();

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<OrderStatusAndQuantity> OrderStatusQuantities = new List<OrderStatusAndQuantity>();

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<MealDetails> MealDetailsList = new List<MealDetails>();

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<ShoppingCartData> ShoppingCartList = new List<ShoppingCartData>();

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public OrderMealUserData OrderMealUser = new OrderMealUserData();

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<OrderMealOrderDetails> OrderMealOrderList = new List<OrderMealOrderDetails>();

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<string> OrderIdList = new List<string>();  //订单Id List

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<InspectionTask> InspectionTaskList = new List<InspectionTask>();

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<EquipmentInspectionItem> EquipmentInspectionItemList = new List<EquipmentInspectionItem>();

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string InspectionTaskFinishPercentage = "";

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public OrderMealOrderDetails orderMealOrderDetails = new OrderMealOrderDetails();

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<string> MealTime = new List<string>();  //餐次

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<string> Taste = new List<string>();  //口味

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<string> appointmentDateList = new List<string>(); //预约送餐日期

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<NosocomialArea> NosocomialAreaList = new List<NosocomialArea>();  //病区（科室）列表

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<AccompanyType> AccompanyTypeList = new List<AccompanyType>();//陪检检查类型列表 

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<TransportReg> TransportRegList = new List<TransportReg>();//运送登记列表 

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<GoodsAndMaterials> OrderListForApproval = new List<GoodsAndMaterials>();//待审批单据列表

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<AccompanyReg> AccompanyRegList = new List<AccompanyReg>();//陪检登记列表  

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string AccompanyHisTaskNum = "0";   //陪检排队信息

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<SimulateGetHisListItem> SimulateGetHisList = new List<SimulateGetHisListItem>();//模拟医院的接口getHisList 

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public MenuPowers mp = new MenuPowers();

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public MaintenanceOrderCount mOrderCount = new MaintenanceOrderCount();

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<AccompanyTool> AccompanyToolList = new List<AccompanyTool>();  //陪检运输工具列表


        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int OrderListForApprovalTotalRow = 0;

        public OrderMealReturnCollectionModel()
        {
            this.MealCategoryList = null;
            this.OrderStatusQuantities = null;
            this.MealDetailsList = null;
            this.ShoppingCartList = null;
            this.OrderMealUser = null;
            this.OrderMealOrderList = null;
            this.InspectionTaskList = null;
            this.EquipmentInspectionItemList = null;
            this.InspectionTaskFinishPercentage = null;
            this.orderMealOrderDetails = null;
            this.MealTime = null;
            this.Taste = null;
            this.appointmentDateList = null;
            this.NosocomialAreaList = null;
            this.AccompanyTypeList = null; 
            this.TransportRegList = null;
            this.AccompanyRegList = null;
            this.SimulateGetHisList = null;
            this.OrderIdList = null;
            this.mp = null;
            this.mOrderCount = null;
            this.AccompanyToolList = null;
            this.AccompanyHisTaskNum = null;
            this.OrderListForApproval = null;
        }
    }
}

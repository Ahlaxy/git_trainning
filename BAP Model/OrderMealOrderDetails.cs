using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BAP_Model
{
    public class OrderMealOrderDetails
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Id { get; set; }
        public string OrderStatus { get; set; }
        public string CreatedDate { get; set; } 
        public string MealTime { get; set; }
        public string AppointmentTime { get; set; }
        public string UserId { get; set; }

        public string PaymentType { get; set; }
        public string OrderItems { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<MealDetails> MealItemsForEachOrder_Flexible = new List<MealDetails>();//灵活点餐items

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<MealDetails> MealItemsForEachOrder_NonFlexible = new List<MealDetails>();//非灵活点餐items

        public string TotalPrice { get; set; }
        public string EmployeePrice { get; set; }
        public string ActualPaidPrice { get; set; } //实付
        public string ShuoldPayPrice { get; set; } //应付
        public string CustomerName { get; set; }
        public string CustomerPhone { get; set; }
        public string IsHospitalEmployeeFlag { get; set; } 
        public string IsOrderTaker { get; set; } //是否为订餐员
        public string DeliveryAddress { get; set; } //送餐地址
        public string DeliveryNosocomialArea { get; set; }//病区
        public string DeliveryBedNumber { get; set; } //床位号
        public string Remark { get; set; } //备注（少放盐不吃辣等）
        public string FlexibleOrderFlag { get; set; } // 
        public string Grade { get;set; } //评分(格式为：1分，2分，3分，4分，5分)
        public string Evaluate { get; set; } //评价（用户输入的评价文本信息）
    }
}


 
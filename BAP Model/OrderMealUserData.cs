using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BAP_Model
{
    public class OrderMealUserData
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Id { get; set; }
        public string WxOpenId { get; set; }
        public int Sex { get; set; }
        public string NickName { get; set; }
        public string HeadImg { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime LastModifiedTime { get; set; }

        public string IsHospitalEmployeeFlag { get; set; }
        public string Name { get; set; }//姓名
        public string Phone { get; set; }//电话
        public string EID { get; set; }  //工号
        public string DeliveryAddress { get; set; }//送餐地址（医院员工）
        public string DeliveryNosocomialArea { get; set; }//送餐病区（非医院员工）
        public string DeliveryNosocomialAreaCode { get; set; }//送餐病区编码（非医院员工）
        public string DeliveryBedNumber { get; set; } //送餐床位号（非医院员工）

        public string IsOrderTakerFlag { get; set; } //是否为订餐员

    }
}


 
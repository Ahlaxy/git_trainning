using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace BAP_Model
{
    public class ShoppingCartData
    { 
       [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string ShoppingCartId { get; set; }
        public string UserId { get; set; }
        public string MealName { get; set; } //餐品名称
        public string CategoryName { get; set; } //类别名称
        public string ImgUrl { get; set; }
        public string Price { get; set; } //单价
        public string EmployeePrice { get; set; } //员工价
        public string GoodsCount { get; set; }// 商品数量 
        public string MealTime { get; set; } //餐次
        public string Taste { get; set; } // 口味
    }
}


 
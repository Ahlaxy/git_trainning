using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BAP_Model
{
    public class MealDetails
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string meadlId { get; set; }
        public string mealName { get; set; }
        public string categoryId { get; set; } //类别Id
        public string categoryName { get; set; } //类别名称
        public string imgUrl { get; set; }  //图片
        public string price { get; set; } //售卖时价格
        public string count { get; set; }//数量
        public string ingredient { get; set; } //原材料
        public string monthlySalesCount { get; set; }// 月销量
        public string voteUpCount { get; set; } //点赞数量
        public string MealDescription { get; set; } //菜品描述
        public string MealTime { get; set; } //餐次
    }
}
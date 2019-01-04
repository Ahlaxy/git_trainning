using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace BAP_Model
{
    public class Material
    {
        public string Mid { get; set; } //物料id
        public string Mname { get; set; } //物料名称
        public string BaseUnit { get; set; } //单位
        public string Quantity { get; set; } //数量
        public string CQ { get; set; } //库存
        public string Price { get; set; }
        public string PictureLocation { get; set; }
    }
}


 
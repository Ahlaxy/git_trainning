using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BAP_Model
{
    public class GoodsAndMaterialsDetails
    {
        public string OrderId { get; set; }
        public string DeptName { get; set; }
        public string DeptId { get; set; }
        public string ApplicationMan { get; set; }
        public string ApplicationPhone { get; set; }
        public string ApplicationDate { get; set; }
        public string Remark { get; set; }
        public string ApprovalRemark { get; set; }
        public string BillStatus { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<Material> MaterialList = new List<Material>(); //物料列表

    }
}

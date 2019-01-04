

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BAP_Model
{
    public class GoodsAndMaterials
    {
        public string OrderId { get; set; }
        public string DeptName { get; set; }
        public string ApplicationMan { get; set; }
        public string ApplicationPhone { get; set; }
        public string ApplicationDate { get; set; }
        public string Remark { get; set; }
        public string ApprovalRemark { get; set; }
        public string BillStatus { get; set; }
        public string BillStatusId { get; set; }
        public string IsApprover { get; set; }
        public string BillType { get; set; }
    }
}

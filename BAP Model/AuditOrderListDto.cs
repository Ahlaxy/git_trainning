using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BAP_Model
{
    public class AuditOrderListDto
    {
        public Guid Wfmid { get; set; }
        public string BillNo { get; set; }
        public int CheckStatus { get; set; }
        public Guid ID { get; set; }
        public string CheckSummary { get; set; }
        public DateTime InitiateData { get; set; }
        public string Initiator { get; set; }
        public string Pkname { get; set; }
        public string Template { get; set; }
        public string WFSname { get; set; }
        public Guid WFMbid { get; set; }
        public int SortIndex { get; set; }

    }
}

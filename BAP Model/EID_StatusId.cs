using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BAP_Model
{
    public class EID_StatusId
    {
        public string EID { get; set; }

        /// <summary>
        /// 维修状态 0:待维修，1：维修中，2：已维修
        /// </summary>
        public string StatusId { get; set; }

    }
}

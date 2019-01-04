using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BAP_Model
{
    public class GetMaintainListRequestModel
    {
        /// <summary>
        /// 工号
        /// </summary>
        public string EID { get; set; }

        /// <summary>
        /// 维修状态 0:待维修，1：维修中，2：已维修
        /// </summary>
        public string StatusId { get; set; }

        /// <summary>
        /// 0:申请人员查看申请列表，1：维修人员查看派给他的维修列表）
        /// </summary>
        public string ApplicantOrMaintainer { get; set; }

        /// <summary>
        /// 设备码
        /// </summary>
        public string DeviceCode { get; set; }

        /// <summary>
        /// 第几页
        /// </summary>
        public string PageNumber { get; set; }

        /// <summary>
        /// 每页多少条数据
        /// </summary>
        public string PageSize { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BAP_Model
{
    public class MaintenanceOrderCount
    {  
        public string WaitingFixCount { get; set; } // 待维修

        public string Fixing { get; set; } //维修中 

        public string Fixed { get; set; } // 已维修 

        public string FixedAndCommented { get; set; } //已评价

        public MaintenanceOrderCount()
        {
            this.WaitingFixCount = "0";
            this.Fixing = "0";
            this.Fixed = "0";
            this.FixedAndCommented = "0"; 
        }
    }
}


 
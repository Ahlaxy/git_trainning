using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BAP_Model
{
    public class MenuPowers
    {  
        /// <summary>
        /// 项目经理派工权限 
        /// </summary>
        public string ProjectManagerDispatchTaskPower { get; set; }   

        /// <summary>
        ///  项目经理查看任务权限 
        /// </summary>
        public string ProjectManagerQueryTaskPower { get; set; } 

        public MenuPowers()
        {
            this.ProjectManagerDispatchTaskPower = "0";
            this.ProjectManagerQueryTaskPower = "0"; 
        }
    }
}


 
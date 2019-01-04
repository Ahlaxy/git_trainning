
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BAP_Model
{
    public class Task
    {
        public string TaskCode { get; set; }
        public string TaskName { get; set; }
        public string UserName { get; set; }
        public string TaskStatus { get; set; }
        public string ProjectId { get; set; } 
    }
}
 
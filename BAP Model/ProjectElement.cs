using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq; 

namespace BAP_Model
{
    public class ProjectElement
    {
        public string ElementId { get; set; }
        public string ElementName { get; set; }
        public string SubItemsCount { get; set; }
        public string IsPersonInCharge { get; set; } 
    }
}


 
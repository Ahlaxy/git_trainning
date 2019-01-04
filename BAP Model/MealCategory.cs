using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BAP_Model
{
    public class MealCategory
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string categoryId { get; set; }
        public string categoryName { get; set; } 
    }
}


 
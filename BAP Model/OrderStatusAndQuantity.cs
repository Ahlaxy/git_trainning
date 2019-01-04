using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace BAP_Model
{
     public class OrderStatusAndQuantity
    { 
         [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
         public string OrderStatusId { get; set; }
         public string Quantity { get; set; } 
    }
}

 
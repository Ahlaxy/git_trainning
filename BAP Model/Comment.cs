using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json; 


namespace BAP_Model
{
    public class Comment
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string orderId { get; set; }
        public string score { get; set; }
        public string comment { get; set; }
    }
}

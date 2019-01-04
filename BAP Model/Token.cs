using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BAP_Model
{
    public class Token
    {
        public int UserId = 0;
        public string SignToken = "";
        public DateTime ExpireTime = DateTime.MinValue;

    }
}
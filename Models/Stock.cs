using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PROMASIDOR__KENYA__LIMITED.Models
{
    public class Stock
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public int QuantityAvailable { get; set; }
        public string ProductType { get; set; }
    }
}
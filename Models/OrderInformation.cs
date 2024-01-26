using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PROMASIDOR__KENYA__LIMITED.Models
{
    public class OrderInformation
    {
        public int OrderID { get; set; }
        public int AgentID { get; set; }
        public string AgentName { get; set; }
        public int CustomerID { get; set; }
        public string CustomerName { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }
        public string AmounttobePaid { get; set; }
        public string AmountPaid { get; set; }
        public string ProductName { get; set; }
    }
}
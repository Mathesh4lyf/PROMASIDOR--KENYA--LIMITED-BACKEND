using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PROMASIDOR__KENYA__LIMITED.Models
{
    public class DeliveryInformation
    {
        public int DeliveryID { get; set; }
        public int CustomerID { get; set; }
        public string CustomerName { get; set; }
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public string AmountPaid { get; set; }
        public DateTime DateOfDelivery { get; set; }
        public int DelivererID { get; set; }
        public string DelivererName { get; set; }
        public string Status { get; set; }
        public string FailedReason { get; set; }
    }
}
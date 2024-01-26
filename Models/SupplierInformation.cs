using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PROMASIDOR__KENYA__LIMITED.Models
{
    public class SupplierInformation
    {
        public int ReceivalID{ get; set; }
        public int SupplierID { get; set; }
        public string SupplierName { get; set; }
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public string AmountPaid { get; set; }
        public string Receivedby { get; set; }
        public string Status{ get; set; }
        public DateTime DateofReceival{ get; set; }
    }
}
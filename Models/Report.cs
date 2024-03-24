using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PROMASIDOR__KENYA__LIMITED.Models
{
    public class Report
    {
        public string orderid { get; set; }

        public string employeename { get; set; }

    public string customername { get; set; }

   public string productname { get; set; }
   public string quantity { get; set; }

  public string amounttobepaid { get; set; }

  public string amountpaid { get; set; }
  public DateTime dateoforder { get; set; }
  public string status { get; set; }
        public DateTime dateofreceival { get; set; }
        public string suppliename { get; set; }
        

    }
}
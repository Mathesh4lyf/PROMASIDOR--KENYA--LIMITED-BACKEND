using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PROMASIDOR__KENYA__LIMITED.Models
{
    public class Employee
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public DateTime DateofBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string Emailaddress { get; set; }
        public string Username { get; set; }
       // public string Password { get; set; }
        public string Role { get; set; }
        public int EmployeeID { get; set; }

    }
}
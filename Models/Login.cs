using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PROMASIDOR__KENYA__LIMITED.Models
{
    public class Login
    {
        public string username { get; set; }
        public string password { get; set; }
    }
    public class UserDetails
    {

        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public DateTime DateofBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string Emailaddress { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public int EmployeeID { get; set; }



        public Result result { get; set; }
    }
    public class Result
    {
        public bool result { get; set; }
        public string message { get; set; }
    }
}
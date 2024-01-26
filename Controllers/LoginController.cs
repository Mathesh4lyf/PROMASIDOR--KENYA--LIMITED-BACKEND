using Newtonsoft.Json.Linq;
using PROMASIDOR__KENYA__LIMITED.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace PROMASIDOR__KENYA__LIMITED.Controllers
{
    public class LoginController : Controller
    {

        Login users = new Login();
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["db_conn"].ConnectionString);



        //[System.Web.Mvc.HttpPost]

        public UserDetails Post(Login users)
            
        {
            UserDetails userdetails = new UserDetails();
            userdetails.result = new Result();
            try
            {
                if (users != null && !string.IsNullOrWhiteSpace(users.username) && !string.IsNullOrWhiteSpace(users.password))
                {

                    using (con)
                    {
                        SqlCommand cmd = new SqlCommand("sp_users", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@username", users.username);
                        cmd.Parameters.AddWithValue("@password", users.password);
                        cmd.Parameters.AddWithValue("@stmttype", "userlogin");
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        if (dt != null && dt.Rows.Count > 0)
                        {

                            userdetails.Firstname = dt.Rows[0]["Firstname"].ToString();
                            userdetails.Lastname = dt.Rows[0]["Lastname"].ToString();
                            userdetails.Emailaddress = dt.Rows[0]["Emailaddress"].ToString();
                            userdetails.PhoneNumber = dt.Rows[0]["PhoneNumber"].ToString();
                            userdetails.Username = dt.Rows[0]["Username"].ToString();
                            userdetails.Password = dt.Rows[0]["Password"].ToString();
                            userdetails.Role = dt.Rows[0]["Role"].ToString();


                            userdetails.result.result = true;
                            userdetails.result.message = "success";
                        }
                        else
                        {
                            userdetails.result.result = false;
                            userdetails.result.message = "Invalid user";
                        }
                    }
                }
                else
                {
                    userdetails.result.result = false;
                    userdetails.result.message = "Please enter username and password";
                }
            }
            catch (Exception ex)
            {
                userdetails.result.result = false;
                userdetails.result.message = "Error occurred: " + ex.Message.ToString();
            }
            return userdetails;
        }
    }
}
using Newtonsoft.Json.Linq;
using PROMASIDOR__KENYA__LIMITED.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Cors;
using System.Web.Mvc;
namespace PROMASIDOR__KENYA__LIMITED.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")] // tune to your needs
    [System.Web.Http.RoutePrefix("")]
    public class RegistrationController : Controller
    {
        Registration users = new Registration();
        SqlConnection con = new
       SqlConnection(ConfigurationManager.ConnectionStrings["db_conn"].ConnectionString);
        //// GET: LoginNew
        //public ActionResult Index()
        //{
        // return View();
        //}
        //// GET: LoginNew/Details/5
        //public ActionResult Details(int id)
        //{
        // return View();
        //}
        //// GET: LoginNew/Create
        //public ActionResult Create()
        //{
        // return View();
        //}
        // POST: LoginNew/Create
        [HttpPost]
        public async Task<JObject> Post(Registration LoginNew)
        {
            JObject response_json = new JObject();
            try
            {
                if (LoginNew != null)
                {
                    SqlCommand cmd = new SqlCommand("AddUserToLogin", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Username", LoginNew.username);
                    cmd.Parameters.AddWithValue("@Password", LoginNew.password);
                    cmd.Parameters.AddWithValue("@ConfirmPassword",
                   LoginNew.confirmpassword);
                    con.Open();
                    int i = cmd.ExecuteNonQuery();
                    con.Close();
                    if (i > 0)
                    {
                        response_json.Add("RESPONSECODE", "00");
                        response_json.Add("RESPONSEMESSAGE", "User Added Successfully!"); 
                    }
                    else
                    {
                        response_json.Add("RESPONSECODE", "01");
                        response_json.Add("RESPONSEMESSAGE", "Error: Passwords do not match or UserName Already Exists! Kindly contact System Administrator for help"); 
                    }
                }
                //else
                //{
                // response_json.Add("RESPONSECODE", "02");
                // response_json.Add("RESPONSEMESSAGE", "Error: Passwords do not match.User not added.");
            //}
 }
            catch (Exception ex)
            {
                response_json.Add("RESPONSECODE", "01");
                response_json.Add("RESPONSEMESSAGE", ex.Message);
            }
            return response_json;
        }
    }
}
// //public ActionResult Create(FormCollection collection)
// //{
// // try
// // {
// // // TODO: Add insert logic here
// // return RedirectToAction("Index");
// // }
// // catch
// // {
// // return View();
// // }
// //}
// // GET: LoginNew/Edit/5
// public ActionResult Edit(int id)
// {
// return View();
// }
// // POST: LoginNew/Edit/5
// [HttpPost]
// public ActionResult Edit(int id, FormCollection collection)
// {
// try
// {
// // TODO: Add update logic here
// return RedirectToAction("Index");
// }
// catch
// {
// return View();
// }
// }
// // GET: LoginNew/Delete/5
// public ActionResult Delete(int id)
// {
// return View();
// }
// // POST: LoginNew/Delete/5
// [HttpPost]
// public ActionResult Delete(int id, FormCollection collection)
// {
// try
// {
// // TODO: Add delete logic here
// return RedirectToAction("Index");
// }
// catch
// {
// return View();
// }
// }
// }
//}
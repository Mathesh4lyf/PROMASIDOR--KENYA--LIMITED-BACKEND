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
    [RoutePrefix("")]
    public class GetOrdersByDateController : Controller
    {
        Report emp = new Report();
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["db_conn"].ConnectionString);
        // GET: Report
        public ActionResult Index()
        {
            return View();
        }

        // GET: Report/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Report/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Report/Create
        [HttpPost]
        public async Task<JObject> Post(Report report)
        {
            JObject response_json = new JObject();
            try
            {
                if (report != null)
                {
                    SqlCommand cmd = new SqlCommand("SELECT OrderID, CustomerName, ProductName, Quantity, AmountToBePaid, AmountPaid, DateOfOrder, Status FROM OrderInformation_View WHERE DateOfOrder = @TargetDate AND CustomerName = @CustomerName", con);
                    cmd.Parameters.AddWithValue("@TargetDate", report.dateoforder.ToString("yyyy-MM-dd"));
                    cmd.Parameters.AddWithValue("@CustomerName", report.customername);
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        response_json.Add("RESPONSECODE", "00");
                        response_json.Add("RESPONSEMESSAGE", "Report Fetched Successfully!");
                        JArray data = new JArray();
                        while (reader.Read())
                        {
                            JObject item = new JObject();
                            item.Add("OrderID", reader["OrderID"].ToString());
                            item.Add("CustomerName", reader["CustomerName"].ToString());
                            item.Add("ProductName", reader["ProductName"].ToString());
                            item.Add("Quantity", reader["Quantity"].ToString());
                            item.Add("AmountToBePaid", reader["AmountToBePaid"].ToString());
                            item.Add("AmountPaid", reader["AmountPaid"].ToString());
                            item.Add("DateOfOrder", reader["DateOfOrder"].ToString());
                            item.Add("Status", reader["Status"].ToString());
                            data.Add(item);
                        }
                        response_json.Add("DATA", data);
                    }
                    else
                    {
                        response_json.Add("RESPONSECODE", "01");
                        response_json.Add("RESPONSEMESSAGE", "No Data Found!");
                    }
                    reader.Close();
                    con.Close();
                }
                else
                {
                    response_json.Add("RESPONSECODE", "01");
                    response_json.Add("RESPONSEMESSAGE", "No Data Found!");
                }
            }
            catch (Exception ex)
            {
                response_json.Add("RESPONSECODE", "01");
                response_json.Add("RESPONSEMESSAGE", ex.Message);
            }
            return response_json;
        }

        // GET: Report/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Report/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Report/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Report/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
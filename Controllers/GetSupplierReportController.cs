using Newtonsoft.Json.Linq;
using PROMASIDOR__KENYA__LIMITED.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
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
    public class GetSupplierReportController : Controller
    {
        Report emp = new Report();
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["db_conn"].ConnectionString);
        // GET: GetSupplierInformationReport
        public ActionResult Index()
        {
            return View();
        }

        // GET: GetSupplierInformationReport/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: GetSupplierInformationReport/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: GetSupplierInformationReport/Create
        [HttpPost]
        public async Task<JObject> Post(Report report)
        {
            JObject response_json = new JObject();
            try
            {
                if (report != null)
                {
                    SqlCommand cmd = new SqlCommand("SELECT SupplierName, ProductName, Quantity, AmountPaid, DateofReceival FROM SupplierInformationView WHERE DateofReceival = @DateofReceival AND SupplierName = @SupplierName", con);
                    cmd.Parameters.AddWithValue("@DateofReceival", report.dateofreceival.ToString("yyyy-MM-dd"));
                    cmd.Parameters.AddWithValue("@SupplierName", report.suppliename);
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        // Clear existing properties before adding new ones
                        response_json.RemoveAll();

                        response_json.Add("RESPONSECODE", "00");
                        response_json.Add("RESPONSEMESSAGE", "Report Fetched Successfully!");
                        JArray data = new JArray();
                        while (reader.Read())
                        {
                            JObject item = new JObject();
                            item.Add("SupplierName", reader["OrderID"].ToString());
                            item.Add("ProductName", reader["CustomerName"].ToString());
                            item.Add("Quantity", reader["ProductName"].ToString());
                            item.Add("DateofReceival", reader["Quantity"].ToString());
                            item.Add("AmountPaid", reader["AmountToBePaid"].ToString());
                            item.Add("EmployeeID", reader["AmountPaid"].ToString());
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

        // GET: GetSupplierInformationReport/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: GetSupplierInformationReport/Edit/5
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

        // GET: GetSupplierInformationReport/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: GetSupplierInformationReport/Delete/5
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
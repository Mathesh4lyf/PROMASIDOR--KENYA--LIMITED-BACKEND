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
    public class DeliveryInformationController : Controller
    {
        // GET: Status
      
            DeliveryInformation emp = new DeliveryInformation();
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["db_conn"].ConnectionString);
    // GET: Employee
    public async Task<JObject> Get()
    {
        JObject response_json = new JObject();
        try
        {
            SqlDataAdapter da = new SqlDataAdapter("select * from DeliveryInformation", con);
            SqlCommand cmd = new SqlCommand("select * from DeliveryInformation", con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();

            if (dt.Rows.Count > 0)
            {
                JObject child = new JObject();
                foreach (DataColumn col in dt.Columns)
                {

                    child.Add(col.ColumnName, dt.Rows[0][col].ToString());
                }
                //JToken b = JToken.FromObject(dt.Rows[0]);
                response_json.Add("RESPONSECODE", "00");
                response_json.Add("RESPONSEMESSAGE", "Success!");
                response_json.Add("DATA", child);
            }
            else
            {
                response_json.Add("RESPONSECODE", "01");
                response_json.Add("RESPONSEMESSAGE", "Failed to get DeliveryInformation!");
            }
        }
        catch (Exception ex)
        {
            response_json.Add("RESPONSECODE", "01");
            response_json.Add("RESPONSEMESSAGE", ex.Message);
        }

        return response_json;
    }


        // GET: DeliveryInformation


        // GET: DeliveryInformation/Details/5
        public JObject StartGet(int id)
        {
            JObject response_json = new JObject();
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("select * from deliveryinformation where DeliveryID = '" + id + "'", con);
                SqlCommand cmd = new SqlCommand("select * from deliveryinformation where DeliveryID = '" + id + "'", con);
                DataTable dt = new DataTable();
                da.Fill(dt);

                con.Open();
                int i = cmd.ExecuteNonQuery();
                con.Close();

                if (dt.Rows.Count > 0)
                {
                    JObject child = new JObject();
                    foreach (DataColumn col in dt.Columns)
                    {

                        child.Add(col.ColumnName, dt.Rows[0][col].ToString());
                    }
                    //JToken b = JToken.FromObject(dt.Rows[0]);
                    response_json.Add("RESPONSECODE", "00");
                    response_json.Add("RESPONSEMESSAGE", "Success!");
                    response_json.Add("DATA", child);
                }
                else
                {
                    response_json.Add("RESPONSECODE", "01");
                    response_json.Add("RESPONSEMESSAGE", "Failed to get deliveryinformation!");
                }

            }
            catch (Exception ex)
            {
                response_json.Add("RESPONSECODE", "01");
                response_json.Add("RESPONSEMESSAGE", ex.Message);
            }

            return response_json;
        }

        // GET: DeliveryInformation/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DeliveryInformation/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: DeliveryInformation/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: DeliveryInformation/Edit/5
        [HttpPost]
        public async Task<JObject> Post(DeliveryInformation deliveryinformation)
        {
            JObject response_json = new JObject();
            try
            {
                if (deliveryinformation != null)
                {
                    SqlCommand cmd = new SqlCommand("Add_DeliveryInformation", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@customerid", deliveryinformation.CustomerID);
                    cmd.Parameters.AddWithValue("@customername", deliveryinformation.CustomerName);
                    cmd.Parameters.AddWithValue("@productname", deliveryinformation.ProductName);
                    cmd.Parameters.AddWithValue("@quantity", deliveryinformation.Quantity);
                    cmd.Parameters.AddWithValue("@amountpaid", deliveryinformation.AmountPaid);
                    cmd.Parameters.AddWithValue("@dateofdelivery", deliveryinformation.DateOfDelivery);
                    cmd.Parameters.AddWithValue("@deliverername", deliveryinformation.DelivererName);
                    cmd.Parameters.AddWithValue("@status", deliveryinformation.Status);
                    cmd.Parameters.AddWithValue("@failedreason", deliveryinformation.FailedReason);
                    con.Open();
                    int i = cmd.ExecuteNonQuery();
                    con.Close();

                    if (i > 0)
                    {
                        response_json.Add("RESPONSECODE", "00");
                        response_json.Add("RESPONSEMESSAGE", "DeliveryInformation Added Successfully!");
                    }
                    else
                    {
                        response_json.Add("RESPONSECODE", "01");
                        response_json.Add("RESPONSEMESSAGE", "DeliveryInformation Already Exists or Has Similar Data!");
                    }
                }


                else
                {
                    response_json.Add("RESPONSECODE", "01");
                    response_json.Add("RESPONSEMESSAGE", "Failed to add!");

                }
            }
            catch (Exception ex)
            {
                response_json.Add("RESPONSECODE", "01");
                response_json.Add("RESPONSEMESSAGE", ex.Message);
            }
            return response_json;
        }

        // GET: DeliveryInformation/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: DeliveryInformation/Delete/5
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

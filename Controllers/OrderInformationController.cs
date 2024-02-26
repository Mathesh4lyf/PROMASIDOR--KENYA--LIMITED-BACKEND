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
    public class OrderInformationController : Controller
    {
        // GET: OrderInformation
        
        OrderInformation emp = new OrderInformation();
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["db_conn"].ConnectionString);
        // GET: Employee
        public async Task<JObject> Get()
        {
            JObject response_json = new JObject();
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("select * from OrderInformation", con);
                SqlCommand cmd = new SqlCommand("select * from OrderInformation", con);
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
                    response_json.Add("RESPONSEMESSAGE", "Failed to get OrderInformation!");
                }
            }
            catch (Exception ex)
            {
                response_json.Add("RESPONSECODE", "01");
                response_json.Add("RESPONSEMESSAGE", ex.Message);
            }

            return response_json;
        }

        // GET: OrderInformation/Details/5
        public JObject StartGet(int id)
        {
            JObject response_json = new JObject();
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("select * from orderinformation where OrderID = '" + id + "'", con);
                SqlCommand cmd = new SqlCommand("select * from orderinformation where OrderID = '" + id + "'", con);
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
                    response_json.Add("RESPONSEMESSAGE", "Failed to get OrderInformation!");
                }

            }
            catch (Exception ex)
            {
                response_json.Add("RESPONSECODE", "01");
                response_json.Add("RESPONSEMESSAGE", ex.Message);
            }

            return response_json;
        }

        // GET: OrderInformation/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: OrderInformation/Create
        [HttpPost]
        public async Task<JObject> Post(OrderInformation orderinformation)
        {
            JObject response_json = new JObject();
            try
            {
                if (orderinformation != null)
                {
                    SqlCommand cmd = new SqlCommand("Add_OrderInformation", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@agentid", orderinformation.AgentID);
                    cmd.Parameters.AddWithValue("@agentname", orderinformation.AgentName);
                    cmd.Parameters.AddWithValue("@customerid", orderinformation.CustomerID);
                    cmd.Parameters.AddWithValue("@customername", orderinformation.CustomerName);
                    cmd.Parameters.AddWithValue("@productid", orderinformation.ProductID);
                    cmd.Parameters.AddWithValue("@productname", orderinformation.ProductName);
                    cmd.Parameters.AddWithValue("@quantity", orderinformation.Quantity);
                    cmd.Parameters.AddWithValue("@amounttobepaid", orderinformation.AmounttobePaid);
                    cmd.Parameters.AddWithValue("@amountpaid", orderinformation.AmountPaid);
                    con.Open();
                    int i = cmd.ExecuteNonQuery();
                    con.Close();

                    if (i > 0)
                    {
                        response_json.Add("RESPONSECODE", "00");
                        response_json.Add("RESPONSEMESSAGE", "OrderInformation Added Successfully!");
                    }
                    else
                    {
                        response_json.Add("RESPONSECODE", "01");
                        response_json.Add("RESPONSEMESSAGE", "OrderInformation Already Exists or Has Similar Data!");
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

        // PUT: api/OrderInformation/5
        public async Task<JObject> Put(OrderInformation orderinformation)
        {
            JObject response_json = new JObject();
            try
            {
                if (orderinformation != null)
                {
                    SqlCommand cmd = new SqlCommand("Update_OrderInformation", con);
                    cmd.CommandType = CommandType.StoredProcedure;


                    cmd.Parameters.AddWithValue("@OrderID", orderinformation.OrderID);
                    cmd.Parameters.AddWithValue("@Quantity", orderinformation.Quantity);
                    cmd.Parameters.AddWithValue("@AmounttobePaid", orderinformation.AmounttobePaid);
                    cmd.Parameters.AddWithValue("@AmountPaid", orderinformation.AmountPaid);



                    con.Open();
                    int i = cmd.ExecuteNonQuery();
                    con.Close();

                    if (i == 1)
                    {
                        response_json.Add("RESPONSECODE", "00");
                        response_json.Add("RESPONSEMESSAGE", "OrderInformation Updated Successfully!");
                    }
                    else
                    {
                        response_json.Add("RESPONSECODE", "01");
                        response_json.Add("RESPONSEMESSAGE", "OrderInformation Already Exists or Has Similar Data!");
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
        public async Task<JObject> Delete(OrderInformation orderinformation)
        {
            JObject response_json = new JObject();
            try
            {
                if (orderinformation != null)
                {
                    SqlCommand cmd = new SqlCommand("delete_orderinformation", con);
                    cmd.CommandType = CommandType.StoredProcedure;


                    cmd.Parameters.AddWithValue("@orderid", orderinformation.OrderID);
                    con.Open();
                    int i = cmd.ExecuteNonQuery();
                    con.Close();

                    if (i == 1)
                    {
                        response_json.Add("RESPONSECODE", "00");
                        response_json.Add("RESPONSEMESSAGE", "OrderInformation deleted!");
                    }
                    else
                    {
                        response_json.Add("RESPONSECODE", "01");
                        response_json.Add("RESPONSEMESSAGE", "Failed to delete!");

                    }
                }
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

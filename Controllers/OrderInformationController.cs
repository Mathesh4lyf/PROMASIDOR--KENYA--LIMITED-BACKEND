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
                SqlDataAdapter da = new SqlDataAdapter("select * from OrderInformation_new", con);
                SqlCommand cmd = new SqlCommand("select * from OrderInformation_new", con);
                DataTable dt = new DataTable();
                da.Fill(dt);

                con.Open();
                int i = cmd.ExecuteNonQuery();
                con.Close();

                if (dt.Rows.Count > 0)
                {
                    JArray dataArray = new JArray();

                    foreach (DataRow row in dt.Rows)
                    {
                        JObject child = new JObject();

                        foreach (DataColumn col in dt.Columns)
                        {
                            child.Add(col.ColumnName, row[col].ToString());
                        }

                        dataArray.Add(child);
                    }

                    response_json.Add("RESPONSECODE", "00");
                    response_json.Add("RESPONSEMESSAGE", "Success!");
                    response_json.Add("DATA", dataArray);
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
                SqlDataAdapter da = new SqlDataAdapter("select * from orderinformation_new where OrderID = '" + id + "'", con);
                SqlCommand cmd = new SqlCommand("select * from orderinformation_new where OrderID = '" + id + "'", con);
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
                    SqlCommand cmd = new SqlCommand("AddOrderInformation", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    //cmd.Parameters.AddWithValue("@agentid", orderinformation.AgentID);
                   // cmd.Parameters.AddWithValue("@agentname", orderinformation.AgentName);
                    //cmd.Parameters.AddWithValue("@customerid", orderinformation.CustomerID);
                    cmd.Parameters.AddWithValue("@CustomerName", orderinformation.CustomerName);
                    //cmd.Parameters.AddWithValue("@productid", orderinformation.ProductID);
                    cmd.Parameters.AddWithValue("@ProductName", orderinformation.ProductName);
                    cmd.Parameters.AddWithValue("@Quantity", orderinformation.Quantity);
                    cmd.Parameters.AddWithValue("@AmountToBePaid", orderinformation.AmounttobePaid);
                    cmd.Parameters.AddWithValue("@AmountPaid", orderinformation.AmountPaid);
                    cmd.Parameters.AddWithValue("@EmployeeName", orderinformation.EmployeeName);
                    cmd.Parameters.AddWithValue("@DateOfOrder", orderinformation.DateOfOrder);
                    cmd.Parameters.AddWithValue("@Status", orderinformation.Status);



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
        [HttpPost]
        public JObject Update(OrderInformation orderinformation)
        //public async Task<JObject> Put(OrderInformation orderinformation)
        {
            JObject response_json = new JObject();
            try
            {
                if (orderinformation != null)
                {
                    SqlCommand cmd = new SqlCommand("Update_orderinformation", con);
                    cmd.CommandType = CommandType.StoredProcedure;


                    cmd.Parameters.AddWithValue("@OrderID", orderinformation.OrderID);
                    cmd.Parameters.AddWithValue("@Quantity", orderinformation.Quantity);
                    cmd.Parameters.AddWithValue("@AmounttobePaid", orderinformation.AmounttobePaid);
                    cmd.Parameters.AddWithValue("@AmountPaid", orderinformation.AmountPaid);
                    cmd.Parameters.AddWithValue("@productname", orderinformation.ProductName);



                    con.Open();
                    int i = cmd.ExecuteNonQuery();
                    con.Close();

                    if (i == 1)
                    {
                        response_json.Add("RESPONSECODE", "00");
                        response_json.Add("RESPONSEMESSAGE", "OrderInformation Updated Successfully!");
                    }
                    //else
                    //{
                    //    response_json.Add("RESPONSECODE", "01");
                    //    response_json.Add("RESPONSEMESSAGE", "OrderInformation Already Exists or Has Similar Data!");
                    //}
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
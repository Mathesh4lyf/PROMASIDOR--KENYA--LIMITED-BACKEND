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
using System.Web.Mvc;

namespace PROMASIDOR__KENYA__LIMITED.Controllers
{
    public class CustomersController : Controller
    {
        Customers emp = new Customers();
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["db_conn"].ConnectionString);
        // GET: Employee
        public async Task<JObject> Get()
        {
            JObject response_json = new JObject();
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("select * from Customers", con);
                SqlCommand cmd = new SqlCommand("select * from Customers", con);
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
                    response_json.Add("RESPONSEMESSAGE", "Failed to get customers!");
                }
            }
            catch (Exception ex)
            {
                response_json.Add("RESPONSECODE", "01");
                response_json.Add("RESPONSEMESSAGE", ex.Message);
            }

            return response_json;
        }

        // GET: Customers/Details/5
        public JObject StartGet(int id)
        {
            JObject response_json = new JObject();
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("select * from customers where CustomerID = '" + id + "'", con);
                SqlCommand cmd = new SqlCommand("select * from customers where CustomerID = '" + id + "'", con);
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
                    response_json.Add("RESPONSEMESSAGE", "Failed to get customers!");
                }

            }
            catch (Exception ex)
            {
                response_json.Add("RESPONSECODE", "01");
                response_json.Add("RESPONSEMESSAGE", ex.Message);
            }

            return response_json;
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        [HttpPost]
        public async Task<JObject> Post(Customers customers)
        {
            JObject response_json = new JObject();
            try
            {
                if (customers != null)
                {
                    SqlCommand cmd = new SqlCommand("Add_Customers", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@customername", customers.CustomerName);
                    cmd.Parameters.AddWithValue("@phonenumber", customers.PhoneNumber);
                    cmd.Parameters.AddWithValue("@emailaddress", customers.EmailAddress);
                    cmd.Parameters.AddWithValue("@location", customers.Location);
                    con.Open();
                    int i = cmd.ExecuteNonQuery();
                    con.Close();

                    if (i > 0)
                    {
                        response_json.Add("RESPONSECODE", "00");
                        response_json.Add("RESPONSEMESSAGE", "Customer Added Successfully!");
                    }
                    else
                    {
                        response_json.Add("RESPONSECODE", "01");
                        response_json.Add("RESPONSEMESSAGE", "Customer Already Exists or Has Similar Data!");
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

        // PUT: api/Customers/5
        public async Task<JObject> Put(Customers customers)
        {
            JObject response_json = new JObject();
            try
            {
                if (customers != null)
                {
                    SqlCommand cmd = new SqlCommand("Update_Customers", con);
                    cmd.CommandType = CommandType.StoredProcedure;


                    cmd.Parameters.AddWithValue("@CustomerID", customers.CustomerID);
                    cmd.Parameters.AddWithValue("@CustomerName", customers.CustomerName);
                    cmd.Parameters.AddWithValue("@Emailaddress", customers.EmailAddress);
                    cmd.Parameters.AddWithValue("@Phonenumber", customers.PhoneNumber);
                    cmd.Parameters.AddWithValue("@location", customers.Location);
                    

                    con.Open();
                    int i = cmd.ExecuteNonQuery();
                    con.Close();

                    if (i == 1)
                    {
                        response_json.Add("RESPONSECODE", "00");
                        response_json.Add("RESPONSEMESSAGE", "Customer Updated Successfully!");
                    }
                    else
                    {
                        response_json.Add("RESPONSECODE", "01");
                        response_json.Add("RESPONSEMESSAGE", "Customer Already Exists or Has Similar Data!");
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

        public async Task<JObject> Delete(Customers customers)
        {
            JObject response_json = new JObject();
            try
            {
                if (customers != null)
                {
                    SqlCommand cmd = new SqlCommand("delete_customers", con);
                    cmd.CommandType = CommandType.StoredProcedure;


                    cmd.Parameters.AddWithValue("@customerid", customers.CustomerID);
                    con.Open();
                    int i = cmd.ExecuteNonQuery();
                    con.Close();

                    if (i == 1)
                    {
                        response_json.Add("RESPONSECODE", "00");
                        response_json.Add("RESPONSEMESSAGE", "Customer deleted!");
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

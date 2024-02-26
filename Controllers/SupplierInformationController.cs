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
    public class SupplierInformationController : Controller
    {
        // GET: SupplierInformation
        SupplierInformation emp = new SupplierInformation();
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["db_conn"].ConnectionString);
        // GET: Employee
        public async Task<JObject> Get()
        {
            JObject response_json = new JObject();
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("select * from SupplierInformation", con);
                SqlCommand cmd = new SqlCommand("select * from SupplierInformation", con);
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
                    response_json.Add("RESPONSEMESSAGE", "Failed to get SupplierInformation!");
                }
            }
            catch (Exception ex)
            {
                response_json.Add("RESPONSECODE", "01");
                response_json.Add("RESPONSEMESSAGE", ex.Message);
            }

            return response_json;
        }

        // GET: SupplierInformation/Details/5
        public JObject StartGet(int id)
        {
            JObject response_json = new JObject();
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("select * from supplierinformation where ReceivalID = '" + id + "'", con);
                SqlCommand cmd = new SqlCommand("select * from supplierinformation where ReceivalID = '" + id + "'", con);
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
                    response_json.Add("RESPONSEMESSAGE", "Failed to get supplierinformation!");
                }

            }
            catch (Exception ex)
            {
                response_json.Add("RESPONSECODE", "01");
                response_json.Add("RESPONSEMESSAGE", ex.Message);
            }

            return response_json;
        }

        // GET: SupplierInformation/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SupplierInformation/Create
        [HttpPost]
        public async Task<JObject> Post(SupplierInformation supplierinformation)
        {
            JObject response_json = new JObject();
            try
            {
                if (supplierinformation != null)
                {
                    SqlCommand cmd = new SqlCommand("Add_SupplierInformation", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@suppliername", supplierinformation.SupplierName);
                    cmd.Parameters.AddWithValue("@productname", supplierinformation.ProductName);
                    cmd.Parameters.AddWithValue("@quantity", supplierinformation.Quantity);
                    cmd.Parameters.AddWithValue("@amountpaid", supplierinformation.AmountPaid);
                    cmd.Parameters.AddWithValue("@receivedby", supplierinformation.Receivedby);
                    cmd.Parameters.AddWithValue("@status", supplierinformation.Status);
                    cmd.Parameters.AddWithValue("@dateofreceival", supplierinformation.DateofReceival);
                    cmd.Parameters.AddWithValue("@supplierid", supplierinformation.SupplierID);
                    cmd.Parameters.AddWithValue("@productid", supplierinformation.ProductID);
                    con.Open();
                    int i = cmd.ExecuteNonQuery();
                    con.Close();

                    if (i > 0)
                    {
                        response_json.Add("RESPONSECODE", "00");
                        response_json.Add("RESPONSEMESSAGE", "SupplierInformation Added Successfully!");
                    }
                    else
                    {
                        response_json.Add("RESPONSECODE", "01");
                        response_json.Add("RESPONSEMESSAGE", "SupplierInformation Already Exists or Has Similar Data!");
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

        
        public async Task<JObject> Put(SupplierInformation supplierinformation)
        {
            JObject response_json = new JObject();
            try
            {
                if (supplierinformation != null)
                {
                    SqlCommand cmd = new SqlCommand("Update_SupplierInformation", con);
                    cmd.CommandType = CommandType.StoredProcedure;


                    cmd.Parameters.AddWithValue("@ReceivalID", supplierinformation.ReceivalID);
                    cmd.Parameters.AddWithValue("@Quantity", supplierinformation.Quantity);
                    cmd.Parameters.AddWithValue("@AmountPaid", supplierinformation.AmountPaid);
                    cmd.Parameters.AddWithValue("@Receivedby", supplierinformation.Receivedby);
                    cmd.Parameters.AddWithValue("@Status", supplierinformation.Status);
                    cmd.Parameters.AddWithValue("@DateofReceival", supplierinformation.DateofReceival);


                    con.Open();
                    int i = cmd.ExecuteNonQuery();
                    con.Close();

                    if (i == 1)
                    {
                        response_json.Add("RESPONSECODE", "00");
                        response_json.Add("RESPONSEMESSAGE", "SupplierInformation Updated Successfully!");
                    }
                    else
                    {
                        response_json.Add("RESPONSECODE", "01");
                        response_json.Add("RESPONSEMESSAGE", "SupplierInformation  Already Exists or Has Similar Data!");
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

        public async Task<JObject> Delete(SupplierInformation supplierinformation)
        {
            JObject response_json = new JObject();
            try
            {
                if (supplierinformation != null)
                {
                    SqlCommand cmd = new SqlCommand("delete_supplierinformation", con);
                    cmd.CommandType = CommandType.StoredProcedure;


                    cmd.Parameters.AddWithValue("@receivalid", supplierinformation.ReceivalID);
                    con.Open();
                    int i = cmd.ExecuteNonQuery();
                    con.Close();

                    if (i == 1)
                    {
                        response_json.Add("RESPONSECODE", "00");
                        response_json.Add("RESPONSEMESSAGE", "SupplierInformation deleted!");
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

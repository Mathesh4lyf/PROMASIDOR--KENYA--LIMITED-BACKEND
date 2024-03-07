using Newtonsoft.Json;
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
    public class StockController : Controller
    {
        // GET: Stock
        Stock emp = new Stock();
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["db_conn"].ConnectionString);
        // GET: Employee
        public async Task<JObject> Get()
        {
            JObject response_json = new JObject();
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("select * from Stock", con);
                SqlCommand cmd = new SqlCommand("select * from Stock", con);
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
                    response_json.Add("RESPONSEMESSAGE", "Failed to get Stock!");
                }
            }
            catch (Exception ex)
            {
                response_json.Add("RESPONSECODE", "01");
                response_json.Add("RESPONSEMESSAGE", ex.Message);
            }

            return response_json;
        }

        // GET: Stock/Details/5
        public JObject StartGet(int id)
        {
            JObject response_json = new JObject();
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("select * from stock where ProductID = '" + id + "'", con);
                SqlCommand cmd = new SqlCommand("select * from stock where ProductID = '" + id + "'", con);
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
                    response_json.Add("RESPONSEMESSAGE", "Failed to get stock!");
                }

            }
            catch (Exception ex)
            {
                response_json.Add("RESPONSECODE", "01");
                response_json.Add("RESPONSEMESSAGE", ex.Message);
            }

            return response_json;
        }


        // GET: Stock/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Stock/Create
        [HttpPost]
        public async Task<JObject> Post(Stock stock)
        {
            JObject response_json = new JObject();
            try
            {
                if (stock != null)
                {
                    SqlCommand cmd = new SqlCommand("Add_stock", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@productid", stock.ProductID);
                    cmd.Parameters.AddWithValue("@productname", stock.ProductName);
                    cmd.Parameters.AddWithValue("@quantityavailable", stock.QuantityAvailable);
                    cmd.Parameters.AddWithValue("@producttype", stock.ProductType);
                    con.Open();
                    int i = cmd.ExecuteNonQuery();
                    con.Close();

                    if (i > 0)
                    {
                        response_json.Add("RESPONSECODE", "00");
                        response_json.Add("RESPONSEMESSAGE", "Stock Added Successfully!");
                    }
                    else
                    {
                        response_json.Add("RESPONSECODE", "01");
                        response_json.Add("RESPONSEMESSAGE", "Stock Already Exists!");
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

        // PUT: api/Stock/5
        [HttpPost]
        public JObject Update(Stock stock)
        //public async Task<JObject> Put(Stock stock)
        {
            JObject response_json = new JObject();
            try
            {
                if (stock != null)
                {
                    SqlCommand cmd = new SqlCommand("Update_Stock", con);
                    cmd.CommandType = CommandType.StoredProcedure;


                    cmd.Parameters.AddWithValue("@ProductID", stock.ProductID);
                    cmd.Parameters.AddWithValue("@ProductName", stock.ProductName);
                    cmd.Parameters.AddWithValue("@QuantityAvailable", stock.QuantityAvailable);
                    cmd.Parameters.AddWithValue("@ProductType", stock.ProductType);



                    con.Open();
                    int i = cmd.ExecuteNonQuery();
                    con.Close();

                    if (i == 1)
                    {
                        response_json.Add("RESPONSECODE", "00");
                        response_json.Add("RESPONSEMESSAGE", "Stock Updated Successfully!");
                    }
                    else
                    {
                        response_json.Add("RESPONSECODE", "01");
                        response_json.Add("RESPONSEMESSAGE", "Stock Already Exists or Has Similar Data!");
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

        public async Task<JObject> Delete(Stock stock)
        {
            JObject response_json = new JObject();
            try
            {
                if (stock != null)
                {
                    SqlCommand cmd = new SqlCommand("delete_stock", con);
                    cmd.CommandType = CommandType.StoredProcedure;


                    cmd.Parameters.AddWithValue("@productid", stock.ProductID);
                    con.Open();
                    int i = cmd.ExecuteNonQuery();
                    con.Close();

                    if (i == 1)
                    {
                        response_json.Add("RESPONSECODE", "00");
                        response_json.Add("RESPONSEMESSAGE", "Stock deleted!");
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
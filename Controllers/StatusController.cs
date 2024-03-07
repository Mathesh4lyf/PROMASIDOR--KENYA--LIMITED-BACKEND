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
    public class StatusController : Controller
    {
        // GET: Status

        Status emp = new Status();
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["db_conn"].ConnectionString);
        // GET: Employee
        public async Task<JObject> Get()
        {
            JObject response_json = new JObject();
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("select * from Status", con);
                SqlCommand cmd = new SqlCommand("select * from Status", con);
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
                    response_json.Add("RESPONSEMESSAGE", "Failed to get Status!");
                }
            }
            catch (Exception ex)
            {
                response_json.Add("RESPONSECODE", "01");
                response_json.Add("RESPONSEMESSAGE", ex.Message);
            }

            return response_json;
        }

        // GET: Status/Details/5
        public JObject StartGet(int id)
        {
            JObject response_json = new JObject();
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("select * from status where StatusID = '" + id + "'", con);
                SqlCommand cmd = new SqlCommand("select * from status where StatusID = '" + id + "'", con);
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
                    response_json.Add("RESPONSEMESSAGE", "Failed to get status!");
                }

            }
            catch (Exception ex)
            {
                response_json.Add("RESPONSECODE", "01");
                response_json.Add("RESPONSEMESSAGE", ex.Message);
            }

            return response_json;
        }

        // GET: Status/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Status/Create
        [HttpPost]
        public async Task<JObject> Post(Status status)
        {
            JObject response_json = new JObject();
            try
            {
                if (status != null)
                {
                    SqlCommand cmd = new SqlCommand("Add_status", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@statusname", status.StatusName);
                    con.Open();
                    int i = cmd.ExecuteNonQuery();
                    con.Close();

                    if (i > 0)
                    {
                        response_json.Add("RESPONSECODE", "00");
                        response_json.Add("RESPONSEMESSAGE", "Status Added Successfully!");
                    }
                    else
                    {
                        response_json.Add("RESPONSECODE", "01");
                        response_json.Add("RESPONSEMESSAGE", "Status Already Exists!");
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

        // PUT: api/Status/5
        [HttpPost]
        public JObject Update(Status status)
        //public async Task<JObject> Put(Status status)
        {
            JObject response_json = new JObject();
            try
            {
                if (status != null)
                {
                    SqlCommand cmd = new SqlCommand("update_status", con);
                    cmd.CommandType = CommandType.StoredProcedure;


                    cmd.Parameters.AddWithValue("@statusID", status.StatusID);
                    cmd.Parameters.AddWithValue("@statusname", status.StatusName);
                    con.Open();
                    int i = cmd.ExecuteNonQuery();
                    con.Close();

                    if (i > 0)
                    {
                        response_json.Add("RESPONSECODE", "00");
                        response_json.Add("RESPONSEMESSAGE", "Status updated successfully! ");
                    }
                    else
                    {
                        response_json.Add("RESPONSECODE", "01");
                        response_json.Add("RESPONSEMESSAGE", "Status Already Exists or Has Similar Data!");
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



        // POST: Status/Delete/5
        public async Task<JObject> Delete(Status status)
        {
            JObject response_json = new JObject();
            try
            {
                if (status != null)
                {
                    SqlCommand cmd = new SqlCommand("delete_status", con);
                    cmd.CommandType = CommandType.StoredProcedure;


                    cmd.Parameters.AddWithValue("@statusID", status.StatusID);
                    con.Open();
                    int i = cmd.ExecuteNonQuery();
                    con.Close();

                    if (i == 1)
                    {
                        response_json.Add("RESPONSECODE", "00");
                        response_json.Add("RESPONSEMESSAGE", "Status deleted!");
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
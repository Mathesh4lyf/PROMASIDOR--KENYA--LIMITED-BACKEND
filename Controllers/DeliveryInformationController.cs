﻿using Newtonsoft.Json.Linq;
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

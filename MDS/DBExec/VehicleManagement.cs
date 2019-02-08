using MDS.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;

namespace MDS.DBExec
{
    public class VehicleManagement
    {
        public static string _CON_STR = ConfigurationManager.ConnectionStrings["MDS"].ConnectionString;

        #region Add Vehicle
        public VehicleModel addVehicle(VehicleModel modelData, string user)
        {
            VehicleModel model = new VehicleModel();
            using (SqlConnection db = new SqlConnection(_CON_STR))
            {
                if (modelData.ExpireDate != null)
                {
                    SqlCommand cm = new SqlCommand("[sp_SetVerhicle]", db);
                    cm.CommandType = CommandType.StoredProcedure;
                    cm.Parameters.AddWithValue("@flag", "Add");
                    cm.Parameters.AddWithValue("@User", user);
                    cm.Parameters.AddWithValue("@Carno1", modelData.Carno1);
                    cm.Parameters.AddWithValue("@CarNo2", modelData.CarNo2);
                    cm.Parameters.AddWithValue("@CarChangWat", modelData.CarChangWat);
                    cm.Parameters.AddWithValue("@CarType", modelData.CarType);
                    cm.Parameters.AddWithValue("@GearID", modelData.GearID);
                    cm.Parameters.AddWithValue("@CarSubType", modelData.CarSubType);
                    cm.Parameters.AddWithValue("@Brand", modelData.Brand);
                    cm.Parameters.AddWithValue("@ChassisNo", modelData.ChassisNo);
                    cm.Parameters.AddWithValue("@EngineNo", modelData.EngineNo);
                    cm.Parameters.AddWithValue("@ExpireDate", modelData.ExpireDate);
                    if (db.State == ConnectionState.Closed) db.Open();
                    SqlDataReader dr = cm.ExecuteReader();
                    if (dr.HasRows)
                    {
                        dr.Read();
                        model.Message = dr["msg"].ToString();
                        model.Result = dr["result"].ToString();
                        dr.Close();
                    }
                }
            }
            return model;
        }
        #endregion

        #region "Edit Vehicle"
        public VehicleModel editVehicle(VehicleModel modelData, string user)
        {
            var model = new VehicleModel();
            using (SqlConnection db = new SqlConnection(_CON_STR))
            {
                if (db.State == ConnectionState.Closed) db.Open();
                SqlCommand cm = new SqlCommand("[sp_SetVerhicle]", db);
                cm.CommandType = CommandType.StoredProcedure;
                cm.Parameters.AddWithValue("@flag", "Edit");
                cm.Parameters.AddWithValue("@User", user);
                cm.Parameters.AddWithValue("@Ind", modelData.Ind);
                cm.Parameters.AddWithValue("@Carno1", modelData.Carno1);
                cm.Parameters.AddWithValue("@CarNo2", modelData.CarNo2);
                cm.Parameters.AddWithValue("@CarChangWat", modelData.CarChangWat);
                cm.Parameters.AddWithValue("@CarType", modelData.CarType);
                cm.Parameters.AddWithValue("@GearID", modelData.GearID);
                cm.Parameters.AddWithValue("@CarSubType", modelData.CarSubType);
                cm.Parameters.AddWithValue("@Brand", modelData.Brand);
                cm.Parameters.AddWithValue("@ChassisNo", modelData.ChassisNo);
                cm.Parameters.AddWithValue("@EngineNo", modelData.EngineNo);
                cm.Parameters.AddWithValue("@ExpireDate", modelData.ExpireDate);
                if (db.State == ConnectionState.Closed) db.Open();
                SqlDataReader dr = cm.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    model.Message = dr["msg"].ToString();
                    model.Result = dr["result"].ToString();
                    dr.Close();
                }
            }
            return model;
        }
        #endregion

        #region GetAllVehicle
        public List<VehicleModel> GetAllVehicle()
        {
            List<VehicleModel> model = new List<VehicleModel>();
            using (SqlConnection db = new SqlConnection(_CON_STR))
            {
                SqlCommand cm = new SqlCommand("[sp_Get_Verhicle]", db);
                cm.CommandType = CommandType.StoredProcedure;
                cm.Parameters.AddWithValue("@flag", "GetAll");

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cm;
                DataTable dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow row in dt.Rows)
                {
                        VehicleModel x = new VehicleModel();
                        x.Ind = row["ind"].ToString();
                        x.Carno1 = row["Carno1"].ToString();
                        x.CarNo2 = row["CarNo2"].ToString();
                        x.CarChangWat = row["CarChangWat"].ToString();
                        x.CarType = row["CarType"].ToString();
                        x.GearID = row["GearID"].ToString();
                        x.CarSubType = row["CarSubType"].ToString();
                        x.Brand = row["Brand"].ToString();
                        x.ChassisNo = row["ChassisNo"].ToString();
                        x.EngineNo = row["EngineNo"].ToString();
                        //x.ExpireDate = row["ExpireDate"].ToString();

                        x.ExpireDate = Convert.ToDateTime(row["ExpireDate"].ToString()).ToString("dd/MM/yyyy", new System.Globalization.CultureInfo("en-GB"));

                        //DateTime dateTime = DateTime.ParseExact("01-Jan-18", "dd-MMM-yy", CultureInfo.InvariantCulture);
                        //string newDateString = dateTime.ToString("yyyy-mmmm-dd");
                        ////En to TH
                        //DateTime strSendDate2 = DateTime.ParseExact("01-Jan-18 12:00:00 AM", "dd-MMMM-yy HH:mm:ss:tt", new System.Globalization.CultureInfo("en-GB"));
                        //var dateC = strSendDate2.ToString("yyyy-mm-dd", new System.Globalization.CultureInfo("th-TH"));
                        x.status = row["status"].ToString();
                        model.Add(x);
                }
            }
            return model;
        }
        #endregion

        #region "Delete Vehicle"
        public ResultModel DeleteVehicle(string id, string User)
        {
            var model = new ResultModel();
            using (SqlConnection db = new SqlConnection(_CON_STR))
            {
                if (db.State == ConnectionState.Closed) db.Open();
                SqlCommand cm = new SqlCommand("[sp_SetVerhicle]", db);
                cm.CommandType = CommandType.StoredProcedure;
                cm.Parameters.AddWithValue("@flag", "Cancel");
                cm.Parameters.AddWithValue("@ind", id);
                cm.Parameters.AddWithValue("@User", User);
                //cm.Parameters.AddWithValue("@IP", HttpContext.Current.Request.UserHostAddress);
                SqlDataReader dr = cm.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    model.Result = dr["result"].ToString();
                    model.Message = dr["msg"].ToString();
                }
            }
            return model;
        }
        #endregion

        #region "Re-DeleteVehicle"
        public ResultModel ReDelete(string id, string User)
        {
            var model = new ResultModel();
            using (SqlConnection db = new SqlConnection(_CON_STR))
            {
                if (db.State == ConnectionState.Closed) db.Open();
                SqlCommand cm = new SqlCommand("[sp_SetVerhicle]", db);
                cm.CommandType = CommandType.StoredProcedure;
                cm.Parameters.AddWithValue("@flag", "Re-Cancel");
                cm.Parameters.AddWithValue("@ind", id);
                cm.Parameters.AddWithValue("@User", User);
                //cm.Parameters.AddWithValue("@IP", HttpContext.Current.Request.UserHostAddress);
                SqlDataReader dr = cm.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    model.Result = dr["result"].ToString();
                    model.Message = dr["msg"].ToString();
                }
            }
            return model;
        }
        #endregion
    }
}
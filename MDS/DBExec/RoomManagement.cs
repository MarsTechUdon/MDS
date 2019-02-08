using System;
using MDS.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace MDS.DBExec
{
    public class RoomManagement
    {
        public static string _CON_STR = ConfigurationManager.ConnectionStrings["MDS"].ConnectionString;

        //get ข้อมูล
        public List<RoomModel> GetAllRoom()
        {
            List<RoomModel> model = new List<RoomModel>();
            using (SqlConnection db = new SqlConnection(_CON_STR))
            {
                SqlCommand cm = new SqlCommand("[sp_GetRoom]", db);
                cm.CommandType = CommandType.StoredProcedure;
                cm.Parameters.AddWithValue("@flag", "GetAll");

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cm;
                DataTable dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow row in dt.Rows)
                {
                    RoomModel x = new RoomModel();
                    x.Ind = row["Ind"].ToString();
                    x.RoomNo = row["RoomNo"].ToString();
                    x.RoomName = row["RoomName"].ToString();
                    x.Seat = row["Seat"].ToString();
                    x.Area = row["Area"].ToString();
                    x.Status = row["Status"].ToString();
                    x.Insertdate = row["Insertdate"].ToString();

                    model.Add(x);
                }
            }
            return model;
        }


        public RoomModel AddRoom2(RoomModel ugmData, string Username)
        {
            var model = new RoomModel();
            using (SqlConnection db = new SqlConnection(_CON_STR))
            {
                SqlCommand cm = new SqlCommand("[sp_SetRoom]", db);

                cm.Parameters.AddWithValue("@flag", "Add");
               cm.Parameters.AddWithValue("@User", Username);
                cm.Parameters.AddWithValue("@RoomNo", ugmData.RoomNo);
                cm.Parameters.AddWithValue("@RoomName", ugmData.RoomName);
                cm.Parameters.AddWithValue("@Seat", ugmData.Seat);
                cm.Parameters.AddWithValue("@Area", ugmData.Area);

                cm.CommandType = CommandType.StoredProcedure;
                if (db.State == ConnectionState.Closed) db.Open();
                SqlDataReader dr = cm.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    model.result = dr["result"].ToString();
                    model.msg = dr["msg"].ToString();
                    model.Ind = dr["Ind"].ToString();
                }
            }
            return model;
        }

        public RoomModel EditRoom2(RoomModel ugmData, string Username)
        {
            var model = new RoomModel();
            using (SqlConnection db = new SqlConnection(_CON_STR))
            {
                SqlCommand cm = new SqlCommand("[sp_SetRoom]", db);

                cm.Parameters.AddWithValue("@flag", "Edit");
                cm.Parameters.AddWithValue("@Ind", ugmData.Ind);
                cm.Parameters.AddWithValue("@User", Username);
                cm.Parameters.AddWithValue("@RoomNo", ugmData.RoomNo);
                cm.Parameters.AddWithValue("@RoomName", ugmData.RoomName);
                cm.Parameters.AddWithValue("@Seat", ugmData.Seat);
                cm.Parameters.AddWithValue("@Area", ugmData.Area);

                cm.CommandType = CommandType.StoredProcedure;
                if (db.State == ConnectionState.Closed) db.Open();
                SqlDataReader dr = cm.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    model.result = dr["result"].ToString();
                    model.msg = dr["msg"].ToString();
                    model.Ind = dr["Ind"].ToString();
                }
            }
            return model;
        }

        public RoomModel CancelRoom2(RoomModel ugmData, string Username)
        {
            var model = new RoomModel();
            using (SqlConnection db = new SqlConnection(_CON_STR))
            {
                SqlCommand cm = new SqlCommand("[sp_SetRoom]", db);

                cm.Parameters.AddWithValue("@flag", "Cancel");
                cm.Parameters.AddWithValue("@Ind", ugmData.Ind);
                cm.Parameters.AddWithValue("@User", Username);


                cm.CommandType = CommandType.StoredProcedure;
                if (db.State == ConnectionState.Closed) db.Open();
                SqlDataReader dr = cm.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    model.result = dr["result"].ToString();
                    model.msg = dr["msg"].ToString();
                    model.Ind = dr["Ind"].ToString();
                }
            }
            return model;
        }

        public RoomModel ReCancelRoom2(RoomModel ugmData, string Username)
        {
            var model = new RoomModel();
            using (SqlConnection db = new SqlConnection(_CON_STR))
            {
                SqlCommand cm = new SqlCommand("[sp_SetRoom]", db);

                cm.Parameters.AddWithValue("@flag", "Re-Cancel");
                cm.Parameters.AddWithValue("@Ind", ugmData.Ind);
                cm.Parameters.AddWithValue("@User", Username);

                cm.CommandType = CommandType.StoredProcedure;
                if (db.State == ConnectionState.Closed) db.Open();
                SqlDataReader dr = cm.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    model.result = dr["result"].ToString();
                    model.msg = dr["msg"].ToString();
                    model.Ind = dr["Ind"].ToString();
                }
            }
            return model;
        }


    }
}
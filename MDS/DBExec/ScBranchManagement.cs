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
    public class ScBranchManagement
    {
        public static string _CON_STR = ConfigurationManager.ConnectionStrings["MDS"].ConnectionString;

        //get ข้อมูล
        public List<ScBranchModel> GetAllScBranch()
        {
            List<ScBranchModel> model = new List<ScBranchModel>();
            using (SqlConnection db = new SqlConnection(_CON_STR))
            {
                SqlCommand cm = new SqlCommand("[sp_GetScBranch]", db);
                cm.CommandType = CommandType.StoredProcedure;
                cm.Parameters.AddWithValue("@flag", "GetAll");

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cm;
                DataTable dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow row in dt.Rows)
                {
                    ScBranchModel x = new ScBranchModel();
                    x.Bid = row["Bid"].ToString();
                    x.BranchID = row["BranchID"].ToString();
                    x.BranchName = row["BranchName"].ToString();
                    x.Branchtxt = row["Branchtxt"].ToString();
                    x.Status = row["Status"].ToString();

                    model.Add(x);
                }
            }
            return model;
        }


        public ScBranchModel AddScBranch2(ScBranchModel ugmData)
        {
            var model = new ScBranchModel();
            using (SqlConnection db = new SqlConnection(_CON_STR))
            {
                SqlCommand cm = new SqlCommand("[sp_SetScBranch]", db);

                cm.Parameters.AddWithValue("@flag", "Add");
                cm.Parameters.AddWithValue("@BranchID", ugmData.BranchID);
                cm.Parameters.AddWithValue("@BranchName", ugmData.BranchName);
                cm.Parameters.AddWithValue("@Branchtxt", ugmData.Branchtxt);

                cm.CommandType = CommandType.StoredProcedure;
                if (db.State == ConnectionState.Closed) db.Open();
                SqlDataReader dr = cm.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    model.result = dr["result"].ToString();
                    model.msg = dr["msg"].ToString();
                    model.Bid = dr["Bid"].ToString();
                }
            }
            return model;
        }

        public ScBranchModel EditScBranch2(ScBranchModel ugmData)
        {
            var model = new ScBranchModel();
            using (SqlConnection db = new SqlConnection(_CON_STR))
            {
                SqlCommand cm = new SqlCommand("[sp_SetScBranch]", db);

                cm.Parameters.AddWithValue("@flag", "Edit");
                cm.Parameters.AddWithValue("@Bid", ugmData.Bid);
                cm.Parameters.AddWithValue("@BranchID", ugmData.BranchID);
                cm.Parameters.AddWithValue("@BranchName", ugmData.BranchName);
                cm.Parameters.AddWithValue("@Branchtxt", ugmData.Branchtxt);

                cm.CommandType = CommandType.StoredProcedure;
                if (db.State == ConnectionState.Closed) db.Open();
                SqlDataReader dr = cm.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    model.result = dr["result"].ToString();
                    model.msg = dr["msg"].ToString();
                    model.Bid = dr["Bid"].ToString();
                }
            }
            return model;
        }

        public ScBranchModel CancelScBranch2(ScBranchModel ugmData)
        {
            var model = new ScBranchModel();
            using (SqlConnection db = new SqlConnection(_CON_STR))
            {
                SqlCommand cm = new SqlCommand("[sp_SetScBranch]", db);

                cm.Parameters.AddWithValue("@flag", "Cancel");
                cm.Parameters.AddWithValue("@Bid", ugmData.Bid);


                cm.CommandType = CommandType.StoredProcedure;
                if (db.State == ConnectionState.Closed) db.Open();
                SqlDataReader dr = cm.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    model.result = dr["result"].ToString();
                    model.msg = dr["msg"].ToString();
                    model.Bid = dr["Bid"].ToString();
                }
            }
            return model;
        }

        public ScBranchModel ReCancelScBranch2(ScBranchModel ugmData)
        {
            var model = new ScBranchModel();
            using (SqlConnection db = new SqlConnection(_CON_STR))
            {
                SqlCommand cm = new SqlCommand("[sp_SetScBranch]", db);

                cm.Parameters.AddWithValue("@flag", "Re-Cancel");
                cm.Parameters.AddWithValue("@Bid", ugmData.Bid);

                cm.CommandType = CommandType.StoredProcedure;
                if (db.State == ConnectionState.Closed) db.Open();
                SqlDataReader dr = cm.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    model.result = dr["result"].ToString();
                    model.msg = dr["msg"].ToString();
                    model.Bid = dr["Bid"].ToString();
                }
            }
            return model;
        }
    }
}
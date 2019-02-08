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
    public class ComCreditcardManagement
    {
        public static string _CON_STR = ConfigurationManager.ConnectionStrings["MDS"].ConnectionString;

        //get ข้อมูล
        public List<ComCreditcardModel> GetAllComCreditcard()
        {
            List<ComCreditcardModel> model = new List<ComCreditcardModel>();
            using (SqlConnection db = new SqlConnection(_CON_STR))
            {
                SqlCommand cm = new SqlCommand("[sp_GetComCreditcard]", db);
                cm.CommandType = CommandType.StoredProcedure;
                cm.Parameters.AddWithValue("@flag", "GetAll");

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cm;
                DataTable dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow row in dt.Rows)
                {
                    ComCreditcardModel x = new ComCreditcardModel();
                    x.Cid = row["Cid"].ToString();
                    x.CrType = row["CrType"].ToString();
                    x.CrCharge = row["CrCharge"].ToString();
                    x.DisplayName = row["DisplayName"].ToString();
                    x.Status = row["Status"].ToString();

                    model.Add(x);
                }
            }
            return model;
        }


        public ComCreditcardModel AddComCreditcard2(ComCreditcardModel ugmData)
        {
            var model = new ComCreditcardModel();
            using (SqlConnection db = new SqlConnection(_CON_STR))
            {
                SqlCommand cm = new SqlCommand("[sp_SetComCreditcard]", db);

                cm.Parameters.AddWithValue("@flag", "Add");
                cm.Parameters.AddWithValue("@CrType", ugmData.CrType);
                cm.Parameters.AddWithValue("@CrCharge", ugmData.CrCharge);
                cm.Parameters.AddWithValue("@DisplayName", ugmData.DisplayName);

                cm.CommandType = CommandType.StoredProcedure;
                if (db.State == ConnectionState.Closed) db.Open();
                SqlDataReader dr = cm.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    model.result = dr["result"].ToString();
                    model.msg = dr["msg"].ToString();
                    model.Cid = dr["Cid"].ToString();
                }
            }
            return model;
        }

        public ComCreditcardModel EditComCreditcard2(ComCreditcardModel ugmData)
        {
            var model = new ComCreditcardModel();
            using (SqlConnection db = new SqlConnection(_CON_STR))
            {
                SqlCommand cm = new SqlCommand("[sp_SetComCreditcard]", db);

                cm.Parameters.AddWithValue("@flag", "Edit");
                cm.Parameters.AddWithValue("@Cid", ugmData.Cid);
                cm.Parameters.AddWithValue("@CrType", ugmData.CrType);
                cm.Parameters.AddWithValue("@CrCharge", ugmData.CrCharge);
                cm.Parameters.AddWithValue("@DisplayName", ugmData.DisplayName);

                cm.CommandType = CommandType.StoredProcedure;
                if (db.State == ConnectionState.Closed) db.Open();
                SqlDataReader dr = cm.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    model.result = dr["result"].ToString();
                    model.msg = dr["msg"].ToString();
                    model.Cid = dr["Cid"].ToString();
                }
            }
            return model;
        }

        public ComCreditcardModel CancelComCreditcard2(ComCreditcardModel ugmData)
        {
            var model = new ComCreditcardModel();
            using (SqlConnection db = new SqlConnection(_CON_STR))
            {
                SqlCommand cm = new SqlCommand("[sp_SetComCreditcard]", db);

                cm.Parameters.AddWithValue("@flag", "Cancel");
                cm.Parameters.AddWithValue("@Cid", ugmData.Cid);


                cm.CommandType = CommandType.StoredProcedure;
                if (db.State == ConnectionState.Closed) db.Open();
                SqlDataReader dr = cm.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    model.result = dr["result"].ToString();
                    model.msg = dr["msg"].ToString();
                    model.Cid = dr["Cid"].ToString();
                }
            }
            return model;
        }

        public ComCreditcardModel ReCancelComCreditcard2(ComCreditcardModel ugmData)
        {
            var model = new ComCreditcardModel();
            using (SqlConnection db = new SqlConnection(_CON_STR))
            {
                SqlCommand cm = new SqlCommand("[sp_SetComCreditcard]", db);

                cm.Parameters.AddWithValue("@flag", "Re-Cancel");
                cm.Parameters.AddWithValue("@Cid", ugmData.Cid);

                cm.CommandType = CommandType.StoredProcedure;
                if (db.State == ConnectionState.Closed) db.Open();
                SqlDataReader dr = cm.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    model.result = dr["result"].ToString();
                    model.msg = dr["msg"].ToString();
                    model.Cid = dr["Cid"].ToString();
                }
            }
            return model;
        }

    }
}
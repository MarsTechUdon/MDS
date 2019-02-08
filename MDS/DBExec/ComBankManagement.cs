using MDS.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MDS.DBExec
{
    public class ComBankManagement
    {
        public static string _CON_STR = ConfigurationManager.ConnectionStrings["MDS"].ConnectionString;

        #region "Add ComBank"
        public ComBankModel addComBank(ComBankModel modelData)
        {
            var model = new ComBankModel();
            using (SqlConnection db = new SqlConnection(_CON_STR))
            {
                if (db.State == ConnectionState.Closed) db.Open();
                SqlCommand cm = new SqlCommand("[sp_SetComBank]", db);
                cm.CommandType = CommandType.StoredProcedure;
                cm.Parameters.AddWithValue("@flag", "Add");
                cm.Parameters.AddWithValue("@BankAbbr", modelData.bankabbr);
                cm.Parameters.AddWithValue("@BankName", modelData.bankname);
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

        #region GetAllComBank
        public List<ComBankModel> GetAllComBank()
        {
            List<ComBankModel> model = new List<ComBankModel>();
            using (SqlConnection db = new SqlConnection(_CON_STR))
            {
                SqlCommand cm = new SqlCommand("[sp_GetComBank]", db);
                cm.CommandType = CommandType.StoredProcedure;
                cm.Parameters.AddWithValue("@flag", "GetAll");

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cm;
                DataTable dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow row in dt.Rows)
                {
                    ComBankModel x = new ComBankModel();
                    x.bid = row["Bid"].ToString();
                    x.bankabbr = row["BankAbbr"].ToString();
                    x.bankname = row["BankName"].ToString();
                    x.status = row["status"].ToString();
                    model.Add(x);
                }
            }
            return model;
        }
        #endregion

        #region "Edit ComBank"
        public ComBankModel editComBank(ComBankModel modelData)
        {
            var model = new ComBankModel();
            using (SqlConnection db = new SqlConnection(_CON_STR))
            {
                if (db.State == ConnectionState.Closed) db.Open();
                SqlCommand cm = new SqlCommand("[sp_SetComBank]", db);
                cm.CommandType = CommandType.StoredProcedure;
                cm.Parameters.AddWithValue("@flag", "Edit");
                cm.Parameters.AddWithValue("@Bid", modelData.bid);
                cm.Parameters.AddWithValue("@BankAbbr", modelData.bankabbr);
                cm.Parameters.AddWithValue("@BankName", modelData.bankname);
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

        #region "Delete ComBank"
        public ResultModel DeleteComBank(string id)
        {
            var model = new ResultModel();
            using (SqlConnection db = new SqlConnection(_CON_STR))
            {
                if (db.State == ConnectionState.Closed) db.Open();
                SqlCommand cm = new SqlCommand("[sp_SetComBank]", db);
                cm.CommandType = CommandType.StoredProcedure;
                cm.Parameters.AddWithValue("@flag", "Cancel");
                cm.Parameters.AddWithValue("@Bid", id);
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

        #region "Re-DeleteComBank"
        public ResultModel ReDeleteComBank(string id)
        {
            var model = new ResultModel();
            using (SqlConnection db = new SqlConnection(_CON_STR))
            {
                if (db.State == ConnectionState.Closed) db.Open();
                SqlCommand cm = new SqlCommand("[sp_SetComBank]", db);
                cm.CommandType = CommandType.StoredProcedure;
                cm.Parameters.AddWithValue("@flag", "Re-Cancel");
                cm.Parameters.AddWithValue("@Bid", id);
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
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
    public class ComBankAccountManagement
    {
        public static string _CON_STR = ConfigurationManager.ConnectionStrings["MDS"].ConnectionString;

        #region "Add ComBankAccount"
        public ComBankAccountModel addComBankAccount(ComBankAccountModel modelData)
        {
            var model = new ComBankAccountModel();
            using (SqlConnection db = new SqlConnection(_CON_STR))
            {
                if (db.State == ConnectionState.Closed) db.Open();
                SqlCommand cm = new SqlCommand("[sp_SetComBankAccount]", db);
                cm.CommandType = CommandType.StoredProcedure;
                cm.Parameters.AddWithValue("@flag", "Add");
                cm.Parameters.AddWithValue("@AccountNo", modelData.AccountNo);
                cm.Parameters.AddWithValue("@AccountName", modelData.AccountName);
                cm.Parameters.AddWithValue("@AccountType", modelData.AccountType);
                cm.Parameters.AddWithValue("@BankName", modelData.BankName);
                cm.Parameters.AddWithValue("@BankBranch", modelData.BankBranch);
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

        #region GetAllComBankAccount
        public List<ComBankAccountModel> GetAllComBankAccount()
        {
            List<ComBankAccountModel> model = new List<ComBankAccountModel>();
            using (SqlConnection db = new SqlConnection(_CON_STR))
            {
                SqlCommand cm = new SqlCommand("[sp_GetComBankAccount]", db);
                cm.CommandType = CommandType.StoredProcedure;
                cm.Parameters.AddWithValue("@flag", "GetAll");

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cm;
                DataTable dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow row in dt.Rows)
                {
                    ComBankAccountModel x = new ComBankAccountModel();
                    x.AccountID = row["AccountID"].ToString();
                    x.AccountNo = row["AccountNo"].ToString();
                    x.AccountName = row["AccountName"].ToString();
                    x.AccountType = row["AccountType"].ToString();
                    x.BankName = row["BankName"].ToString();
                    x.BankBranch = row["BankBranch"].ToString();
                    x.status = row["status"].ToString();
                    model.Add(x);
                }
            }
            return model;
        }
        #endregion

        #region "Edit ComBankAccount"
        public ComBankAccountModel editComBankAccount(ComBankAccountModel modelData)
        {
            var model = new ComBankAccountModel();
            using (SqlConnection db = new SqlConnection(_CON_STR))
            {
                if (db.State == ConnectionState.Closed) db.Open();
                SqlCommand cm = new SqlCommand("[sp_SetComBankAccount]", db);
                cm.CommandType = CommandType.StoredProcedure;
                cm.Parameters.AddWithValue("@flag", "Edit");
                cm.Parameters.AddWithValue("@AccountID", modelData.AccountID);
                cm.Parameters.AddWithValue("@AccountNo", modelData.AccountNo);
                cm.Parameters.AddWithValue("@AccountName", modelData.AccountName);
                cm.Parameters.AddWithValue("@AccountType", modelData.AccountType);
                cm.Parameters.AddWithValue("@BankName", modelData.BankName);
                cm.Parameters.AddWithValue("@BankBranch", modelData.BankBranch);
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

        #region "Delete ComBankAccount"
        public ResultModel DeleteComBankAccount(string id)
        {
            var model = new ResultModel();
            using (SqlConnection db = new SqlConnection(_CON_STR))
            {
                if (db.State == ConnectionState.Closed) db.Open();
                SqlCommand cm = new SqlCommand("[sp_SetComBankAccount]", db);
                cm.CommandType = CommandType.StoredProcedure;
                cm.Parameters.AddWithValue("@flag", "Cancel");
                cm.Parameters.AddWithValue("@AccountID", id);
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

        #region "Re-DeleteComBankAccount"
        public ResultModel ReDeleteComBankAccount(string id)
        {
            var model = new ResultModel();
            using (SqlConnection db = new SqlConnection(_CON_STR))
            {
                if (db.State == ConnectionState.Closed) db.Open();
                SqlCommand cm = new SqlCommand("[sp_SetComBankAccount]", db);
                cm.CommandType = CommandType.StoredProcedure;
                cm.Parameters.AddWithValue("@flag", "Re-Cancel");
                cm.Parameters.AddWithValue("@AccountID", id);
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
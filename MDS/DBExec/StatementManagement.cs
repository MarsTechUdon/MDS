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
    public class StatementManagement
    {
        public static string _CON_STR = ConfigurationManager.ConnectionStrings["MDS"].ConnectionString;

        //get ข้อมูล
        public List<StatementModel> GetListAccount()
        {
            List<StatementModel> model = new List<StatementModel>();
            using (SqlConnection db = new SqlConnection(_CON_STR))
            {
                //String t = DateTime.Now.ToString("yyyy-MM-dd");
                string momo = DateTime.Now.ToString("yyyy");
                var num = Convert.ToInt32(momo);
                var t = "";
                if (num >= 2561)
                {
                    num = num - 543;
                    var jojo = DateTime.Now.ToString("MM-dd");
                   t = num + "-" + jojo;
                }
                else
                {
                   t = DateTime.Now.ToString("yyyy-MM-dd");
                }
                SqlCommand cm = new SqlCommand("[sp_SCBankStatement]", db);
                cm.CommandType = CommandType.StoredProcedure;
                cm.Parameters.AddWithValue("@flag", "listaccount");
                cm.Parameters.AddWithValue("@fdate", t);
                cm.Parameters.AddWithValue("@tdate", t);

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cm;
                DataTable dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow row in dt.Rows)
                {
                    StatementModel x = new StatementModel();
                    //x.accountID = row["AccountID"].ToString();
                    x.accountno = row["AccountNo"].ToString();
                    x.accountname = row["AccountName"].ToString();

                    model.Add(x);
                }
            }
            return model;
        }

        public List<StatementModel> GetListAccount2(string ugmData, string ugmData2)
        {
            List<StatementModel> model = new List<StatementModel>();
            using (SqlConnection db = new SqlConnection(_CON_STR))
            {

                SqlCommand cm = new SqlCommand("[sp_SCBankStatement]", db);
                cm.CommandType = CommandType.StoredProcedure;
                cm.Parameters.AddWithValue("@flag", "listaccount");
                cm.Parameters.AddWithValue("@fdate", ugmData);
                cm.Parameters.AddWithValue("@tdate", ugmData2);

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cm;
                DataTable dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow row in dt.Rows)
                {
                    StatementModel x = new StatementModel();
                    //x.accountID = row["AccountID"].ToString();
                    x.accountno = row["AccountNo"].ToString();
                    x.accountname = row["AccountName"].ToString();

                    model.Add(x);
                }
            }
            return model;
        }

        public List<StatementModel> GetStByAccount()
        {
            List<StatementModel> model = new List<StatementModel>();
            float num_amount=0;
            using (SqlConnection db = new SqlConnection(_CON_STR))
            {
                //String t = DateTime.Now.ToString("yyyy-MM-dd");
                string momo = DateTime.Now.ToString("yyyy");
                var num = Convert.ToInt32(momo);
                var t = "";
                if (num >= 2561)
                {
                    num = num - 543;
                    var jojo = DateTime.Now.ToString("MM-dd");
                    t = num + "-" + jojo;
                }
                else
                {
                    t = DateTime.Now.ToString("yyyy-MM-dd");
                }
                SqlCommand cm = new SqlCommand("[sp_SCBankStatement]", db);
                cm.CommandType = CommandType.StoredProcedure;
                cm.Parameters.AddWithValue("@flag", "getStByAccount");
                cm.Parameters.AddWithValue("@fdate", t);
                cm.Parameters.AddWithValue("@tdate", t);

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cm;
                DataTable dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow row in dt.Rows)
                {
                    
                    StatementModel x = new StatementModel();
                    x.ind = row["ind"].ToString();
                    x.accountno2 = row["accountno"].ToString();
                    if (row["approvedate"].ToString() == "")
                    {
                        x.approvedate = row["approvedate"].ToString();
                    }
                    else
                    {
                        x.approvedate = Convert.ToDateTime(row["approvedate"].ToString()).ToString("yyyy-MM-dd HH:mm", new System.Globalization.CultureInfo("en-GB"));
                    }
                    x.insertdate = Convert.ToDateTime(row["insertdate"].ToString()).ToString("yyyy-MM-dd HH:mm", new System.Globalization.CultureInfo("en-GB"));
                    x.detail = row["detail"].ToString();
                    x.amount = row["amount"].ToString();
                    x.re_f = row["ref"].ToString();
                    x.transdate = Convert.ToDateTime(row["transdate"].ToString()).ToString("yyyy-MM-dd HH:mm", new System.Globalization.CultureInfo("en-GB"));

                    model.Add(x);
                }
            }
            return model;
        }
        public List<StatementModel> GetStByAccount2(string ugmData, string ugmData2)
        {
            List<StatementModel> model = new List<StatementModel>();
           // var model = new List<StatementModel>();
            using (SqlConnection db = new SqlConnection(_CON_STR))
            {
                SqlCommand cm = new SqlCommand("[sp_SCBankStatement]", db);
                cm.CommandType = CommandType.StoredProcedure;
                cm.Parameters.AddWithValue("@flag", "getStByAccount");
                cm.Parameters.AddWithValue("@fdate", ugmData);
                cm.Parameters.AddWithValue("@tdate", ugmData2);

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cm;
                DataTable dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow row in dt.Rows)
                {
                    StatementModel x = new StatementModel();
                    x.ind = row["ind"].ToString();
                    x.accountno2 = row["accountno"].ToString();

                    if (row["approvedate"].ToString() == "")
                    {
                        x.approvedate = row["approvedate"].ToString();
                    }
                    else
                    {
                        x.approvedate = Convert.ToDateTime(row["approvedate"].ToString()).ToString("yyyy-MM-dd HH:mm", new System.Globalization.CultureInfo("en-GB"));
                    }
                    x.insertdate = Convert.ToDateTime(row["insertdate"].ToString()).ToString("yyyy-MM-dd HH:mm", new System.Globalization.CultureInfo("en-GB"));
                    x.detail = row["detail"].ToString();
                    x.amount = row["amount"].ToString();
                    x.re_f = row["ref"].ToString();
                    x.transdate = Convert.ToDateTime(row["transdate"].ToString()).ToString("yyyy-MM-dd HH:mm", new System.Globalization.CultureInfo("en-GB"));

                    model.Add(x);
                }

            }
            return model;
        }

        public List<ComBankAccountModel> GetBankaccount()
        {
            List<ComBankAccountModel> model = new List<ComBankAccountModel>();
            using (SqlConnection db = new SqlConnection(_CON_STR))
            {
                SqlCommand cm = new SqlCommand("[sp_SCLookup]", db);
                cm.CommandType = CommandType.StoredProcedure;
                cm.Parameters.AddWithValue("@flag", "lbankaccount");

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cm;
                DataTable dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow row in dt.Rows)
                {
                    ComBankAccountModel x = new ComBankAccountModel();
                    //x.accountID = row["AccountID"].ToString();
                    x.AccountNo = row["AccountNo"].ToString();
                    x.AccountName = row["AccountName"].ToString();

                    model.Add(x);
                }
            }
            return model;
        }
        public StatementModel AddStatement2(StatementModel ugmData, string username)
        {
            var model = new StatementModel();
            using (SqlConnection db = new SqlConnection(_CON_STR))
            {
                string datetime = ugmData.transdate_day + " " + ugmData.transdate_time + ":00";
                SqlCommand cm = new SqlCommand("[sp_SCBankStatement]", db);

                cm.Parameters.AddWithValue("@flag", "Add");
                cm.Parameters.AddWithValue("@accountno", ugmData.accountno);
                cm.Parameters.AddWithValue("@transdate", datetime);
                cm.Parameters.AddWithValue("@amount", ugmData.amount);
                cm.Parameters.AddWithValue("@ref", ugmData.re_f);
                cm.Parameters.AddWithValue("@remark", ugmData.detail);
                cm.Parameters.AddWithValue("@user", username);
                cm.Parameters.AddWithValue("@ip", HttpContext.Current.Request.UserHostAddress);

                cm.CommandType = CommandType.StoredProcedure;
                if (db.State == ConnectionState.Closed) db.Open();
                SqlDataReader dr = cm.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    model.result = dr["result"].ToString();
                    model.msg = dr["msg"].ToString();
                  //  model.ind = dr["Cid"].ToString();
                }
            }
            return model;
        }
        public StatementModel EditStatement2(StatementModel ugmData, string username)
        {
            var model = new StatementModel();
            using (SqlConnection db = new SqlConnection(_CON_STR))
            {
                string datetime = ugmData.transdate_day +" "+ ugmData.transdate_time ;
                ///string datestr = Convert.ToDateTime(datetime).ToString("yyyy-MM-dd HH:mm", new System.Globalization.CultureInfo("en-GB"));
                SqlCommand cm = new SqlCommand("[sp_SCBankStatement]", db);

                cm.Parameters.AddWithValue("@flag", "edit");
                cm.Parameters.AddWithValue("@ind", ugmData.ind);
                cm.Parameters.AddWithValue("@accountno", ugmData.accountno);
                cm.Parameters.AddWithValue("@transdate", datetime);
                cm.Parameters.AddWithValue("@amount", ugmData.amount);
                cm.Parameters.AddWithValue("@ref", ugmData.re_f);
                cm.Parameters.AddWithValue("@remark", ugmData.detail);
                cm.Parameters.AddWithValue("@user", username);
                cm.Parameters.AddWithValue("@ip", HttpContext.Current.Request.UserHostAddress);

                cm.CommandType = CommandType.StoredProcedure;
                if (db.State == ConnectionState.Closed) db.Open();
                SqlDataReader dr = cm.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    model.result = dr["result"].ToString();
                    model.msg = dr["msg"].ToString();
                    model.ind = dr["ind"].ToString();
                }
            }
            return model;

        }

        public StatementModel DeleteStatement2(StatementModel ugmData, string username)
        {
            var model = new StatementModel();
            using (SqlConnection db = new SqlConnection(_CON_STR))
            {
                SqlCommand cm = new SqlCommand("[sp_SCBankStatement]", db);

                cm.Parameters.AddWithValue("@flag", "delete");
                cm.Parameters.AddWithValue("@ind", ugmData.ind);

                cm.CommandType = CommandType.StoredProcedure;
                if (db.State == ConnectionState.Closed) db.Open();
                SqlDataReader dr = cm.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    model.result = dr["result"].ToString();
                    model.msg = dr["msg"].ToString();
                 //   model.ind = dr["Cid"].ToString();
                }
            }
            return model;
        }

        public StatementModel ApproveStatement()
        {
            var model = new StatementModel();
            using (SqlConnection db = new SqlConnection(_CON_STR))
            {
                SqlCommand cm = new SqlCommand("[sp_SCBankStatement]", db);

                cm.Parameters.AddWithValue("@flag", "approvestatement");
                cm.CommandType = CommandType.StoredProcedure;
                if (db.State == ConnectionState.Closed) db.Open();
                SqlDataReader dr = cm.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    model.result = dr["result"].ToString();
                    model.msg = dr["msg"].ToString();
                    //  model.ind = dr["Cid"].ToString();
                }
            }
            return model;
        }
        ////////////////////////////////////////////////////////
        #region "Add Upload"
        public static string addupload(StatementModel modelData, string chk_acno, string user)
        {
            string Result = "";
            using (SqlConnection db = new SqlConnection(_CON_STR))
            {
                if (db.State == ConnectionState.Closed) db.Open();
                SqlCommand cm = new SqlCommand("[sp_SCBankStatement]", db);
                cm.CommandType = CommandType.StoredProcedure;

                if (modelData.accountno != "")
                {
                    cm.Parameters.AddWithValue("@flag", "upload");
                    cm.Parameters.AddWithValue("@accountno", chk_acno);
                    cm.Parameters.AddWithValue("@transdate", modelData.transdate);
                    cm.Parameters.AddWithValue("@effectdate", modelData.effectdate);
                    cm.Parameters.AddWithValue("@description", modelData.description);

                    if (modelData.debit != "")
                    {
                        cm.Parameters.AddWithValue("@debit", Convert.ToDecimal(modelData.debit));
                    }
                    else
                    {
                        cm.Parameters.AddWithValue("@debit", 0);
                    }
                    if (modelData.credit != "")
                    {
                        cm.Parameters.AddWithValue("@credit", Convert.ToDecimal(modelData.credit));
                    }
                    else
                    {
                        cm.Parameters.AddWithValue("@credit", 0);
                    }
                    if (modelData.balance != "")
                    {
                        cm.Parameters.AddWithValue("@balance", Convert.ToDecimal(modelData.balance));
                    }
                    else
                    {
                        cm.Parameters.AddWithValue("@balance", 0);
                    }
                    cm.Parameters.AddWithValue("@channel", modelData.channel);
                    cm.Parameters.AddWithValue("@user", user);
                }

                if (db.State == ConnectionState.Closed) db.Open();
                SqlDataReader dr = cm.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    modelData.msg = dr["msg"].ToString();
                    modelData.result = dr["result"].ToString();
                    dr.Close();
                }
            }
            return Result;
        }
        #endregion
    }
}



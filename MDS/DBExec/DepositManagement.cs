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
    public class DepositManagement
    {
        public static string _CON_STR = ConfigurationManager.ConnectionStrings["MDS"].ConnectionString;

        #region GetListBatch
        public ListBatchModel getListBatch()
        {
            var model = new ListBatchModel();
            model.BatchList = new List<ListBatchModel>();
            model.BankAccount = new List<ComBankAccountModel>();
            model.BranchList = new List<CurrentBranchModel>();
            using (SqlConnection db = new SqlConnection(_CON_STR))
            {
                SqlCommand cm = new SqlCommand("[sp_SCDeposit]", db);
                cm.Parameters.AddWithValue("@flag", "ListBatch");
                cm.CommandType = CommandType.StoredProcedure;
                if (db.State == ConnectionState.Closed) db.Open();
                SqlDataReader dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    var depositModel = new ListBatchModel()
                    {
                        Batchno = dr["Batchno"].ToString(),
                        //BatchDate = dr["BatchDate"].ToString(),
                        BatchDate = Convert.ToDateTime(dr["BatchDate"].ToString()).ToString("dd/MM/yyyy HH:MM", new System.Globalization.CultureInfo("en-GB")),
                        depositdate = Convert.ToDateTime(dr["depositdate"].ToString()).ToString("yyyy-MM-dd", new System.Globalization.CultureInfo("en-GB")),
                        branchid = dr["branchid"].ToString(),
                        item = dr["item"].ToString(),
                        accountno = dr["accountno"].ToString(),
                        insertby = dr["insertby"].ToString(),
                        amount = dr["amount"].ToString(),
                        remark = dr["remark"].ToString(),
                        status = dr["status"].ToString(),
                    };
                    model.BatchList.Add(depositModel);
                }

                dr.NextResult();
                while (dr.Read())
                {
                    var BankAccount = new ComBankAccountModel()
                    {
                        AccountID = dr["AccountID"].ToString(),
                        AccountNo = dr["AccountNo"].ToString(),
                    };
                    model.BankAccount.Add(BankAccount);
                }
                
            }
            using (SqlConnection db = new SqlConnection(_CON_STR))
            {
                SqlCommand cm = new SqlCommand("[sp_GetSCBranch]", db);
                cm.Parameters.AddWithValue("@flag", "GetAll");
                cm.CommandType = CommandType.StoredProcedure;
                if (db.State == ConnectionState.Closed) db.Open();
                SqlDataReader dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    var BranchList = new CurrentBranchModel()
                    {
                        branchid = dr["branchid"].ToString(),
                        branchname = dr["branchname"].ToString(),
                    };
                    model.BranchList.Add(BranchList);
                }
            }
            return model;
        }
        #endregion

        #region GetListBatch
        public string CheckMClosePerUser()
        {
            var chk = "";
            //CheckMClosePerUser
            using (SqlConnection db = new SqlConnection(_CON_STR))
            {
                SqlCommand cm = new SqlCommand("[sp_SetCompany]", db);
                cm.Parameters.AddWithValue("@flag", "getParamInfo");
                cm.CommandType = CommandType.StoredProcedure;
                if (db.State == ConnectionState.Closed) db.Open();
                SqlDataReader dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    chk = dr["ParaMClosePerUser"].ToString();
                }
            }
            return chk;
        }
        #endregion

        #region GetListDeposit
        public ListBatchModel getListDeposit(string username)
        {
            var model = new ListBatchModel();
            model.BatchList = new List<ListBatchModel>();
            model.BankAccount = new List<ComBankAccountModel>();
            model.BranchList = new List<CurrentBranchModel>();

            //Select Deposit
            using (SqlConnection db = new SqlConnection(_CON_STR))
            {
                SqlCommand cm = new SqlCommand("[sp_SCDeposit]", db);
                cm.Parameters.AddWithValue("@flag", "ListForDeposit");
                cm.Parameters.AddWithValue("@DepositUser", username);
                cm.CommandType = CommandType.StoredProcedure;
                if (db.State == ConnectionState.Closed) db.Open();
                SqlDataReader dr = cm.ExecuteReader();
                decimal summary = 0;
                while (dr.Read())
                {
                    if (dr["rcby"].ToString() == "cash" || dr["rcby"].ToString() == "dbnote" || dr["rcby"].ToString() == "cheque")
                    {
                        summary = summary + decimal.Parse(dr["amount"].ToString());
                    }

                    var depositModel = new ListBatchModel()
                    {
                        rno = dr["rno"].ToString(),
                        branch = dr["branch"].ToString(),
                        paydate = dr["paydate"].ToString(),
                        //paydate = Convert.ToDateTime(dr["paydate"].ToString()).ToString("yyyy-MM-dd", new System.Globalization.CultureInfo("en-GB")),
                        //keydate = Convert.ToDateTime(dr["keydate"].ToString()).ToString("yyyy-MM-dd", new System.Globalization.CultureInfo("en-GB")),
                        keydate = dr["keydate"].ToString(),
                        uname = dr["uname"].ToString(),
                        detail = dr["detail"].ToString(),
                        amount2 = dr["amount"].ToString(),
                        payid = dr["payid"].ToString(),
                        status2 = dr["status"].ToString(),
                        rcby = dr["rcby"].ToString(),
                        rcbydesc = dr["rcbydesc"].ToString(),
                        summary = summary.ToString()
                    };
                    model.BatchList.Add(depositModel);
                }
            }

            // Select AccountNO
            using (SqlConnection db = new SqlConnection(_CON_STR))
            {
                SqlCommand cm = new SqlCommand("[sp_SCLookup]", db);
                cm.Parameters.AddWithValue("@flag", "lbankaccount");
                cm.CommandType = CommandType.StoredProcedure;
                if (db.State == ConnectionState.Closed) db.Open();
                SqlDataReader dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    var BankAccount = new ComBankAccountModel()
                    {
                        AccountNo = dr["AccountNo"].ToString(),
                        AccountName = dr["accountname"].ToString(),
                    };
                    model.BankAccount.Add(BankAccount);
                }
            }

            // Select Branch
            using (SqlConnection db = new SqlConnection(_CON_STR))
            {
                SqlCommand cm = new SqlCommand("[sp_GetSCBranch]", db);
                cm.Parameters.AddWithValue("@flag", "GetAll");
                cm.CommandType = CommandType.StoredProcedure;
                if (db.State == ConnectionState.Closed) db.Open();
                SqlDataReader dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    var BranchList = new CurrentBranchModel()
                    {
                        branchid = dr["branchid"].ToString(),
                        branchname = dr["branchname"].ToString(),
                    };
                    model.BranchList.Add(BranchList);
                }
            }

            ////Select Deposit
            //using (SqlConnection db = new SqlConnection(_CON_STR))
            //{
            //    SqlCommand cm = new SqlCommand("[sp_SCDeposit]", db);
            //    cm.Parameters.AddWithValue("@flag", "ListForDeposit");
            //    cm.Parameters.AddWithValue("@branchid", branchid);
            //    cm.CommandType = CommandType.StoredProcedure;
            //    if (db.State == ConnectionState.Closed) db.Open();
            //    SqlDataReader dr = cm.ExecuteReader();
            //    decimal summary = 0;
            //    while (dr.Read())
            //    {
            //        if (dr["rcby"].ToString() == "cash" || dr["rcby"].ToString() == "dbnote" || dr["rcby"].ToString() == "cheque")
            //        {
            //            summary = summary + decimal.Parse(dr["amount"].ToString());
            //        }

            //        var depositModel = new ListBatchModel()
            //        {
            //            rno = dr["rno"].ToString(),
            //            branch = dr["branch"].ToString(),
            //            paydate = dr["paydate"].ToString(),
            //            //paydate = Convert.ToDateTime(dr["paydate"].ToString()).ToString("yyyy-MM-dd", new System.Globalization.CultureInfo("en-GB")),
            //            //keydate = Convert.ToDateTime(dr["keydate"].ToString()).ToString("yyyy-MM-dd", new System.Globalization.CultureInfo("en-GB")),
            //            keydate = dr["keydate"].ToString(),
            //            uname = dr["uname"].ToString(),
            //            detail = dr["detail"].ToString(),
            //            amount2 = dr["amount"].ToString(),
            //            payid = dr["payid"].ToString(),
            //            status2 = dr["status"].ToString(),
            //            rcby = dr["rcby"].ToString(),
            //            rcbydesc = dr["rcbydesc"].ToString(),
            //            summary = summary.ToString(),
            //            DepositUser = chk
            //        };
            //        model.BatchList.Add(depositModel);
            //    }
            //}

            //// Select AccountNO
            //using (SqlConnection db = new SqlConnection(_CON_STR))
            //{
            //    SqlCommand cm = new SqlCommand("[sp_SCLookup]", db);
            //    cm.Parameters.AddWithValue("@flag", "lbankaccount");
            //    cm.CommandType = CommandType.StoredProcedure;
            //    if (db.State == ConnectionState.Closed) db.Open();
            //    SqlDataReader dr = cm.ExecuteReader();
            //    while (dr.Read())
            //    {
            //        var BankAccount = new ComBankAccountModel()
            //        {
            //            AccountNo = dr["AccountNo"].ToString(),
            //            AccountName = dr["accountname"].ToString(),
            //        };
            //        model.BankAccount.Add(BankAccount);
            //    }
            //}

            //// Select Branch
            //using (SqlConnection db = new SqlConnection(_CON_STR))
            //{
            //    SqlCommand cm = new SqlCommand("[sp_GetSCBranch]", db);
            //    cm.Parameters.AddWithValue("@flag", "GetAll");
            //    cm.CommandType = CommandType.StoredProcedure;
            //    if (db.State == ConnectionState.Closed) db.Open();
            //    SqlDataReader dr = cm.ExecuteReader();
            //    while (dr.Read())
            //    {
            //        var BranchList = new CurrentBranchModel()
            //        {
            //            branchid = dr["branchid"].ToString(),
            //            branchname = dr["branchname"].ToString(),
            //        };
            //        model.BranchList.Add(BranchList);
            //    }
            //}

            return model;
        }
        #endregion

        #region GetListDepositBranch
        public ListBatchModel getListDeposit2(string branchid)
        {
            var model = new ListBatchModel();
            model.BatchList = new List<ListBatchModel>();
            model.BankAccount = new List<ComBankAccountModel>();
            model.BranchList = new List<CurrentBranchModel>();

            //Select Deposit
            using (SqlConnection db = new SqlConnection(_CON_STR))
            {
                SqlCommand cm = new SqlCommand("[sp_SCDeposit]", db);
                cm.Parameters.AddWithValue("@flag", "ListForDeposit");
                cm.Parameters.AddWithValue("@branchid", branchid);
                cm.CommandType = CommandType.StoredProcedure;
                if (db.State == ConnectionState.Closed) db.Open();
                SqlDataReader dr = cm.ExecuteReader();
                decimal summary = 0;
                while (dr.Read())
                {
                    if (dr["rcby"].ToString() == "cash" || dr["rcby"].ToString() == "dbnote" || dr["rcby"].ToString() == "cheque")
                    {
                        summary = summary + decimal.Parse(dr["amount"].ToString());
                    }

                    var depositModel = new ListBatchModel()
                    {
                        rno = dr["rno"].ToString(),
                        branch = dr["branch"].ToString(),
                        paydate = dr["paydate"].ToString(),
                        //paydate = Convert.ToDateTime(dr["paydate"].ToString()).ToString("yyyy-MM-dd", new System.Globalization.CultureInfo("en-GB")),
                        //keydate = Convert.ToDateTime(dr["keydate"].ToString()).ToString("yyyy-MM-dd", new System.Globalization.CultureInfo("en-GB")),
                        keydate = dr["keydate"].ToString(),
                        uname = dr["uname"].ToString(),
                        detail = dr["detail"].ToString(),
                        amount2 = dr["amount"].ToString(),
                        payid = dr["payid"].ToString(),
                        status2 = dr["status"].ToString(),
                        rcby = dr["rcby"].ToString(),
                        rcbydesc = dr["rcbydesc"].ToString(),
                        summary = summary.ToString()
                    };
                    model.BatchList.Add(depositModel);
                }
            }

            // Select AccountNO
            using (SqlConnection db = new SqlConnection(_CON_STR))
            {
                SqlCommand cm = new SqlCommand("[sp_SCLookup]", db);
                cm.Parameters.AddWithValue("@flag", "lbankaccount");
                cm.CommandType = CommandType.StoredProcedure;
                if (db.State == ConnectionState.Closed) db.Open();
                SqlDataReader dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    var BankAccount = new ComBankAccountModel()
                    {
                        AccountNo = dr["AccountNo"].ToString(),
                        AccountName = dr["accountname"].ToString(),
                    };
                    model.BankAccount.Add(BankAccount);
                }
            }

            // Select Branch
            using (SqlConnection db = new SqlConnection(_CON_STR))
            {
                SqlCommand cm = new SqlCommand("[sp_GetSCBranch]", db);
                cm.Parameters.AddWithValue("@flag", "GetAll");
                cm.CommandType = CommandType.StoredProcedure;
                if (db.State == ConnectionState.Closed) db.Open();
                SqlDataReader dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    var BranchList = new CurrentBranchModel()
                    {
                        branchid = dr["branchid"].ToString(),
                        branchname = dr["branchname"].ToString(),
                    };
                    model.BranchList.Add(BranchList);
                }
            }
            return model;
        }
        #endregion

        #region ListDepositByBatchno
        public ListBatchModel getListDepositByBatchno(string batchno)
        {
            var model = new ListBatchModel();
            model.BatchList = new List<ListBatchModel>();
            model.BankAccount = new List<ComBankAccountModel>();
            model.BranchList = new List<CurrentBranchModel>();

            //Select Deposit
            using (SqlConnection db = new SqlConnection(_CON_STR))
            {
                SqlCommand cm = new SqlCommand("[sp_SCDeposit]", db);
                cm.Parameters.AddWithValue("@flag", "ListDepositByBatchno");
                cm.Parameters.AddWithValue("@batchno", batchno);
                cm.CommandType = CommandType.StoredProcedure;
                if (db.State == ConnectionState.Closed) db.Open();
                SqlDataReader dr = cm.ExecuteReader();
                decimal summary = 0;
                while (dr.Read())
                {
                    //if (dr["rcby"].ToString() == "cash" || dr["rcby"].ToString() == "dbnote" || dr["rcby"].ToString() == "cheque")
                    //{
                    //    summary = summary + decimal.Parse(dr["amount"].ToString());
                    //}

                    var depositModel = new ListBatchModel()
                    {
                        rno = dr["rno"].ToString(),
                        branch = dr["branch"].ToString(),
                        paydate = dr["paydate"].ToString(),
                        //paydate = Convert.ToDateTime(dr["paydate"].ToString()).ToString("yyyy-MM-dd", new System.Globalization.CultureInfo("en-GB")),
                        //keydate = Convert.ToDateTime(dr["keydate"].ToString()).ToString("yyyy-MM-dd", new System.Globalization.CultureInfo("en-GB")),
                        keydate = dr["keydate"].ToString(),
                        uname = dr["uname"].ToString(),
                        detail = dr["detail"].ToString(),
                        amount2 = dr["amount"].ToString(),
                        //payid = dr["payid"].ToString(),
                        status2 = dr["status"].ToString(),
                        //rcby = dr["rcby"].ToString(),
                        //rcbydesc = dr["rcbydesc"].ToString(),
                        //summary = summary.ToString(),
                    };
                    model.BatchList.Add(depositModel);
                }
            }

            // Select AccountNO
            using (SqlConnection db = new SqlConnection(_CON_STR))
            {
                SqlCommand cm = new SqlCommand("[sp_SCLookup]", db);
                cm.Parameters.AddWithValue("@flag", "lbankaccount");
                cm.CommandType = CommandType.StoredProcedure;
                if (db.State == ConnectionState.Closed) db.Open();
                SqlDataReader dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    var BankAccount = new ComBankAccountModel()
                    {
                        AccountNo = dr["AccountNo"].ToString(),
                        AccountName = dr["accountname"].ToString(),
                    };
                    model.BankAccount.Add(BankAccount);
                }
            }

            // Select Branch
            using (SqlConnection db = new SqlConnection(_CON_STR))
            {
                SqlCommand cm = new SqlCommand("[sp_GetSCBranch]", db);
                cm.Parameters.AddWithValue("@flag", "GetAll");
                cm.CommandType = CommandType.StoredProcedure;
                if (db.State == ConnectionState.Closed) db.Open();
                SqlDataReader dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    var BranchList = new CurrentBranchModel()
                    {
                        branchid = dr["branchid"].ToString(),
                        branchname = dr["branchname"].ToString(),
                    };
                    model.BranchList.Add(BranchList);
                }
            }

            return model;
        }
        #endregion

        #region "Edit Deposit"
        public ListBatchModel editDeposit(ListBatchModel modelData, string username)
        {
            var model = new ListBatchModel();
            using (SqlConnection db = new SqlConnection(_CON_STR))
            {
                if (db.State == ConnectionState.Closed) db.Open();
                SqlCommand cm = new SqlCommand("[sp_SCDeposit]", db);
                cm.CommandType = CommandType.StoredProcedure;
                cm.Parameters.AddWithValue("@flag", "EditDeposit");
                cm.Parameters.AddWithValue("@batchno", modelData.Batchno);
                cm.Parameters.AddWithValue("@depositdate", modelData.depositdate);
                cm.Parameters.AddWithValue("@accountno", modelData.accountno);
                cm.Parameters.AddWithValue("@remark", modelData.remark);
                cm.Parameters.AddWithValue("@user", username);
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

        #region Add Deposit
        public ListBatchModel addDeposit(ListBatchModel modelData, string username, string branchid)
        {
            ListBatchModel model = new ListBatchModel();
            using (SqlConnection db = new SqlConnection(_CON_STR))
            {
                SqlCommand cm = new SqlCommand("[sp_SCDeposit]", db);
                cm.CommandType = CommandType.StoredProcedure;
                cm.Parameters.AddWithValue("@flag", "NewDeposit");
                if (modelData.payid != "-")
                {
                    cm.Parameters.AddWithValue("@setpayid", modelData.payid);
                    cm.Parameters.AddWithValue("@depositdate", modelData.depositdate);
                    cm.Parameters.AddWithValue("@branchid", branchid);
                    cm.Parameters.AddWithValue("@item", modelData.item);
                    cm.Parameters.AddWithValue("@amount", modelData.summary);
                    cm.Parameters.AddWithValue("@accountno", modelData.accountno);
                    cm.Parameters.AddWithValue("@remark", modelData.remark);
                    cm.Parameters.AddWithValue("@user", username);
                    cm.Parameters.AddWithValue("@ip", HttpContext.Current.Request.UserHostAddress);
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
                else
                {
                    model.Result = "N";
                }
            }
            return model;
        }
        #endregion
    }
}
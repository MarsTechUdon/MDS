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
    public class CurrentBranchManagement
    {
        public static string _CON_STR = ConfigurationManager.ConnectionStrings["MDS"].ConnectionString;
        #region "ดึงข้อมูล CurrentBranch"
        public static List<CurrentBranchModel> GetCurrentBranch()
        {
            var list = new List<CurrentBranchModel>();
            using (SqlConnection db = new SqlConnection(_CON_STR))
            {
                SqlCommand cm = new SqlCommand("sp_SCGetCurrentBranch", db);
                cm.Parameters.AddWithValue("@flag", "listbranch");
                cm.CommandType = CommandType.StoredProcedure;
                if (db.State == ConnectionState.Closed) db.Open();
                SqlDataReader dr = cm.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        var model = new CurrentBranchModel();
                        model.branchid = dr["branchid"].ToString();
                        model.branchname = dr["branchname"].ToString();
                        list.Add(model);
                    }
                    dr.Close();
                }
            }
            return list;
        }
        #endregion
        #region "encodeCurrentBranch"
        public static string encode(string branchid,string branchname)
        {
            string branchtxt = "";
            using (SqlConnection db = new SqlConnection(_CON_STR))
            {
                SqlCommand cm = new SqlCommand("sp_SCGetCurrentBranch", db);
                cm.Parameters.AddWithValue("@flag", "encode");
                cm.Parameters.AddWithValue("@branchid", branchid);
                cm.Parameters.AddWithValue("@branchname", branchname);
                cm.CommandType = CommandType.StoredProcedure;
                if (db.State == ConnectionState.Closed) db.Open();
                SqlDataReader dr = cm.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        branchtxt = dr["branchtxt"].ToString();
                    }
                    dr.Close();
                }
            }
            return branchtxt;
        }
        #endregion
        #region "decodeCurrentBranch"
        public static CurrentBranchModel decode(string branchtxt)
        {
            var model = new CurrentBranchModel();
            using (SqlConnection db = new SqlConnection(_CON_STR))
            {
                SqlCommand cm = new SqlCommand("sp_SCGetCurrentBranch", db);
                cm.Parameters.AddWithValue("@flag", "decode");
                cm.Parameters.AddWithValue("@branchtxt", branchtxt);
                cm.CommandType = CommandType.StoredProcedure;
                if (db.State == ConnectionState.Closed) db.Open();
                SqlDataReader dr = cm.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                     
                        model.branchid = dr["branchid"].ToString();
                        model.branchname = dr["branchname"].ToString();
                    }
                    dr.Close();
                }
            }
            return model;
        }
        #endregion
    }
}
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
    public class ReportManagement
    {
        public static string _CON_STR = ConfigurationManager.ConnectionStrings["MDS"].ConnectionString;

        #region "Get Current Date"
        public static DepositACModel GetCurrentDate()
        {
            DepositACModel CurrentDate = new DepositACModel();
            using (SqlConnection db = new SqlConnection(_CON_STR))
            {
                SqlCommand cm = new SqlCommand("[sp_SCLookup]", db);
                cm.CommandType = CommandType.StoredProcedure;
                cm.Parameters.AddWithValue("@flag", "lcurrentperiod");

                if (db.State == ConnectionState.Closed) db.Open();
                SqlDataReader dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    CurrentDate.fdate = dr["fdate"].ToString();
                    CurrentDate.tdate = dr["tdate"].ToString();
                }
            }
            return CurrentDate;
        }
        #endregion
        #region "TestTakers"
        public List<TestTakersModel> GetDocNumberBooking(TestTakersModel ugmData)
        {
            var list = new List<TestTakersModel>();
            using (SqlConnection db = new SqlConnection(_CON_STR))
            {
                SqlCommand cm = new SqlCommand("sp_reportExamList", db);
                cm.Parameters.AddWithValue("@fdate", ugmData.fdate);
                cm.Parameters.AddWithValue("@tdate", ugmData.tdate);
                cm.CommandType = CommandType.StoredProcedure;
                if (db.State == ConnectionState.Closed) db.Open();
                SqlDataReader dr = cm.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        var model = new TestTakersModel();
                        model.courseid = dr["courseid"].ToString();
                        model.examdate = dr["examdate"].ToString();
                        model.coursegroupname = dr["coursegroupname"].ToString();
                        model.coursenickname = dr["coursenickname"].ToString();
                        model.coursegroupid = dr["coursegroupid"].ToString();
                        model.studentcount = dr["studentcount"].ToString();
                        list.Add(model);
                    }
                    dr.Close();
                }
            }
            return list;
        }
        #endregion
    }
}
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
    public class OverviewManagement
    {
        public static string _CON_STR = ConfigurationManager.ConnectionStrings["MDS"].ConnectionString;
        #region "ดึงข้อมูล Main"
        public MainModel Getoverview(String currentdate)
        {
            var list = new MainModel();
            list.Main = new MainDetailModel();
            list.Branch = new List<MainDetailModel>();
            using (SqlConnection db = new SqlConnection(_CON_STR))
            {
                SqlCommand cm = new SqlCommand("sp_SCgetoverview", db);
                cm.Parameters.AddWithValue("@currentdate", currentdate);
                cm.Parameters.AddWithValue("@flag", "main");
                cm.CommandType = CommandType.StoredProcedure;
                if (db.State == ConnectionState.Closed) db.Open();
                SqlDataReader dr = cm.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        list.Main.btw = dr["btw"].ToString();
                        list.Main.debt = dr["debt"].ToString();
                        list.Main.remainreceipt = dr["remainreceipt"].ToString();
                        list.Main.total = string.Format("{0:n}", Convert.ToDecimal(dr["total"].ToString()));
                        list.Main.wait = string.Format("{0:n}", Convert.ToDecimal(dr["wait"].ToString()));
                        list.Main.deposit = string.Format("{0:n}", Convert.ToDecimal(dr["deposit"].ToString()));
                        list.Main.trancar = string.Format("{0:n}", Convert.ToDecimal(dr["trancar"].ToString()));
                        list.Main.trancarcount = dr["trancarcount"].ToString();
                        list.Main.trancardesc = dr["trancardesc"].ToString();
                        list.Main.car = string.Format("{0:n}", Convert.ToDecimal(dr["car"].ToString()));
                        list.Main.carcount = dr["carcount"].ToString();
                        list.Main.cardesc = dr["cardesc"].ToString();
                        list.Main.motocycle = string.Format("{0:n}", Convert.ToDecimal(dr["motocycle"].ToString()));
                        list.Main.motocyclecount= dr["motocyclecount"].ToString();
                        list.Main.motocycledesc = dr["motocycledesc"].ToString();
                        list.Main.license = string.Format("{0:n}", Convert.ToDecimal(dr["license"].ToString()));
                        list.Main.licensecount = dr["licensecount"].ToString();
                        list.Main.licensedesc = dr["licensedesc"].ToString();
                        list.Main.other = string.Format("{0:n}", Convert.ToDecimal(dr["other"].ToString()));
                        list.Main.othercount = dr["othercount"].ToString();
                        list.Main.otherdesc = dr["otherdesc"].ToString();
                    }
                    dr.NextResult();
                    while (dr.Read())
                    {
                        var model = new MainDetailModel();
                        model.branch = dr["branchname"].ToString();
                        model.total = string.Format("{0:n}", Convert.ToDecimal(dr["total"].ToString()));
                        model.wait = string.Format("{0:n}", Convert.ToDecimal(dr["wait"].ToString()));
                        model.deposit = string.Format("{0:n}", Convert.ToDecimal(dr["deposit"].ToString()));
                    
                        list.Branch.Add(model);
                    }
                    dr.Close();
                }
            }
            return list;
        }
        #endregion
        #region "ดึงข้อมูล GetoverviewDetail1"
        public List<OverviewDetailModel> GetoverviewDetail1(string currentdate,string ovid)
        {
            var list = new List<OverviewDetailModel>();
            using (SqlConnection db = new SqlConnection(_CON_STR))
            {
                SqlCommand cm = new SqlCommand("sp_SCgetoverview", db);
                cm.Parameters.AddWithValue("@currentdate", currentdate);
                cm.Parameters.AddWithValue("@flag", ovid);
                cm.CommandType = CommandType.StoredProcedure;
                if (db.State == ConnectionState.Closed) db.Open();
                SqlDataReader dr = cm.ExecuteReader();
                if (dr.HasRows)
                {                    

                    while (dr.Read())
                    {
                        var model = new OverviewDetailModel();
                        model.rno = dr["rno"].ToString();
                        model.courseicon = dr["courseicon"].ToString();
                        model.courseiconcolor = dr["courseiconcolor"].ToString();
                        model.coursegroupname = dr["coursegroupname"].ToString();
                        model.coursenickname = dr["coursenickname"].ToString();
                        model.coursename = dr["coursename"].ToString();
                        model.regisdate = dr["regisdate"].ToString();
                        model.studentname = dr["studentname"].ToString();
                        model.idcard = dr["idcard"].ToString();
                        model.mobileno = dr["mobileno"].ToString();
                        model.courseprice = string.Format("{0:n}", Convert.ToDecimal(dr["courseprice"].ToString()));
                        model.payamount = string.Format("{0:n}", Convert.ToDecimal(dr["payamount"].ToString()));
                        model.remain = string.Format("{0:n}", Convert.ToDecimal(dr["remain"].ToString()));
                        model.ind = dr["ind"].ToString();
                        model.voucherid = dr["voucherid"].ToString();
                        model.studenttype = dr["studenttype"].ToString();
                        list.Add(model);
                    }
                    dr.Close();
                }
            }
            return list;
        }
        #endregion
        #region "ดึงข้อมูล GetoverviewDetail2"
        public List<OverviewDetailModel> GetoverviewDetail2(string currentdate, string ovid)
        {
            var list = new List<OverviewDetailModel>();
            using (SqlConnection db = new SqlConnection(_CON_STR))
            {
                SqlCommand cm = new SqlCommand("sp_SCgetoverview", db);
                cm.Parameters.AddWithValue("@currentdate", currentdate);
                cm.Parameters.AddWithValue("@flag", ovid);
                cm.CommandType = CommandType.StoredProcedure;
                if (db.State == ConnectionState.Closed) db.Open();
                SqlDataReader dr = cm.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        var model = new OverviewDetailModel();
                        model.rno = dr["rno"].ToString();
                        model.branchid = dr["branchid"].ToString();
                        model.branchname = dr["branchname"].ToString();
                        model.courseicon = dr["courseicon"].ToString();
                        model.courseiconcolor = dr["courseiconcolor"].ToString();
                        model.coursegroupname = dr["coursegroupname"].ToString();
                        model.coursenickname = dr["coursenickname"].ToString();
                        model.coursename = dr["coursename"].ToString();
                        model.regisdate = dr["regisdate"].ToString();
                        model.studentname = dr["studentname"].ToString();
                        model.idcard = dr["idcard"].ToString();
                        model.mobileno = dr["mobileno"].ToString();
                        model.courseprice = string.Format("{0:n}", Convert.ToDecimal(dr["courseprice"].ToString()));
                        model.payamount = string.Format("{0:n}", Convert.ToDecimal(dr["payamount"].ToString()));
                        model.remain = string.Format("{0:n}", Convert.ToDecimal(dr["remain"].ToString()));
                        model.ind = dr["ind"].ToString();
                        model.voucherid = dr["voucherid"].ToString();
                        model.studenttype = dr["studenttype"].ToString();
                        list.Add(model);
                    }
                    dr.Close();
                }
            }
            return list;
        }
        #endregion
        #region "ดึงข้อมูล GetoverviewDetail3"
        public List<OverviewDetailModel> GetoverviewDetail3(string currentdate, string ovid)
        {
            var list = new List<OverviewDetailModel>();
            using (SqlConnection db = new SqlConnection(_CON_STR))
            {
                SqlCommand cm = new SqlCommand("sp_SCgetoverview", db);
                cm.Parameters.AddWithValue("@currentdate", currentdate);
                cm.Parameters.AddWithValue("@flag", ovid);
                cm.CommandType = CommandType.StoredProcedure;
                if (db.State == ConnectionState.Closed) db.Open();
                SqlDataReader dr = cm.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        var model = new OverviewDetailModel();
                        model.rno = dr["rno"].ToString();
                        model.courseicon = dr["courseicon"].ToString();
                        model.courseiconcolor = dr["courseiconcolor"].ToString();
                        model.coursegroupname = dr["coursegroupname"].ToString();
                        model.coursenickname = dr["coursenickname"].ToString();
                        model.coursename = dr["coursename"].ToString();
                        model.regisdate = dr["regisdate"].ToString();
                        model.studentname = dr["studentname"].ToString();
                        model.idcard = dr["idcard"].ToString();
                        model.mobileno = dr["mobileno"].ToString();
                        model.courseprice = string.Format("{0:n}", Convert.ToDecimal(dr["courseprice"].ToString()));
                        model.payamount = string.Format("{0:n}", Convert.ToDecimal(dr["payamount"].ToString()));
                        model.remain = string.Format("{0:n}", Convert.ToDecimal(dr["remain"].ToString()));
                        model.ind = dr["ind"].ToString();
                        model.voucherid = dr["voucherid"].ToString();
                        model.studenttype = dr["studenttype"].ToString();
                        list.Add(model);
                    }
                    dr.Close();
                }
            }
            return list;
        }
        #endregion
        #region "ดึงข้อมูล GetMediafile"
        public static List<MediaFileModel> GetMediafile()
        {
            var list = new List<MediaFileModel>();
            using (SqlConnection db = new SqlConnection(_CON_STR))
            {
                SqlCommand cm = new SqlCommand("sp_GetMediaFile", db);
                cm.Parameters.AddWithValue("@flag", "GetAll");
                cm.CommandType = CommandType.StoredProcedure;
                if (db.State == ConnectionState.Closed) db.Open();
                SqlDataReader dr = cm.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        var model = new MediaFileModel();
                        model.FDesc = dr["FDesc"].ToString();
                        model.FName = dr["FName"].ToString();
                        model.FLink = dr["FLink"].ToString();
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
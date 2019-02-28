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
    public class SchoolManagement
    {
        public static string _CON_STR = ConfigurationManager.ConnectionStrings["MDS"].ConnectionString;
        #region "ดึงข้อมูล GetCompany"
        public CompanyModel GetCompany()
        {
            var model = new CompanyModel();
            using (SqlConnection db = new SqlConnection(_CON_STR))
            {
                SqlCommand cm = new SqlCommand("sp_SetCompany", db);
                cm.Parameters.AddWithValue("@flag", "getCompanyInfoById");
                cm.Parameters.AddWithValue("@ind", "1");
                cm.CommandType = CommandType.StoredProcedure;
                if (db.State == ConnectionState.Closed) db.Open();
                SqlDataReader dr = cm.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        var emailpass= "";
                        if (dr["sendemailpass"].ToString() != "") {
                            emailpass = ENDEtxtManagement.Decrypt(dr["sendemailpass"].ToString());
                        }
                        model.ind = dr["ind"].ToString();
                        model.SchoolLogo = dr["logopath"].ToString()+dr["SchoolLogo"].ToString();
                        model.SchoolName = dr["SchoolName"].ToString();
                        model.schoolAddr1 = dr["schoolAddr1"].ToString();
                        model.schoolAddr2 = dr["schoolAddr2"].ToString();
                        model.schoolAddr3 = dr["schoolAddr3"].ToString();
                        model.CompanyName = dr["CompanyName"].ToString();
                        model.CompanyLogo = dr["logopath"].ToString()+dr["CompanyLogo"].ToString();
                        model.CompanyAddr1 = dr["CompanyAddr1"].ToString();
                        model.CompanyAddr2 = dr["CompanyAddr2"].ToString();
                        model.CompanyAddr3 = dr["CompanyAddr3"].ToString();
                        model.SchoolNameE = dr["SchoolNameE"].ToString();
                        model.schoolAddrE1 = dr["schoolAddrE1"].ToString();
                        model.schoolAddrE2 = dr["schoolAddrE2"].ToString();
                        model.schoolAddrE3 = dr["schoolAddrE3"].ToString();
                        model.CompanyNameE = dr["CompanyNameE"].ToString();
                        model.CompanyAddrE1 = dr["CompanyAddrE1"].ToString();
                        model.CompanyAddrE2 = dr["CompanyAddrE2"].ToString();
                        model.CompanyAddrE3 = dr["CompanyAddrE3"].ToString();
                        model.schoolfavicon = dr["logopath"].ToString()+dr["schoolfavicon"].ToString();
                        model.sendemailuser = dr["sendemailuser"].ToString();
                        model.sendemailpass = emailpass;
                        model.ParaMClosePerUser = dr["ParaMClosePerUser"].ToString();
                        model.Lastupdate = dr["Lastupdate"].ToString();
                        model.IP = dr["IP"].ToString();
                        model.updateby = dr["updateby"].ToString();
                        model.CompanyLogoName = dr["CompanyLogo"].ToString();
                        model.SchoolLogoName = dr["SchoolLogo"].ToString();
                        model.faviconName = dr["schoolfavicon"].ToString();
                    }

                    dr.Close();
                }
            }
            return model;
        }
        #endregion
        #region "ดึงข้อมูล GetCompanyView"
        public CompanyModel GetCompanyView()
        {
            var model = new CompanyModel();
            using (SqlConnection db = new SqlConnection(_CON_STR))
            {
                SqlCommand cm = new SqlCommand("sp_SetCompany", db);
                cm.Parameters.AddWithValue("@flag", "getCompanyInfoById");
                cm.Parameters.AddWithValue("@ind", "1");
                cm.CommandType = CommandType.StoredProcedure;
                if (db.State == ConnectionState.Closed) db.Open();
                SqlDataReader dr = cm.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        model.SchoolName = dr["SchoolName"].ToString();
                        model.SchoolNameE = dr["SchoolNameE"].ToString();
                        model.schoolAddr1 = dr["schoolAddr1"].ToString();
                        model.schoolAddr2 = dr["schoolAddr2"].ToString();
                        model.schoolAddr3 = dr["schoolAddr3"].ToString();
                        model.CompanyLogo = dr["logopath"].ToString() + dr["CompanyLogo"].ToString();
                        model.SchoolLogo = dr["logopath"].ToString() + dr["SchoolLogo"].ToString();
                        model.schoolfavicon = dr["logopath"].ToString() + dr["schoolfavicon"].ToString();
                        model.logopath = dr["logopath"].ToString();
                    }

                    dr.Close();
                }
            }
            return model;
        }
        #endregion
        #region "EditCompany"
        public ResultBookingModel EditCompany(CompanyModel value)
        {
            var model = new ResultBookingModel();
            var emailpass = "";
            if (value.sendemailpass != "")
            {
                emailpass = ENDEtxtManagement.Encrypt(value.sendemailpass);
            }
            using (SqlConnection db = new SqlConnection(_CON_STR))
            {
                SqlCommand cm = new SqlCommand("sp_SetCompany", db);
                cm.Parameters.AddWithValue("@flag", "Edit");
                cm.Parameters.AddWithValue("@ind", value.ind);
                cm.Parameters.AddWithValue("@SchoolLogo", value.SchoolLogo);
                cm.Parameters.AddWithValue("@SchoolName", value.SchoolName);
                cm.Parameters.AddWithValue("@schoolAddr1", value.schoolAddr1);
                cm.Parameters.AddWithValue("@schoolAddr2", value.schoolAddr2);
                cm.Parameters.AddWithValue("@schoolAddr3", value.schoolAddr3);
                cm.Parameters.AddWithValue("@CompanyName", value.CompanyName);
                cm.Parameters.AddWithValue("@CompanyLogo", value.CompanyLogo);
                cm.Parameters.AddWithValue("@CompanyAddr1", value.CompanyAddr1);
                cm.Parameters.AddWithValue("@CompanyAddr2", value.CompanyAddr2);
                cm.Parameters.AddWithValue("@CompanyAddr3", value.CompanyAddr3);
                cm.Parameters.AddWithValue("@schoolnameE", value.SchoolNameE);
                cm.Parameters.AddWithValue("@schooladdrE1", value.schoolAddrE1);
                cm.Parameters.AddWithValue("@schooladdrE2", value.schoolAddrE2);
                cm.Parameters.AddWithValue("@schooladdrE3", value.schoolAddrE3);
                cm.Parameters.AddWithValue("@companynameE", value.CompanyNameE);
                cm.Parameters.AddWithValue("@companyaddrE1", value.CompanyAddrE1);
                cm.Parameters.AddWithValue("@companyaddrE2", value.CompanyAddrE2);
                cm.Parameters.AddWithValue("@companyaddrE3", value.CompanyAddrE3);
                cm.Parameters.AddWithValue("@schoolfavicon", value.schoolfavicon);
                cm.Parameters.AddWithValue("@sendemailuser", value.sendemailuser);
                cm.Parameters.AddWithValue("@sendemailpass", emailpass);
                cm.Parameters.AddWithValue("@ip", HttpContext.Current.Request.UserHostAddress);
                cm.Parameters.AddWithValue("@user", value.updateby);
                cm.CommandType = CommandType.StoredProcedure;
                if (db.State == ConnectionState.Closed) db.Open();
                SqlDataReader dr = cm.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        model.ind = dr["ind"].ToString();
                        model.msg = dr["msg"].ToString();
                        model.result = dr["result"].ToString();

                    }

                    dr.Close();
                }
            }
            return model;
        }
        #endregion
        #region "ดึงข้อมูล GetListConfig"
        public List<ConfigModel> GetListConfig()
        {
            var list = new List<ConfigModel>();
            using (SqlConnection db = new SqlConnection(_CON_STR))
            {
                SqlCommand cm = new SqlCommand("sp_SetCompany", db);
                cm.Parameters.AddWithValue("@flag", "ListConfig");
                cm.Parameters.AddWithValue("@ind", "1");
                cm.CommandType = CommandType.StoredProcedure;
                if (db.State == ConnectionState.Closed) db.Open();
                SqlDataReader dr = cm.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        var model = new ConfigModel();
                        model.comInd = dr["comInd"].ToString();
                        model.fieldType = dr["fieldType"].ToString();
                        model.paraDesc = dr["paraDesc"].ToString();
                        model.paraid = dr["paraid"].ToString();
                        model.paraName = dr["paraName"].ToString();
                        model.paraUnit = dr["paraUnit"].ToString();
                        model.paraValue = dr["paraValue"].ToString();
                        list.Add(model);
                    }

                    dr.Close();
                }
            }
            return list;
        }
        #endregion
        #region "SetConfig"
        public ResultBookingModel SetConfig(string paraid,string paravalue)
        {
            var model = new ResultBookingModel();
            using (SqlConnection db = new SqlConnection(_CON_STR))
            {
                SqlCommand cm = new SqlCommand("sp_SetCompany", db);
                cm.Parameters.AddWithValue("@flag", "SetConfig");
                cm.Parameters.AddWithValue("@ind", "1");
                cm.Parameters.AddWithValue("@paraid", paraid);
                cm.Parameters.AddWithValue("@paravalue", paravalue);
                cm.CommandType = CommandType.StoredProcedure;
                if (db.State == ConnectionState.Closed) db.Open();
                SqlDataReader dr = cm.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        model.ind = dr["ind"].ToString();
                        model.paraid = dr["paraid"].ToString();
                        model.msg = dr["msg"].ToString();
                        model.result = dr["result"].ToString();

                    }

                    dr.Close();
                }
            }
            return model;
        }
        #endregion
        #region "getParamInfoEditDay"
        public static string getParamInfoEditDay()
        {
            string result = "";
            using (SqlConnection db = new SqlConnection(_CON_STR))
            {
                SqlCommand cm = new SqlCommand("sp_SetCompany", db);
                cm.Parameters.AddWithValue("@flag", "getParamInfoEditDay");
                cm.Parameters.AddWithValue("@ind", "4");
                cm.CommandType = CommandType.StoredProcedure;
                if (db.State == ConnectionState.Closed) db.Open();
                SqlDataReader dr = cm.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        result = dr["ParaMEditStDay"].ToString();
                    }

                    dr.Close();
                }
            }
            return result;
        }
        #endregion
        #region "getLogoPath"
        public  string getLogoPath()
        {
            string result = "";
            using (SqlConnection db = new SqlConnection(_CON_STR))
            {
                SqlCommand cm = new SqlCommand("sp_SetCompany", db);
                cm.Parameters.AddWithValue("@flag", "getLogoPath");
                cm.CommandType = CommandType.StoredProcedure;
                if (db.State == ConnectionState.Closed) db.Open();
                SqlDataReader dr = cm.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        result = dr["logopath"].ToString();
                    }

                    dr.Close();
                }
            }
            return result;
        }
        #endregion
    }
}
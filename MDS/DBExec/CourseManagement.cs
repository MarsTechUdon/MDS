using System;
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
    public class CourseManagement
    {
        public static string _CON_STR = ConfigurationManager.ConnectionStrings["MDS"].ConnectionString;

        public CourseModel SelectCourse()
        {
            var model = new CourseModel();
            model.CourseGroupDataList = new List<CourseGroupModel>();
            model.CourseDataList = new List<CourseModel>();
            using (SqlConnection db = new SqlConnection(_CON_STR))
            {

                SqlCommand cm = new SqlCommand("sp_GetSCCourse", db);
                cm.Parameters.AddWithValue("@flag", "GetAll");
                cm.CommandType = CommandType.StoredProcedure;
                if (db.State == ConnectionState.Closed) db.Open();
                SqlDataReader dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    var courseModel = new CourseModel()
                    {
                        Cid = dr["courseid"].ToString(),
                        CgID = dr["coursegroupid"].ToString(),
                        CName = dr["coursename"].ToString(),
                        CPrice = dr["courseprice"].ToString(),
                        Status = dr["status"].ToString(),

                        CGroupName = dr["coursegroupname"].ToString(),
                        
                    };
                    model.CourseDataList.Add(courseModel);
                }
                dr.NextResult();
                while (dr.Read())
                {
                    var courseGroupModel = new CourseGroupModel()
                    {
                        CgID = dr["coursegroupid"].ToString(),
                        CgName = dr["coursegroupname"].ToString(),
                    };
                    model.CourseGroupDataList.Add(courseGroupModel);
                }

            }
            return model;
        }
        public CourseModel AddCourse2(CourseModel ugmData)
        {
            var model = new CourseModel();
            using (SqlConnection db = new SqlConnection(_CON_STR))
            {
                SqlCommand cm = new SqlCommand("[sp_SetSCCourse]", db);

                cm.Parameters.AddWithValue("@flag", "Add");
                cm.Parameters.AddWithValue("@CName", ugmData.CName);
                cm.Parameters.AddWithValue("@CPrice", ugmData.CPrice);
                cm.Parameters.AddWithValue("@CgID", ugmData.CgID);

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
        public CourseModel EditCourse2(CourseModel ugmData)
        {
            var model = new CourseModel();
            using (SqlConnection db = new SqlConnection(_CON_STR))
            {
                SqlCommand cm = new SqlCommand("[sp_SetSCCourse]", db);

                cm.Parameters.AddWithValue("@flag", "Edit");
                cm.Parameters.AddWithValue("@Cid", ugmData.Cid);
                cm.Parameters.AddWithValue("@CName", ugmData.CName);
                cm.Parameters.AddWithValue("@CPrice", ugmData.CPrice);
                cm.Parameters.AddWithValue("@CgID", ugmData.CgID);

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
        public CourseModel CancelCourse2(CourseModel ugmData)
        {
            var model = new CourseModel();
            using (SqlConnection db = new SqlConnection(_CON_STR))
            {
                SqlCommand cm = new SqlCommand("[sp_SetSCCourse]", db);

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
        public CourseModel ReCancelCourse2(CourseModel ugmData)
        {
            var model = new CourseModel();
            using (SqlConnection db = new SqlConnection(_CON_STR))
            {
                SqlCommand cm = new SqlCommand("[sp_SetSCCourse]", db);

                cm.Parameters.AddWithValue("@flag", "RE-Cancel");
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
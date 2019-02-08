using MDS.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;

namespace MDS.DBExec
{
    public class SubjectManagement
    {
        public static string _CON_STR = ConfigurationManager.ConnectionStrings["MDS"].ConnectionString;

        public SubjectModel SelectSubject()
        {
            var model = new SubjectModel();
            model.CourseDataList = new List<CourseModel>();
            model.SubjectDataList = new List<SubjectModel>();
            using (SqlConnection db = new SqlConnection(_CON_STR))
            {

                SqlCommand cm = new SqlCommand("sp_GetSCSubject", db);
                cm.Parameters.AddWithValue("@flag", "GetAll");
                cm.CommandType = CommandType.StoredProcedure;
                if (db.State == ConnectionState.Closed) db.Open();
                SqlDataReader dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    var subjectModel = new SubjectModel()
                    {
                        Sid = dr["subjectid"].ToString(),
                        SName = dr["subjectname"].ToString(),
                        SnickName = dr["subjectnickname"].ToString(),
                        Cid = dr["courseid"].ToString(),
                        Stype = dr["subjecttype"].ToString(),
                        smin = dr["subjectmin"].ToString(),
                        status = dr["status"].ToString(),

                        CourseName = dr["coursename"].ToString(),

                    };
                    model.SubjectDataList.Add(subjectModel);
                }
                dr.NextResult();
                while (dr.Read())
                {
                    var courseModel = new CourseModel()
                    {
                        Cid = dr["courseid"].ToString(),
                        CName = dr["coursename"].ToString(),
                    };
                    model.CourseDataList.Add(courseModel);
                }
            }
            return model;
        }


        #region Add Subject
        public SubjectModel addSubject(SubjectModel modelData)
        {
            SubjectModel model = new SubjectModel();
            using (SqlConnection db = new SqlConnection(_CON_STR))
            {
                SqlCommand cm = new SqlCommand("[sp_SetSCSubject]", db);
                cm.CommandType = CommandType.StoredProcedure;
                cm.Parameters.AddWithValue("@flag", "Add");
                cm.Parameters.AddWithValue("@SName", modelData.SName);
                cm.Parameters.AddWithValue("@SnickName", modelData.SnickName);
                cm.Parameters.AddWithValue("@Cid", modelData.Cid);
                cm.Parameters.AddWithValue("@Stype", modelData.Stype);
                cm.Parameters.AddWithValue("@smin", modelData.smin);
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

        #region "Edit Subject"
        public SubjectModel editSubject(SubjectModel modelData)
        {
            var model = new SubjectModel();
            using (SqlConnection db = new SqlConnection(_CON_STR))
            {
                if (db.State == ConnectionState.Closed) db.Open();
                SqlCommand cm = new SqlCommand("[sp_SetSCSubject]", db);
                cm.CommandType = CommandType.StoredProcedure;
                cm.Parameters.AddWithValue("@flag", "Edit");
                cm.Parameters.AddWithValue("@Sid", modelData.Sid);
                cm.Parameters.AddWithValue("@SName", modelData.SName);
                cm.Parameters.AddWithValue("@SnickName", modelData.SnickName);
                cm.Parameters.AddWithValue("@Cid", modelData.Cid);
                cm.Parameters.AddWithValue("@Stype", modelData.Stype);
                cm.Parameters.AddWithValue("@smin", modelData.smin);
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
        
        #region "Delete Subject"
        public ResultModel DeleteSubject(string id)
        {
            var model = new ResultModel();
            using (SqlConnection db = new SqlConnection(_CON_STR))
            {
                if (db.State == ConnectionState.Closed) db.Open();
                SqlCommand cm = new SqlCommand("[sp_SetSCSubject]", db);
                cm.CommandType = CommandType.StoredProcedure;
                cm.Parameters.AddWithValue("@flag", "Cancel");
                cm.Parameters.AddWithValue("@Sid", id);
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

        #region "Re-DeleteSubject"
        public ResultModel ReDelete(string id)
        {
            var model = new ResultModel();
            using (SqlConnection db = new SqlConnection(_CON_STR))
            {
                if (db.State == ConnectionState.Closed) db.Open();
                SqlCommand cm = new SqlCommand("[sp_SetSCSubject]", db);
                cm.CommandType = CommandType.StoredProcedure;
                cm.Parameters.AddWithValue("@flag", "Re-Cancel");
                cm.Parameters.AddWithValue("@Sid", id);
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
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
    public class CourseGroupManagement
    {
        public static string _CON_STR = ConfigurationManager.ConnectionStrings["MDS"].ConnectionString;

        #region GetAllCourseGroup
        public List<CourseGroupModel> GetAllCourseGroup()
        {
            List<CourseGroupModel> model = new List<CourseGroupModel>();
            using (SqlConnection db = new SqlConnection(_CON_STR))
            {
                SqlCommand cm = new SqlCommand("[sp_GetSCCourseGroup]", db);
                cm.CommandType = CommandType.StoredProcedure;
                cm.Parameters.AddWithValue("@flag", "GetAll");

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cm;
                DataTable dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow row in dt.Rows)
                {
                    CourseGroupModel x = new CourseGroupModel();
                    x.CgID = row["coursegroupid"].ToString();
                    x.CgName = row["coursegroupname"].ToString();
                    model.Add(x);
                }
            }
            return model;
        }
        #endregion

        #region Add CourseGroup
        public CourseGroupModel addCourseGroup(CourseGroupModel modelData)
        {
            CourseGroupModel model = new CourseGroupModel();
            using (SqlConnection db = new SqlConnection(_CON_STR))
            {
                SqlCommand cm = new SqlCommand("[sp_SetSCCourseGroup]", db);
                cm.CommandType = CommandType.StoredProcedure;
                cm.Parameters.AddWithValue("@flag", "Add");
                cm.Parameters.AddWithValue("@CgName", modelData.CgName);
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

        #region "Edit CourseGroup"
        public CourseGroupModel EditCourseGroup(CourseGroupModel modelData)
        {
            CourseGroupModel model = new CourseGroupModel();
            using (SqlConnection db = new SqlConnection(_CON_STR))
            {
                if (db.State == ConnectionState.Closed) db.Open();
                SqlCommand cm = new SqlCommand("[sp_SetSCCourseGroup]", db);
                cm.CommandType = CommandType.StoredProcedure;
                cm.Parameters.AddWithValue("@flag", "Edit");
                cm.Parameters.AddWithValue("@CgID", modelData.CgID);
                cm.Parameters.AddWithValue("@CgName", modelData.CgName);
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
    }
}
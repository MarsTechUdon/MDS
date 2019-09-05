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
    public class TeacherManagement
    {
        public static string _CON_STR = ConfigurationManager.ConnectionStrings["MDS"].ConnectionString;

        #region รายการครู
        public List<TeacherModel> GetAllTeacher()
        {
            List<TeacherModel> model = new List<TeacherModel>();
            using (SqlConnection db = new SqlConnection(_CON_STR))
            {
                SqlCommand cm = new SqlCommand("[sp_SetTeacher]", db);
                cm.CommandType = CommandType.StoredProcedure;
                cm.Parameters.AddWithValue("@flag", "ListTeacher");

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cm;
                DataTable dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow row in dt.Rows)
                {
                    TeacherModel x = new TeacherModel();
                    x.ind = row["ind"].ToString();
                    x.cardimg = row["cardimg"].ToString();
                    x.teachername = row["teachername"].ToString();
                    x.email = row["email"].ToString();
                    x.mobileno = row["mobileno"].ToString();
                    x.nickname = row["nickname"].ToString();
                    x.status = row["status"].ToString();
                    if (row["qrurl"].ToString() != (""))
                    {
                        x.Qrurl = row["qrurl"].ToString();
                    }
                    model.Add(x);
                }
            }
            return model;
        }
        #endregion

        #region เพิ่มครู
        public TeacherModel AddTeacher(TeacherModel modelData)
        {
            var model = new TeacherModel();
            using (SqlConnection db = new SqlConnection(_CON_STR))
            {
                SqlCommand cm = new SqlCommand("[sp_SetTeacher]", db);

                cm.Parameters.AddWithValue("@flag", "Add");

                cm.Parameters.AddWithValue("@IDCard", modelData.idcard);
                cm.Parameters.AddWithValue("@TitleT", modelData.titleT);
                cm.Parameters.AddWithValue("@NameT", modelData.nameT);
                cm.Parameters.AddWithValue("@SurNameT", modelData.surnameT);
                cm.Parameters.AddWithValue("@TitleE", modelData.titleE);

                cm.Parameters.AddWithValue("@NameE", modelData.nameE);
                cm.Parameters.AddWithValue("@SurNameE", modelData.surnameE);
                cm.Parameters.AddWithValue("@BirthDate", modelData.birthdate);
                cm.Parameters.AddWithValue("@Age", modelData.age);
                cm.Parameters.AddWithValue("@Email", modelData.email);

                cm.Parameters.AddWithValue("@Nation", modelData.nation);
                cm.Parameters.AddWithValue("@Gentle", modelData.gentle);
                cm.Parameters.AddWithValue("@MobileNo", modelData.mobileno);
                cm.Parameters.AddWithValue("@Address", modelData.address);
                cm.Parameters.AddWithValue("@TumbolID", modelData.tumbol);

                cm.Parameters.AddWithValue("@AmphurID", modelData.amphur);
                cm.Parameters.AddWithValue("@ChangwatID", modelData.changwat);
                cm.Parameters.AddWithValue("@ZipCode", modelData.zipcode);
                cm.Parameters.AddWithValue("@CardImg", modelData.cardimg);
                //cm.Parameters.AddWithValue("@Disability", modelData.Disability);
                //cm.Parameters.AddWithValue("@Disease", modelData.disease);
                // cm.Parameters.AddWithValue("@DocumentNo", modelData.documentNo);
                // cm.Parameters.AddWithValue("@PassportNo", modelData.passportNo);
                // cm.Parameters.AddWithValue("@PassportCountry", modelData.passportCountry);
                //cm.Parameters.AddWithValue("@PassportImg", modelData.passportimg);
                // cm.Parameters.AddWithValue("@PassportImgFType", modelData.passportimgftype);
                // cm.Parameters.AddWithValue("@IP", HttpContext.Current.Request.UserHostAddress);
                // cm.Parameters.AddWithValue("@branchid", modelData.branchid);
                cm.Parameters.AddWithValue("@User", modelData.User);
                cm.Parameters.AddWithValue("@nickname", modelData.nickname);

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
        #endregion

        #region แก้ไขครู
        public TeacherModel EditTeacher(TeacherModel modelData)
        {
            var model = new TeacherModel();
            using (SqlConnection db = new SqlConnection(_CON_STR))
            {
                SqlCommand cm = new SqlCommand("[sp_SetTeacher]", db);
                string birthdate = modelData.birthdate + " "+"00:00:00";

                cm.Parameters.AddWithValue("@flag", "Edit");
                cm.Parameters.AddWithValue("@ind", modelData.ind);
                cm.Parameters.AddWithValue("@IDCard", modelData.idcard);
                cm.Parameters.AddWithValue("@TitleT", modelData.titleT);
                cm.Parameters.AddWithValue("@NameT", modelData.nameT);
                cm.Parameters.AddWithValue("@SurNameT", modelData.surnameT);
                cm.Parameters.AddWithValue("@TitleE", modelData.titleE);

                cm.Parameters.AddWithValue("@NameE", modelData.nameE);
                cm.Parameters.AddWithValue("@SurNameE", modelData.surnameE);
                cm.Parameters.AddWithValue("@BirthDate", birthdate);
                cm.Parameters.AddWithValue("@Age", modelData.age);
                cm.Parameters.AddWithValue("@Email", modelData.email);

                cm.Parameters.AddWithValue("@Nation", modelData.nation);
                cm.Parameters.AddWithValue("@Gentle", modelData.gentle);
                cm.Parameters.AddWithValue("@MobileNo", modelData.mobileno);
                cm.Parameters.AddWithValue("@Address", modelData.address);
                cm.Parameters.AddWithValue("@TumbolID", modelData.tumbol);

                cm.Parameters.AddWithValue("@AmphurID", modelData.amphur);
                cm.Parameters.AddWithValue("@ChangwatID", modelData.changwat);
                cm.Parameters.AddWithValue("@ZipCode", modelData.zipcode);
                cm.Parameters.AddWithValue("@CardImg", modelData.cardimg);
                cm.Parameters.AddWithValue("@User", modelData.User);
                cm.Parameters.AddWithValue("@nickname", modelData.nickname);

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
        #endregion

        #region ดึงข้อมูลครู By Id
        public TeacherModel GetTeacherById(TeacherModel model)
        {
            using (SqlConnection db = new SqlConnection(_CON_STR))
            {
                SqlCommand cm = new SqlCommand("[sp_SetTeacher]", db);
                cm.Parameters.AddWithValue("@flag", "ListTeacherById");
                cm.Parameters.AddWithValue("@IP", HttpContext.Current.Request.UserHostAddress);
                cm.Parameters.AddWithValue("@ind ", model.ind);
                cm.CommandType = CommandType.StoredProcedure;
                if (db.State == ConnectionState.Closed) db.Open();
                SqlDataReader dr = cm.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        //model.bookingno = dr["bookingno"].ToString();
                        // model.voucherno = dr["voucherno"].ToString();
                        // model.Teacherind = dr["ind"].ToString();
                        //model.Teachertype = dr["Teachertype"].ToString();
                        model.ind = dr["ind"].ToString();
                        model.idcard = dr["idcard"].ToString();
                        model.titleT = dr["titleT"].ToString();
                        model.nameT = dr["nameT"].ToString();
                        model.surnameT = dr["surnameT"].ToString();
                        model.titleE = dr["titleE"].ToString();
                        model.nameE = dr["nameE"].ToString();
                        model.surnameE = dr["surnameE"].ToString();

                        //model.examdate = dr["examdate"].ToString();
                        model.birthdate = dr["birthdate"].ToString();

                        model.age = dr["age"].ToString();
                        model.email = dr["email"].ToString();
                        model.nation = dr["nation"].ToString();
                        model.gentle = dr["gentle"].ToString();
                        model.mobileno = dr["mobileno"].ToString();
                        model.address = dr["address"].ToString();
                        model.tumbol = dr["tumbol"].ToString();
                        model.amphur = dr["amphur"].ToString();
                        model.changwat = dr["changwat"].ToString();
                        model.zipcode = dr["zipcode"].ToString();
                        model.cardimg = dr["cardimg"].ToString();
                        model.nickname = dr["nickname"].ToString();
                        //model.Disability = dr["Disability"].ToString();
                        //model.disease = dr["disease"].ToString();
                        //model.courseid = dr["courseid"].ToString();
                        //model.coursename = dr["coursename"].ToString();
                        //model.coursegroup = dr["coursegroup"].ToString();
                        //model.documentNo = dr["documentNo"].ToString();
                        //model.passportNo = dr["passportNo"].ToString();
                        //model.passportCountry = dr["passportCountry"].ToString();
                        //model.passportimg = dr["passportimg"].ToString();
                        //model.passportimgftype = dr["passportimgftype"].ToString();
                        //model.courseprice = dr["courseprice"].ToString();
                    }
                }
                return model;
            }
        }
        #endregion

        #region cancle ครู
        public TeacherModel CancelTeacher2(TeacherModel ugmData)
        {
            var model = new TeacherModel();
            using (SqlConnection db = new SqlConnection(_CON_STR))
            {
                SqlCommand cm = new SqlCommand("[sp_SetTeacher]", db);

                cm.Parameters.AddWithValue("@flag", "Cancel");
                cm.Parameters.AddWithValue("@ind", ugmData.ind);


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
        #endregion

        #region Re-cancle ครู
        public TeacherModel ReCancelTeacher2(TeacherModel ugmData)
        {
            var model = new TeacherModel();
            using (SqlConnection db = new SqlConnection(_CON_STR))
            {
                SqlCommand cm = new SqlCommand("[sp_SetTeacher]", db);

                cm.Parameters.AddWithValue("@flag", "Re-Cancel");
                cm.Parameters.AddWithValue("@ind", ugmData.ind);

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
        #endregion

        #region "List ข้อมูลวันหยุดครู"
        /// <summary>
        /// ดึงข้อมูล List ข้อมูลวันหยุดครู
        /// by : nopphakorn
        /// </summary>
        /// <returns></returns>
        public List<LeaveDateModel> GetListLeaveDate(LeaveDateModel value)
        {
            var list = new List<LeaveDateModel>();
            using (SqlConnection db = new SqlConnection(_CON_STR))
            {
                SqlCommand cm = new SqlCommand("sp_SetTeacher", db);
                cm.Parameters.AddWithValue("@flag", "ListLeaveDate");
                cm.Parameters.AddWithValue("@fdate", value.fdate);
                cm.Parameters.AddWithValue("@tdate", value.tdate);
                cm.Parameters.AddWithValue("@ind", value.ind);
                cm.CommandType = CommandType.StoredProcedure;
                if (db.State == ConnectionState.Closed) db.Open();
                SqlDataReader dr = cm.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        var model = new LeaveDateModel();
                        if (dr["leavefdate"].ToString() != "")
                        {
                            //DateTime _leavefdate = DateTime.ParseExact(dr["leavefdate"].ToString(), "yyyy-MM-dd", new System.Globalization.CultureInfo("en-GB"));
                            //model.leavefdate = _leavefdate.ToString("dd/MM/yyyy", new System.Globalization.CultureInfo("en-GB"));
                            model.leavefdate = Convert.ToDateTime(dr["leavefdate"].ToString()).ToString("dd/MM/yyyy", new System.Globalization.CultureInfo("en-GB"));
                        }
                        model.ind = dr["ind"].ToString();
                        model.teacherind = dr["teacherind"].ToString();
                        model.teachername = dr["teacharname"].ToString();
                        model.leaveftime = dr["leaveftime"].ToString();
                        model.leavettime = dr["leavettime"].ToString();
                        model.remark = dr["remark"].ToString();
                        model.para = dr["para"].ToString();
                        list.Add(model);
                    }
                    dr.Close();
                }
            }
            return list;
        }
        #endregion
        #region "List ข้อมูลครู"
        /// <summary>
        /// ดึงข้อมูล List ข้อมูลครู
        /// by : nopphakorn
        /// </summary>
        /// <returns></returns>
        public List<LeaveDateModel> GetListteacherActive()
        {
            var list = new List<LeaveDateModel>();
            using (SqlConnection db = new SqlConnection(_CON_STR))
            {
                SqlCommand cm = new SqlCommand("sp_SCLookup", db);
                cm.Parameters.AddWithValue("@flag", "lteacherActive");
                cm.CommandType = CommandType.StoredProcedure;
                if (db.State == ConnectionState.Closed) db.Open();
                SqlDataReader dr = cm.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        var model = new LeaveDateModel();
                        model.ind = dr["ind"].ToString();
                        model.teachername = dr["teachername"].ToString();
                        model.nickname = dr["nickname"].ToString();
                        list.Add(model);
                    }
                    dr.Close();
                }
            }
            return list;
        }
        #endregion
        #region "AddLeavebyday"
        public ResultModel AddLeavebyday(LeaveDateModel value, string user)
        {
            var model = new ResultModel();
            if (value.fdate_day != null)
            {
                DateTime _fdate = DateTime.ParseExact(value.fdate_day.Trim(), "dd/MM/yyyy", new System.Globalization.CultureInfo("en-GB"));
                value.fdate_day = _fdate.ToString("yyyy-MM-dd", new System.Globalization.CultureInfo("en-GB"));
            }
            if (value.tdate_day != null)
            {
                DateTime _tdate = DateTime.ParseExact(value.tdate_day.Trim(), "dd/MM/yyyy", new System.Globalization.CultureInfo("en-GB"));
                value.tdate_day = _tdate.ToString("yyyy-MM-dd", new System.Globalization.CultureInfo("en-GB"));
            }
            using (SqlConnection db = new SqlConnection(_CON_STR))
            {
                SqlCommand cm = new SqlCommand("[sp_SetTeacher]", db);
                cm.Parameters.AddWithValue("@flag", "addLeave");
                cm.Parameters.AddWithValue("@fadd", "byday");
                cm.Parameters.AddWithValue("@ind", value.ind);
                cm.Parameters.AddWithValue("@day", value.day);
                cm.Parameters.AddWithValue("@fldate", value.fdate_day);
                cm.Parameters.AddWithValue("@tldate", value.tdate_day);
                cm.Parameters.AddWithValue("@remark", value.remark);
                cm.Parameters.AddWithValue("@ip", HttpContext.Current.Request.UserHostAddress);
                cm.Parameters.AddWithValue("@user", user);
                cm.CommandType = CommandType.StoredProcedure;
                if (db.State == ConnectionState.Closed) db.Open();
                SqlDataReader dr = cm.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    model.Result = dr["result"].ToString();
                    model.Message = dr["msg"].ToString();
                    model.ReturnId = dr["ind"].ToString();
                }
            }
            return model;
        }
        #endregion
        #region "AddLeavebydate"
        public ResultModel AddLeavebydate(LeaveDateModel value, string user)
        {
            var model = new ResultModel();
            if (value.fdate != null)
            {
                DateTime _fdate = DateTime.ParseExact(value.fdate.Trim(), "dd/MM/yyyy", new System.Globalization.CultureInfo("en-GB"));
                value.fdate = _fdate.ToString("yyyy-MM-dd", new System.Globalization.CultureInfo("en-GB"));
            }
            if (value.tdate != null)
            {
                DateTime _tdate = DateTime.ParseExact(value.tdate.Trim(), "dd/MM/yyyy", new System.Globalization.CultureInfo("en-GB"));
                value.tdate = _tdate.ToString("yyyy-MM-dd", new System.Globalization.CultureInfo("en-GB"));
            }
            using (SqlConnection db = new SqlConnection(_CON_STR))
            {
                SqlCommand cm = new SqlCommand("[sp_SetTeacher]", db);
                cm.Parameters.AddWithValue("@flag", "addLeave");
                cm.Parameters.AddWithValue("@fadd", "bydate");
                cm.Parameters.AddWithValue("@ind", value.ind);
                cm.Parameters.AddWithValue("@fldate", value.fdate);
                cm.Parameters.AddWithValue("@tldate", value.tdate);
                cm.Parameters.AddWithValue("@ftime", value.leaveftime);
                cm.Parameters.AddWithValue("@ttime", value.leavettime);
                cm.Parameters.AddWithValue("@remark", value.remark);
                cm.Parameters.AddWithValue("@ip", HttpContext.Current.Request.UserHostAddress);
                cm.Parameters.AddWithValue("@user", user);
                cm.CommandType = CommandType.StoredProcedure;
                if (db.State == ConnectionState.Closed) db.Open();
                SqlDataReader dr = cm.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    model.Result = dr["result"].ToString();
                    model.Message = dr["msg"].ToString();
                    model.ReturnId = dr["ind"].ToString();
                }
            }
            return model;
        }
        #endregion
        #region "editLeavedate"
        public ResultModel editLeavedate(LeaveDateModel value, string user)
        {
            var model = new ResultModel();
            if (value.leavefdate != null)
            {
                DateTime _fdate = DateTime.ParseExact(value.leavefdate.Trim(), "dd/MM/yyyy", new System.Globalization.CultureInfo("en-GB"));
                value.leavefdate = _fdate.ToString("yyyy-MM-dd", new System.Globalization.CultureInfo("en-GB"));
            }
            using (SqlConnection db = new SqlConnection(_CON_STR))
            {
                SqlCommand cm = new SqlCommand("[sp_SetTeacher]", db);
                cm.Parameters.AddWithValue("@flag", "editLeave");
                cm.Parameters.AddWithValue("@leaveind", value.ind);
                cm.Parameters.AddWithValue("@leavefdate", value.leavefdate);
                cm.Parameters.AddWithValue("@leaveftime", value.leaveftime);
                cm.Parameters.AddWithValue("@leavettime", value.leavettime);
                cm.Parameters.AddWithValue("@remark", value.remark);
                cm.Parameters.AddWithValue("@ip", HttpContext.Current.Request.UserHostAddress);
                cm.Parameters.AddWithValue("@user", user);
                cm.CommandType = CommandType.StoredProcedure;
                if (db.State == ConnectionState.Closed) db.Open();
                SqlDataReader dr = cm.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    model.Result = dr["result"].ToString();
                    model.Message = dr["msg"].ToString();
                    model.ReturnId = dr["ind"].ToString();
                }
            }
            return model;
        }
        #endregion
        #region "deleteLeaveDate"
        public ResultModel deleteLeaveDate(LeaveDateModel value)
        {
            var model = new ResultModel();
            using (SqlConnection db = new SqlConnection(_CON_STR))
            {
                SqlCommand cm = new SqlCommand("[sp_SetTeacher]", db);
                cm.Parameters.AddWithValue("@flag", "deleteLeaveDate");
                cm.Parameters.AddWithValue("@listDeteteLeave", value.listDeteteLeave);
                cm.CommandType = CommandType.StoredProcedure;
                if (db.State == ConnectionState.Closed) db.Open();
                SqlDataReader dr = cm.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    model.Result = dr["result"].ToString();
                    model.Message = dr["msg"].ToString();
                    model.ReturnId = dr["ind"].ToString();
                }
            }
            return model;
        }
        #endregion
        #region "Getleavebyid"
        public LeaveDateModel Getleavebyid(LeaveDateModel value)
        {
            var model = new LeaveDateModel();
            using (SqlConnection db = new SqlConnection(_CON_STR))
            {
                SqlCommand cm = new SqlCommand("[sp_SetTeacher]", db);
                cm.Parameters.AddWithValue("@flag", "getleavebyid");
                cm.Parameters.AddWithValue("@leaveind ", value.ind);
                cm.CommandType = CommandType.StoredProcedure;
                if (db.State == ConnectionState.Closed) db.Open();
                SqlDataReader dr = cm.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        if (dr["leavefdate"].ToString() != "")
                        {
                            //DateTime _leavefdate = DateTime.ParseExact(dr["leavefdate"].ToString(), "yyyy-MM-dd", new System.Globalization.CultureInfo("en-GB"));
                            //model.leavefdate = _leavefdate.ToString("dd/MM/yyyy", new System.Globalization.CultureInfo("en-GB"));
                            model.leavefdate = Convert.ToDateTime(dr["leavefdate"].ToString()).ToString("dd/MM/yyyy", new System.Globalization.CultureInfo("en-GB"));
                        }
                        if (dr["leaveftime"].ToString() != "")
                        {
                            TimeSpan _leaveftime = TimeSpan.Parse(dr["leaveftime"].ToString());
                            model.leaveftime = _leaveftime.ToString(@"hh\:mm");
                        }
                        if (dr["leavettime"].ToString() != "")
                        {
                            TimeSpan _leavettime = TimeSpan.Parse(dr["leavettime"].ToString());
                            model.leavettime = _leavettime.ToString(@"hh\:mm");
                        }
                        model.ind = dr["ind"].ToString();
                        model.teacherind = dr["teacherind"].ToString();
                        model.remark = dr["remark"].ToString();
                        model.ip = dr["ip"].ToString();
                        model.insertby = dr["insertby"].ToString();
                        model.insertdate = dr["insertdate"].ToString();
                        model.updateby = dr["updateby"].ToString();
                        model.updatedate = dr["updatedate"].ToString();
                        model.para = dr["para"].ToString();
                        model.teachername = dr["teachername"].ToString();

                    }
                }
                return model;
            }
        }
        #endregion

    }
}
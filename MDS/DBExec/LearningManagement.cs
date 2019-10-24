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
    public class LearningManagement
    {
        public static string _CON_STR = ConfigurationManager.ConnectionStrings["MDS"].ConnectionString;
        #region "ดึงข้อมูล Getlcourse"
        public static List<lcourseModel> Getlcourse()
        {
            var list = new List<lcourseModel>();
            using (SqlConnection db = new SqlConnection(_CON_STR))
            {
                SqlCommand cm = new SqlCommand("sp_SCLookup", db);
                cm.Parameters.AddWithValue("@flag", "lcourse");
                cm.CommandType = CommandType.StoredProcedure;
                if (db.State == ConnectionState.Closed) db.Open();
                SqlDataReader dr = cm.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        var model = new lcourseModel();
                        model.courseid = dr["courseid"].ToString();
                        model.coursename = dr["coursename"].ToString();
                        model.courseprice = dr["courseprice"].ToString();
                        model.coursegroup = dr["coursegroup"].ToString();
                        model.coursetime = dr["coursetime"].ToString();
                        model.coursegroupid = dr["coursegroupid"].ToString();
                        model.courseid_time = dr["courseid"].ToString() + " " + dr["coursegroupid"].ToString() + " " + dr["coursetime"].ToString();
                        list.Add(model);
                    }
                    dr.Close();
                }
            }
            return list;
        }
        #endregion
        #region "ดึงข้อมูล Getsubject"
        public static List<lsubjectModel> Getsubject(string courseid)
        {
            var list = new List<lsubjectModel>();
            using (SqlConnection db = new SqlConnection(_CON_STR))
            {
                SqlCommand cm = new SqlCommand("sp_SCLookup", db);
                cm.Parameters.AddWithValue("@flag", "lsubject");
                cm.Parameters.AddWithValue("@courseid", courseid);
                cm.CommandType = CommandType.StoredProcedure;
                if (db.State == ConnectionState.Closed) db.Open();
                SqlDataReader dr = cm.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        var model = new lsubjectModel();
                        model.subjectid = dr["subjectid"].ToString();
                        model.subjectname = dr["subjectname"].ToString();
                        model.subjectnickname = dr["subjectnickname"].ToString();
                        model.courseid = dr["courseid"].ToString();
                        model.subjecttype = dr["subjecttype"].ToString();
                        model.subjectmin = dr["subjectmin"].ToString();
                        model.status = dr["status"].ToString();
                        list.Add(model);
                    }
                    dr.Close();
                }
            }
            return list;
        }
        #endregion
        #region "ดึงข้อมูล GetsubjectByBookingid"
        public static List<lsubjectModel> GetsubjectByBookingid(string bookingid)
        {
            var list = new List<lsubjectModel>();
            using (SqlConnection db = new SqlConnection(_CON_STR))
            {
                SqlCommand cm = new SqlCommand("sp_SCLookup", db);
                cm.Parameters.AddWithValue("@flag", "lsubjectRemain");
                cm.Parameters.AddWithValue("@bookingid", bookingid);
                cm.CommandType = CommandType.StoredProcedure;
                if (db.State == ConnectionState.Closed) db.Open();
                SqlDataReader dr = cm.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        var model = new lsubjectModel();
                        model.bookingid = dr["bookingid"].ToString();
                        model.subjectid = dr["subjectid"].ToString();
                        model.subjectnickname = dr["subjectnickname"].ToString();
                        model.subjectname = dr["subjectname"].ToString();
                        model.subjecttype = dr["subjecttype"].ToString();
                        model.subjectdesc = dr["subjectdesc"].ToString();                        
                        model.subjectmin = dr["subjectmin"].ToString();
                        model.sumstudytime = dr["sumstudytime"].ToString();
                        model.remaintime = dr["remaintime"].ToString();
                        list.Add(model);
                    }
                    dr.Close();
                }
            }
            return list;
        }
        #endregion
        #region "ดึงข้อมูล lsubjectRemainById"
        public static List<lsubjectModel> GetlsubjectRemainById(string bookingid,string subjectid)
        {
            var list = new List<lsubjectModel>();
            using (SqlConnection db = new SqlConnection(_CON_STR))
            {
                SqlCommand cm = new SqlCommand("sp_SCLookup", db);
                cm.Parameters.AddWithValue("@flag", "lsubjectRemainById");
                cm.Parameters.AddWithValue("@bookingid", bookingid);
                cm.Parameters.AddWithValue("@subjectid", subjectid);
                cm.CommandType = CommandType.StoredProcedure;
                if (db.State == ConnectionState.Closed) db.Open();
                SqlDataReader dr = cm.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        var model = new lsubjectModel();
                        model.bookingid = dr["bookingid"].ToString();
                        model.subjectid = dr["subjectid"].ToString();
                        model.subjectnickname = dr["subjectnickname"].ToString();
                        model.subjectname = dr["subjectname"].ToString();
                        model.subjecttype = dr["subjecttype"].ToString();
                        model.subjectdesc = dr["subjectdesc"].ToString();
                        model.subjectmin = dr["subjectmin"].ToString();
                        model.sumstudytime = dr["sumstudytime"].ToString();
                        model.remaintime = dr["remaintime"].ToString();
                        list.Add(model);
                    }
                    dr.Close();
                }
            }
            return list;
        }
        #endregion
        #region "ดึงข้อมูล Getteacher"
        public static List<lteacherModel> Getteacher()
        {
            var list = new List<lteacherModel>();
            using (SqlConnection db = new SqlConnection(_CON_STR))
            {
                SqlCommand cm = new SqlCommand("sp_SCLookup", db);
                cm.Parameters.AddWithValue("@flag", "lteacher");
                cm.CommandType = CommandType.StoredProcedure;
                if (db.State == ConnectionState.Closed) db.Open();
                SqlDataReader dr = cm.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        var model = new lteacherModel();
                        model.titleT = dr["titleT"].ToString();
                        model.nameT = dr["nameT"].ToString();
                        model.surnameT = dr["surnameT"].ToString();
                        model.nickname = dr["nickname"].ToString();
                        model.ind = dr["ind"].ToString();
                        model.coursegroupid = dr["coursegroupid"].ToString();
                        model.coursegroupname = dr["coursegroupname"].ToString();
                        list.Add(model);
                    }
                    dr.Close();
                }
            }
            return list;
        }
        #endregion
        #region "ดึงข้อมูล GetteacherCalendar"
        public static List<TeacherCalendarModel> GetteacherCalendar()
        {
            var list = new List<TeacherCalendarModel>();
            using (SqlConnection db = new SqlConnection(_CON_STR))
            {
                SqlCommand cm = new SqlCommand("sp_SCLookup", db);
                cm.Parameters.AddWithValue("@flag", "lteacherwithHour");
                cm.CommandType = CommandType.StoredProcedure;
                if (db.State == ConnectionState.Closed) db.Open();
                SqlDataReader dr = cm.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        var model = new TeacherCalendarModel();
                        model.id= dr["ind"].ToString();
                        model.title = dr["nameT"].ToString() + " " +dr["surnameT"].ToString() + " - " + dr["nickname"].ToString();
                        model.a1 = dr["a1"].ToString();
                        model.a2 = dr["a2"].ToString();
                        model.a3 = dr["a3"].ToString();
                        model.a4 = dr["a4"].ToString();
                        model.a5 = dr["a5"].ToString();
                        model.b1 = dr["b1"].ToString();
                        model.b2 = dr["b2"].ToString();
                        model.b3 = dr["b3"].ToString();
                        model.b4 = dr["b4"].ToString();
                        model.b5 = dr["b5"].ToString();
                        model.c1 = dr["c1"].ToString();
                        model.c2 = dr["c2"].ToString();
                        model.c3 = dr["c3"].ToString();
                        model.c4 = dr["c4"].ToString();
                        model.c5 = dr["c5"].ToString();
                        model.d1 = dr["d1"].ToString();
                        model.d2 = dr["d2"].ToString();
                        model.d3 = dr["d3"].ToString();
                        model.d4 = dr["d4"].ToString();
                        model.d5 = dr["d5"].ToString();
                        list.Add(model);
                    }
                    dr.Close();
                }
            }
            return list;
        }
        #endregion
        #region "ดึงข้อมูล GetteacherBycourseid"
        public static List<lteacherModel> GetteacherBycourseid(string courseid)
        {
            var list = new List<lteacherModel>();
            using (SqlConnection db = new SqlConnection(_CON_STR))
            {
                SqlCommand cm = new SqlCommand("sp_SCLookup", db);
                cm.Parameters.AddWithValue("@flag", "lteacherbycourse");
                cm.Parameters.AddWithValue("@coursegroupid", courseid);
                cm.CommandType = CommandType.StoredProcedure;
                if (db.State == ConnectionState.Closed) db.Open();
                SqlDataReader dr = cm.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        var model = new lteacherModel();
                        model.titleT = dr["titleT"].ToString();
                        model.nameT = dr["nameT"].ToString();
                        model.surnameT = dr["surnameT"].ToString();
                        model.nickname = dr["nickname"].ToString();
                        model.ind = dr["ind"].ToString();
                        model.coursegroupid = dr["coursegroupid"].ToString();
                        model.coursegroupname = dr["coursegroupname"].ToString();
                        model.studytime = dr["studytime"].ToString();
                        list.Add(model);
                    }
                    dr.Close();
                }
            }
            return list;
        }
        #endregion
        #region "ดึงข้อมูล AddBooking"
        public static ResultBookingModel AddBooking(string courseid, string user, string coursetime, string branchid, string bookingname, string remark, string gear)
        {
            var model = new ResultBookingModel();
            using (SqlConnection db = new SqlConnection(_CON_STR))
            {
                if (db.State == ConnectionState.Closed) db.Open();
                SqlCommand cm = new SqlCommand("[sp_SCBooking]", db);
                cm.CommandType = CommandType.StoredProcedure;
                cm.Parameters.AddWithValue("@flag", "AddBooking");
                cm.Parameters.AddWithValue("@courseid", courseid);
                cm.Parameters.AddWithValue("@studentind", "0");
                cm.Parameters.AddWithValue("@voucherid", "1");
                cm.Parameters.AddWithValue("@remark", remark);
                cm.Parameters.AddWithValue("@user", user);
                cm.Parameters.AddWithValue("@ip", HttpContext.Current.Request.UserHostAddress);
                cm.Parameters.AddWithValue("@coursetime", coursetime);
                cm.Parameters.AddWithValue("@branchid", branchid);
                cm.Parameters.AddWithValue("@boookingname", bookingname);
                cm.Parameters.AddWithValue("@gear", gear);
                SqlDataReader dr = cm.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {

                        model.bookingid = dr["bookingid"].ToString();
                        model.voucherid = dr["voucherid"].ToString();
                        model.msg = dr["msg"].ToString();
                        model.result = dr["result"].ToString();
                    };
                }
                dr.Close();

            }
            return model;
        }
        #endregion
        #region "ดึงข้อมูล GetListBooking"
        public static List<BookingModel> GetListBooking()
        {
            var list = new List<BookingModel>();
            using (SqlConnection db = new SqlConnection(_CON_STR))
            {
                SqlCommand cm = new SqlCommand("sp_SCBooking", db);
                cm.Parameters.AddWithValue("@flag", "ListBooking");
                cm.CommandType = CommandType.StoredProcedure;
                if (db.State == ConnectionState.Closed) db.Open();
                SqlDataReader dr = cm.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        var model = new BookingModel();
                        model.branchname = dr["branchname"].ToString();
                        model.bookingid = ENDEtxtManagement.Encrypt(dr["bookingid"].ToString());
                        model.bookingidshow = dr["bookingid"].ToString();
                        model.bookingname = dr["bookingname"].ToString();
                        model.bookingdate = dr["bookingdate"].ToString();
                        model.courseid = ENDEtxtManagement.Encrypt(dr["courseid"].ToString());
                        model.coursenickname = dr["coursenickname"].ToString();
                        model.studentname = dr["studentname"].ToString();
                        model.fullname = dr["uname"].ToString();
                        model.statusdesc = dr["statusdesc"].ToString();
                        //model.totaltime = dr["totaltime"].ToString();
                        if (!dr["totaltime"].ToString().Equals("00:00:00:000"))
                        {
                            var strTime = dr["totaltime"].ToString().Split(':');
                            if (!strTime[1].Equals("00"))
                            {
                                model.totaltime = Convert.ToInt32(strTime[0]).ToString() + " ชม." + strTime[1] + " นาที";
                            }
                            else
                            {
                                model.totaltime = Convert.ToInt32(strTime[0]).ToString() + " ชม.";
                            }

                        }
                        else
                        {
                            model.totaltime = "0 ชม.";
                        }
                        list.Add(model);
                    }
                    dr.Close();
                }
            }
            return list;
        }
        #endregion
        #region "ดึงข้อมูล GetbookingByid"
        public static List<BookingDetailModel> GetbookingByid(string bookingid)
        {
            var list = new List<BookingDetailModel>();
            using (SqlConnection db = new SqlConnection(_CON_STR))
            {
                if (db.State == ConnectionState.Closed) db.Open();
                SqlCommand cm = new SqlCommand("[sp_SCBooking]", db);
                cm.CommandType = CommandType.StoredProcedure;
                cm.Parameters.AddWithValue("@flag", "ListBookingById");
                cm.Parameters.AddWithValue("@bookingid", bookingid);
                SqlDataReader dr = cm.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        if (dr["ind"].ToString() != "")
                        {
                            if (dr["status"].ToString() != "1")
                            {
                                DateTime stDate = DateTime.ParseExact(dr["studydate"].ToString(), "yyyy-MM-dd", new System.Globalization.CultureInfo("en-GB"));
                                //DateTime stDate = DateTime.Parse(dr["studydate"].ToString());
                                var model = new BookingDetailModel();
                                model.branchid = dr["branchid"].ToString();
                                model.branchname = dr["branchname"].ToString();
                                model.bookingid = dr["bookingid"].ToString();
                                model.ind = dr["ind"].ToString();
                                model.bookingdate = dr["bookingdate"].ToString();
                                model.courseid = dr["courseid"].ToString();
                                model.studentind = dr["studentind"].ToString();
                                model.voucherid = dr["voucherid"].ToString();
                                model.insertby = dr["insertby"].ToString();
                                model.fullname = dr["uname"].ToString();
                                model.status = dr["status"].ToString();
                                model.statusdesc = dr["statusdesc"].ToString();
                                model.teacherind = dr["teacherind"].ToString();
                                model.nickname = dr["nickname"].ToString();
                                model.subjectid = dr["subjectid"].ToString();
                                //model.studydate = stDate.ToString("yyyy-MM-dd", new System.Globalization.CultureInfo("en-GB"));
                                model.studydate = stDate.ToString("dd/MM/yyyy", new System.Globalization.CultureInfo("en-GB"));
                                model.studystime = Convert.ToDateTime(dr["studystime"].ToString()).ToString("HH:mm", new System.Globalization.CultureInfo("en-GB"));
                                model.studyetime = Convert.ToDateTime(dr["studyetime"].ToString()).ToString("HH:mm", new System.Globalization.CultureInfo("en-GB"));
                                model.studytime = dr["studytime"].ToString();
                                model.coursetime = dr["coursetime"].ToString();
                                model.subjectname = dr["subjectname"].ToString();
                                model.subjectnickname = dr["subjectnickname"].ToString();
                                model.sumstudytime = dr["sumstudytime"].ToString();
                                model.sumsubjecttime = dr["sumsubjecttime"].ToString();
                                model.bookingname = dr["bookingname"].ToString();
                                model.remark = dr["remark"].ToString();
                                model.subjecttype = dr["subjecttype"].ToString();
                                model.subjecttypedesc = dr["subjecttypedesc"].ToString();
                                model.coursenickname = dr["coursenickname"].ToString();
                                model.title = dr["title"].ToString();
                                model.description = dr["description"].ToString();
                                model.subjectmin = dr["subjectmin"].ToString();
                                model.studentname = dr["studentname"].ToString();                                
                                model.gear = dr["gear"].ToString();
                                list.Add(model);
                            }

                        }
                    };
                }
                dr.Close();

            }
            return list;
        }
        #endregion
        #region "ดึงข้อมูล GetbookingByidOneRow"
        public static List<BookingDetailModel> GetbookingByidOneRow(string bookingid)
        {
            var list = new List<BookingDetailModel>();
            using (SqlConnection db = new SqlConnection(_CON_STR))
            {
                if (db.State == ConnectionState.Closed) db.Open();
                SqlCommand cm = new SqlCommand("[sp_SCBooking]", db);
                cm.CommandType = CommandType.StoredProcedure;
                cm.Parameters.AddWithValue("@flag", "ListBookingById");
                cm.Parameters.AddWithValue("@bookingid", bookingid);
                SqlDataReader dr = cm.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        var model = new BookingDetailModel();
                        model.branchid = dr["branchid"].ToString();
                        model.branchname = dr["branchname"].ToString();
                        model.bookingid = dr["bookingid"].ToString();
                        model.ind = dr["ind"].ToString();
                        model.bookingdate = dr["bookingdate"].ToString();
                        model.courseid = dr["courseid"].ToString();
                        model.studentind = dr["studentind"].ToString();
                        model.voucherid = dr["voucherid"].ToString();
                        model.insertby = dr["insertby"].ToString();
                        model.fullname = dr["uname"].ToString();
                        model.status = dr["status"].ToString();
                        model.statusdesc = dr["statusdesc"].ToString();
                        model.teacherind = dr["teacherind"].ToString();
                        model.nickname = dr["nickname"].ToString();
                        model.subjectid = dr["subjectid"].ToString();
                        model.studytime = dr["studytime"].ToString();
                        model.coursetime = dr["coursetime"].ToString();
                        model.subjectname = dr["subjectname"].ToString();
                        model.subjectnickname = dr["subjectnickname"].ToString();
                        model.sumstudytime = dr["sumstudytime"].ToString();
                        model.sumsubjecttime = dr["sumsubjecttime"].ToString();
                        model.bookingname = dr["bookingname"].ToString();
                        model.remark = dr["remark"].ToString();
                        model.subjecttype = dr["subjecttype"].ToString();
                        model.subjecttypedesc = dr["subjecttypedesc"].ToString();
                        model.coursenickname = dr["coursenickname"].ToString();
                        model.title = dr["title"].ToString();
                        model.description = dr["description"].ToString();
                        model.studentname = dr["studentname"].ToString();
                        model.gear = dr["gear"].ToString();
                        list.Add(model);
                    };
                }
                dr.Close();

            }
            return list;
        }
        #endregion
        #region "ดึงข้อมูล GetbookingByidCalendar"
        public static List<CalendarModel> GetbookingByidCalendar(string bookingid)
        {
            var list = new List<CalendarModel>();
            using (SqlConnection db = new SqlConnection(_CON_STR))
            {
                if (db.State == ConnectionState.Closed) db.Open();
                SqlCommand cm = new SqlCommand("[sp_SCBooking]", db);
                cm.CommandType = CommandType.StoredProcedure;
                cm.Parameters.AddWithValue("@flag", "ListBookingById");
                cm.Parameters.AddWithValue("@bookingid", bookingid);
                SqlDataReader dr = cm.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {

                        var model = new CalendarModel();
                        if (dr["ind"].ToString() != "")
                        {
                            if (dr["status"].ToString() != "1")
                            {
                                DateTime stDate = DateTime.ParseExact(dr["studydate"].ToString(), "yyyy-MM-dd", new System.Globalization.CultureInfo("en-GB"));
                                //DateTime stDate = DateTime.Parse(dr["studydate"].ToString());
                                model.eventID = dr["ind"].ToString();
                                model.resourceId=dr["teacherind"].ToString();
                                model.title = dr["title"].ToString() + "-" + dr["description"].ToString();
                                //model.description = dr["subjectid"].ToString() + "-" + dr["title"].ToString() + "-" + dr["description"].ToString() + "-" + dr["bookingid"].ToString() + "-" + dr["teacherind"].ToString() + "-" + dr["studytime"].ToString();
                                model.description = dr["bookingid"].ToString();
                                model.start = stDate.ToString("yyyy-MM-dd", new System.Globalization.CultureInfo("en-GB")) + "T" + Convert.ToDateTime(dr["studystime"].ToString()).ToString("HH:mm:ss", new System.Globalization.CultureInfo("en-GB"));
                                model.end = stDate.ToString("yyyy-MM-dd", new System.Globalization.CultureInfo("en-GB")) + "T" + Convert.ToDateTime(dr["studyetime"].ToString()).ToString("HH:mm:ss", new System.Globalization.CultureInfo("en-GB"));
                                model.backgroundColor = "";
                                if (dr["subjecttype"].ToString() != "E")
                                {
                                    model.color = "#66ff66";
                                    model.borderColor = "#33cc33";
                                }
                                else {
                                    model.color = "#1cb674";
                                    model.borderColor = "#38bc84";

                                }
                                model.textColor= "#0000FF";
                                model.allDay = "";
                                model.rendering = "";
                                model.overlap = "true";
                                list.Add(model);
                            }
                        }
                    };
                }
                dr.Close();

            }
            return list;
        }
        #endregion
        #region "ดึงข้อมูล ListBookingByteacher"
        public static List<CalendarModel> ListBookingByteacher(string courseid, string arrteacher)
        {
            var list = new List<CalendarModel>();
            using (SqlConnection db = new SqlConnection(_CON_STR))
            {
                SqlCommand cm = new SqlCommand("sp_SCBooking", db);
                cm.Parameters.AddWithValue("@flag", "ListBookingByteacher");
                cm.Parameters.AddWithValue("@courseid", courseid);
                cm.Parameters.AddWithValue("@listteacher", arrteacher);
                cm.CommandType = CommandType.StoredProcedure;
                if (db.State == ConnectionState.Closed) db.Open();
                SqlDataReader dr = cm.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        var model = new CalendarModel();
                        if (dr["ind"].ToString() != "")
                        {
                            if (dr["status"].ToString() != "1")
                            {
                                DateTime stDate = DateTime.Parse(dr["studydate"].ToString());
                                //DateTime stDate = DateTime.ParseExact(dr["studydate"].ToString(), "yyyy-MM-dd", new System.Globalization.CultureInfo("en-GB"));
                                model.eventID = dr["ind"].ToString();
                                model.resourceId = dr["teacherind"].ToString();
                                model.title = dr["title"].ToString() + "-" + dr["description"].ToString();
                                //model.description = dr["subjectid"].ToString() + "-" + dr["title"].ToString() + "-" + dr["description"].ToString() + "-" + dr["bookingid"].ToString();
                                model.description = dr["bookingid"].ToString();
                                model.start = stDate.ToString("yyyy-MM-dd", new System.Globalization.CultureInfo("en-GB")) + "T" + Convert.ToDateTime(dr["studystime"].ToString()).ToString("HH:mm:ss", new System.Globalization.CultureInfo("en-GB"));
                                model.end = stDate.ToString("yyyy-MM-dd", new System.Globalization.CultureInfo("en-GB")) + "T" + Convert.ToDateTime(dr["studyetime"].ToString()).ToString("HH:mm:ss", new System.Globalization.CultureInfo("en-GB"));
                                model.backgroundColor = "";
                                model.color = "#FEFCAD";
                                model.borderColor = "#cac702";
                                model.textColor = "#0000FF";
                                model.allDay = "";
                                model.rendering = "";
                                model.overlap = "true";
                                list.Add(model);
                            }
                        }
                    }
                    dr.Close();
                }
            }
            return list;
        }
        #endregion
        #region "ดึงข้อมูล ListBookingByDate"
        public static List<CalendarModel> ListBookingByDate(string currentdate)
        {
            var list = new List<CalendarModel>();
            using (SqlConnection db = new SqlConnection(_CON_STR))
            {
                SqlCommand cm = new SqlCommand("sp_SCBooking", db);
                cm.Parameters.AddWithValue("@flag", "ListBookingByDate");
                cm.Parameters.AddWithValue("@currentdate", currentdate);
                cm.CommandType = CommandType.StoredProcedure;
                if (db.State == ConnectionState.Closed) db.Open();
                SqlDataReader dr = cm.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        var model = new CalendarModel();
                        if (dr["ind"].ToString() != "")
                        {
                            DateTime stDate = DateTime.Parse(dr["studydate"].ToString());
                            model.eventID = dr["ind"].ToString();
                            model.resourceId = dr["teacherind"].ToString();
                            model.title = dr["title"].ToString() + "-" + dr["description"].ToString();
                            //model.description = dr["subjectid"].ToString() + "-" + dr["title"].ToString() + "-" + dr["description"].ToString() + "-" + dr["bookingid"].ToString() + "-" + dr["teacherind"].ToString() + "-" + dr["studytime"].ToString();
                            model.description = dr["bookingid"].ToString();
                            model.start = stDate.ToString("yyyy-MM-dd", new System.Globalization.CultureInfo("en-GB")) + "T" + Convert.ToDateTime(dr["studystime"].ToString()).ToString("HH:mm:ss", new System.Globalization.CultureInfo("en-GB"));
                            model.end = stDate.ToString("yyyy-MM-dd", new System.Globalization.CultureInfo("en-GB")) + "T" + Convert.ToDateTime(dr["studyetime"].ToString()).ToString("HH:mm:ss", new System.Globalization.CultureInfo("en-GB"));
                            model.backgroundColor = "";
                            //model.color = "#FEFCAD";
                            //model.borderColor = "#cac702";
                            //model.textColor = "#0000FF";
                            model.color = dr["color"].ToString();
                            model.borderColor = dr["borderColor"].ToString();
                            model.textColor = dr["textColor"].ToString();
                            model.allDay = "";
                            model.rendering = "";
                            model.overlap = "true";
                            model.flgcalendar = dr["flgcalendar"].ToString(); ;
                            list.Add(model);
                        }


                    }
                    dr.Close();
                }
            }
            return list;
        }
        #endregion
        #region "ดึงข้อมูล ListBookingByDateGear"
        public static List<CalendarModel> ListBookingByDateGear(string currentdate)
        {
            var list = new List<CalendarModel>();
            using (SqlConnection db = new SqlConnection(_CON_STR))
            {
                SqlCommand cm = new SqlCommand("sp_SCBooking", db);
                cm.Parameters.AddWithValue("@flag", "ListBookingByDate");
                cm.Parameters.AddWithValue("@currentdate", currentdate);
                cm.CommandType = CommandType.StoredProcedure;
                if (db.State == ConnectionState.Closed) db.Open();
                SqlDataReader dr = cm.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        var model = new CalendarModel();
                        if (dr["ind"].ToString() != "")
                        {
                            if (dr["subjecttype"].ToString() == "P") {
                                DateTime stDate = DateTime.Parse(dr["studydate"].ToString());
                                model.eventID = dr["ind"].ToString();
                                model.resourceId = dr["gear"].ToString();
                                model.title = dr["title"].ToString() + "-" + dr["description"].ToString();
                                //model.description = dr["subjectid"].ToString() + "-" + dr["title"].ToString() + "-" + dr["description"].ToString() + "-" + dr["bookingid"].ToString() + "-" + dr["teacherind"].ToString() + "-" + dr["studytime"].ToString();
                                model.description = dr["bookingid"].ToString();
                                model.start = stDate.ToString("yyyy-MM-dd", new System.Globalization.CultureInfo("en-GB")) + "T" + Convert.ToDateTime(dr["studystime"].ToString()).ToString("HH:mm:ss", new System.Globalization.CultureInfo("en-GB"));
                                model.end = stDate.ToString("yyyy-MM-dd", new System.Globalization.CultureInfo("en-GB")) + "T" + Convert.ToDateTime(dr["studyetime"].ToString()).ToString("HH:mm:ss", new System.Globalization.CultureInfo("en-GB"));
                                model.backgroundColor = "";
                                model.color = "#FEFCAD";
                                model.borderColor = "#cac702";
                                model.textColor = "#0000FF";
                                model.allDay = "";
                                model.rendering = "";
                                model.overlap = "true";
                                list.Add(model);
                            }
                            
                        }


                    }
                    dr.Close();
                }
            }
            return list;
        }
        #endregion
        #region "AddBookingDetail"
        public static ResultBookingModel AddBookingDetail(BookingDetailModel value, string user)
        {
            var model = new ResultBookingModel();
            var strsubjectid = value.subjectid.Split('-');
            DateTime stDate = DateTime.ParseExact(value.studydate, "dd/MM/yyyy", new System.Globalization.CultureInfo("en-GB"));
            DateTime startTime = DateTime.ParseExact(value.studystime, "HH:mm", new System.Globalization.CultureInfo("en-GB"));
            DateTime endTime = DateTime.ParseExact(value.studyetime, "HH:mm", new System.Globalization.CultureInfo("en-GB"));
            TimeSpan span = endTime.Subtract(startTime);
            var TotalMinutes = span.TotalMinutes;

            using (SqlConnection db = new SqlConnection(_CON_STR))
            {
                if (db.State == ConnectionState.Closed) db.Open();
                SqlCommand cm = new SqlCommand("[sp_SCBooking]", db);
                cm.CommandType = CommandType.StoredProcedure;
                cm.Parameters.AddWithValue("@flag", "AddBookingDetail");
                cm.Parameters.AddWithValue("@bookingid", value.bookingid);
                cm.Parameters.AddWithValue("@subjectid", strsubjectid[0]);
                cm.Parameters.AddWithValue("@teacherind", value.teacherind);
                cm.Parameters.AddWithValue("@studydate", stDate.ToString("yyyy-MM-dd", new System.Globalization.CultureInfo("en-GB")));
                cm.Parameters.AddWithValue("@studystime", value.studystime);
                cm.Parameters.AddWithValue("@studyetime", value.studyetime);
                cm.Parameters.AddWithValue("@user", user);
                cm.Parameters.AddWithValue("@ip", HttpContext.Current.Request.UserHostAddress);
                cm.Parameters.AddWithValue("@studytime", TotalMinutes);

                SqlDataReader dr = cm.ExecuteReader();
                if (dr.HasRows)
                {

                    while (dr.Read())
                    {
                        model.bookingdetailind = dr["bookingdetailind"].ToString();
                        model.bookingid = dr["bookingid"].ToString();
                        model.voucherid = dr["voucherid"].ToString();
                        model.msg = dr["msg"].ToString();
                        model.result = dr["result"].ToString();
                    };
                }
                dr.Close();

            }
            return model;
        }
        #endregion
        #region "EditBookingDetail"
        public static ResultBookingModel EditBookingDetail(BookingDetailModel value, string user)
        {
            var model = new ResultBookingModel();
            var strsubjectid = value.subjectid.Split('-');
            DateTime stDate = DateTime.ParseExact(value.studydate, "dd/MM/yyyy", new System.Globalization.CultureInfo("en-GB"));
            DateTime startTime = DateTime.ParseExact(value.studystime, "HH:mm", new System.Globalization.CultureInfo("en-GB"));
            DateTime endTime = DateTime.ParseExact(value.studyetime, "HH:mm", new System.Globalization.CultureInfo("en-GB"));
            TimeSpan span = endTime.Subtract(startTime);
            var TotalMinutes = span.TotalMinutes;

            using (SqlConnection db = new SqlConnection(_CON_STR))
            {
                if (db.State == ConnectionState.Closed) db.Open();
                SqlCommand cm = new SqlCommand("[sp_SCBooking]", db);
                cm.CommandType = CommandType.StoredProcedure;
                cm.Parameters.AddWithValue("@flag", "EditBookingDetail");
                cm.Parameters.AddWithValue("@bookingdetailind", value.bookingdetailind);
                cm.Parameters.AddWithValue("@bookingid", value.bookingid);
                cm.Parameters.AddWithValue("@subjectid", strsubjectid[0]);
                cm.Parameters.AddWithValue("@teacherind", value.teacherind);
                cm.Parameters.AddWithValue("@studydate", stDate.ToString("yyyy-MM-dd", new System.Globalization.CultureInfo("en-GB")));
                cm.Parameters.AddWithValue("@studystime", value.studystime);
                cm.Parameters.AddWithValue("@studyetime", value.studyetime);
                cm.Parameters.AddWithValue("@user", user);
                cm.Parameters.AddWithValue("@ip", HttpContext.Current.Request.UserHostAddress);
                cm.Parameters.AddWithValue("@studytime", TotalMinutes);

                SqlDataReader dr = cm.ExecuteReader();
                if (dr.HasRows)
                {

                    while (dr.Read())
                    {
                        model.bookingdetailind = dr["bookingdetailind"].ToString();
                        model.bookingid = dr["bookingid"].ToString();
                        model.voucherid = dr["voucherid"].ToString();
                        model.msg = dr["msg"].ToString();
                        model.result = dr["result"].ToString();
                    };
                }
                dr.Close();

            }
            return model;
        }
        #endregion
        #region "SetStatusBooking"
        public static ResultBookingModel SetStatusBooking(string bookingid)
        {
            var model = new ResultBookingModel();
            using (SqlConnection db = new SqlConnection(_CON_STR))
            {
                if (db.State == ConnectionState.Closed) db.Open();
                SqlCommand cm = new SqlCommand("[sp_SCBooking]", db);
                cm.CommandType = CommandType.StoredProcedure;
                cm.Parameters.AddWithValue("@flag", "setStatusBooking");
                cm.Parameters.AddWithValue("@bookingid", bookingid);
                SqlDataReader dr = cm.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        model.bookingdetailind = dr["bookingdetailind"].ToString();
                        model.bookingid = dr["bookingid"].ToString();
                        model.voucherid = dr["voucherid"].ToString();
                        model.msg = dr["msg"].ToString();
                        model.result = dr["result"].ToString();
                    };
                }
                dr.Close();

            }
            return model;
        }
        #endregion
        #region "ดึงข้อมูล checkBookingdetail"
        public static List<ResultCheckBdModel> checkBookingdetail(BookingDetailModel value)
        {
            var list = new List<ResultCheckBdModel>();
            var strsubjectid = value.subjectid.Split('-');
            DateTime stDate = DateTime.ParseExact(value.studydate, "dd/MM/yyyy", new System.Globalization.CultureInfo("en-GB"));

            using (SqlConnection db = new SqlConnection(_CON_STR))
            {
                if (db.State == ConnectionState.Closed) db.Open();
                SqlCommand cm = new SqlCommand("[sp_SCBooking]", db);
                cm.CommandType = CommandType.StoredProcedure;
                cm.Parameters.AddWithValue("@flag", "checkBookingdetail");
                cm.Parameters.AddWithValue("@bookingid", value.bookingid);
                cm.Parameters.AddWithValue("@subjectid", strsubjectid[0]);
                cm.Parameters.AddWithValue("@teacherind", value.teacherind);
                cm.Parameters.AddWithValue("@studydate", stDate.ToString("yyyy-MM-dd", new System.Globalization.CultureInfo("en-GB")));
                cm.Parameters.AddWithValue("@studystime", value.studystime);
                cm.Parameters.AddWithValue("@studyetime", value.studyetime);
                cm.Parameters.AddWithValue("@bookingdetailind", value.bookingdetailind);
                SqlDataReader dr = cm.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        var model = new ResultCheckBdModel();
                        if (dr["result"].ToString() == "Y")
                        {
                            model.result = dr["result"].ToString();
                            model.msg = dr["msg"].ToString();
                            model.teacher = dr["teacher"].ToString();
                            model.subjectid = dr["subjectid"].ToString();
                        }
                        else
                        {
                            model.result = dr["result"].ToString();
                            model.msg = dr["msg"].ToString();
                            model.teacherind = dr["teacherind"].ToString();
                            model.subjectid = dr["subjectid"].ToString();
                            model.bookingid = dr["bookingid"].ToString();
                            model.ind = dr["ind"].ToString();
                            model.studydate = Convert.ToDateTime(dr["studydate"].ToString()).ToString("yyyy-MM-dd", new System.Globalization.CultureInfo("en-GB"));
                            model.studystime = Convert.ToDateTime(dr["studystime"].ToString()).ToString("HH:mm", new System.Globalization.CultureInfo("en-GB"));
                            model.studyetime = Convert.ToDateTime(dr["studyetime"].ToString()).ToString("HH:mm", new System.Globalization.CultureInfo("en-GB"));
                            model.subjectnickname = dr["subjectnickname"].ToString();
                            model.nickname = dr["nickname"].ToString();                                
                        }
                        list.Add(model);
                    };
                }
                dr.Close();

            }
            return list;
        }
        #endregion
        #region "ดึงข้อมูล DeteteBookingDetail"
        public static ResultBookingModel DeteteBookingDetail(string eventID, string bookingid)
        {
            var model = new ResultBookingModel();

            using (SqlConnection db = new SqlConnection(_CON_STR))
            {
                if (db.State == ConnectionState.Closed) db.Open();
                SqlCommand cm = new SqlCommand("[sp_SCBooking]", db);
                cm.CommandType = CommandType.StoredProcedure;
                cm.Parameters.AddWithValue("@flag", "DeteteBookingDetail");
                cm.Parameters.AddWithValue("@bookingdetailind", eventID);
                cm.Parameters.AddWithValue("@bookingid", bookingid);
                SqlDataReader dr = cm.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        model.result = dr["result"].ToString();
                        model.msg = dr["msg"].ToString();
                        model.bookingid = dr["bookingid"].ToString();
                        model.voucherid = dr["voucherid"].ToString();
                        model.bookingdetailind = dr["bookingdetailind"].ToString();
                    };
                }
                dr.Close();

            }
            return model;
        }
        #endregion
        #region "EditBooking"
        public static ResultBookingModel EditBooking(string bookingind, string bookingname, string remark, string username,string gear)
        {
            var model = new ResultBookingModel();
            using (SqlConnection db = new SqlConnection(_CON_STR))
            {
                if (db.State == ConnectionState.Closed) db.Open();
                SqlCommand cm = new SqlCommand("[sp_SCBooking]", db);
                cm.CommandType = CommandType.StoredProcedure;
                cm.Parameters.AddWithValue("@flag", "EditBooking");
                cm.Parameters.AddWithValue("@boookingname", bookingname);
                cm.Parameters.AddWithValue("@remark", remark);
                cm.Parameters.AddWithValue("@bookingid", bookingind);
                cm.Parameters.AddWithValue("@user", username);
                cm.Parameters.AddWithValue("@gear", gear);
                SqlDataReader dr = cm.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        model.bookingid = dr["bookingid"].ToString();
                        model.voucherid = dr["voucherid"].ToString();
                        model.msg = dr["msg"].ToString();
                        model.result = dr["result"].ToString();
                    };
                }
                dr.Close();

            }
            return model;
        }
        #endregion
        #region "SetconfirmBooking"
        public static ResultBookingModel SetconfirmBooking(string bookingid)
        {
            var model = new ResultBookingModel();
            using (SqlConnection db = new SqlConnection(_CON_STR))
            {
                if (db.State == ConnectionState.Closed) db.Open();
                SqlCommand cm = new SqlCommand("[sp_SCBooking]", db);
                cm.CommandType = CommandType.StoredProcedure;
                cm.Parameters.AddWithValue("@flag", "setconfirmBooking");
                cm.Parameters.AddWithValue("@bookingid", bookingid);
                SqlDataReader dr = cm.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        model.bookingdetailind = dr["bookingdetailind"].ToString();
                        model.bookingid = dr["bookingid"].ToString();
                        model.voucherid = dr["voucherid"].ToString();
                        model.msg = dr["msg"].ToString();
                        model.result = dr["result"].ToString();
                    };
                }
                dr.Close();

            }
            return model;
        }
        #endregion
        #region "BookingSetExamDate"
        public static ResultBookingModel BookingSetExamDate(string bookingexamdate, string bookingid)
        {
            string examdat = "";
            var model = new ResultBookingModel();
            if (bookingexamdate != "")
            {
                DateTime stDate = DateTime.ParseExact(bookingexamdate, "dd/MM/yyyy", new System.Globalization.CultureInfo("en-GB"));
                examdat = stDate.ToString("yyyy-MM-dd", new System.Globalization.CultureInfo("en-GB"));
            }
            using (SqlConnection db = new SqlConnection(_CON_STR))
            {
                if (db.State == ConnectionState.Closed) db.Open();
                SqlCommand cm = new SqlCommand("[sp_SCBooking]", db);
                cm.CommandType = CommandType.StoredProcedure;
                cm.Parameters.AddWithValue("@flag", "BookingSetExamDate");
                cm.Parameters.AddWithValue("@bookingid", bookingid);
                cm.Parameters.AddWithValue("@bookingexamdate", examdat);
                SqlDataReader dr = cm.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        model.bookingid = dr["bookingid"].ToString();
                        model.msg = dr["msg"].ToString();
                        model.result = dr["result"].ToString();
                    };
                }
                dr.Close();

            }
            return model;
        }
        #endregion
        #region "liststudybydate"
        public static List<ListstudyModel> liststudybydate(string level, string currentdate)
        {
            var list = new List<ListstudyModel>();
            using (SqlConnection db = new SqlConnection(_CON_STR))
            {
                if (db.State == ConnectionState.Closed) db.Open();
                SqlCommand cm = new SqlCommand("[sp_SCBooking]", db);
                cm.CommandType = CommandType.StoredProcedure;
                cm.Parameters.AddWithValue("@flag", "liststudybydate");
                cm.Parameters.AddWithValue("@level", level);
                cm.Parameters.AddWithValue("@currentdate", currentdate);
                SqlDataReader dr = cm.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        var model = new ListstudyModel();
                        model.subjectid = dr["subjectid"].ToString();
                        model.studydate = dr["studydate"].ToString();
                        model.studystime = dr["studystime"].ToString();
                        model.studyetime = dr["studyetime"].ToString();
                        model.subjectnickname = dr["subjectnickname"].ToString();
                        model.subjecttype = dr["subjecttype"].ToString();
                        model.coursenickname = dr["coursenickname"].ToString();
                        model.teachername = dr["teachername"].ToString();
                        model.studentname = dr["studentname"].ToString();
                        model.docno = dr["docno"].ToString();
                        list.Add(model);
                    };
                }
                dr.Close();

            }
            return list;
        }
        #endregion
        #region "liststudybysubjectid"
        public static ListstudyModel liststudybysubjectid(string level, string currentdate, string subjectid, string studystime, string studyetime)
        {
            var model = new ListstudyModel();
            using (SqlConnection db = new SqlConnection(_CON_STR))
            {
                if (db.State == ConnectionState.Closed) db.Open();
                SqlCommand cm = new SqlCommand("[sp_SCBooking]", db);
                cm.CommandType = CommandType.StoredProcedure;
                cm.Parameters.AddWithValue("@flag", "liststudybydate");
                cm.Parameters.AddWithValue("@level", level);
                cm.Parameters.AddWithValue("@currentdate", currentdate);
                cm.Parameters.AddWithValue("@subjectid", subjectid);
                cm.Parameters.AddWithValue("@studystime", studystime);
                cm.Parameters.AddWithValue("@studyetime", studyetime);
                SqlDataReader dr = cm.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        DateTime stDate = DateTime.Parse(dr["studydate"].ToString());
                        model.subjectid = dr["subjectid"].ToString();
                        model.studydate = stDate.ToString("dd/MM/yyyy", new System.Globalization.CultureInfo("en-GB"));
                        model.studystime = dr["studystime"].ToString();
                        model.studyetime = dr["studyetime"].ToString();
                        model.subjectnickname = dr["subjectnickname"].ToString();
                        model.subjecttype = dr["subjecttype"].ToString();
                        model.coursenickname = dr["coursenickname"].ToString();
                        model.teachername = dr["teachername"].ToString();
                        model.studentname = dr["studentname"].ToString();
                        model.docno = dr["docno"].ToString();                    };
                }
                dr.Close();

            }
            return model;
        }
        #endregion
        #region "ดึงข้อมูลเกียร์"
        public static List<lgearModel> GetGear()
        {
            var list = new List<lgearModel>();
            using (SqlConnection db = new SqlConnection(_CON_STR))
            {
                if (db.State == ConnectionState.Closed) db.Open();
                SqlCommand cm = new SqlCommand("[sp_SCLookup]", db);
                cm.CommandType = CommandType.StoredProcedure;
                cm.Parameters.AddWithValue("@flag", "lgear");
                SqlDataReader dr = cm.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        var model = new lgearModel();
                        model.gear = dr["gear"].ToString();
                        model.display = dr["display"].ToString();
                        list.Add(model);
                    };
                }
                dr.Close();

            }
            return list;
        }
        #endregion
        #region "ดึงข้อมูลCalendar"
        public static List<lgearModel> GetGearCalendar()
        {
            var list = new List<lgearModel>();
            using (SqlConnection db = new SqlConnection(_CON_STR))
            {
                if (db.State == ConnectionState.Closed) db.Open();
                SqlCommand cm = new SqlCommand("[sp_SCLookup]", db);
                cm.CommandType = CommandType.StoredProcedure;
                cm.Parameters.AddWithValue("@flag", "lgear");
                SqlDataReader dr = cm.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        var model = new lgearModel();
                        model.id = dr["gear"].ToString();
                        model.title = dr["display"].ToString();
                        list.Add(model);
                    };
                }
                dr.Close();

            }
            return list;
        }
        #endregion
        #region "ดึงข้อมูล workhours"
        public static WorkhoursModel workhours()
        {
            var model = new WorkhoursModel();
            using (SqlConnection db = new SqlConnection(_CON_STR))
            {
                if (db.State == ConnectionState.Closed) db.Open();
                SqlCommand cm = new SqlCommand("[sp_SCLookup]", db);
                cm.CommandType = CommandType.StoredProcedure;
                cm.Parameters.AddWithValue("@flag", "workhours");
                SqlDataReader dr = cm.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {

                        model.day = dr["day"].ToString();
                        model.stime = DateTime.Parse(dr["stime"].ToString()).ToString("HH:mm", new System.Globalization.CultureInfo("en-GB"));
                        model.etime = DateTime.Parse(dr["etime"].ToString()).ToString("HH:mm", new System.Globalization.CultureInfo("en-GB"));
                    };
                }
                dr.Close();

            }
            return model;
        }
        #endregion
    }
}
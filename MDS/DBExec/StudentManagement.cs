using MDS.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MDS.DBExec
{
    public class StudentManagement
    {
        public static string _CON_STR = ConfigurationManager.ConnectionStrings["MDS"].ConnectionString;

        #region "List หลักสูตร"
        public static List<CourseSelectModel> GetCourseList()
        {
            List<CourseSelectModel> SectionList = new List<CourseSelectModel>();
            using (SqlConnection db = new SqlConnection(_CON_STR))
            {
                SqlCommand cm = new SqlCommand("[sp_SCLookup]", db);
                cm.CommandType = CommandType.StoredProcedure;
                cm.Parameters.AddWithValue("@flag", "lcourse");
              
                if (db.State == ConnectionState.Closed) db.Open();
                SqlDataReader dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    SectionList.Add(new CourseSelectModel()
                    {
                        Text = dr["coursename"].ToString(),
                        Value = dr["courseid"].ToString(),
                        Group = dr["coursegroup"].ToString()
                    });
                }
            }
            return SectionList;
        }
        #endregion

        #region "List ประเทศผู้ออก Passport"
        public static List<SelectListItem> GetCountryList()
        {
            List<SelectListItem> SectionList = new List<SelectListItem>();
            using (SqlConnection db = new SqlConnection(_CON_STR))
            {
                SqlCommand cm = new SqlCommand("[sp_SCLookup]", db);
                cm.CommandType = CommandType.StoredProcedure;
                cm.Parameters.AddWithValue("@flag", "lcountry");

                if (db.State == ConnectionState.Closed) db.Open();
                SqlDataReader dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    SectionList.Add(new SelectListItem()
                    {
                        Text = dr["countryname"].ToString(),
                        Value = dr["countryabbr"].ToString()
                    });
                }
            }
            return SectionList;
        }
        #endregion

        #region "List คำนำหน้า TH"
        public static List<SelectListItem> GetTitleTHList()
        {
            List<SelectListItem> SectionList = new List<SelectListItem>();
            using (SqlConnection db = new SqlConnection(_CON_STR))
            {
                SqlCommand cm = new SqlCommand("[sp_SCLookup]", db);
                cm.CommandType = CommandType.StoredProcedure;
                cm.Parameters.AddWithValue("@flag", "ltitle");

                if (db.State == ConnectionState.Closed) db.Open();
                SqlDataReader dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    SectionList.Add(new SelectListItem()
                    {
                        Text = dr["ttitle"].ToString(),
                        Value = dr["ttitle"].ToString()
                    });
                }
            }
            return SectionList;
        }
        #endregion

        #region "List คำนำหน้า EN"
        public static List<SelectListItem> GetTitleENList()
        {
            List<SelectListItem> SectionList = new List<SelectListItem>();
            using (SqlConnection db = new SqlConnection(_CON_STR))
            {
                SqlCommand cm = new SqlCommand("[sp_SCLookup]", db);
                cm.CommandType = CommandType.StoredProcedure;
                cm.Parameters.AddWithValue("@flag", "ltitle");

                if (db.State == ConnectionState.Closed) db.Open();
                SqlDataReader dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    SectionList.Add(new SelectListItem()
                    {
                        Text = dr["etitle"].ToString(),
                        Value = dr["etitle"].ToString()
                    });
                }
            }
            return SectionList;
        }
        #endregion

        #region บันทึกข้อมูลนักเรียน
        public StudentModel Insert(StudentModel modelData)
        {
            StudentModel model = new StudentModel();
            using (SqlConnection db = new SqlConnection(_CON_STR))
            {
                SqlCommand cm = new SqlCommand("[sp_SetStudent]", db);
                cm.CommandType = CommandType.StoredProcedure;
                cm.Parameters.AddWithValue("@flag", "Add");
                cm.Parameters.AddWithValue("@StudentType", modelData.studenttype);
                cm.Parameters.AddWithValue("@courseid", modelData.courseid);
                cm.Parameters.AddWithValue("@IDCard", modelData.idcard);
                cm.Parameters.AddWithValue("@TitleT", modelData.titleT);
                cm.Parameters.AddWithValue("@NameT", modelData.nameT);
                cm.Parameters.AddWithValue("@SurNameT", modelData.surnameT);
                cm.Parameters.AddWithValue("@TitleE", modelData.titleE);
                cm.Parameters.AddWithValue("@NameE", modelData.nameE);
                cm.Parameters.AddWithValue("@SurNameE", modelData.surnameE);
                //string date = string.Empty;
                //string Examdate = string.Empty;
                //try
                //{
                //    //TH to EN
                //    DateTime strSendDate = DateTime.ParseExact(modelData.birthdate, "yyyy-mm-dd", new System.Globalization.CultureInfo("th-TH"));
                //    date = strSendDate.ToString("yyyy-mm-dd", new System.Globalization.CultureInfo("en-GB"));
                //    //TH to EN
                //    DateTime strExamdate = DateTime.ParseExact(modelData.examdate, "yyyy-mm-dd", new System.Globalization.CultureInfo("th-TH"));
                //    Examdate = strExamdate.ToString("yyyy-mm-dd", new System.Globalization.CultureInfo("en-GB"));

                //    ////En To En
                //    //DateTime strSendDate1 = DateTime.ParseExact("1994-03-07", "yyyy-mm-dd", new System.Globalization.CultureInfo("en-GB"));
                //    //var dateB = strSendDate1.ToString("yyyy-mm-dd", new System.Globalization.CultureInfo("en-GB"));

                //    ////En to TH
                //    //DateTime strSendDate2 = DateTime.ParseExact("1994-03-07", "yyyy-mm-dd", new System.Globalization.CultureInfo("en-GB"));
                //    //var dateC = strSendDate2.ToString("yyyy-mm-dd", new System.Globalization.CultureInfo("th-TH"));

                //}
                //catch (Exception ex) { }
                //cm.Parameters.AddWithValue("@examdate", Examdate);
                //cm.Parameters.AddWithValue("@BirthDate", date);
                cm.Parameters.AddWithValue("@examdate", modelData.examdate);
                cm.Parameters.AddWithValue("@channelid", modelData.channelid);
                cm.Parameters.AddWithValue("@BirthDate", modelData.birthdate);

                cm.Parameters.AddWithValue("@Age", modelData.age);
                cm.Parameters.AddWithValue("@Email", modelData.email);
                cm.Parameters.AddWithValue("@Nation", modelData.nation);
                cm.Parameters.AddWithValue("@Gentle", modelData.gentle);
                cm.Parameters.AddWithValue("@MobileNo", modelData.mobileno);
                cm.Parameters.AddWithValue("@Address", modelData.address);
                cm.Parameters.AddWithValue("@Tumbol", modelData.tumbol);
                cm.Parameters.AddWithValue("@Amphur", modelData.amphur);

                cm.Parameters.AddWithValue("@Changwat", modelData.changwat);
                cm.Parameters.AddWithValue("@ZipCode", modelData.zipcode);
                cm.Parameters.AddWithValue("@CardImg", modelData.cardimg);
                cm.Parameters.AddWithValue("@Disability", modelData.Disability);
                cm.Parameters.AddWithValue("@Disease", modelData.disease);
                cm.Parameters.AddWithValue("@DocumentNo", modelData.documentNo);
                cm.Parameters.AddWithValue("@PassportNo", modelData.passportNo);
                cm.Parameters.AddWithValue("@PassportCountry", modelData.passportCountry);
                cm.Parameters.AddWithValue("@PassportImg", modelData.passportimg);
                cm.Parameters.AddWithValue("@PassportImgFType", modelData.passportimgftype);
                cm.Parameters.AddWithValue("@IP", HttpContext.Current.Request.UserHostAddress); 
                cm.Parameters.AddWithValue("@branchid", modelData.branchid);
                cm.Parameters.AddWithValue("@User", modelData.User);
                cm.Parameters.AddWithValue("@bookingid", modelData.bookingno);

                if (db.State == ConnectionState.Closed) db.Open();
                SqlDataReader dr = cm.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    model.Result = dr["result"].ToString();
                    model.Message = dr["msg"].ToString();
                    model.studentind = dr["ind"].ToString();
                    model.voucherid = dr["voucherid"].ToString();
                    dr.Close();
                }
            }   
                return model;
        }
        #endregion


        #region แก้ไขข้อมูลนักเรียน
        public StudentModel Edit(StudentModel modelData)
        {
            StudentModel model = new StudentModel();
            using (SqlConnection db = new SqlConnection(_CON_STR))
            {
                SqlCommand cm = new SqlCommand("[sp_SetStudent]", db);
                cm.CommandType = CommandType.StoredProcedure;
                cm.Parameters.AddWithValue("@flag", "Edit");
                cm.Parameters.AddWithValue("@ind", modelData.studentind);
                cm.Parameters.AddWithValue("@StudentType", modelData.studenttype);
                cm.Parameters.AddWithValue("@courseid", modelData.courseid);
                cm.Parameters.AddWithValue("@IDCard", modelData.idcard);
                cm.Parameters.AddWithValue("@TitleT", modelData.titleT);
                cm.Parameters.AddWithValue("@NameT", modelData.nameT);
                cm.Parameters.AddWithValue("@SurNameT", modelData.surnameT);
                cm.Parameters.AddWithValue("@TitleE", modelData.titleE);
                cm.Parameters.AddWithValue("@NameE", modelData.nameE);
                cm.Parameters.AddWithValue("@SurNameE", modelData.surnameE);
                cm.Parameters.AddWithValue("@examdate", modelData.examdate);
                cm.Parameters.AddWithValue("@BirthDate", modelData.birthdate);
                cm.Parameters.AddWithValue("@channelid", modelData.channelid); 
                //string date = string.Empty;
                //string Examdate = string.Empty;
                //try
                //{
                //    //TH to EN
                //    DateTime strSendDate = DateTime.ParseExact(modelData.birthdate, "yyyy-mm-dd", new System.Globalization.CultureInfo("th-TH"));
                //    date = strSendDate.ToString("yyyy-mm-dd", new System.Globalization.CultureInfo("en-GB"));
                //    //TH to EN
                //    DateTime strExamdate = DateTime.ParseExact(modelData.examdate, "yyyy-mm-dd", new System.Globalization.CultureInfo("th-TH"));
                //    Examdate = strExamdate.ToString("yyyy-mm-dd", new System.Globalization.CultureInfo("en-GB"));
                //}
                //catch (Exception ex) { }
                //cm.Parameters.AddWithValue("@examdate", Examdate);
                //cm.Parameters.AddWithValue("@BirthDate", date);
                cm.Parameters.AddWithValue("@Age", modelData.age);
                cm.Parameters.AddWithValue("@Email", modelData.email);
                cm.Parameters.AddWithValue("@Nation", modelData.nation);
                cm.Parameters.AddWithValue("@Gentle", modelData.gentle);
                cm.Parameters.AddWithValue("@MobileNo", modelData.mobileno);
                cm.Parameters.AddWithValue("@Address", modelData.address);
                cm.Parameters.AddWithValue("@Tumbol", modelData.tumbol);
                cm.Parameters.AddWithValue("@Amphur", modelData.amphur);

                cm.Parameters.AddWithValue("@Changwat", modelData.changwat);
                cm.Parameters.AddWithValue("@ZipCode", modelData.zipcode);
                cm.Parameters.AddWithValue("@CardImg", modelData.cardimg);
                cm.Parameters.AddWithValue("@Disability", modelData.Disability);
                cm.Parameters.AddWithValue("@Disease", modelData.disease);
                cm.Parameters.AddWithValue("@DocumentNo", modelData.documentNo);
                cm.Parameters.AddWithValue("@PassportNo", modelData.passportNo);
                cm.Parameters.AddWithValue("@PassportCountry", modelData.passportCountry);
                cm.Parameters.AddWithValue("@PassportImg", modelData.passportimg);
                cm.Parameters.AddWithValue("@PassportImgFType", modelData.passportimgftype);
                cm.Parameters.AddWithValue("@IP", HttpContext.Current.Request.UserHostAddress);
                cm.Parameters.AddWithValue("@branchid", modelData.branchid);
                cm.Parameters.AddWithValue("@User", modelData.User);

                if (db.State == ConnectionState.Closed) db.Open();
                SqlDataReader dr = cm.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    model.Result = dr["result"].ToString();
                    model.Message = dr["msg"].ToString();
                    dr.Close();
                }
            }
            return model;
        }
        #endregion

        #region ดึงข้อมูลนักเรียน By Id
        public StudentModel GetStudentById(StudentModel model)
        {
            using (SqlConnection db = new SqlConnection(_CON_STR))
            {
                model.ListOfTableStudent = new List<TableStudentModel>();
                model.ListOfPretest = new List<PretestModel>();
                SqlCommand cm = new SqlCommand("[sp_SetStudent]", db);
                cm.Parameters.AddWithValue("@flag", "getStudentById");
                cm.Parameters.AddWithValue("@IP", HttpContext.Current.Request.UserHostAddress);
                cm.Parameters.AddWithValue("@ind ", model.studentind);
                cm.CommandType = CommandType.StoredProcedure;
                if (db.State == ConnectionState.Closed) db.Open();
                SqlDataReader dr = cm.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        model.bookingno = dr["bookingno"].ToString();
                        model.voucherno = dr["voucherno"].ToString();
                        model.studentind = dr["ind"].ToString();
                        model.studenttype = dr["studenttype"].ToString();
                        model.idcard = dr["idcard"].ToString();
                        model.titleT = dr["titleT"].ToString();
                        model.nameT = dr["nameT"].ToString();
                        model.surnameT = dr["surnameT"].ToString();
                        model.titleE = dr["titleE"].ToString();
                        model.nameE = dr["nameE"].ToString();
                        model.surnameE = dr["surnameE"].ToString();

                        model.examdate = dr["examdate"].ToString();
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
                        model.Disability = dr["Disability"].ToString();
                        model.disease = dr["disease"].ToString();
                        model.courseid = dr["courseid"].ToString();
                        model.coursename = dr["coursename"].ToString();
                        model.coursegroup = dr["coursegroup"].ToString();
                        model.documentNo = dr["documentNo"].ToString();
                        model.passportNo = dr["passportNo"].ToString();
                        model.passportCountry = dr["passportCountry"].ToString();
                        model.passportimg = dr["passportimg"].ToString();
                        model.passportimgftype = dr["passportimgftype"].ToString();
                        model.courseprice = dr["courseprice"].ToString();
                        model.Header1 = dr["Header1"].ToString();
                        model.Header2 = dr["Header2"].ToString();
                        model.qrurl = dr["qrurl"].ToString();
                        model.remark = dr["remark"].ToString();
                        model.channelid = dr["channelid"].ToString();
                        model.channelname = dr["channelname"].ToString();
                    }
                    dr.NextResult();
                    while (dr.Read())
                    {
                        var modelTable = new TableStudentModel()
                        {
                            studentname = dr["studentname"].ToString(),
                            mobileno = dr["mobileno"].ToString(),
                            coursegroupname = dr["coursegroupname"].ToString(),
                            coursenickname = dr["coursenickname"].ToString(),
                            coursename = dr["coursename"].ToString(),
                            studydate = dr["studydate"].ToString(),
                            studystime = dr["studystime"].ToString(),
                            studyetime = dr["studyetime"].ToString(),
                            subjecttype = dr["subjecttype"].ToString(),
                            subjecttypedesc = dr["subjecttypedesc"].ToString(),
                            subjectname = dr["subjectname"].ToString(),
                            subjectnickname = dr["subjectnickname"].ToString(),
                            nickname = dr["nickname"].ToString()
                        };
                        model.ListOfTableStudent.Add(modelTable);
                    }
                    dr.NextResult();
                    while (dr.Read())
                    {
                        var modelPreTest = new PretestModel()
                        {
                            examdate = dr["examdate"].ToString(),
                            finishdate = dr["finishdate"].ToString(),
                            examscore = dr["examscore"].ToString(),
                            result = dr["result"].ToString(),
                            style = dr["style"].ToString()
                        };
                        model.ListOfPretest.Add(modelPreTest);
                    }
                    dr.Close();
                }
                return model;
            }
        }
        #endregion
        
        #region ข้อมูล StudentList
        public List<ManageStudentModel> ListStudent()
        {
            List<ManageStudentModel> List = new List<ManageStudentModel>();
            using (SqlConnection db = new SqlConnection(_CON_STR))
            { 
                SqlCommand cm = new SqlCommand("[sp_SetStudent]", db);
                cm.Parameters.AddWithValue("@flag", "ListStudent");
                cm.Parameters.AddWithValue("@IP", HttpContext.Current.Request.UserHostAddress);
                cm.CommandType = CommandType.StoredProcedure;
                if (db.State == ConnectionState.Closed) db.Open();
                SqlDataReader dr = cm.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        var model = new ManageStudentModel()
                        {
                            bookingno = dr["bookingno"].ToString(),
                            voucherno = dr["voucherno"].ToString(),
                            studentind = dr["studentind"].ToString(),
                            studenttype = dr["studenttype"].ToString(),
                            cardimg = dr["cardimg"].ToString(),
                            studentname = dr["studentname"].ToString(),
                            regisdate = dr["regisdate"].ToString(),
                            course = dr["course"].ToString(),
                            courseicon = dr["courseicon"].ToString(),
                            courseamount = dr["courseamount"].ToString(),
                            idcard = dr["idcard"].ToString(),
                            age = dr["age"].ToString(),
                            nation = dr["nation"].ToString(),
                            mobileno = dr["mobileno"].ToString(),
                            examdate = dr["examdate"].ToString(),
                            status = dr["status"].ToString(),
                            studytable = dr["studytable"].ToString(),
                            moneystatus = dr["moneystatus"].ToString(),
                            moneyremain = dr["moneyremain"].ToString(),
                            pretest = dr["pretest"].ToString(),
                            pretestcount = dr["pretestcount"].ToString(),
                            coursename = dr["coursename"].ToString(),
                            courseiconcolor = dr["courseiconcolor"].ToString(),
                            branchid = dr["branchid"].ToString(),
                            updateby = dr["updateby"].ToString(),
                            uname = dr["uname"].ToString()
                            
                        };
                        List.Add(model);
                    }
             }
                return List;
            }

         }
        #endregion

        #region "ListStudent Filter"
        public static List<SelectListItem> GetFilterList()
        {
            List<SelectListItem> SectionList = new List<SelectListItem>();
            using (SqlConnection db = new SqlConnection(_CON_STR))
            {
                SqlCommand cm = new SqlCommand("[sp_SetStudent_Filter]", db);
                cm.CommandType = CommandType.StoredProcedure;
                cm.Parameters.AddWithValue("@flag", "ListFilter");

                if (db.State == ConnectionState.Closed) db.Open();
                SqlDataReader dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    SectionList.Add(new SelectListItem()
                    {
                        Text = dr["filtername"].ToString(),
                        Value = dr["filterno"].ToString()
                    });
                }
            }
            return SectionList;
        }
        #endregion

        #region ข้อมูล StudentList Filter
        public List<ManageStudentModel> ListStudentFilter(string filterno)
        {
            List<ManageStudentModel> List = new List<ManageStudentModel>();
            using (SqlConnection db = new SqlConnection(_CON_STR))
            {
                SqlCommand cm = new SqlCommand("[sp_SetStudent_Filter]", db);
                cm.Parameters.AddWithValue("@flag", "FilterStudent");
                cm.Parameters.AddWithValue("@filterno", filterno);
                cm.CommandType = CommandType.StoredProcedure;
                if (db.State == ConnectionState.Closed) db.Open();
                SqlDataReader dr = cm.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        var model = new ManageStudentModel()
                        {
                            bookingno = dr["bookingno"].ToString(),
                            voucherno = dr["voucherno"].ToString(),
                            studentind = dr["studentind"].ToString(),
                            studenttype = dr["studenttype"].ToString(),
                            cardimg = dr["cardimg"].ToString(),
                            studentname = dr["studentname"].ToString(),
                            regisdate = dr["regisdate"].ToString(),
                            course = dr["course"].ToString(),
                            courseicon = dr["courseicon"].ToString(),
                            courseamount = dr["courseamount"].ToString(),
                            idcard = dr["idcard"].ToString(),
                            age = dr["age"].ToString(),
                            nation = dr["nation"].ToString(),
                            mobileno = dr["mobileno"].ToString(),
                            examdate = dr["examdate"].ToString(),
                            status = dr["status"].ToString(),
                            studytable = dr["studytable"].ToString(),
                            moneystatus = dr["moneystatus"].ToString(),
                            moneyremain = dr["moneyremain"].ToString(),
                            pretest = dr["pretest"].ToString(),
                            pretestcount = dr["pretestcount"].ToString(),
                            coursename = dr["coursename"].ToString(),
                            courseiconcolor = dr["courseiconcolor"].ToString(),
                            branchid = dr["branchid"].ToString(),
                            updateby = dr["updateby"].ToString(),
                            uname = dr["uname"].ToString(),
                            Qrurl = dr["qrurl"].ToString()

                        };
                        List.Add(model);
                    }
                }
                return List;
            }

        }
        #endregion

        #region "ลบข้อมูล DeleteStudent"
        public ResultModel Delete(string Studentind, string User)
        {
            var model = new ResultModel();
            using (SqlConnection db = new SqlConnection(_CON_STR))
            {
                if (db.State == ConnectionState.Closed) db.Open();
                SqlCommand cm = new SqlCommand("[sp_SetStudent]", db);
                cm.CommandType = CommandType.StoredProcedure;
                cm.Parameters.AddWithValue("@flag", "Cancel");
                cm.Parameters.AddWithValue("@ind", Studentind);
                cm.Parameters.AddWithValue("@User", User);
                cm.Parameters.AddWithValue("@IP", HttpContext.Current.Request.UserHostAddress);
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

        #region "ลบข้อมูล Re-DeleteStudent"
        public ResultModel ReDelete(string Studentind, string User)
        {
            var model = new ResultModel();
            using (SqlConnection db = new SqlConnection(_CON_STR))
            {
                if (db.State == ConnectionState.Closed) db.Open();
                SqlCommand cm = new SqlCommand("[sp_SetStudent]", db);
                cm.CommandType = CommandType.StoredProcedure;
                cm.Parameters.AddWithValue("@flag", "Re-Cancel");
                cm.Parameters.AddWithValue("@ind", Studentind);
                cm.Parameters.AddWithValue("@User", User);
                cm.Parameters.AddWithValue("@IP", HttpContext.Current.Request.UserHostAddress);
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

        #region "List ประเภทของค่ารับเงิน"
        public static List<SelectListItem> GetReceiveList()
        {
            List<SelectListItem> SectionList = new List<SelectListItem>();
            using (SqlConnection db = new SqlConnection(_CON_STR))
            {
                SqlCommand cm = new SqlCommand("[sp_SCLookup]", db);
                cm.CommandType = CommandType.StoredProcedure;
                cm.Parameters.AddWithValue("@flag", "lrc");

                if (db.State == ConnectionState.Closed) db.Open();
                SqlDataReader dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    SectionList.Add(new SelectListItem()
                    {
                        Text = dr["rcname"].ToString(),
                        Value = dr["rcid"].ToString()
                    });
                }
            }
            return SectionList;
        }
        #endregion

        #region "List บัตรเครดิต"
        public static List<SelectListItem> GetReceiveCardList()
        {
            List<SelectListItem> SectionList = new List<SelectListItem>();
            using (SqlConnection db = new SqlConnection(_CON_STR))
            {
                SqlCommand cm = new SqlCommand("[sp_SCLookup]", db);
                cm.CommandType = CommandType.StoredProcedure;
                cm.Parameters.AddWithValue("@flag", "lcrcard");

                if (db.State == ConnectionState.Closed) db.Open();
                SqlDataReader dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    SectionList.Add(new SelectListItem()
                    {
                        Text = dr["displayname"].ToString(),
                        Value = dr["cid"].ToString()
                    });
                }
            }
            return SectionList;
        }
        #endregion

        #region "List บัญชีรับเงินโอน"
        public static List<SelectListItem> GetBankaccountList()
        {
            List<SelectListItem> SectionList = new List<SelectListItem>();
            using (SqlConnection db = new SqlConnection(_CON_STR))
            {
                SqlCommand cm = new SqlCommand("[sp_SCLookup]", db);
                cm.CommandType = CommandType.StoredProcedure;
                cm.Parameters.AddWithValue("@flag", "lbankaccount");

                if (db.State == ConnectionState.Closed) db.Open();
                SqlDataReader dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    SectionList.Add(new SelectListItem()
                    {
                        Text = dr["accountname"].ToString(),
                        Value = dr["accountno"].ToString()
                    });
                }
            }
            return SectionList;
        }
        #endregion

        #region "List ชื่อธนาคาร"
        public static List<SelectListItem> GetBankList()
        {
            List<SelectListItem> SectionList = new List<SelectListItem>();
            using (SqlConnection db = new SqlConnection(_CON_STR))
            {
                SqlCommand cm = new SqlCommand("[sp_SCLookup]", db);
                cm.CommandType = CommandType.StoredProcedure;
                cm.Parameters.AddWithValue("@flag", "lbank");

                if (db.State == ConnectionState.Closed) db.Open();
                SqlDataReader dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    SectionList.Add(new SelectListItem()
                    {
                        Text = dr["bankname"].ToString(),
                        Value = dr["bankabbr"].ToString()
                    });
                }
            }
            return SectionList;
        }
        #endregion


        #region "ดึงข้อมูล AddBooking"
        public static ResultBookingModel AddBooking(StudentModel modelData)
        {
            //string courseid,string studentind, string user, string coursetime, string branchid
            var model = new ResultBookingModel();
            using (SqlConnection db = new SqlConnection(_CON_STR))
            {
                if (db.State == ConnectionState.Closed) db.Open();
                SqlCommand cm = new SqlCommand("[sp_SCBooking]", db);
                cm.CommandType = CommandType.StoredProcedure;
                cm.Parameters.AddWithValue("@flag", "AddBooking");
                cm.Parameters.AddWithValue("@courseid", modelData.courseid);
                cm.Parameters.AddWithValue("@studentind", modelData.studentind);
                cm.Parameters.AddWithValue("@voucherid", modelData.voucherid);
                cm.Parameters.AddWithValue("@remark", "");
                cm.Parameters.AddWithValue("@user", modelData.User);
                cm.Parameters.AddWithValue("@ip", HttpContext.Current.Request.UserHostAddress);
                cm.Parameters.AddWithValue("@coursetime", modelData.coursetime);
                cm.Parameters.AddWithValue("@branchid", modelData.branchid);
                if (modelData.nameT != "")
                {
                    cm.Parameters.AddWithValue("@boookingname","จอง"+modelData.nameT + " " + modelData.surnameT);
                }
                else
                {
                    cm.Parameters.AddWithValue("@boookingname","จอง"+ modelData.nameE + " " + modelData.surnameE);
                }
                
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

        //exec sp_SetStudent @flag='listTempbooking',@user='khuan',@ip='',@courseid=1
        #region "ดึงข้อมูล ListTempBooking"
        public static List<StudentBookingModel> ListTempBooking(StudentBookingModel model)
        {
            List<StudentBookingModel> res = new List<StudentBookingModel>();
            using (SqlConnection db = new SqlConnection(_CON_STR))
            {
                if (db.State == ConnectionState.Closed) db.Open();
                SqlCommand cm = new SqlCommand("[sp_SetStudent]", db);
                cm.CommandType = CommandType.StoredProcedure;
                cm.Parameters.AddWithValue("@flag", "listTempbooking");
                cm.Parameters.AddWithValue("@courseid", model.courseid);
                cm.Parameters.AddWithValue("@user", model.user);
                cm.Parameters.AddWithValue("@ip", HttpContext.Current.Request.UserHostAddress);
                
                SqlDataReader dr = cm.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        var modeldata = new StudentBookingModel() {
                            bookingid = dr["bookingid"].ToString(),
                            bookingname = dr["bookingname"].ToString(),
                            courseid = dr["courseid"].ToString(),
                            coursename = dr["coursename"].ToString(),
                            coursenickname = dr["coursenickname"].ToString(),
                            insertby = dr["insertby"].ToString(),
                            insertdate = dr["insertdate"].ToString()
                        };
                        res.Add(modeldata);
                    };
                }
                dr.Close();

            }
            return res;
        }
        #endregion

        #region "AssignBooking"
        public static StudentBookingModel AssignBooking(StudentBookingModel model)
        {
            StudentBookingModel res = new StudentBookingModel();
            using (SqlConnection db = new SqlConnection(_CON_STR))
            {
                if (db.State == ConnectionState.Closed) db.Open();
                SqlCommand cm = new SqlCommand("[sp_SetStudent]", db);
                cm.CommandType = CommandType.StoredProcedure;
                cm.Parameters.AddWithValue("@flag", "assignBooking");
                cm.Parameters.AddWithValue("@bookingid", model.bookingid);
                cm.Parameters.AddWithValue("@ind", model.studentind);
                cm.Parameters.AddWithValue("@user", model.user);
                cm.Parameters.AddWithValue("@ip", HttpContext.Current.Request.UserHostAddress);

                SqlDataReader dr = cm.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    res.bookingid = dr["bookingid"].ToString();
                    res.voucherid = dr["voucherid"].ToString();
                    res.studentind = dr["ind"].ToString();
                    res.msg = dr["msg"].ToString();
                    res.result = dr["result"].ToString();
                    dr.Close();
                }
            }
            return res;
        }
        #endregion

        #region "GetCouseById"
        /// <summary>
        /// ดึง CouseTime
        /// </summary>
        /// <param name="couseid"></param>
        /// <returns>cousetime</returns>
        public static string GetCouseById(string cid)
        {
            string coursetime = string.Empty;
            using (SqlConnection db = new SqlConnection(_CON_STR))
            {
                if (db.State == ConnectionState.Closed) db.Open();
                SqlCommand cm = new SqlCommand("[sp_GetSCCourse]", db);
                cm.CommandType = CommandType.StoredProcedure;
                cm.Parameters.AddWithValue("@flag", "GetByID");
                cm.Parameters.AddWithValue("@cid", cid);
                SqlDataReader dr = cm.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    coursetime = dr["coursetime"].ToString();
                    dr.Close();
                }
            }
            return coursetime;
        }
        #endregion

        #region "List ช่องทาง"
        public static List<SelectListItem> GetIChannelList()
        {
            List<SelectListItem> SectionList = new List<SelectListItem>();
            using (SqlConnection db = new SqlConnection(_CON_STR))
            {
                SqlCommand cm = new SqlCommand("[sp_SCLookup]", db);
                cm.CommandType = CommandType.StoredProcedure;
                cm.Parameters.AddWithValue("@flag", "lchannel");

                if (db.State == ConnectionState.Closed) db.Open();
                SqlDataReader dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    SectionList.Add(new SelectListItem()
                    {
                        Text = dr["channelname"].ToString(),
                        Value = dr["channelid"].ToString()
                    });
                }
            }
            return SectionList;
        }
        #endregion
    }
}
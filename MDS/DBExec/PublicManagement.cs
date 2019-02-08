using MDS.Models;
using QRCoder;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MDS.DBExec
{
    public class PublicManagement
    {
        public static string _CON_STR = ConfigurationManager.ConnectionStrings["MDS"].ConnectionString;

        #region "GenQRcode"
        public string Image(string url)
        {
            string code = "http://163.44.197.45/MDS/Public/Student?id=0x01000000738E62A713B7686C52B39E2BFFBDA0088173B423D43F2FA7";
            code = url;
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(code, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            System.Web.UI.WebControls.Image imgBarCode = new System.Web.UI.WebControls.Image();
            imgBarCode.Height = 150;
            imgBarCode.Width = 150;
            byte[] byteImage;
            using (Bitmap bitMap = qrCode.GetGraphic(21, Color.Black, Color.White, (Bitmap)Bitmap.FromFile(HttpContext.Current.Server.MapPath("~/MDSMiddleFile/DefaultMediaIMG/LOGO.png"))))
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    bitMap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                    byteImage = ms.ToArray();
                    imgBarCode.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(byteImage);
                }
            }
            return imgBarCode.ImageUrl;
        }
        #endregion

        #region ดึงข้อมูลนักเรียนById Public
        public StudentModel GetStudentById(string studentid)
        {
            using (SqlConnection db = new SqlConnection(_CON_STR))
            {
                StudentModel model = new StudentModel();
                model.ListOfTableStudent = new List<TableStudentModel>();
                model.ListOfReceipt = new PaymentModel();
                model.ListPayment = new List<PaymentModel>();
                model.ListOfPretest = new List<PretestModel>();
                SqlCommand cm = new SqlCommand("[sp_QrcodePublic]", db);
                cm.Parameters.AddWithValue("@flag", "getStudent");
                cm.Parameters.AddWithValue("@currentdatetime", DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fff",CultureInfo.InvariantCulture));
                //cm.Parameters.AddWithValue("@currentdatetime", "2019-01-18 09:50:53.673");
                cm.Parameters.AddWithValue("@studentind ", studentid);
                cm.CommandType = CommandType.StoredProcedure;
                if (db.State == ConnectionState.Closed) db.Open();
                SqlDataReader dr = cm.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        model.Result = dr["result"].ToString();
                        model.Message = dr["msg"].ToString();
                    }
                    dr.NextResult();
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
                            nickname = dr["nickname"].ToString(),
                            teachername = dr["teachername"].ToString(),
                            style = dr["style"].ToString(),
                            cardimg = dr["cardimg"].ToString()
                        };
                        model.ListOfTableStudent.Add(modelTable);
                    }
                    dr.NextResult();
                    while (dr.Read())
                    {
                        var modelPayment = new PaymentModel()
                        {
                            rno = dr["rno"].ToString(),
                            payno = dr["payno"].ToString(),
                            payid = dr["payid"].ToString(),
                            paydate = dr["paydate"].ToString(),
                            keydate = dr["keydate"].ToString(),
                            detail = dr["detail"].ToString(),
                            amount = dr["amount"].ToString(),
                            remark = dr["remark"].ToString(),
                            status = dr["status"].ToString()
                        };
                        model.ListPayment.Add(modelPayment);
                    }
                    dr.NextResult();
                    while (dr.Read())
                    {
                        var modelReceipt = new PaymentModel()
                        {
                            receiptno = dr["receiptno"].ToString(),
                            remain = dr["remain"].ToString(),
                            courseprice = dr["courseprice"].ToString()
                        };
                        model.ListOfReceipt = modelReceipt;
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

        #region "List ภาษา"
        public static List<SelectListItem> GetLanguage()
        {
            List<SelectListItem> SectionList = new List<SelectListItem>();
            using (SqlConnection db = new SqlConnection(_CON_STR))
            {
                SqlCommand cm = new SqlCommand("[sp_SCLookup]", db);
                cm.CommandType = CommandType.StoredProcedure;
                cm.Parameters.AddWithValue("@flag", "llanguage");

                if (db.State == ConnectionState.Closed) db.Open();
                SqlDataReader dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    SectionList.Add(new SelectListItem()
                    {
                        Text = dr["langdesc"].ToString(),
                        Value = dr["langid"].ToString()
                    });
                }
            }
            return SectionList;
        }
        #endregion

        #region "ดึงภาษาส่ง ภาษาเข้าข้อสอบ"
        /// <summary>
        /// ฟังก์ชั่นดึงชุดข้อสอบ
        /// </summary>
        /// <param name="studentid"></param>
        /// <param name="Lang"></param>
        /// <returns></returns>
        public static string GetExam(string studentid,string Lang)
        {
            string Examid = string.Empty;
            using (SqlConnection db = new SqlConnection(_CON_STR))
            {
                SqlCommand cm = new SqlCommand("[sp_SCExam]", db);
                cm.CommandType = CommandType.StoredProcedure;
                cm.Parameters.AddWithValue("@flag", "getExam");
                cm.Parameters.AddWithValue("@studentind", studentid);
                cm.Parameters.AddWithValue("@langid", Lang);
                if (db.State == ConnectionState.Closed) db.Open();
                SqlDataReader dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    Examid = dr["examid"].ToString();
                }
            }
            return Examid;
        }
        #endregion

        #region "ดึงภาษาส่ง ภาษาเข้าข้อสอบ"
        /// <summary>
        /// ฟังก์ชั่นดึงภาษาเข้าข้อสอบ
        /// </summary>
        /// <param name="studentid"></param>
        /// <param name="Lang"></param>
        /// <returns></returns>
        public static SessionCompanyModel GetCompany(SessionCompanyModel model)
        {
            using (SqlConnection db = new SqlConnection(_CON_STR))
            {
                SqlCommand cm = new SqlCommand("[sp_SetCompany]", db);
                cm.CommandType = CommandType.StoredProcedure;
                cm.Parameters.AddWithValue("@flag", "getCompanyInfo");
                cm.Parameters.AddWithValue("@ind", model.ind);
                cm.Parameters.AddWithValue("@langid", model.langid);
                if (db.State == ConnectionState.Closed) db.Open();
                SqlDataReader dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    model.ind = dr["ind"].ToString();
                    model.schoollogo = dr["schoollogo"].ToString();
                    model.schoolname = dr["schoolname"].ToString();
                    model.schooladdr1 = dr["schooladdr1"].ToString();
                    model.schooladdr2 = dr["schooladdr2"].ToString();
                    model.schooladdr3 = dr["schooladdr3"].ToString();
                    model.footer = dr["footer"].ToString();

                }
            }
            return model;
        }
        #endregion


        #region ดึงข้อมูลครูById Public
        public TeacherModel GetTeacherById(TeacherModel model, string id)
        {
            DateTime today = DateTime.Today;
            DateTime today3 = DateTime.Today.AddDays(+3);
            //var now = today.ToString("yyyy/MM/dd", new System.Globalization.CultureInfo("en-GB"));
            //var future = today.ToString("yyyy/MM/dd", new System.Globalization.CultureInfo("en-GB"));
            using (SqlConnection db = new SqlConnection(_CON_STR))
            {
                model.ListOfTableTeacher = new List<TableTeacherModel>();
                SqlCommand cm = new SqlCommand("[sp_QrcodePublic]", db);
                cm.Parameters.AddWithValue("@flag", "getTeacher");
                //cm.Parameters.AddWithValue("@IP", HttpContext.Current.Request.UserHostAddress);
                cm.Parameters.AddWithValue("@teacherind ", id);
                //cm.Parameters.AddWithValue("@fdate ", now);
                //cm.Parameters.AddWithValue("@tdate ", future);
                cm.CommandType = CommandType.StoredProcedure;
                if (db.State == ConnectionState.Closed) db.Open();
                SqlDataReader dr = cm.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        model.result = dr["result"].ToString();
                        model.msg = dr["msg"].ToString();
                    }
                    dr.NextResult();
                    while (dr.Read())
                    {
                        var modelTable = new TableTeacherModel()
                        {
                            studentname = dr["studentname"].ToString(),
                            mobileno = dr["mobileno"].ToString(),
                            coursenickname = dr["coursenickname"].ToString(),
                            //studydate = dr["studydate"].ToString(),
                            studydate = Convert.ToDateTime(dr["studydate"].ToString()).ToString("dd/MM/yyyy", new System.Globalization.CultureInfo("en-GB")),
                            //studystime = dr["studystime"].ToString(),
                            //studyetime = dr["studyetime"].ToString(),
                            studystime = Convert.ToDateTime(dr["studystime"].ToString()).ToString("HH:ss"),
                            studyetime = Convert.ToDateTime(dr["studyetime"].ToString()).ToString("HH:ss"),
                            subjecttypedesc = dr["subjecttypedesc"].ToString(),
                            subjectnickname = dr["subjectnickname"].ToString(),
                            ind = dr["ind"].ToString(),
                            teacherind = dr["teacherind"].ToString(),

                            cardimg = dr["cardimg"].ToString(),
                            teacherName = dr["teachername"].ToString(),
                            tnickname = dr["nickname"].ToString(),
                            teacherimg = dr["teacherimg"].ToString()
                        };
                        model.ListOfTableTeacher.Add(modelTable);
                    }
                    dr.Close();
                }
                return model;
            }
        }
        #endregion

        #region ดึงข้อมูลครูById, Date Public
        public TeacherModel GetTeacherByDate(TeacherModel model, string id, string ugmData, string ugmData2)
        {
            using (SqlConnection db = new SqlConnection(_CON_STR))
            {
                model.ListOfTableTeacher = new List<TableTeacherModel>();
                SqlCommand cm = new SqlCommand("[sp_QrcodePublic]", db);
                cm.Parameters.AddWithValue("@flag", "getTeacher");
                //cm.Parameters.AddWithValue("@IP", HttpContext.Current.Request.UserHostAddress);
                cm.Parameters.AddWithValue("@teacherind ", id);
                cm.Parameters.AddWithValue("@fdate ", ugmData);
                cm.Parameters.AddWithValue("@tdate ", ugmData2);
                cm.CommandType = CommandType.StoredProcedure;
                if (db.State == ConnectionState.Closed) db.Open();
                SqlDataReader dr = cm.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        model.result = dr["result"].ToString();
                        model.msg = dr["msg"].ToString();
                    }
                    dr.NextResult();
                    while (dr.Read())
                    {
                        var modelTable = new TableTeacherModel()
                        {
                            studentname = dr["studentname"].ToString(),
                            mobileno = dr["mobileno"].ToString(),
                            coursenickname = dr["coursenickname"].ToString(),
                            //studydate = dr["studydate"].ToString(),
                            studydate = Convert.ToDateTime(dr["studydate"].ToString()).ToString("dd/MM/yyyy", new System.Globalization.CultureInfo("en-GB")),
                            //studystime = dr["studystime"].ToString(),
                            //studyetime = dr["studyetime"].ToString(),
                            studystime = Convert.ToDateTime(dr["studystime"].ToString()).ToString("HH:ss"),
                            studyetime = Convert.ToDateTime(dr["studyetime"].ToString()).ToString("HH:ss"),
                            subjecttypedesc = dr["subjecttypedesc"].ToString(),
                            subjectnickname = dr["subjectnickname"].ToString(),
                            ind = dr["ind"].ToString(),

                            cardimg = dr["cardimg"].ToString(),
                            teacherName = dr["teachername"].ToString(),
                            tnickname = dr["nickname"].ToString(),
                            teacherimg = dr["teacherimg"].ToString(),
                            teacherind = id
                        };
                        model.ListOfTableTeacher.Add(modelTable);
                    }
                    dr.Close();
                }
                return model;
            }
        }
        #endregion
    }
}
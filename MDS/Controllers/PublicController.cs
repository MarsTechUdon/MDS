using MDS.DBExec;
using MDS.Fillter;
using MDS.Models;
using MDS.Reports.TeacherHours.TeacherHoursXSDTableAdapters;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace MDS.Controllers
{
    public class PublicController : Controller
    {
        PublicManagement PublicManagement = new PublicManagement();
        SchoolManagement school = new SchoolManagement();
        // GET: Public
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Student(string id)
        {
            string studenid = ENDEtxtManagement.Decrypt(id);
            //string studenid = "21";

            SessionCompanyModel SessionCompany = new SessionCompanyModel();
            SessionCompany.ind = "1";
            SessionCompany.langid = "TH";
            SessionCompany = PublicManagement.GetCompany(SessionCompany);
            Session["SessionCompany"] = SessionCompany;

            StudentModel model = new StudentModel();
            model = PublicManagement.GetStudentById(studenid);
            //หลักสูตร,ประเทศ,คำนำหน้าTH, คำนำหน้าEN
            //ViewData["ListCourse"] = StudentManagement.GetCourseList();
            //ViewData["ListCountry"] = StudentManagement.GetCountryList();
            //ViewData["ListTitleTH"] = StudentManagement.GetTitleTHList();
            //ViewData["ListTitleEN"] = StudentManagement.GetTitleENList();

            //ประเภทของค่า รับเงิน,บัตรเครดิต,บัญชีรับเงินโอน,ชื่อธนาคาร
            ViewData["ListReceive"] = StudentManagement.GetReceiveList();
            ViewData["ListReceiveCard"] = StudentManagement.GetReceiveCardList();
            ViewData["ListBankaccount"] = StudentManagement.GetBankaccountList();
            ViewData["ListBank"] = StudentManagement.GetBankList();

            //Lang
            ViewData["Lang"] = PublicManagement.GetLanguage();
            ViewBag.tabActive = "4";
            return View(model);
        }
        public ActionResult Teacher(string id)
        {
            string teacherid = ENDEtxtManagement.Decrypt(id);
            TeacherModel value = new TeacherModel();
            value.ind = teacherid;
            value = new TeacherManagement().GetTeacherById(value);
            var list = new TeacherModel();
         
            SessionCompanyModel SessionCompany = new SessionCompanyModel();
            SessionCompany.ind = "1";
            SessionCompany.langid = "TH";
            SessionCompany = PublicManagement.GetCompany(SessionCompany);
            Session["SessionCompany"] = SessionCompany;
        
            TeacherModel model = new TeacherModel();
            ViewBag.tid = teacherid;
            list = new PublicManagement().GetTeacherById(model, teacherid);
            //modelteach = list.ListOfTableTeacher.FirstOrDefault(x => x.teacherind.Equals(teacherid));
            ViewBag.cardimg = value.cardimg;
            ViewBag.Fulltname = value.titleT + " " + value.nameT +" "+ value.surnameT;
            ViewBag.nickname = value.nickname;
            ViewBag.TableOfTeacher = list.ListOfTableTeacher;
            return View("Teacher", list);
        }

        public ActionResult TeacherDetailsDate(FormCollection Value)
        {

            var teacherind = Value["teacherind"];
            TeacherModel value = new TeacherModel();
            value.ind = teacherind;
            value = new TeacherManagement().GetTeacherById(value);
            var list = new TeacherModel();

            var modelteach = new TableTeacherModel();
            SessionCompanyModel SessionCompany = new SessionCompanyModel();
            SessionCompany.ind = "1";
            SessionCompany.langid = "TH";
            SessionCompany = PublicManagement.GetCompany(SessionCompany);
            Session["SessionCompany"] = SessionCompany;
            TeacherModel model = new TeacherModel();
            var ugmData = Value["ugmData"];
            var ugmData2 = Value["ugmData2"];
            model.ind = Value["teacherind"];
            ViewBag.tid = teacherind;
            ViewBag.fdate = ugmData;
            ViewBag.tdate = ugmData2;
            ViewBag.cardimg = value.cardimg;
            ViewBag.Fulltname = value.titleT + " " + value.nameT + " " + value.surnameT;
            ViewBag.nickname = value.nickname;
            ViewBag.TableOfTeacher = list.ListOfTableTeacher;
            list = new PublicManagement().GetTeacherByDate(model, teacherind, ugmData, ugmData2);
            modelteach = list.ListOfTableTeacher.FirstOrDefault(x => x.teacherind.Equals(teacherind));
            ViewBag.TableOfTeacher = new PublicManagement().GetTeacherByDate(model, teacherind, ugmData, ugmData2);
            //var TableOfTeacher = new PublicManagement().GetTeacherByDate(model, teacherind, ugmData, ugmData2);
            ViewBag.TableOfTeacher = list.ListOfTableTeacher;
            
            return View("Teacher",list);
        }
        public ActionResult GetExam(string studentind,string LangValue)
        {
            string Examid = string.Empty;
            Examid = PublicManagement.GetExam(studentind, LangValue);
            string studentid = ENDEtxtManagement.Encrypt(studentind);
            string examid = ENDEtxtManagement.Encrypt(Examid);
            if (Examid != "")
            {
                return RedirectToAction("Explanation", "Exam", new { studentind = studentid, examid = examid , LangValue = LangValue });
            }
            else
            {
                string sid = ENDEtxtManagement.Encrypt(studentind);
                return RedirectToAction("Student", new { id = sid });
            }

            
        }


        [NeedLogin]
        [HttpPost]
        public JsonResult GenQR(string Url, string Logo)
        {
            string imgBase64 = PublicManagement.Image(Url, Logo);
            return Json(imgBase64);
        }

        #region GetHoursTeacherPublic 
        public ActionResult GetHoursTeacherPublic(string teacherind, string fdate, string tdate)
        {
            string dateCurrent = DateTime.Now.ToString("yyyy-MM-dd");
            int teacherId = (teacherind == null ? 0 : Int32.Parse(teacherind));
            string fd = (fdate == null ? dateCurrent : fdate);
            string td = (tdate == null ? dateCurrent : tdate);

            string subjecttype = "A";
            ViewBag.SubType = subjecttype;

            SessionCompanyModel SessionCompany = new SessionCompanyModel();
            SessionCompany.ind = "1";
            SessionCompany.langid = "TH";
            SessionCompany = PublicManagement.GetCompany(SessionCompany);
            Session["SessionCompany"] = SessionCompany;

            ReportViewer reportViewer = new ReportViewer();
            reportViewer.ProcessingMode = ProcessingMode.Local;
            reportViewer.ShowPrintButton = true;
            reportViewer.ShowExportControls = true;
            reportViewer.ShowFindControls = false;
            reportViewer.AsyncRendering = false;
            reportViewer.SizeToReportContent = true;
            reportViewer.ZoomMode = ZoomMode.Percent;
            reportViewer.Width = Unit.Percentage(100);
            reportViewer.Height = Unit.Percentage(100);

            reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"\Reports\TeacherHours\TeacherHoursPublic.rdlc";
            sp_reportTeacherHoursTableAdapter TeacherHours = new sp_reportTeacherHoursTableAdapter();

            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("TeacherHoursPublicDataset", (object)TeacherHours.GetData(fd, td, subjecttype, teacherId)));

            reportViewer.LocalReport.DisplayName = DateTime.Now.ToShortDateString() + "_รายงานรายงานจำนวน_ชม_ครู";
            ReportParameter Datecurrent = new ReportParameter("Datecurrent", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss", new CultureInfo("th-TH")));
            reportViewer.LocalReport.EnableExternalImages = true;
            var company = school.GetCompanyView();
            ReportParameter SchoolLogo = new ReportParameter("SchoolLogo", new Uri(Server.MapPath("~/" + company.SchoolLogo)).AbsoluteUri);
            ReportParameter SchoolName = new ReportParameter("SchoolName", company.SchoolName);
            ReportParameter SchoolAdd1 = new ReportParameter("SchoolAddr1", company.schoolAddr1);
            ReportParameter SchoolAdd2 = new ReportParameter("SchoolAddr2", company.schoolAddr2);
            ReportParameter SchoolAdd3 = new ReportParameter("SchoolAddr3", company.schoolAddr3);
            ReportParameter Fdate = new ReportParameter("Fdate", fd);
            ReportParameter Tdate = new ReportParameter("Tdate", td);
            string type = string.Empty;
            if (subjecttype == "A")
            {
                type = "ทั้งหมด";
            }
            else if (subjecttype == "T")
            {
                type = "ทฤษฎี";
            }
            else if (subjecttype == "P")
            {
                type = "ปฏิบัติ";
            }
            ReportParameter SubjectType = new ReportParameter("SubjectType", type);
            reportViewer.LocalReport.SetParameters(new ReportParameter[] { Datecurrent, SchoolLogo, SchoolName, SchoolAdd1, SchoolAdd2, SchoolAdd3, Fdate, Tdate, SubjectType });
            ViewBag.ReportViewer = reportViewer;
            return View();
        }
        #endregion
    }
}

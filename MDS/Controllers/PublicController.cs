using MDS.DBExec;
using MDS.Fillter;
using MDS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MDS.Controllers
{
    public class PublicController : Controller
    {
        PublicManagement PublicManagement = new PublicManagement();
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

    }
}

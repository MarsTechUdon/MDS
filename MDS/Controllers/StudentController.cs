using MDS.DBExec;
using MDS.Fillter;
using MDS.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MDS.Controllers
{
    public class StudentController : Controller
    {
        StudentManagement StudentManagement = new StudentManagement();
        PaymentManagement PaymentManagement = new PaymentManagement();
        PublicManagement PublicManagement = new PublicManagement();
        // GET: Student
        public ActionResult Index()
        {
            return View();
        }
        [NeedLogin]
        public ActionResult StudentList(string filterno = null)
        {
            IList<ManageStudentModel> ListStudents = new List<ManageStudentModel>();
            ManageStudentModel model = new ManageStudentModel();
            
            if (filterno != null)
            {
                ListStudents = StudentManagement.ListStudentFilter(filterno);
                model.filterno = filterno;
            }
            else
            {
                model.filterno = "5";
                ListStudents = StudentManagement.ListStudentFilter(model.filterno);
                //ListStudents = StudentManagement.ListStudent();
            }
            
            ViewData["FilterList"] = StudentManagement.GetFilterList();
            //IList<ManageStudentModel> ListShowStudents = new List<ManageStudentModel>();
            //ListShowStudents = ListStudents;
            foreach (var item in ListStudents)
            {
                string url = item.Qrurl.ToString();
                string application = UrlHelper.GenerateUrl("Default", "Index", "Login", null, null, null, null, 
                    System.Web.Routing.RouteTable.Routes, Request.RequestContext, false);
                string QRUrl = "http://" + Request.Url.Authority + application + "Public/Student?id=" + url;
                //string QRUrl = "http://" + Request.Url.Authority + Request.ApplicationPath + url;
                //string imgBase64 = PublicManagement.Image(QRUrl);
                //item.Qrurl = imgBase64;
                item.URL = QRUrl;
            }
            ViewData["ListStudents"] = ListStudents;
            ViewBag.Result = TempData["Result"];
            ViewBag.Message = TempData["Message"];
            return View(model);
        }

        [NeedLogin]
        public ActionResult Createstudent(string Studenttype,string bid = null)
        {
            var model = new StudentModel();
            model.studenttype = Studenttype;
            model.bookingno = bid;
            if (bid == null) model.bookingno = "0";
            ViewData["ListCourse"] = StudentManagement.GetCourseList();
            ViewData["ListTitleTH"] = StudentManagement.GetTitleTHList();
            ViewData["ListTitleEN"] = StudentManagement.GetTitleENList();
            return View(model);
        }
        [NeedLogin]
        public ActionResult CreateForeignStudent(string Studenttype, string bid = null)
        {
            var model = new StudentModel();
            model.studenttype = Studenttype;
            model.bookingno = bid;
            if (bid == null) model.bookingno = "0";
            ViewData["ListCourse"] = StudentManagement.GetCourseList();
            ViewData["ListTitleEN"] = StudentManagement.GetTitleENList();
            ViewData["ListCountry"] = StudentManagement.GetCountryList();
            return View(model);
        }

        [NeedLogin]
        [HttpPost]
        public ActionResult StudentRegister(FormCollection Value, HttpPostedFileBase attachment)
        {
            StudentModel model = new StudentModel();
            model.studenttype = Value["studenttype"];
            model.cardimg = Value["photo_temp"];
            model.titleT = Value["titleT"];
            model.nameT = Value["name"];
            model.surnameT = Value["lname"];
            model.titleE = Value["titleE"];
            model.nameE = Value["name_en"];
            model.surnameE = Value["lname_en"];
            model.idcard = Value["idcard"];
            model.birthdate = Value["dateofbirth"];
            model.age = Value["age"];
            model.email = Value["email"];
            model.nation = Value["nationality"];
            model.gentle = Value["gender"];
            model.tumbol = Value["district"];
            model.Disability = Value["disabled"];
            model.amphur = Value["amphoe"];
            model.changwat = Value["province"];
            model.zipcode = Value["zipcode"];
            if (model.changwat == "กรุงเทพมหานคร")
            {
                model.address = Value["address"] + " " + model.tumbol + " " + model.amphur + " จ." + model.changwat + " " + model.zipcode;
            }
            else
            {
                model.address = Value["address"] + " ต." + model.tumbol + " อ." + model.amphur + " จ." + model.changwat + " " + model.zipcode;
            }
            model.mobileno = Value["tel"];
            model.disease = Value["medical_prob"];
            model.courseid = Value["courseid"];
            model.examdate = Value["examdate"];

            if (attachment != null && attachment.ContentLength > 0)
            {
                var fileName = Path.GetFileName(attachment.FileName);
                string[] name = fileName.Split('.');
                string typeName = "." + name.Last().ToString();
                var fileNewname = DateTime.Now.ToFileTime() + "_" + fileName;
                var savedFileName = Path.Combine(Server.MapPath("~/MDSMiddleFile/ForeignStudent"), fileNewname);
                model.passportimg = fileNewname;
                model.passportimgftype = typeName;
                attachment.SaveAs(savedFileName);
            }
            model.passportNo = Value["idpassport"];
            model.passportCountry = Value["countrycode"];
            var UserData = Session["UserProfile"] as UserSessionModel;
            model.User = UserData.Username;
            var BranchData = Session["GetBranch"] as CurrentBranchModel;
            model.branchid = BranchData.branchid;
            model.bookingno = "0";

            var res = StudentManagement.Insert(model);
            TempData["Result"] = res.Result;
            TempData["Message"] = res.Message;
            TempData["Studentind"] = res.studentind;
            TempData["voucherid"] = res.voucherid;
            TempData["studenttype"] = model.studenttype;
            TempData["tabActive"] = "3";
            model.studentind = res.studentind;
            if (res.Result != "N")
            {
                //var resBooking = StudentManagement.AddBooking(model);
                //TempData["Result"] = resBooking.result;
                //TempData["Message"] = resBooking.msg;
                StudentModel modelStudent = new StudentModel();
                modelStudent.studentind = res.studentind;
                modelStudent.voucherid = res.voucherid;
                modelStudent.tabActive = "3";
                return RedirectToAction("StudentDetail", modelStudent);
            }
            else
            {
              
                return RedirectToAction("StudentList");
            }
           
        }

        [NeedLogin]
        [HttpPost]
        public ActionResult StudentEdit(StudentModel dataModel, FormCollection Value, HttpPostedFileBase attachment)
        {
            StudentModel model = new StudentModel();
            model.studentind = dataModel.studentind;
            model.studenttype = dataModel.studenttype;
            model.voucherid = Value["voucherid"];
            model.cardimg = Value["photo_temp"];
            if (model.studenttype == "2") model.cardimg = Value["photo_temp1"];
           
            model.titleT = Value["titleT"];
            model.nameT = Value["name"];
            model.surnameT = Value["lname"];
            model.titleE = Value["titleE"];
            model.nameE = Value["name_en"];
            model.surnameE = Value["lname_en"];
            model.idcard = Value["idcard"];
            model.birthdate = Value["dateofbirth"];
            model.age = Value["age"];
            model.email = Value["email"];
            model.nation = Value["nationality"];
            model.gentle = Value["gender"];
            model.tumbol = Value["district"];
            model.Disability = Value["disabled"];
            model.amphur = Value["amphoe"];
            model.changwat = Value["province"];
            model.zipcode = Value["zipcode"];
            model.address = Value["address"];
            model.mobileno = Value["tel"];
            model.disease = Value["medical_prob"];
            model.courseid = Value["courseid"];
            model.examdate = Value["examdateTemp"]; 
             if (model.studenttype == "2") model.examdate = Value["examdateEditTemp"];

            if (attachment != null && attachment.ContentLength > 0)
            {
                var fileName = Path.GetFileName(attachment.FileName);
                string[] name = fileName.Split('.');
                string typeName = "." + name.Last().ToString();
                var fileNewname = DateTime.Now.ToFileTime() + "_" + fileName;
                var savedFileName = Path.Combine(Server.MapPath("~/MDSMiddleFile/ForeignStudent"), fileNewname);
                model.passportimg = fileNewname;
                model.passportimgftype = typeName;
                attachment.SaveAs(savedFileName);
            }
            model.passportNo = Value["idpassport"];
            model.passportCountry = Value["countrycode"];
            if(model.studenttype =="2") model.passportCountry = Value["passportCountry"];
            var UserData = Session["UserProfile"] as UserSessionModel;
            model.User = UserData.Username;
            var BranchData = Session["GetBranch"] as CurrentBranchModel;
            model.branchid = BranchData.branchid;

            var res = StudentManagement.Edit(model);
            TempData["Result"] = res.Result;
            TempData["Message"] = res.Message;

            TempData["Studentind"] = dataModel.studentind;
            TempData["voucherid"] = Value["voucherid"];
            TempData["studenttype"] = model.studenttype;
            TempData["tabActive"] = "1";

            StudentModel modelStudent = new StudentModel();
            modelStudent.studentind = dataModel.studentind;
            modelStudent.voucherid = Value["voucherid"];
            modelStudent.tabActive = "1";
            return RedirectToAction("StudentDetail", modelStudent);

        }

        [NeedLogin]
        [HttpPost]
        public ActionResult DeleteStudent(FormCollection Value)
        {
            string StudentId = Value["frmId"].ToString();
            var UserData = Session["UserProfile"] as UserSessionModel;
            string User = UserData.Username;
            var model = new ResultModel();
            model = StudentManagement.Delete(StudentId, User);
            TempData["Result"] = model.Result;
            TempData["Message"] = model.Message;

            return RedirectToAction("StudentList");
        }

        [NeedLogin]
        [HttpPost]
        public ActionResult ReDeleteStudent(FormCollection Value)
        {
            string StudentId = Value["frmreId"].ToString();
            var UserData = Session["UserProfile"] as UserSessionModel;
            string User = UserData.Username;
            var model = new ResultModel();
            model = StudentManagement.ReDelete(StudentId, User);
            TempData["Result"] = model.Result;
            TempData["Message"] = model.Message;

            return RedirectToAction("StudentList");
        }

        [NeedLogin]
        public ActionResult StudentDetail(FormCollection Value,StudentModel modelStudent)
        {
            StudentModel model = new StudentModel();
            model.studentind = modelStudent.studentind;
            ViewBag.tabActive = modelStudent.tabActive;
            model.voucherid = modelStudent.voucherid;
            if (TempData["Paymentstudentind"] != null)
            {
                model.studentind = TempData["Paymentstudentind"].ToString();
                ViewBag.tabActive = TempData["tabActive"].ToString();
            }
            if (TempData["Studentind"] != null)
            {
                model.studentind = TempData["Studentind"].ToString();
                ViewBag.tabActive = TempData["tabActive"].ToString();
                model.studenttype = TempData["studenttype"].ToString();
                model.voucherid = TempData["voucherid"].ToString();

            }
            model = StudentManagement.GetStudentById(model);
            var res = PaymentManagement.ListPayment(model.studentind);
            model.ListPayment = res.ListPayment;
            model.ListOfReceipt = res.ListOfReceipt;

            var UserData = Session["UserProfile"] as UserSessionModel;
            model.User = UserData.Username;
            var BranchData = Session["GetBranch"] as CurrentBranchModel;
            model.branchid = BranchData.branchid;
            List<StudentBookingModel> ListTempbooking = new List<StudentBookingModel>();
            //เช็ค bookingid = 0 คือยังไม่มีตารางเรียน
            if (model.bookingno.Equals("0"))
            {
                StudentBookingModel modelbooking = new StudentBookingModel();
                modelbooking.courseid = model.courseid;
                modelbooking.user = model.User;
                //modelbooking.user = "khuan";
                ListTempbooking = StudentManagement.ListTempBooking(modelbooking);
            }
            ViewData["ListTempbooking"] = ListTempbooking;
            ViewBag.sid = ENDEtxtManagement.Encrypt(model.studentind);
            ViewBag.qrbase64 = model.qrurl;

            //หลักสูตร,ประเทศ,คำนำหน้าTH, คำนำหน้าEN
            ViewData["ListCourse"] = StudentManagement.GetCourseList();
            ViewData["ListCountry"] = StudentManagement.GetCountryList();
            ViewData["ListTitleTH"] = StudentManagement.GetTitleTHList();
            ViewData["ListTitleEN"] = StudentManagement.GetTitleENList();

            //ประเภทของค่า รับเงิน,บัตรเครดิต,บัญชีรับเงินโอน,ชื่อธนาคาร
            ViewData["ListReceive"] = StudentManagement.GetReceiveList();
            ViewData["ListReceiveCard"] = StudentManagement.GetReceiveCardList();
            ViewData["ListBankaccount"] = StudentManagement.GetBankaccountList();
            ViewData["ListBank"] = StudentManagement.GetBankList();

            //ViewBag.studentind = Value["studentind"].ToString();
            //ViewBag.studenttype = Value["studenttype"].ToString();

            ViewBag.DateToday = DateTime.Now.ToString("yyyy-MM-dd", new System.Globalization.CultureInfo("en-GB"));
            ViewBag.Result = TempData["Result"];
            ViewBag.Message = TempData["Message"];
            return View(model);
        }

        [NeedLogin]
        public ActionResult AssignBooking(StudentBookingModel model)
        {
            //var resBooking = StudentManagement.AddBooking(model);
            //TempData["Result"] = resBooking.result;
            //TempData["Message"] = resBooking.msg;
            if (model.bookingid == null)
            {
                string selected= Request.Form["TempBooking_variation"].ToString();
                model.bookingid = selected;
            }
            var UserData = Session["UserProfile"] as UserSessionModel;
            model.user = UserData.Username;


            var resAssign = StudentManagement.AssignBooking(model);
            TempData["Result"] = resAssign.result;
            TempData["Message"] = resAssign.msg;

            StudentModel modelStudent = new StudentModel();
            modelStudent.studentind = resAssign.studentind;
            modelStudent.voucherid = resAssign.voucherid;
            modelStudent.bookingno = resAssign.bookingid;
            modelStudent.tabActive = "2";
            return RedirectToAction("StudentDetail", modelStudent);
        }

        [NeedLogin]
        public ActionResult AddBooking(StudentModel model = null)
        {
            var UserData = Session["UserProfile"] as UserSessionModel;
            model.User = UserData.Username;
            var BranchData = Session["GetBranch"] as CurrentBranchModel;
            model.branchid = BranchData.branchid;

            string coursetime = StudentManagement.GetCouseById(model.courseid);
            model.coursetime = coursetime;

            var resBooking = StudentManagement.AddBooking(model);
            TempData["Result"] = resBooking.result;
            TempData["Message"] = resBooking.msg;

            StudentBookingModel modelbooking = new StudentBookingModel();
            modelbooking.studentind = model.studentind;
            modelbooking.voucherid = resBooking.voucherid;
            modelbooking.bookingid = resBooking.bookingid;
            modelbooking.tabActive = "2";
            return RedirectToAction("AssignBooking", modelbooking);
        }
    }
}
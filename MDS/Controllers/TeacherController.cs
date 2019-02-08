using MDS.DBExec;
using MDS.Fillter;
using MDS.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using MDS.DBEntity;

namespace MDS.Controllers
{
    public class TeacherController : Controller
    {
        PublicManagement PublicManagement = new PublicManagement();
        // GET: Teacher
        public ActionResult Index()
        {
            return View();
        }
        [NeedLogin]
        public ActionResult TeacherList()
        {
            IList<TeacherModel> model = new List<TeacherModel>();
            var ListOfTeacher = new TeacherManagement().GetAllTeacher();
            foreach (var item in ListOfTeacher)
            {
                if (item.Qrurl != (""))
                {
                    string url = item.Qrurl.ToString();
                    //string[] link = url.Split('=');
                    //string QRUrl = Request.Url.Authority + Request.ApplicationPath + link[0] + "=" + ENDEtxtManagement.Encrypt(link[1]);
                    string application = UrlHelper.GenerateUrl("Default", "Index", "Login", null, null, null, null,
                    System.Web.Routing.RouteTable.Routes, Request.RequestContext, false);
                    string QRUrl = "http://" + Request.Url.Authority + application + "Public/Teacher?id=" + url;
                    //string str = "http://localhost:58804/Public/StudentDetails?key=0x01000000BE270303EDA935B7F5681EA22B6A37616E445D4AB3AFA23C";
                    //string imgBase64 = PublicManagement.Image(QRUrl);
                    //item.Qrurl = imgBase64;
                    item.URL = QRUrl;
                }
            }
            ViewBag.msg = TempData["msg"];
            ViewBag.boolResult = TempData["boolResult"];
            return View("TeacherList", ListOfTeacher);
        }
        [NeedLogin]
        public ActionResult CreateTeacher()
        {
            var model = new TeacherModel();

            ViewData["ListTitleTH"] = StudentManagement.GetTitleTHList();
            ViewData["ListTitleEN"] = StudentManagement.GetTitleENList();
            return View(model);
        }
        [NeedLogin]
        public ActionResult AddTeacher(TeacherModel dataModel, FormCollection Value)
        {
            TeacherModel model = new TeacherModel();
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
            model.amphur = Value["amphoe"];
            model.changwat= Value["province"];
            model.zipcode = Value["zipcode"];
            model.address = Value["address"] + " จังหวัด" + model.changwat + " " + model.zipcode;
            model.mobileno = Value["tel"];
            model.nickname = Value["nickname"];

            var UserData = Session["UserProfile"] as UserSessionModel;
            model.User = UserData.Username;
            var res = new TeacherManagement().AddTeacher(model);
            //TempData["ind"] = res.ind;
            model.ind = res.ind;
            TempData["boolResult"] = res.result;
            TempData["msg"] = res.msg;
            if (res.result != "N")
            {
                return RedirectToAction("TeacherDetail", new { model.ind });
            }
            else
            {
                return RedirectToAction("TeacherList");
            }
        }
        [HttpPost]
        public ActionResult EditTeacher(TeacherModel dataModel, FormCollection Value)
        {
            TeacherModel model = new TeacherModel();

            model.ind = Value["ind"];
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
            model.amphur = Value["amphoe"];
            model.changwat = Value["province"];
            model.zipcode = Value["zipcode"];
            model.address = Value["address"];
            model.mobileno = Value["tel"];
            model.nickname = Value["nickname"];

            var UserData = Session["UserProfile"] as UserSessionModel;
            model.User = UserData.Username;
            var res = new TeacherManagement().EditTeacher(model);
            TempData["boolResult"] = res.result;
            TempData["msg"] = res.msg;

            return RedirectToAction("TeacherDetail", new { model.ind });

        }
        [NeedLogin]
        public ActionResult TeacherDetail(FormCollection Value, TeacherModel modelTeacher)
        { 
            TeacherModel model = new TeacherModel();
            model.ind = modelTeacher.ind;
            model = new TeacherManagement().GetTeacherById(model);

            ViewData["ListTitleTH"] = StudentManagement.GetTitleTHList();
            ViewData["ListTitleEN"] = StudentManagement.GetTitleENList();

            var UserData = Session["UserProfile"] as UserSessionModel;
            model.User = UserData.Username;

            ViewBag.DateToday = DateTime.Now.ToString("yyyy-MM-dd", new System.Globalization.CultureInfo("en-GB"));
            ViewBag.boolResult = TempData["boolResult"];
            ViewBag.msg = TempData["msg"];

            return View(model);
        }
        [NeedLogin]
        public ActionResult CancelTeacher(TeacherModel ugm)
        {
            var result = new TeacherManagement().CancelTeacher2(ugm);
            TempData["msg"] = result.msg;
            TempData["boolResult"] = result.result;
            return RedirectToAction("TeacherList");
        }
        [NeedLogin]
        public ActionResult ReCancelTeacher(TeacherModel ugm)
        {
            var result = new TeacherManagement().ReCancelTeacher2(ugm);
            TempData["msg"] = result.msg;
            TempData["boolResult"] = result.result;
            return RedirectToAction("TeacherList");
        }
    }
}
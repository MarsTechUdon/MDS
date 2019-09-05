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
        #region "ListLeaveDate"
        /// <summary>
        ///   วันหยุดครู
        /// by : nopphakorn
        /// </summary>
        /// <returns></returns>
        [NeedLogin]
        public ActionResult ListLeaveDate()
        {

            var value = new LeaveDateModel();
            ViewBag.Result = TempData["Result"];
            ViewBag.Message = TempData["Message"];
            var datenow = DateTime.Now;
            var userDt = datenow.ToString("yyyy-MM-dd", new System.Globalization.CultureInfo("en-GB"));
            var Dtdatenow = datenow.ToString("dd/MM/yyyy", new System.Globalization.CultureInfo("en-GB"));
            value.fdate = userDt;
            //value.fdate = "2019-08-20";
            value.tdate = userDt;
            //var currentdate = "2018-11-28";           
            ViewBag.ListLeaveDate = (TempData["ListLeaveDate"] != null ? TempData["ListLeaveDate"] : new TeacherManagement().GetListLeaveDate(value));
            ViewBag.fdate = (TempData["fdate"] != null ? TempData["fdate"].ToString() : Dtdatenow);
            ViewBag.tdate = (TempData["tdate"] != null ? TempData["tdate"].ToString() : Dtdatenow);  //ViewBag.fdate = "28/11/2018";
            ViewBag.Activedate = Dtdatenow;
            ViewBag.ind = (TempData["ind"] != null ? TempData["ind"].ToString() : "");
            ViewBag.ListteacherActive = new TeacherManagement().GetListteacherActive();

            return View();
        }
        #endregion
        #region "SearchLeaveDate"
        /// <summary>
        ///   ค้นหา วันหยุดครู
        /// by : nopphakorn
        /// </summary>
        /// <returns></returns>
        [NeedLogin]
        public ActionResult SearchLeaveDate(LeaveDateModel value)
        {
            string fdate = "";
            string tdate = "";
            DateTime sfDate = DateTime.ParseExact(value.fdate, "dd/MM/yyyy", new System.Globalization.CultureInfo("en-GB"));
            value.fdate = sfDate.ToString("yyyy-MM-dd", new System.Globalization.CultureInfo("en-GB"));
            fdate = sfDate.ToString("dd/MM/yyy", new System.Globalization.CultureInfo("en-GB"));

            DateTime stDate = DateTime.ParseExact(value.tdate, "dd/MM/yyyy", new System.Globalization.CultureInfo("en-GB"));
            value.tdate = stDate.ToString("yyyy-MM-dd", new System.Globalization.CultureInfo("en-GB"));
            tdate = stDate.ToString("dd/MM/yyy", new System.Globalization.CultureInfo("en-GB"));

            TempData["ListLeaveDate"] = new TeacherManagement().GetListLeaveDate(value);
            TempData["fdate"] = fdate;
            TempData["tdate"] = tdate;
            TempData["ind"] = value.ind;
            return RedirectToAction("ListLeaveDate", "Teacher");
        }
        #endregion
        #region "addLeavedate"
        /// <summary>
        ///  เพิ่ม วันหยุดครู
        /// by : nopphakorn
        /// </summary>
        /// <returns></returns>
        [NeedLogin]
        public ActionResult addLeavedate(LeaveDateModel value)
        {
            var userData = Session["UserProfile"] as UserSessionModel;
            var model = new ResultModel();
            if (value.typedate == "1")
            {
                model = new TeacherManagement().AddLeavebyday(value, userData.Username);
            }
            else
            {
                model = new TeacherManagement().AddLeavebydate(value, userData.Username);
            }
            TempData["Result"] = model.Result;
            TempData["Message"] = model.Message;
            return RedirectToAction("ListLeaveDate", "Teacher");
        }
        #endregion
        #region "editLeavedate"
        /// <summary>
        ///  แก้ไข วันหยุดครู
        /// by : nopphakorn
        /// </summary>
        /// <returns></returns>
        [NeedLogin]
        public ActionResult editLeavedate(LeaveDateModel value)
        {
            var userData = Session["UserProfile"] as UserSessionModel;
            var model = new ResultModel();
            model = new TeacherManagement().editLeavedate(value, userData.Username);
            TempData["Result"] = model.Result;
            TempData["Message"] = model.Message;
            return RedirectToAction("ListLeaveDate", "Teacher");
        }
        #endregion
        #region "deleteLeaveDate"
        /// <summary>
        ///  ลบ วันหยุดครู
        /// by : nopphakorn
        /// </summary>
        /// <returns></returns>
        [NeedLogin]
        public ActionResult deleteLeaveDate(LeaveDateModel value)
        {
            var userData = Session["UserProfile"] as UserSessionModel;
            var model = new ResultModel();
            if (value.delind != null)
            {
                value.listDeteteLeave = string.Join(",", value.delind);
            }
            model = new TeacherManagement().deleteLeaveDate(value);
            TempData["Result"] = model.Result;
            TempData["Message"] = model.Message;
            return RedirectToAction("ListLeaveDate", "Teacher");
            //return Json(model, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region "Getleavebyid"
        /// <summary>
        ///  ดึงขอ้มูล วันหยุดครู by ind
        /// by : nopphakorn
        /// </summary>
        /// <returns></returns>
        [NeedLogin]
        public ActionResult Getleavebyid(LeaveDateModel value)
        {
            var model = new LeaveDateModel();
            model = new TeacherManagement().Getleavebyid(value);
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}
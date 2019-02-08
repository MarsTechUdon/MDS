using MDS.DBExec;
using MDS.Fillter;
using MDS.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MDS.Controllers
{
    public class ManageExamController : Controller
    {
        // GET: ManageExam
        public ActionResult Index()
        {
            return View();
        }
        [NeedLogin]
        public ActionResult ManageExamList(string ugmData, string ugmData2)
        {
            var model = new ManageExamModel();

            ViewData["ListExam"] = new ManageExamManagement().GetListExam(ugmData, ugmData2);

            ViewData["ListSubjectGroup"] = ManageExamManagement.GetSubjectGroup();
            ViewData["ListLanguage"] = ManageExamManagement.GetLanguage();

            ViewBag.msg = TempData["msg"];
            ViewBag.boolResult = TempData["boolResult"];

            return View(model);
        }
        public ActionResult CreateManageExam()
        {
            var model = new ManageExamModel();

            ViewData["ListSubjectGroup"] = ManageExamManagement.GetSubjectGroup();
            ViewData["ListLanguage"] = ManageExamManagement.GetLanguage();
            return View(model);
        }
        [NeedLogin]
        public ActionResult ManageExamDetail(FormCollection Value, ManageExamModel modelManageExam)
        {
            ManageExamModel model = new ManageExamModel();
            model.ind = modelManageExam.ind;
            model = new ManageExamManagement().GetManageExamByID(model);

            ViewData["ListSubjectGroup"] = ManageExamManagement.GetSubjectGroup();
            ViewData["ListLanguage"] = ManageExamManagement.GetLanguage();

            var UserData = Session["UserProfile"] as UserSessionModel;
            model.User = UserData.Username;

            ViewBag.boolResult = TempData["boolResult"];
            ViewBag.msg = TempData["msg"];

            return View(model);
        }
        //[NeedLogin]
        //public ActionResult GetManageExamByID(string ind)
        //{
        //    ManageExamModel model = new ManageExamModel();
        //    model = new ManageExamManagement().GetManageExamByID(Convert.ToInt32(ind));

        //    return Json(model);
        //}
        [NeedLogin]
        public ActionResult AddManageExam(ManageExamModel ugm)
        {
            ManageExamModel model = new ManageExamModel();
            var userData = Session["UserProfile"] as UserSessionModel;
            var result = new ManageExamManagement().AddManageExam(ugm, userData.Username);
            TempData["msg"] = result.msg;
            TempData["boolResult"] = result.result;
            model.ind = result.ind;
            //return RedirectToAction("ManageExamList");
            if (result.result != "N")
            {
                return RedirectToAction("ManageExamDetail", new { model.ind });
            }
            else
            {
                return RedirectToAction("ManageExamList");
            }
        }
        [NeedLogin]
        public ActionResult EditManageExam(ManageExamModel ugm)
        {
            ManageExamModel model = new ManageExamModel();
            var userData = Session["UserProfile"] as UserSessionModel;
            var result = new ManageExamManagement().EditManageExam(ugm, userData.Username);
            TempData["msg"] = result.msg;
            TempData["boolResult"] = result.result;
            model.ind = result.ind;
            return RedirectToAction("ManageExamDetail", new { model.ind });
        }
        [NeedLogin]
        public ActionResult DeleteManageExam(ManageExamModel ugm)
        {
            var userData = Session["UserProfile"] as UserSessionModel;
            var result = new ManageExamManagement().DeleteManageExam(ugm);
            TempData["msg"] = result.msg;
            TempData["boolResult"] = result.result;
            return RedirectToAction("ManageExamList");
        }

    }
}
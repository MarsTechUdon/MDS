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
        public ActionResult ManageExamList(string ugmData, string ugmData2, string dltno)
        {
            var model = new ManageExamModel();

            ViewData["ListExam"] = new ManageExamManagement().GetListExam(ugmData, ugmData2, dltno);
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
        public ActionResult ManageExamDetail( ManageExamModel modelManageExam)
        {
            ManageExamModel model = new ManageExamModel();
            model.ind = modelManageExam.ind;
            model = new ManageExamManagement().GetManageExamByID(model);
            ViewData["ListSubjectGroup"] = ManageExamManagement.GetSubjectGroup();
            ViewData["ListLanguage"] = ManageExamManagement.GetLanguage();

            var UserData = Session["UserProfile"] as UserSessionModel;
            model.User = UserData.Username;
            ViewBag.boolResult = TempData["boolResult"];
            var temp_array_id = TempData["array_id"];
            if (temp_array_id!=null)
            {
                ViewBag.array_id = TempData["array_id"];
            }
            else
            {
                ViewBag.array_id = modelManageExam.array_id;
            }
            ViewBag.msg = TempData["msg"];

            return View(model);
        }

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
            model.array_id = ugm.array_id;
            TempData["array_id"] = model.array_id;
            return RedirectToAction("ManageExamDetail", new { model.ind});
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
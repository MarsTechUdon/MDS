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
    public class SchoolController : Controller
    {
        // GET: School
        SchoolManagement school = new SchoolManagement();
        [NeedLogin]
        public ActionResult Schoolinfo()
        {
            ViewBag.Result = TempData["Result"];
            ViewBag.Message = TempData["Message"];
            ViewBag.ResultCF = TempData["ResultCF"];
            ViewBag.MessageCF = TempData["MessageCF"];

            ViewBag.paraid = TempData["paraid"];
            ViewBag.CFtype = TempData["CFtype"];
            if (TempData["tabActive"] != null)
            {
                ViewBag.tabActive = TempData["tabActive"];
            }
            else {
                ViewBag.tabActive = "1";
            }
            ViewBag.GetCompany= school.GetCompany();
            ViewBag.GetListConfig = school.GetListConfig();
            return View();
        }
        [NeedLogin]
        public ActionResult EditCompany(CompanyModel value)
        {
            var model = new ResultBookingModel();
            var userData = Session["UserProfile"] as UserSessionModel;
            value.updateby = userData.Username;
            model = school.EditCompany(value);
            TempData["Result"] = model.result;
            TempData["Message"] = model.msg;
            TempData["tabActive"] = "1";
            return RedirectToAction("Schoolinfo");
        }
        [NeedLogin]
        [HttpPost]
        public ActionResult SetConfig(string paraid, string paravalue,string paranameName, string type)
        {
            var model = new ResultBookingModel();
            model = school.SetConfig(paraid, paravalue);
            if (model.result == "Y")
            {
                TempData["MessageCF"] = "แก้ไข "+ paranameName+" สำเร็จ";
            }
            else {
                TempData["MessageCF"] = "แก้ไข " + paranameName + " ไม่สำเร็จ";
            }
            TempData["ResultCF"] = model.result;
            TempData["CFtype"] = type;
            TempData["tabActive"] = "2";
            TempData["paraid"] = paraid;
            
            return RedirectToAction("Schoolinfo", "School");
            //return Json(model, JsonRequestBehavior.AllowGet);
        }
    }
}
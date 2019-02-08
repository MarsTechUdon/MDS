using MDS.DBExec;
using MDS.Fillter;
using MDS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using MDS.DBEntity;

namespace MDS.Controllers
{
    public class RcNameController : Controller
    {
        // GET: RcName
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult RcNameList()
        {
            RcNameModel model = new RcNameModel();
            var ListOfRcName = new RcNameManagement().GetAllRcName();

            ViewBag.msg = TempData["msg"];
            ViewBag.boolResult = TempData["boolResult"];
            return View("RcNameList", ListOfRcName);
        }

        [NeedLogin]
        public ActionResult AddRcName(RcNameModel ugm)
        {
            var result = new RcNameManagement().AddRcName2(ugm);
            TempData["msg"] = result.msg;
            TempData["boolResult"] = result.result;
            return RedirectToAction("RcNameList");
        }

        [NeedLogin]
        public ActionResult EditRcName(RcNameModel ugm)
        {
            var result = new RcNameManagement().EditRcName2(ugm);
            TempData["msg"] = result.msg;
            TempData["boolResult"] = result.result;
            return RedirectToAction("RcNameList");
        }

        public ActionResult CancelRcName(RcNameModel ugm)
        {
            var result = new RcNameManagement().CancelRcName2(ugm);
            TempData["msg"] = result.msg;
            TempData["boolResult"] = result.result;
            return RedirectToAction("RcNameList");
        }

        public ActionResult ReCancelRcName(RcNameModel ugm)
        {
            var result = new RcNameManagement().ReCancelRcName2(ugm);
            TempData["msg"] = result.msg;
            TempData["boolResult"] = result.result;
            return RedirectToAction("RcNameList");
        }
    }
}
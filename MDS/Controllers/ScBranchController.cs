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
    public class ScBranchController : Controller
    {
        // GET: ScBranch
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ScBranchList()
        {
            ScBranchModel model = new ScBranchModel();
            var ListOfScBranch = new ScBranchManagement().GetAllScBranch();

            ViewBag.msg = TempData["msg"];
            ViewBag.boolResult = TempData["boolResult"];
            return View("ScBranchList", ListOfScBranch);
        }

        [NeedLogin]
        public ActionResult AddScBranch(ScBranchModel ugm)
        {
            var result = new ScBranchManagement().AddScBranch2(ugm);
            TempData["msg"] = result.msg;
            TempData["boolResult"] = result.result;
            return RedirectToAction("ScBranchList");
        }

        [NeedLogin]
        public ActionResult EditScBranch(ScBranchModel ugm)
        {
            var result = new ScBranchManagement().EditScBranch2(ugm);
            TempData["msg"] = result.msg;
            TempData["boolResult"] = result.result;
            return RedirectToAction("ScBranchList");
        }

        public ActionResult CancelScBranch(ScBranchModel ugm)
        {
            var result = new ScBranchManagement().CancelScBranch2(ugm);
            TempData["msg"] = result.msg;
            TempData["boolResult"] = result.result;
            return RedirectToAction("ScBranchList");
        }

        public ActionResult ReCancelScBranch(ScBranchModel ugm)
        {
            var result = new ScBranchManagement().ReCancelScBranch2(ugm);
            TempData["msg"] = result.msg;
            TempData["boolResult"] = result.result;
            return RedirectToAction("ScBranchList");
        }

    }
}
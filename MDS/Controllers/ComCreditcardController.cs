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
    public class ComCreditcardController : Controller
    {
        // GET: ComCreditcard
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ComCreditcardList()
        {
            ComCreditcardModel model = new ComCreditcardModel();
            var ListOfComCreditcard = new ComCreditcardManagement().GetAllComCreditcard();

            ViewBag.msg = TempData["msg"];
            ViewBag.boolResult = TempData["boolResult"];
            return View("ComCreditcardList", ListOfComCreditcard);
        }

        [NeedLogin]
        public ActionResult AddComCreditcard(ComCreditcardModel ugm)
        {
            var result = new ComCreditcardManagement().AddComCreditcard2(ugm);
            TempData["msg"] = result.msg;
            TempData["boolResult"] = result.result;
            return RedirectToAction("ComCreditcardList");
        }

        [NeedLogin]
        public ActionResult EditComCreditcard(ComCreditcardModel ugm)
        {
            var result = new ComCreditcardManagement().EditComCreditcard2(ugm);
            TempData["msg"] = result.msg;
            TempData["boolResult"] = result.result;
            return RedirectToAction("ComCreditcardList");
        }

        public ActionResult CancelComCreditcard(ComCreditcardModel ugm)
        {
            var result = new ComCreditcardManagement().CancelComCreditcard2(ugm);
            TempData["msg"] = result.msg;
            TempData["boolResult"] = result.result;
            return RedirectToAction("ComCreditcardList");
        }

        public ActionResult ReCancelComCreditcard(ComCreditcardModel ugm)
        {
            var result = new ComCreditcardManagement().ReCancelComCreditcard2(ugm);
            TempData["msg"] = result.msg;
            TempData["boolResult"] = result.result;
            return RedirectToAction("ComCreditcardList");
        }
    }
}
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
    public class ReceiptController : Controller
    {
        // GET: Receipt
        public ActionResult Index()
        {
            return View();
        }
        [NeedLogin]
        public ActionResult ReceiptList()
        {
            ReceiptModel model = new ReceiptModel();
            var ListOfReceipt = new ReceiptManagement().GetAllReceipt();

            ViewBag.msg = TempData["msg"];
            ViewBag.boolResult = TempData["boolResult"];
            return View("ReceiptList", ListOfReceipt);
        }
        [NeedLogin]
        public ActionResult CancelReceipt(ReceiptModel ugm)
        {
            var userData = Session["UserProfile"] as UserSessionModel;
            var ListOfReceipt = new ReceiptManagement().CancelReceipt2(ugm, userData.Username);

            //ViewBag.msg = TempData["msg"];
            //ViewBag.boolResult = TempData["boolResult"];
            return RedirectToAction("ReceiptList");
        }
    }
}
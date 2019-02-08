using MDS.Fillter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MDS.Controllers
{
    public class AccountingController : Controller
    {
        // GET: Accounting
        [NeedLogin]
        public ActionResult ReceiptsAndDeposit()
        {

            return View();
        }
    }
}
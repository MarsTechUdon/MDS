using MDS.DBExec;
using MDS.Fillter;
using MDS.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MDS.Controllers
{
    public class PartialController : Controller
    {

        [NeedLogin]
        public ActionResult DownloadFile()
        {
            ViewBag.Filedownload = OverviewManagement.GetMediafile();
            return View();
        }
    }
}
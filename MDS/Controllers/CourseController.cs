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
    public class CourseController : Controller
    {
        // GET: Course
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CourseList()
        {
            CourseModel model = new CourseModel();
            var ListOfCourse = new CourseManagement().SelectCourse();

            ViewBag.msg = TempData["msg"];
            ViewBag.boolResult = TempData["boolResult"];
            return View("CourseList", ListOfCourse);
        }
        [NeedLogin]
        public ActionResult AddCourse(CourseModel ugm)
        {
            var result = new CourseManagement().AddCourse2(ugm);
            TempData["msg"] = result.msg;
            TempData["boolResult"] = result.result;
            return RedirectToAction("CourseList");
        }
        [NeedLogin]
        public ActionResult EditCourse(CourseModel ugm)
        {
            var result = new CourseManagement().EditCourse2(ugm);
            TempData["msg"] = result.msg;
            TempData["boolResult"] = result.result;
            return RedirectToAction("CourseList");
        }

        public ActionResult CancelCourse(CourseModel ugm)
        {
            var result = new CourseManagement().CancelCourse2(ugm);
            TempData["msg"] = result.msg;
            TempData["boolResult"] = result.result;
            return RedirectToAction("CourseList");
        }

        public ActionResult ReCancelCourse(CourseModel ugm)
        {
            var result = new CourseManagement().ReCancelCourse2(ugm);
            TempData["msg"] = result.msg;
            TempData["boolResult"] = result.result;
            return RedirectToAction("CourseList");
        }

    }
}
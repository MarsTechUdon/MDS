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
    public class CourseGroupController : Controller
    {
        CourseGroupManagement cg_mn = new CourseGroupManagement();
        // GET: CourseGroup
        [NeedLogin]
        public ActionResult CourseGroupList()
        {
            CourseGroupModel model = new CourseGroupModel();
            var CourseGroupList = new CourseGroupManagement().GetAllCourseGroup();

            return View("CourseGroupList", CourseGroupList);
        }

        // POST: ComBank/Edit/5
        [NeedLogin]
        [HttpPost]
        public ActionResult addCourseGroup(FormCollection Value)
        {
            CourseGroupModel model = new CourseGroupModel();
            model.CgName = Value["cg_name"];
            var res = cg_mn.addCourseGroup(model);
            return RedirectToAction("CourseGroupList");
        }

        // POST: ComBank/Edit/5
        [NeedLogin]
        [HttpPost]
        public ActionResult EditCourseGroup(FormCollection Value)
        {
            CourseGroupModel model = new CourseGroupModel();
            model.CgID = Value["edit_cg_id"];
            model.CgName = Value["edit_cg_name"];
            var res = cg_mn.EditCourseGroup(model);
            return RedirectToAction("CourseGroupList");
        }
    }
}

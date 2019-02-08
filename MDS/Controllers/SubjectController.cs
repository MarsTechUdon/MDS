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
    public class SubjectController : Controller
    {
        SubjectManagement sj_mn = new SubjectManagement();
        // GET: Subject
        [NeedLogin]
        public ActionResult SubjectList()
        {
            SubjectModel model = new SubjectModel();
            var ListOfSubject = new SubjectManagement().SelectSubject();

            ViewBag.msg = TempData["msg"];
            ViewBag.boolResult = TempData["boolResult"];
            return View("SubjectList", ListOfSubject);
            //SubjectModel model = new SubjectModel();
            //var ListSubject = new SubjectManagement().GetAllSubject();
            //return View("SubjectList", ListSubject);
        }

        // POST: Subject/Create
        [NeedLogin]
        [HttpPost]
        public ActionResult AddSubject(FormCollection Value)
        {
            SubjectModel modelData = new SubjectModel();
            modelData.SName = Value["sj_name"];
            modelData.SnickName = Value["sj_nickname"];
            modelData.Cid = Value["c_id"];
            modelData.Stype = Value["type"];
            modelData.smin = Value["min"];
            var result = sj_mn.addSubject(modelData);
            return RedirectToAction("SubjectList");
        }

        // POST: Subject/Edit/5
        [NeedLogin]
        [HttpPost]
        public ActionResult editSubject(FormCollection Value)
        {
            SubjectModel modelData = new SubjectModel();
            modelData.Sid = Value["sj_id"];
            modelData.SName = Value["sj_name"];
            modelData.SnickName = Value["sj_nickname"];
            modelData.Cid = Value["c_id"];
            modelData.Stype = Value["edit_type"];
            modelData.smin = Value["min"];
            var res = sj_mn.editSubject(modelData);
            return RedirectToAction("SubjectList");
        }

        // POST: Subject/Delete/5
        [NeedLogin]
        [HttpPost]
        public ActionResult DeleteSubject(FormCollection Value)
        {
            string id = Value["frmId"].ToString();
            var model = new ResultModel();
            model = sj_mn.DeleteSubject(id);
            TempData["Result"] = model.Result;
            TempData["Message"] = model.Message;

            return RedirectToAction("SubjectList");
        }

        [NeedLogin]
        [HttpPost]
        public ActionResult ReDeleteSubject(FormCollection Value)
        {
            string id = Value["frmreId"].ToString();
            var model = new ResultModel();
            model = sj_mn.ReDelete(id);
            TempData["Result"] = model.Result;
            TempData["Message"] = model.Message;

            return RedirectToAction("SubjectList");
        }
    }
}

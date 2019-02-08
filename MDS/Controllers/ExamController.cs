using MDS.DBExec;
using MDS.Fillter;
using MDS.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Services;

namespace MDS.Controllers
{
    public class ExamController : Controller
    {
        // GET: Exam
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Explanation(string studentind, string examid ,string LangValue)
        {
            string sid = ENDEtxtManagement.Decrypt(studentind);
            string eid = ENDEtxtManagement.Decrypt(examid);
            ViewBag.temp_examid = eid;
            ViewBag.temp_studentind = sid;
            ViewBag.LangValue = LangValue;

            return View();
        }

        public ActionResult ExamList(string studentind,string examid, string LangValue)
        {
            ManageExamModel model = new ManageExamModel();
            var ListOfExam = new ExamManagement().SelectExam(studentind, examid);

            ViewBag.temp_examid = examid;
            ViewBag.temp_studentind = studentind;
            string sid = ENDEtxtManagement.Encrypt(studentind);
            ViewBag.temp_en_studentind = sid;
            ViewBag.LangValue = LangValue;
            ViewBag.msg = TempData["msg"];
            ViewBag.boolResult = TempData["boolResult"];
            return View("ExamList", ListOfExam);
        }

        [HttpPost]
        public ActionResult AddExam(string examid, string examQNO, string examAnswerNo)
        {
            var model = new ManageExamModel();
            model = new ExamManagement().AddExam(examid, examQNO, examAnswerNo);
            model.examid = "Haha";
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetQImage(string examid, string examQNO, string examAnswerNo)
        {
            var model = new ManageExamModel();

            if (examAnswerNo=="0")
            {
                model = new ExamManagement().GetQImage(examid, examQNO);
            }
            else if (examAnswerNo == "1")
            {
                var Aimage1 = new ExamManagement().GetAImage1(examid, examQNO);
                model.choiceimage1 = Aimage1.choiceimage1;
            }
            else if (examAnswerNo == "2")
            {
                var Aimage2 = new ExamManagement().GetAImage2(examid, examQNO);
                model.choiceimage2 = Aimage2.choiceimage2;
            }
            else if (examAnswerNo == "3")
            {
                var Aimage3 = new ExamManagement().GetAImage3(examid, examQNO);
                model.choiceimage3 = Aimage3.choiceimage3;
            }
            else if (examAnswerNo == "4")
            {
                var Aimage4 = new ExamManagement().GetAImage4(examid, examQNO);
                model.choiceimage4 = Aimage4.choiceimage4;

            }

            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetQSound(string examid, string examQNO)
        {
            var model = new ManageExamModel();
            model = new ExamManagement().GetQSound(examid, examQNO);

            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetASound(string examid, string examQNO , string examAnswerNo)
        {
            var model = new ManageExamModel();
            model = new ExamManagement().GetASound(examid, examQNO , examAnswerNo);
   
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ExamListScore(string studentind, string examid , string LangValue)
        {
            var model = new ManageExamModel();
            var ListOfExam = new ExamManagement().SelectExam(studentind, examid);
            int count_score = 0;int chk_doing = 0;
                foreach (var Question in ListOfExam.QuestionList)
                {
                    foreach (var Choice in ListOfExam.ChoiceList)
                    {
                        if (Choice.examQNO == Question.examQNO)
                        {
                        if (Choice.answer.Equals("True"))
                        {
                            chk_doing = chk_doing + 1;
                        }
                        if (@Choice.answer.Equals("True") && @Choice.iscorrect.Equals("True"))
                            {
                                count_score = count_score + 1;
                            }
                        }
                    }
                }
            string Str_count_score = count_score.ToString();
            ViewBag.num_score = count_score;
            ViewBag.num_chk_doing = chk_doing;
            ViewBag.LangValue = LangValue;
            model = new ExamManagement().AddFinishExam(Str_count_score, examid);
            //ViewBag.num_score = score;
            string sid = ENDEtxtManagement.Encrypt(studentind);
            ViewBag.num_studentind = sid;
            ViewBag.num_examid = examid;
            ViewBag.msg = TempData["msg"];
            ViewBag.boolResult = TempData["boolResult"];
            return View("ExamListScore", ListOfExam);
        }

    }
}
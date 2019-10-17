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
    public class LearningController : Controller
    {
        //#region "GetCalendar"
        //#endregion
        #region "Booking"
        [NeedLogin]
        public ActionResult Booking()
        {
            ViewBag.Result = TempData["Result"];
            ViewBag.Message = TempData["Message"];

            ViewBag.course = LearningManagement.Getlcourse();
            ViewBag.ListBooking = LearningManagement.GetListBooking();
            return View();
        }
        #endregion
        #region "CreateLearning"
        [NeedLogin]
        public ActionResult CreateLearning(CreateLearningModel value)
        {
            var teach = "";
            if (value.arrteacher != null)
            {
                teach = string.Join(",", value.arrteacher);
            }
            //foreach (var item in value.arrteacher) {
            //    teach = string.Concat(teach, item+',');
            //}
            var BranchModel = new CurrentBranchModel();
            var Cookies = Request.Cookies["branchtxt"];
            BranchModel = CurrentBranchManagement.decode(Cookies.Value);
            var str = value.courseid.Split(' ');
            var userData = Session["UserProfile"] as UserSessionModel;
            ResultBookingModel model = new ResultBookingModel();
            model = LearningManagement.AddBooking(str[0]/*courseid*/, userData.Username, str[2]/*coursetime*/, BranchModel.branchid, value.bookingname, value.remark, value.gear);
            if (model.result == "N")
            {
                TempData["Result"] = model.result;
                TempData["Message"] = model.msg;
                return RedirectToAction("Booking");
            }
            else
            {
                TempData["Result"] = model.result;
                TempData["Message"] = model.msg;
                var value1 = (str[0] != "") ? ENDEtxtManagement.Encrypt(str[0]) : "";//courseid
                var value2 = (teach != "") ? ENDEtxtManagement.Encrypt(teach) : "";//teacherid
                var value3 = (model.bookingid != "") ? ENDEtxtManagement.Encrypt(model.bookingid) : "";//bookingid
                return RedirectToAction("Timetable", "Learning", new { value1, value2, value3 });
            }

        }
        #endregion        
        #region "GetCalendarBooking"
        [HttpPost]
        public JsonResult GetCalendarBooking()
        {
            var currentdate = DateTime.Now.ToString("yyyy-MM-dd", new System.Globalization.CultureInfo("en-GB"));
            //List<CalendarModel> listCalendar = new List<CalendarModel>();
            ListCalendarModel model = new ListCalendarModel();
            model.TeacherList = LearningManagement.GetteacherCalendar();
            model.CalendarList = LearningManagement.ListBookingByDate(currentdate);

            return Json(model, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region "GetCalendarCar"
        [HttpPost]
        public JsonResult GetCalendarCar()
        {
            var currentdate = DateTime.Now.ToString("yyyy-MM-dd", new System.Globalization.CultureInfo("en-GB"));
            //List<CalendarModel> listCalendar = new List<CalendarModel>();
            ListCalendarModel model = new ListCalendarModel();
            model.GearList = LearningManagement.GetGearCalendar();
            model.CalendarList = LearningManagement.ListBookingByDateGear(currentdate);

            return Json(model, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region "GetTeacher"
        [NeedLogin]
        public ActionResult GetTeacher(string coursegroupid)
        {

            List<lteacherModel> model = new List<lteacherModel>();
            List<lteacherModel> list = new List<lteacherModel>();
            list = LearningManagement.Getteacher();
            model = list.Where(s => s.coursegroupid == coursegroupid).ToList();//หลักสูตร
            return Json(model);
        }
        #endregion
        #region "Timetable"
        [NeedLogin]
        public ActionResult Timetable(string value1, string value2, string value3)
        {
            ViewBag.Result = TempData["Result"];
            ViewBag.Message = TempData["Message"];
            ViewBag.BookingConfirm = TempData["BookingConfirm"];
            var subject = new List<lsubjectModel>();
            var subjectByBookingid = new List<lsubjectModel>();
            var listteach = new List<lteacherModel>();
            var listteachselect = new List<lteacherModel>();
            var listteachContact1 = new List<lteacherModel>();
            var listteachContact2 = new List<lteacherModel>();
            var listBookingDetail = new List<BookingDetailModel>();
            var listBookingDetailOneRow = new List<BookingDetailModel>();
            var courseid = (value1 != "") ? ENDEtxtManagement.Decrypt(value1) : "";//courseid
            var arrteacher = (value2 != "") ? ENDEtxtManagement.Decrypt(value2) : "";//idteacher
            var bookingid = (value3 != "") ? ENDEtxtManagement.Decrypt(value3) : "";//bookingid        

            //ชื่อหลักสูตร
            List<lcourseModel> listourse = new List<lcourseModel>();
            listourse = LearningManagement.Getlcourse();
            var coursename = listourse.FirstOrDefault(x => x.courseid.Equals(courseid.ToString())).coursename;
            var coursegroupid = listourse.FirstOrDefault(x => x.courseid.Equals(courseid.ToString())).coursegroupid;
            //วิชาทั้งหมดของ course   
            subject = LearningManagement.Getsubject(courseid);
            ViewBag.subject = subject;
            //วิชาที่เหลือ bookingid   
            subjectByBookingid = LearningManagement.GetsubjectByBookingid(bookingid);
            ViewBag.subjectByBookingid = subjectByBookingid;
            var newData = subject.Except(subjectByBookingid);
            //ListBookingDetail
            listBookingDetail = LearningManagement.GetbookingByid(bookingid);
            ViewBag.BookingDetail = listBookingDetail;
            //group by subjectid
            ViewBag.BookingGroupBySubject = listBookingDetail.GroupBy(x => new { x.subjectid }).Select(s => s.FirstOrDefault()).ToList();
            //ListBookingDetailOneRow
            listBookingDetailOneRow = LearningManagement.GetbookingByidOneRow(bookingid);
            //group by bookingid//ข้อมมูล booking 1 row
            ViewBag.Booking1row = listBookingDetailOneRow.GroupBy(x => new { x.bookingid }).Select(s => s.FirstOrDefault()).ToList();
            if (listBookingDetail.Count() != 0)
            {
                //เวลาหลักสูตรรวม
                ViewBag.coursetime = listBookingDetail.FirstOrDefault(x => x.bookingid.Equals(bookingid)).coursetime;
                //เวลาตารางเรียนรวม
                ViewBag.sumstudytime = listBookingDetail.FirstOrDefault(x => x.bookingid.Equals(bookingid)).sumstudytime;
            }
            else
            {
                ViewBag.coursetime = "0";
                ViewBag.sumstudytime = "1";
            }
            //ครูผู้สอน by courseid
            listteach = LearningManagement.GetteacherBycourseid(coursegroupid);
            ViewBag.teachbycourseid = listteach;
            if (arrteacher != null)
            {
                var arrteacherStr = arrteacher.Split(',');
                foreach (string item in arrteacherStr)
                {
                    listteachContact1 = listteach.Where(s => s.ind == item).ToList();
                    listteachselect = listteachselect.Concat(listteachContact1).ToList();

                }
                ViewBag.teach = listteachselect;
            }
            else
            {
                ViewBag.teach = listteach;
            }
            string dateprevious = SchoolManagement.getParamInfoEditDay(); //วันย้อนหลัง
            DateTime dateForButton = DateTime.Now.AddDays((Convert.ToInt32(dateprevious) * -1));
            ViewBag.previous = dateForButton.ToString("dd/MM/yyyy", new System.Globalization.CultureInfo("en-GB"));
            ViewBag.dateprevious = (Convert.ToInt32(dateprevious) * -1);
            //End ครูผู้สอน by courseid
            ViewBag.coursename = coursename;
            ViewBag.courseid = courseid;
            ViewBag.arrteacher = arrteacher;
            ViewBag.bookingid = bookingid;
            ViewBag.ActiveDate = (TempData["ActiveDate"] != null ? TempData["ActiveDate"].ToString() : "");
            ViewBag.ActiveView = (TempData["ActiveView"] != null ? TempData["ActiveView"].ToString() : "");
            //TempData["ActiveView"] = value.ViewCalendar;
            return View();
        }
        #endregion
        #region "selectTeachInTimetable"
        [NeedLogin]
        public ActionResult selectTeachInTimetable(CreateLearningModel value)
        {
            var teach = "";
            if (value.arrteacher != null)
            {
                teach = string.Join(",", value.arrteacher);
            }
            var value1 = (value.courseid != "") ? ENDEtxtManagement.Encrypt(value.courseid) : "";//courseid
            var value2 = (teach != "") ? ENDEtxtManagement.Encrypt(teach) : "";//teacherid
            var value3 = (value.bookingind != "") ? ENDEtxtManagement.Encrypt(value.bookingind) : "";//bookingid  
            return RedirectToAction("Timetable", "Learning", new { value1, value2, value3 });
        }
        #endregion
        #region "GetCalendar"
        [HttpPost]
        public JsonResult GetCalendar(string cid, string teacher, string bookingid)
        {
            var currentdate = DateTime.Now.ToString("yyyy-MM-dd", new System.Globalization.CultureInfo("en-GB"));
            List<CalendarModel> listCalendar1 = new List<CalendarModel>();
            List<CalendarModel> listCalendar2 = new List<CalendarModel>();
            if (teacher != "")
            {
                listCalendar1 = LearningManagement.ListBookingByteacher(cid, teacher);
            }
            else
            {
                listCalendar1 = LearningManagement.ListBookingByDate(currentdate);
            }
            if (bookingid != "")
            {
                listCalendar2 = LearningManagement.GetbookingByidCalendar(bookingid);
            }

            var result = listCalendar2.Concat(listCalendar1);
            var listCalendar = result.GroupBy(x => new { x.eventID }).Select(s => s.FirstOrDefault()).ToList();

            return Json(listCalendar, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region "GetByBookinginddetail"
        [HttpPost]
        public JsonResult GetByBookinginddetail(string ind, string bookingid)
        {
            var listBookingDetail = new List<BookingDetailModel>();
            var modal = new BookingDetailModel();
            listBookingDetail = LearningManagement.GetbookingByid(bookingid);
            modal = listBookingDetail.FirstOrDefault(x => x.ind.Equals(ind));
            return Json(modal, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region "InsertBookingDetail
        [NeedLogin]
        public ActionResult InsertBookingDetail(BookingDetailModel value)
        {
            var model = new ResultBookingModel();
            var resultset = new ResultBookingModel();
            var userData = Session["UserProfile"] as UserSessionModel;
            if (value.bookingdetailind == null)
            {
                value.bookingdetailind = "0";
            }
            model = LearningManagement.AddBookingDetail(value, userData.Username);

            if (model.result == "Y")
            {
                resultset = LearningManagement.SetStatusBooking(value.bookingid);
                if (resultset.result == "Y")
                {
                    TempData["Result"] = model.result;
                    TempData["Message"] = model.msg;
                }
                else
                {
                    TempData["Result"] = resultset.result;
                    TempData["Message"] = resultset.msg;
                }
                DateTime stDate = DateTime.ParseExact(value.studydate, "dd/MM/yyyy", new System.Globalization.CultureInfo("en-GB"));
                TempData["ActiveDate"] = stDate.ToString("yyyy-MM-dd", new System.Globalization.CultureInfo("en-GB"));
                TempData["ActiveView"] = value.ViewCalendar;

            }
            else
            {
                TempData["Result"] = model.result;
                TempData["Message"] = model.msg;

            }
            var value1 = (value.courseid != "") ? ENDEtxtManagement.Encrypt(value.courseid) : "";//courseid
            var value2 = (value.arrteacher != "") ? ENDEtxtManagement.Encrypt(value.arrteacher) : "";//teacherid
            var value3 = (value.bookingid != "") ? ENDEtxtManagement.Encrypt(value.bookingid) : "";//bookingid  
            return RedirectToAction("Timetable", "Learning", new { value1, value2, value3 });
        }
        #endregion
        #region "CheckRepeat
        [NeedLogin]
        public ActionResult CheckRepeat(BookingDetailModel value)
        {
            var check = new List<ResultCheckBdModel>();
            var list = new ReturnCheckBdModel();
            if (value.bookingdetailind == null)
            {
                value.bookingdetailind = "0";
            }
            check = LearningManagement.checkBookingdetail(value);

            if (check.Count() == 0)
            {
                list.result = "Y";
                list.resultlist = check;
            }
            else
            {
                var listCalendar = check.GroupBy(x => new { x.result }).Select(s => s.FirstOrDefault()).ToList();
                var reult = listCalendar.Where(s => s.result.Contains("Y")).ToList();
                if (reult.Count() != 0)
                {

                    list.result = "Y";
                    list.resultlist = check;

                }
                else
                {
                    list.result = "N";
                    list.resultlist = check;
                }
            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region "GetEditBookingDetail"
        [HttpPost]
        public JsonResult GetEditBookingDetail(string subjectid, string bookingid)
        {
            var subjectByBookingid = new List<lsubjectModel>();
            var subjectRemainById = new List<lsubjectModel>();

            subjectByBookingid = LearningManagement.GetsubjectByBookingid(bookingid);
            subjectRemainById = LearningManagement.GetlsubjectRemainById(bookingid, subjectid);
            var result = subjectRemainById.Concat(subjectByBookingid);
            var listsubject = result.GroupBy(x => new { x.subjectid }).Select(s => s.FirstOrDefault()).ToList();
            return Json(listsubject, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region "UpdateBookingDetail
        [NeedLogin]
        public ActionResult UpdateBookingDetail(BookingDetailModel value)
        {
            var model = new ResultBookingModel();
            var resultset = new ResultBookingModel();
            var userData = Session["UserProfile"] as UserSessionModel;
            model = LearningManagement.EditBookingDetail(value, userData.Username);
            DateTime stDate = DateTime.ParseExact(value.studydate, "dd/MM/yyyy", new System.Globalization.CultureInfo("en-GB"));
            TempData["ActiveDate"] = stDate.ToString("yyyy-MM-dd", new System.Globalization.CultureInfo("en-GB"));
            TempData["ActiveView"] = value.ViewCalendar;
            TempData["Result"] = model.result;
            TempData["Message"] = model.msg;
            var value1 = (value.courseid != "") ? ENDEtxtManagement.Encrypt(value.courseid) : "";//courseid
            var value2 = (value.arrteacher != "") ? ENDEtxtManagement.Encrypt(value.arrteacher) : "";//teacherid
            var value3 = (value.bookingid != "") ? ENDEtxtManagement.Encrypt(value.bookingid) : "";//bookingid  
            return RedirectToAction("Timetable", "Learning", new { value1, value2, value3 });
        }
        #endregion
        #region "DeteteBookingDetail"
        [NeedLogin]
        public ActionResult DeteteBookingDetail(string eventID, string arrteacher, string courseid, string bookingind)
        {
            var resultset = new ResultBookingModel();
            var model = new ResultBookingModel();
            model = LearningManagement.DeteteBookingDetail(eventID, bookingind);
            if (model.result == "Y")
            {
                resultset = LearningManagement.SetStatusBooking(bookingind);
                if (resultset.result == "Y")
                {
                    TempData["Result"] = model.result;
                    TempData["Message"] = model.msg;
                }
                else
                {
                    TempData["Result"] = resultset.result;
                    TempData["Message"] = resultset.msg;
                }

            }
            else
            {
                TempData["Result"] = model.result;
                TempData["Message"] = model.msg;
            }
            var value1 = (courseid != "") ? ENDEtxtManagement.Encrypt(courseid) : "";//courseid
            var value2 = (arrteacher != "") ? ENDEtxtManagement.Encrypt(arrteacher) : "";//arrteacher
            var value3 = (bookingind != "") ? ENDEtxtManagement.Encrypt(bookingind) : "";//bookingid  
            return RedirectToAction("Timetable", "Learning", new { value1, value2, value3 });
        }
        #endregion
        #region " EditBooking"
        [NeedLogin]
        public ActionResult EditBooking(string bookingname, string remark, string arrteacher, string courseid, string bookingind, string gear)
        {
            var userData = Session["UserProfile"] as UserSessionModel;
            var model = new ResultBookingModel();
            model = LearningManagement.EditBooking(bookingind, bookingname, remark, userData.Username, gear);
            TempData["Result"] = model.result;
            TempData["Message"] = model.msg;
            var value1 = (courseid != "") ? ENDEtxtManagement.Encrypt(courseid) : "";//courseid
            var value2 = (arrteacher != "") ? ENDEtxtManagement.Encrypt(arrteacher) : "";//teacherid
            var value3 = (bookingind != "") ? ENDEtxtManagement.Encrypt(bookingind) : "";//bookingid  
            return RedirectToAction("Timetable", "Learning", new { value1, value2, value3 });
        }
        #endregion
        #region "GetremainingTime"
        [NeedLogin]
        public ActionResult GetremainingTime(string bookingid, string courseid, string subjectid, string start, string end, string stdate, string subjecttype)
        {
            var listBookingDetail = new List<BookingDetailModel>();
            var model = new remainingTimeModel();
            var sumsubjecttime = "0";
            var TotalMinutes = "0";
            DateTime StartDate = DateTime.ParseExact(stdate, "dd/MM/yyyy", new System.Globalization.CultureInfo("en-US"));
            //DateTime StartDate = DateTime.Parse(stdate);
            string now = DateTime.Now.ToString("dd/MM/yyyy", new System.Globalization.CultureInfo("en-GB"));
            //DateTime EndDate = DateTime.Parse(now);
            DateTime EndDate = DateTime.ParseExact(now, "dd/MM/yyyy", new System.Globalization.CultureInfo("en-US"));
            TimeSpan result = StartDate - EndDate;
            string previous = SchoolManagement.getParamInfoEditDay();
            int sumdate = result.Days + Convert.ToInt32(previous);
            if (sumdate >= 0)
            {

                var strsubject = LearningManagement.Getsubject(courseid);//วิชา
                var subjectmin = strsubject.FirstOrDefault(x => x.subjectid.Equals(subjectid)).subjectmin;//หลักสูตร

                listBookingDetail = LearningManagement.GetbookingByid(bookingid);//ListBookingDetail

                //var CountSubject = listBookingDetail.Where(s => s.subjectid.Contains(strsubjectid[0])).ToList();
                var CountSubject = listBookingDetail.FirstOrDefault(i => i.subjectid == subjectid);
                //if (CountSubject.Count() != 0)
                if (CountSubject != null)
                {
                    var BookingGroupBySubject = listBookingDetail.GroupBy(x => new { x.subjectid }).Select(s => s.FirstOrDefault()).ToList();//group by subjectid
                    sumsubjecttime = BookingGroupBySubject.FirstOrDefault(x => x.subjectid.Equals(subjectid)).sumsubjecttime;//หลักสูตร
                }
                int remainingTemp = Convert.ToInt32(subjectmin) - Convert.ToInt32(sumsubjecttime);
                if (subjecttype == "T")
                {
                    TotalMinutes = remainingTemp.ToString();
                }
                else
                {
                    if (!end.Equals(""))
                    {
                        DateTime startTime = DateTime.ParseExact(start, "HH:mm", new System.Globalization.CultureInfo("en-GB"));
                        DateTime endTime = DateTime.ParseExact(end, "HH:mm", new System.Globalization.CultureInfo("en-GB"));
                        TimeSpan span = endTime.Subtract(startTime);
                        string resultMin = span.TotalMinutes.ToString();
                        TotalMinutes = (resultMin == "0") ? "-1" : resultMin;
                    }
                    else
                    {
                        TotalMinutes = remainingTemp.ToString();

                    }
                }

                int remainingTime = remainingTemp - Convert.ToInt32(TotalMinutes);
                model.TotalMinutes = TotalMinutes;
                model.remaining = remainingTime.ToString();
                model.remainingTemp = remainingTemp.ToString();
            }
            else
            {
                model.remainingDate = result.Days.ToString();
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region "GetremainingTimeEdit"
        [NeedLogin]
        public ActionResult GetremainingTimeEdit(string bookingid, string courseid, string subjectid, string start, string end, string stdate, string subjecttype, string studytime)
        {
            var listBookingDetail = new List<BookingDetailModel>();
            var model = new remainingTimeModel();
            var sumsubjecttime = "0";
            var TotalMinutes = "0";
            DateTime StartDate = DateTime.ParseExact(stdate, "dd/MM/yyyy", new System.Globalization.CultureInfo("en-US"));
            //DateTime StartDate = DateTime.Parse(stdate);
            string now = DateTime.Now.ToString("dd/MM/yyyy", new System.Globalization.CultureInfo("en-GB"));
            //DateTime EndDate = DateTime.Parse(now);
            DateTime EndDate = DateTime.ParseExact(now, "dd/MM/yyyy", new System.Globalization.CultureInfo("en-US"));
            TimeSpan result = StartDate - EndDate;
            string previous = SchoolManagement.getParamInfoEditDay();
            int sumdate = result.Days + Convert.ToInt32(previous);
            if (sumdate >= 0)
            {

                var strsubject = LearningManagement.Getsubject(courseid);//วิชา
                var subjectmin = strsubject.FirstOrDefault(x => x.subjectid.Equals(subjectid)).subjectmin;//หลักสูตร

                listBookingDetail = LearningManagement.GetbookingByid(bookingid);//ListBookingDetail

                //var CountSubject = listBookingDetail.Where(s => s.subjectid.Contains(strsubjectid[0])).ToList();
                var CountSubject = listBookingDetail.FirstOrDefault(i => i.subjectid == subjectid);
                //if (CountSubject.Count() != 0)
                if (CountSubject != null)
                {
                    var BookingGroupBySubject = listBookingDetail.GroupBy(x => new { x.subjectid }).Select(s => s.FirstOrDefault()).ToList();//group by subjectid
                    sumsubjecttime = BookingGroupBySubject.FirstOrDefault(x => x.subjectid.Equals(subjectid)).sumsubjecttime;//หลักสูตร
                }
                int remainingTemp = (Convert.ToInt32(subjectmin) - Convert.ToInt32(sumsubjecttime)) + Convert.ToInt32(studytime);
                int remainingTempCurrent = (Convert.ToInt32(subjectmin) - Convert.ToInt32(sumsubjecttime));
                if (subjecttype == "T")
                {
                    TotalMinutes = remainingTemp.ToString();
                }
                else
                {
                    if (!end.Equals(""))
                    {
                        DateTime startTime = DateTime.ParseExact(start, "HH:mm", new System.Globalization.CultureInfo("en-GB"));
                        DateTime endTime = DateTime.ParseExact(end, "HH:mm", new System.Globalization.CultureInfo("en-GB"));
                        TimeSpan span = endTime.Subtract(startTime);
                        string resultMin = span.TotalMinutes.ToString();
                        TotalMinutes = (resultMin == "0") ? "-1" : resultMin;
                    }
                    else
                    {
                        TotalMinutes = remainingTemp.ToString();

                    }
                }
                int remainingTime = remainingTemp - Convert.ToInt32(TotalMinutes);
                int Current = remainingTemp;
                model.TotalMinutes = TotalMinutes;
                model.remaining = remainingTime.ToString();
                model.remainingTemp = remainingTempCurrent.ToString();
                model.remainingCurrent = Current.ToString();
            }
            else
            {
                model.remainingDate = result.Days.ToString();
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region " AddBookingConfirmSet 3"
        [NeedLogin]
        public ActionResult AddBookingConfirm(string arrteacher, string courseid, string bookingind)
        {
            var model = new ResultBookingModel();
            var listBookingDetail = new List<BookingDetailModel>();
            var BookingDetail = new BookingDetailModel();
            listBookingDetail = LearningManagement.GetbookingByid(bookingind);
            var Detail = listBookingDetail.GroupBy(x => new { x.bookingid }).Select(s => s.FirstOrDefault()).ToList();
            var studentind = Detail.FirstOrDefault(x => x.bookingid.Equals(bookingind)).studentind;//studentind
            var voucherid = Detail.FirstOrDefault(x => x.bookingid.Equals(bookingind)).voucherid;//studentind


            model = LearningManagement.SetconfirmBooking(bookingind);
            if (model.result == "Y")
            {
                if (studentind == "0")
                {
                    TempData["BookingConfirm"] = model.result;
                    var value1 = (courseid != "") ? ENDEtxtManagement.Encrypt(courseid) : "";//courseid
                    var value2 = (arrteacher != "") ? ENDEtxtManagement.Encrypt(arrteacher) : "";//teacherid
                    var value3 = (bookingind != "") ? ENDEtxtManagement.Encrypt(bookingind) : "";//bookingid  
                    return RedirectToAction("Timetable", "Learning", new { value1, value2, value3 });
                }
                else
                {
                    StudentModel modelStudent = new StudentModel();
                    modelStudent.studentind = studentind;
                    modelStudent.voucherid = voucherid;
                    modelStudent.tabActive = "3";
                    return RedirectToAction("StudentDetail", "Student", modelStudent);
                }


            }
            else
            {
                TempData["Result"] = model.result;
                TempData["Message"] = model.msg;
                var value1 = (courseid != "") ? ENDEtxtManagement.Encrypt(courseid) : "";//courseid
                var value2 = (arrteacher != "") ? ENDEtxtManagement.Encrypt(arrteacher) : "";//teacherid
                var value3 = (bookingind != "") ? ENDEtxtManagement.Encrypt(bookingind) : "";//bookingid  
                return RedirectToAction("Timetable", "Learning", new { value1, value2, value3 });
            }

        }
        #endregion
        #region "SentToCreateStudent"
        [NeedLogin]
        public ActionResult SentToCreateStudent(string Studenttype, string bookingind, string bookingexamdate)
        {

            //var model = new ResultBookingModel();
            //TempData["Result"] = model.result;
            //TempData["Message"] = model.msg;
            var examdate = bookingexamdate;
            //var Studenttype = (sentStudenttype != "") ? ENDEtxtManagement.Encrypt(sentStudenttype) : "";//Studenttype
            var bid = (bookingind != "") ? ENDEtxtManagement.Encrypt(bookingind) : "";//bookingind  
            if (Studenttype != "2")
            {
                return RedirectToAction("Createstudent", "Student", new { Studenttype, bid });
            }
            else
            {
                return RedirectToAction("CreateForeignStudent", "Student", new { Studenttype, bid, examdate });
            }

        }
        #endregion
        #region "SetToTimetable"
        [NeedLogin]
        public ActionResult SetToTimetable(string courseid, string bookingind)
        {
            var value1 = (courseid != "") ? ENDEtxtManagement.Encrypt(courseid) : "";//courseid
            var value2 = "";//teacherid
            var value3 = (bookingind != "") ? ENDEtxtManagement.Encrypt(bookingind) : "";//bookingid  
            return RedirectToAction("Timetable", "Learning", new { value1, value2, value3 });
        }
        #endregion
        #region "SetBookingExamDate
        [NeedLogin]
        public ActionResult SetBookingExamDate(string bookingexamdate, string bookingid)
        {
            var model = new ResultBookingModel();

            model = LearningManagement.BookingSetExamDate(bookingexamdate, bookingid);

            return Json(model, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region " ClassroomTimetable"
        [NeedLogin]
        public ActionResult ClassroomTimetable()
        {
            ViewBag.Result = TempData["Result"];
            ViewBag.Message = TempData["Message"];
            var datenow = DateTime.Now;
            var userDt = datenow.ToString("yyyy-MM-dd", new System.Globalization.CultureInfo("en-GB"));
            var Dtdatenow = datenow.ToString("dd/MM/yyyy", new System.Globalization.CultureInfo("en-GB"));
            var currentdate = userDt;
            //var currentdate = "2018-11-28";
            if (TempData["liststudybydate"] != null)
            {
                ViewBag.liststudybydate = TempData["liststudybydate"];
            }
            else
            {

                ViewBag.liststudybydate = LearningManagement.liststudybydate("0", currentdate);
            }
            if (TempData["Classroomdate"] != null)
            {
                ViewBag.Classroomdate = TempData["Classroomdate"].ToString();
            }
            else
            {
                ViewBag.Classroomdate = Dtdatenow;
                //ViewBag.Classroomdate = "28/11/2018";
            }
            return View();
        }
        #endregion
        #region " SeachingTimeTeach"
        [NeedLogin]
        public ActionResult SeachingTimeTeach(string Classroomdate)
        {
            string strdate = "";
            string Classroomstrdate = "";
            DateTime stDate = DateTime.ParseExact(Classroomdate, "dd/MM/yyyy", new System.Globalization.CultureInfo("en-GB"));
            strdate = stDate.ToString("yyyy-MM-dd", new System.Globalization.CultureInfo("en-GB"));
            Classroomstrdate = stDate.ToString("dd/MM/yyy", new System.Globalization.CultureInfo("en-GB"));
            TempData["liststudybydate"] = LearningManagement.liststudybydate("0", strdate);
            TempData["Classroomdate"] = Classroomstrdate;
            return RedirectToAction("ClassroomTimetable", "Learning");
        }
        #endregion
        #region "GetstudybydatePreview"
        [HttpPost]
        public JsonResult GetstudybydatePreview(string currentdate, string subjectid, string studystime, string studyetime)
        {
            string strdate = "";
            var modal = new ListstudyModel();
            DateTime stDate = DateTime.ParseExact(currentdate, "dd/MM/yyyy", new System.Globalization.CultureInfo("en-GB"));
            strdate = stDate.ToString("yyyy-MM-dd", new System.Globalization.CultureInfo("en-GB"));
            modal = LearningManagement.liststudybysubjectid("1", strdate, subjectid, studystime, studyetime);
            return Json(modal, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}
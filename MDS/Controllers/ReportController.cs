using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using MDS.Reports.Receipt.ReceiptXSDTableAdapters;
using MDS.Reports.Student.StudentXSDTableAdapters;
using MDS.Reports.Deposit.DepositXSDTableAdapters;
using MDS.Reports.Appointment.AppointmentXSDTableAdapters;
using MDS.Reports.DepositAC.DepositACXSDTableAdapters;
using MDS.Reports.DepositACRevenue.DepositACRevenueXSDTableAdapters;
using System.Globalization;
using MDS.Models;
using MDS.DBExec;
using MDS.Fillter;

namespace MDS.Controllers
{
    public class ReportController : Controller
    {
        PaymentManagement PaymentManagement = new PaymentManagement();
        PublicManagement PublicManagement = new PublicManagement();
        ReportManagement ReportManagement = new ReportManagement();
        // GET: Report
        public ActionResult Index()
        {
            return View();
        }

        [NeedLogin]
        [HttpPost]
        public ActionResult Receipt(string flag, StudentModel model)
        {
            //string flag = "1";
            //string receiptno = "61-00-0001";
            //string To = "2018-12-01";
            string receiptno = PaymentManagement.GenReceipt(model);

            ReportViewer reportViewer = new ReportViewer();
            reportViewer.ProcessingMode = ProcessingMode.Local;
            //reportViewer.SizeToReportContent = true;
            reportViewer.ShowPrintButton = true;
            reportViewer.ShowExportControls = false;
            reportViewer.ShowFindControls = false;
            reportViewer.AsyncRendering = false;
            reportViewer.SizeToReportContent = true;
            reportViewer.ZoomMode = ZoomMode.Percent;

            //reportViewer.ShowExportControls = false;
            reportViewer.Width = Unit.Percentage(100);
            reportViewer.Height = Unit.Percentage(100);


            reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"\Reports\Receipt\Receipt.rdlc";
            sp_SCPrintReceipt11TableAdapter tbReceipt1 = new sp_SCPrintReceipt11TableAdapter();
            sp_SCPrintReceipt22TableAdapter tbReceipt2 = new sp_SCPrintReceipt22TableAdapter();
            sp_SCPrintReceipt33TableAdapter tbReceipt3 = new sp_SCPrintReceipt33TableAdapter();
            sp_SCPrintReceipt11TableAdapter tbReceipt4 = new sp_SCPrintReceipt11TableAdapter();

            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("ReceiptDataset1", (object)tbReceipt1.GetData(flag, receiptno, 0)));
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("ReceiptDataset2", (object)tbReceipt2.GetData(flag, receiptno, 0)));
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("ReceiptDataset3", (object)tbReceipt3.GetData(flag, receiptno, 0)));
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("ReceiptDataset4", (object)tbReceipt4.GetData("2", receiptno,0)));

            reportViewer.LocalReport.DisplayName = DateTime.Now.ToShortDateString() + "_ใบเสร็จรับเงิน";
            var UserData = Session["UserProfile"] as UserSessionModel;
            ReportParameter User = new ReportParameter("User", UserData.Username + ": " + UserData.UserFirstNameEN + " " + UserData.UserLastNameEN);
            ReportParameter datecurrent = new ReportParameter("Datecurrent", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss", new CultureInfo("th-TH")));
            reportViewer.LocalReport.SetParameters(new ReportParameter[] { User, datecurrent });

            ViewBag.ReportViewer = reportViewer;
            return View(model);
        }

        [NeedLogin]
        public ActionResult ReceiptSingle(string flag, string receiptno, string payid,string Studentind, string voucherid)
        {
            var pid = Int32.Parse(payid);
            ViewBag.Studentind = Studentind;
            ViewBag.Voucherid = voucherid;
            ReportViewer reportViewer = new ReportViewer();
            reportViewer.ProcessingMode = ProcessingMode.Local;
            reportViewer.ShowPrintButton = true;
            reportViewer.ShowExportControls = false;
            reportViewer.ShowFindControls = false;
            reportViewer.AsyncRendering = false;
            reportViewer.SizeToReportContent = true;
            reportViewer.ZoomMode = ZoomMode.Percent;
            reportViewer.Width = Unit.Percentage(100);
            reportViewer.Height = Unit.Percentage(100);

            //reportViewer.ShowZoomControl = false;

            reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"\Reports\ReceiptSingle\ReceiptSingle.rdlc";
            sp_SCPrintReceipt11TableAdapter tbReceipt1 = new sp_SCPrintReceipt11TableAdapter();
            sp_SCPrintReceipt22TableAdapter tbReceipt2 = new sp_SCPrintReceipt22TableAdapter();
            sp_SCPrintReceipt33TableAdapter tbReceipt3 = new sp_SCPrintReceipt33TableAdapter();

            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("ReceiptDataset1", (object)tbReceipt1.GetData(flag, receiptno, pid)));
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("ReceiptDataset2", (object)tbReceipt2.GetData(flag, receiptno, pid)));
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("ReceiptDataset3", (object)tbReceipt3.GetData(flag, receiptno, pid)));

            reportViewer.LocalReport.DisplayName = DateTime.Now.ToShortDateString() + "_ใบรับเงิน";
            var UserData = Session["UserProfile"] as UserSessionModel;
            ReportParameter User = new ReportParameter("User", UserData.Username + ": " + UserData.UserFirstNameEN + " " + UserData.UserLastNameEN);
            ReportParameter datecurrent = new ReportParameter("Datecurrent", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss", new CultureInfo("th-TH")));
            reportViewer.LocalReport.SetParameters(new ReportParameter[] { User, datecurrent });
            ViewBag.ReportViewer = reportViewer;
            return View();
        }

        [NeedLogin]
        public ActionResult Student(string studentind, string voucherid)
        {
            var ind = Int32.Parse(studentind);
            ViewBag.Studentind = studentind;
            ViewBag.Voucherid = voucherid;
            ReportViewer reportViewer = new ReportViewer();
            reportViewer.ProcessingMode = ProcessingMode.Local;
            reportViewer.ShowPrintButton = true;
            reportViewer.ShowExportControls = false;
            reportViewer.ShowFindControls = false;
            reportViewer.AsyncRendering = false;
            reportViewer.SizeToReportContent = true;
            reportViewer.ZoomMode = ZoomMode.Percent;
            reportViewer.Width = Unit.Percentage(100);
            reportViewer.Height = Unit.Percentage(100);

            //reportViewer.ShowZoomControl = false;

            reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"\Reports\Student\StudentRDLC.rdlc";
            sp_PrintStudentRegisTableAdapter tbStudent1 = new sp_PrintStudentRegisTableAdapter();
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("StudentDataSet", (object)tbStudent1.GetData(ind)));

            reportViewer.LocalReport.DisplayName = DateTime.Now.ToShortDateString() + "_ใบสมัครเรียน";
            var UserData = Session["UserProfile"] as UserSessionModel;
            ReportParameter User = new ReportParameter("User", UserData.Username + ": " + UserData.UserFirstNameEN + " " + UserData.UserLastNameEN);
            ReportParameter datecurrent = new ReportParameter("Datecurrent", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss", new CultureInfo("th-TH")));
            reportViewer.LocalReport.SetParameters(new ReportParameter[] { User, datecurrent });
            ViewBag.ReportViewer = reportViewer;
            return View();
        }

        [NeedLogin]
        public ActionResult DepositReport(string batchno,string batchdate)
        {
            //var Batchno = Int32.Parse(batchno);
            //ViewBag.Studentind = Studentind;
            //ViewBag.Voucherid = voucherid;
            ReportViewer reportViewer = new ReportViewer();
            reportViewer.ProcessingMode = ProcessingMode.Local;
            reportViewer.ShowPrintButton = true;
            reportViewer.ShowExportControls = false;
            reportViewer.ShowFindControls = false;
            reportViewer.AsyncRendering = false;
            reportViewer.SizeToReportContent = true;
            reportViewer.ZoomMode = ZoomMode.Percent;
            reportViewer.Width = Unit.Percentage(100);
            reportViewer.Height = Unit.Percentage(100);

            reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"\Reports\Deposit\DepositReport.rdlc";
            sp_SCDepositPrint2TableAdapter Deposit = new sp_SCDepositPrint2TableAdapter();
            sp_SCDepositPrint3TableAdapter Deposit3 = new sp_SCDepositPrint3TableAdapter();

            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("DepositDataSet", (object)Deposit.GetData(batchno)));
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("DepositDataSet1", (object)Deposit3.GetData(batchno)));

            reportViewer.LocalReport.DisplayName = DateTime.Now.ToShortDateString() + "_ใบนำฝาก";
            var UserData = Session["UserProfile"] as UserSessionModel;
            ReportParameter User = new ReportParameter("User", UserData.Username + ": " + UserData.UserFirstNameEN + " " + UserData.UserLastNameEN);
            ReportParameter Datecurrent = new ReportParameter("Datecurrent", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss", new CultureInfo("th-TH")));
            ReportParameter Batchdate = new ReportParameter("Batchdate", batchdate);
            reportViewer.LocalReport.SetParameters(new ReportParameter[] { User, Datecurrent, Batchdate });
            ViewBag.ReportViewer = reportViewer;
            return View();
        }

        
        [NeedLogin]
        public ActionResult AppointmentrReport(string sid, string qr)
        {
            int Studentid = Convert.ToInt32(ENDEtxtManagement.Decrypt(sid));
            ViewBag.Studentind = Studentid;

            //เอา str gen Base64
            string url = qr;
            string application = UrlHelper.GenerateUrl("Default", "Index", "Login", null, null, null, null,
                    System.Web.Routing.RouteTable.Routes, Request.RequestContext, false);
            string QRUrl = "http://" + Request.Url.Authority + application + "Public/Student?id=" + url;
            //string QRUrl = "http://" + Request.Url.Authority + Request.ApplicationPath + "/Public/Student?id=" + url;
            string imgBase64 = PublicManagement.Image(QRUrl);

            ReportViewer reportViewer = new ReportViewer();
            reportViewer.ProcessingMode = ProcessingMode.Local;
            reportViewer.ShowPrintButton = true;
            reportViewer.ShowExportControls = false;
            reportViewer.ShowFindControls = false;
            reportViewer.AsyncRendering = false;
            reportViewer.SizeToReportContent = true;
            reportViewer.ZoomMode = ZoomMode.Percent;
            reportViewer.Width = Unit.Percentage(100);
            reportViewer.Height = Unit.Percentage(100);

            reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"\Reports\Appointment\AppointmentReport.rdlc";
            //sp_SCPrintAppointment1TableAdapter Appointment1 = new sp_SCPrintAppointment1TableAdapter();
            //sp_SCPrintAppointment2TableAdapter Appointment2 = new sp_SCPrintAppointment2TableAdapter();
            sp_SCPrintAppointment3TableAdapter Appointment1 = new sp_SCPrintAppointment3TableAdapter();
            sp_SCPrintAppointment4TableAdapter Appointment2 = new sp_SCPrintAppointment4TableAdapter();

            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("AppointmentDataSet1", (object)Appointment1.GetData(Studentid, imgBase64)));
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("AppointmentDataSet2", (object)Appointment2.GetData(Studentid)));

            reportViewer.LocalReport.DisplayName = DateTime.Now.ToShortDateString() + "_ใบนัด";
            var UserData = Session["UserProfile"] as UserSessionModel;
            ReportParameter User = new ReportParameter("User", UserData.Username + ": " + UserData.UserFirstNameEN + " " + UserData.UserLastNameEN);
            ReportParameter Datecurrent = new ReportParameter("Datecurrent", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss", new CultureInfo("th-TH")));
            ReportParameter QrBase64 = new ReportParameter("QrBase64", imgBase64);
            reportViewer.LocalReport.SetParameters(new ReportParameter[] { User, Datecurrent, QrBase64});
            ViewBag.ReportViewer = reportViewer;
            return View();
        }

        [NeedLogin]
        public ActionResult DepositAC()
        {
            var model = ReportManagement.GetCourseList();
            ViewBag.Fdate = model.fdate;
            ViewBag.Tdate = model.tdate;
            ReportViewer reportViewer = new ReportViewer();
            reportViewer.ProcessingMode = ProcessingMode.Local;
            reportViewer.ShowPrintButton = true;
            reportViewer.ShowExportControls = true;
            reportViewer.ShowFindControls = false;
            reportViewer.AsyncRendering = false;
            reportViewer.SizeToReportContent = true;
            reportViewer.ZoomMode = ZoomMode.Percent;
            reportViewer.Width = Unit.Percentage(100);
            reportViewer.Height = Unit.Percentage(100);

            reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"\Reports\DepositAC\DepositACReport.rdlc";
            sp_SCDepositPrintACTableAdapter DepositAC = new sp_SCDepositPrintACTableAdapter();

            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("DepositACDataset", (object)DepositAC.GetData(model.fdate, model.tdate)));

            reportViewer.LocalReport.DisplayName = DateTime.Now.ToShortDateString() + "_รายงานสรุปเงินมัดจำ";
            var UserData = Session["UserProfile"] as UserSessionModel;
            ReportParameter User = new ReportParameter("User", UserData.Username + ": " + UserData.UserFirstNameEN + " " + UserData.UserLastNameEN);
            ReportParameter Datecurrent = new ReportParameter("Datecurrent", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss", new CultureInfo("th-TH")));
            reportViewer.LocalReport.SetParameters(new ReportParameter[] { User, Datecurrent });
            ViewBag.ReportViewer = reportViewer;
            return View();
        }
        [NeedLogin]
        public ActionResult DepositACReport(FormCollection form)
        {
            string fdate = form["fdate"].ToString();
            string tdate = form["tdate"].ToString();
            ReportViewer reportViewer = new ReportViewer();
            reportViewer.ProcessingMode = ProcessingMode.Local;
            reportViewer.ShowPrintButton = true;
            reportViewer.ShowExportControls = true;
            reportViewer.ShowFindControls = false;
            reportViewer.AsyncRendering = false;
            reportViewer.SizeToReportContent = true;
            reportViewer.ZoomMode = ZoomMode.Percent;
            reportViewer.Width = Unit.Percentage(100);
            reportViewer.Height = Unit.Percentage(100);

            reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"\Reports\DepositAC\DepositACReport.rdlc";
            sp_SCDepositPrintACTableAdapter DepositAC = new sp_SCDepositPrintACTableAdapter();

            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("DepositACDataset", (object)DepositAC.GetData(fdate, tdate)));

            reportViewer.LocalReport.DisplayName = DateTime.Now.ToShortDateString() + "_รายงานสรุปเงินมัดจำ";
            var UserData = Session["UserProfile"] as UserSessionModel;
            ReportParameter User = new ReportParameter("User", UserData.Username + ": " + UserData.UserFirstNameEN + " " + UserData.UserLastNameEN);
            ReportParameter Datecurrent = new ReportParameter("Datecurrent", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss", new CultureInfo("th-TH")));
            reportViewer.LocalReport.SetParameters(new ReportParameter[] { User, Datecurrent});
            ViewBag.ReportViewer = reportViewer;

            //TempData["ReportViewer"] = ViewBag.ReportViewer;
            //TempData["fdate"] = fdate;
            //TempData["tdate"] = tdate;
            //return RedirectToAction("DepositAC");
            ViewBag.Fdate = fdate;
            ViewBag.Tdate = tdate;
            return View("DepositAC");
        }

        [NeedLogin]
        public ActionResult DepositACRevenue()
        {
            var model = ReportManagement.GetCourseList();
            //model.fdate = "2018-10-01";
            //model.tdate = "2018-12-30";
            ViewBag.Fdate = model.fdate;
            ViewBag.Tdate = model.tdate;
            ReportViewer reportViewer = new ReportViewer();
            reportViewer.ProcessingMode = ProcessingMode.Local;
            reportViewer.ShowPrintButton = true;
            reportViewer.ShowExportControls = true;
            reportViewer.ShowFindControls = false;
            reportViewer.AsyncRendering = false;
            reportViewer.SizeToReportContent = true;
            reportViewer.ZoomMode = ZoomMode.Percent;
            reportViewer.Width = Unit.Percentage(100);
            reportViewer.Height = Unit.Percentage(100);

            reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"\Reports\DepositACRevenue\DepositACRevenue.rdlc";
            sp_SCDepositPrintACRevenueTableAdapter DepositACRevenue = new sp_SCDepositPrintACRevenueTableAdapter();

            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("DepositACRevenueDataSet", (object)DepositACRevenue.GetData(model.fdate, model.tdate)));

            reportViewer.LocalReport.DisplayName = DateTime.Now.ToShortDateString() + "_รายงานรับรู้รายได้";
            var UserData = Session["UserProfile"] as UserSessionModel;
            ReportParameter User = new ReportParameter("User", UserData.Username + ": " + UserData.UserFirstNameEN + " " + UserData.UserLastNameEN);
            ReportParameter Datecurrent = new ReportParameter("Datecurrent", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss", new CultureInfo("th-TH")));
            reportViewer.LocalReport.SetParameters(new ReportParameter[] { User, Datecurrent });
            ViewBag.ReportViewer = reportViewer;
            return View();
        }
        [NeedLogin]
        public ActionResult DepositACRevenueReport(FormCollection form)
        {
            string fdate = form["fdate"].ToString();
            string tdate = form["tdate"].ToString();
            ReportViewer reportViewer = new ReportViewer();
            reportViewer.ProcessingMode = ProcessingMode.Local;
            reportViewer.ShowPrintButton = true;
            reportViewer.ShowExportControls = true;
            reportViewer.ShowFindControls = false;
            reportViewer.AsyncRendering = false;
            reportViewer.SizeToReportContent = true;
            reportViewer.ZoomMode = ZoomMode.Percent;
            reportViewer.Width = Unit.Percentage(100);
            reportViewer.Height = Unit.Percentage(100);

            reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"\Reports\DepositACRevenue\DepositACRevenue.rdlc";
            sp_SCDepositPrintACRevenueTableAdapter DepositACRevenue = new sp_SCDepositPrintACRevenueTableAdapter();

            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("DepositACRevenueDataSet", (object)DepositACRevenue.GetData(fdate, tdate)));

            reportViewer.LocalReport.DisplayName = DateTime.Now.ToShortDateString() + "_รายงานรับรู้รายได้";
            var UserData = Session["UserProfile"] as UserSessionModel;
            ReportParameter User = new ReportParameter("User", UserData.Username + ": " + UserData.UserFirstNameEN + " " + UserData.UserLastNameEN);
            ReportParameter Datecurrent = new ReportParameter("Datecurrent", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss", new CultureInfo("th-TH")));
            reportViewer.LocalReport.SetParameters(new ReportParameter[] { User, Datecurrent });
            ViewBag.ReportViewer = reportViewer;

            //TempData["ReportViewer"] = ViewBag.ReportViewer;
            //TempData["fdate"] = fdate;
            //TempData["tdate"] = tdate;
            //return RedirectToAction("DepositAC");
            ViewBag.Fdate = fdate;
            ViewBag.Tdate = tdate;
            return View("DepositACRevenue");
        }
    }
}
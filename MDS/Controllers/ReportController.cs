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
using MDS.Reports.Channel.ChannelXSDTableAdapters;
using MDS.Reports.TeacherHours.TeacherHoursXSDTableAdapters;
using System.Globalization;
using MDS.Models;
using MDS.DBExec;
using MDS.Fillter;
using MDS.Reports.ReportStudent.ReportStudentXSDTableAdapters;
using MDS.Reports.Revenue.RevenueXSDTableAdapters;
using MDS.Reports.TestTakersName.TestTakersNameXSDTableAdapters;

namespace MDS.Controllers
{
    public class ReportController : Controller
    {
        PaymentManagement PaymentManagement = new PaymentManagement();
        PublicManagement PublicManagement = new PublicManagement();
        ReportManagement ReportManagement = new ReportManagement();
        SchoolManagement SchoolManagement = new SchoolManagement();
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

            reportViewer.LocalReport.EnableExternalImages = true;
            var company = Session["Company"] as CompanyModel;
            ReportParameter SchoolLogo = new ReportParameter("SchoolLogo", new Uri(Server.MapPath("~/" + company.SchoolLogo)).AbsoluteUri);
            ReportParameter SchoolName = new ReportParameter("SchoolName", company.SchoolName);
            ReportParameter SchoolAdd1 = new ReportParameter("SchoolAddr1", company.schoolAddr1);
            ReportParameter SchoolAdd2 = new ReportParameter("SchoolAddr2", company.schoolAddr2);
            ReportParameter SchoolAdd3 = new ReportParameter("SchoolAddr3", company.schoolAddr3);
            reportViewer.LocalReport.SetParameters(new ReportParameter[] { User, datecurrent, SchoolLogo, SchoolName, SchoolAdd1, SchoolAdd2, SchoolAdd3 });

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

            reportViewer.LocalReport.EnableExternalImages = true;
            var company = Session["Company"] as CompanyModel;
            ReportParameter SchoolLogo = new ReportParameter("SchoolLogo", new Uri(Server.MapPath("~/" + company.SchoolLogo)).AbsoluteUri);
            ReportParameter SchoolName = new ReportParameter("SchoolName", company.SchoolName);
            ReportParameter SchoolAdd1 = new ReportParameter("SchoolAddr1", company.schoolAddr1);
            ReportParameter SchoolAdd2 = new ReportParameter("SchoolAddr2", company.schoolAddr2);
            ReportParameter SchoolAdd3 = new ReportParameter("SchoolAddr3", company.schoolAddr3);
            reportViewer.LocalReport.SetParameters(new ReportParameter[] { User, datecurrent , SchoolLogo, SchoolName , SchoolAdd1 , SchoolAdd2 , SchoolAdd3 });
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

            reportViewer.LocalReport.EnableExternalImages = true;
            var Company = Session["Company"] as CompanyModel;
            ReportParameter SchoolLogo = new ReportParameter("SchoolLogo", new Uri(Server.MapPath("~/" + Company.SchoolLogo)).AbsoluteUri);
            reportViewer.LocalReport.SetParameters(new ReportParameter[] { User, datecurrent, SchoolLogo});
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
            reportViewer.LocalReport.EnableExternalImages = true;
            var company = Session["Company"] as CompanyModel;
            ReportParameter SchoolLogo = new ReportParameter("SchoolLogo", new Uri(Server.MapPath("~/" + company.SchoolLogo)).AbsoluteUri);
            ReportParameter SchoolName = new ReportParameter("SchoolName", company.SchoolName);
            ReportParameter SchoolAdd1 = new ReportParameter("SchoolAddr1", company.schoolAddr1);
            ReportParameter SchoolAdd2 = new ReportParameter("SchoolAddr2", company.schoolAddr2);
            ReportParameter SchoolAdd3 = new ReportParameter("SchoolAddr3", company.schoolAddr3);
            reportViewer.LocalReport.SetParameters(new ReportParameter[] { User, Datecurrent , Batchdate , SchoolLogo, SchoolName, SchoolAdd1, SchoolAdd2, SchoolAdd3 });
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

            var company = Session["Company"] as CompanyModel;
            string imgBase64 = PublicManagement.Image(QRUrl, company.SchoolLogo);

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

            reportViewer.LocalReport.EnableExternalImages = true;
            ReportParameter SchoolLogo = new ReportParameter("SchoolLogo", new Uri(Server.MapPath("~/" + company.SchoolLogo)).AbsoluteUri);
            reportViewer.LocalReport.SetParameters(new ReportParameter[] { User, Datecurrent, QrBase64 , SchoolLogo });
            ViewBag.ReportViewer = reportViewer;
            return View();
        }

        [NeedLogin]
        public ActionResult DepositAC()
        {
            var model = ReportManagement.GetCurrentDate();
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
            reportViewer.LocalReport.EnableExternalImages = true;
            var company = Session["Company"] as CompanyModel;
            ReportParameter SchoolLogo = new ReportParameter("SchoolLogo", new Uri(Server.MapPath("~/" + company.SchoolLogo)).AbsoluteUri);
            ReportParameter SchoolName = new ReportParameter("SchoolName", company.SchoolName);
            ReportParameter SchoolAdd1 = new ReportParameter("SchoolAddr1", company.schoolAddr1);
            ReportParameter SchoolAdd2 = new ReportParameter("SchoolAddr2", company.schoolAddr2);
            ReportParameter SchoolAdd3 = new ReportParameter("SchoolAddr3", company.schoolAddr3);
            reportViewer.LocalReport.SetParameters(new ReportParameter[] { User, Datecurrent, SchoolLogo, SchoolName, SchoolAdd1, SchoolAdd2, SchoolAdd3 });
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
            reportViewer.LocalReport.EnableExternalImages = true;
            var company = Session["Company"] as CompanyModel;
            ReportParameter SchoolLogo = new ReportParameter("SchoolLogo", new Uri(Server.MapPath("~/" + company.SchoolLogo)).AbsoluteUri);
            ReportParameter SchoolName = new ReportParameter("SchoolName", company.SchoolName);
            ReportParameter SchoolAdd1 = new ReportParameter("SchoolAddr1", company.schoolAddr1);
            ReportParameter SchoolAdd2 = new ReportParameter("SchoolAddr2", company.schoolAddr2);
            ReportParameter SchoolAdd3 = new ReportParameter("SchoolAddr3", company.schoolAddr3);
            reportViewer.LocalReport.SetParameters(new ReportParameter[] { User, Datecurrent, SchoolLogo, SchoolName, SchoolAdd1, SchoolAdd2, SchoolAdd3 });
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
            var model = ReportManagement.GetCurrentDate();
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
            reportViewer.LocalReport.EnableExternalImages = true;
            var company = Session["Company"] as CompanyModel;
            ReportParameter SchoolLogo = new ReportParameter("SchoolLogo", new Uri(Server.MapPath("~/" + company.SchoolLogo)).AbsoluteUri);
            ReportParameter SchoolName = new ReportParameter("SchoolName", company.SchoolName);
            ReportParameter SchoolAdd1 = new ReportParameter("SchoolAddr1", company.schoolAddr1);
            ReportParameter SchoolAdd2 = new ReportParameter("SchoolAddr2", company.schoolAddr2);
            ReportParameter SchoolAdd3 = new ReportParameter("SchoolAddr3", company.schoolAddr3);
            reportViewer.LocalReport.SetParameters(new ReportParameter[] { User, Datecurrent, SchoolLogo, SchoolName, SchoolAdd1, SchoolAdd2, SchoolAdd3 });
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
            reportViewer.LocalReport.EnableExternalImages = true;
            var company = Session["Company"] as CompanyModel;
            ReportParameter SchoolLogo = new ReportParameter("SchoolLogo", new Uri(Server.MapPath("~/" + company.SchoolLogo)).AbsoluteUri);
            ReportParameter SchoolName = new ReportParameter("SchoolName", company.SchoolName);
            ReportParameter SchoolAdd1 = new ReportParameter("SchoolAddr1", company.schoolAddr1);
            ReportParameter SchoolAdd2 = new ReportParameter("SchoolAddr2", company.schoolAddr2);
            ReportParameter SchoolAdd3 = new ReportParameter("SchoolAddr3", company.schoolAddr3);
            reportViewer.LocalReport.SetParameters(new ReportParameter[] { User, Datecurrent, SchoolLogo, SchoolName, SchoolAdd1, SchoolAdd2, SchoolAdd3 });
            ViewBag.ReportViewer = reportViewer;

            //TempData["ReportViewer"] = ViewBag.ReportViewer;
            //TempData["fdate"] = fdate;
            //TempData["tdate"] = tdate;
            //return RedirectToAction("DepositAC");
            ViewBag.Fdate = fdate;
            ViewBag.Tdate = tdate;
            return View("DepositACRevenue");
        }

        [NeedLogin]
        public ActionResult ChannelReport()
        {
            var model = ReportManagement.GetCurrentDate();
            ViewBag.Fdate = model.fdate;
            ViewBag.Tdate = model.tdate;
            string dtype = "R";
            ViewBag.datetype = dtype;
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

            reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"\Reports\Channel\ChannelReport.rdlc";
            sp_reportchannelTableAdapter Channel = new sp_reportchannelTableAdapter();

            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("ChannelDataset", (object)Channel.GetData(model.fdate, model.tdate, dtype)));

            reportViewer.LocalReport.DisplayName = DateTime.Now.ToShortDateString() + "_รายงานช่องทาง";
            var UserData = Session["UserProfile"] as UserSessionModel;
            ReportParameter User = new ReportParameter("User", UserData.Username + ": " + UserData.UserFirstNameEN + " " + UserData.UserLastNameEN);
            ReportParameter Datecurrent = new ReportParameter("Datecurrent", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss", new CultureInfo("th-TH")));
            reportViewer.LocalReport.EnableExternalImages = true;
            var company = Session["Company"] as CompanyModel;
            ReportParameter SchoolLogo = new ReportParameter("SchoolLogo", new Uri(Server.MapPath("~/" + company.SchoolLogo)).AbsoluteUri);
            ReportParameter SchoolName = new ReportParameter("SchoolName", company.SchoolName);
            ReportParameter SchoolAdd1 = new ReportParameter("SchoolAddr1", company.schoolAddr1);
            ReportParameter SchoolAdd2 = new ReportParameter("SchoolAddr2", company.schoolAddr2);
            ReportParameter SchoolAdd3 = new ReportParameter("SchoolAddr3", company.schoolAddr3);
            ReportParameter Fdate = new ReportParameter("Fdate", model.fdate);
            ReportParameter Tdate = new ReportParameter("Tdate", model.tdate);
            string DatetypeStr = string.Empty;
            if (dtype == "R")
            {
                DatetypeStr = "วันที่สมัคร";
            }
            else
            {
                DatetypeStr = "วันที่จบหลักสูตร";
            }
            ReportParameter Datetype = new ReportParameter("DatetypeStr", DatetypeStr);
            reportViewer.LocalReport.SetParameters(new ReportParameter[] { User, Datecurrent, SchoolLogo, SchoolName, SchoolAdd1, SchoolAdd2, SchoolAdd3 , Fdate , Tdate, Datetype });
            ViewBag.ReportViewer = reportViewer;
            return View();
        }
        [NeedLogin]
        public ActionResult ChannelSearchReport(FormCollection form)
        {
            string fdate = form["fdate"].ToString();
            string tdate = form["tdate"].ToString();
            string dtype = form["dtype"].ToString();
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

            reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"\Reports\Channel\ChannelReport.rdlc";
            sp_reportchannelTableAdapter Channel = new sp_reportchannelTableAdapter();

            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("ChannelDataset", (object)Channel.GetData(fdate, tdate, dtype)));

            reportViewer.LocalReport.DisplayName = DateTime.Now.ToShortDateString() + "_รายงานช่องทาง";
            var UserData = Session["UserProfile"] as UserSessionModel;
            ReportParameter User = new ReportParameter("User", UserData.Username + ": " + UserData.UserFirstNameEN + " " + UserData.UserLastNameEN);
            ReportParameter Datecurrent = new ReportParameter("Datecurrent", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss", new CultureInfo("th-TH")));
            reportViewer.LocalReport.EnableExternalImages = true;
            var company = Session["Company"] as CompanyModel;
            ReportParameter SchoolLogo = new ReportParameter("SchoolLogo", new Uri(Server.MapPath("~/" + company.SchoolLogo)).AbsoluteUri);
            ReportParameter SchoolName = new ReportParameter("SchoolName", company.SchoolName);
            ReportParameter SchoolAdd1 = new ReportParameter("SchoolAddr1", company.schoolAddr1);
            ReportParameter SchoolAdd2 = new ReportParameter("SchoolAddr2", company.schoolAddr2);
            ReportParameter SchoolAdd3 = new ReportParameter("SchoolAddr3", company.schoolAddr3);
            ReportParameter Fdate = new ReportParameter("Fdate", fdate);
            ReportParameter Tdate = new ReportParameter("Tdate", tdate);
            string DatetypeStr = string.Empty;
            if (dtype == "R")
            {
                DatetypeStr = "วันที่สมัคร";
            }
            else
            {
                DatetypeStr = "วันที่จบหลักสูตร";
            }
            ReportParameter Datetype = new ReportParameter("DatetypeStr", DatetypeStr);
            reportViewer.LocalReport.SetParameters(new ReportParameter[] { User, Datecurrent, SchoolLogo, SchoolName, SchoolAdd1, SchoolAdd2, SchoolAdd3, Fdate, Tdate, Datetype });
            ViewBag.ReportViewer = reportViewer;
            ViewBag.Fdate = fdate;
            ViewBag.Tdate = tdate;
            ViewBag.datetype = dtype;
            return View("ChannelReport");
        }

        [NeedLogin]
        public ActionResult TeacherHoursReport()
        {
            var model = ReportManagement.GetCurrentDate();
            ViewBag.Fdate = model.fdate;
            ViewBag.Tdate = model.tdate;
            string subjecttype = "A";
            ViewBag.SubType = subjecttype;

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

            reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"\Reports\TeacherHours\TeacherHoursReport.rdlc";
            sp_reportTeacherHoursTableAdapter TeacherHours = new sp_reportTeacherHoursTableAdapter();

            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("TeacherHoursDataset", (object)TeacherHours.GetData(model.fdate, model.tdate, subjecttype)));

            reportViewer.LocalReport.DisplayName = DateTime.Now.ToShortDateString() + "_รายงานรายงานจำนวน_ชม_ครู";
            var UserData = Session["UserProfile"] as UserSessionModel;
            ReportParameter User = new ReportParameter("User", UserData.Username + ": " + UserData.UserFirstNameEN + " " + UserData.UserLastNameEN);
            ReportParameter Datecurrent = new ReportParameter("Datecurrent", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss", new CultureInfo("th-TH")));
            reportViewer.LocalReport.EnableExternalImages = true;
            var company = Session["Company"] as CompanyModel;
            ReportParameter SchoolLogo = new ReportParameter("SchoolLogo", new Uri(Server.MapPath("~/" + company.SchoolLogo)).AbsoluteUri);
            ReportParameter SchoolName = new ReportParameter("SchoolName", company.SchoolName);
            ReportParameter SchoolAdd1 = new ReportParameter("SchoolAddr1", company.schoolAddr1);
            ReportParameter SchoolAdd2 = new ReportParameter("SchoolAddr2", company.schoolAddr2);
            ReportParameter SchoolAdd3 = new ReportParameter("SchoolAddr3", company.schoolAddr3);
            ReportParameter Fdate = new ReportParameter("Fdate", model.fdate);
            ReportParameter Tdate = new ReportParameter("Tdate", model.tdate);
            string type = string.Empty;
            if (subjecttype == "A")
            {
                type = "ทั้งหมด";
            }
            else if(subjecttype == "T")
            {
                type = "ทฤษฎี";
            }
            else if (subjecttype == "P")
            {
                type = "ปฏิบัติ";
            }
            ReportParameter SubjectType = new ReportParameter("SubjectType", type);
            reportViewer.LocalReport.SetParameters(new ReportParameter[] { User, Datecurrent, SchoolLogo, SchoolName, SchoolAdd1, SchoolAdd2, SchoolAdd3, Fdate, Tdate , SubjectType });
            ViewBag.ReportViewer = reportViewer;
            return View();
        }
        [NeedLogin]
        public ActionResult TeacherHoursSearchReport(FormCollection form)
        {
            string fdate = form["fdate"].ToString();
            string tdate = form["tdate"].ToString();
            string subjecttype = form["SubjectType"].ToString();
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

            reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"\Reports\TeacherHours\TeacherHoursReport.rdlc";
            sp_reportTeacherHoursTableAdapter TeacherHours = new sp_reportTeacherHoursTableAdapter();

            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("TeacherHoursDataset", (object)TeacherHours.GetData(fdate, tdate, subjecttype)));

            reportViewer.LocalReport.DisplayName = DateTime.Now.ToShortDateString() + "_รายงานรายงานจำนวน_ชม_ครู";
            var UserData = Session["UserProfile"] as UserSessionModel;
            ReportParameter User = new ReportParameter("User", UserData.Username + ": " + UserData.UserFirstNameEN + " " + UserData.UserLastNameEN);
            ReportParameter Datecurrent = new ReportParameter("Datecurrent", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss", new CultureInfo("th-TH")));
            reportViewer.LocalReport.EnableExternalImages = true;
            var company = Session["Company"] as CompanyModel;
            ReportParameter SchoolLogo = new ReportParameter("SchoolLogo", new Uri(Server.MapPath("~/" + company.SchoolLogo)).AbsoluteUri);
            ReportParameter SchoolName = new ReportParameter("SchoolName", company.SchoolName);
            ReportParameter SchoolAdd1 = new ReportParameter("SchoolAddr1", company.schoolAddr1);
            ReportParameter SchoolAdd2 = new ReportParameter("SchoolAddr2", company.schoolAddr2);
            ReportParameter SchoolAdd3 = new ReportParameter("SchoolAddr3", company.schoolAddr3);
            ReportParameter Fdate = new ReportParameter("Fdate", fdate);
            ReportParameter Tdate = new ReportParameter("Tdate", tdate);
            string type = string.Empty;
            if (subjecttype == "A")
            {
                type = "ทั้งหมด";
            }
            else if (subjecttype == "T")
            {
                type = "ทฤษฎี";
            }
            else if (subjecttype == "P")
            {
                type = "ปฏิบัติ";
            }
            ReportParameter SubjectType = new ReportParameter("SubjectType", type);
            reportViewer.LocalReport.SetParameters(new ReportParameter[] { User, Datecurrent, SchoolLogo, SchoolName, SchoolAdd1, SchoolAdd2, SchoolAdd3, Fdate, Tdate, SubjectType });
            ViewBag.ReportViewer = reportViewer;
            ViewBag.Fdate = fdate;
            ViewBag.Tdate = tdate;
            ViewBag.SubType = subjecttype;
            return View("TeacherHoursReport");
        }

        // GET: ReportStudent
        [NeedLogin]
        public ActionResult ReportStudent()
        {
            var model = ReportManagement.GetCurrentDate();
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

            reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"\Reports\ReportStudent\ReportStudentRDLC.rdlc";
            sp_reportStudentTableAdapter ReportStudent = new sp_reportStudentTableAdapter();

            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("StudentDataset", (object)ReportStudent.GetData(model.fdate, model.tdate)));

            reportViewer.LocalReport.DisplayName = DateTime.Now.ToShortDateString() + "_รายงานสรุปจำนวนนักเรียน";

            var UserData = Session["UserProfile"] as UserSessionModel;
            ReportParameter User = new ReportParameter("User", UserData.Username + ": " + UserData.UserFirstNameEN + " " + UserData.UserLastNameEN);
            ReportParameter Datecurrent = new ReportParameter("Datecurrent", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss", new CultureInfo("th-TH")));
            reportViewer.LocalReport.EnableExternalImages = true;

            var company = Session["Company"] as CompanyModel;
            ReportParameter SchoolLogo = new ReportParameter("SchoolLogo", new Uri(Server.MapPath("~/" + company.SchoolLogo)).AbsoluteUri);
            ReportParameter SchoolName = new ReportParameter("SchoolName", company.SchoolName);
            ReportParameter SchoolAddr1 = new ReportParameter("SchoolAddr1", company.schoolAddr1);
            ReportParameter SchoolAddr2 = new ReportParameter("SchoolAddr2", company.schoolAddr2);
            ReportParameter SchoolAddr3 = new ReportParameter("SchoolAddr3", company.schoolAddr3);

            ReportParameter Fdate = new ReportParameter("Fdate", model.fdate);
            ReportParameter Tdate = new ReportParameter("Tdate", model.tdate);

            reportViewer.LocalReport.SetParameters(new ReportParameter[] { User, Datecurrent, SchoolLogo, SchoolName, SchoolAddr1, SchoolAddr2, SchoolAddr3, Fdate, Tdate });
            ViewBag.ReportViewer = reportViewer;
            return View();
        }

        [NeedLogin]
        public ActionResult SumReportStudent(FormCollection form)
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

            reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"\Reports\ReportStudent\ReportStudentRDLC.rdlc";
            sp_reportStudentTableAdapter ReportStudent = new sp_reportStudentTableAdapter();

            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("StudentDataset", (object)ReportStudent.GetData(fdate, tdate)));

            reportViewer.LocalReport.DisplayName = DateTime.Now.ToShortDateString() + "_รายงานสรุปจำนวนนักเรียน";

            var UserData = Session["UserProfile"] as UserSessionModel;
            ReportParameter User = new ReportParameter("User", UserData.Username + ": " + UserData.UserFirstNameEN + " " + UserData.UserLastNameEN);
            ReportParameter Datecurrent = new ReportParameter("Datecurrent", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss", new CultureInfo("th-TH")));
            reportViewer.LocalReport.EnableExternalImages = true;

            var company = Session["Company"] as CompanyModel;
            ReportParameter SchoolLogo = new ReportParameter("SchoolLogo", new Uri(Server.MapPath("~/" + company.SchoolLogo)).AbsoluteUri);
            ReportParameter SchoolName = new ReportParameter("SchoolName", company.SchoolName);
            ReportParameter SchoolAddr1 = new ReportParameter("SchoolAddr1", company.schoolAddr1);
            ReportParameter SchoolAddr2 = new ReportParameter("SchoolAddr2", company.schoolAddr2);
            ReportParameter SchoolAddr3 = new ReportParameter("SchoolAddr3", company.schoolAddr3);

            ReportParameter Fdate = new ReportParameter("Fdate", fdate);
            ReportParameter Tdate = new ReportParameter("Tdate", tdate);

            reportViewer.LocalReport.SetParameters(new ReportParameter[] { User, Datecurrent, SchoolLogo, SchoolName, SchoolAddr1, SchoolAddr2, SchoolAddr3, Fdate, Tdate });
            ViewBag.ReportViewer = reportViewer;

            ViewBag.Fdate = fdate;
            ViewBag.Tdate = tdate;
            return View("ReportStudent");
        }

        // GET: Revenue
        [NeedLogin]
        public ActionResult Revenue()
        {
            var model = ReportManagement.GetCurrentDate();
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

            reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"\Reports\Revenue\RevenueRDLC.rdlc";
            sp_reportreceiveTableAdapter Revenue = new sp_reportreceiveTableAdapter();

            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("RevenueDataset", (object)Revenue.GetData(model.fdate, model.tdate)));

            reportViewer.LocalReport.DisplayName = DateTime.Now.ToShortDateString() + "_รายงานสรุปรายได้";

            var UserData = Session["UserProfile"] as UserSessionModel;
            ReportParameter User = new ReportParameter("User", UserData.Username + ": " + UserData.UserFirstNameEN + " " + UserData.UserLastNameEN);
            ReportParameter Datecurrent = new ReportParameter("Datecurrent", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss", new CultureInfo("th-TH")));
            reportViewer.LocalReport.EnableExternalImages = true;

            var company = Session["Company"] as CompanyModel;
            ReportParameter SchoolLogo = new ReportParameter("SchoolLogo", new Uri(Server.MapPath("~/" + company.SchoolLogo)).AbsoluteUri);
            ReportParameter SchoolName = new ReportParameter("SchoolName", company.SchoolName);
            ReportParameter SchoolAddr1 = new ReportParameter("SchoolAddr1", company.schoolAddr1);
            ReportParameter SchoolAddr2 = new ReportParameter("SchoolAddr2", company.schoolAddr2);
            ReportParameter SchoolAddr3 = new ReportParameter("SchoolAddr3", company.schoolAddr3);

            ReportParameter Fdate = new ReportParameter("Fdate", model.fdate);
            ReportParameter Tdate = new ReportParameter("Tdate", model.tdate);

            reportViewer.LocalReport.SetParameters(new ReportParameter[] { User, Datecurrent, SchoolLogo, SchoolName, SchoolAddr1, SchoolAddr2, SchoolAddr3, Fdate, Tdate });
            ViewBag.ReportViewer = reportViewer;
            return View();
        }

        [NeedLogin]
        public ActionResult SumRevenue(FormCollection form)
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

            reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"\Reports\Revenue\RevenueRDLC.rdlc";
            sp_reportreceiveTableAdapter Revenue = new sp_reportreceiveTableAdapter();

            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("RevenueDataset", (object)Revenue.GetData(fdate, tdate)));

            reportViewer.LocalReport.DisplayName = DateTime.Now.ToShortDateString() + "_รายงานสรุปรายได้";

            var UserData = Session["UserProfile"] as UserSessionModel;
            ReportParameter User = new ReportParameter("User", UserData.Username + ": " + UserData.UserFirstNameEN + " " + UserData.UserLastNameEN);
            ReportParameter Datecurrent = new ReportParameter("Datecurrent", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss", new CultureInfo("th-TH")));
            reportViewer.LocalReport.EnableExternalImages = true;

            var company = Session["Company"] as CompanyModel;
            ReportParameter SchoolLogo = new ReportParameter("SchoolLogo", new Uri(Server.MapPath("~/" + company.SchoolLogo)).AbsoluteUri);
            ReportParameter SchoolName = new ReportParameter("SchoolName", company.SchoolName);
            ReportParameter SchoolAddr1 = new ReportParameter("SchoolAddr1", company.schoolAddr1);
            ReportParameter SchoolAddr2 = new ReportParameter("SchoolAddr2", company.schoolAddr2);
            ReportParameter SchoolAddr3 = new ReportParameter("SchoolAddr3", company.schoolAddr3);


            ReportParameter Fdate = new ReportParameter("Fdate", fdate);
            ReportParameter Tdate = new ReportParameter("Tdate", tdate);

            reportViewer.LocalReport.SetParameters(new ReportParameter[] { User, Datecurrent, SchoolLogo, SchoolName, SchoolAddr1, SchoolAddr2, SchoolAddr3, Fdate, Tdate });
            ViewBag.ReportViewer = reportViewer;

            ViewBag.Fdate = fdate;
            ViewBag.Tdate = tdate;
            return View("Revenue");
        }

        // GET: TestTakers
        #region TestTakers
        [NeedLogin]
        public ActionResult TestTakers(FormCollection val)
        {
            var date = ReportManagement.GetCurrentDate();
            ViewBag.Fdate = (val["fdate"] != null ? val["fdate"] : date.fdate);
            ViewBag.Tdate = (val["tdate"] != null ? val["tdate"] : date.tdate);

            var ugmData = new TestTakersModel();
            ugmData.fdate = ViewBag.Fdate;
            ugmData.tdate = ViewBag.Tdate;
            List<TestTakersModel> model = new List<TestTakersModel>();
            model = ReportManagement.GetDocNumberBooking(ugmData);

            return View(model);
        }
        #endregion
        #region TestTakersName
        [NeedLogin]
        [HttpPost]
        public ActionResult TestTakersName(TestTakersModel ugm)
        {
            var date = ReportManagement.GetCurrentDate();
            ViewBag.Fdate = (ugm.fdate != null ? ugm.fdate : date.fdate);
            ViewBag.Tdate = (ugm.tdate != null ? ugm.tdate : date.tdate);

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

            reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"\Reports\TestTakersName\TestTakersNameRDLC.rdlc";
            sp_reportexamnamelistTableAdapter TestTakerName = new sp_reportexamnamelistTableAdapter();

            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("TestTakersNameDataset", (object)TestTakerName.GetData(ugm.examdate, short.Parse(ugm.courseid))));

            reportViewer.LocalReport.DisplayName = DateTime.Now.ToShortDateString() + "_ทะเบียนรายชื่อผู้เข้าสอบ";

            var UserData = Session["UserProfile"] as UserSessionModel;
            ReportParameter User = new ReportParameter("User", UserData.Username + ": " + UserData.UserFirstNameEN + " " + UserData.UserLastNameEN);
            ReportParameter Datecurrent = new ReportParameter("Datecurrent", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss", new CultureInfo("th-TH")));
            reportViewer.LocalReport.EnableExternalImages = true;

            var company = Session["Company"] as CompanyModel;
            ReportParameter SchoolLogo = new ReportParameter("SchoolLogo", new Uri(Server.MapPath("~/" + company.SchoolLogo)).AbsoluteUri);
            ReportParameter SchoolName = new ReportParameter("SchoolName", company.SchoolName);
            ReportParameter SchoolAddr1 = new ReportParameter("SchoolAddr1", company.schoolAddr1);
            ReportParameter SchoolAddr2 = new ReportParameter("SchoolAddr2", company.schoolAddr2);
            ReportParameter SchoolAddr3 = new ReportParameter("SchoolAddr3", company.schoolAddr3);

            string[] str = ugm.examdate.Split('-');
            string month = "";
            if (str[1] == "01") { month = "มกราคม"; }
            else if (str[1] == "02") { month = "กุมภาพันธ์"; }
            else if (str[1] == "03") { month = "มีนาคม"; }
            else if (str[1] == "04") { month = "เมษายน"; }
            else if (str[1] == "05") { month = "พฤษภาคม"; }
            else if (str[1] == "06") { month = "มิถุนายน"; }
            else if (str[1] == "07") { month = "กรกฎาคม"; }
            else if (str[1] == "08") { month = "สิงหาคม"; }
            else if (str[1] == "09") { month = "กันยายน"; }
            else if (str[1] == "10") { month = "ตุลาคม"; }
            else if (str[1] == "11") { month = "พฤษจิกายน"; }
            else if (str[1] == "12") { month = "ธันวาคม"; }
            int day = Int32.Parse(str[2]);
            int year = Int32.Parse(str[0]);

            string dateTH = day + " " + month + " " + (year + 543);

            ReportParameter coursegroupname = new ReportParameter("coursegroupname", ugm.coursegroupname);
            ReportParameter examdate = new ReportParameter("examdate", dateTH);

            reportViewer.LocalReport.SetParameters(new ReportParameter[] { User, Datecurrent, SchoolLogo, SchoolName, SchoolAddr1, SchoolAddr2, SchoolAddr3, coursegroupname, examdate });
            ViewBag.ReportViewer = reportViewer;
            return View();
        }
        #endregion
        #region TestTakersTheory
        [NeedLogin]
        [HttpPost]
        public ActionResult TestTakersTheory(TestTakersModel ugm)
        {
            var date = ReportManagement.GetCurrentDate();
            ViewBag.Fdate = (ugm.fdate != null ? ugm.fdate : date.fdate);
            ViewBag.Tdate = (ugm.tdate != null ? ugm.tdate : date.tdate);

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

            reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"\Reports\TestTakersName\TestTakersTheoryRDLC.rdlc";
            sp_reportexamnamelistTableAdapter TestTakerName = new sp_reportexamnamelistTableAdapter();
            sp_reportexamStaffListTableAdapter TestTakerStaffList = new sp_reportexamStaffListTableAdapter();

            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("TestTakersNameDataset", (object)TestTakerName.GetData(ugm.examdate, short.Parse(ugm.courseid))));
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("TestTakersStaffListDataset", (object)TestTakerStaffList.GetData(short.Parse(ugm.courseid), ugm.cflg)));

            reportViewer.LocalReport.DisplayName = DateTime.Now.ToShortDateString() + "_แบบบันทึกการสอบภาคทฤษฏี (" + ugm.coursegroupname + ")";

            var UserData = Session["UserProfile"] as UserSessionModel;
            ReportParameter User = new ReportParameter("User", UserData.Username + ": " + UserData.UserFirstNameEN + " " + UserData.UserLastNameEN);
            ReportParameter Datecurrent = new ReportParameter("Datecurrent", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss", new CultureInfo("th-TH")));
            reportViewer.LocalReport.EnableExternalImages = true;

            var company = Session["Company"] as CompanyModel;
            ReportParameter SchoolLogo = new ReportParameter("SchoolLogo", new Uri(Server.MapPath("~/" + company.SchoolLogo)).AbsoluteUri);
            ReportParameter SchoolName = new ReportParameter("SchoolName", company.SchoolName);
            ReportParameter SchoolAddr1 = new ReportParameter("SchoolAddr1", company.schoolAddr1);
            ReportParameter SchoolAddr2 = new ReportParameter("SchoolAddr2", company.schoolAddr2);
            ReportParameter SchoolAddr3 = new ReportParameter("SchoolAddr3", company.schoolAddr3);

            string[] str = ugm.examdate.Split('-');
            string month = "";
            if (str[1] == "01") { month = "มกราคม"; }
            else if (str[1] == "02") { month = "กุมภาพันธ์"; }
            else if (str[1] == "03") { month = "มีนาคม"; }
            else if (str[1] == "04") { month = "เมษายน"; }
            else if (str[1] == "05") { month = "พฤษภาคม"; }
            else if (str[1] == "06") { month = "มิถุนายน"; }
            else if (str[1] == "07") { month = "กรกฎาคม"; }
            else if (str[1] == "08") { month = "สิงหาคม"; }
            else if (str[1] == "09") { month = "กันยายน"; }
            else if (str[1] == "10") { month = "ตุลาคม"; }
            else if (str[1] == "11") { month = "พฤษจิกายน"; }
            else if (str[1] == "12") { month = "ธันวาคม"; }
            int day = Int32.Parse(str[2]);
            int year = Int32.Parse(str[0]);

            string dateTH = day + " " + month + " " + (year + 543);

            ReportParameter coursegroupname = new ReportParameter("coursegroupname", ugm.coursegroupname);
            ReportParameter examdate = new ReportParameter("examdate", dateTH);

            reportViewer.LocalReport.SetParameters(new ReportParameter[] { User, Datecurrent, SchoolLogo, SchoolName, SchoolAddr1, SchoolAddr2, SchoolAddr3, coursegroupname, examdate });
            ViewBag.ReportViewer = reportViewer;
            return View();
        }
        #endregion
        #region TestTakersCar
        [NeedLogin]
        public ActionResult TestTakersCar(TestTakersModel ugm)
        {
            var date = ReportManagement.GetCurrentDate();
            ViewBag.Fdate = (ugm.fdate != null ? ugm.fdate : date.fdate);
            ViewBag.Tdate = (ugm.tdate != null ? ugm.tdate : date.tdate);

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

            reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"\Reports\TestTakersName\TestTakersCarRDLC.rdlc";
            sp_reportexamnamelistTableAdapter TestTakerName = new sp_reportexamnamelistTableAdapter();
            sp_reportexamStaffListTableAdapter TestTakerStaffList = new sp_reportexamStaffListTableAdapter();

            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("TestTakersNameDataset", (object)TestTakerName.GetData(ugm.examdate, short.Parse(ugm.courseid))));
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("TestTakersStaffListDataset", (object)TestTakerStaffList.GetData(short.Parse(ugm.courseid), ugm.cflg)));

            reportViewer.LocalReport.DisplayName = DateTime.Now.ToShortDateString() + "_แบบบันทึกการสอบภาคทฤษฏี (" + ugm.coursegroupname + ")";

            var UserData = Session["UserProfile"] as UserSessionModel;
            ReportParameter User = new ReportParameter("User", UserData.Username + ": " + UserData.UserFirstNameEN + " " + UserData.UserLastNameEN);
            ReportParameter Datecurrent = new ReportParameter("Datecurrent", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss", new CultureInfo("th-TH")));
            reportViewer.LocalReport.EnableExternalImages = true;

            var company = Session["Company"] as CompanyModel;
            ReportParameter SchoolLogo = new ReportParameter("SchoolLogo", new Uri(Server.MapPath("~/" + company.SchoolLogo)).AbsoluteUri);
            ReportParameter SchoolName = new ReportParameter("SchoolName", company.SchoolName);
            ReportParameter SchoolAddr1 = new ReportParameter("SchoolAddr1", company.schoolAddr1);
            ReportParameter SchoolAddr2 = new ReportParameter("SchoolAddr2", company.schoolAddr2);
            ReportParameter SchoolAddr3 = new ReportParameter("SchoolAddr3", company.schoolAddr3);

            string[] str = ugm.examdate.Split('-');
            string month = "";
            if (str[1] == "01") { month = "มกราคม"; }
            else if (str[1] == "02") { month = "กุมภาพันธ์"; }
            else if (str[1] == "03") { month = "มีนาคม"; }
            else if (str[1] == "04") { month = "เมษายน"; }
            else if (str[1] == "05") { month = "พฤษภาคม"; }
            else if (str[1] == "06") { month = "มิถุนายน"; }
            else if (str[1] == "07") { month = "กรกฎาคม"; }
            else if (str[1] == "08") { month = "สิงหาคม"; }
            else if (str[1] == "09") { month = "กันยายน"; }
            else if (str[1] == "10") { month = "ตุลาคม"; }
            else if (str[1] == "11") { month = "พฤษจิกายน"; }
            else if (str[1] == "12") { month = "ธันวาคม"; }
            int day = Int32.Parse(str[2]);
            int year = Int32.Parse(str[0]);

            string dateTH = day + " " + month + " " + (year + 543);

            ReportParameter coursegroupname = new ReportParameter("coursegroupname", ugm.coursegroupname);
            ReportParameter examdate = new ReportParameter("examdate", dateTH);

            reportViewer.LocalReport.SetParameters(new ReportParameter[] { User, Datecurrent, SchoolLogo, SchoolName, SchoolAddr1, SchoolAddr2, SchoolAddr3, coursegroupname, examdate });
            ViewBag.ReportViewer = reportViewer;
            return View();
        }
        #endregion
        #region TestTakersMotorcycle
        [NeedLogin]
        public ActionResult TestTakersMotorcycle(TestTakersModel ugm)
        {
            var date = ReportManagement.GetCurrentDate();
            ViewBag.Fdate = (ugm.fdate != null ? ugm.fdate : date.fdate);
            ViewBag.Tdate = (ugm.tdate != null ? ugm.tdate : date.tdate);

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

            reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"\Reports\TestTakersName\TestTakersMotorcycleRDLC.rdlc";
            sp_reportexamnamelistTableAdapter TestTakerName = new sp_reportexamnamelistTableAdapter();
            sp_reportexamStaffListTableAdapter TestTakerStaffList = new sp_reportexamStaffListTableAdapter();

            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("TestTakersNameDataset", (object)TestTakerName.GetData(ugm.examdate, short.Parse(ugm.courseid))));
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("TestTakersStaffListDataset", (object)TestTakerStaffList.GetData(short.Parse(ugm.courseid), ugm.cflg)));

            reportViewer.LocalReport.DisplayName = DateTime.Now.ToShortDateString() + "_แบบบันทึกการสอบภาคทฤษฏี (" + ugm.coursegroupname + ")";

            var UserData = Session["UserProfile"] as UserSessionModel;
            ReportParameter User = new ReportParameter("User", UserData.Username + ": " + UserData.UserFirstNameEN + " " + UserData.UserLastNameEN);
            ReportParameter Datecurrent = new ReportParameter("Datecurrent", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss", new CultureInfo("th-TH")));
            reportViewer.LocalReport.EnableExternalImages = true;

            var company = Session["Company"] as CompanyModel;
            ReportParameter SchoolLogo = new ReportParameter("SchoolLogo", new Uri(Server.MapPath("~/" + company.SchoolLogo)).AbsoluteUri);
            ReportParameter SchoolName = new ReportParameter("SchoolName", company.SchoolName);
            ReportParameter SchoolAddr1 = new ReportParameter("SchoolAddr1", company.schoolAddr1);
            ReportParameter SchoolAddr2 = new ReportParameter("SchoolAddr2", company.schoolAddr2);
            ReportParameter SchoolAddr3 = new ReportParameter("SchoolAddr3", company.schoolAddr3);

            string[] str = ugm.examdate.Split('-');
            string month = "";
            if (str[1] == "01") { month = "มกราคม"; }
            else if (str[1] == "02") { month = "กุมภาพันธ์"; }
            else if (str[1] == "03") { month = "มีนาคม"; }
            else if (str[1] == "04") { month = "เมษายน"; }
            else if (str[1] == "05") { month = "พฤษภาคม"; }
            else if (str[1] == "06") { month = "มิถุนายน"; }
            else if (str[1] == "07") { month = "กรกฎาคม"; }
            else if (str[1] == "08") { month = "สิงหาคม"; }
            else if (str[1] == "09") { month = "กันยายน"; }
            else if (str[1] == "10") { month = "ตุลาคม"; }
            else if (str[1] == "11") { month = "พฤษจิกายน"; }
            else if (str[1] == "12") { month = "ธันวาคม"; }
            int day = Int32.Parse(str[2]);
            int year = Int32.Parse(str[0]);

            string dateTH = day + " " + month + " " + (year + 543);

            ReportParameter coursegroupname = new ReportParameter("coursegroupname", ugm.coursegroupname);
            ReportParameter examdate = new ReportParameter("examdate", dateTH);

            reportViewer.LocalReport.SetParameters(new ReportParameter[] { User, Datecurrent, SchoolLogo, SchoolName, SchoolAddr1, SchoolAddr2, SchoolAddr3, coursegroupname, examdate });
            ViewBag.ReportViewer = reportViewer;
            return View();
        }
        #endregion
        #region TestTakersTransportation
        [NeedLogin]
        public ActionResult TestTakersTransportation(TestTakersModel ugm)
        {
            var date = ReportManagement.GetCurrentDate();
            ViewBag.Fdate = (ugm.fdate != null ? ugm.fdate : date.fdate);
            ViewBag.Tdate = (ugm.tdate != null ? ugm.tdate : date.tdate);

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

            reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"\Reports\TestTakersName\TestTakersTransportationRDLC.rdlc";
            sp_reportexamnamelistTableAdapter TestTakerName = new sp_reportexamnamelistTableAdapter();
            sp_reportexamStaffListTableAdapter TestTakerStaffList = new sp_reportexamStaffListTableAdapter();

            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("TestTakersNameDataset", (object)TestTakerName.GetData(ugm.examdate, short.Parse(ugm.courseid))));
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("TestTakersStaffListDataset", (object)TestTakerStaffList.GetData(short.Parse(ugm.courseid), ugm.cflg)));

            reportViewer.LocalReport.DisplayName = DateTime.Now.ToShortDateString() + "_แบบบันทึกการสอบภาคทฤษฏี (" + ugm.coursegroupname + ")";

            var UserData = Session["UserProfile"] as UserSessionModel;
            ReportParameter User = new ReportParameter("User", UserData.Username + ": " + UserData.UserFirstNameEN + " " + UserData.UserLastNameEN);
            ReportParameter Datecurrent = new ReportParameter("Datecurrent", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss", new CultureInfo("th-TH")));
            reportViewer.LocalReport.EnableExternalImages = true;

            var company = Session["Company"] as CompanyModel;
            ReportParameter SchoolLogo = new ReportParameter("SchoolLogo", new Uri(Server.MapPath("~/" + company.SchoolLogo)).AbsoluteUri);
            ReportParameter SchoolName = new ReportParameter("SchoolName", company.SchoolName);
            ReportParameter SchoolAddr1 = new ReportParameter("SchoolAddr1", company.schoolAddr1);
            ReportParameter SchoolAddr2 = new ReportParameter("SchoolAddr2", company.schoolAddr2);
            ReportParameter SchoolAddr3 = new ReportParameter("SchoolAddr3", company.schoolAddr3);

            string[] str = ugm.examdate.Split('-');
            string month = "";
            if (str[1] == "01") { month = "มกราคม"; }
            else if (str[1] == "02") { month = "กุมภาพันธ์"; }
            else if (str[1] == "03") { month = "มีนาคม"; }
            else if (str[1] == "04") { month = "เมษายน"; }
            else if (str[1] == "05") { month = "พฤษภาคม"; }
            else if (str[1] == "06") { month = "มิถุนายน"; }
            else if (str[1] == "07") { month = "กรกฎาคม"; }
            else if (str[1] == "08") { month = "สิงหาคม"; }
            else if (str[1] == "09") { month = "กันยายน"; }
            else if (str[1] == "10") { month = "ตุลาคม"; }
            else if (str[1] == "11") { month = "พฤษจิกายน"; }
            else if (str[1] == "12") { month = "ธันวาคม"; }
            int day = Int32.Parse(str[2]);
            int year = Int32.Parse(str[0]);

            string dateTH = day + " " + month + " " + (year + 543);

            ReportParameter coursegroupname = new ReportParameter("coursegroupname", ugm.coursegroupname);
            ReportParameter examdate = new ReportParameter("examdate", dateTH);

            reportViewer.LocalReport.SetParameters(new ReportParameter[] { User, Datecurrent, SchoolLogo, SchoolName, SchoolAddr1, SchoolAddr2, SchoolAddr3, coursegroupname, examdate });
            ViewBag.ReportViewer = reportViewer;
            return View();
        }
        #endregion
    }
}
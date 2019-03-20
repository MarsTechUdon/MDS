﻿using MDS.DBExec;
using MDS.Fillter;
using MDS.Models;
using MDS.Reports.Revenue.RevenueXSDTableAdapters;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace MDS.Controllers
{
    public class RevenueController : Controller
    {
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
            reportViewer.LocalReport.SetParameters(new ReportParameter[] { User, Datecurrent, SchoolLogo, SchoolName, SchoolAddr1, SchoolAddr2, SchoolAddr3 });
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
            reportViewer.LocalReport.SetParameters(new ReportParameter[] { User, Datecurrent, SchoolLogo, SchoolName, SchoolAddr1, SchoolAddr2, SchoolAddr3 });
            ViewBag.ReportViewer = reportViewer;

            ViewBag.Fdate = fdate;
            ViewBag.Tdate = tdate;
            return View("Revenue");
        }
    }
}
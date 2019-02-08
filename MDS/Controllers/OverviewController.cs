using MDS.DBExec;
using MDS.Fillter;
using MDS.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MDS.Controllers
{
    public class OverviewController : Controller
    {
        OverviewManagement OverviewMN = new OverviewManagement();
        // GET: Overview
        [NeedLogin]
        public ActionResult Main()
        {
            ViewBag.Result = TempData["Result"];
            ViewBag.Message = TempData["Message"];
            var userDt = DateTime.Now.ToString("yyyy-MM-dd", new System.Globalization.CultureInfo("en-GB"));
            var currentdate = userDt;
            ViewBag.Getoverview = OverviewMN.Getoverview(currentdate);
            return View();
        }
        [NeedLogin]
        public ActionResult addCurrentBranch(string branch)
        {
            var BranchModel = new CurrentBranchModel();
            string[] str = branch.Split(' ');
            var branchtxt = CurrentBranchManagement.encode(str[0], str[1]);
            var Cookies = Request.Cookies["branchtxt"];
            if (Cookies == null)
            {
                try
                {

                    Response.Cookies["branchtxt"].Value = branchtxt;
                    //Response.Cookies["branchtxt"].Expires = DateTime.Now.AddMinutes(2); 
                    Response.Cookies["branchtxt"].Expires = DateTime.Now.AddDays(365);
                    TempData["Result"] = "Y";
                    TempData["Message"] = "บันทึกสำเร็จ";
                    BranchModel.branchid = str[0];
                    BranchModel.branchname = str[1];
                    Session["GetBranch"] = BranchModel;
                }
                catch (Exception ex)
                {
                    Response.Cookies["branchtxt"].Value = "";
                    TempData["Result"] = "N";
                    TempData["Message"] = "บันทึกไม่สำเร็จ";
                }
            }
            else if (Cookies.Value=="") {
                try
                {
                    Response.Cookies["branchtxt"].Value = branchtxt;
                    TempData["Result"] = "Y";
                    TempData["Message"] = "บันทึกสำเร็จ";
                    BranchModel.branchid = str[0];
                    BranchModel.branchname = str[1];
                    Session["GetBranch"] = BranchModel;
                }
                catch (Exception ex)
                {
                    Response.Cookies["branchtxt"].Value = "";
                    TempData["Result"] = "N";
                    TempData["Message"] = "บันทึกไม่สำเร็จ";
                }
            }

            return RedirectToAction("Main", "Overview");
        }

        #region"get Viewdata1"
        [NeedLogin]
        public ActionResult Viewdata1(string ovid,string title)
        {
            var list = new List<OverviewDetailModel>();
            var userDt = DateTime.Now.ToString("yyyy-MM-dd", new System.Globalization.CultureInfo("en-GB"));
            var currentdate = userDt;
            list = OverviewMN.GetoverviewDetail1(currentdate, ovid);
            ViewBag.list = list;
            ViewBag.titledata = title;
            return View();
        }
        #endregion
        [NeedLogin]
        #region"get Viewdata2"
        [NeedLogin]
        public ActionResult Viewdata2(string ovid, string title)
        {
            var list = new List<OverviewDetailModel>();
            var userDt = DateTime.Now.ToString("yyyy-MM-dd", new System.Globalization.CultureInfo("en-GB"));
            var currentdate = userDt;
            list = OverviewMN.GetoverviewDetail2(currentdate, ovid);
            ViewBag.list = list;
            ViewBag.titledata = title;
            return View();
        }
        #endregion
        [NeedLogin]
        #region"get Viewdata3"
        [NeedLogin]
        public ActionResult Viewdata3(string ovid, string title)
        {
            var list = new List<OverviewDetailModel>();
            var userDt = DateTime.Now.ToString("yyyy-MM-dd", new System.Globalization.CultureInfo("en-GB"));
            var currentdate = userDt;
            list = OverviewMN.GetoverviewDetail3(currentdate, ovid);
            ViewBag.list = list;
            ViewBag.titledata = title;
            return View();
        }
        #endregion
        [NeedLogin]
        public ActionResult ChangeBranch()
        {
            ViewBag.Result = TempData["Result"];
            ViewBag.Message = TempData["Message"];
            var Branchlist = new List<CurrentBranchModel>();
            Branchlist = CurrentBranchManagement.GetCurrentBranch();
            ViewBag.Branchlist = Branchlist;
            return View();
        }
        [NeedLogin]
        public ActionResult UpdateBranch(string branch)
        {
            var BranchModel = new CurrentBranchModel();
            string[] str = branch.Split(' ');
            var branchtxt = CurrentBranchManagement.encode(str[0], str[1]);
            try
            {
                Response.Cookies["branchtxt"].Value = branchtxt;
                Response.Cookies["branchtxt"].Expires = DateTime.Now.AddDays(365);
                TempData["Result"] = "Y";
                TempData["Message"] = "บันทึกสำเร็จ";
                BranchModel.branchid = str[0];
                BranchModel.branchname = str[1];
                Session["GetBranch"] = BranchModel;
            }
            catch (Exception ex)
            {
                TempData["Result"] = "N";
                TempData["Message"] = "บันทึกไม่สำเร็จ";
            }

           

            return RedirectToAction("ChangeBranch", "Overview");
        }
      
        public ActionResult Error()
        {
            return View("Error");
        }
    }
}
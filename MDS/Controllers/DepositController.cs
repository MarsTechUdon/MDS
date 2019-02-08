using MDS.DBExec;
using MDS.Fillter;
using MDS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MDS.Views.Deposit
{
    public class DepositController : Controller
    {
        DepositManagement dp_mn = new DepositManagement();

        // GET: Deposit
        [NeedLogin]
        public ActionResult ListBatch()
        {
            ListBatchModel model = new ListBatchModel();
            var ListBatch = new DepositManagement().getListBatch();
            return View("ListBatch", ListBatch);
        }

        // GET: Deposit
        [NeedLogin]
        public ActionResult DepositDetail(string bid = null)
        {

            ListBatchModel model = new ListBatchModel();
            var chkMClose = new DepositManagement().CheckMClosePerUser();
            ViewBag.chkclose = chkMClose;
            if (chkMClose == "True" || chkMClose == "TRUE" || chkMClose == "1")
            {
                var userdata = Session["userprofile"] as UserSessionModel;
                model = new DepositManagement().getListDeposit(userdata.Username);
                
            }
            else
            {
                var branchdata = Session["getbranch"] as CurrentBranchModel;
                model = new DepositManagement().getListDeposit2(bid);
                
            }
            return View(model);
        }

        // GET: Deposit
        [NeedLogin]
        public ActionResult DepositEdit(string batchno, string batchdate, string accountno, string depositdate, string remark, string amount)
        {
            ListBatchModel model = new ListBatchModel();
            ViewBag.batchno = batchno;
            ViewBag.batchdate = batchdate;
            ViewBag.accountno = accountno;
            ViewBag.depositdate = depositdate;
            ViewBag.remark = remark;
            ViewBag.amount = amount;
            var ListDeposit = new DepositManagement().getListDepositByBatchno(batchno);
            return View("DepositEdit", ListDeposit);
        }

        // POST: Deposit/add/5
        [NeedLogin]
        [HttpPost]
        public ActionResult addDeposit(ListBatchModel modelData)
        {
            var model = new ListBatchModel();
            var userData = Session["UserProfile"] as UserSessionModel;
            var BranchData = Session["GetBranch"] as CurrentBranchModel;
            var result = new DepositManagement().addDeposit(modelData, userData.Username, BranchData.branchid);
            ViewBag.Result = result.Result;
            return RedirectToAction("ListBatch");
        }

        // POST: Deposit/Edit/5
        [NeedLogin]
        [HttpPost]
        public ActionResult editDeposit(FormCollection Value)
        {
            ListBatchModel modelData = new ListBatchModel();
            var userData = Session["UserProfile"] as UserSessionModel;
            modelData.Batchno = Value["batchno"];
            modelData.depositdate = Value["depositdate"];
            modelData.accountno = Value["accountno"];
            modelData.remark = Value["remark"];
            var res = dp_mn.editDeposit(modelData, userData.Username);
            return RedirectToAction("ListBatch");
        }
    }
}
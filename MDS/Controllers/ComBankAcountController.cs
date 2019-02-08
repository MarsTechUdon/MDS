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
    public class ComBankAccountController : Controller
    {
        ComBankAccountManagement combankaccountManage = new ComBankAccountManagement();
        // GET: ComBank
        [NeedLogin]
        public ActionResult ComBankAccountList()
        {
            var ListComBankAccount = new ComBankAccountManagement().GetAllComBankAccount();
            return View("ComBankAccountList", ListComBankAccount);
        }


        // POST: ComBank/Edit/5
        [NeedLogin]
        [HttpPost]
        public ActionResult AddComBankAccount(FormCollection Value)
        {
            ComBankAccountModel model = new ComBankAccountModel();
            model.AccountNo = Value["account_no"];
            model.AccountName = Value["account_name"];
            model.AccountType = Value["account_type"];
            model.BankName = Value["bank_name"];
            model.BankBranch = Value["branch_name"];
            var res = combankaccountManage.addComBankAccount(model);
            return RedirectToAction("ComBankAccountList");
        }

        // POST: ComBankAccount/Edit/5
        [NeedLogin]
        [HttpPost]
        public ActionResult EditComBankAccount(FormCollection Value)
        {
            ComBankAccountModel model = new ComBankAccountModel();
            model.AccountID = Value["accountid"];
            model.AccountNo = Value["accountno"];
            model.AccountName = Value["accountname"];
            model.AccountType = Value["accounttype"];
            model.BankName = Value["bankname"];
            model.BankBranch = Value["bankbranch"];
            var res = combankaccountManage.editComBankAccount(model);
            return RedirectToAction("ComBankAccountList");
        }

        // POST: ComBank/Delete/5
        [NeedLogin]
        [HttpPost]
        public ActionResult DeleteComBank(FormCollection Value)
        {
            string id = Value["frmId"].ToString();
            var model = new ResultModel();
            model = combankaccountManage.DeleteComBankAccount(id);
            TempData["Result"] = model.Result;
            TempData["Message"] = model.Message;

            return RedirectToAction("ComBankAccountList");
        }

        [NeedLogin]
        [HttpPost]
        public ActionResult ReDeleteComBank(FormCollection Value)
        {
            string id = Value["frmreId"].ToString();
            var model = new ResultModel();
            model = combankaccountManage.ReDeleteComBankAccount(id);
            TempData["Result"] = model.Result;
            TempData["Message"] = model.Message;

            return RedirectToAction("ComBankAccountList");
        }
    }
}

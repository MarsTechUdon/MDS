using MDS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MDS.DBExec;
using MDS.Fillter;
using MDS.Models;

namespace MDS.Controllers
{
    public class ComBankController : Controller
    {
         ComBankManagement combankManage = new ComBankManagement();
        // GET: ComBank
        [NeedLogin]
        public ActionResult ComBankList()
        {
            ComBankModel model = new ComBankModel();
            var ListComBank = new ComBankManagement().GetAllComBank();

            return View("ComBankList", ListComBank);
        }

        // POST: ComBank/Edit/5
        [NeedLogin]
        [HttpPost]
        public ActionResult AddComBank(FormCollection Value)
        {
            ComBankModel model = new ComBankModel();
            model.bankabbr = Value["bank_abbr"];
            model.bankname = Value["bank_name"];
            var res = combankManage.addComBank(model);
            return RedirectToAction("ComBankList");
        }

        // POST: ComBank/Edit/5
        [NeedLogin]
        [HttpPost]
        public ActionResult EditComBank(FormCollection Value)
        {
            ComBankModel model = new ComBankModel();
            model.bid = Value["bank_id"];
            model.bankabbr = Value["edit_bank_abbr"];
            model.bankname = Value["edit_bank_name"];
            var res = combankManage.editComBank(model);
            return RedirectToAction("ComBankList");
        }

        // POST: ComBank/Delete/5
        [NeedLogin]
        [HttpPost]
        public ActionResult DeleteComBank(FormCollection Value)
        {
            string id = Value["frmId"].ToString();
            var model = new ResultModel();
            model = combankManage.DeleteComBank(id);
            TempData["Result"] = model.Result;
            TempData["Message"] = model.Message;

            return RedirectToAction("ComBankList");
        }

        [NeedLogin]
        [HttpPost]
        public ActionResult ReDeleteComBank(FormCollection Value)
        {
            string id = Value["frmreId"].ToString();
            var model = new ResultModel();
            model = combankManage.ReDeleteComBank(id);
            TempData["Result"] = model.Result;
            TempData["Message"] = model.Message;

            return RedirectToAction("ComBankList");
        }
    }
}

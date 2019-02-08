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
    public class PaymentController : Controller
    {
        PaymentManagement PaymentManagement = new PaymentManagement();
        // GET: Payment
        public ActionResult Index()
        {
            return View();
        }

        [NeedLogin]
        [HttpPost]
        public JsonResult SelectPaymentById(string paymentId)
        {
            //var lawManage = new LawManagement();
            //LawManageModel lawDataJson = new LawManageModel();
            //lawDataJson = lawManage.GetLawByID(Convert.ToInt32(LawId));
            var model = PaymentManagement.GetPaymentById(paymentId);
            return Json(model);
        }

        [NeedLogin]
        [HttpPost]
        public JsonResult VerifyCoupon(string couponNo)
        {
            var model = PaymentManagement.Verify(couponNo);
            model.amount = @Convert.ToInt32(Convert.ToDecimal(model.amount)).ToString();
            return Json(model);
        }

        [NeedLogin]
        [HttpPost]
        public ActionResult CreatePayment(PaymentModel modelData)
        {
            PaymentModel model = new PaymentModel();

            var UserData = Session["UserProfile"] as UserSessionModel;
            modelData.user = UserData.Username;
            var BranchData = Session["GetBranch"] as CurrentBranchModel;
            modelData.branchid = BranchData.branchid;
            if (modelData.Rcby == "dbnote")
            {
                modelData.amount = (Int32.Parse(modelData.amountDbnote) * (-1)).ToString();
            }
            if (modelData.Rcby == "discount")
            {
                modelData.amount = modelData.amountDiscount; 

            }
            model = PaymentManagement.InsertPayment(modelData);
            if (model.Result == "Y" && modelData.Rcby == "discount")
            {
                CouponModel modelcoupon = new CouponModel();
                modelcoupon = PaymentManagement.UseCoupon(modelData.reference, modelData.studentind, model.payid,modelData.voucherid);
                TempData["Result"] = modelcoupon.Result;
                TempData["Message"] = modelcoupon.Message;
            }
            else
            {
                TempData["Result"] = model.Result;
                TempData["Message"] = model.Message;
            }
            model.studentind = modelData.studentind;

            //ส่ง studentind กลับไปไม่งั้น Refresh จะerror
            StudentModel modelStudent = new StudentModel();
            modelStudent.studentind = modelData.studentind;
            modelStudent.voucherid = modelData.voucherid;
            modelStudent.tabActive = "3";
            TempData["Paymentstudentind"] = modelData.studentind;
            TempData["tabActive"] = "3";
            return RedirectToAction("StudentDetail","Student", modelStudent);
        }

        [NeedLogin]
        [HttpPost]
        public ActionResult EditPayment(PaymentModel modelData)
        {
            PaymentModel model = new PaymentModel();
            var UserData = Session["UserProfile"] as UserSessionModel;
            model.user = UserData.Username;
            var BranchData = Session["GetBranch"] as CurrentBranchModel;
            model.branchid = BranchData.branchid;
            model.payid = modelData.payid;
            model.paydate = modelData.paydate;
            model.rcid = Request.Form["hideRcid"].ToString();
            model.Rcby = Request.Form["hideRcby"].ToString();
            model.remark = modelData.remark;
            model.reference = modelData.reference;
            model.amount = modelData.amount;
            model.bookingid = modelData.bookingid;
            model.studentind = modelData.studentind;
            model.voucherid = modelData.voucherid;

            // <option value="cash">เงินสด</option>
            //<option value="tran">โอน</option>
            //<option value="crcard">บัตรเครดิต</option>
            //<option value="cheque">เช็ค</option>
            //<option value="dbnote">คืนเงิน</option>
            //เช็ค Rcby 
            if (model.Rcby == "tran") 
            {
                model.accountno = modelData.accountno;
            }
            else if (model.Rcby == "crcard") {
                model.cid = modelData.cid;
                model.CrCardNo = modelData.CrCardNo;

            }
            else if (model.Rcby == "cheque")
            {
                model.chequeno = modelData.chequeno;
                model.bankabbr = modelData.bankabbr;
                model.bankbranch = modelData.bankbranch;
                model.chequedate = modelData.chequedate;
            }
            else if (model.Rcby == "dbnote")
            {
                model.amount = (Int32.Parse(modelData.amountDbnote)*(-1)).ToString();
            }
            else if (model.Rcby == "discount")
            {
                model.amount = modelData.amountDiscount;

            }
            var resmodel = PaymentManagement.EditPayment(model);
            resmodel.studentind = modelData.studentind;
            TempData["Result"] = resmodel.Result;
            TempData["Message"] = resmodel.Message;

            //ส่ง studentind กลับไปไม่งั้น Refresh จะerror
            StudentModel modelStudent = new StudentModel();
            modelStudent.studentind = modelData.studentind;
            modelStudent.voucherid = modelData.voucherid;
            modelStudent.tabActive = "3";
            TempData["Paymentstudentind"] = modelData.studentind;
            TempData["tabActive"] = "3";
            return RedirectToAction("StudentDetail", "Student", modelStudent);
        }
        

    }
}
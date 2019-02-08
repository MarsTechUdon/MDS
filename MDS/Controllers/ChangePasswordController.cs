using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MDS.DBEntity;
using MDS.DBExec;
using MDS.Fillter;
using MDS.Models;

namespace MDS.Controllers
{
    public class ChangePasswordController : Controller
    {
    
                
        [NeedLogin]
        public ActionResult ChangePassword()
        {

            ViewBag.Result = TempData["Result"];
            ViewBag.Message = TempData["Message"];
            return View();
        }

        [NeedLogin]
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordModel value)
        {
            var userManage = new UserManagement();
            if (ModelState.IsValid == false)
            {
                ViewBag.Result = TempData["Result"];
                ViewBag.Message = TempData["Message"];
                return View("ChangePassword");
            }
            else
            {
                var _result = userManage.ChangePassword(value);
                if (_result.Result.Equals("Y"))
                {
                    //Session.Abandon();
                    TempData["Result"] = "Y";
                    TempData["Message"] = "บันทึกรหัสผ่านใหม่สำเร็จ";
                    return RedirectToAction("Main", "Overview");
                }
                else
                {
                    TempData["Result"] = _result.Result;
                    TempData["Message"] = _result.Message;
                    return RedirectToAction("ChangePassword");
                }
            }
        }

      
    }
}
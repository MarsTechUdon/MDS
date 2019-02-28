using MDS.DBEntity;
using MDS.DBExec;
using MDS.Fillter;
using MDS.Models;
using Newtonsoft.Json.Linq;
using reCAPTCHA.MVC;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MDS.Controllers
{
    public class LoginController : Controller
    {
        SchoolManagement school = new SchoolManagement();
        [IsLoggedIn]
        public ActionResult Index(string username = null)
        {
            //ViewBag.AlertMessage = TempData["AlertMessage"];
            ViewBag.GetCompany = school.GetCompanyView();
            ViewData["ErrorMessage"] = TempData["FailLogin"];
            ViewData["username"] = username;
            ViewData["SuccessMessage"] = TempData["IsChange"];
            return View("Login");
        }

        [NeedLogin]
        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Index");
        }

        //[CaptchaValidator(
        ////PrivateKey = "6LedW2AUAAAAAGIa-wzJMKh9W3lC5PqeK_R-8LTY",  /*localhost key*/
        ////PrivateKey = "6LeM83MUAAAAAL0n1Du9iCERmIIvrt2zi5z77qSw",  /*localhost key 192.168.1.17 */
        //PrivateKey = "6LcDwWAUAAAAABd3xzbUTUeZ2Qug2kFThwlpesuU",    /*163 key*/
        ////PrivateKey = "6LehXHcUAAAAANAU69Qff3LgxAlBlBoeLpdwtzGz",   /*marsdrivingcenter*/
        //ErrorMessage = "Invalid input captcha.",
        //RequiredMessage = "The captcha field is required.")]
        [IsLoggedIn]
        [HttpPost]
        public ActionResult Login(UserLoginModel loginData)
        {
            if (ModelState.IsValid) //this will take care of captcha
            {
                using (var db = new MDSEntities())
                {
                    var loginResult = db.sp_UserLogin(loginData.Username, loginData.Password, Request.UserHostAddress).FirstOrDefault();
                    if (loginResult != null && loginResult.HaveUser == "N")
                    {
                        TempData["FailLogin"] = "ชื่อผู้ใช้หรือรหัสผ่านไม่ถูกต้อง";
                        return RedirectToAction("Index", new { username = loginData.Username });
                    }
                    if (loginResult != null && loginResult.UserLock == "Y")
                    {
                        TempData["FailLogin"] = "คุณใส่รหัสผิดเกินจำนวนครั้งที่กำหนด กรุณาติดต่อผู้ดูแลระบบ";
                        return RedirectToAction("Index", new { username = loginData.Username });
                    }
                    if (loginResult != null && loginResult.PasswordCorrect == "N")
                    {
                        TempData["FailLogin"] = "ชื่อผู้ใช้หรือรหัสผ่านไม่ถูกต้อง";
                        return RedirectToAction("Index", new { username = loginData.Username });
                    }
                    if (loginResult != null && (loginResult.PasswordCorrect == "Y" && loginResult.ForceChangePassword == "Y"))
                    {

                        var entxt = ENDEtxtManagement.Encrypt(loginResult.UserName);
                        var userSession = new ChangePasswordModel();
                        userSession.UserID = loginResult.UserID.ToString();
                        userSession.Username = loginResult.UserName;
                        userSession.FirstName = loginResult.FirstName;
                        userSession.LastName = loginResult.LastName;
                        Session["userSession"] = userSession;
                       
                        return RedirectToAction("FirstLogin");
                    }
                    if (loginResult != null && (loginResult.PasswordCorrect == "Y" && loginResult.ForceChangePassword == "N"))
                    {
                        var entxt = ENDEtxtManagement.Encrypt(loginResult.UserName);
                        var userSession = new UserSessionModel();
                        userSession.UserId = loginResult.UserID.ToString();
                        userSession.UserEmail = loginResult.UserEmail;
                        userSession.Tel = loginResult.Tel;
                        userSession.UserFirstNameEN = loginResult.FirstName;
                        userSession.UserLastNameEN = loginResult.LastName;
                        userSession.Username = loginResult.UserName;
                        userSession.UserImg = loginResult.UserIMG;
                        userSession.entext = entxt;
                        Session["UserProfile"] = userSession;
                        var userMenu = new List<UserMenuModel>();
                        var getUserMenu = db.sp_UM_SelectMenuByUserID(loginResult.UserID);
                        foreach (var menu in getUserMenu)
                        {
                            var userMenuModel = new UserMenuModel();
                            userMenuModel.MStatus = menu.Mstatus;
                            userMenuModel.MenuID = menu.MenuID;
                            userMenuModel.MenuName = menu.MenuName;
                            userMenuModel.Status = menu.status;
                            userMenuModel.Action = menu.MenuAction;
                            userMenuModel.Controller = menu.MenuControl;
                            userMenuModel.MenuShow = menu.MenuShow;
                            userMenuModel.Mgroup = menu.MenuParent.ToString();
                            userMenuModel.Mlevel = menu.MenuLevel.ToString();
                            userMenuModel.Morder = menu.menuno.ToString();
                            userMenu.Add(userMenuModel);
                        }
                        Session["UserMenu"] = userMenu;
                        TempData["LoginNew"] = "y";
                        /*Getbranch*/
                        var BranchModel = new CurrentBranchModel();
                        var Branchlist = new List<CurrentBranchModel>();
                        var Cookies = Request.Cookies["branchtxt"];
                        if (Cookies != null)
                        { 
                            if (Cookies.Value!="") {                                
                                BranchModel = CurrentBranchManagement.decode(Cookies.Value);
                                Branchlist = CurrentBranchManagement.GetCurrentBranch();
                                bool has = Branchlist.Any(txt => txt.branchname == BranchModel.branchname);
                                if (has) {
                                    Session["Branchlist"] = null;
                                    Session["GetBranch"] = BranchModel;
                                }
                                else {
                                    Session["Branchlist"] = Branchlist;
                                    Session["GetBranch"] = null;
                                }
                             
                            }
                        }
                        else
                        {
                            Branchlist = CurrentBranchManagement.GetCurrentBranch();
                            Session["Branchlist"] = Branchlist;
                            Session["GetBranch"] = null;

                        }
                        Session["Company"] = school.GetCompanyView();
                        /*END*/
                        return RedirectToAction("Main", "Overview");
                    }
                    else
                    {
                        return RedirectToAction("Index", new { username = loginData.Username });
                    }
                }
            }
            else
            {
                return View("Login");
            }
        }
        [NeedLoginFirst]
        public ActionResult FirstLogin()
        {
            ViewBag.GetCompany = school.GetCompanyView();
            ViewBag.Result = TempData["Result"];
            ViewBag.Message = TempData["Message"];
            return View();

        }
        [NeedLoginFirst]
        [HttpPost]
        public ActionResult ChangeFirstLoginPassword(ChangePasswordModel value)
        {
            var userManage = new UserManagement();
            if (ModelState.IsValid == false)
            {
                ViewBag.Result = TempData["Result"];
                ViewBag.Message = TempData["Message"];
                return RedirectToAction("FirstLogin");
            }
            else
            {
                var _result = userManage.ChangePassword(value);
                if (_result.Result.Equals("Y"))
                {
                    using (var db = new MDSEntities()) {
                        var loginResult = db.sp_UserLogin(value.Username, value.NewPassword, Request.UserHostAddress).FirstOrDefault();
                        var entxt = ENDEtxtManagement.Encrypt(loginResult.UserName);
                        var userSession = new UserSessionModel();
                        userSession.UserId = loginResult.UserID.ToString();
                        userSession.UserEmail = loginResult.UserEmail;
                        userSession.Tel = loginResult.Tel;
                        userSession.UserFirstNameEN = loginResult.FirstName;
                        userSession.UserLastNameEN = loginResult.LastName;
                        userSession.Username = loginResult.UserName;
                        userSession.UserImg = loginResult.UserIMG;
                        userSession.entext = entxt;
                        Session["UserProfile"] = userSession;
                        var userMenu = new List<UserMenuModel>();
                        var getUserMenu = db.sp_UM_SelectMenuByUserID(loginResult.UserID);
                        foreach (var menu in getUserMenu)
                        {
                            var userMenuModel = new UserMenuModel();
                            userMenuModel.MStatus = menu.Mstatus;
                            userMenuModel.MenuID = menu.MenuID;
                            userMenuModel.MenuName = menu.MenuName;
                            userMenuModel.Status = menu.status;
                            userMenuModel.Action = menu.MenuAction;
                            userMenuModel.Controller = menu.MenuControl;
                            userMenuModel.MenuShow = menu.MenuShow;
                            userMenuModel.Mgroup = menu.MenuParent.ToString();
                            userMenuModel.Mlevel = menu.MenuLevel.ToString();
                            userMenuModel.Morder = menu.menuno.ToString();
                            userMenu.Add(userMenuModel);
                        }
                        Session["UserMenu"] = userMenu;
                        TempData["LoginNew"] = "y";
                        /*Getbranch*/
                        var BranchModel = new CurrentBranchModel();
                        var Branchlist = new List<CurrentBranchModel>();
                        var Cookies = Request.Cookies["branchtxt"];
                        if (Cookies != null)
                        {
                            if (Cookies.Value != "")
                            {
                                BranchModel = CurrentBranchManagement.decode(Cookies.Value);
                                Branchlist = CurrentBranchManagement.GetCurrentBranch();
                                bool has = Branchlist.Any(txt => txt.branchname == BranchModel.branchname);
                                if (has)
                                {
                                    Session["Branchlist"] = null;
                                    Session["GetBranch"] = BranchModel;
                                }
                                else
                                {
                                    Session["Branchlist"] = Branchlist;
                                    Session["GetBranch"] = null;
                                }

                            }
                        }
                        else
                        {
                            Branchlist = CurrentBranchManagement.GetCurrentBranch();
                            Session["Branchlist"] = Branchlist;
                            Session["GetBranch"] = null;

                        }
                        TempData["Result"] = "Y";
                        TempData["Message"] = "บันทึกรหัสผ่านใหม่สำเร็จ";
                        Session["Company"] = school.GetCompanyView();
                        /*END*/
                        return RedirectToAction("Main", "Overview");
                    }
                }
                else
                {
                    TempData["Result"] = _result.Result;
                    TempData["Message"] = _result.Message;
                    return RedirectToAction("FirstLogin");

                }
            }
        }
    }
}
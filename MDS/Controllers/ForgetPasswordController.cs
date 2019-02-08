using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using MDS.DBEntity;
using MDS.DBExec;
using MDS.Fillter;
using Microsoft.Ajax.Utilities;
using RestSharp;
using RestSharp.Authenticators;

namespace MDS.Controllers
{
    public class ForgetPasswordController : Controller
    {
        [IsLoggedIn]
        public ActionResult Index()
        {
            return View();
        }

        [IsLoggedIn]
        public ActionResult ResetPassword(string value)
        {
            ViewData["UserID"] = value;
            ViewData["Message"] = TempData["FailLogin"];
            return View();
        }

        [IsLoggedIn]
        [HttpPost]
        public ActionResult ResetPassword(FormCollection Value)
        {
            var userid = Value["userid"].ToString();
            var Id = Convert.ToInt32(ENDEtxtManagement.Decrypt(userid));
            var NewPassword = Value["NewPassword"].ToString();
            string IpAddress = Request.UserHostAddress;
            if (Id != 0)
            {
                using (var db = new MDSEntities())
                {
                    var Dataalert = db.sp_ResetPwd(Id, NewPassword, IpAddress).SingleOrDefault();
                    var str = Dataalert.Split('_');
                    if (str[1] == "Y")
                    {
                        TempData["IsChange"] = str[0];
                    }
                    else {
                        TempData["FailLogin"] = str[0];
                    }

                }
                //Session.Abandon();
                return RedirectToAction("Index", "Login");
            }
            else
            {
                TempData["FailLogin"] = "กรุณาทำรายการใหม่อีกครั้ง";

            }
            return RedirectToAction("ResetPassword");
        }

        [IsLoggedIn]
        public ActionResult SendtoEmail(FormCollection Value)
        {
            UserManagement userManage = new UserManagement();
            var host = Request.Url.Authority;
            //var hostURL = Request.ApplicationPath;
            var fullURL = UrlHelper.GenerateUrl("Default", "Index", "Login", null, null, null, null, System.Web.Routing.RouteTable.Routes, Request.RequestContext, false);
           
            using (var db = new MDSEntities())
            {
                var Listdata = db.sp_ForgetPwd(Value["Email"]).ToList();

                foreach (var data in Listdata)
                {
                    var str = data.ErrorMessage.Split('_');
                    if (str[1] == "Y")
                    {
                        TempData["result"] = str[1];
                        TempData["SuccessMessage"] = str[0];
                    }
                    else
                    {
                        TempData["result"] = str[1];
                        TempData["FailLogin"] = str[0];
                    }
                    if (data.UserId != 0)
                    {
                        var model = userManage.SelectUserDataById(data.UserId.ToString());
                        var email = model.UserData.Email.ToString();
                      
                        try
                        {
                            var idtemp = ENDEtxtManagement.Encrypt(model.UserData.UserID);
                            MailMessage mm = new MailMessage();
                            mm.From = new System.Net.Mail.MailAddress("marstechnology2016@gmail.com");
                            mm.IsBodyHtml = true;
                            mm.To.Add(email.Trim());
                            mm.Subject = "Password Recovery";
                            mm.Body = string.Format("Hi {0},<br /><br />We got a request to reset your  password. <br /> " +
                                                    "<a href='{1}'> {1}<a>.<br />If you Ignore this message, your password won't be changed. " +
                                                    "<br /><br />Thank You.", model.UserData.FirstNameEN.ToString() + "  " + model.UserData.LastNameEN.ToString(), "http://" + host+ fullURL + "ForgetPassword/ResetPassword?value=" + idtemp);

                            mm.IsBodyHtml = true;

                            NetworkCredential NetworkCred = new NetworkCredential();
                            //NetworkCred.UserName = "admin@marsdrivingcenter.com";
                            //NetworkCred.Password = "mars1234";
                            //NetworkCred.UserName = "khuanchai27238@gmail.com";
                            //NetworkCred.Password = "nop27238";
                            NetworkCred.UserName = "marstechnology2016@gmail.com";
                            NetworkCred.Password = "wysiwyg123#";
                            System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient();
                            smtp.Host = "smtp.gmail.com";
                            smtp.Port = 587;
                            //smtp.Host = "smtp-relay.gmail.com";
                            //smtp.Port = 465;
                            //smtp.Host = "aspmx.l.google.com";
                            //smtp.Port = 25;
                            smtp.EnableSsl = true;
                            smtp.UseDefaultCredentials = false;
                            smtp.Credentials = NetworkCred;
                            smtp.Send(mm);
                            ViewBag.Result = TempData["result"];
                            ViewBag.Message = TempData["SuccessMessage"];
                        }
                        catch (Exception ex)
                        {
                            ViewBag.Result = "N";
                            ViewBag.Message = ex.Message;
                            return View("Index");
                        }

                        return View("Index");
                    }
                    else
                    {
                        ViewBag.Result = TempData["result"];
                        ViewBag.Message = TempData["FailLogin"];
                    }
                }

            }
            return View("Index");
        }  
    }
}
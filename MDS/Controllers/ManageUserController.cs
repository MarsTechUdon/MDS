using MDS.Models;
using MDS.DBExec;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MDS.Fillter;

namespace MDS.Controllers
{
    public class ManageUserController : Controller
    {
        UserManagement userManage = new UserManagement();
        [NeedLogin]
        public ActionResult ManageUser()
        {
            ViewBag.Result = TempData["Result"];
            ViewBag.Message = TempData["Message"];
            var model = userManage.SelectAllUser();
            return View("ManageUser", model);
        }

        [HttpPost]
        public ActionResult DeleteUser(string userId)
        {
            var result = userManage.DeleteUser(userId);
            TempData["Result"] = result.Result;
            TempData["Message"] = result.Message;
            return RedirectToAction("ManageUser");
        }
        [NeedLogin]
        public ActionResult AddUser()
        {
          var  model= userManage.SelectAddUser();
            var data = new UserModel();
            if (TempData["addData"] != null)
            {
                ViewBag.addData = TempData["addData"] as UserModel;
            }
            else {
                data.Username = "";
                data.EmpCode = "";
                data.PrefixId = "";
                data.UserGId = "";
                data.FirstNameTH = "";
                data.LastNameTH = "";
                data.FirstNameEN = "";
                data.LastNameEN = "";
                data.Email = "";
                data.Tel = "";
                data.Mobile = "";
                data.Fax = "";
                data.DepartId = "";
                data.Status = "";
                data.UserImage = "";
                ViewBag.addData = data;
            }
            ViewBag.Result = TempData["Result"];
            ViewBag.Message = TempData["Message"];
            return View("AddUser", model);
        }
        [NeedLogin]
        [HttpPost]
        public ActionResult InsertUser(UserModel addData)
        {
            if (addData.UserImageFile != null && addData.UserImageFile.ContentLength > 0)
            {
                var fileName = Path.GetFileName(addData.UserImageFile.FileName);
                var saveName = DateTime.Now.ToFileTime() + "_" + fileName;
                var path = Path.Combine(Server.MapPath("~/MDSMiddleFile/UserIMG"), saveName);
                addData.UserImage = saveName;
                addData.UserImageFile.SaveAs(path);
            }
            else {
                addData.UserImage = "";
            }
            var UserData = Session["UserProfile"] as UserSessionModel;
            addData.CreateBy = UserData.Username;
            var result = userManage.AddUser(addData);
            if (result.Result == "N")
            {
                TempData["addData"] = addData;
                TempData["Result"] = result.Result;
                TempData["Message"] = result.Message;
                return RedirectToAction("AddUser");
            }
            else {
                TempData["Result"] = result.Result;
                TempData["Message"] = result.Message;
                return RedirectToAction("ManageUser");
            }
         
        }
        [NeedLogin]
        public ActionResult EditUser(string userId)
        {
            var model = userManage.SelectUserDataById(userId);
            return View("EditUser", model);
        }
        [NeedLogin]
        [HttpPost]
        public ActionResult InsertEditUser(UserModel addData)
        {
            if (addData.UserImageFile != null && addData.UserImageFile.ContentLength > 0)
            {
                var fileName = Path.GetFileName(addData.UserImageFile.FileName);
                var saveName = DateTime.Now.ToFileTime() + "_" + fileName;
                var path = Path.Combine(Server.MapPath("~/MDSMiddleFile/UserIMG"), saveName);
                addData.UserImage = saveName;
                addData.UserImageFile.SaveAs(path);
            }
            var UserData = Session["UserProfile"] as UserSessionModel;
            addData.CreateBy = UserData.Username;
            var result = userManage.EditUser(addData);
            TempData["Result"] = result.Result;
            TempData["Message"] = result.Message;
            return RedirectToAction("ManageUser");
        }
    }
}
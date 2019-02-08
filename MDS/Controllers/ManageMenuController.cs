using MDS.Models;
using MDS.DBExec;
using MDS.Fillter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MDS.Controllers
{
    public class ManageMenuController : Controller
    {
        MenuManagement menuManage = new MenuManagement();

        [NeedLogin]
        public ActionResult ManageMenu()
        {
            ViewBag.Result = TempData["Result"];
            ViewBag.Message = TempData["Message"];
            var model = menuManage.SelectAllMenu();
            return View("ManageMenu", model);
        }

        [HttpPost]
        public ActionResult DeleteMenu(string menuId)
        {
            var result = menuManage.DeleteMenu(menuId);
            TempData["Result"] = result.Result;
            TempData["Message"] = result.Message;
            return RedirectToAction("ManageMenu");
        }

        [HttpPost]
        public ActionResult EditMenu(ManageMenuModel editData)
        {
            var userData = Session["UserProfile"] as UserSessionModel;
            editData.User = userData.Username;
            var result = menuManage.EditMenu(editData);
            TempData["Result"] = result.Result;
            TempData["Message"] = result.Message;
            return RedirectToAction("ManageMenu");
        }

        [HttpPost]
        public ActionResult EditLangMenu(MenuLangModel editData)
        {
            var userData = Session["UserProfile"] as UserSessionModel;
            var result = menuManage.DeleteLangMenu(editData.MenuId);
            if (result.Result == "Y")
            {
                foreach (var data in editData.MenuList)
                {
                    if (data.LangCode != null && data.LangName != null)
                    {
                        data.MenuId = editData.MenuId;
                        data.User = userData.Username;
                        result = menuManage.EditLangMenu(data);
                    }                    
                }
            }            
            TempData["Result"] = result.Result;
            TempData["Message"] = result.Message;
            return RedirectToAction("ManageMenu");
        }

        [HttpPost]
        public JsonResult SelectMenuById(string menuId)
        {
            var langDataJson = menuManage.SelectMenuById(menuId);
            return Json(langDataJson);
        }

        [HttpPost]
        public JsonResult SelectMenuAllLang(string menuId)
        {
            var langDataJson = menuManage.SelectMenuAllLang(menuId);
            langDataJson.MenuId = menuId;
            return Json(langDataJson);
        }
    }
}
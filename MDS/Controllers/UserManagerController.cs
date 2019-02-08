using MDS.DBEntity;
using MDS.DBExec;
using MDS.Fillter;
using MDS.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MDS.Controllers
{
    public class UserManagerController : Controller
    {
        UserManagement userManage = new UserManagement();

        [NeedLogin]
        public ActionResult AddUser()
        {
            ViewBag.ErrorMessage = TempData["ErrorMessage"];
            AddResultModel model = new AddResultModel();
            model.UserDataForForm = new UserModel();
            model.UserLoginForForm = new UserLoginModel();
            return View("AddUser", model);
        }

        [NeedLogin]
        [HttpPost]
        public ActionResult AddUser(UserModel userModel, UserLoginModel loginModel, FormCollection value)
        {
            var roleId = value["role"];
            string IP = Request.UserHostAddress;
            using (var db = new MDSEntities())
            {
                var add_result = db.sp_UM_AddUserDetail_bk(loginModel.Username, loginModel.Password, userModel.PrefixNameTH, userModel.FirstNameTH, userModel.LastNameTH, userModel.PrefixNameEN, userModel.FirstNameEN, userModel.LastNameEN, userModel.IdCard, userModel.Email, "admin", IP, userModel.Tel, "active", null, 3).First();
                AddResultModel model = new AddResultModel();
                model.result = add_result.result;
                model.UserID = add_result.UserID;
                model.UserName = add_result.Username;


                if (model.result.Equals("1"))
                {
                    model.UserDataForForm = new UserModel();
                    model.UserLoginForForm = new UserLoginModel();
                    return View("AddUser", model);
                }
                else if (model.result.Equals("0"))
                {
                    model.UserDataForForm = userModel;
                    model.UserLoginForForm = loginModel;
                    return View("AddUser", model);
                }
            }
            return null;
        }

        [NeedLogin]
        [MenuFillter]
        public ActionResult ManageUser()
        {
            using (var db = new MDSEntities())
            {
                var allUser = db.sp_SelectAllUserDetail();
                var model = new List<UserModel>();
                foreach (var user in allUser)
                {
                    var userModel = new UserModel();
                    userModel.PrefixNameEN = user.PrefixNameEN;
                    userModel.PrefixNameTH = user.PrefixNameTH;
                    userModel.Email = user.Email;
                    userModel.FirstNameEN = user.FirstNameEN;
                    userModel.LastNameEN = user.LastNameEN;
                    userModel.FirstNameTH = user.FirstNameTH;
                    userModel.LastNameTH = user.LastNameTH;
                    userModel.IdCard = user.IdCard;
                    userModel.Tel = user.Tel;
                    userModel.RoleName = user.RoleName;
                    userModel.Username = user.Username;
                    model.Add(userModel);
                }
                return View("ManageUser", model);
            }
        }

        [NeedLogin]
        public ActionResult GetGrantRoleUser(string UserId)
        {
            using (var db = new MDSEntities())
            {
                var UserDetail = db.sp_SelectAllUserDetail().ToList().FirstOrDefault(m => m.UserID == Int32.Parse(UserId));
                IList<RoleSelectionModel> ListRole = new List<RoleSelectionModel>();
                var RoleList = db.sp_SelectAllRole().ToList();
                foreach (var RoleData in RoleList)
                {
                    RoleSelectionModel RoleModel = new RoleSelectionModel();
                    if (UserDetail.RoleID == RoleData.RoleID)
                    {
                        RoleModel.RoleID = RoleData.RoleID;
                        RoleModel.RoleName = RoleData.RoleName;
                        RoleModel.UserRoleID = UserDetail.RoleID;
                    }
                    else
                    {
                        RoleModel.RoleID = RoleData.RoleID;
                        RoleModel.RoleName = RoleData.RoleName;
                        RoleModel.UserRoleID = 0;
                    }
                    ListRole.Add(RoleModel);
                }
                //ViewData["RoleData"] = ListRole;
                return Json(ListRole);
            }

        }

        [NeedLogin]
        [HttpPost]
        public ActionResult GrantRoleUser(string RoleId, string UserId)
        {
            using (var db = new MDSEntities())
            {
                var result = db.sp_UpdateRole(Int32.Parse(UserId), Int32.Parse(RoleId)).SingleOrDefault();
                if (result.result == "Y")
                {
                    return Json(result.alertMessage);
                }
                else
                {
                    return Json(result.alertMessage);
                }
            }
        }

        [NeedLogin]
        public ActionResult GetSelectedCheckboxMenu(string userId)
        {
            using (var db = new MDSEntities())
            {
                var menuList = db.sp_UM_SelectMenuFromUserID(Int32.Parse(userId)).ToList();

                IList<SelectedMenuModel> ListMenu = new List<SelectedMenuModel>();
                foreach (var MenuData in menuList)
                {
                    SelectedMenuModel menuModel = new SelectedMenuModel();
                    menuModel.MenuID = MenuData.MenuID;
                    menuModel.MenuName = MenuData.MenuName;
                    menuModel.MStatus = MenuData.Mstatus;
                    ListMenu.Add(menuModel);
                }
                return Json(ListMenu);
            }
        }

        [NeedLogin]
        [HttpPost]
        public ActionResult GrantMenu(string[] MenuId, string UserId)
        {
            var grantString = "";
            foreach (var str in MenuId)
            {
                grantString += str + ",";
            }
            //delete last Character
            grantString = grantString.Substring(0, grantString.Length - 1);

            using (var db = new MDSEntities())
            {
                var grant_result = db.sp_UM_GrantMenu(Int32.Parse(UserId), 123, grantString, Request.UserHostAddress);
            }

            return Json("SUCCESS");
        }

        [NeedLogin]
        public ActionResult EditProfile()
        {
            var userData = Session["UserProfile"] as UserSessionModel;
            var model = userManage.SelectUserDataById(userData.UserId);
            ViewBag.Result = TempData["Result"];
            ViewBag.Message = TempData["Message"];
            return View("EditProfile", model);
        }

        [NeedLogin]
        [HttpPost]
        public ActionResult EditProfile(UserModel userModel)
        {
            if (userModel.UserImageFile != null && userModel.UserImageFile.ContentLength > 0)
            {
                var fileName = Path.GetFileName(userModel.UserImageFile.FileName);
                var saveName = DateTime.Now.ToFileTime() + "_" + fileName;
                var path = Path.Combine(Server.MapPath("~/MDSMiddleFile/UserIMG"), saveName);
                userModel.UserImage = saveName;
                userModel.UserImageFile.SaveAs(path);
            }
            var result = userManage.EditProfile(userModel);
            TempData["Result"] = result.Result;
            TempData["Message"] = result.Message;
            return RedirectToAction("EditProfile");
        }

        #region ManageUserGroup

        [NeedLogin]
        public ActionResult ManageUserGroup()
        {
            UserGroupModel model = new UserGroupModel();
            var ListOfUserGroup = new UserManagement().GetAllUserGroup();

            ViewBag.msg = TempData["msg"];
            ViewBag.boolResult = TempData["boolResult"];
            return View("ManageUserGroup", ListOfUserGroup);
        }

        [NeedLogin]
        public ActionResult AddUserGroup(UserGroupModel ugm)
        {
            var userData = Session["UserProfile"] as UserSessionModel;
            var result = new UserManagement().AddUserGroup(ugm, userData.Username);
            TempData["msg"] = result.msg;
            TempData["boolResult"] = result.result;
            return RedirectToAction("ManageUserGroup");
        }

        [NeedLogin]
        public ActionResult GetUserGroupByID(string UserGId)
        {
            UserGroupModel model = new UserGroupModel();
            model = new UserManagement().GetUserGroupByID(Convert.ToInt32(UserGId));

            return Json(model);
        }

        [NeedLogin]
        public ActionResult GetMenuByUserGroupId(string UserGId)
        {
            GrantMenuModel model = new GrantMenuModel();
            model.ugmData = new UserManagement().GetMenuByUserGroupId(Convert.ToInt32(UserGId));
            model.UserGId = UserGId;
            return Json(model);
        }

        [NeedLogin]
        public ActionResult GrantMenuByUserGId(UserGroupModel model)
        {
            var allMenu = new UserManagement().GetMenuByUserGroupId(Convert.ToInt32(model.UserGId));
            foreach (var menu in allMenu)
            {
                var status = "N";
                if (model.arrMenuId != null)
                {
                    if (model.arrMenuId.FirstOrDefault(x => x == menu.MenuId) != null)
                    {
                        status = "A";
                    }
                }                
                var result = new UserManagement().SetGrantMenuById(Convert.ToInt32(menu.MenuId),Convert.ToInt32(model.UserGId), status);
                TempData["msg"] = result.msg;
                TempData["boolResult"] = result.result;
            }          

            return RedirectToAction("ManageUserGroup");
        }

        [NeedLogin]
        public ActionResult EditUserGroup(UserGroupModel ugm)
        {
            var userData = Session["UserProfile"] as UserSessionModel;
            var result = new UserManagement().EditUserGroup(ugm, userData.Username);
            TempData["msg"] = result.msg;
            TempData["boolResult"] = result.result;
            return RedirectToAction("ManageUserGroup");
        }

        [NeedLogin]
        public ActionResult DeleteUserGroupByID(string uid)
        {
            var result = new UserManagement().DeleteUserGroupByID(Convert.ToInt32(uid));
            TempData["msg"] = result.msg;
            TempData["boolResult"] = result.result;
            return RedirectToAction("ManageUserGroup");
        }

        #endregion
    }
}
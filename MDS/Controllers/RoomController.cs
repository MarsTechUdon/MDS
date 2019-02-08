using MDS.DBExec;
using MDS.Fillter;
using MDS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using MDS.DBEntity;


namespace MDS.Controllers
{
    public class RoomController : Controller
    {
        // GET: Room
        public ActionResult Index()
        {
            return View();
        }
        [NeedLogin]
        public ActionResult Room()
        {
            RoomModel model = new RoomModel();
            var ListOfRoom = new RoomManagement().GetAllRoom();

            ViewBag.msg = TempData["msg"];
            ViewBag.boolResult = TempData["boolResult"];
            return View("room", ListOfRoom);
        }

        [NeedLogin]
        public ActionResult AddRoom(RoomModel ugm)
        {
         var userData = Session["UserProfile"] as UserSessionModel;
         var result = new RoomManagement().AddRoom2(ugm, userData.Username);
          TempData["msg"] = result.msg;
          TempData["boolResult"] = result.result;
            return RedirectToAction("Room");
        }
        [NeedLogin]
        public ActionResult EditRoom(RoomModel ugm)
        {
            var userData = Session["UserProfile"] as UserSessionModel;
            var result = new RoomManagement().EditRoom2(ugm, userData.Username);
            TempData["msg"] = result.msg;
            TempData["boolResult"] = result.result;
            return RedirectToAction("Room");
        }

        public ActionResult CancelRoom(RoomModel ugm)
        {
            var userData = Session["UserProfile"] as UserSessionModel;
            var result = new RoomManagement().CancelRoom2(ugm, userData.Username);
            TempData["msg"] = result.msg;
            TempData["boolResult"] = result.result;
            return RedirectToAction("Room");
        }
        
        public ActionResult ReCancelRoom(RoomModel ugm)
        {
            var userData = Session["UserProfile"] as UserSessionModel;
            var result = new RoomManagement().ReCancelRoom2(ugm, userData.Username);
            TempData["msg"] = result.msg;
            TempData["boolResult"] = result.result;
            return RedirectToAction("Room");
        }
    }
}
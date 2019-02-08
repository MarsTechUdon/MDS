using MDS.DBEntity;
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
    public class VehicleController : Controller
    {
        VehicleManagement vehicleManage = new VehicleManagement();
        [NeedLogin]
        public ActionResult VehicleList()
        {
            VehicleModel model = new VehicleModel();
            var ListVehicle = new VehicleManagement().GetAllVehicle();
           
            return View("VehicleList", ListVehicle);
        }

        // POST: Vehicle/Create
        [NeedLogin]
        [HttpPost]
        public ActionResult CreateVehicle(VehicleModel modelData)
        {
            var userData = Session["UserProfile"] as UserSessionModel;
            var result = vehicleManage.addVehicle(modelData, userData.Username);
             //TempData["msg"] = result.msg;
             //TempData["boolResult"] = result.result;
            return RedirectToAction("VehicleList");
        }


        // POST: Vehicle/Edit/5
        [NeedLogin]
        [HttpPost]
        public ActionResult EditVehicle(FormCollection Value)
        {
            VehicleModel model = new VehicleModel();
            var UserData = Session["UserProfile"] as UserSessionModel;
            model.User = UserData.Username;
            model.Ind = Value["ind"];
            model.Carno1 = Value["edit_Carno1"];
            model.CarNo2 = Value["edit_Carno2"];
            model.CarChangWat = Value["edit_CarChangWat"];
            model.CarType = Value["edit_CarType"];
            model.GearID = Value["edit_GearID"];
            model.CarSubType = Value["edit_CarSubType"];
            model.ChassisNo = Value["edit_ChassisNo"];
            model.EngineNo = Value["edit_EngineNo"];
            model.Brand = Value["edit_Brand"];
            model.ExpireDate = Value["edit_ExpireDate"];

            var res = vehicleManage.editVehicle(model, model.User);
            return RedirectToAction("VehicleList");
        }

        // POST: Vehicle/Delete/5
        [NeedLogin]
        [HttpPost]
        public ActionResult DeleteVehicle(FormCollection Value)
        {
            string id = Value["frmId"].ToString();
            var UserData = Session["UserProfile"] as UserSessionModel;
            string User = UserData.Username;
            var model = new ResultModel();
            model = vehicleManage.DeleteVehicle(id, User);
            TempData["Result"] = model.Result;
            TempData["Message"] = model.Message;

            return RedirectToAction("VehicleList");
        }

        [NeedLogin]
        [HttpPost]
        public ActionResult ReDeleteVehicle(FormCollection Value)
        {
            string id = Value["frmreId"].ToString();
            var UserData = Session["UserProfile"] as UserSessionModel;
            string User = UserData.Username;
            var model = new ResultModel();
            model = vehicleManage.ReDelete(id, User);
            TempData["Result"] = model.Result;
            TempData["Message"] = model.Message;

            return RedirectToAction("VehicleList");
        }
    }
}

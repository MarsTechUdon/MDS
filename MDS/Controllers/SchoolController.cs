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
    public class SchoolController : Controller
    {
        // GET: School
        SchoolManagement school = new SchoolManagement();
        [NeedLogin]
        public ActionResult Schoolinfo()
        {
            ViewBag.Result = TempData["Result"];
            ViewBag.Message = TempData["Message"];
            ViewBag.ResultCF = TempData["ResultCF"];
            ViewBag.MessageCF = TempData["MessageCF"];

            ViewBag.paraid = TempData["paraid"];
            ViewBag.CFtype = TempData["CFtype"];
            if (TempData["tabActive"] != null)
            {
                ViewBag.tabActive = TempData["tabActive"];
            }
            else {
                ViewBag.tabActive = "1";
            }
            ViewBag.GetCompany = school.GetCompany();
            ViewBag.GetListConfig = school.GetListConfig();
            return View();
        }
        [NeedLogin]
        public ActionResult EditCompany(CompanyModel value, HttpPostedFileBase CompanyLogoFile, HttpPostedFileBase SchoolLogoFile, HttpPostedFileBase schoolfaviconFile)
        {
            string filePathCompany = string.Empty;
            string filePathSchool = string.Empty;
            string filePathfavicon = string.Empty;
            var model = new ResultBookingModel();
            var userData = Session["UserProfile"] as UserSessionModel;
            value.updateby = userData.Username;
            string logopath = school.getLogoPath();
            string path = Server.MapPath("~/"+ logopath);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            if (CompanyLogoFile != null)
            {               
                var tempFileCompany = System.IO.Path.GetTempFileName();
                string extensionCompany = Path.GetExtension(CompanyLogoFile.FileName);
                //filePath = path + Path.GetFileName(postedFile.FileName);
                string FileNameCompany = "CompanyLogo" + extensionCompany;
                filePathCompany = path + FileNameCompany;
                //var ccc = System.IO.File.Exists(filePathCompany);
                if (System.IO.File.Exists(filePathCompany))
                {
                    try
                    {
                        System.IO.File.Copy(filePathCompany, tempFileCompany, true);
                        System.IO.File.Delete(tempFileCompany);
                        CompanyLogoFile.SaveAs(filePathCompany);
                        value.CompanyLogo = FileNameCompany;
                    }
                    catch (Exception)
                    {
                    }
                }
                else {
                    System.IO.File.Delete(tempFileCompany);
                    CompanyLogoFile.SaveAs(filePathCompany);
                    value.CompanyLogo = FileNameCompany;
                }
            }
            if (SchoolLogoFile != null)
            {
                var tempFileSchool = System.IO.Path.GetTempFileName();
                string extensionSchool = Path.GetExtension(SchoolLogoFile.FileName);
                string FileNameSchool = "SchoolLogo" + extensionSchool;
                filePathSchool = path + FileNameSchool;
                //var ccc = System.IO.File.Exists(filePathSchool);
                if (System.IO.File.Exists(filePathCompany))
                {
                    try
                    {
                        System.IO.File.Copy(filePathSchool, tempFileSchool, true);
                        System.IO.File.Delete(tempFileSchool);
                        SchoolLogoFile.SaveAs(filePathSchool);
                        value.SchoolLogo = FileNameSchool;
                    }
                    catch (Exception)
                    {
                    }
                }
                else {
                    System.IO.File.Delete(tempFileSchool);
                    SchoolLogoFile.SaveAs(filePathSchool);
                    value.SchoolLogo = FileNameSchool;
                }
                    
            }
            if (schoolfaviconFile != null)
            {
                var tempFilefavicon = System.IO.Path.GetTempFileName();
                string extensionfavicon = Path.GetExtension(schoolfaviconFile.FileName);
                string FileNamefavicon = "Favicon" + extensionfavicon;
                filePathfavicon = path + FileNamefavicon;
                //var ccc = System.IO.File.Exists(filePathfavicon);
                if (System.IO.File.Exists(filePathfavicon))
                {
                    try
                    {
                        System.IO.File.Copy(filePathfavicon, tempFilefavicon, true);
                        System.IO.File.Delete(tempFilefavicon);
                        schoolfaviconFile.SaveAs(filePathfavicon);
                        value.schoolfavicon = FileNamefavicon;
                    }
                    catch (Exception)
                    {
                    }
                }
                else {
                    System.IO.File.Delete(tempFilefavicon);
                    schoolfaviconFile.SaveAs(filePathfavicon);
                    value.schoolfavicon = FileNamefavicon;
                }
            }
            model = school.EditCompany(value);
            Session.Remove("Company");
            Session["Company"] = school.GetCompanyView();
            TempData["Result"] = model.result;
            TempData["Message"] = model.msg;
            TempData["tabActive"] = "1";
            return RedirectToAction("Schoolinfo");
        }
        [NeedLogin]
        [HttpPost]
        public ActionResult SetConfig(string paraid, string paravalue,string paranameName, string type)
        {
            var model = new ResultBookingModel();
            model = school.SetConfig(paraid, paravalue);
            if (model.result == "Y")
            {
                TempData["MessageCF"] = "แก้ไข "+ paranameName+" สำเร็จ";
            }
            else {
                TempData["MessageCF"] = "แก้ไข " + paranameName + " ไม่สำเร็จ";
            }
            TempData["ResultCF"] = model.result;
            TempData["CFtype"] = type;
            TempData["tabActive"] = "2";
            TempData["paraid"] = paraid;
            
            return RedirectToAction("Schoolinfo", "School");
            //return Json(model, JsonRequestBehavior.AllowGet);
        }
    }
}
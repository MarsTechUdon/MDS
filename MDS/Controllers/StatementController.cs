using MDS.DBExec;
using MDS.Fillter;
using MDS.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MDS.Controllers
{
    public class StatementController : Controller
    {
        StatementManagement OverviewMN = new StatementManagement();
        // GET: Statement
        public ActionResult Index()
        {
            return View();
        }
        
        [NeedLogin]

        public ActionResult StatementList(string ugmData, string ugmData2)
        {
            String t = DateTime.Now.ToString("hh:mm");
            ViewBag.temp_time = t;
            if (string.IsNullOrEmpty(ugmData) || string.IsNullOrEmpty(ugmData2))
            {
                ViewBag.detail = new StatementManagement().GetStByAccount();
                ViewBag.list = new StatementManagement().GetListAccount();
                //String d = DateTime.Now.ToString("yyyy-MM-dd");
               
                //ViewBag.temp_search_datef = d;
                //ViewBag.temp_search_datet = d;

                string momo = DateTime.Now.ToString("yyyy");
                var num = Convert.ToInt32(momo);

                if (num >= 2561)
                {
                    num = num - 543;
                    var jojo = DateTime.Now.ToString("MM-dd");
                    ViewBag.temp_search_datef = num + "-" + jojo;
                    ViewBag.temp_search_datet = num + "-" + jojo;
                }
                else
                {
                    ViewBag.temp_search_datef = DateTime.Now.ToString("yyyy-MM-dd");
                    ViewBag.temp_search_datet = DateTime.Now.ToString("yyyy-MM-dd");
                }
            }
            else
            {
                ViewBag.detail = new StatementManagement().GetStByAccount2(ugmData, ugmData2);
                ViewBag.list = new StatementManagement().GetListAccount2(ugmData, ugmData2);
                ViewBag.temp_search_datef = ugmData;
                ViewBag.temp_search_datet = ugmData2;
            }
            ViewBag.bankaccount = new StatementManagement().GetBankaccount();
            ViewBag.msg = TempData["msg"];
            ViewBag.boolResult = TempData["boolResult"];

            return View("StatementList");
            // "StatementList", ListOfRcName
        }

        [NeedLogin]
        public ActionResult AddStatement(StatementModel ugm, string ugmData, string ugmData2)
        {
            var userData = Session["UserProfile"] as UserSessionModel;
            var result = new StatementManagement().AddStatement2(ugm, userData.Username);
            TempData["msg"] = result.msg;
            TempData["boolResult"] = result.result;
            return RedirectToAction("StatementList", new { ugmData, ugmData2 });
        }
        [NeedLogin]
        public ActionResult EditStatement(StatementModel ugm, string ugmData, string ugmData2)
        {
            var userData = Session["UserProfile"] as UserSessionModel;
            var result = new StatementManagement().EditStatement2(ugm, userData.Username);

            TempData["msg"] = result.msg;
            TempData["boolResult"] = result.result;

            // return RedirectToAction("StatementList");
            return RedirectToAction("StatementList", new { ugmData, ugmData2 });
        }

        [NeedLogin]
        public ActionResult DeleteStatement(StatementModel ugm, string ugmData, string ugmData2)
        {
            var userData = Session["UserProfile"] as UserSessionModel;
            var result = new StatementManagement().DeleteStatement2(ugm, userData.Username);
            TempData["msg"] = result.msg;
            TempData["boolResult"] = result.result;
            return RedirectToAction("StatementList", new { ugmData, ugmData2 });
        }

        [NeedLogin]
        public ActionResult ApproveStatement()
        {
            var result = new StatementManagement().ApproveStatement();
            TempData["msg"] = result.msg;
            TempData["boolResult"] = result.result;
            return RedirectToAction("StatementList");
        }

        /////////////////////////////////////////////////////////////////////////////////
        [NeedLogin]
        public ActionResult UploadStatement()
        {
            ViewBag.TempAccountNo = TempData["accountNo"];
            ViewBag.Status = TempData["status"];
            ViewBag.accountno = new StatementManagement().GetBankaccount();
            return View(new List<StatementModel>());
        }

        [NeedLogin]
        [HttpPost]
        public ActionResult UploadStatement(FormCollection Value, HttpPostedFileBase postedFile)
        {
            List<StatementModel> ListStatement = new List<StatementModel>();
            string filePath = string.Empty;
            string line;
            string accountTemp = "";
            string strresult = "";
            string ac_no = "";
            ac_no = Value["accountno"].ToString();
            StreamReader file = null;
            var i = 0;
            if (postedFile != null)
            {
                string path = Server.MapPath("~/Content/Uploads/");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                var tempFile = System.IO.Path.GetTempFileName();
                filePath = path + Path.GetFileName(postedFile.FileName);
                string extension = Path.GetExtension(postedFile.FileName);
                var ccc = System.IO.File.Exists(filePath);
                if (System.IO.File.Exists(filePath))
                {
                    try
                    {
                        System.IO.File.Copy(filePath, tempFile, true);
                        System.IO.File.Delete(tempFile);
                        postedFile.SaveAs(filePath);
                    }
                    catch (Exception)
                    {
                    }
                }
                else
                {
                    System.IO.File.Delete(filePath);
                    postedFile.SaveAs(filePath);
                }
                

                try
                {
                    file = new StreamReader(filePath);

                    while ((line = file.ReadLine()) != null)
                    {
                        i++;
                    }
                }
                catch (FileNotFoundException e)
                {
                    Console.WriteLine("Error: File not found.");
                }

                //Read Line is Define
                try
                {
                    file = new StreamReader(filePath);
                    var k = 0;
                    while ((line = file.ReadLine()) != null)
                    {
                        k++;
                        Console.WriteLine(line);
                        //Find Account Number
                        if (k == 2)
                        {
                            accountTemp = line.Split('\"')[1];
                        }
                        if (k >= 5 && k <= (i - 5))
                        {
                            ListStatement.Add(new StatementModel
                            {
                                transdate = line.Split('\"')[1],
                                effectdate = line.Split('\"')[3],
                                description = line.Split('\"')[5],
                                debit = line.Split('\"')[7],
                                credit = line.Split('\"')[9],
                                balance = line.Split('\"')[11],
                                channel = line.Split('\"')[13],
                                chk_acno = accountTemp,
                            });
                        }
                    }
                }
                catch (FileNotFoundException e)
                {
                    Console.WriteLine("Error: File not found.");
                }
                TempData["accountNo"] = accountTemp;
            }
            try
            {
                strresult = ListStatement.FirstOrDefault(x => x.chk_acno.Equals(ac_no)).chk_acno;//
                if (strresult != "")
                {
                    //upload
                    var UserData = Session["UserProfile"] as UserSessionModel;
                    foreach (var item in ListStatement)
                    {
                        if (item.transdate != "" && item.effectdate != "")
                        {
                            var result = StatementManagement.addupload(item, strresult, UserData.Username);
                        }
                    }
                    TempData["status"] = "Y";
                }
            }
            catch (Exception ex)
            {
                TempData["status"] = "N";
            }
            ViewBag.TempAccountNo = TempData["accountNo"];
            ViewBag.Status = TempData["status"];
            ViewBag.accountno = new StatementManagement().GetBankaccount();
            return View(ListStatement);
        }

        public static List<StatementModel> ConvertXSLXtoDataTable(string strFilePath, string connString, string ac_no)
        {
            OleDbConnection oledbConn = new OleDbConnection(connString);
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            try
            {

                oledbConn.Open();
                using (DataTable Sheets = oledbConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null))
                {

                    for (int i = 0; i < Sheets.Rows.Count; i++)
                    {
                        string worksheets = Sheets.Rows[i]["TABLE_NAME"].ToString();
                        OleDbCommand cmd = new OleDbCommand(String.Format("SELECT * FROM [{0}]", worksheets), oledbConn);
                        OleDbDataAdapter oleda = new OleDbDataAdapter();
                        oleda.SelectCommand = cmd;

                        oleda.Fill(ds);
                    }

                    dt = ds.Tables[0];
                }

            }
            catch (Exception ex)
            {
            }

            finally
            {

                oledbConn.Close();
            }
            List<StatementModel> list = new List<StatementModel>();
            foreach (DataRow item in dt.Rows)
            {
                StatementModel model = new StatementModel();
                model.transdate = item[0].ToString();
                model.effectdate = item[1].ToString();
                model.description = item[2].ToString();
                model.debit = item[3].ToString();
                model.credit = item[4].ToString();
                model.balance = item[5].ToString();
                model.channel = item[6].ToString();
                model.chk_acno = item[7].ToString();
                list.Add(model);

            }
            return list;

        }
    }
}
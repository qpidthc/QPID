using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;

namespace WebHTCBackEnd.Controllers
{
    public class EventExportController : Controller
    {
        // GET: EventExport
        public ActionResult Index()
        {
            if (Session["acc"] == null)
            {
                return View("../Register/index");
            }
            ViewBag.ACCOUNT = Session["acc"].ToString();

            return View();
        }

        public ActionResult ExportData()
        {
            if (Session["acc"] == null)
            {
                return View("../Register/index");
            }
            ViewBag.ACCOUNT = Session["acc"].ToString();

            Error.Error error = null;
            string[] top10Events;

            WebHTCBackEnd.Models.Events.THC_EventExport objExport = new Models.Events.THC_EventExport();
            objExport.queryTop10Events(out top10Events, out error);

            if (error != null)
            {
                //ERROR Page
                return View();
            }
            else
            {
                DataTable rewardData = null;
                var lanSet = new Language.Event_Export();
                lanSet.CurrentZone = THC_Library.Language.LanguageBase.CURRENT_LANGUAGE;
                ViewData["lan"] = lanSet;
                ViewBag.TOP10_EVENTS = top10Events;
                //ViewBag.reward_types = classes.RewardType.GetRewardType(Language.LanguageBase.CURRENT_LANGUAGE);
                //ViewBag.reward_venders = classes.RewardVender.GetRewardVender(Language.LanguageBase.CURRENT_LANGUAGE);
                return View(rewardData);
            }
        }

        public ActionResult EventExportSearch(string event_no, string event_name)
        {
            if (Session["acc"] == null)
            {
                return View("../Register/index");
            }
            ViewBag.ACCOUNT = Session["acc"].ToString();

            Error.Error error = null;
            //string strEventKey, strEventNo, strEventName, strVenderNo, strVenderName;
            string[] top10Events;
            string eventKey;

            WebHTCBackEnd.Models.Events.THC_EventExport objExport = new Models.Events.THC_EventExport();
            DataTable exportData = objExport.queryExportSearch(event_no, event_name, out eventKey, out top10Events, out error);

            if (error != null)
            {
                return View();
            }
            else
            {
                var lanSet = new Language.Event_Export();
                lanSet.CurrentZone = THC_Library.Language.LanguageBase.CURRENT_LANGUAGE;
                ViewData["lan"] = lanSet;
                //ViewBag.product_types = classes.ProductType.GetProductType(Language.LanguageBase.CURRENT_LANGUAGE);
                ViewBag.s_event_no = event_no;
                ViewBag.s_event_name = event_name;               
                ViewBag.TOP10_EVENTS = top10Events;
                ViewBag.EVENT_KEY = eventKey;
                //ViewBag.reward_venders = classes.RewardVender.GetRewardVender(Language.LanguageBase.CURRENT_LANGUAGE);
                return View("ExportData", exportData);
            }
        }

        [HttpPost]
        public JsonResult EventExportLog(string event_key, string event_name)
        {
            Error.Error error = null;
            WebHTCBackEnd.Models.Events.THC_EventExport objExport = new Models.Events.THC_EventExport();
            DataTable logData = objExport.queryExportLog(event_key, out error);

            string retJson;
            if (error != null)
            {
                retJson = Newtonsoft.Json.JsonConvert.SerializeObject(error);
            }
            else
            {
                retJson = Newtonsoft.Json.JsonConvert.SerializeObject(logData);
            }

            return Json(retJson, "application/json", JsonRequestBehavior.AllowGet);
        }
                
        [HttpPost]
        public JsonResult NewEventDownload()
        {
            var EventKey = Request.Form["event_key"];
            var EventName = Request["event_name"];
            var Employee = Request.Form["txt_employee"];

            Error.Error error = null;
            int iNewId;
            string completeTime;

            string fileName = string.Format("{0}_{1}.txt", EventName, DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss"));
            string csvFileName = string.Format("{0}\\{1}",
                                  Server.MapPath("~/exports"),
                                  fileName);
            if (System.IO.File.Exists(csvFileName))
            {
                System.IO.File.Delete(csvFileName);
            }

            WebHTCBackEnd.Models.Events.THC_EventExport objExport = new Models.Events.THC_EventExport();
            DataTable exportTable = objExport.addNewExport(csvFileName, EventKey, Employee, out iNewId, out completeTime, out error);

            string retJson;
            if (error != null)
            {
                retJson = Newtonsoft.Json.JsonConvert.SerializeObject(error);
            }
            else
            {
                //DateTime datCompleteTime = DateTime.Parse(completeTime);
                //string fileName = string.Format("{0}_{1}.csv", EventName, DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss"));
                //string csvFileName = string.Format("{0}\\{1}",
                //                      Server.MapPath("~/exports"),
                //                      fileName);

                //EQC001,EQC002,EQC003,EQC004,EQC005,EC,AEP003,AEP004,AEP005,AEP006,AE003,AE013,AE014                
                //if (System.IO.File.Exists(csvFileName))
                //{
                //    System.IO.File.Delete(csvFileName);
                //}
                //System.IO.StreamWriter csvWrite = new System.IO.StreamWriter(csvFileName, false, System.Text.Encoding.UTF8);
                //string strLine;
                //foreach (DataRow row in exportTable.Rows)
                //{
                //    //EQC001,EQC002,EQC003,EQC004,EQC005,EC,AEP003,AEP004,AEP005,AEP006,AEP011,AEP012,AEP013,AE002,AE003,AE013,AE014,AE019
                //    strLine = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16}",
                //                    row["EQC001"], row["EQC002"], row["EQC003"], row["EQC004"], row["EQC005"],
                //                    row["EC"], row["AEP003"], row["AEP004"], row["AEP005"], row["AEP006"],row["AEP011"],
                //                    row["AEP012"], row["AEP013"], row["AE002"], row["AE003"], row["AE013"], row["AE014"], row["AE019"]);
                //    csvWrite.WriteLine(strLine);
                //}
                //csvWrite.Close();
                
                retJson = string.Format("[{{ \"NewId\" : {0}, \"DateTime\" : \"{1}\" ,\"FILE\" : \"{2}\" }}]",
                                iNewId, completeTime, fileName);
            }

            return Json(retJson, "application/json", JsonRequestBehavior.AllowGet);
        }

    }
}
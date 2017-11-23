using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;

namespace WebTHCEventUI.Controllers
{
    public class BackendController : Controller
    {
        // GET: Backend
        public ActionResult Index()
        {
            if (Session["acc"] == null)
            {
                return View("../Control/index");
            }
            Models.ControlUser ctlUser = (Models.ControlUser)Session["acc"];
            ViewBag.ACCOUNT = ctlUser.Account;
            ViewBag.ADMIN = ctlUser.IsAdmin;
            return View();
        }

        public ActionResult EventActivities()
        {
            if (Session["acc"] == null)
            {
                return View("../Control/index");
            }
            Models.ControlUser ctlUser = (Models.ControlUser)Session["acc"];
            ViewBag.ACCOUNT = ctlUser.Account;
            ViewBag.ADMIN = ctlUser.IsAdmin;

            THC_Library.Error error;

            Models.MyEvent myEvent = new Models.MyEvent();
            DataTable eventsTable = myEvent.getMyEventList(out error);
                        
            if (error != null)
            {
                ViewBag.Error = error.ErrorMessage;
                return View("Error");               
            }
            else
            {                
                ViewBag.Data = eventsTable;
                return View();
            }           
        }

        [HttpPost]
        public ActionResult EventInfo(string event_no)
        {
            THC_Library.Error error;

            Models.MyEvent myEvent = new Models.MyEvent();
            DataTable eventTable = myEvent.getMyEvent(event_no, out error);

            string retJson;
            if (error != null)
            {
                retJson = Newtonsoft.Json.JsonConvert.SerializeObject(error);               
            }
            else
            {
                retJson = Newtonsoft.Json.JsonConvert.SerializeObject(eventTable);                
            }
            return Json(retJson, "application/json", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult EventModify()
        {
            var reqEventNo = Request.Form["event_no"];
            var reqActivityPage = Request.Form["activity_page"];

            THC_Library.Error error;
            
            Models.MyEvent myEvent = new Models.MyEvent();
            myEvent.updateEvent(reqEventNo, reqActivityPage, out error);

            string retJson;
            if (error != null)
            {
                retJson = Newtonsoft.Json.JsonConvert.SerializeObject(error);
            }
            else
            {
                retJson = "{ \"result\" : 1 }";
            }
            return Json(retJson, "application/json", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult EventAsync(string event_no)
        {
            THC_Library.Error error;
            string retJson;

            if (Session["acc"] == null)
            {
                error = THC_Library.Error.AbortError();
                retJson = Newtonsoft.Json.JsonConvert.SerializeObject(error);
                return Json(retJson, "application/json", JsonRequestBehavior.AllowGet);
            }

            Models.MyEvent myEvent = new Models.MyEvent();
            myEvent.AsyneEvent(event_no, out error);
                       
            if (error != null)
            {
                retJson = Newtonsoft.Json.JsonConvert.SerializeObject(error);
            }
            else
            {
                retJson = "{ \"result\" : 1 }";
            }
            return Json(retJson, "application/json", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult EventClear(string event_no)
        {
           
            THC_Library.Error error;
            string retJson;

            if (Session["acc"] == null)
            {
                error = THC_Library.Error.AbortError();               
                retJson = Newtonsoft.Json.JsonConvert.SerializeObject(error);
                return Json(retJson, "application/json", JsonRequestBehavior.AllowGet);
            }

            Models.MyEvent myEvent = new Models.MyEvent();
            myEvent.ClearEvent(event_no, out error);
           
            if (error != null)
            {
                retJson = Newtonsoft.Json.JsonConvert.SerializeObject(error);
            }
            else
            {
                retJson = "{ \"result\" : 1 }";
            }
            return Json(retJson, "application/json", JsonRequestBehavior.AllowGet);
        }
              
        public ActionResult EventRewards()
        {
            if (Session["acc"] == null)
            {
                return View("../Control/index");
            }
            Models.ControlUser ctlUser = (Models.ControlUser)Session["acc"];
            ViewBag.ACCOUNT = ctlUser.Account;
            ViewBag.ADMIN = ctlUser.IsAdmin;

            THC_Library.Error error;

            Models.MyEvent myEvent = new Models.MyEvent();
            DataTable eventsTable = myEvent.getMyEventList(out error);

            if (error != null)
            {
                ViewBag.Error = error.ErrorMessage;
                return View("Error");
            }
            else
            {
                var lanSet = new THC_Library.Language.LanguageBase();
                lanSet.CurrentZone = THC_Library.Language.LanguageBase.CURRENT_LANGUAGE;
                ViewData["lan"] = lanSet;    
                ViewBag.Data = eventsTable;
                return View();
            }           
        }

        [HttpPost]
        public JsonResult EventRewardInfo(string event_no)
        {
            THC_Library.Error error;

            Models.MyEvent myEvent = new Models.MyEvent();
            DataTable eventTable = myEvent.getEventRewards(event_no, out error);

            string retJson;
            if (error != null)
            {
                retJson = Newtonsoft.Json.JsonConvert.SerializeObject(error);
            }
            else
            {
                retJson = Newtonsoft.Json.JsonConvert.SerializeObject(eventTable);
            }

            //int recordsTotal = eventTable.Rows.Count;

            //var j = "[ {222,33333,2222,3331,1,4,5}]";
            //return Json(new { recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = retJson }, JsonRequestBehavior.AllowGet);
            return Json(retJson, "application/json", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SingleRewardInfo(string reward_key)
        {
            THC_Library.Error error;

            Models.MyEvent myEvent = new Models.MyEvent();
            DataTable rewardTable = myEvent.getSingleRewardInfo(reward_key, out error);

            string retJson;
            if (error != null)
            {
                retJson = Newtonsoft.Json.JsonConvert.SerializeObject(error);
            }
            else
            {
                retJson = Newtonsoft.Json.JsonConvert.SerializeObject(rewardTable);
            }                                      
            return Json(retJson, "application/json", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult UpdateEventReward(string reward_key)
        {
            THC_Library.Error error;

            var rwd_name = Request.Form["txt_name"];
            var rwd_memo = Request.Form["txt_memo"];
            var rwd_vender = Request.Form["txt_vender"];
            var rwd_img = Request.Form["txt_img"];
            var rwd_effect_date = Request.Form["txt_effective_time"];
            var rwd_sms = Request.Form["txt_sms"];

            Models.MyEvent myEvent = new Models.MyEvent();
            int iAffect = myEvent.updateRewardInfo(reward_key, rwd_name, rwd_memo, rwd_vender, 
                                rwd_img, rwd_effect_date, rwd_sms, out error);

            string retJson;
            if (error != null)
            {
                retJson = Newtonsoft.Json.JsonConvert.SerializeObject(error);
            }
            else
            {
                retJson = "{ \"result\" : 1 }";
            }
            return Json(retJson, "application/json", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult UpdateEventRewardWithFile(HttpPostedFileBase rwdFile)
        {
            THC_Library.Error error;

            var rwd_key = Request.Form["reward_key"];
            var rwd_name = Request.Form["txt_name"];
            var rwd_memo = Request.Form["txt_memo"];
            var rwd_vender = Request.Form["txt_vender"];
            var rwd_img = Request.Form["txt_img"];
            var rwd_win_desc = Request.Form["txt_win_desc"];
            var rwd_effect_date = Request.Form["txt_effective_time"];
            var rwd_sms = Request.Form["txt_sms"];
            var file_path = Server.MapPath("~/");

            Models.MyEvent myEvent = new Models.MyEvent();
            int iAffect = myEvent.updateRewardInfoWithFile(rwd_key, rwd_name, rwd_memo, rwd_vender,
                                rwd_img, rwd_win_desc, rwd_effect_date, rwd_sms, file_path, rwdFile, out error);

            string retJson;
            if (error != null)
            {
                retJson = Newtonsoft.Json.JsonConvert.SerializeObject(error);
            }
            else
            {
                retJson = "{ \"result\" : 1 }";
            }
            return Json(retJson, "application/json", JsonRequestBehavior.AllowGet);
        }

        public ActionResult RewardEarn(string event_no)
        {
            if (Session["acc"] == null)
            {
                return View("../Control/index");
            }
            Models.ControlUser ctlUser = (Models.ControlUser)Session["acc"];
            ViewBag.ACCOUNT = ctlUser.Account;
            ViewBag.ADMIN = ctlUser.IsAdmin;

            THC_Library.Error error;

            Models.MyEvent myEvent = new Models.MyEvent();
            DataTable eventsTable = myEvent.getMyEventList(out error);

            if (error != null)
            {
                ViewBag.Error = error.ErrorMessage;
                return View("Error");
            }
            else
            {
                var lanSet = new THC_Library.Language.LanguageBase();
                lanSet.CurrentZone = THC_Library.Language.LanguageBase.CURRENT_LANGUAGE;
                ViewData["lan"] = lanSet;   
                ViewBag.Data = eventsTable;
                return View("RewardEarnList");
            }           

            
        }

        [HttpPost]
        public JsonResult RewardEarnList(string event_no)
        {
            THC_Library.Error error;
            Models.MyEvent myEvent = new Models.MyEvent();
            DataTable earnTable = myEvent.getRewardEarnList(event_no, out error);

            string retJson;
            if (error != null)
            {
                retJson = Newtonsoft.Json.JsonConvert.SerializeObject(error);
            }
            else
            {
                retJson = Newtonsoft.Json.JsonConvert.SerializeObject(earnTable);
            }
            return Json(retJson, "application/json", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult RewardEarnExport(string event_no)
        {
            THC_Library.Error error;
            string retJson;

            if (Session["acc"] == null)
            {
                error = THC_Library.Error.AbortError();
                retJson = Newtonsoft.Json.JsonConvert.SerializeObject(error);
                return Json(retJson, "application/json", JsonRequestBehavior.AllowGet);
            }

            Models.MyEvent myEvent = new Models.MyEvent();
            DataTable earnTable = myEvent.exportRewardEarn(event_no, out error);

           
            if (error != null)
            {
                retJson = Newtonsoft.Json.JsonConvert.SerializeObject(error);
                return Json(retJson, "application/json", JsonRequestBehavior.AllowGet);
            }
            else
            {
                string fileName = string.Format("{0}_{1}.csv", event_no, DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss"));
                string csvFileName = string.Format("{0}\\{1}",
                                      Server.MapPath("~/exports"),
                                      fileName);

                if (System.IO.File.Exists(csvFileName))
                {
                    System.IO.File.Delete(csvFileName);
                }
                System.IO.StreamWriter csvWrite = new System.IO.StreamWriter(csvFileName, false, System.Text.Encoding.UTF8);
                string strLine;
                //: "QRC001", bVisible: false },
                //{ "title": "序號", "data": "QRC004", bVisible: true },
                //{ "title": "獎項碼", "data": "QRC008", bVisible: true },
                //{ "title": "獎項名稱", "data": "QRC011", bVisible: true },
                //{ "title": "掃描時間", "data": "QRC013", bVisible: true },
                //{ "title": "實際碼", "data": "QRC015", bVisible: true },
                //{ "title": "得獎帳號", "data": "QRC016", bVisible: true }
                csvWrite.WriteLine("序號,獎項碼,獎項名稱,掃描時間,實際碼,得獎帳號,身分證號,手機,性別,地址");
                string strAddr;
                foreach (DataRow row in earnTable.Rows)
                {
                    try
                    {
                        string accInfo = THC_Library.APPCURL.GetAccountInfoByAuthorizeKey(
                                                            row["QRC016"].ToString(), THC_Library.APPCURL.AUTH_KEY);
                        dynamic results = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(accInfo);

                        //results.Mobil = dataReader["CM008"].ToString();
                        //results.Address = dataReader["CM009"].ToString();
                        //results.IId = dataReader["CM010"].ToString();
                        //results.Gender = dataReader["CM012"].ToString();
                        //results.Age = dataReader["CM013"].ToString();
                        //if (dataReader["CM018"] == DBNull.Value)
                        //{
                        //    accInfo.Image = null;
                        //}
                        //else
                        //{
                        //    accInfo.Image = (byte[])dataReader["CM018"];
                        //}
                        //accInfo.Number = 0;
                        //accInfo.ErrorMessage = "";

                        strAddr = results.Address.ToString();
                        strAddr = strAddr.Replace("*", "");
                        //QRC001,QRC004,QRC008,QRC011,QRC013,QRC015,QRC016
                        strLine = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9}",
                                        row["QRC004"], row["QRC008"], row["QRC011"], row["QRC013"],
                                        row["QRC015"], row["QRC016"], results.IId, results.Mobil,
                                        results.Gender, strAddr);

                        csvWrite.WriteLine(strLine);

                    }
                    catch (Exception ex)
                    {
                        error = new THC_Library.Error();
                        error.ErrorMessage = ex.ToString();
                        error.Number = 1001;
                        retJson = Newtonsoft.Json.JsonConvert.SerializeObject(error);
                        return Json(retJson, "application/json", JsonRequestBehavior.AllowGet);
                    }
                   
                }
                csvWrite.Close();
                retJson = "{ \"id\": \"" + fileName + "\"}";
                        
            }
            return Json(retJson, "application/json", JsonRequestBehavior.AllowGet);      
            
        }

        public ActionResult Weather()
        {
            if (Session["acc"] == null)
            {
                return View("../Control/index");
            }
            Models.ControlUser ctlUser = (Models.ControlUser)Session["acc"];
            ViewBag.ACCOUNT = ctlUser.Account;
            ViewBag.ADMIN = ctlUser.IsAdmin;

            return View();
        }        

        public ActionResult DataAnalysis()
        {
            if (Session["acc"] == null)
            {
                return View("../Control/index");
            }
            Models.ControlUser ctlUser = (Models.ControlUser)Session["acc"];
            ViewBag.ACCOUNT = ctlUser.Account;
            ViewBag.ADMIN = ctlUser.IsAdmin;

            THC_Library.Error error;

            Models.MyEvent myEvent = new Models.MyEvent();
            DataTable eventsTable = myEvent.getMyEventList(out error);

            if (error != null)
            {
                ViewBag.Error = error.ErrorMessage;
                return View("Error");
            }
            else
            {
                ViewBag.Data = eventsTable;               
                return View();
            }
        }

        public ActionResult DataDashboard()
        {
            if (Session["acc"] == null)
            {
                return View("../Control/index");
            }
            Models.ControlUser ctlUser = (Models.ControlUser)Session["acc"];
            ViewBag.ACCOUNT = ctlUser.Account;
            ViewBag.ADMIN = ctlUser.IsAdmin;

            THC_Library.Error error;

            Models.MyEvent myEvent = new Models.MyEvent();
            DataTable eventsTable = myEvent.getMyEventList(out error);

            if (error != null)
            {
                ViewBag.Error = error.ErrorMessage;
                return View("Error");
            }
            else
            {
                ViewBag.Data = eventsTable;
                return View();
            }
        }

        [HttpPost]
        public JsonResult ScanRateData(string event_no)
        {
            THC_Library.Error error;

            float fScanCount, fTotalCount, fScanRate;
            Models.MyEvent myEvent = new Models.MyEvent();
            myEvent.getScanRate(event_no, out fScanCount, out fTotalCount, out fScanRate, out error);

            string retJson;
            if (error != null)
            {
                retJson = Newtonsoft.Json.JsonConvert.SerializeObject(error);
            }
            else
            {
                retJson = string.Format("{{ \"SCANRATE\": {0}, \"SCANCOUNT\": {1}, \"TOTAL\": {2} }}",
                                    fScanRate, fScanCount, fTotalCount);
            }
            return Json(retJson, "application/json", JsonRequestBehavior.AllowGet);
           
        } 

        [HttpPost]
        public JsonResult ScanArea(string event_no)
        {
            THC_Library.Error error;
           
            Models.MyEvent myEvent = new Models.MyEvent();
            DataTable areaTable = myEvent.getScanArea(event_no, out error);

            string retJson;
            if (error != null)
            {
                retJson = Newtonsoft.Json.JsonConvert.SerializeObject(error);
            }
            else
            {
                retJson = Newtonsoft.Json.JsonConvert.SerializeObject(areaTable);
            }
            return Json(retJson, "application/json", JsonRequestBehavior.AllowGet);
           
        }

        [HttpPost]
        public JsonResult ScanAge(string event_no)
        {
            THC_Library.Error error;

            Models.MyEvent myEvent = new Models.MyEvent();
            DataTable ageTable = myEvent.getScanAge(event_no, out error);

            string retJson;
            if (error != null)
            {
                retJson = Newtonsoft.Json.JsonConvert.SerializeObject(error);
            }
            else
            {
                retJson = Newtonsoft.Json.JsonConvert.SerializeObject(ageTable);
            }
            return Json(retJson, "application/json", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ScanGender(string event_no)
        {
            THC_Library.Error error;
           
            Models.MyEvent myEvent = new Models.MyEvent();
            DataTable areaTable = myEvent.getScanGender(event_no, out error);

            string retJson;
            if (error != null)
            {
                retJson = Newtonsoft.Json.JsonConvert.SerializeObject(error);
            }
            else
            {
                retJson = Newtonsoft.Json.JsonConvert.SerializeObject(areaTable);
            }
            return Json(retJson, "application/json", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ScanCount_InDay_7(string event_no)
        {
            THC_Library.Error error;

            Models.MyEvent myEvent = new Models.MyEvent();
            DataTable dateTable = myEvent.getScanCount_InDay_7(event_no, out error);

            string retJson;
            if (error != null)
            {
                retJson = Newtonsoft.Json.JsonConvert.SerializeObject(error);
            }
            else
            {
                retJson = Newtonsoft.Json.JsonConvert.SerializeObject(dateTable);
            }
            return Json(retJson, "application/json", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ScanTimeTemptrue(string event_no, string days)
        {

            THC_Library.Error error;

            Models.MyEvent myEvent = new Models.MyEvent();
            DataTable dateTable = myEvent.getTimeTemptrue(event_no, days, out error);

            string retJson;
            if (error != null)
            {
                retJson = Newtonsoft.Json.JsonConvert.SerializeObject(error);
            }
            else
            {
                retJson = Newtonsoft.Json.JsonConvert.SerializeObject(dateTable);
            }
            return Json(retJson, "application/json", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ScanTimeCounterByArea(string event_no, string days)
        {

            THC_Library.Error error;

            Models.MyEvent myEvent = new Models.MyEvent();
            DataTable areaTable = myEvent.getTimeCountByArea(event_no, days, out error);

            string retJson;
            if (error != null)
            {
                retJson = Newtonsoft.Json.JsonConvert.SerializeObject(error);
            }
            else
            {
                //DataRow[] northArea = areaTable.Select("WH004=1");
                //DataRow[] centralArea = areaTable.Select("WH004=2");
                //DataRow[] southArea = areaTable.Select("WH004=3");
                //DataRow[] eastArea = areaTable.Select("WH004=4");

                retJson = Newtonsoft.Json.JsonConvert.SerializeObject(areaTable);
            }
            return Json(retJson, "application/json", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ScanRate(string event_no, string counter)
        {

            THC_Library.Error error;
            string strTotal;

            Models.MyEvent myEvent = new Models.MyEvent();
            DataTable rateTable = myEvent.getScanRate(event_no, counter, out strTotal, out error);

            string retJson;
            if (error != null)
            {
                //retJson = Newtonsoft.Json.JsonConvert.SerializeObject(error);
                return Json(error, "application/json", JsonRequestBehavior.AllowGet);
            }
            else
            {
                retJson = Newtonsoft.Json.JsonConvert.SerializeObject(rateTable);
                var jsonResult = new { data = retJson, total = strTotal };
                return Json( jsonResult , JsonRequestBehavior.AllowGet);
            }
            
        }

        [HttpPost]
        public JsonResult Weather(string days)
        {

            THC_Library.Error error;

            Models.MyEvent myEvent = new Models.MyEvent();
            DataTable weatherTable = myEvent.getWeather(out error);

            string retJson;
            if (error != null)
            {
                retJson = Newtonsoft.Json.JsonConvert.SerializeObject(error);
            }
            else
            {
                retJson = Newtonsoft.Json.JsonConvert.SerializeObject(weatherTable);
            }
            return Json(retJson, "application/json", JsonRequestBehavior.AllowGet);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;

namespace WebHTCBackEnd.Controllers
{
    public class RewardManageController : Controller
    {
        // GET: RewarManage
        public ActionResult Index()
        {
            return View();
        }
                
        public ActionResult RewardData()
        {
            Error.Error error = null;
            string[] top10Events;

            WebHTCBackEnd.Models.Events.THC_EventReward objReward = new Models.Events.THC_EventReward();
            objReward.queryTop10Events(out top10Events, out error);

            if (error != null)
            {
                //ERROR Page
                return View();
            }
            else
            {
                DataTable rewardData = null;
                var lanSet = new Language.Event_Reward();
                lanSet.CurrentZone = Language.LanguageBase.CURRENT_LANGUAGE;
                ViewData["lan"] = lanSet;                
                ViewBag.TOP10_EVENTS = top10Events;
                ViewBag.reward_types = classes.RewardType.GetRewardType(Language.LanguageBase.CURRENT_LANGUAGE);
                ViewBag.reward_venders = classes.RewardVender.GetRewardVender(Language.LanguageBase.CURRENT_LANGUAGE);
                return View(rewardData);
            }
        }
        
        public ActionResult RewardSearch(string event_no, string event_name)
        {
            Error.Error error = null;
            string strEventKey, strEventNo, strEventName, strVenderNo, strVenderName;
            string[] top10Events;

            WebHTCBackEnd.Models.Events.THC_EventReward objReward = new Models.Events.THC_EventReward();
            DataTable rewardData = objReward.queryRewardSearch(event_no, event_name, out strEventKey, out strEventNo,
                                                out strEventName, out strVenderNo, out strVenderName, out top10Events,
                                                out error);

            if (error != null)
            {                
                return View();
            }
            else
            {
                var lanSet = new Language.Event_Reward();
                lanSet.CurrentZone = Language.LanguageBase.CURRENT_LANGUAGE;
                ViewData["lan"] = lanSet;
                ViewBag.product_types = classes.ProductType.GetProductType(Language.LanguageBase.CURRENT_LANGUAGE);
                ViewBag.s_event_no = event_no;
                ViewBag.s_event_name = event_name;
                ViewBag.EVENT_KEY = strEventKey;
                ViewBag.EVENT_NO = strEventNo;
                ViewBag.EVENT_NAME = strEventName;
                ViewBag.VENDER_NO = strVenderNo;
                ViewBag.VENDER_NAME = strVenderName;
                ViewBag.TOP10_EVENTS = top10Events;
                ViewBag.reward_types = classes.RewardType.GetRewardType(Language.LanguageBase.CURRENT_LANGUAGE);
                ViewBag.reward_venders = classes.RewardVender.GetRewardVender(Language.LanguageBase.CURRENT_LANGUAGE);
                return View("RewardData", rewardData);
            }

        }

        [HttpPost]
        public JsonResult SingleRewardData(string key)
        {
            Error.Error error = null;
            WebHTCBackEnd.Models.Events.THC_EventReward objReward = new Models.Events.THC_EventReward();
            DataTable rewardData = objReward.queryRewardByKey(key, out error);

            string retJson;
            if (error != null)
            {
                retJson = Newtonsoft.Json.JsonConvert.SerializeObject(error);
            }
            else
            {
                retJson = Newtonsoft.Json.JsonConvert.SerializeObject(rewardData);
            }
            return Json(retJson, "application/json", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult NewEventReward()
        {            
            var EventKey = Request.Form["event_key"];
            var RwdType = Request.Form["txt_ptype"];
            var RwdLevel = Request.Form["txt_level"];
            var RwdName = Request.Form["txt_name"];
            var RwdNumber = Request.Form["txt_number"];
            var RwdSMS = Request.Form["txt_sms"];
            var RwdMemo = Request.Form["txt_memo"];
            var RwdVender = Request.Form["txt_rwdvender"];
            var Assign = Request.Form["txt_assign"];
            var RwdImage = Request.Form["txt_image"];
            var RwdEffective = Request.Form["txt_effective_time"];
            
            Error.Error error = null;
            WebHTCBackEnd.Models.Events.THC_EventReward objReward = new Models.Events.THC_EventReward();
            int iId = objReward.addNewReward(EventKey, RwdType, RwdLevel, RwdName, RwdNumber, RwdMemo,
                                RwdVender, Assign, RwdImage, RwdEffective, RwdSMS, out error);

            string retJson;
            if (error != null)
            {
                retJson = Newtonsoft.Json.JsonConvert.SerializeObject(error);
            }
            else
            {
                retJson = string.Format("[{{ \"NewId\" : {0} }}]", iId);
            }

            return Json(retJson, "application/json", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult UpdateEventReward()
        {
            var RwdKey = Request.Form["rwd_key"];
            var RwdType = Request.Form["txt_ptype"];
            var RwdLevel = Request.Form["txt_level"];
            var RwdName = Request.Form["txt_name"];
            var RwdNumber = Request.Form["txt_number"];
            var RwdSMS = Request.Form["txt_sms"];
            var RwdMemo = Request.Form["txt_memo"];
            var RwdVender = Request.Form["txt_rwdvender"];
            var Assign = Request.Form["txt_assign"];
            var RwdImage = Request.Form["txt_image"];
            var RwdEffective = Request.Form["txt_effective_time"];

            Error.Error error = null;
            WebHTCBackEnd.Models.Events.THC_EventReward objReward = new Models.Events.THC_EventReward();
            int iAffectedCount = objReward.updateReward(RwdKey, RwdType, RwdLevel, RwdName, RwdNumber, RwdMemo,
                                RwdVender, Assign, RwdImage, RwdEffective, RwdSMS, out error);

            string retJson;
            if (error != null)
            {
                retJson = Newtonsoft.Json.JsonConvert.SerializeObject(error);
            }
            else
            {
                retJson = WebHTCBackEnd.Utility.JsonResponeUtility.ResultOK();
            }

            return Json(retJson, "application/json");
        }

        [HttpPost]
        public JsonResult DeleteEventReward(string key)
        {
            Error.Error error = null;
            WebHTCBackEnd.Models.Events.THC_EventReward objReward = new Models.Events.THC_EventReward();
            objReward.deleteReward(key, out error);

            string retJson;
            if (error != null)
            {
                retJson = Newtonsoft.Json.JsonConvert.SerializeObject(error);
            }
            else
            {
                retJson = WebHTCBackEnd.Utility.JsonResponeUtility.ResultOK();
            }

            return Json(retJson, "application/json", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult RewardUpload(HttpPostedFileBase rwdFile)
        {
            Error.Error error;
            string jobId = Guid.NewGuid().ToString().Replace("-", "_");
            string rwdVender = Request.Form["vender"];

            WebHTCBackEnd.Models.Events.THC_EventReward objReward = new Models.Events.THC_EventReward();
            objReward.chechVender(rwdVender, out error);

            string retJson = string.Format("{{ \"result\" : \"ok\" , \"job\" : \"{0}\"}}",
                              jobId);

            if (error != null)
            {
                retJson = Newtonsoft.Json.JsonConvert.SerializeObject(error);
            }
            else
            {
                if (rwdFile != null)
                {
                    if (rwdFile.ContentLength > 0)
                    {
                        var fileName = System.IO.Path.GetFileName(rwdFile.FileName);
                        var fileExtension = System.IO.Path.GetExtension(fileName);
                        var path = System.IO.Path.Combine(Server.MapPath("~/codeupload"), jobId + fileExtension);
                        try
                        {
                            rwdFile.SaveAs(path);
                            //var saveFile = new System.IO.StreamReader(path);
                            //int iLineCount = 0;
                            //string strLine;
                            //while ( (strLine = saveFile.ReadLine()) != null)
                            //{
                            //    iLineCount++;
                            //}
                            //saveFile.Close();

                            retJson = string.Format("{{ \"result\" : \"ok\" , \"job\" : \"{0}\"}}",
                                   jobId);
                        }
                        catch (Exception ex)
                        {
                            retJson = string.Format("{{ \"result\" : \"error\" , \"state\" : \"{0}\"}}",
                                  ex.Message);
                        }
                    }
                    else
                    {
                        retJson = "{{ \"result\" : \"error\" , \"state\" : \"file length 0.\"}}";
                    }
                }
                else
                {
                    retJson = "{{ \"result\" : \"error\" , \"state\" : \"no file upload.\"}}";
                }  
            }           
                     
            return Json(retJson, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AllotReward(string key, string event_key, string jobid)
        {  
            string jobFile = System.IO.Path.Combine(Server.MapPath("~/codeupload"), jobid + ".txt");            
            Error.Error error;
            string retJson;

            if (!System.IO.File.Exists(jobFile))
            {
                error = new Error.Error();
                error.ErrorMessage = "上傳工作檔案已不存在";
                error.Number = 102;
                retJson = Newtonsoft.Json.JsonConvert.SerializeObject(error);
            }
            else
            {
                WebHTCBackEnd.Models.Events.THC_EventReward eventReward = new Models.Events.THC_EventReward();
                List<string> rwdList = eventReward.getRewardArrayByjobId(jobFile, out error);

               
                if (error != null)
                {
                    retJson = Newtonsoft.Json.JsonConvert.SerializeObject(error);
                }
                else
                {
                    THC_CodeTypeLib.RewardProxy rewardProxy = (THC_CodeTypeLib.RewardProxy)
                   Activator.GetObject(typeof(THC_CodeTypeLib.RewardProxy),
                   "tcp://127.0.0.1:8000/RemoteReward");
                    rewardProxy.RewardAllotForEdenred(key, event_key, rwdList, jobid);
                    retJson = string.Format("[{{ \"STATE\" : \"{0}\" }}]",
                                            "RUN", "OK");

                }
            }
            
            return Json(retJson, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetRewardState(string job_id)
        {
            THC_CodeTypeLib.RewardState rwdState;
            try
            {
                THC_CodeTypeLib.RewardProxy rewardProxy = (THC_CodeTypeLib.RewardProxy)
                       Activator.GetObject(typeof(THC_CodeTypeLib.CodeGenProxy),
                       "tcp://127.0.0.1:8000/RemoteReward");
                rwdState = rewardProxy.GetRewardState(job_id);
            }
            catch (Exception ex)
            {
                rwdState = new THC_CodeTypeLib.RewardState();
                rwdState.HasError = true;
                rwdState.State = ex.ToString();
            }
            string retJson = Newtonsoft.Json.JsonConvert.SerializeObject(rwdState);
            return Json(retJson, "application/json", JsonRequestBehavior.AllowGet);
        }
    }
}
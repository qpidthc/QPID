using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Text.RegularExpressions;

namespace WebHTCBackEnd.Controllers
{
    public class EventsController : Controller
    {
        // GET: Events
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult SingleEventData(string event_no)
        {
            Error.Error error = null;
            WebHTCBackEnd.Models.Events.THC_Event objEvent = new Models.Events.THC_Event();
            DataTable eventData = objEvent.queryEventByEventNo(event_no, out error);

            string retJson;
            if (error != null)
            {
                retJson = Newtonsoft.Json.JsonConvert.SerializeObject(error);
            }
            else
            {
                retJson = Newtonsoft.Json.JsonConvert.SerializeObject(eventData);
            }
            return Json(retJson, "application/json", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult NewEventData()
        {
            var EventNo = Request.Form["txt_event_no"];
            var EventName = Request.Form["txt_event_name"];
            var Vender = Request.Form["txt_vender"];
            var Lunch = Request.Form["txt_lunch"];                        
            var StartTime = Request.Form["txt_start_time"];
            var EndTime = Request.Form["txt_end_time"];
            var CodeId =  Request.Form["txt_code_id"];
            var CodeNums = Request.Form["txt_code_nums"];
            var QRBit = Request.Form["txt_qr_bit"];
            var SerialBit = Request.Form["txt_serial_bit"];
            var ComBit = "0";//Request.Form["txt_com_bit"];
            var Production = Request.Form["txt_product"];
            var WebUrl = Request.Form["txt_web_url"];
            var EventType = Request.Form["txt_event_type"];
            var Memo = Request.Form["txt_memo"];
            var Length = Request.Form["txt_qr_length"];
            var Page = Request.Form["txt_page"];
            //var Used = Request.Form["txt_used"];
           

            Error.Error error = null;
            WebHTCBackEnd.Models.Events.THC_Event events = new Models.Events.THC_Event();
            int iId = events.addNewEvent(EventNo, EventName, Vender, Lunch, StartTime, EndTime, CodeId, CodeNums, QRBit,
                                        SerialBit, ComBit, Production, WebUrl, EventType, Memo, Length, Page, out error);

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

        public ActionResult EventData()
        {           
            WebHTCBackEnd.Models.Events.THC_Event objEvent = new Models.Events.THC_Event();
            DataSet eventSet = objEvent.queryEventListAndVender();
                      
            var lanSet = new Language.Activity_Event_Lan();
            lanSet.CurrentZone = Language.LanguageBase.CURRENT_LANGUAGE;
            ViewData["lan"] = lanSet;
            return View(eventSet);
        }

        [HttpPost]
        public JsonResult UpdateEventData(string event_no)
        {
            var EventNo = Request.Form["txt_event_no"];
            var EventName = Request.Form["txt_event_name"];
            var Vender = Request.Form["txt_vender"];
            var Lunch = Request.Form["txt_lunch"];
            var StartTime = Request.Form["txt_start_time"];
            var EndTime = Request.Form["txt_end_time"];
            var CodeId = Request.Form["txt_code_id"];
            var CodeNums = Request.Form["txt_code_nums"];
            var QRBit = Request.Form["txt_qr_bit"];
            var SerialBit = Request.Form["txt_serial_bit"];
            var ComBit = "0";//Request.Form["txt_com_bit"];
            var Production = Request.Form["txt_product"];
            var WebUrl = Request.Form["txt_web_url"];
            var EventType = Request.Form["txt_event_type"];
            var Memo = Request.Form["txt_memo"];
            var Length = Request.Form["txt_qr_length"];
            var Page = Request.Form["txt_page"];

            Error.Error error = null;
            WebHTCBackEnd.Models.Events.THC_Event events = new Models.Events.THC_Event();
            events.updateEvent(EventNo, EventName, Vender, Lunch, StartTime, EndTime, CodeId, CodeNums, QRBit,
                                SerialBit, ComBit, Production, WebUrl, EventType, Memo, Length, Page, out error);

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
        public JsonResult DeleteEventData(string event_no)
        {
            Error.Error error = null;
            WebHTCBackEnd.Models.Events.THC_Event objEvent = new Models.Events.THC_Event();
            objEvent.deleteEvent(event_no, out error);

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


        public ActionResult EventDataSearch(string event_no, string event_name, string vender)
        {            
            WebHTCBackEnd.Models.Events.THC_Event objEvent = new Models.Events.THC_Event();
            DataSet eventData = objEvent.queryEventSearch(event_no, event_name, vender);

            var lanSet = new Language.Activity_Event_Lan();
            lanSet.CurrentZone = Language.LanguageBase.CURRENT_LANGUAGE;
            ViewData["lan"] = lanSet;

            ViewBag.s_event_no = event_no;
            ViewBag.s_event_name = event_name;
            ViewBag.s_vender = vender;

            return View("EventData", eventData);
        }

    }
}
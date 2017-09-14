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
            return View();
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
                //ViewBag.Error = error.ErrorMessage;
                //return View("~/Backend/Error");
            }
            else
            {
                retJson = Newtonsoft.Json.JsonConvert.SerializeObject(eventTable);
                //ViewBag.Data = eventTable;
                //return View();
            }
            return Json(retJson, "application/json", JsonRequestBehavior.AllowGet);
        }

        public ActionResult EventRewards(string event_no)
        {
            return View();
        }

        public ActionResult RewardEarn()
        {
            return View();
        }

        public ActionResult Weather()
        {
            return View();
        }        

        public ActionResult DataAnalysis()
        {
            return View();
        }

        [HttpPost]
        public JsonResult ScanArea(string event_no)
        {

            return Json("", "application/json", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ScanAge(string event_no)
        {
            return Json("", "application/json", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ScanGender(string event_no)
        {
            return Json("", "application/json", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ScanTimeTemptrue(string event_no)
        {

            return Json("", "application/json", JsonRequestBehavior.AllowGet);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;

namespace WebTHCAPP.Controllers
{
    public class AppController : Controller
    {
        // GET: App
        public ActionResult Index()
        {
            return View("HistoryList");
        }

        public ActionResult HistoryList()
        {
            //string acc = "test@gmail.com";
            THC_Library.Error error;

            //Models.Member member = new Models.Member();
            //DataTable resultTable = member.getRewardGain(acc, out error);

            //if (error != null)
            //{
            //    return View(resultTable);
            //}
            //else
            //{
            //    return View(resultTable);
            //}


            if (Session["tk"] != null)
            {
                Models.AppSession appSession = (Models.AppSession)Session["tk"];
                ViewBag.ACC = appSession.Account;

                Models.Member member = new Models.Member();
                DataTable resultTable = member.getRewardGain(appSession.Account, out error);

                if (error == null)
                {
                    return View(resultTable);
                }
                else
                {
                    @ViewBag.NUMBER = error.Number;
                    @ViewBag.ERROR = error.ErrorMessage;
                    return View("../Error/SystemError");
                
                }
            }
            else
            {
                return View("../Error/NotAllow");
            }            
        }

        public ActionResult Activities()
        {           
            THC_Library.Error error;

            //Models.Activity activity = new Models.Activity();
            //DataTable resultTable = activity.getActivities(out error);

            //if (error != null)
            //{
            //    return View(resultTable);
            //}
            //else
            //{
            //    return View(resultTable);
            //}

            if (Session["tk"] != null)
            {
                Models.Activity activity = new Models.Activity();
                DataTable resultTable = activity.getActivities(out error);

                if (error == null)
                {
                    return View(resultTable);
                }
                else
                {
                    @ViewBag.NUMBER = error.Number;
                    @ViewBag.ERROR = error.ErrorMessage;
                    return View("../Error/SystemError");
                }
            }
            else
            {
                return View("../Error/NotAllow");
            }            
        }

        public ActionResult ActivityDesc(string page)
        {
            //ViewBag.PAGE = page;
            //return View();

            if (Session["tk"] != null)
            {
                ViewBag.PAGE = page;
                return View();
            }
            else
            {
                return View("../Error/NotAllow");
            }            
        }

        public ActionResult UsrInfo()
        {
            THC_Library.Error error;
            //Models.Member m = new Models.Member();
            //Models.AccountInfo acc = m.getAccountInfo("Test@gmail.com", "636401686312802583", out error);

            //if (error == null)
            //{
            //    ViewBag.TICKET = "636401686312802583";
            //    ViewBag.ACC = "Test@gmail.com";
            //    ViewBag.MOBIL = acc.Mobil;
            //    ViewBag.GENDER = acc.Gender;
            //    ViewBag.AGE = acc.Age;
            //    ViewBag.IID = acc.IId;
            //    ViewBag.ADDRESS = acc.Address;
            //    return View();
            //}
            //else
            //{
            //    return View("Index");
            //}

            if (Session["tk"] != null)
            {
                Models.AppSession appSession = (Models.AppSession)Session["tk"];
                ViewBag.ACC = appSession.Account;

                Models.Member member = new Models.Member();
                Models.AccountInfo acc = member.getAccountInfo(appSession.Account, appSession.Ticket.ToString(), out error);

                if (error == null)
                {
                    ViewBag.TICKET = appSession.Ticket.ToString();
                    ViewBag.ACC = appSession.Account;
                    ViewBag.MOBIL = acc.Mobil;
                    ViewBag.GENDER = acc.Gender;
                    ViewBag.AGE = acc.Age;
                    ViewBag.IID = acc.IId;
                    ViewBag.ADDRESS = acc.Address;
                    return View();
                }
                else
                {
                    @ViewBag.NUMBER = error.Number;
                    @ViewBag.ERROR = error.ErrorMessage;
                    return View("../Error/SystemError");
                }
            }
            else
            {
                return View("../Error/NotAllow");
            }                       
        }       
       
        public ActionResult Question()
        {
            return View();
        }

        public ActionResult About()
        {
            return View("");
        }

        [HttpPost]
        public JsonResult getCities()
        {
            THC_Library.Error error;

            Models.TaiwanAddress twAddr = new Models.TaiwanAddress();
            DataTable addrTable = twAddr.getCities(out error);

            string retJson;
            if (error != null)
            {
                retJson = Newtonsoft.Json.JsonConvert.SerializeObject(error);
            }
            else
            {
                retJson = Newtonsoft.Json.JsonConvert.SerializeObject(addrTable);
            }
            return Json(retJson, "application/json", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult getTowns(string city)
        {
            THC_Library.Error error;

            Models.TaiwanAddress twAddr = new Models.TaiwanAddress();
            DataTable addrTable = twAddr.getTown(city, out error);

            string retJson;
            if (error != null)
            {
                retJson = Newtonsoft.Json.JsonConvert.SerializeObject(error);
            }
            else
            {
                retJson = Newtonsoft.Json.JsonConvert.SerializeObject(addrTable);
            }
            return Json(retJson, "application/json", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult getRoad(string city, string town)
        {
            THC_Library.Error error;

            Models.TaiwanAddress twAddr = new Models.TaiwanAddress();
            DataTable addrTable = twAddr.getRoad(city, town, out error);

            string retJson;
            if (error != null)
            {
                retJson = Newtonsoft.Json.JsonConvert.SerializeObject(error);
            }
            else
            {
                retJson = Newtonsoft.Json.JsonConvert.SerializeObject(addrTable);
            }
            return Json(retJson, "application/json", JsonRequestBehavior.AllowGet);
        }
    }
}
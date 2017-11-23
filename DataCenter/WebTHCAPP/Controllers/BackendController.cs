using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebTHCAPP.Controllers
{
    public class BackendController : Controller
    {
        // GET: Backend
        public ActionResult Index()
        {
            if (Session["acc"] == null)
            {
                return View();
            }
            ViewBag.ACCOUNT = Session["acc"].ToString();            
            return View();
        }

        public ActionResult EnterControl()
        {
            THC_Library.Error error;
            string strAccount = Request.Form["user"];
            string strPwd = Request.Form["pass"];

            if (string.IsNullOrEmpty(strAccount))
            {
                ViewBag.ERROR_MESSAGE = "請輸入帳號";
                return View("Index");
            }
            if (string.IsNullOrEmpty(strPwd))
            {
                ViewBag.ERROR_MESSAGE = "請輸入密碼";
                return View("Index");
            }

            Models.SystemControl systemCtl = new Models.SystemControl();
            System.Data.DataTable userTable = systemCtl.enterVerify(strAccount, strPwd, out error);

            if (error != null)
            {
                ViewBag.ORG_ACCOUNT = strAccount;
                ViewBag.ERROR_MESSAGE = error.ErrorMessage;
                return View("Index");
            }
            else
            {
                Session["acc"] = strAccount;
                ViewBag.ACCOUNT = Session["acc"].ToString();

                var lanSet = new THC_Library.Language.LanguageBase();
                lanSet.CurrentZone = THC_Library.Language.LanguageBase.CURRENT_LANGUAGE;
                ViewData["lan"] = lanSet;

                if (userTable == null)
                {
                    //Models.MyEvent myEvent = new Models.MyEvent();
                    //System.Data.DataTable eventsTable = myEvent.getMyEventList(out error);

                    if (error != null)
                    {
                        ViewBag.Error = error.ErrorMessage;
                        return View("../Backend/Error");
                    }
                    else
                    {
                        //ViewBag.Data = eventsTable;
                        return View("../Backend/EventActivities");
                    }
                }
                else
                {
                    ViewBag.ADMIN = true;
                    return View("Preview", userTable);
                }

            }
        }

        [HttpPost]
        public JsonResult THC_AnsyActivity(string authkey)
        {
            THC_Library.Error error = null;
            string strTick = Request.Headers["QPID-TICK"];
            string strData = Request.Headers["QPID-DATA"];
            error = WebTHCAPP.Models.RequestChecker.CheckRequest(strTick, strData);
            Models.ResultBase result = new Models.ResultBase();
            result.Number = 0;
            result.ErrorMessage = "";

            if (error == null)
            {
                var reqActivity = Request.Form["activity"];

                Models.Activity activity = new Models.Activity();
                activity.asyncActivity(reqActivity, out error);
            }

            if (error != null)
            {
                result.Number = error.Number;
                result.ErrorMessage = error.ErrorMessage;
            }

            return Json(result, "application/json", JsonRequestBehavior.AllowGet);

        }


        [HttpPost]
        public JsonResult THC_ClearLogActivity(string authkey)
        {
            THC_Library.Error error = null;
            string strTick = Request.Headers["QPID-TICK"];
            string strData = Request.Headers["QPID-DATA"];
            error = WebTHCAPP.Models.RequestChecker.CheckRequest(strTick, strData);
            Models.ResultBase result = new Models.ResultBase();
            result.Number = 0;
            result.ErrorMessage = "";

            if (error == null)
            {
                var reqActivity = Request.Form["activity"];

                Models.Activity activity = new Models.Activity();
                activity.clearLogActivity(reqActivity, out error);
            }

            if (error != null)
            {
                result.Number = error.Number;
                result.ErrorMessage = error.ErrorMessage;
            }

            return Json(result, "application/json", JsonRequestBehavior.AllowGet);

        }
    }
}
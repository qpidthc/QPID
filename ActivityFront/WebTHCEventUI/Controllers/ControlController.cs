using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebTHCEventUI.Controllers
{
    public class ControlController : Controller
    {
        // GET: Control
        public ActionResult Index()
        {
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
                var lanSet = new THC_Library.Language.LanguageBase();
                lanSet.CurrentZone = THC_Library.Language.LanguageBase.CURRENT_LANGUAGE;
                ViewData["lan"] = lanSet;
                
                if (userTable == null)
                {
                    Models.MyEvent myEvent = new Models.MyEvent();
                    System.Data.DataTable eventsTable = myEvent.getMyEventList(out error);

                    if (error != null)
                    {
                        ViewBag.Error = error.ErrorMessage;
                        return View("../Backend/Error");
                    }
                    else
                    {
                        Models.ControlUser ctlUser = new Models.ControlUser();
                        ctlUser.Account = strAccount;
                        ctlUser.IsAdmin = false;
                        Session["acc"] = ctlUser;
                        ViewBag.ACCOUNT = Session["acc"].ToString();
                        ViewBag.Data = eventsTable;
                        return View("../Backend/EventActivities");
                    }
                }
                else
                {
                    Models.ControlUser ctlUser = new Models.ControlUser();
                    ctlUser.Account = strAccount;
                    ctlUser.IsAdmin = true;
                    Session["acc"] = ctlUser;
                    ViewBag.ACCOUNT = strAccount;
                    ViewBag.ADMIN = ctlUser.IsAdmin;
                    return View("UserControl", userTable);
                }
                
            }           
        }

        public ActionResult UserControl()
        {
            if (Session["acc"] == null)
            {
                return View("index");
            }

            var lanSet = new THC_Library.Language.LanguageBase();
            lanSet.CurrentZone = THC_Library.Language.LanguageBase.CURRENT_LANGUAGE;
            ViewData["lan"] = lanSet;
            Models.ControlUser ctlUser = (Models.ControlUser)Session["acc"];
            ViewBag.ACCOUNT = ctlUser.Account;
            ViewBag.ADMIN = ctlUser.IsAdmin;
            return View();
        }

        public ActionResult ImportActivity()
        {
            if (Session["acc"] == null)
            {
                return View("index");
            }

            var lanSet = new THC_Library.Language.LanguageBase();
            lanSet.CurrentZone = THC_Library.Language.LanguageBase.CURRENT_LANGUAGE;
            ViewData["lan"] = lanSet;

            Models.ControlUser ctlUser = (Models.ControlUser)Session["acc"];
            ViewBag.ACCOUNT = ctlUser.Account;
            ViewBag.ADMIN = ctlUser.IsAdmin;

            return View();
        }

        [HttpPost]
        public JsonResult ImportActivityRun(HttpPostedFileBase rwdFile)
        {
            THC_Library.Error error;

            Models.MyEvent myEvent = new Models.MyEvent();
            myEvent.runTimeSetting(rwdFile, out error);

            string retJson;
            if (error != null)
            {
                retJson = Newtonsoft.Json.JsonConvert.SerializeObject(error);
            }
            else
            {
                retJson = string.Format("{{ \"RESULT\" : {0} }}", 1);
            }
            return Json(retJson, "application/json", JsonRequestBehavior.AllowGet);
        }

        public ActionResult ModifyAccessCode()
        {
            if (Session["acc"] == null)
            {
                return View("index");
            }
            Models.ControlUser ctlUser = (Models.ControlUser)Session["acc"];
            ViewBag.ACCOUNT = ctlUser.Account;
            ViewBag.ADMIN = ctlUser.IsAdmin;
            return View("ChangeCode");
        }
                
        [HttpPost]
        public JsonResult UserInfo()
        {            
            THC_Library.Error error;
            var user = Request.Form["account"];
           
            Models.SystemControl systemCtl = new Models.SystemControl();
            System.Data.DataTable userTable = systemCtl.getUserInfo(user, out error);

            string retJson;
            if (error != null)
            {
                retJson = Newtonsoft.Json.JsonConvert.SerializeObject(error);
            }
            else
            {
                retJson = Newtonsoft.Json.JsonConvert.SerializeObject(userTable);
            }
            return Json(retJson, "application/json", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult NewUser()
        {
            THC_Library.Error error;
            var user = Request.Form["txt_acc"];
            var name = Request.Form["txt_name"];
            var access_code = Request.Form["txt_pass"];

            Models.SystemControl systemCtl = new Models.SystemControl();
            int iExcuteCount = systemCtl.addNewAccount(user, name, access_code, out error);

            string retJson;
            if (error != null)
            {
                retJson = Newtonsoft.Json.JsonConvert.SerializeObject(error);
            }
            else
            {
                retJson = string.Format("{{ \"RESULT\" : {0} }}", iExcuteCount);
            }
            return Json(retJson, "application/json", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult UpdateUser()
        {
            THC_Library.Error error;
            var user = Request.Form["txt_acc"];
            var name = Request.Form["txt_name"];
            var access_code = Request.Form["txt_pass"];

            Models.SystemControl systemCtl = new Models.SystemControl();
            int iExcuteCount = systemCtl.updateUser(user, name, access_code, out error);

            string retJson;
            if (error != null)
            {
                retJson = Newtonsoft.Json.JsonConvert.SerializeObject(error);
            }
            else
            {
                retJson = string.Format("{{ \"RESULT\" : {0} }}", iExcuteCount);
            }
            return Json(retJson, "application/json", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DeleteUser()
        {
            THC_Library.Error error;
            var user = Request.Form["account"];

            Models.SystemControl systemCtl = new Models.SystemControl();
            int iExcuteCount = systemCtl.deleteUser(user, out error);

            string retJson;
            if (error != null)
            {
                retJson = Newtonsoft.Json.JsonConvert.SerializeObject(error);
            }
            else
            {
                retJson = string.Format("{{ \"RESULT\" : {0} }}", iExcuteCount);
            }
            return Json(retJson, "application/json", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Logout()
        {
            THC_Library.Error error;
            string strAccount = Request.Form["user"];

            Models.SystemControl systemCtl = new Models.SystemControl();
            systemCtl.clearLoginTime(strAccount, out error);

            string retJson;
            if (error != null)
            {
                retJson = Newtonsoft.Json.JsonConvert.SerializeObject(error);
            }
            else
            {
                Session["acc"] = null;
                retJson = "{ \"RESULT\": 1}";
            }
            return Json(retJson, "application/json", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult ChangeCode()
        {
            if (Session["acc"] == null)
            {
                return View("index");
            }

            THC_Library.Error error;
            var acc = Session["acc"].ToString();
            var old = Request.Form["txt_old"];
            var new1 = Request.Form["txt_new1"];
            var new2 = Request.Form["txt_new2"];

            Models.SystemControl systemCtl = new Models.SystemControl();
            systemCtl.changePassword(acc, old, new1, new2, out error);

            string retJson;
            if (error != null)
            {
                retJson = Newtonsoft.Json.JsonConvert.SerializeObject(error);
            }
            else
            {
                retJson = "{ \"RESULT\" : 1 }";
            }
            return Json(retJson, "application/json", JsonRequestBehavior.AllowGet);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebHTCBackEnd.Controllers
{
    public class RegisterController : Controller
    {
        // GET: Register
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

            Models.Register.SystemControl systemCtl = new Models.Register.SystemControl();
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

                var lanSet = new Language.User_Lan();
                lanSet.CurrentZone = THC_Library.Language.LanguageBase.CURRENT_LANGUAGE;
                ViewData["lan"] = lanSet;

                if (userTable == null)
                {                  
                    ViewBag.ADMIN = false;
                    return View("Users", userTable);
                }
                else
                {
                    ViewBag.ADMIN = true;
                    return View("Users", userTable);
                }

            }
        }

        public ActionResult UserControl()
        {
            if (Session["acc"] == null)
            {
                return View("index");
            }

            var lanSet = new Language.User_Lan();
            lanSet.CurrentZone = THC_Library.Language.LanguageBase.CURRENT_LANGUAGE;
            ViewData["lan"] = lanSet;
            ViewBag.ACCOUNT = Session["acc"].ToString();
            return View("Users");
        }

        public ActionResult ModifyAccessCode()
        {
            if (Session["acc"] == null)
            {
                return View("index");
            }
            var lanSet = new Language.ChangeCode_Lan();
            lanSet.CurrentZone = THC_Library.Language.LanguageBase.CURRENT_LANGUAGE;
            ViewData["lan"] = lanSet;
            ViewBag.ACCOUNT = Session["acc"].ToString();
            return View("ChangeCode");
        }

        [HttpPost]
        public JsonResult UserInfo()
        {
            THC_Library.Error error;
            var user = Request.Form["account"];

            Models.Register.SystemControl systemCtl = new Models.Register.SystemControl();
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

            Models.Register.SystemControl systemCtl = new Models.Register.SystemControl();
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

            Models.Register.SystemControl systemCtl = new Models.Register.SystemControl();
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

            Models.Register.SystemControl systemCtl = new Models.Register.SystemControl();
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

            Models.Register.SystemControl systemCtl = new Models.Register.SystemControl();
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

            Models.Register.SystemControl systemCtl = new Models.Register.SystemControl();
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
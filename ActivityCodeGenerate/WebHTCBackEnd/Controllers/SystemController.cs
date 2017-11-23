using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebHTCBackEnd.Controllers
{
    public class SystemController : Controller
    {
        // GET: System
        public ActionResult Index()
        {
            if (Session["acc"] == null)
            {
                return View("../Register/index");
            }
            ViewBag.ACCOUNT = Session["acc"].ToString();

            return View();
        }

        public JsonResult SetLanguage(string lan)
        {
            if(lan == "TW")
            {
                THC_Library.Language.LanguageBase.CURRENT_LANGUAGE = THC_Library.Language.Lan_Zone.TW;
            }
            else if(lan == "EN")
            {
                THC_Library.Language.LanguageBase.CURRENT_LANGUAGE = THC_Library.Language.Lan_Zone.EN;
            }
            else
            {
                THC_Library.Language.LanguageBase.CURRENT_LANGUAGE = THC_Library.Language.Lan_Zone.CN;
            }           
            string  retJson = WebHTCBackEnd.Utility.JsonResponeUtility.ResultOK();
            return Json(retJson, "application/json", JsonRequestBehavior.AllowGet);
        }
    }
}
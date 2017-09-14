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
            return View();
        }

        public JsonResult SetLanguage(string lan)
        {
            if(lan == "TW")
            {
                Language.LanguageBase.CURRENT_LANGUAGE = Language.Lan_Zone.TW;
            }
            else if(lan == "EN")
            {
                 Language.LanguageBase.CURRENT_LANGUAGE = Language.Lan_Zone.EN;
            }
            else
            {
                 Language.LanguageBase.CURRENT_LANGUAGE = Language.Lan_Zone.CN;
            }           
            string  retJson = WebHTCBackEnd.Utility.JsonResponeUtility.ResultOK();
            return Json(retJson, "application/json", JsonRequestBehavior.AllowGet);
        }
    }
}
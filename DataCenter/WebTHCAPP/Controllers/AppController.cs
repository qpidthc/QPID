using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebTHCAPP.Controllers
{
    public class AppController : Controller
    {
        // GET: App
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult HistoryList()
        {
            return View("");
        }

        public ActionResult NewActivity()
        {
            return View("");
        }

        public ActionResult About()
        {
            return View("");
        }

        
    }
}
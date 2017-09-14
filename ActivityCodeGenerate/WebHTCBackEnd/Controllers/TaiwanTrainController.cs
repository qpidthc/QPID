using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebHTCBackEnd.Controllers
{
    public class TaiwanTrainController : Controller
    {
        // GET: TaiwanTrain
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Go(string id)
        {
            if (!string.IsNullOrEmpty(id) && id == "67ED2F")
            {
                ViewBag.id = id;
                return View();
            }
            else
            {
                Response.Redirect("http://www.tr.net.tw/");
                return null;
            }
        }
    }
}
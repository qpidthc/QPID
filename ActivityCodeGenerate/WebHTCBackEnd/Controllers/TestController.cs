using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebHTCBackEnd.Controllers
{
    public class TestController : Controller
    {
        //
        // GET: /Test/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Call()
        {
            var c = new Models.Test.MyClass();
            c.Name = "1234";
            return View(c);
        }
	}
      
}
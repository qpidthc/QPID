using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;

namespace WebHTCBackEnd.Controllers
{
    public class SystemBaseController : Controller
    {
        // GET: SystemBase
        public ActionResult Index()
        {
            if (Session["acc"] == null)
            {
                return View("../Register/index");
            }
            ViewBag.ACCOUNT = Session["acc"].ToString();

            return View();
        }

        public ActionResult VenderIndex()
        {
            if (Session["acc"] == null)
            {
                return View("../Register/index");
            }
            ViewBag.ACCOUNT = Session["acc"].ToString();

            return View();
            
        }

        [HttpPost]
        public JsonResult SingleVenderData(string accno)
        //public ActionResult SingleVenderData()
        {           
            WebHTCBackEnd.Models.SystemBase.THC_Vender vender = new Models.SystemBase.THC_Vender();
            DataTable venderData = vender.queryVenderByAccountNo(accno);
            string strJson = Newtonsoft.Json.JsonConvert.SerializeObject(venderData);
            return Json(strJson, "application/json", JsonRequestBehavior.AllowGet);    
        }

        [HttpPost]
        public JsonResult NewVenderData()
        {
            var accNo = Request.Form["txt_account"];
            var Name = Request.Form["txt_name"];
            var FullName = Request.Form["txt_full_name"];
            var BNo = Request.Form["txt_bno"];
            var Addr = Request.Form["txt_addr"];
            var Person1 = Request.Form["txt_con_1"];
            var Tel1 = Request.Form["txt_tel_1"];
            var Mail1 = Request.Form["txt_mail_1"];
            var Person2 = Request.Form["txt_con_2"];
            var Tel2 = Request.Form["txt_tel_2"];
            var Mail2 = Request.Form["txt_mail_2"];

            Error.Error error = null;
            WebHTCBackEnd.Models.SystemBase.THC_Vender vender = new Models.SystemBase.THC_Vender();
            int iId = vender.addNewVender(accNo, Name, FullName, BNo, Addr, Person1, Tel1, Mail1, Person2, Tel2, Mail2, out error);

            string retJson;
            if (error != null)
            {
                retJson = Newtonsoft.Json.JsonConvert.SerializeObject(error);
            }
            else
            {
                retJson = string.Format("[{{ \"NewId\" : {0} }}]", iId);
            }

            return Json(retJson, "application/json", JsonRequestBehavior.AllowGet);  
        }

        [HttpPost]
        public JsonResult UpdateVenderData(string accno)
        {
            var accNo = Request.Form["txt_account"];
            var Name = Request.Form["txt_name"];
            var FullName = Request.Form["txt_full_name"];
            var BNo = Request.Form["txt_bno"];
            var Addr = Request.Form["txt_addr"];
            var Person1 = Request.Form["txt_con_1"];
            var Tel1 = Request.Form["txt_tel_1"];
            var Mail1 = Request.Form["txt_mail_1"];
            var Person2 = Request.Form["txt_con_2"];
            var Tel2 = Request.Form["txt_tel_2"];
            var Mail2 = Request.Form["txt_mail_2"];

            Error.Error error = null;
            WebHTCBackEnd.Models.SystemBase.THC_Vender vender = new Models.SystemBase.THC_Vender();
            vender.updateVender(accNo, Name, FullName, BNo, Addr, Person1, Tel1, Mail1, Person2, Tel2, Mail2, out error);

            string retJson;
            if (error != null)
            {
                retJson = Newtonsoft.Json.JsonConvert.SerializeObject(error);
            }
            else
            {
                retJson = WebHTCBackEnd.Utility.JsonResponeUtility.ResultOK();
            }

            return Json(retJson, "application/json", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DeleteVenderData(string accno)
        {           
            Error.Error error = null;
            WebHTCBackEnd.Models.SystemBase.THC_Vender vender = new Models.SystemBase.THC_Vender();
            vender.deleteVender(accno, out error);

            string retJson;
            if (error != null)
            {
                retJson = Newtonsoft.Json.JsonConvert.SerializeObject(error);
            }
            else
            {
                retJson = WebHTCBackEnd.Utility.JsonResponeUtility.ResultOK();
            }

            return Json(retJson, "application/json", JsonRequestBehavior.AllowGet);
        }

        public ActionResult VenderDate()
        {
            WebHTCBackEnd.Models.SystemBase.THC_Vender vender = new Models.SystemBase.THC_Vender();
            DataTable venderData = vender.queryVenderList();
                       
            var lanSet = new Language.Vender_Lan();
            lanSet.CurrentZone = THC_Library.Language.LanguageBase.CURRENT_LANGUAGE;
            ViewData["lan"] = lanSet;
            return View(venderData);
        }

        public ActionResult VenderDataSearch(string accno, string accname, string accbon)
        {
            if (Session["acc"] == null)
            {
                return View("../Register/index");
            }
            ViewBag.ACCOUNT = Session["acc"].ToString();

            string accNo = Request.QueryString["accno"];
            WebHTCBackEnd.Models.SystemBase.THC_Vender vender = new Models.SystemBase.THC_Vender();
            DataTable venderData = vender.queryVenderSearch(accno, accname, accbon);
                       
            var lanSet = new Language.Vender_Lan();
            lanSet.CurrentZone = THC_Library.Language.LanguageBase.CURRENT_LANGUAGE;
            ViewData["lan"] = lanSet;

            ViewBag.s_acc_no = accno;
            ViewBag.s_acc_name = accname;
            ViewBag.s_bno = accbon;

            return View("VenderDate", venderData);
        }
        


    }
}
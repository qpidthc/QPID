using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebTHCEventUI.Controllers
{
    public class RegisterController : Controller
    {
        // GET: Register
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult newAccount()
        {
            var reqMail = Request.Form["mail"];
            var reqGender = Request.Form["gender"];
            var reqAge = Request.Form["age"];           
            var reqPwd = Request.Form["pwd"];
           
            //string strAccount = null;

            //if (!String.IsNullOrEmpty(reqMail))
            //{
            //    strAccount = reqMail;
            //}
            //else if (!String.IsNullOrEmpty(reqMobil))
            //{
            //    strAccount = reqMobil;
            //}
           
            THC_Library.Error error;
            long timeStamp;

            
            Models.Member member = new Models.Member();
            int newKey = member.newAccount(reqMail, reqMail, "", reqPwd, reqGender, reqAge, out timeStamp, out error);

            string retJson;
            if (error != null)
            {
                retJson = Newtonsoft.Json.JsonConvert.SerializeObject(error);
            }
            else
            {
                retJson = string.Format("[{{ \"NewId\" : {0}, \"TICKET\" : \"{1}\" , \"ACC\" : \"{2}\" }} ]",
                                newKey, timeStamp, reqMail);
            }

            return Json(retJson, "application/json", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult verifyAccount()
        {
            var reqMail = Request.Form["mail"];
            var reqPwd = Request.Form["pwd"];

            THC_Library.Error error;

            Models.Member member = new Models.Member();
            long lgTimestamp = member.verifyAccount(reqMail, reqPwd, out error);

            string retJson;
            if (error != null)
            {
                retJson = Newtonsoft.Json.JsonConvert.SerializeObject(error);
            }
            else
            {
                if (lgTimestamp > -1)
                {
                    retJson = string.Format("[{{ \"VERIFY\" : 1 , \"TICKET\" : \"{0}\" , \"ACC\" : \"{1}\" }} ]", 
                        lgTimestamp, reqMail);
                }
                else
                {
                    retJson = "[{ \"VERIFY\" : 0 , \"MESSAGE\" : \"登入錯誤\"} ]";
                }                
            }
            
            return Json(retJson, "application/json", JsonRequestBehavior.AllowGet);
        }
        
        [HttpPost]
        public JsonResult verifyAccountWithSession()
        {
            var reqMail = Request.Form["mail"];
            var reqPwd = Request.Form["pwd"];

            THC_Library.Error error;

            Models.Member member = new Models.Member();
            long lgTimestamp = member.verifyAccount(reqMail, reqPwd, out error);

            string retJson;
            if (error != null)
            {
                retJson = Newtonsoft.Json.JsonConvert.SerializeObject(error);
            }
            else
            {
                if (lgTimestamp > -1)
                {
                    string strSession = THC_Library.UserData.EncrySaveData(reqMail, reqPwd);

                    retJson = string.Format("[{{ \"VERIFY\" : 1 , \"TICKET\" : \"{0}\" , \"ACC\" : \"{1}\", \"SESSION\" : \"{2}\" }} ]",
                        lgTimestamp, reqMail, strSession);
                }
                else
                {
                    retJson = "[{ \"VERIFY\" : 0 , \"MESSAGE\" : \"登入錯誤\"} ]";
                }
            }

            return Json(retJson, "application/json", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult deCodeSession()
        {
            var reqSession = Request.Form["session"];
                       
            string strAccount, strPwd;
            bool bResult = THC_Library.UserData.DecrypSaveData(reqSession, out strAccount, out strPwd);

            string retJson;
            if (bResult)
            {
                retJson = string.Format("[{{ \"VERIFY\" : 1 , \"ACC\" : \"{0}\" , \"TOKEN\" : \"{1}\" }} ]",
                       strAccount, strPwd);
            }
            else
            {
                retJson = "[{ \"VERIFY\" : 0 , \"MESSAGE\" : \"無法解析\"} ]";
            }

            return Json(retJson, "application/json", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult verifyAccount_FB()
        {           
            string regActivity = Request.Form["ac"];
            string regCode = Request.Form["code"];
            string regMail = Request.Form["fb"];
            string regName = Request.Form["name"];
            string regGender = Request.Form["gender"];

            THC_Library.Error error;
            string strAccount;
            Models.Member member = new Models.Member();
            long lgTimestamp = member.verifyFaceBookAccount(regActivity, regCode, regMail, regName, regGender, out strAccount, out error);

            string retJson;
            if (error != null)
            {
                retJson = Newtonsoft.Json.JsonConvert.SerializeObject(error);
            }
            else
            {
                if (lgTimestamp > -1)
                {                   
                    retJson = string.Format("[{{ \"VERIFY\" : 1 , \"TICKET\" : \"{0}\" , \"ACC\" : \"{1}\" }} ]",
                        lgTimestamp, strAccount);
                }
                else
                {
                    retJson = "[{ \"VERIFY\" : 0 , \"MESSAGE\" : \"登入錯誤\"} ]";
                }
            }

            return Json(retJson, "application/json", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult renewAccountInfo()
        {
            var reqMail = Request.Form["ml"];
            var reqTicket = Request.Form["tk"];
            var reqMobil = Request.Form["m"];
            var reqIId = Request.Form["iid"];
            var reqAddr = Request.Form["addr"];
                                  
            THC_Library.Error error;

            Models.Member member = new Models.Member();
            int iRowCount = member.updateAccount(reqMail, reqTicket, reqMobil, reqIId, reqAddr, out error);

            string retJson;
            if (error != null)
            {
                retJson = Newtonsoft.Json.JsonConvert.SerializeObject(error);
            }
            else
            {
                if (iRowCount > 0)
                {
                    retJson = string.Format("[{{ \"VERIFY\" : 1, \"COUNT\" : \"{0}\" }} ]",
                                    iRowCount);
                }
                else
                {
                    retJson = "[{ \"VERIFY\" : 0 , \"MESSAGE\" : \"資料未更新錯誤\"} ]";
                }
            }

            return Json(retJson, "application/json", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult renewAccountMobil()
        {
            var reqMail = Request.Form["ml"];
            var reqTicket = Request.Form["tk"];
            var reqMobil = Request.Form["m"];            
           
            THC_Library.Error error;

            Models.Member member = new Models.Member();
            int iRowCount = member.updateMobil(reqMail, reqTicket, reqMobil, out error);

            string retJson;
            if (error != null)
            {
                retJson = Newtonsoft.Json.JsonConvert.SerializeObject(error);
            }
            else
            {
                if (iRowCount > 0)
                {
                    retJson = string.Format("[{{ \"VERIFY\" : 1, \"COUNT\" : \"{0}\" }} ]",
                                    iRowCount);
                }
                else
                {
                    retJson = "[{ \"VERIFY\" : 0 , \"MESSAGE\" : \"資料未更新錯誤\"} ]";
                }
            }

            return Json(retJson, "application/json", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult requestForget()
        {
            var reqMail = Request.Form["ml"];           

            THC_Library.Error error;

            Models.Member member = new Models.Member();
            bool bResult = member.requestForgetPassword(reqMail, out error);

            string retJson;
            if (error != null)
            {
                retJson = Newtonsoft.Json.JsonConvert.SerializeObject(error);
            }
            else
            {
                if (bResult)
                {
                    retJson = string.Format("[{{ \"VERIFY\" : 1 }} ]");
                }
                else
                {
                    retJson = "[{ \"VERIFY\" : 0 , \"MESSAGE\" : \"資料未更新錯誤\"} ]";
                }
            }

            return Json(retJson, "application/json", JsonRequestBehavior.AllowGet);
        }

                
    }
}
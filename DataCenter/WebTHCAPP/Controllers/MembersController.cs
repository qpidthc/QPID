using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebTHCAPP.Controllers
{
    public class MembersController : Controller
    {
        // GET: Members
        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult MyInfo()
        {
            var reqAcc= Request.Form["acc"];
            var reqTicket = Request.Form["tk"];

            if (string.IsNullOrEmpty(reqAcc) || string.IsNullOrEmpty(reqTicket))
            {
                return View();
            }
            else
            {
                return View();
            }            
        }

        public ActionResult load2(string acc, string tk)
        {
            if (string.IsNullOrEmpty(acc) || string.IsNullOrEmpty(tk))
            {
                return View("");
            }

            THC_Library.Error error = null;

            Models.Member member = new Models.Member();
            long newTicket = member.loginFromActivity(acc, tk, out error);

            if (error != null)
            {
                ViewBag.TICKET = newTicket;
                return View("index");
            }
            else
            {
                return View();
            }            
        }
                        
        //newAccount
        [HttpPost]        
        public JsonResult THC_Member_01()
        {
            THC_Library.Error error = null;
            long timeStamp = 0;
            int newKey = -1;           

            string strTick = Request.Headers["QPID-TICK"];            
            string strData = Request.Headers["QPID-DATA"];

            error = WebTHCAPP.Models.RequestChecker.CheckRequest(strTick, strData);

            Models.Result result = new Models.Result();

            if (error == null)
            {
                var reqMail = Request.Form["mail"];
                var reqPwd = Request.Form["pwd"];
                var reqMobil = Request.Form["mobil"];
                string strAccount = Request.Form["acc"];

                Models.Member member = new Models.Member();
                newKey = member.newAccount(strAccount, reqMail, reqMobil, reqPwd, out timeStamp, out error);
                result.Addition = newKey.ToString();
                result.Ticket = timeStamp.ToString();
                result.Acc = strAccount;
                result.Number = 0;
                
                //retJson = string.Format("[{{ \"NewId\" : {0}, \"TICKET\" : \"{1}\" , \"ACC\" : \"{2}\" }} ]",
                //                newKey, timeStamp, strAccount);
            }           
           
            if (error != null)
            {
                result.Number = error.Number;
                result.ErrorMessage = error.ErrorMessage;
                //retJson = Newtonsoft.Json.JsonConvert.SerializeObject(error);
            }
           
            return Json(result, "application/json", JsonRequestBehavior.AllowGet);
        }

        //verifyAccount
        [HttpPost]
        public JsonResult THC_Member_02()
        {
            THC_Library.Error error = null;           
            string strTick = Request.Headers["QPID-TICK"];
            string strData = Request.Headers["QPID-DATA"];
            error = WebTHCAPP.Models.RequestChecker.CheckRequest(strTick, strData);

            Models.Result result = new Models.Result();
            if (error == null)
            {
                var reqMail = Request.Form["mail"];
                var reqPwd = Request.Form["pwd"];
                Models.Member member = new Models.Member();
                long lgTimestamp = member.verifyAccount(reqMail, reqPwd, out error);
                if (lgTimestamp > -1)
                {
                    result.Verify = 1;
                    result.Ticket = lgTimestamp.ToString();
                    result.Acc = reqMail;
                }
                else
                {                   
                    result.Ticket = lgTimestamp.ToString();
                    result.Number = 10;
                    result.Verify = 0;
                    result.ErrorMessage = "登入錯誤";                    
                }
            }
                                  
            if (error != null)
            {
                result.Number = error.Number;
                result.ErrorMessage = error.ErrorMessage;              
            }
            
            return Json(result, "application/json", JsonRequestBehavior.AllowGet);
        }

        //verifyAccount_FB
        [HttpPost]
        public JsonResult THC_Member_03()
        {
            THC_Library.Error error = null;
            
            string strTick = Request.Headers["QPID-TICK"];
            string strData = Request.Headers["QPID-DATA"];
            error = WebTHCAPP.Models.RequestChecker.CheckRequest(strTick, strData);

            Models.Result result = new Models.Result();
            if (error == null)
            {
                string regActivity = Request.Form["ac"];
                string regCode = Request.Form["code"];
                string regMail = Request.Form["fb"];
                string regName = Request.Form["name"];
                string regGender = Request.Form["gender"];

                string strAccount;
                Models.Member member = new Models.Member();
                long lgTimestamp = member.verifyFaceBookAccount(regMail, regName, regGender, out strAccount, out error);
                if (lgTimestamp > -1)
                {
                    result.Verify = 1;
                    result.Ticket = lgTimestamp.ToString();
                    result.Acc = strAccount;
                    //retJson = string.Format("[{{ \"VERIFY\" : 1 , \"TICKET\" : \"{0}\" , \"ACC\" : \"{1}\" }} ]",
                    //    lgTimestamp, strAccount);
                }
                else
                {
                    result.Ticket = lgTimestamp.ToString();
                    result.Number = 10;
                    result.Verify = 0;
                    result.ErrorMessage = "登入錯誤";
                    //retJson = "[{ \"VERIFY\" : 0 , \"MESSAGE\" : \"登入錯誤\"} ]";
                }
            }
           
            if (error != null)
            {
                result.Number = error.Number;
                result.ErrorMessage = error.ErrorMessage;
            }
           
            return Json(result, "application/json", JsonRequestBehavior.AllowGet);
        }

        //renewAccountInfo
        [HttpPost]
        public JsonResult THC_Member_04()
        {
            THC_Library.Error error = null;            
            string strTick = Request.Headers["QPID-TICK"];
            string strData = Request.Headers["QPID-DATA"];
            error = WebTHCAPP.Models.RequestChecker.CheckRequest(strTick, strData);
            Models.Result result = new Models.Result();

            if (error == null)
            {
                var reqMail = Request.Form["ml"];
                var reqTicket = Request.Form["tk"];
                var reqMobil = Request.Form["m"];
                var reqGender = Request.Form["g"];
                var reqAge = Request.Form["a"];
                var reqIId = Request.Form["iid"];
                var reqAddr = Request.Form["addr"];

                Models.Member member = new Models.Member();
                int iRowCount = member.updateAccount(reqMail, reqTicket, reqMobil, reqGender,
                                    reqAge, reqIId, reqAddr, out error);
                if (iRowCount > 0)
                {
                    result.Verify = 1;                    
                    result.Addition = iRowCount.ToString();
                }
                else
                {                  
                    result.Number = 20;
                    result.Verify = 0;
                    result.ErrorMessage = "資料未更新錯誤";                   
                }
            }
            
            if (error != null)
            {
                result.Number = error.Number;
                result.ErrorMessage = error.ErrorMessage;               
            }
           
            return Json(result, "application/json", JsonRequestBehavior.AllowGet);
        }

        //getAccountInfo
        [HttpPost]
        public JsonResult THC_Member_05()
        {
            THC_Library.Error error = null;
            string strTick = Request.Headers["QPID-TICK"];
            string strData = Request.Headers["QPID-DATA"];
            error = WebTHCAPP.Models.RequestChecker.CheckRequest(strTick, strData);
            Models.Result result = new Models.Result();

            if (error == null)
            {
                var reqAcc = Request.Form["acc"];
                var reqTicket = Request.Form["tk"];

                Models.Member member = new Models.Member();
                Models.AccountInfo accInfo = member.getAccountInfo(reqAcc, reqTicket, out error);
                if (accInfo != null)
                {
                    return Json(accInfo, "application/json", JsonRequestBehavior.AllowGet);
                    //retJson = string.Format("[{{ \"VERIFY\" : 1, \"COUNT\" : \"{0}\" }} ]",
                    //                iRowCount);
                }
                else
                {
                    result.Number = 22;
                    result.Verify = 0;
                    result.ErrorMessage = "無效登入";
                    //retJson = "[{ \"VERIFY\" : 0 , \"MESSAGE\" : \"資料未更新錯誤\"} ]";
                }
            }

            if (error != null)
            {
                result.Number = error.Number;
                result.ErrorMessage = error.ErrorMessage;
            }

            return Json(result, "application/json", JsonRequestBehavior.AllowGet);
        }

        //record QR scan data
        [HttpPost]
        public JsonResult THC_Member_06()
        {
            THC_Library.Error error = null;
            string strTick = Request.Headers["QPID-TICK"];
            string strData = Request.Headers["QPID-DATA"];
            error = WebTHCAPP.Models.RequestChecker.CheckRequest(strTick, strData);
            Models.Result result = new Models.Result();

            if (error == null)
            {
                //string.Format("eventkey={0}&code={1}&date={2}&acc={3}&age={4}&gender={5}&area={6}&temp={7}&weather={8}",
                var reqEventKey = Request.Form["eventkey"];
                var reqCode = Request.Form["code"];
                var reqDate = Request.Form["date"];
                var reqAcc = Request.Form["acc"];
                var reqAge = Request.Form["age"];
                var reqGender = Request.Form["gender"];
                var reqArea = Request.Form["area"];
                var reqTempture = Request.Form["temp"];
                var reqWeather = Request.Form["weather"];
                var reqLat = Request.Form["lat"];
                var reqLong = Request.Form["lng"];
                var reqRewardName = Request.Form["rwd"];
                var reqTicket = Request.Form["tk"];
                

                Models.Member member = new Models.Member();
                member.newRecord( reqEventKey, reqCode, reqDate, reqAcc, reqAge, reqGender, reqArea,
                                    reqTempture, reqWeather, reqLat, reqLong, reqRewardName, reqTicket, out error);

                if (error == null)
                {
                    result.Verify = 1;
                    result.Number = 0;
                    result.ErrorMessage = "";
                    result.Acc = reqAcc;
                    result.Ticket = reqTicket;
                    //return Json(accInfo, "application/json", JsonRequestBehavior.AllowGet);
                    //retJson = string.Format("[{{ \"VERIFY\" : 1, \"COUNT\" : \"{0}\" }} ]",
                    //                iRowCount);
                }
                else
                {
                    result.Number = error.Number;
                    result.Verify = 0;
                    result.ErrorMessage = error.ErrorMessage;
                    //retJson = "[{ \"VERIFY\" : 0 , \"MESSAGE\" : \"資料未更新錯誤\"} ]";
                }
            }

            if (error != null)
            {
                result.Number = error.Number;
                result.ErrorMessage = error.ErrorMessage;
            }

            return Json(result, "application/json", JsonRequestBehavior.AllowGet);
        }

        //login from activity
        //[HttpPost]
        public ActionResult THC_Member_07(string acc, string tk)
        {
            THC_Library.Error error = null;
            //string strTick = Request.Headers["QPID-TICK"];
            //string strData = Request.Headers["QPID-DATA"];
            //error = WebTHCAPP.Models.RequestChecker.CheckRequest(strTick, strData);
            Models.Result result = new Models.Result();

            if (string.IsNullOrEmpty(acc) || string.IsNullOrEmpty(tk))
            {
                return View("../Error/NotAllow");
            }

            Models.Member member = new Models.Member();
            long newTicket = member.loginFromActivity(acc, tk, out error);

            if (error == null)
            {
                Models.AppSession appSession = new Models.AppSession();
                appSession.Account = acc;
                appSession.Ticket = newTicket;

                Session["tk"] = appSession;
                ViewBag.ACC = acc;
                ViewBag.TICKET = newTicket;
                return View("../App/index");
            }
            else
            {
                //result.Number = error.Number;
                //result.Verify = 0;
                //result.ErrorMessage = error.ErrorMessage;
                ViewBag.NUMBER = error.Number;
                ViewBag.ERROR = error.ErrorMessage;

                return View("../Error/SystemError");
            }
            
        }

        [HttpPost]
        public ActionResult RenewUserInfo()
        {
            THC_Library.Error error;
            Models.Result result = new Models.Result();

            //if (Session["tk"] == null)
            //{
            //    result.Number = 10;
            //    result.Verify = 0;
            //    result.ErrorMessage = "無效的操作";
            //    return Json(result, "application/json", JsonRequestBehavior.AllowGet);
            //}

            var reqMail = Request.Form["ml"];
            var reqTicket = Request.Form["tk"];
            var reqMobil = Request.Form["m"];
            var reqGender = Request.Form["g"];
            var reqAge = Request.Form["a"];
            var reqIId = Request.Form["iid"];
            var reqAddr = Request.Form["addr"];

            Models.Member member = new Models.Member();
            int iRowCount = member.updateAccount(reqMail, reqTicket, reqMobil, reqGender,
                                reqAge, reqIId, reqAddr, out error);

            if (error == null)
            {

                if (iRowCount > 0)
                {
                    result.Number = 0;
                    result.Verify = 1;
                    result.Addition = iRowCount.ToString();
                }
                else
                {
                    result.Number = 20;
                    result.Verify = 0;
                    result.ErrorMessage = "資料未更新錯誤";
                }
            }
            else
            {
                result.Number = error.Number;
                result.ErrorMessage = error.ErrorMessage;
            }

            return Json(result, "application/json", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult ForgestPassword()
        {
            THC_Library.Error error;
            Models.Result result = new Models.Result();

            var reqMail = Request.Form["ml"];
            string accessCode;           

            Models.Member member = new Models.Member();
            member.requestResetPassword(reqMail, out accessCode, out error);

            if (error == null)
            {
                String strPathAndQuery = Request.Url.PathAndQuery;
                String strUrl = Request.Url.AbsoluteUri.Replace(strPathAndQuery, "/");

                Models.MailClass mail = new Models.MailClass();
                mail.Send(reqMail, accessCode, strUrl, out error);

                if (error != null)
                {
                    result.Number = error.Number;
                    result.ErrorMessage = error.ErrorMessage;
                }
                else
                {
                    result.Number = 0;
                    result.ErrorMessage = "";
                }
            }
            else
            {
                result.Number = error.Number;
                result.ErrorMessage = error.ErrorMessage;
            }

            return Json(result, "application/json", JsonRequestBehavior.AllowGet);
        }

        public ActionResult AccessRestPassword(string acc, string access)
        {
            THC_Library.Error error;
            Models.Result result = new Models.Result();

            if (string.IsNullOrEmpty(acc) || string.IsNullOrEmpty(access))
            {
                return View("../Error/NotAllow");
            }

            Models.Member member = new Models.Member();
            bool bCodeExist = member.accessResetPassword(acc, access, out error);

            if (error == null)
            {
                if (bCodeExist)
                {
                    ViewBag.ACCESS_CODE = access;
                    ViewBag.ACC = acc;
                    return View();

                }
                else
                {
                    return View("../Error/NotAllow");
                }
            }
            else
            {
                ViewBag.NUMBER = 201;
                ViewBag.ERROR = "系統錯誤";
                return View("../Error/SystemError");
            }
           
        }

        [HttpPost]
        public ActionResult DoRestPassword(string acc, string access, string pwd)
        {
            THC_Library.Error error;
            Models.Result result = new Models.Result();

            if (string.IsNullOrEmpty(acc) || string.IsNullOrEmpty(access) || string.IsNullOrEmpty(pwd))
            {
                result.Number = 201;
                result.ErrorMessage = "無效的請求";
                return Json(result, "application/json", JsonRequestBehavior.AllowGet);
            }

            Models.Member member = new Models.Member();
            member.doResetPassword(acc, access, pwd, out error);

            if (error == null)
            {
                result.Number = 0;
                result.ErrorMessage = "";
            }
            else
            {
                result.Number = 201;
                result.ErrorMessage = "系統錯誤";
            }
            return Json(result, "application/json", JsonRequestBehavior.AllowGet);
        }
    }
}
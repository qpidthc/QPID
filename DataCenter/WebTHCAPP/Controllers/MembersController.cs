using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Drawing;
using System.Drawing.Imaging;

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
                var strAccount = Request.Form["acc"];
                var reqGender = Request.Form["gender"];
                var reqAge = Request.Form["age"];  

                Models.Member member = new Models.Member();
                newKey = member.newAccount(strAccount, reqMail, reqMobil, reqPwd, reqGender, reqAge, out timeStamp, out error);
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
                int state;
                Models.Member member = new Models.Member();
                long lgTimestamp = member.verifyAccount(reqMail, reqPwd, out state, out error);
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
                    if (state == 1)
                    {
                        result.ErrorMessage = "帳號不存在";  
                    }
                    else if (state == 2)
                    {
                        result.ErrorMessage = "密碼錯誤";  
                    }
                    else
                    {
                        result.ErrorMessage = "登入錯誤";  
                    }                                      
                }
            }
                                  
            if (error != null)
            {
                result.Number = error.Number;
                result.ErrorMessage = error.ErrorMessage;              
            }
            
            return Json(result, "application/json", JsonRequestBehavior.AllowGet);
        }
        //verifyAccountWithInfo
        [HttpPost]
        public JsonResult THC_Member_02_1()
        {
            THC_Library.Error error = null;
            string strTick = Request.Headers["QPID-TICK"];
            string strData = Request.Headers["QPID-DATA"];
            error = WebTHCAPP.Models.RequestChecker.CheckRequest(strTick, strData);

            Models.ResultWithInfo result = new Models.ResultWithInfo();
            if (error == null)
            {
                var reqMail = Request.Form["mail"];
                var reqPwd = Request.Form["pwd"];
                int state;
                string name, mobil, addr, iid, gender, age;
                Models.Member member = new Models.Member();
                long lgTimestamp = member.verifyAccountWitInfo(reqMail, reqPwd, out state, out name, out mobil, out addr, out iid, out gender, out age, out error);
                if (lgTimestamp > -1)
                {
                    result.Verify = 1;
                    result.Ticket = lgTimestamp.ToString();
                    result.Acc = reqMail;
                    result.Name = name;
                    result.Mobil = mobil;
                    result.Addr = addr;
                    result.IId = iid;
                    result.Gender = gender;
                    result.Age = age;
                }
                else
                {
                    result.Ticket = lgTimestamp.ToString();
                    result.Number = 10;
                    result.Verify = 0;
                    if (state == 1)
                    {
                        result.ErrorMessage = "帳號不存在";
                    }
                    else if (state == 2)
                    {
                        result.ErrorMessage = "密碼錯誤";
                    }
                    else
                    {
                        result.ErrorMessage = "登入錯誤";
                    }
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
                var reqIId = Request.Form["iid"];
                var reqAddr = Request.Form["addr"];

                Models.Member member = new Models.Member();
                int iRowCount = member.updateAccount(reqMail, reqTicket, reqMobil, reqIId, reqAddr, null, out error);
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

        //renewAccountMobil
        [HttpPost]
        public JsonResult THC_Member_04_m()
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
               
                Models.Member member = new Models.Member();
                int iRowCount = member.updateAccountMobil(reqMail, reqTicket, reqMobil, out error);
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
                var reqECNumber = Request.Form["ec"];
                var reqRewardType = Request.Form["rwdtype"];
                var reqWinDesc = Request.Form["windesc"];
                var reqTicket = Request.Form["tk"];
                

                Models.Member member = new Models.Member();
                member.newRecord( reqEventKey, reqCode, reqDate, reqAcc, reqAge, reqGender, reqArea,
                                    reqTempture, reqWeather, reqLat, reqLong, reqRewardName, reqECNumber, reqRewardType,
                                    reqWinDesc, reqTicket, out error);

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
        public ActionResult THC_Member_07(string acc, string evt, string tk)
        {
            THC_Library.Error error = null;
            //string strTick = Request.Headers["QPID-TICK"];
            //string strData = Request.Headers["QPID-DATA"];
            //error = WebTHCAPP.Models.RequestChecker.CheckRequest(strTick, strData);
            Models.Result result = new Models.Result();

            if (string.IsNullOrEmpty(acc) || string.IsNullOrEmpty(evt) || string.IsNullOrEmpty(tk))
            {
                return View("../Error/NotAllow");
            }

            Models.Member member = new Models.Member();
            long newTicket = member.loginFromActivity(acc, tk, out error);

            if (error == null)
            {
                Models.AppSession appSession = new Models.AppSession();
                appSession.Account = acc;
                appSession.EventNo = int.Parse(evt);
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
        public JsonResult THC_Member_08(string acc, string authkey)
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
                Models.AccountInfo accInfo = member.getAccountInfoNoTicket(reqAcc, out error);
                if (accInfo != null)
                {
                    return Json(accInfo, "application/json", JsonRequestBehavior.AllowGet);                   
                }
                else
                {
                    result.Number = 22;
                    result.Verify = 0;
                    result.ErrorMessage = "無效資料";                    
                }
            }

            if (error != null)
            {
                result.Number = error.Number;
                result.ErrorMessage = error.ErrorMessage;
            }

            return Json(result, "application/json", JsonRequestBehavior.AllowGet);

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
                                reqAge, reqIId, reqAddr, null, out error);

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
        //
        [HttpPost]
        public ActionResult RenewUserInfo2(HttpPostedFileBase userFile)
        {
            byte[] bImage = null;

            THC_Library.Error error;
            Models.Result result = new Models.Result();

            if (Session["tk"] == null)
            {
                result.Number = 999;
                result.Verify = 0;
                result.ErrorMessage = "無效的操作";
                return Json(result, "application/json", JsonRequestBehavior.AllowGet);
            }

            var reqMail = Request.Form["ml"];
            var reqTicket = Request.Form["tk"];
            var reqMobil = Request.Form["m"];
            var reqGender = Request.Form["g"];
            var reqAge = Request.Form["a"];
            var reqIId = Request.Form["iid"];
            var reqAddr = Request.Form["addr"];

            Models.Member member = new Models.Member();

            if (userFile != null)
            {
                bImage = member.compressImage(userFile.InputStream);
            }

            int iRowCount = member.updateAccount(reqMail, reqTicket, reqMobil, reqGender,
                                reqAge, reqIId, reqAddr, bImage, out error);

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
        public ActionResult MyPoto()
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

            //Models.AppSession appSession = (Models.AppSession)Session["tk"];
            //var reqMail = appSession.Account;
            //var reqTicket = appSession.Ticket;

            var reqMail = Request.Form["ml"];
            var reqTicket = Request.Form["tk"];
           
            Models.Member member = new Models.Member();
                       
            byte[] myPoto = member.getMyPoto(reqMail, reqTicket, out error);

            if (error == null)
            {                
                if (myPoto != null)
                {
                    result.Number = 0;
                    result.Verify = 1;
                    result.Addition = "data:image/png;base64," + Convert.ToBase64String(myPoto, 0, myPoto.Length);
                }
                else
                {
                    result.Number = 21;
                    result.Verify = 0;
                    result.ErrorMessage = "取得圖檔錯誤";
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
                //String myURL = HttpContext.Request.Url.Host;
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
            //http://60.251.140.166/WebTHCApp/Members/AccessRestPassword?acc=tungken@gmail.com&access=29b67e6e0c874958989747007b1d4e8c
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
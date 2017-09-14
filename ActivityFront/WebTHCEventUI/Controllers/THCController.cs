using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;

namespace WebTHCEventUI.Controllers
{
    public class THCController : Controller
    {
       
        // GET: THC
        public ActionResult Index(string ac, string code)
        {
            //if (!THC_Library.RequestChecker.IsMobil(Request.ServerVariables["HTTP_USER_AGENT"]))
            //{
            //    return View();
            //}
            //else
            {
                if (string.IsNullOrEmpty(ac) || string.IsNullOrEmpty(code))
                {
                    ViewBag.Message = "";
                    ViewBag.MessageInfo = "";// string.Format("{0} {1}", ac, code);
                    return View("outOfPage");
                }
                else
                {
                    ViewBag.ACTIVITY = ac;
                    ViewBag.CODE = code;
                    return View();
                }
            }           
        }

        public ActionResult preGo(string ac,  string code)
        {
            //if (!THC_Library.RequestChecker.IsMobil(Request.ServerVariables["HTTP_USER_AGENT"]))
            //{
            //    return View();
            //}
            //else
            {
                if (string.IsNullOrEmpty(ac) || string.IsNullOrEmpty(code))
                {
                    return View("outOfPage");
                }

                THC_Library.Error error;

                 Models.CodeRender codeRender = new Models.CodeRender();
                 codeRender.checkActivityAndCode(ac, code, out error);
                
                 if (error != null)
                 {
                     switch (error.Number)
                     {
                         case THC_Library.CodeRenderException.ACTIVITY_FINISHED:
                             ViewBag.Message = "活動已結束";
                             ViewBag.MessageInfo = error.ErrorMessage;
                             return View("outOfPage");

                         case THC_Library.CodeRenderException.ACTIVITY_NOT_START:
                             ViewBag.Message = "活動尚未開始";
                             ViewBag.MessageInfo = error.ErrorMessage;
                             return View("outOfPage");

                         case THC_Library.CodeRenderException.INVAILD_ACTIVITY:
                             ViewBag.Message = "無效的活動";
                             break;
                         case THC_Library.CodeRenderException.INVAILD_CODE:
                             ViewBag.Message = "無效的碼號";
                             break;
                         //return View("wrongPage");                       
                         case THC_Library.CodeRenderException.REPEAT_SCAN:
                             ViewBag.Message = "已掃描";
                             string[] strSplit =  error.ErrorMessage.Split(' ');
                             ViewBag.RepeatDate = strSplit[0];
                             ViewBag.RepeatTime = strSplit[1];
                             return View("repeatPage");
                         case THC_Library.CodeRenderException.SYSTEM_ERROR:
                             break;
                     }
                     return View("wrongPage");
                 }
                 else
                 {
                     ViewBag.ACTIVITY = ac;
                     ViewBag.CODE = code;
                     return View("index");
                 }               
            }
           
        }

        public ActionResult preLogin(string ac, string code, string city, string lat, string lng)
        {
            ViewBag.ACTIVITY = ac;
            ViewBag.CODE = code;
            ViewBag.CITY = city;
            ViewBag.LAT = lat;
            ViewBag.LONG = lng;
            return View("preLoginPage");
        }

        public ActionResult login(string ac, string code, string city, string lat, string lng)
        {
            ViewBag.ACTIVITY = ac;
            ViewBag.CODE = code;
            ViewBag.CITY = city;
            ViewBag.LAT = lat;
            ViewBag.LONG = lng;
            return View("loginPage");
        }

        public ActionResult regAccount(string ac, string code, string city, string lat, string lng)
        {
            ViewBag.ACTIVITY = ac;
            ViewBag.CODE = code;
            ViewBag.CITY = city;
            ViewBag.LAT = lat;
            ViewBag.LONG = lng;
            return View("regAccount");
        }

        public ActionResult go(string ac, string code, string tk, string ml, string city, string lat, string lng)
        {
            THC_Library.Error error;
            THC_Library.Reward.RewardConvertor reward;
            string gender, age, Mobil, iid, addr;
            int iLogKey;

            Models.CodeRender codeRender = new Models.CodeRender();
            bool bWin = codeRender.go(ac, code, tk, ml, city, lat, lng, 
                                    out gender, out age, out Mobil, out iid, out addr, out reward, 
                                    out iLogKey, out error);

            if (error != null)
            {
                switch (error.Number)
                {
                    case THC_Library.CodeRenderException.ACTIVITY_FINISHED:
                        ViewBag.Message = "活動已結束";
                        ViewBag.MessageInfo = error.ErrorMessage;
                        return View("outOfPage");              
                      
                    case THC_Library.CodeRenderException.ACTIVITY_NOT_START:
                        ViewBag.Message = "活動尚未開始";
                        ViewBag.MessageInfo = error.ErrorMessage;
                        return View("outOfPage");

                    case THC_Library.CodeRenderException.INVAILD_ACTIVITY:
                        ViewBag.Message = "無效的活動";
                        break;
                    case THC_Library.CodeRenderException.INVAILD_CODE:
                        ViewBag.Message = "無效的碼號";
                        break;
                    //return View("wrongPage");                       
                    case THC_Library.CodeRenderException.REPEAT_SCAN:
                        ViewBag.TICKET = tk;
                        ViewBag.ACC = ml;
                        ViewBag.CITY = city;
                        ViewBag.Message = "已掃描";
                        string[] strSplit =  error.ErrorMessage.Split(' ');
                        ViewBag.RepeatDate = strSplit[0];
                        ViewBag.RepeatTime = strSplit[1];
                        ViewBag.DOMAIN = Models.Domain.REMOTE_ACCESS_DOMAIN;
                        return View("repeatPage");
                    case THC_Library.CodeRenderException.LOGIN_INVALID:
                        ViewBag.Message = "無效的登入";
                        break;
                    case THC_Library.CodeRenderException.SYSTEM_ERROR:
                        ViewBag.Message = "系統錯誤";
                        break;                        
                }
                return View("wrongPage");
            }
            else
            {              
                if (bWin)
                {                    
                    ViewBag.ACTIVITY = ac;
                    ViewBag.CODE = code;
                    ViewBag.TICKET = tk;
                    ViewBag.ACC = ml;
                    ViewBag.MOBIL = Mobil;
                    ViewBag.GENDER = gender;
                    ViewBag.AGE = age;
                    ViewBag.IID = iid;
                    ViewBag.ADDRESS = addr;
                    ViewBag.LOG_KEY = iLogKey.ToString();

                    ViewBag.CITY = city;
                    ViewBag.LAT = lat;
                    ViewBag.LONG = lng;

                    ViewBag.RWD_NAME = reward.RewardName;
                    ViewBag.RWD_IMG = reward.RewardImage;
                    ViewBag.RWD_COUPNUMBER = reward.CouponNumber;

                    if(reward.RewardType == THC_Library.Reward.RewardType.ElectricCoupon)
                    {
                        if (reward is THC_Library.Reward.Edenred)
                        {
                            THC_Library.Reward.Edenred edenred = reward as THC_Library.Reward.Edenred;
                            ViewBag.RWD_DATE = edenred.ValidPeriod;                            
                        }     
                        //虛擬獎項
                        return View("rewardPage");
                    }
                    else
                    {
                        if (reward is THC_Library.Reward.Phyicalenred)
                        {
                            THC_Library.Reward.Phyicalenred phyenred = reward as THC_Library.Reward.Phyicalenred;
                            ViewBag.RWD_DESC = phyenred.Description;
                        }     
                        //實體獎項
                        return View("rewardPhyicalPage");
                    }
                    
                }
                else
                {
                    ViewBag.TICKET = tk;
                    ViewBag.ACC = ml;
                    ViewBag.DOMAIN = Models.Domain.REMOTE_ACCESS_DOMAIN;
                    return View("tryAgain");
                }               
            }            
        }
                
        public ActionResult get()
        {
            THC_Library.Error error;
            var reqActivity = Request.QueryString["act"];
            var reqCode = Request.QueryString["code"];
            var reqTimestamp = Request.QueryString["tk"];
            var reqAccount = Request.QueryString["ml"];
            var reqMobil = Request.QueryString["m"];
            var reqLogKey = Request.QueryString["lk"];
            var reqCoupNumber = Request.QueryString["coup"];
            var reqRwdType = Request.QueryString["type"];

            var reqCity = Request.QueryString["city"];
            var reqLat = Request.QueryString["lat"];
            var reqLong = Request.QueryString["lng"];

            Models.CodeRender codeRender = new Models.CodeRender();
            bool bOK = codeRender.done(reqActivity, reqCode, reqTimestamp, reqAccount, reqCity, reqLat, reqLong, 
                                reqCoupNumber, reqLogKey, out error);

            if (error != null)
            {                                
                ViewBag.Message = error.ErrorMessage;
                return View("wrongPage");
            }
            else
            {
                ViewBag.MOBIL = reqMobil;
                ViewBag.RWD_COUPNUMBER = reqCoupNumber;

                if (reqRwdType == "0")
                {
                    ViewBag.ACC = reqAccount;
                    ViewBag.TICKET = reqTimestamp;
                    ViewBag.MOBIL = reqMobil;
                    ViewBag.DOMAIN = Models.Domain.REMOTE_ACCESS_DOMAIN;
                    return View("get");
                }
                else if (reqRwdType == "1")
                {
                    ViewBag.ACC = reqAccount;
                    ViewBag.TICKET = reqTimestamp;
                    ViewBag.MOBIL = reqMobil;
                    ViewBag.DOMAIN = Models.Domain.REMOTE_ACCESS_DOMAIN;
                    return View("getPhyical");
                }
                else
                {
                    ViewBag.Message = "";
                    ViewBag.MessageInfo = "";
                    return View("outOfPage");
                }
            }
           
        }

        public ActionResult phyicalReward()
        {
            return View("rewardPhyicalPage");
        }

        [HttpPost]
        public JsonResult getCities()
        {            
            THC_Library.Error error;

            Models.TaiwanAddress twAddr = new Models.TaiwanAddress();
            DataTable addrTable = twAddr.getCities(out error);

            string retJson;
            if (error != null)
            {
                retJson = Newtonsoft.Json.JsonConvert.SerializeObject(error);
            }
            else
            {
                retJson = Newtonsoft.Json.JsonConvert.SerializeObject(addrTable);
            }
            return Json(retJson, "application/json", JsonRequestBehavior.AllowGet);
        }
               
        [HttpPost]
        public JsonResult getTowns(string city)
        {
            THC_Library.Error error;

            Models.TaiwanAddress twAddr = new Models.TaiwanAddress();
            DataTable addrTable = twAddr.getTown(city, out error);

            string retJson;
            if (error != null)
            {
                retJson = Newtonsoft.Json.JsonConvert.SerializeObject(error);
            }
            else
            {
                retJson = Newtonsoft.Json.JsonConvert.SerializeObject(addrTable);
            }
            return Json(retJson, "application/json", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult getRoad(string city, string town)
        {
            THC_Library.Error error;

            Models.TaiwanAddress twAddr = new Models.TaiwanAddress();
            DataTable addrTable = twAddr.getRoad(city, town, out error);

            string retJson;
            if (error != null)
            {
                retJson = Newtonsoft.Json.JsonConvert.SerializeObject(error);
            }
            else
            {
                retJson = Newtonsoft.Json.JsonConvert.SerializeObject(addrTable);
            }
            return Json(retJson, "application/json", JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult doc(string code)
        {
            string retJson = "";

            if (string.IsNullOrEmpty(code))
            {
                retJson = "{\"RESPONSE\" : \"ABORT\"}";
                return Json(retJson, "application/json", JsonRequestBehavior.AllowGet);
            }
            if (code != "adDDFasF")
            {
                retJson = "{\"RESPONSE\" : \"ABORT\"}";
                return Json(retJson, "application/json", JsonRequestBehavior.AllowGet);
            }

            IList<System.Data.SqlClient.SqlParameter> paraList = 
                            new System.Collections.Generic.List<System.Data.SqlClient.SqlParameter>();
            string strSQL = "update qr_record set QRC012=0,QRC013=NULL";
            THC_Library.DataBase.DataBaseControl dbCtl = new THC_Library.DataBase.DataBaseControl();
            try
            {
                dbCtl.Open();                
                dbCtl.ExecuteCommad(strSQL, paraList);
                retJson = "{\"RESPONSE\" : \"DONE\"}";
            }
            catch (Exception ex)
            {
                retJson = "{\"RESPONSE\" : \"ERROR\"}";
            }
            finally
            {
                dbCtl.Close();
            }
            return Json(retJson, "application/json", JsonRequestBehavior.AllowGet);
        }
    }
}
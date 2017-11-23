using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;

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
            //string acc = "test@gmail.com";
            THC_Library.Error error;

            //Models.Member member = new Models.Member();
            //DataTable resultTable = member.getRewardGain("test@gmail.com", out error);
                        
            //if (error != null)
            //{
            //    return View(resultTable);
            //}
            //else
            //{                
            //    return View(resultTable);
            //}


            if (Session["tk"] != null)
            {
                Models.AppSession appSession = (Models.AppSession)Session["tk"];
                ViewBag.ACC = appSession.Account;

                Models.Member member = new Models.Member();
                DataTable resultTable = member.getRewardGain(appSession.Account, out error);

                if (error == null)
                {
                    return View(resultTable);
                }
                else
                {
                    @ViewBag.NUMBER = error.Number;
                    @ViewBag.ERROR = error.ErrorMessage;
                    return View("../Error/SystemError");

                }
            }
            else
            {
                return RedirectToAction("", "App");
                //return View("../Error/NotAllow");
            }            
        }

        public ActionResult WinDesc(string activity , string page)
        {
            //ViewBag.ACTIVITY = activity;
            //ViewBag.PAGE = page;
            //return View();

            if (Session["tk"] != null)
            {
                ViewBag.ACTIVITY = activity;
                ViewBag.PAGE = page;
                return View();
            }
            else
            {
                return RedirectToAction("", "App");
                //return View("../Error/NotAllow");
            }            
        }

        public ActionResult Activities()
        {           
            THC_Library.Error error;

            //int EventNo = 1037;
            //if (EventNo < 0)
            //{
            //    Models.Activity activity = new Models.Activity();
            //    DataTable resultTable = activity.getActivities(out error);

            //    if (error == null)
            //    {
            //        return View(resultTable);
            //    }
            //    else
            //    {
            //        @ViewBag.NUMBER = error.Number;
            //        @ViewBag.ERROR = error.ErrorMessage;
            //        return View("../Error/SystemError");
            //    }
            //}
            //else
            //{
            //    return RedirectToAction("Activity", "App");
            //}

            if (Session["tk"] != null)
            {
                Models.AppSession appSession = (Models.AppSession)Session["tk"];
                if (appSession.EventNo < 0)
                {
                    Models.Activity activity = new Models.Activity();
                    DataTable resultTable = activity.getActivities(out error);

                    if (error == null)
                    {
                        return View(resultTable);
                    }
                    else
                    {
                        @ViewBag.NUMBER = error.Number;
                        @ViewBag.ERROR = error.ErrorMessage;
                        return View("../Error/SystemError");
                    }
                }
                else
                {
                    return RedirectToAction("Activity", "App");
                }
            }
            else
            {
                return RedirectToAction("", "App");
            }            
        }

        public ActionResult Activity()
        {
            THC_Library.Error error;
            string strPage;
            
            //Models.Activity activity = new Models.Activity();
            //activity.getActivity(1037, out strPage, out error);

            //if (error == null)
            //{
            //    ViewBag.PAGE = strPage;
            //    return View();
            //}
            //else
            //{
            //    @ViewBag.NUMBER = error.Number;
            //    @ViewBag.ERROR = error.ErrorMessage;
            //    return View("../Error/SystemError");
            //}

            if (Session["tk"] != null)
            {
                Models.AppSession appSession = (Models.AppSession)Session["tk"];

                Models.Activity activity = new Models.Activity();
                activity.getActivity(appSession.EventNo, out strPage, out error);

                if (error == null)
                {
                    ViewBag.PAGE = strPage;
                    return View();
                }
                else
                {
                    @ViewBag.NUMBER = error.Number;
                    @ViewBag.ERROR = error.ErrorMessage;
                    return View("../Error/SystemError");
                }
            }
            else
            {
                return RedirectToAction("", "App");
            }
        }

        public ActionResult ActivityDesc(string page)
        {
            //ViewBag.PAGE = page;
            //return View();

            if (Session["tk"] != null)
            {
                ViewBag.PAGE = page;
                return View();
            }
            else
            {
                return RedirectToAction("", "App");
            }            
        }

        public ActionResult UsrInfo()
        {
            THC_Library.Error error;
            //Models.Member m = new Models.Member();
            //Models.AccountInfo acc = m.getAccountInfo("Test@gmail.com", "636461981531234989", out error);

            //if (error == null)
            //{
            //    ViewBag.TICKET = "636461981531234989";
            //    ViewBag.ACC = "Test@gmail.com";
            //    ViewBag.MOBIL = acc.Mobil;
            //    ViewBag.GENDER = acc.Gender;
            //    ViewBag.AGE = acc.Age;
            //    ViewBag.IID = acc.IId;
            //    ViewBag.ADDRESS = acc.Address;
            //    if (acc.Image != null)
            //    {
            //        ViewBag.IMAGE = "data:image/png;base64," + Convert.ToBase64String(acc.Image, 0, acc.Image.Length);
            //    }
            //    else
            //    {
            //        ViewBag.IMAGE = null;
            //    }

            //    return View();
            //}
            //else
            //{
            //    return View("Index");
            //}

            if (Session["tk"] != null)
            {
                Models.AppSession appSession = (Models.AppSession)Session["tk"];
                ViewBag.ACC = appSession.Account;

                Models.Member member = new Models.Member();
                Models.AccountInfo acc = member.getAccountInfo(appSession.Account, appSession.Ticket.ToString(), out error);

                if (error == null)
                {
                    ViewBag.TICKET = appSession.Ticket.ToString();
                    ViewBag.ACC = appSession.Account;
                    ViewBag.MOBIL = acc.Mobil;
                    ViewBag.GENDER = acc.Gender;
                    ViewBag.AGE = acc.Age;
                    ViewBag.IID = acc.IId;
                    ViewBag.ADDRESS = acc.Address;
                    if (acc.Image != null)
                    {
                        ViewBag.IMAGE = "data:image/png;base64," + Convert.ToBase64String(acc.Image, 0, acc.Image.Length);
                    }
                    else
                    {
                        ViewBag.IMAGE = null;
                    }
                    return View();
                }
                else
                {
                    @ViewBag.NUMBER = error.Number;
                    @ViewBag.ERROR = error.ErrorMessage;
                    return View("../Error/SystemError");
                }
            }
            else
            {
                return RedirectToAction("login", "App");
            }                       
        }       
       
        public ActionResult Question()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Login()
        {
            if (Session["tk"] != null)
            {
                return View("Index");
            }

            var acc = Request.Form["txt_account"];
            var pwd = Request.Form["txt_pwd"];

            if (String.IsNullOrEmpty(acc) && String.IsNullOrEmpty(pwd))
            {
                return View();
            }
            else
            {
                if (String.IsNullOrEmpty(acc) && !String.IsNullOrEmpty(pwd))
                {
                    ViewBag.ERROR = "請輸入帳號";
                    return View();
                }
                else if (!String.IsNullOrEmpty(acc) && String.IsNullOrEmpty(pwd))
                {
                    ViewBag.ERROR = "請輸入密碼";
                    return View();
                }
                else
                {
                    THC_Library.Error error;
                    int state;
                    Models.Member member = new Models.Member();
                    long newTicket = member.verifyAccount(acc, pwd, out state, out error);

                    if (error != null)
                    {
                        ViewBag.ERROR = "系統錯誤";
                        return View();
                    }
                    else
                    {
                        if (newTicket == -1)
                        {
                            if (state == 1)
                            {
                                ViewBag.ERROR = "帳號不存在";
                            }
                            else if (state == 2)
                            {
                                ViewBag.ERRORe = "密碼錯誤";
                            }
                            else
                            {
                                ViewBag.ERROR = "登入錯誤";
                            }
                            ViewBag.ACC = acc;
                            return View();
                        }
                        else
                        {
                            Models.AppSession appSession = new Models.AppSession();
                            appSession.Account = acc;
                            appSession.EventNo = -1;
                            appSession.Ticket = newTicket;
                            Session["tk"] = appSession;
                            ViewBag.ACC = acc;
                            ViewBag.TICKET = newTicket;

                            return RedirectToAction("", "App");
                            //Response.Redirect("/");
                            //return View("Index");
                        }
                    }
                }               
            }
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
    }
}
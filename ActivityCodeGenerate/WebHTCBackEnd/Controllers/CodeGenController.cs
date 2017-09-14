using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;

namespace WebHTCBackEnd.Controllers
{
    public class CodeGenController : Controller
    {
        // GET: CodeGen
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CodeGenData()
        {
            Error.Error error = null;
            string[] top10Events;

            WebHTCBackEnd.Models.Events.THC_EventCodeGen objCodeGen = new Models.Events.THC_EventCodeGen();
            objCodeGen.queryEventCodeGen(out top10Events, out error);

            if (error != null)
            {
                //ERROR Page
                return View();
            }
            else
            {
                DataTable codegenData = null;
                var lanSet = new Language.Event_Code_Gen();
                lanSet.CurrentZone = Language.LanguageBase.CURRENT_LANGUAGE;
                ViewData["lan"] = lanSet;
                ViewBag.product_types = classes.ProductType.GetProductType(Language.LanguageBase.CURRENT_LANGUAGE);                
                ViewBag.TOP10_EVENTS = top10Events;
                return View(codegenData);
            }            
        }

        public ActionResult CodeGenSearch(string event_no, string event_name)
        {
            Error.Error error = null;
            string strEventKey, strEventNo, strEventName, strVenderNo, strVenderName;
            string[] top10Events;

            WebHTCBackEnd.Models.Events.THC_EventCodeGen objCodeGen = new Models.Events.THC_EventCodeGen();
            DataTable codegenData = objCodeGen.queryEventCodeGenSearch(event_no, event_name, out strEventKey, out strEventNo, 
                                                out strEventName, out strVenderNo, out strVenderName, out top10Events,
                                                out error);

            if (error != null)
            {
                //ERROR Page
                return View();
            }
            else
            {
                var lanSet = new Language.Event_Code_Gen();
                lanSet.CurrentZone = Language.LanguageBase.CURRENT_LANGUAGE;
                ViewData["lan"] = lanSet;               
                ViewBag.s_event_no = event_no;
                ViewBag.s_event_name = event_name;
                ViewBag.EVENT_KEY = strEventKey;
                ViewBag.EVENT_NO = strEventNo;
                ViewBag.EVENT_NAME = strEventName;
                ViewBag.VENDER_NO = strVenderNo;
                ViewBag.VENDER_NAME = strVenderName;
                ViewBag.TOP10_EVENTS = top10Events;
                ViewBag.product_types = classes.ProductType.GetProductType(Language.LanguageBase.CURRENT_LANGUAGE);                
                return View("CodeGenData", codegenData);
            }
            
        }

        [HttpPost]
        public JsonResult GetCodeGenList(string event_key)
        {
            Error.Error error = null;
            WebHTCBackEnd.Models.Events.THC_EventCodeGen objCodeGen = new Models.Events.THC_EventCodeGen();
            DataTable codegenData = objCodeGen.queryEventCodeGenList(event_key, out error);

            string retJson;
            if (error != null)
            {
                retJson = Newtonsoft.Json.JsonConvert.SerializeObject(error);
            }
            else
            {
                retJson = Newtonsoft.Json.JsonConvert.SerializeObject(codegenData);
            }
            return Json(retJson, "application/json", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SingleCodeGenData(string key)
        {
            Error.Error error = null;
            WebHTCBackEnd.Models.Events.THC_EventCodeGen objCodeGen = new Models.Events.THC_EventCodeGen();
            DataTable codegenData = objCodeGen.queryEventCodeGenByKey(key, out error);

            string retJson;
            if (error != null)
            {
                retJson = Newtonsoft.Json.JsonConvert.SerializeObject(error);
            }
            else
            {
                retJson = Newtonsoft.Json.JsonConvert.SerializeObject(codegenData);
            }
            return Json(retJson, "application/json", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult NewEventCodeGen()
        {
            var EventNo = Request.Form["txt_event_no"];
            var EventName = Request.Form["lbl_event_name"];
            var GenDateTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"); //Request.Form["txt_gen_datetime"];
            var ProductType = Request.Form["txt_ptype"];
            var PONumber = Request.Form["txt_po_number"];
            var SerialNumber = Request.Form["txt_serial_number"];
            var GenNumber = Request.Form["txt_gen_number"];
            var Employee = Request.Form["txt_emplyee"];          

            Error.Error error = null;
            WebHTCBackEnd.Models.Events.THC_EventCodeGen objCodeGen = new Models.Events.THC_EventCodeGen();
            int iId = objCodeGen.addNewEventCodeGen(EventNo, EventName, GenDateTime, ProductType, PONumber, SerialNumber, GenNumber, Employee, out error);
            
            string retJson;
            if (error != null)
            {
                retJson = Newtonsoft.Json.JsonConvert.SerializeObject(error);
            }
            else
            {
                retJson = string.Format("[{{ \"NewId\" : {0} , \"GenDateTime\" : \"{1}\" }}]", iId, GenDateTime);
            }

            return Json(retJson, "application/json", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult UpdateEventCodeGen()
        {
            var EventKey = Request.Form["key"];
            var EventNo = Request.Form["txt_event_no"];
            var EventName = Request.Form["lbl_event_name"];           
            var ProductType = Request.Form["txt_ptype"];
            var PONumber = Request.Form["txt_po_number"];
            var SerialNumber = Request.Form["txt_serial_number"];
           
            Error.Error error = null;
            WebHTCBackEnd.Models.Events.THC_EventCodeGen objCodeGen = new Models.Events.THC_EventCodeGen();
            int iAffectedCount = objCodeGen.updateEventCodeGen(EventKey, ProductType, PONumber, SerialNumber, out error);

            string retJson;
            if (error != null)
            {
                retJson = Newtonsoft.Json.JsonConvert.SerializeObject(error);
            }
            else
            {
                retJson = WebHTCBackEnd.Utility.JsonResponeUtility.ResultOK();
            }

            return Json(retJson, "application/json");
        }

        [HttpPost]
        public JsonResult GenerateCode(string key)
        {
            Error.Error error = null;
            
            WebHTCBackEnd.Models.Events.THC_EventCodeGen objCodeGen = new Models.Events.THC_EventCodeGen();
            DataTable codegenData = objCodeGen.queryEventCodeGenByKey(key, out error);

            string retJson;
            if (error != null)
            {
                retJson = Newtonsoft.Json.JsonConvert.SerializeObject(error);
               
            }
            else
            {
                //if (codegenData.Rows.Count == 1)
                {
                    string strEventKey = codegenData.Rows[0]["AE001"].ToString();
                    string strEventName = codegenData.Rows[0]["AE003"].ToString();
                    int iQRBit = int.Parse(codegenData.Rows[0]["AE010"].ToString());
                    int iSerialBit = int.Parse(codegenData.Rows[0]["AE011"].ToString());
                    int iLen = int.Parse(codegenData.Rows[0]["AE018"].ToString());
                    int iTotalQty = int.Parse(codegenData.Rows[0]["EQCH007"].ToString());
                    int iSerial = int.Parse(codegenData.Rows[0]["AE017"].ToString()) + 1;


                    THC_CodeTypeLib.CodeGenProxy codeProxy = (THC_CodeTypeLib.CodeGenProxy)
                        Activator.GetObject(typeof(THC_CodeTypeLib.CodeGenProxy),
                        "tcp://127.0.0.1:8000/RemoteCodeGen");
                    string strId = codeProxy.CodeGenerate(strEventName, strEventKey, key, iLen, iSerialBit, iQRBit, iTotalQty, iSerial);

                    retJson = string.Format("[{{ \"STATE\" : \"{0}\", \"ID\" : \"{1}\" }}]", 
                                        "RUN", strId);
                }
            }

            return Json(retJson, "application/json", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetCodeState(string id)
        {
            THC_CodeTypeLib.CodeGenState codeState;
            try
            {
                THC_CodeTypeLib.CodeGenProxy codeProxy = (THC_CodeTypeLib.CodeGenProxy)
                       Activator.GetObject(typeof(THC_CodeTypeLib.CodeGenProxy),
                       "tcp://127.0.0.1:8000/RemoteCodeGen");
                codeState = codeProxy.GetCondeGenState(id);
            }
            catch (Exception ex)
            {
                codeState = new THC_CodeTypeLib.CodeGenState();                
                codeState.HasError = true;
                codeState.State = ex.ToString();                
            }            
            string retJson = Newtonsoft.Json.JsonConvert.SerializeObject(codeState);
            return Json(retJson, "application/json", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DeleteEventCodeGen(string event_no)
        {
            Error.Error error = null;
            WebHTCBackEnd.Models.Events.THC_Event objEvent = new Models.Events.THC_Event();
            objEvent.deleteEvent(event_no, out error);

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
        public ActionResult GenDownloadCode(string event_key, string id)
        {
            var EventKey = Request.Form["event_key"];
            var Id = Request.Form["id"];

            Error.Error error;
            string eventURL;

            WebHTCBackEnd.Models.Events.THC_EventCodeGen objCodeGen = new Models.Events.THC_EventCodeGen();
            DataTable codeTable = objCodeGen.downloadCode(EventKey, id, out eventURL, out error);

            string retJson;
            if (error != null)
            {
                retJson = Newtonsoft.Json.JsonConvert.SerializeObject(error);
                return Json(retJson, "application/json", JsonRequestBehavior.AllowGet);
            }
            else
            {
                if (eventURL.EndsWith("/"))
                {
                    eventURL = eventURL.Substring(0, eventURL.Length - 1);
                }
                string strFileName = Guid.NewGuid().ToString() + ".txt";
                System.IO.StreamWriter writer = null;
                try
                {                    
                    string tmpFileName = string.Format("{0}\\{1}",
                                        Server.MapPath("~/codefiles"), //Server.MapPath("/codefiles"),
                                        strFileName);
                    writer = new System.IO.StreamWriter(tmpFileName, false);
                    foreach (DataRow codeRow in codeTable.Rows)
                    {
                        writer.WriteLine(string.Format("{0}/{1}{2}{3}", eventURL, codeRow["EQC003"], 
                                        codeRow["EQC004"], codeRow["EQC005"]));
                    }
                    writer.Close();

                    /*
EQC001	nvarchar(20)	活動發碼代號 PK	
EQC002	Int	序號         	
EQC003	varchar(10)	QR CODE 亂碼 pk	
EQC004	Char(1)	序號補碼	
EQC005	Char(1)	QR補碼	
EQC006	varchar (10)	產生日期 	yyyy/MM/dd

                     */
                }
                catch (Exception ex)
                {
                    error = new Error.Error();
                    error.ErrorMessage = ex.ToString();
                }

                if (error != null)
                {
                    retJson = Newtonsoft.Json.JsonConvert.SerializeObject(error);
                    return Json(retJson, "application/json", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    //retJson = Utility.JsonResponeUtility.ResultOK();
                    retJson = string.Format("[{{ \"FILE\" : \"{0}\" }}]", strFileName);
                }
                
            }  
            return Json(retJson, "application/json", JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult DownloadCode(string event_no, string id, string filename)
        {
            string tmpFileName = string.Format("{0}\\{1}",
                                       Server.MapPath("~/codefiles"),//Server.MapPath("/codefiles"),
                                       filename);
            string strDownloadName = string.Format("{0}_{1}.txt", event_no, id);
            return File(tmpFileName, "text/plain", strDownloadName);          
        }
       
    }
}
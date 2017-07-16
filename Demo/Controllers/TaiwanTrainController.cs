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
            string strLink = GetCodeInfo(id);
            ViewBag.id = id;

            ViewBag.Link = strLink;
            return View();
            //if (!string.IsNullOrEmpty(id) && id == "67ED2F")
            //{
            //    string strLink = GetCodeInfo(id);

            //    ViewBag.id = id;
            //    return View();
            //}
            //else
            //{
            //    Response.Redirect("http://www.tr.net.tw/");
            //    return null;
            //}
        }

        private string GetCodeInfo(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                string strSQL = "select * from codes where cid='" + id + "'";
                DB.DataBaseControl dbCtl = new DB.DataBaseControl();
                dbCtl.Open();
                System.Data.IDataReader dataReader = dbCtl.GetReader(strSQL, null);
                string strResult = "";
                if (dataReader.Read())
                {
                    strResult = dataReader["link"].ToString().Trim();
                }
                dataReader.Close();
                dbCtl.Close();
                return strResult;
            }
            else
            {
                return "";
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net;
using System.Collections.Specialized;
using System.IO;
using System.Security.Cryptography;

namespace THC_Library
{
    public class APPCURL
    {

        public static string APP_ENDPOINT
        {
            get;
            set;
        }

        public static string newAccount(string account, string mail, string mobil, string pwd)
        {
            const string METHOD = "Members/THC_Member_01";
            string strData = string.Format("acc={0}&mail={1}&mobil={2}&pwd={3}",
                        account, mail, mobil, pwd);

            APP_ENDPOINT = "http://localhost:51323/" + METHOD;
            return SendPost(strData);

        }

        public static string VerifyAccount(string mail, string pwd)
        {
            const string METHOD = "Members/THC_Member_02";
            string strData = string.Format("mail={0}&pwd={1}",
                              mail, pwd);
            APP_ENDPOINT = "http://localhost:51323/" + METHOD;
            return SendPost(strData);
        }

        public static string VerifyAccount_FB(string ac, string code, string fb, string name, string gender)
        {
            const string METHOD = "Members/THC_Member_03";
            string strData = string.Format("ac={0}&code={1}&fb={2}&name={3}&gender={4}",
                              ac, code, fb, name, gender);
            APP_ENDPOINT = "http://localhost:51323/" + METHOD;
            return SendPost(strData);
        }

        public static string RenewAccountInfo(string ml, string tk, string m, string g, string a, string iid, string addr)
        {
            //var reqMail = Request.Form["ml"];
            //var reqTicket = Request.Form["tk"];
            //var reqMobil = Request.Form["m"];
            //var reqGender = Request.Form["g"];
            //var reqAge = Request.Form["a"];
            //var reqIId = Request.Form["iid"];
            //var reqAddr = Request.Form["addr"];

            const string METHOD = "Members/THC_Member_04";
            string strData = string.Format("ml={0}&tk={1}&m={2}&g={3}&a={4}&iid={5}&addr={6}",
                              ml, tk, m, g, a, iid, addr);
            APP_ENDPOINT = "http://localhost:51323/" + METHOD;
            return SendPost(strData);
        }

        public static string GetAccountInfo(string acc, string tk)
        {
            const string METHOD = "Members/THC_Member_05";
            string strData = string.Format("acc={0}&tk={1}", acc, tk);
            APP_ENDPOINT = "http://localhost:51323/" + METHOD;
            return SendPost(strData);
        }

        public static void ScanRecord()
        {

        }

        private static string SendPost(string values)
        {                        
            using (var client = new WebClient())
            {
                var bytes = Encoding.UTF8.GetBytes(values);

                string strTick = DateTime.Now.Ticks.ToString();
                string strEncry = Encry(strTick);

                client.Headers.Add("QPID-TICK", strTick);
                client.Headers.Add("QPID-DATA", strEncry);
                client.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                var respData = client.UploadData(APP_ENDPOINT, "POST", bytes);
                return Encoding.UTF8.GetString(respData);
            }
        }

        public static string Encry(string source)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] key = Encoding.ASCII.GetBytes("Adrf732V");
            byte[] iv = Encoding.ASCII.GetBytes("xHue73Ki");
            byte[] dataByteArray = Encoding.UTF8.GetBytes(source);

            des.Key = key;
            des.IV = iv;
            string encrypt = "";
            using (MemoryStream ms = new MemoryStream())
            using (CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write))
            {
                cs.Write(dataByteArray, 0, dataByteArray.Length);
                cs.FlushFinalBlock();
                encrypt = Convert.ToBase64String(ms.ToArray());
            }
            return encrypt;
        }

    }
}

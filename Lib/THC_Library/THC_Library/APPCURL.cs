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

        private static string APP_ENDPOINT;

        public static string AUTH_KEY = "{51360D11-002F-4455-88BB-ECFE1FDE747F}";

        public static string APP_URL
        {
            get;
            set;
        }

        public static string newAccount(string account, string mail, string mobil, string pwd, string gender, string age)
        {
            const string METHOD = "Members/THC_Member_01";
            string strData = string.Format("acc={0}&mail={1}&mobil={2}&pwd={3}&gender={4}&age={5}",
                        account, mail, mobil, pwd, gender, age);

            APP_ENDPOINT = APP_URL + METHOD;
            return SendPost(strData);

        }

        public static string VerifyAccount(string mail, string pwd)
        {
            const string METHOD = "Members/THC_Member_02";
            string strData = string.Format("mail={0}&pwd={1}",
                              mail, pwd);
            APP_ENDPOINT = APP_URL + METHOD;
            return SendPost(strData);
        }

        public static string VerifyAccountWithInfo(string mail, string pwd)
        {
            const string METHOD = "Members/THC_Member_02_1";
            string strData = string.Format("mail={0}&pwd={1}",
                              mail, pwd);
            APP_ENDPOINT = APP_URL + METHOD;
            return SendPost(strData);
        }

        public static string VerifyAccount_FB(string ac, string code, string fb, string name, string gender)
        {
            const string METHOD = "Members/THC_Member_03";
            string strData = string.Format("ac={0}&code={1}&fb={2}&name={3}&gender={4}",
                              ac, code, fb, name, gender);
            APP_ENDPOINT = APP_URL + METHOD;
            return SendPost(strData);
        }

        public static string RenewAccountInfo(string ml, string tk, string m, string iid, string addr)
        {
            //var reqMail = Request.Form["ml"];
            //var reqTicket = Request.Form["tk"];
            //var reqMobil = Request.Form["m"];
            //var reqGender = Request.Form["g"];
            //var reqAge = Request.Form["a"];
            //var reqIId = Request.Form["iid"];
            //var reqAddr = Request.Form["addr"];

            const string METHOD = "Members/THC_Member_04";
            string strData = string.Format("ml={0}&tk={1}&m={2}&iid={3}&addr={4}",
                              ml, tk, m, iid, addr);
            APP_ENDPOINT = APP_URL + METHOD;
            return SendPost(strData);
        }

        public static string RenewAccountMobil(string ml, string tk, string m)
        {
            const string METHOD = "Members/THC_Member_04_m";
            string strData = string.Format("ml={0}&tk={1}&m={2}", ml, tk, m);
            APP_ENDPOINT = APP_URL + METHOD;
            return SendPost(strData);
        }

        public static string GetAccountInfo(string acc, string tk)
        {
            const string METHOD = "Members/THC_Member_05";
            string strData = string.Format("acc={0}&tk={1}", acc, tk);
            APP_ENDPOINT = APP_URL + METHOD;
            return SendPost(strData);
        }

        public static string GetAccountInfoByAuthorizeKey(string acc, string key)
        {
            if (key != AUTH_KEY)
            {
                throw new Exception("Invaild Code");
            }
            const string METHOD = "Members/THC_Member_08";
            string strData = string.Format("acc={0}", acc);
            APP_ENDPOINT = APP_URL + METHOD;
            return SendPost(strData);
        }

        public static string ScanRecord(string eventkey, string qrcode, string date, string account,
                                string age, string gender, string area, string temp, string weather,
                               string lat, string lng, string rwdtype, string tk)
        {
            //EUR001
            //EUR002
            //EUR003
            //EUR004
            //EUR005
            //EUR006
            //EUR007
            //EUR008
            //EUR009
            //EUR010
            //EUR011
            //EUR012
            //EUR015

            const string METHOD = "Members/THC_Member_06";
            string strData = string.Format("eventkey={0}&code={1}&date={2}&acc={3}&age={4}&gender={5}&area={6}&temp={7}&weather={8}&lat={9}&lng={10}&rwdtype={11}&tk={12}",
                             eventkey, qrcode, date, account, age, gender, area, temp, weather, lat, lng, rwdtype, tk);
            APP_ENDPOINT = APP_URL + METHOD;
            return SendPost(strData);

        }


        public static string ScanRecord(string eventkey, string qrcode, string date, string account,
                                string age, string gender, string area, string temp, string weather,
                                string lat, string lng, string rewardname, string ec, string rwdtype,
                                string windesc, string tk)
        {
            //EUR001
            //EUR002
            //EUR003
            //EUR004
            //EUR005
            //EUR006
            //EUR007
            //EUR008
            //EUR009
            //EUR010
            //EUR011
            //EUR012
            //EUR013
            //EUR014
            //EUR015
            //EUR016

            const string METHOD = "Members/THC_Member_06";
            string strData = string.Format("eventkey={0}&code={1}&date={2}&acc={3}&age={4}&gender={5}&area={6}&temp={7}&weather={8}&lat={9}&lng={10}&rwd={11}&ec={12}&rwdtype={13}&windesc={14}&tk={15}",
                             eventkey, qrcode, date, account, age, gender, area, temp, weather, lat, lng, rewardname, ec, rwdtype, windesc, tk);
            APP_ENDPOINT = APP_URL + METHOD;
            return SendPost(strData);

        }


        public static string RequestResetPassword(string account)
        {          
            const string METHOD = "Members/ForgestPassword";
            string strData = string.Format("ml={0}", account);
            APP_ENDPOINT = APP_URL + METHOD;
            return SendPost(strData);
        }


        public static string AnscyActivity(string activity)
        {
            const string METHOD = "Backend/THC_AnsyActivity";
            string strData = string.Format("activity={0}", activity);
            APP_ENDPOINT = APP_URL + METHOD;
            return SendPost(strData);
            
        }

        public static string ClearRecordLogActivity(string activity)
        {
            const string METHOD = "Backend/THC_ClearLogActivity";
            string strData = string.Format("activity={0}", activity);
            APP_ENDPOINT = APP_URL + METHOD;
            return SendPost(strData);
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

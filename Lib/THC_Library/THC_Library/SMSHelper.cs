using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Collections;
using System.IO;

namespace THC_Library
{
    public class SMSHelper
    {
        public static bool SendTo(string account, string mobil, string content)
        {
            //宣告簡訊內容參數
            string postData = "";
            //使用者帳號
            string username = "58176511";
            //使用者密碼
            string password = "Aa123456Bb";

            //// 三竹 Http 發送 URL
            string URL = "http://smexpress.mitake.com.tw/SmSendPost.asp?username=" + username + "&password=" + password;

            HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(URL);
            myRequest.UserAgent = "Mozilla/5.0 (Windows; U; Windows NT 6.1; en-US) AppleWebKit/533.4 (KHTML, like Gecko) Chrome/5.0.375.126 Safari/533.4";
            CookieContainer cCookie = new CookieContainer();
            myRequest.CookieContainer = cCookie;
            myRequest.ContentType = "application/x-www-form-urlencoded";
            myRequest.Accept = "application/xml,application/xhtml+xml,text/html;q=0.9,text/plain;q=0.8,image/png,*/*;q=0.5";

            postData = "[102]\r\n" + "DestName=" + account + "\r\n" + "dstaddr=" + mobil + "\r\n" + "smbody=" + content + "\r\n";
            //postData = postData + "[103]\r\n" + "DestName=三寶\r\n" + "dstaddr=0999999999\r\n" + "smbody=我是測試3\r\n";
            
            byte[] bData = System.Text.Encoding.GetEncoding("Big5").GetBytes(postData);


            myRequest.Method = "POST";
            myRequest.ContentLength = bData.Length;

            try
            {
                Stream oStream = myRequest.GetRequestStream();
                oStream.Write(bData, 0, bData.Length);

                string oResp = string.Empty;

                using (var resp = myRequest.GetResponse())
                {
                    using (var responseStream = resp.GetResponseStream())
                    {
                        using (var responseReader = new StreamReader(responseStream))
                        {
                            oResp = responseReader.ReadToEnd();
                        }
                    }
                }
                //HttpContext.Current.Response.Write(oResp);

                oStream.Close();               
                return true;
            }
            catch (Exception ex)
            {               
                //HttpContext.Current.Response.Write("Respnse Timeout");
                return false;
            }
        }
    }
}

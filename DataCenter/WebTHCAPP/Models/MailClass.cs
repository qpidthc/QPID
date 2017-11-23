using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;

namespace WebTHCAPP.Models
{
    public class MailClass
    {
        public void Send(string acc, string access_code, string mydomain, out THC_Library.Error error)
        {
            error = null;
            MailMessage mms = new MailMessage();            
            mms.From = new MailAddress("qpid0421@gmail.com");           
            mms.Subject = "THC宏全QPID密碼變更通知";
            //信件內容
            mms.Body =  "請點選以下連結修改密碼<br/>" +
                        string.Format("{0}WebTHCApp/Members/AccessRestPassword?acc={1}&access={2}", mydomain, acc, access_code);
            mms.IsBodyHtml = true;
            mms.BodyEncoding = System.Text.Encoding.UTF8;

            //mms.To.Add("tungken@gmail.com");
            mms.To.Add(acc);

            using (SmtpClient client = new SmtpClient("smtp.gmail.com", 587))
            {
                try
                {
                    client.EnableSsl = true;
                    client.UseDefaultCredentials = false;
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    client.Credentials = new System.Net.NetworkCredential("qpid0421@gmail.com", "Qpid_58176511"); //Qpid58176511
                    client.Send(mms);
                }
                catch (Exception ex)
                {
                    error = new THC_Library.Error();
                    error.Number = 900;
                    error.ErrorMessage = "發信服務系統錯誤";
                }
                
            }
        }
    }
}
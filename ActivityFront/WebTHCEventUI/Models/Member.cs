using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using THC_Library;

namespace WebTHCEventUI.Models
{
    public class Member
    {
        public bool checkExits(string mail, out Error error)
        {
            error = null;

            return false;
        }

        public long verifyAccount(string mail, string pwd, out Error error)
        {
            error = null;
            long lgTimestamp = -1;

            try
            {
                string jsonString = THC_Library.APPCURL.VerifyAccount(mail, pwd);
                dynamic jsonResult = Newtonsoft.Json.JsonConvert.DeserializeObject(jsonString);

                if (jsonResult.Number == 0)
                {
                    if (jsonResult.Verify == 1)
                    {
                        string ticket = jsonResult.Ticket;
                        long.TryParse(ticket, out lgTimestamp);

                    }
                    else
                    {
                        error = new Error();
                        error.Number = jsonResult.Number;
                        error.ErrorMessage = jsonResult.ErrorMessage;
                    }
                }
                else
                {
                    error = new Error();
                    error.Number = jsonResult.Number;
                    error.ErrorMessage = jsonResult.ErrorMessage;
                }               
            }
            catch (Exception ex)
            {
                error = new Error();
                error.Number = 100;
                error.ErrorMessage = "系統錯誤";
            }
            
            return lgTimestamp;
        }

        public int newAccount(string acccount, string mail, string mobil, string pwd, out long timestamp, out Error error)
        {
            error = null;
            timestamp = -1;

            try
            {
                string jsonString = THC_Library.APPCURL.newAccount(acccount, mail, mobil, pwd);
                dynamic jsonResult = Newtonsoft.Json.JsonConvert.DeserializeObject(jsonString);

                if (jsonResult.Number == 0)
                {
                    string strTicket = jsonResult.Ticket;
                    long.TryParse(strTicket, out timestamp);
                    string newKey = jsonResult.Addition;
                    int iaccKey = int.Parse(newKey);
                }
                else
                {
                    error = new Error();
                    error.Number = jsonResult.Number;
                    error.ErrorMessage = jsonResult.ErrorMessage;
                }
            }
            catch (Exception ex)
            {
                error = new Error();
                error.Number = 100;
                error.ErrorMessage = "系統錯誤";
            }

            return 0;
        }

        public int updateAccount(string acccount, string timestamp, string mobil, string gender, string age,
                            string iid, string addr, out Error error)
        {
            error = null;
            int iUpdateCount = 0;

            try
            {
                string jsonString = THC_Library.APPCURL.RenewAccountInfo(acccount, timestamp, mobil, gender, age, iid, addr);
                dynamic jsonResult = Newtonsoft.Json.JsonConvert.DeserializeObject(jsonString);

                if (jsonResult.Number == 0)
                {
                    if (jsonResult.Verify == 1)
                    {                       
                        string updateCount = jsonResult.Addition;
                        iUpdateCount = int.Parse(updateCount);
                    }
                    else
                    {
                        error = new Error();
                        error.Number = jsonResult.Number;
                        error.ErrorMessage = jsonResult.ErrorMessage;
                    }
                }
                else
                {
                    error = new Error();
                    error.Number = jsonResult.Number;
                    error.ErrorMessage = jsonResult.ErrorMessage;
                }
            }
            catch (Exception ex)
            {
                error = new Error();
                error.Number = 100;
                error.ErrorMessage = "系統錯誤";
            }

            //SqlParameter sqlParam;            
            //IList<SqlParameter> paraList = new System.Collections.Generic.List<SqlParameter>();
            //string strSQL = "update consumer_member set CM008=@CM008,CM009=@CM009,CM010=@CM010,CM012=@CM012,CM013=@CM013 " + 
            //                "where CM002=@CM002 and CM016=@CM016";
            ////CM008 手機 CM009 地址 CM010 身分證號 CM012 性別 CM013 年齡
            //THC_Library.DataBase.DataBaseControl dbCtl = new THC_Library.DataBase.DataBaseControl();
            //try
            //{
            //    paraList.Clear();
            //    sqlParam = new SqlParameter("@CM008", mobil);
            //    paraList.Add(sqlParam);
            //    if (addr == null)
            //    {
            //        sqlParam = new SqlParameter("@CM009", DBNull.Value);
            //    }
            //    else
            //    {
            //        sqlParam = new SqlParameter("@CM009", addr);
            //    }
            //    paraList.Add(sqlParam);
            //    if (iid == null)
            //    {
            //        sqlParam = new SqlParameter("@CM010", DBNull.Value);
            //    }
            //    else
            //    {
            //        sqlParam = new SqlParameter("@CM010", iid);
            //    }                
            //    paraList.Add(sqlParam);
            //    if (gender == null)
            //    {
            //        sqlParam = new SqlParameter("@CM012", DBNull.Value);
            //    }
            //    else
            //    {
            //        sqlParam = new SqlParameter("@CM012", gender);
            //    }
            //    paraList.Add(sqlParam);
            //    if (age == null)
            //    {
            //        sqlParam = new SqlParameter("@CM013", DBNull.Value);
            //    }
            //    else
            //    {
            //        sqlParam = new SqlParameter("@CM013", age);
            //    }                
            //    paraList.Add(sqlParam);
            //    sqlParam = new SqlParameter("@CM002", acccount);
            //    paraList.Add(sqlParam);
            //    sqlParam = new SqlParameter("@CM016", SqlDbType.BigInt);
            //    sqlParam.Value = long.Parse(timestamp);
            //    paraList.Add(sqlParam);

            //    dbCtl.Open();
            //    iUpdateCount = dbCtl.ExecuteCommad(strSQL, paraList);

            //}           
            //catch (Exception ex)
            //{
            //    error = new Error();
            //    error.Number = 100;
            //    error.ErrorMessage = "資料更新系統錯誤";
            //}
            //finally
            //{
            //    dbCtl.Close();
            //}
            return iUpdateCount;
        }

        public long verifyFaceBookAccount(string ac, string code, string mail, string name, string gender, out string account, out Error error)
        {
            error = null;
            account = "";
            long lgTimestamp = -1;
            //bool bHasAccount = false;
            //string strAcc = "";
            //string strMail = "";

            try
            {
                string jsonString = THC_Library.APPCURL.VerifyAccount_FB(ac, code, mail, name, gender);
                dynamic jsonResult = Newtonsoft.Json.JsonConvert.DeserializeObject(jsonString);

                if (jsonResult.Number == 0)
                {
                    if (jsonResult.Verify == 1)
                    {
                        account = jsonResult.Acc;
                        string strTicket = jsonResult.Ticket;
                        long.TryParse(strTicket, out lgTimestamp);                      
                    }
                    else
                    {
                        error = new Error();
                        error.Number = jsonResult.Number;
                        error.ErrorMessage = jsonResult.ErrorMessage;
                    }
                }
                else
                {
                    error = new Error();
                    error.Number = jsonResult.Number;
                    error.ErrorMessage = jsonResult.ErrorMessage;
                }
            }
            catch (Exception ex)
            {
                error = new Error();
                error.Number = 100;
                error.ErrorMessage = "系統錯誤";
            }

            return lgTimestamp;
        }

        public bool requestForgetPassword(string acccount, out Error error)
        {
            error = null;
            bool bResult = false;
            
            try
            {
                string jsonString = THC_Library.APPCURL.RequestResetPassword(acccount);
                dynamic jsonResult = Newtonsoft.Json.JsonConvert.DeserializeObject(jsonString);

                if (jsonResult.Number == 0)
                {
                    bResult = true;
                }
                else
                {
                    error = new Error();
                    error.Number = jsonResult.Number;
                    error.ErrorMessage = jsonResult.ErrorMessage;
                }
            }
            catch (Exception ex)
            {
                error = new Error();
                error.Number = 100;
                error.ErrorMessage = "系統錯誤";
            }
            return bResult;
        }

    }
}
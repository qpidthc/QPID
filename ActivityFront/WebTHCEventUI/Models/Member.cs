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

            //SqlParameter sqlParam;
            //IDataReader dataReader;
            //IList<SqlParameter> paraList = new System.Collections.Generic.List<SqlParameter>();
            //string strSQL = "select CM002,CM017 from consumer_member where CM003=@CM003";

            //THC_Library.DataBase.DataBaseControl dbCtl = new THC_Library.DataBase.DataBaseControl();
            //try
            //{
            //    sqlParam = new SqlParameter("@CM003", mail);
            //    paraList.Add(sqlParam);

            //    dbCtl.Open();
            //    dataReader = dbCtl.GetReader(strSQL, paraList);
            //    if (dataReader.Read())
            //    {
            //        strAcc = dataReader["CM002"].ToString();
            //        strMail = dataReader["CM017"].ToString();
            //        bHasAccount = true;
            //    }                
            //    dataReader.Close();

            //    if (bHasAccount)
            //    {
            //        lgTimestamp = DateTime.Now.Ticks;
            //        strSQL = "update consumer_member set CM016=@CM016 where CM002=@CM002";
            //        paraList.Clear();
            //        sqlParam = new SqlParameter("@CM016", lgTimestamp);
            //        paraList.Add(sqlParam);
            //        sqlParam = new SqlParameter("@CM002", strAcc);
            //        paraList.Add(sqlParam);
            //        dbCtl.ExecuteCommad(strSQL, paraList);
            //        account = strAcc;
            //    }
            //    else
            //    {
            //        //以FB註冊會員資料
            //        strSQL = "insert into consumer_member (CM002,CM003,CM006,CM007,CM012,CM014,CM016,CM017) values " +
            //                "(@CM002,@CM003,@CM006,@CM007,@CM012,@CM014,@CM016,@CM017);SELECT CAST(scope_identity() AS int);";

            //        paraList.Clear();
            //        sqlParam = new SqlParameter("@CM002", mail);
            //        paraList.Add(sqlParam);
            //        sqlParam = new SqlParameter("@CM003", mail);
            //        paraList.Add(sqlParam);
            //        sqlParam = new SqlParameter("@CM006", name);
            //        paraList.Add(sqlParam);
            //        sqlParam = new SqlParameter("@CM007", "");
            //        paraList.Add(sqlParam);
            //        sqlParam = new SqlParameter("@CM012", gender);
            //        paraList.Add(sqlParam);
            //        sqlParam = new SqlParameter("@CM014", DateTime.Now);
            //        paraList.Add(sqlParam);
            //        lgTimestamp = DateTime.Now.Ticks;
            //        sqlParam = new SqlParameter("@CM016", lgTimestamp);
            //        paraList.Add(sqlParam);
            //        sqlParam = new SqlParameter("@CM017", SqlDbType.VarChar);
            //        sqlParam.Value = mail;
            //        paraList.Add(sqlParam);

            //        object accKey = dbCtl.ExecuteScalar(strSQL, paraList);
            //        int iaccKey = Convert.ToInt32(accKey);
            //        account = mail;
            //    }
            //}
            //catch (SqlException sqlEx)
            //{
            //    error = new Error();
            //    if (sqlEx.Number == 2601)
            //    {
            //        error.Number = 101;
            //        error.ErrorMessage = "帳號已註冊";
            //    }
            //    else
            //    {
            //        error.Number = 100;
            //        error.ErrorMessage = "系統錯誤";
            //    }
            //}
            //catch (Exception ex)
            //{
            //    error = new Error();
            //    error.Number = 100;
            //    error.ErrorMessage = "系統錯誤";
            //}
            //finally
            //{
            //    dbCtl.Close();
            //}

            return lgTimestamp;
        }

    }
}
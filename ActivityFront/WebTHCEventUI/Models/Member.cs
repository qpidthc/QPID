using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using THC_Library;
using THC_Library.DataBase;

namespace WebTHCEventUI.Models
{
    public class Member
    {
       
        public long verifyAccount(string mail, string pwd, out Error error)
        {
            error = null;
            long lgTimestamp = -1;

            try
            {
                bool bExisted = checkLocalAccount(mail, out error);

                string jsonString;
                dynamic jsonResult;
                if (bExisted)
                {
                    jsonString = THC_Library.APPCURL.VerifyAccount(mail, pwd);
                    jsonResult = Newtonsoft.Json.JsonConvert.DeserializeObject(jsonString);
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
                else
                {
                    jsonString = THC_Library.APPCURL.VerifyAccountWithInfo(mail, pwd);
                    jsonResult = Newtonsoft.Json.JsonConvert.DeserializeObject(jsonString);
                    if (jsonResult.Number == 0)
                    {
                        if (jsonResult.Verify == 1)
                        {
                            string ticket = jsonResult.Ticket;
                            long.TryParse(ticket, out lgTimestamp);
                            string strMobil = jsonResult.Mobil.ToString();
                            string strGender = jsonResult.Gender.ToString();
                            string strAge = jsonResult.Age.ToString();
                            newLocalAccount(mail, mail, strMobil, strGender, strAge, out error);
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
                                
               
            }
            catch (Exception ex)
            {
                error = new Error();
                error.Number = 100;
                error.ErrorMessage = "系統錯誤";
            }
            
            return lgTimestamp;
        }

        public int newAccount(string acccount, string mail, string mobil, string pwd,
                            string gender, string age, out long timestamp, out Error error)
        {
            error = null;
            timestamp = -1;

            try
            {
                string jsonString = THC_Library.APPCURL.newAccount(acccount, mail, mobil, pwd, gender, age);
                dynamic jsonResult = Newtonsoft.Json.JsonConvert.DeserializeObject(jsonString);

                if (jsonResult.Number == 0)
                {
                    string strTicket = jsonResult.Ticket;
                    long.TryParse(strTicket, out timestamp);
                    string newKey = jsonResult.Addition;
                    int iaccKey = int.Parse(newKey);
                    newLocalAccount(acccount, mail, mobil, gender, age, out error);

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

        public int updateAccount(string acccount, string timestamp, string mobil, string iid, string addr, out Error error)
        {
            error = null;
            int iUpdateCount = 0;

            try
            {
                string jsonString = THC_Library.APPCURL.RenewAccountInfo(acccount, timestamp, mobil, iid, addr);
                dynamic jsonResult = Newtonsoft.Json.JsonConvert.DeserializeObject(jsonString);

                if (jsonResult.Number == 0)
                {
                    if (jsonResult.Verify == 1)
                    {                       
                        string updateCount = jsonResult.Addition;
                        iUpdateCount = int.Parse(updateCount);

                        updateLocalAccount(acccount, mobil, iid, addr, out error);
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

            return iUpdateCount;
        }

        public int updateMobil(string acccount, string timestamp, string mobil, out Error error)
        {
            error = null;
            int iUpdateCount = 0;

            try
            {
                string jsonString = THC_Library.APPCURL.RenewAccountMobil(acccount, timestamp, mobil);
                dynamic jsonResult = Newtonsoft.Json.JsonConvert.DeserializeObject(jsonString);

                if (jsonResult.Number == 0)
                {
                    if (jsonResult.Verify == 1)
                    {
                        string updateCount = jsonResult.Addition;
                        iUpdateCount = int.Parse(updateCount);
                        updateLocalMobil(acccount, mobil, out error);
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

                        localFaceBookAccount(mail, name, gender, out error);
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
                error.Number = 150;
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

        public bool checkLocalAccount(string account, out Error error)
        {
            error = null;
            bool bExisted = false; 
          
            IList<SqlParameter> paraList = new System.Collections.Generic.List<SqlParameter>();
            string strSQL = "select CM002 from consumer_member where CM002=@CM002";
            paraList.Add(new  SqlParameter("@CM002", account));
            DataBaseControl dbCtl = new DataBaseControl();

            try
            {
                dbCtl.Open();
                IDataReader dataReader = dbCtl.GetReader(strSQL, paraList);
                if (dataReader.Read())
                {
                    bExisted = true;
                }
                dataReader.Close();                               

            }
            catch (Exception ex)
            {
                error = new THC_Library.Error();
                error.Number = THC_Library.THCException.SYSTEM_ERROR;
                error.ErrorMessage = ex.Message;
            }
            finally
            {
                dbCtl.Close();
            }

            return bExisted;
        }

        public void newLocalAccount(string acccount, string mail, string mobil, string gender, 
                    string age, out Error error)
        {           
            error = null;          
            SqlParameter sqlParam;
            /*
             CM001	Int	PK		1.0
CM002	varchar(50)	會員帳號	唯一 mail  	1.0
CM003	varchar (50)	FB帳號		1.0
CM004	varchar (50)	Line帳號		1.0
CM005	varchar (50)	Google帳號		1.0
CM006	nvarchar (20)	會員姓名		1.0
CM007	varchar(10)	手機號碼		1.0
CM008	nvarchar(200)	地址		1.0
CM009	varchar(10)	身分證號		1.0
CM010	char(1)	性別	‘’未填 0 女 1 男 2 彩虹 	1.0
CM011	char(1)	年齡	1 10-19 2 20-29 ……	1.0

             */
            IList<SqlParameter> paraList = new System.Collections.Generic.List<SqlParameter>();
            string strSQL = "insert into consumer_member (CM002,CM007,CM010,CM011) values " +
                            "(@CM002,@CM007,@CM010,@CM011);";

            THC_Library.DataBase.DataBaseControl dbCtl = new THC_Library.DataBase.DataBaseControl();
            try
            {               
                sqlParam = new SqlParameter("@CM002", acccount);
                paraList.Add(sqlParam);
                sqlParam = new SqlParameter("@CM007", mobil);
                if (mobil == null)
                    sqlParam.Value = DBNull.Value;
                else
                    sqlParam.Value = mobil;
                paraList.Add(sqlParam);
                sqlParam = new SqlParameter("@CM010", SqlDbType.Char);
                if (gender == null)
                    sqlParam.Value = "0";
                else
                    sqlParam.Value = gender;
                paraList.Add(sqlParam);
                sqlParam = new SqlParameter("@CM011", SqlDbType.Char);
                if (age == null)
                    sqlParam.Value = DBNull.Value;
                else
                    sqlParam.Value = age;
                paraList.Add(sqlParam);
               
                dbCtl.Open();
                dbCtl.ExecuteCommad(strSQL, paraList);                
            }
            catch (SqlException sqlEx)
            {
                error = new Error();
                if (sqlEx.Number == 2601)
                {
                    error.Number = 101;
                    error.ErrorMessage = "帳號已註冊";
                }
                else
                {
                    error.Number = 100;
                    error.ErrorMessage = "系統錯誤";
                }
            }
            catch (Exception ex)
            {
                error = new Error();
                error.Number = 100;
                error.ErrorMessage = "系統錯誤";
            }
            finally
            {
                dbCtl.Close();
            }           
        }

        public void updateLocalAccount(string acccount, string mobil, string iid, string addr, out Error error)
        {
            /*
         CM001	Int	PK		1.0
CM002	varchar(50)	會員帳號	唯一 mail  	1.0
CM003	varchar (50)	FB帳號		1.0
CM004	varchar (50)	Line帳號		1.0
CM005	varchar (50)	Google帳號		1.0
CM006	nvarchar (20)	會員姓名		1.0
CM007	varchar(10)	手機號碼		1.0
CM008	nvarchar(200)	地址		1.0
CM009	varchar(10)	身分證號		1.0
CM010	char(1)	性別	‘’未填 0 女 1 男 2 彩虹 	1.0
CM011	char(1)	年齡	1 10-19 2 20-29 ……	1.0

         */

            error = null;
            SqlParameter sqlParam;

            IList<SqlParameter> paraList = new System.Collections.Generic.List<SqlParameter>();
            string strSQL = "update consumer_member set CM007=@CM007,CM008=@CM008,CM009=@CM009 " +
                            "where CM002=@CM002;";

            THC_Library.DataBase.DataBaseControl dbCtl = new THC_Library.DataBase.DataBaseControl();
            try
            {  
                sqlParam = new SqlParameter("@CM007", mobil);
                if (mobil == null)
                    sqlParam.Value = DBNull.Value;
                else
                    sqlParam.Value = mobil;
                paraList.Add(sqlParam);
                sqlParam = new SqlParameter("@CM008", SqlDbType.NVarChar);
                if (addr == null)
                    sqlParam.Value = DBNull.Value;
                else
                    sqlParam.Value = addr;
                paraList.Add(sqlParam);
                sqlParam = new SqlParameter("@CM009", SqlDbType.VarChar);
                if (iid == null)
                    sqlParam.Value = DBNull.Value;
                else
                    sqlParam.Value = iid;
                paraList.Add(sqlParam);
                sqlParam = new SqlParameter("@CM002", acccount);
                paraList.Add(sqlParam);

                dbCtl.Open();
                dbCtl.ExecuteScalar(strSQL, paraList);
            }
            catch (SqlException sqlEx)
            {
                error = new Error();
                if (sqlEx.Number == 2601)
                {
                    error.Number = 101;
                    error.ErrorMessage = "帳號已註冊";
                }
                else
                {
                    error.Number = 100;
                    error.ErrorMessage = "系統錯誤";
                }
            }
            catch (Exception ex)
            {
                error = new Error();
                error.Number = 100;
                error.ErrorMessage = "系統錯誤";
            }
            finally
            {
                dbCtl.Close();
            }
        }

        public void updateLocalMobil(string acccount, string mobil, out Error error)
        {           
            error = null;
            SqlParameter sqlParam;

            IList<SqlParameter> paraList = new System.Collections.Generic.List<SqlParameter>();
            string strSQL = "update consumer_member set CM007=@CM007 where CM002=@CM002;";

            THC_Library.DataBase.DataBaseControl dbCtl = new THC_Library.DataBase.DataBaseControl();
            try
            {
                sqlParam = new SqlParameter("@CM007", mobil);
                if (mobil == null)
                    sqlParam.Value = DBNull.Value;
                else
                    sqlParam.Value = mobil;
                paraList.Add(sqlParam);
                sqlParam = new SqlParameter("@CM002", acccount);
                paraList.Add(sqlParam);

                dbCtl.Open();
                dbCtl.ExecuteScalar(strSQL, paraList);
            }
            catch (SqlException sqlEx)
            {
                error = new Error();
                if (sqlEx.Number == 2601)
                {
                    error.Number = 101;
                    error.ErrorMessage = "帳號已註冊";
                }
                else
                {
                    error.Number = 100;
                    error.ErrorMessage = "系統錯誤";
                }
            }
            catch (Exception ex)
            {
                error = new Error();
                error.Number = 100;
                error.ErrorMessage = "系統錯誤";
            }
            finally
            {
                dbCtl.Close();
            }
        }

        public void localFaceBookAccount(string mail, string name, string gender, out Error error)
        {
            error = null;  
            bool bHasAccount = false;          
            SqlParameter sqlParam;
            IDataReader dataReader;
            IList<SqlParameter> paraList = new System.Collections.Generic.List<SqlParameter>();
            string strSQL = "select CM002 from consumer_member where CM003=@CM003";

            DataBaseControl dbCtl = new DataBaseControl();
            try
            {
                sqlParam = new SqlParameter("@CM003", mail);
                paraList.Add(sqlParam);

                dbCtl.Open();
                dataReader = dbCtl.GetReader(strSQL, paraList);
                if (dataReader.Read())
                {                   
                    bHasAccount = true;
                }
                dataReader.Close();

                if (bHasAccount)
                {
                  
                }
                else
                {
                    //以FB註冊會員資料
                    strSQL = "insert into consumer_member (CM002,CM003,CM010) values " +
                            "(@CM002,@CM007,@CM010,@CM011);";

                    sqlParam = new SqlParameter("@CM002", mail);
                    paraList.Add(sqlParam);
                    sqlParam = new SqlParameter("@CM003", mail);
                    paraList.Add(sqlParam);                   
                    sqlParam = new SqlParameter("@CM010", SqlDbType.Char);
                    if (gender == null)
                        sqlParam.Value = "0";
                    else
                        sqlParam.Value = gender;
                    paraList.Add(sqlParam);
                  
                    dbCtl.Open();
                    dbCtl.ExecuteCommad(strSQL, paraList);    
                }
            }
            catch (SqlException sqlEx)
            {
                error = new Error();
                if (sqlEx.Number == 2601)
                {
                    error.Number = 101;
                    error.ErrorMessage = "帳號已註冊";
                }
                else
                {
                    error.Number = 100;
                    error.ErrorMessage = sqlEx.ToString(); //"系統錯誤";
                }
            }
            catch (Exception ex)
            {
                error = new Error();
                error.Number = 100;
                error.ErrorMessage = ex.ToString();// "系統錯誤";
            }
            finally
            {
                dbCtl.Close();
            }
        }

    }
}
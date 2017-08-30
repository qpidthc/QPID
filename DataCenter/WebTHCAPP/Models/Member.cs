using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using THC_Library;

namespace WebTHCAPP.Models
{
    public class Member
    {
        private const int BASE_ERROR = 800;
        public bool checkExits(string mail, out Error error)
        {
            error = null;

            return false;
        }

        public long verifyAccount(string mail, string pwd, out Error error)
        {
            error = null;
            long lgTimestamp = -1;
            bool bReturn = false;
            SqlParameter sqlParam;
            IDataReader dataReader;
            IList<SqlParameter> paraList = new System.Collections.Generic.List<SqlParameter>();
            string strSQL = "select CM007 from consumer_member where CM002=@CM002";

            THC_Library.DataBase.DataBaseControl dbCtl = new THC_Library.DataBase.DataBaseControl();
            try
            {
                byte[] pwdBytes = System.Text.Encoding.Default.GetBytes(pwd); //將字串來源轉為Byte[] 
                System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create(); //使用MD5 
                pwdBytes = md5.ComputeHash(pwdBytes);//進行加密 
                pwd = Convert.ToBase64String(pwdBytes);//將加密後的字串從byte[]轉回string

                sqlParam = new SqlParameter("@CM002", mail);
                paraList.Add(sqlParam);


                dbCtl.Open();
                dataReader = dbCtl.GetReader(strSQL, paraList);
                if (dataReader.Read())
                {
                    string realPwd = dataReader["CM007"].ToString();


                    if (string.Compare(realPwd, pwd) == 0)
                    {
                        bReturn = true;
                    }
                }
                dataReader.Close();

                if (bReturn)
                {
                    lgTimestamp = DateTime.Now.Ticks;
                    strSQL = "update consumer_member set CM016=@CM016 where CM002=@CM002";
                    paraList.Clear();
                    sqlParam = new SqlParameter("@CM016", lgTimestamp);
                    paraList.Add(sqlParam);
                    sqlParam = new SqlParameter("@CM002", mail);
                    paraList.Add(sqlParam);
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

            return lgTimestamp;
        }

        public int newAccount(string acccount, string mail, string mobil, string pwd, out long timestamp, out Error error)
        {
            error = null;
            timestamp = -1;
            SqlParameter sqlParam;

            IList<SqlParameter> paraList = new System.Collections.Generic.List<SqlParameter>();
            string strSQL = "insert into consumer_member (CM002,CM007,CM008,CM014,CM016,CM017) values " +
                            "(@CM002,@CM007,@CM008,@CM014,@CM016,@CM017);SELECT CAST(scope_identity() AS int);";

            THC_Library.DataBase.DataBaseControl dbCtl = new THC_Library.DataBase.DataBaseControl();
            try
            {
                byte[] pwdBytes = System.Text.Encoding.Default.GetBytes(pwd); //將字串來源轉為Byte[] 
                System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create(); //使用MD5 
                pwdBytes = md5.ComputeHash(pwdBytes);//進行加密 
                pwd = Convert.ToBase64String(pwdBytes);//將加密後的字串從byte[]轉回string

                sqlParam = new SqlParameter("@CM002", acccount);
                paraList.Add(sqlParam);
                sqlParam = new SqlParameter("@CM007", pwd);
                paraList.Add(sqlParam);
                sqlParam = new SqlParameter("@CM008", SqlDbType.VarChar);
                if (mobil == null)
                    sqlParam.Value = DBNull.Value;
                else
                    sqlParam.Value = mobil;
                paraList.Add(sqlParam);
                sqlParam = new SqlParameter("@CM014", DateTime.Now);
                paraList.Add(sqlParam);
                timestamp = DateTime.Now.Ticks;
                sqlParam = new SqlParameter("@CM016", timestamp);
                paraList.Add(sqlParam);
                sqlParam = new SqlParameter("@CM017", SqlDbType.VarChar);
                if (mail == null)
                    sqlParam.Value = DBNull.Value;
                else
                    sqlParam.Value = mail;
                paraList.Add(sqlParam);

                dbCtl.Open();
                object accKey = dbCtl.ExecuteScalar(strSQL, paraList);
                int iaccKey = Convert.ToInt32(accKey);

                return iaccKey;
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

            return 0;
        }

        public int updateAccount(string acccount, string timestamp, string mobil, string gender, string age,
                                 string iid, string addr, out Error error)
        {
            error = null;
            int iUpdateCount = 0;
            SqlParameter sqlParam;
            IList<SqlParameter> paraList = new System.Collections.Generic.List<SqlParameter>();
            string strSQL = "update consumer_member set CM008=@CM008,CM009=@CM009,CM010=@CM010,CM012=@CM012,CM013=@CM013 " +
                            "where CM002=@CM002 and CM016=@CM016";
            //CM008 手機 CM009 地址 CM010 身分證號 CM012 性別 CM013 年齡
            THC_Library.DataBase.DataBaseControl dbCtl = new THC_Library.DataBase.DataBaseControl();
            try
            {
                paraList.Clear();
                sqlParam = new SqlParameter("@CM008", mobil);
                paraList.Add(sqlParam);
                if (addr == null)
                {
                    sqlParam = new SqlParameter("@CM009", DBNull.Value);
                }
                else
                {
                    sqlParam = new SqlParameter("@CM009", addr);
                }
                paraList.Add(sqlParam);
                if (iid == null)
                {
                    sqlParam = new SqlParameter("@CM010", DBNull.Value);
                }
                else
                {
                    sqlParam = new SqlParameter("@CM010", iid);
                }
                paraList.Add(sqlParam);
                if (gender == null)
                {
                    sqlParam = new SqlParameter("@CM012", DBNull.Value);
                }
                else
                {
                    sqlParam = new SqlParameter("@CM012", gender);
                }
                paraList.Add(sqlParam);
                if (age == null)
                {
                    sqlParam = new SqlParameter("@CM013", DBNull.Value);
                }
                else
                {
                    sqlParam = new SqlParameter("@CM013", age);
                }
                paraList.Add(sqlParam);
                sqlParam = new SqlParameter("@CM002", acccount);
                paraList.Add(sqlParam);
                sqlParam = new SqlParameter("@CM016", SqlDbType.BigInt);
                sqlParam.Value = long.Parse(timestamp);
                paraList.Add(sqlParam);

                dbCtl.Open();
                iUpdateCount = dbCtl.ExecuteCommad(strSQL, paraList);

            }
            catch (Exception ex)
            {
                error = new Error();
                error.Number = 100;
                error.ErrorMessage = "資料更新系統錯誤";
            }
            finally
            {
                dbCtl.Close();
            }
            return iUpdateCount;
        }

        public long verifyFaceBookAccount(string mail, string name, string gender, out string account, out Error error)
        {
            error = null;
            account = "";
            long lgTimestamp = -1;
            bool bHasAccount = false;
            string strAcc = "";
            string strMail = "";
            SqlParameter sqlParam;
            IDataReader dataReader;
            IList<SqlParameter> paraList = new System.Collections.Generic.List<SqlParameter>();
            string strSQL = "select CM002,CM017 from consumer_member where CM003=@CM003";

            THC_Library.DataBase.DataBaseControl dbCtl = new THC_Library.DataBase.DataBaseControl();
            try
            {
                sqlParam = new SqlParameter("@CM003", mail);
                paraList.Add(sqlParam);

                dbCtl.Open();
                dataReader = dbCtl.GetReader(strSQL, paraList);
                if (dataReader.Read())
                {
                    strAcc = dataReader["CM002"].ToString();
                    strMail = dataReader["CM017"].ToString();
                    bHasAccount = true;
                }
                dataReader.Close();

                if (bHasAccount)
                {
                    lgTimestamp = DateTime.Now.Ticks;
                    strSQL = "update consumer_member set CM016=@CM016 where CM002=@CM002";
                    paraList.Clear();
                    sqlParam = new SqlParameter("@CM016", lgTimestamp);
                    paraList.Add(sqlParam);
                    sqlParam = new SqlParameter("@CM002", strAcc);
                    paraList.Add(sqlParam);
                    dbCtl.ExecuteCommad(strSQL, paraList);
                    account = strAcc;
                }
                else
                {
                    //以FB註冊會員資料
                    strSQL = "insert into consumer_member (CM002,CM003,CM006,CM007,CM012,CM014,CM016,CM017) values " +
                            "(@CM002,@CM003,@CM006,@CM007,@CM012,@CM014,@CM016,@CM017);SELECT CAST(scope_identity() AS int);";

                    paraList.Clear();
                    sqlParam = new SqlParameter("@CM002", mail);
                    paraList.Add(sqlParam);
                    sqlParam = new SqlParameter("@CM003", mail);
                    paraList.Add(sqlParam);
                    sqlParam = new SqlParameter("@CM006", name);
                    paraList.Add(sqlParam);
                    sqlParam = new SqlParameter("@CM007", "");
                    paraList.Add(sqlParam);
                    sqlParam = new SqlParameter("@CM012", gender);
                    paraList.Add(sqlParam);
                    sqlParam = new SqlParameter("@CM014", DateTime.Now);
                    paraList.Add(sqlParam);
                    lgTimestamp = DateTime.Now.Ticks;
                    sqlParam = new SqlParameter("@CM016", lgTimestamp);
                    paraList.Add(sqlParam);
                    sqlParam = new SqlParameter("@CM017", SqlDbType.VarChar);
                    sqlParam.Value = mail;
                    paraList.Add(sqlParam);

                    object accKey = dbCtl.ExecuteScalar(strSQL, paraList);
                    int iaccKey = Convert.ToInt32(accKey);
                    account = mail;
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

            return lgTimestamp;
        }

        public AccountInfo getAccountInfo(string acc, string tk, out Error error)
        {
            error = null;
            AccountInfo accInfo = null;
            IDataReader dataReader;
            IList<SqlParameter> paraList = new System.Collections.Generic.List<SqlParameter>();
            string strSQL = "select * from consumer_member where CM002=@CM002 and CM016=@CM016";
            THC_Library.DataBase.DataBaseControl dbCtl = new THC_Library.DataBase.DataBaseControl();
            try
            {
                paraList.Clear();
                paraList.Add(new SqlParameter("@CM002", acc));
                paraList.Add(new SqlParameter("@CM016", tk));
                
                dbCtl.Open();
                dataReader = dbCtl.GetReader(strSQL, paraList);
                if (dataReader.Read())
                {
                    accInfo = new AccountInfo();
                    accInfo.FB = dataReader["CM003"].ToString();
                    accInfo.Mail = dataReader["CM017"].ToString();
                    accInfo.Mobil = dataReader["CM008"].ToString();
                    accInfo.Address = dataReader["CM009"].ToString();
                    accInfo.IId = dataReader["CM010"].ToString();
                    accInfo.Gender = dataReader["CM012"].ToString();
                    accInfo.Age = dataReader["CM013"].ToString();
                    accInfo.Number = 0;
                    accInfo.ErrorMessage = "";
                }
                dataReader.Close();
                                
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

            return accInfo;

        }

        public void newRecord(string eventkey, string qrcode, string date, string account,
                                string age, string gender, string area, string temp, string weather,
                                string tk, out Error error)
        {           
            error = null;
            AccountInfo accInfo = null;
            IDataReader dataReader;
            SqlParameter sqlParam;
            IList<SqlParameter> paraList = new System.Collections.Generic.List<SqlParameter>();
            string strSQL = "select * from consumer_member where CM002=@CM002 and CM016=@CM016";
            THC_Library.DataBase.DataBaseControl dbCtl = new THC_Library.DataBase.DataBaseControl();

            bool bCanRecord = false;

            try
            {
                paraList.Clear();
                paraList.Add(new SqlParameter("@CM002", account));
                paraList.Add(new SqlParameter("@CM016", tk));

                dbCtl.Open();
                dataReader = dbCtl.GetReader(strSQL, paraList);
                if (dataReader.Read())
                {
                    bCanRecord = true;
                }
                dataReader.Close();

                if (!bCanRecord)
                {
                    throw new THC_Library.THCException(801, "無效的帳號資料");
                }

                strSQL = "insert into event_user_records (EUR002,EUR003,EUR004,EUR005,EUR006,EUR007,EUR008,EUR009,EUR010) values " +
                        "(@EUR002,@EUR003,@EUR004,@EUR005,@EUR006,@EUR007,@EUR008,@EUR009,@EUR010);SELECT CAST(scope_identity() AS int);";

                paraList.Clear();
                sqlParam = new SqlParameter("@EUR002", SqlDbType.Int);
                sqlParam.Value = eventkey;
                paraList.Add(sqlParam);
                sqlParam = new SqlParameter("@EUR003", SqlDbType.VarChar);
                sqlParam.Value = qrcode;
                paraList.Add(sqlParam);
                sqlParam = new SqlParameter("@EUR004", SqlDbType.DateTime);
                sqlParam.Value = date;
                paraList.Add(sqlParam);
                sqlParam = new SqlParameter("@EUR005", SqlDbType.VarChar);
                sqlParam.Value = account; //帳號
                paraList.Add(sqlParam);
                sqlParam = new SqlParameter("@EUR006", SqlDbType.Char);
                sqlParam.Value = age; //年紀
                paraList.Add(sqlParam);
                sqlParam = new SqlParameter("@EUR007", SqlDbType.Char);
                sqlParam.Value = gender; //性別
                paraList.Add(sqlParam);
                sqlParam = new SqlParameter("@EUR008", SqlDbType.NVarChar);
                sqlParam.Value = ""; //地區
                paraList.Add(sqlParam);
                sqlParam = new SqlParameter("@EUR009", SqlDbType.SmallInt);
                sqlParam.Value = -199; //溫度
                paraList.Add(sqlParam);
                sqlParam = new SqlParameter("@EUR010", SqlDbType.Char);
                sqlParam.Value = ""; //天氣
                paraList.Add(sqlParam);

                dbCtl.ExecuteScalar(strSQL, paraList);


            }
            catch (THC_Library.THCException thcEx)
            {
                error = new Error();
                error.Number = thcEx.Number;
                error.ErrorMessage = thcEx.Message;
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

        public long loginFromActivity(string acc, string tk, out Error error)
        {
            error = null;

            long newTicket = -1;
            IDataReader dataReader;
            IList<SqlParameter> paraList = new System.Collections.Generic.List<SqlParameter>();
            string strSQL = "select * from consumer_member where CM002=@CM002 and CM016=@CM016";
            THC_Library.DataBase.DataBaseControl dbCtl = new THC_Library.DataBase.DataBaseControl();

            paraList.Add(new SqlParameter("@CM002", acc));
            paraList.Add(new SqlParameter("@CM016", tk));

            bool bchkSession = false;
            try
            {
                dbCtl.Open();

                dataReader = dbCtl.GetReader(strSQL, paraList);
                if (dataReader.Read())
                {
                    bchkSession = true;
                }
                dataReader.Close();

                if (bchkSession)
                {
                    newTicket = DateTime.Now.Ticks;
                    strSQL = "update consumer_member set CM016=@CM016 where CM002=@CM002";
                    paraList.Clear();
                    paraList.Add(new SqlParameter("@CM016", newTicket));
                    paraList.Add(new SqlParameter("@CM002", acc));

                    dbCtl.ExecuteCommad(strSQL, paraList);
                }
                else
                {
                    THCException thcEx = new THCException(BASE_ERROR + 7, "無效的登入");
                    throw thcEx;
                }
            }
            catch (THCException thcEx)
            {
                error = new Error();
                error.Number = thcEx.Number;
                error.ErrorMessage = thcEx.Message;
            }
            catch (Exception ex)
            {
                error = new Error();
                error.Number = BASE_ERROR + 6;
                error.ErrorMessage = "重啟登入系統錯誤";
            }
            finally
            {
                dbCtl.Close();
            }

            return newTicket;
        }
    }
}
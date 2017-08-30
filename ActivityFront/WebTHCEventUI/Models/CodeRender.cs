﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using THC_Library.DataBase;

namespace WebTHCEventUI.Models
{
    public class CodeRender
    {
        public bool go(string ac, string code, string tk, string ml, out string gender, out string age, 
                            out string mobil, out string iid, out string addr, out THC_Library.Reward.RewardConvertor rwd, 
                            out int logkey, out THC_Library.Error error)
        {
            error = null;
            rwd = null;
            gender = "";
            age = "";
            mobil = "";
            iid = "";
            addr = "";
            logkey = -1;
            DateTime datNow = DateTime.Now;
            DateTime datNowDate = new DateTime(datNow.Year, datNow.Month, datNow.Day);
            int iIdentityKey;
            int eventKey = -1;
            string eventName = "";
            DateTime startTime = DateTime.MaxValue;
            DateTime endTime = DateTime.MinValue;

            SqlParameter sqlParam;
            IDataReader dataReader;
            IList<SqlParameter> paraList = new System.Collections.Generic.List<SqlParameter>();
            DataBaseControl dbCtl = new DataBaseControl();
            //paraList.Add(new SqlParameter("@EQCH002", event_key));
            string strSQL = "select * from activity_event where AE002=@AE002";
            paraList.Add(new SqlParameter("@AE002", ac));

            bool bRightEvent = false;
            bool bKeyExist = false;
            bool bWin = false;

            try
            {               
                dbCtl.Open();

                dataReader = dbCtl.GetReader(strSQL, paraList);
                if (dataReader.Read())
                {
                    bRightEvent = true;
                    eventKey = int.Parse(dataReader["AE001"].ToString());
                    eventName = dataReader["AE003"].ToString();
                    startTime = Convert.ToDateTime(dataReader["AE005"]);
                    endTime = Convert.ToDateTime(dataReader["AE006"]);
                }               
                dataReader.Close();

                if (!bRightEvent)
                {                                        
                    throw new THC_Library.CodeRenderException( THC_Library.CodeRenderException.INVAILD_ACTIVITY, "無效的活動");
                }
                else
                {
                    if (startTime.Subtract(datNowDate).TotalDays > 0)
                    {
                        //未開始
                        THC_Library.CodeRenderException codeException = 
                            new THC_Library.CodeRenderException(THC_Library.CodeRenderException.ACTIVITY_NOT_START, "活動尚未開始");
                        codeException.AdditionalMessage = string.Format("{0} 活動期間 {1} - {2}", eventName, startTime, endTime);
                        throw codeException;
                    }
                    if (endTime.Subtract(datNowDate).TotalDays < 0)
                    {
                        //結束
                        THC_Library.CodeRenderException codeException = new THC_Library.CodeRenderException(THC_Library.CodeRenderException.ACTIVITY_FINISHED, "活動已結束");
                        codeException.AdditionalMessage = string.Format("{0} 活動期間 {1} - {2}", eventName, startTime, endTime);
                        throw codeException;
                    }

                }

                //確認登入
                bool bLoginChecked = false;

                string jsonString = THC_Library.APPCURL.GetAccountInfo(ml, tk);
                dynamic jsonResult = Newtonsoft.Json.JsonConvert.DeserializeObject(jsonString);

                if (jsonResult.Number == 0)
                {
                    //AccountInfo
                    bLoginChecked = true;
                    mobil = jsonResult.Mobil;
                    addr = jsonResult.Address;
                    iid = jsonResult.IId;
                    gender = jsonResult.Gender;
                    age = jsonResult.Age;                    
                }
                else
                {
                    //Result
                    bLoginChecked = false;
                }
                                
                if (!bLoginChecked)
                {
                    THC_Library.CodeRenderException codeException = 
                                new THC_Library.CodeRenderException(THC_Library.CodeRenderException.LOGIN_INVALID, "無效登入");                    
                    throw codeException;
                }

                strSQL = "select * from qr_record where QRC015=@QRC015";
                paraList.Clear();
                paraList.Add(new SqlParameter("@QRC015", code));
                dataReader = dbCtl.GetReader(strSQL, paraList);
                object EC = "";

                if (dataReader.Read())
                {
                    int iScanCounter = int.Parse(dataReader["QRC012"].ToString());
                    if (iScanCounter == 0)
                    {
                        //中獎
                        EC = dataReader["QRC008"];
                        if (EC != DBNull.Value)
                        {
                            bWin = true;
                            THC_Library.Reward.RewardConvertor rwdConvertor; 
                            if (dataReader["QRC009"].ToString() == "0")
                            {
                                //虛擬
                                rwdConvertor = new THC_Library.Reward.Edenred();
                                THC_Library.Reward.Edenred edenred = rwdConvertor as THC_Library.Reward.Edenred;
                                edenred.RewardName = dataReader["QRC011"].ToString();
                                edenred.RewardType = THC_Library.Reward.RewardType.ElectricCoupon;
                                edenred.CouponNumber = EC.ToString();
                                if (dataReader["QRC017"] != DBNull.Value)
                                {
                                    string strJSon = dataReader["QRC017"].ToString();
                                    if (strJSon.Length > 0)
                                    {
                                        dynamic jsonReward = Newtonsoft.Json.JsonConvert.DeserializeObject(strJSon);
                                        edenred.ValidPeriod = jsonReward.v_date;
                                        edenred.RewardImage = jsonReward.img;
                                    }
                                }       
                            }
                            else
                            {
                                //實體
                                rwdConvertor = new THC_Library.Reward.Phyicalenred();
                                THC_Library.Reward.Phyicalenred phyenred = rwdConvertor as THC_Library.Reward.Phyicalenred;
                                phyenred.RewardName = dataReader["QRC011"].ToString();
                                phyenred.RewardType = THC_Library.Reward.RewardType.PhyicalReward;
                                phyenred.CouponNumber = EC.ToString();
                                if (dataReader["QRC017"] != DBNull.Value)
                                {
                                    string strJSon = dataReader["QRC017"].ToString();
                                    if (strJSon.Length > 0)
                                    {
                                        dynamic jsonReward = Newtonsoft.Json.JsonConvert.DeserializeObject(strJSon);
                                        phyenred.Description = jsonReward.desc;
                                        phyenred.RewardImage = jsonReward.img;
                                    }
                                }       
                            }

                            rwd = rwdConvertor;
                        }
                        bKeyExist = true;
                    }
                    else
                    {
                        DateTime lastTime;
                        DateTime.TryParse(dataReader["QRC013"].ToString(), out lastTime);                       
                        dataReader.Close();
                        
                        THC_Library.CodeRenderException codeException =
                            new THC_Library.CodeRenderException(THC_Library.CodeRenderException.REPEAT_SCAN, lastTime.ToString("yyyy/MM/dd HH:mm"));
                        codeException.AdditionalMessage = lastTime.ToString("yyyy/MM/dd HH:mm"); //string.Format("上次掃描時間 <br/>{0}", lastTime.ToString("MM/dd HH:mm:ss"));
                        throw codeException;
                    }
                }
                dataReader.Close();

                if (!bKeyExist)
                {
                    //掃描的 code 不再發行裡面
                    throw new THC_Library.CodeRenderException(THC_Library.CodeRenderException.INVAILD_CODE, "無效的發碼");
                }
                if (!bWin)
                {
                    //未中獎
                    strSQL = "update qr_record set QRC012=QRC012+1, QRC013=@QRC013 where QRC015=@QRC015;";
                    paraList.Clear();
                    sqlParam = new SqlParameter("@QRC013", SqlDbType.DateTime);
                    sqlParam.Value = datNow;
                    paraList.Add(sqlParam);
                    sqlParam = new SqlParameter("@QRC015", SqlDbType.VarChar);
                    sqlParam.Value = code;
                    paraList.Add(sqlParam);
                    dbCtl.ExecuteCommad(strSQL, paraList);
                }

            }
            catch (THC_Library.CodeRenderException codeex)
            {
                error = new THC_Library.Error();
                error.Number = codeex.Number;
                error.ErrorMessage = codeex.AdditionalMessage;
            }
            catch (Exception ex)
            {
                dbCtl.RollBackTransaction();
                error = new THC_Library.Error();
                error.Number = THC_Library.THCException.SYSTEM_ERROR;
                error.ErrorMessage = "系統發生異常錯誤，請稍後再上線使用。";//ex.Message;
            }
            finally
            {
                dbCtl.Close();
            }

            return bWin;
        }

        public bool done(string ac, string code, string tk, string ml,  
                        string coupnumber , string logkey, out THC_Library.Error error)
        {
            error = null;                       
            
            IDataReader dataReader;
            SqlParameter sqlParam;
            IList<SqlParameter> paraList = new System.Collections.Generic.List<SqlParameter>();
            string strSQL = "select AE001,AE003 from activity_event where AE002=@AE002";
            paraList.Add(new SqlParameter("@AE002", ac));
            DataBaseControl dbCtl = new DataBaseControl();

            DateTime datNow = DateTime.Now;
            int eventKey = -1;
            string eventName;
            string mobil = "";
            string gender = "";
            string age = "";
            bool bRightEvent = false;
          
            try
            {
                dbCtl.Open();

                dataReader = dbCtl.GetReader(strSQL, paraList);
                if (dataReader.Read())
                {
                    bRightEvent = true;
                    eventKey = int.Parse(dataReader["AE001"].ToString());
                    eventName = dataReader["AE003"].ToString();                    
                }
                dataReader.Close();

                if (!bRightEvent)
                {
                    throw new THC_Library.CodeRenderException(THC_Library.CodeRenderException.INVAILD_ACTIVITY, "無效的活動");
                }

                //確認登入
                bool bLoginChecked = false;

                string jsonString = THC_Library.APPCURL.GetAccountInfo(ml, tk);
                dynamic jsonResult = Newtonsoft.Json.JsonConvert.DeserializeObject(jsonString);

                if (jsonResult.Number == 0)
                {
                    //AccountInfo
                    bLoginChecked = true;
                    mobil = jsonResult.Mobil;                    
                    gender = jsonResult.Gender;
                    age = jsonResult.Age;
                    //addr = jsonResult.Address;
                    //iid = jsonResult.IId;
                }
                else
                {
                    //Result
                    bLoginChecked = false;
                }

                if (!bLoginChecked)
                {
                    THC_Library.CodeRenderException codeException =
                                new THC_Library.CodeRenderException(THC_Library.CodeRenderException.LOGIN_INVALID, "無效登入");
                    throw codeException;
                }

                strSQL = "select QRC009,QRC012,QRC013 from qr_record where QRC008=@QRC008 and QRC015=@QRC015";
                paraList.Clear();
                paraList.Add(new SqlParameter("@QRC008", coupnumber));
                paraList.Add(new SqlParameter("@QRC015", code));
                dataReader = dbCtl.GetReader(strSQL, paraList);

                string rwardType = "";
                if (dataReader.Read())
                {
                    rwardType = dataReader["QRC009"].ToString();
                    int iScanCounter = int.Parse(dataReader["QRC012"].ToString());

                    if (iScanCounter > 0)
                    {
                        DateTime lastTime;
                        DateTime.TryParse(dataReader["QRC013"].ToString(), out lastTime);   
                        dataReader.Close();
                        THC_Library.CodeRenderException codeException =
                            new THC_Library.CodeRenderException(THC_Library.CodeRenderException.REPEAT_SCAN, lastTime.ToString("yyyy/MM/dd HH:mm"));
                        codeException.AdditionalMessage = lastTime.ToString("yyyy/MM/dd HH:mm");
                        throw codeException;
                    }
                }
                else
                {
                    dataReader.Close();
                    THC_Library.CodeRenderException codeException =
                                new THC_Library.CodeRenderException(THC_Library.CodeRenderException.INVAILD_CODE, "無效的發碼");
                    throw codeException;
                }
                dataReader.Close();
                             
                dbCtl.BeginTransaction();

                strSQL = "insert into event_user_records (EUR002,EUR003,EUR004,EUR005,EUR006,EUR007,EUR008,EUR009,EUR010) values " +
                        "(@EUR002,@EUR003,@EUR004,@EUR005,@EUR006,@EUR007,@EUR008,@EUR009,@EUR010);SELECT CAST(scope_identity() AS int);";

                paraList.Clear();
                sqlParam = new SqlParameter("@EUR002", SqlDbType.Int);
                sqlParam.Value = eventKey;
                paraList.Add(sqlParam);
                sqlParam = new SqlParameter("@EUR003", SqlDbType.VarChar);
                sqlParam.Value = code;
                paraList.Add(sqlParam);
                sqlParam = new SqlParameter("@EUR004", SqlDbType.DateTime);
                sqlParam.Value = datNow;
                paraList.Add(sqlParam);
                sqlParam = new SqlParameter("@EUR005", SqlDbType.VarChar);
                sqlParam.Value = ml; //帳號
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
                sqlParam.Value = -200; //溫度
                paraList.Add(sqlParam);
                sqlParam = new SqlParameter("@EUR010", SqlDbType.Char);
                sqlParam.Value = ""; //天氣
                paraList.Add(sqlParam);


                object newId = dbCtl.ExecuteScalar(strSQL, paraList);
                               
                //中獎
                strSQL = "update qr_record set QRC012=QRC012+1, QRC013=@QRC013,QRC014=@QRC014,QRC016=@QRC016 where QRC015=@QRC015;";
                paraList.Clear();
                sqlParam = new SqlParameter("@QRC013", SqlDbType.DateTime);
                sqlParam.Value = datNow;
                paraList.Add(sqlParam);
                sqlParam = new SqlParameter("@QRC014", SqlDbType.Int);
                sqlParam.Value = newId;
                paraList.Add(sqlParam);
                sqlParam = new SqlParameter("@QRC016", SqlDbType.VarChar);
                sqlParam.Value = ml;
                paraList.Add(sqlParam);
                sqlParam = new SqlParameter("@QRC015", SqlDbType.VarChar);
                sqlParam.Value = code;
                paraList.Add(sqlParam);
                dbCtl.ExecuteCommad(strSQL, paraList);         

                dbCtl.CommintTransaction();

                if (rwardType == "0")
                {
                    //bool bSMS_OK = THC_Library.SMSHelper.SendTo(ml, mobil, "恭喜中獎 " + coupnumber);
                    //if (bSMS_OK)
                    //{
                    //}     
                }
                        

                jsonString = THC_Library.APPCURL.ScanRecord(eventKey.ToString(), code, datNow.ToString(), ml, age, gender, "", "", "", tk);
                jsonResult = Newtonsoft.Json.JsonConvert.DeserializeObject(jsonString);

                //if (jsonResult.Number != 0)
                //{                   
                                                         
                //}               
            }
            catch (THC_Library.CodeRenderException codeex)
            {
                error = new THC_Library.Error();
                error.Number = codeex.Number;
                error.ErrorMessage = codeex.AdditionalMessage;
            }
            catch (Exception ex)
            {
                //dbCtl.RollBackTransaction();
                error = new THC_Library.Error();
                error.Number = THC_Library.THCException.SYSTEM_ERROR;
                error.ErrorMessage = "系統發生異常錯誤，請紀錄您的中獎序號，並與客服人員聯絡，我們會盡訊處理這問題。";
            }
            finally
            {
                dbCtl.Close();
            }

            return true;
        }

        public void checkActivityAndCode(string ac, string code, out THC_Library.Error error)
        {
            error = null;
            DateTime datNow = DateTime.Now;
            DateTime datNowDate = new DateTime(datNow.Year, datNow.Month, datNow.Day);
            int eventKey;
            string eventName = "";
            DateTime startTime = DateTime.MaxValue;
            DateTime endTime = DateTime.MinValue;

            SqlParameter sqlParam;
            IDataReader dataReader;
            IList<SqlParameter> paraList = new System.Collections.Generic.List<SqlParameter>();
            DataBaseControl dbCtl = new DataBaseControl();
            //paraList.Add(new SqlParameter("@EQCH002", event_key));
            string strSQL = "select * from activity_event where AE002=@AE002";
            paraList.Add(new SqlParameter("@AE002", ac));
            try
            {
                bool bRightEvent = false;
                bool bKeyExist = false;
               
                dbCtl.Open();

                dataReader = dbCtl.GetReader(strSQL, paraList);
                if (dataReader.Read())
                {
                    bRightEvent = true;
                    eventKey = int.Parse(dataReader["AE001"].ToString());
                    eventName = dataReader["AE003"].ToString();
                    startTime = Convert.ToDateTime(dataReader["AE005"]);
                    endTime = Convert.ToDateTime(dataReader["AE006"]);
                }
                dataReader.Close();

                if (!bRightEvent)
                {
                    throw new THC_Library.CodeRenderException(THC_Library.CodeRenderException.INVAILD_ACTIVITY, "無效的活動");
                }
                else
                {
                    if (startTime.Subtract(datNowDate).TotalDays > 0)
                    {
                        //未開始
                        THC_Library.CodeRenderException codeException =
                            new THC_Library.CodeRenderException(THC_Library.CodeRenderException.ACTIVITY_NOT_START, "活動尚未開始");
                        codeException.AdditionalMessage = string.Format("{0} 活動期間 {1} - {2}", eventName, startTime, endTime);
                        throw codeException;
                    }
                    if (endTime.Subtract(datNowDate).TotalDays < 0)
                    {
                        //結束
                        THC_Library.CodeRenderException codeException = new THC_Library.CodeRenderException(THC_Library.CodeRenderException.ACTIVITY_FINISHED, "活動已結束");
                        codeException.AdditionalMessage = string.Format("{0} 活動期間 {1} - {2}", eventName, startTime, endTime);
                        throw codeException;
                    }

                }

                strSQL = "select * from qr_record where QRC015=@QRC015";
                paraList.Clear();
                paraList.Add(new SqlParameter("@QRC015", code));
                dataReader = dbCtl.GetReader(strSQL, paraList);

                if (dataReader.Read())
                {
                    int iScanCounter = int.Parse(dataReader["QRC012"].ToString());
                    if (iScanCounter == 0)
                    {                       
                        bKeyExist = true;
                    }
                    else
                    {
                        DateTime lastTime;
                        DateTime.TryParse(dataReader["QRC013"].ToString(), out lastTime);
                        dataReader.Close();
                        THC_Library.CodeRenderException codeException =
                            new THC_Library.CodeRenderException(THC_Library.CodeRenderException.REPEAT_SCAN, lastTime.ToString("yyyy/MM/dd HH:mm"));
                        codeException.AdditionalMessage = lastTime.ToString("yyyy/MM/dd HH:mm"); 
                        throw codeException;
                    }
                }
                dataReader.Close();

                if (!bKeyExist)
                {
                    //掃描的 code 不再發行裡面
                    throw new THC_Library.CodeRenderException(THC_Library.CodeRenderException.INVAILD_CODE, "無效的發碼");
                }                                
            }
            catch (THC_Library.CodeRenderException codeex)
            {
                error = new THC_Library.Error();
                error.Number = codeex.Number;
                error.ErrorMessage = codeex.AdditionalMessage;
            }
            catch (Exception ex)
            {
                dbCtl.RollBackTransaction();
                error = new THC_Library.Error();
                error.Number = THC_Library.THCException.SYSTEM_ERROR;
                error.ErrorMessage = "系統發生異常錯誤，請稍後再上線使用。";//ex.Message;
            }
            finally
            {
                dbCtl.Close();
            }
        }

        public void updateRewardData(string  act, string code, string ml, string tk, out THC_Library.Error error)
        {
            error = null;
            SqlParameter sqlParam;
            IDataReader dataReader;
            IList<SqlParameter> paraList = new System.Collections.Generic.List<SqlParameter>();

            DateTime datNow = DateTime.Now;
            DataBaseControl dbCtl = new DataBaseControl();
            string strSQL;
            try
            {
                dbCtl.Open();

                bool bLoginChecked = false;
                strSQL = "select * from consumer_member where CM002=@CM002 and CM016=@CM016";
                paraList.Clear();
                paraList.Add(new SqlParameter("@CM002", ml));
                paraList.Add(new SqlParameter("@CM016", tk));
                dataReader = dbCtl.GetReader(strSQL, paraList);
                if (dataReader.Read())
                {
                    bLoginChecked = true;
                }
                dataReader.Close();
                
                if (!bLoginChecked)
                {
                    THC_Library.CodeRenderException codeException =
                                new THC_Library.CodeRenderException(THC_Library.CodeRenderException.LOGIN_INVALID, "無效登入");
                    throw codeException;
                }

                strSQL = "insert into event_user_records (EUR002,EUR003,EUR004,EUR005,EUR006) values " +
                         "(@EUR002,@EUR003,@EUR004,@EUR005,@EUR006);SELECT CAST(scope_identity() AS int);";

                dbCtl.BeginTransaction();

                paraList.Clear();
                sqlParam = new SqlParameter("@EUR002", SqlDbType.Int);
                sqlParam.Value = "";// eventKey;
                paraList.Add(sqlParam);
                sqlParam = new SqlParameter("@EUR003", SqlDbType.VarChar);
                sqlParam.Value = code;
                paraList.Add(sqlParam);
                sqlParam = new SqlParameter("@EUR004", SqlDbType.DateTime);
                sqlParam.Value = datNow;
                paraList.Add(sqlParam);
                sqlParam = new SqlParameter("@EUR005", SqlDbType.VarChar);
                sqlParam.Value = "";
                paraList.Add(sqlParam);
                sqlParam = new SqlParameter("@EUR006", SqlDbType.VarChar);
                sqlParam.Value = "";
                paraList.Add(sqlParam);

                object newId = dbCtl.ExecuteScalar(strSQL, paraList);
                int iIdentityKey;
                int.TryParse(newId.ToString(), out iIdentityKey);

                ////中獎
                strSQL = "update qr_record set QRC012=QRC012+1, QRC013=@QRC013,QRC014=@QRC014,QRC016=@QRC016 where QRC015=@QRC015;";
                paraList.Clear();
                sqlParam = new SqlParameter("@QRC013", SqlDbType.DateTime);
                sqlParam.Value = datNow;
                paraList.Add(sqlParam);
                sqlParam = new SqlParameter("@QRC014", SqlDbType.Int);
                sqlParam.Value = newId;
                paraList.Add(sqlParam);
                sqlParam = new SqlParameter("@QRC016", SqlDbType.VarChar);
                sqlParam.Value = ml;
                paraList.Add(sqlParam);
                sqlParam = new SqlParameter("@QRC015", SqlDbType.VarChar);
                sqlParam.Value = code;
                paraList.Add(sqlParam);
                dbCtl.ExecuteCommad(strSQL, paraList);

                dbCtl.CommintTransaction();
            }
            catch (Exception ex)
            {
                dbCtl.RollBackTransaction();
                error = new THC_Library.Error();
                error.Number = 300;
                error.ErrorMessage = "";
            }
            finally
            {
                dbCtl.Close();
            }
            //dbCtl.BeginTransaction();
                       
            //string strSQL = "insert into event_user_records (EUR002,EUR003,EUR004,EUR005,EUR006) values " +
            //        "(@EUR002,@EUR003,@EUR004,@EUR005,@EUR006);SELECT CAST(scope_identity() AS int);";

            //paraList.Clear();
            //sqlParam = new SqlParameter("@EUR002", SqlDbType.Int);
            //sqlParam.Value = eventKey;
            //paraList.Add(sqlParam);
            //sqlParam = new SqlParameter("@EUR003", SqlDbType.VarChar);
            //sqlParam.Value = code;
            //paraList.Add(sqlParam);
            //sqlParam = new SqlParameter("@EUR004", SqlDbType.DateTime);
            //sqlParam.Value = datNow;
            //paraList.Add(sqlParam);
            //sqlParam = new SqlParameter("@EUR005", SqlDbType.VarChar);
            //sqlParam.Value = "";
            //paraList.Add(sqlParam);
            //sqlParam = new SqlParameter("@EUR006", SqlDbType.VarChar);
            //sqlParam.Value = "";
            //paraList.Add(sqlParam);

            //object newId = dbCtl.ExecuteScalar(strSQL, paraList);

            //int.TryParse(newId.ToString(), out iIdentityKey);

            ////中獎
            //strSQL = "update qr_record set QRC012=QRC012+1, QRC013=@QRC013,QRC014=@QRC014,QRC016=@QRC016 where QRC015=@QRC015;";
            //paraList.Clear();
            //sqlParam = new SqlParameter("@QRC013", SqlDbType.DateTime);
            //sqlParam.Value = datNow;
            //paraList.Add(sqlParam);
            //sqlParam = new SqlParameter("@QRC014", SqlDbType.Int);
            //sqlParam.Value = newId;
            //paraList.Add(sqlParam);
            //sqlParam = new SqlParameter("@QRC016", SqlDbType.VarChar);
            //sqlParam.Value = ml;
            //paraList.Add(sqlParam);
            //sqlParam = new SqlParameter("@QRC015", SqlDbType.VarChar);
            //sqlParam.Value = code;
            //paraList.Add(sqlParam);
            //dbCtl.ExecuteCommad(strSQL, paraList);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using THC_Library.DataBase;

namespace WebTHCEventUI.Models
{
    public class MyEvent
    {
        public DataTable getMyEventList(out THC_Library.Error error)
        {
            error = null;
            DataTable resultTable = null;

            IList<SqlParameter> paraList = new System.Collections.Generic.List<SqlParameter>();
            string strSQL = "select AE001,AE002,AE003 from activity_event";
            
            DataBaseControl dbCtl = new DataBaseControl();

            try
            {
                dbCtl.Open();
                resultTable = dbCtl.GetDataTable(strSQL, paraList);               
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

            return resultTable;
        }

        public DataTable getMyEvent(string event_no, out THC_Library.Error error)
        {
            error = null;    
            DataTable resultTable = null;

            IList<SqlParameter> paraList = new System.Collections.Generic.List<SqlParameter>();
            string strSQL = "select * from activity_event where AE002=@AE002";
            paraList.Add(new SqlParameter("@AE002", event_no));

            DataBaseControl dbCtl = new DataBaseControl();

            try
            {
                dbCtl.Open();
                resultTable = dbCtl.GetDataTable(strSQL, paraList);                                  
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

            return resultTable;
        }

        public void updateEvent(string event_no, string page, out THC_Library.Error error)
        {
            error = null;
            SqlParameter sqlParam;
            IList<SqlParameter> paraList = new System.Collections.Generic.List<SqlParameter>();
            string strSQL = "update activity_event set AE013=@AE013 where AE002=@AE002";
            
            DataBaseControl dbCtl = new DataBaseControl();

            try
            {
                sqlParam = new SqlParameter("@AE013", SqlDbType.VarChar);
                sqlParam.Value = page;
                paraList.Add(sqlParam);
                sqlParam = new SqlParameter("@AE002", SqlDbType.NVarChar);
                sqlParam.Value = event_no;
                paraList.Add(sqlParam);
                
                dbCtl.Open();
                dbCtl.ExecuteCommad(strSQL, paraList);                
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
        }

        public void AsyneEvent(string event_no, out THC_Library.Error error)
        {
            error = null;           

            IList<SqlParameter> paraList = new System.Collections.Generic.List<SqlParameter>();
            string strSQL = "select * from activity_event where AE002=@AE002";
            paraList.Add(new SqlParameter("@AE002", event_no));

            DataBaseControl dbCtl = new DataBaseControl();

            try
            {
                dbCtl.Open();
                DataTable eventTable = dbCtl.GetDataTable(strSQL, paraList);
                string eventJson = Newtonsoft.Json.JsonConvert.SerializeObject(eventTable);
                string jsonResult = THC_Library.APPCURL.AnscyActivity(eventJson);
                dynamic resultObj = Newtonsoft.Json.JsonConvert.DeserializeObject(jsonResult);

                if (resultObj.Number != 0)                
                {
                    throw new Exception(resultObj.ErrorMessage.ToString());
                }         
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
        }

        public void ClearEvent(string event_no, out THC_Library.Error error)
        {
            error = null;
            IList<SqlParameter> paraList = new System.Collections.Generic.List<SqlParameter>();
            string strSQL = "select AE001 from activity_event where AE002=@AE002";
            paraList.Add(new SqlParameter("@AE002", event_no));
         
            DataBaseControl dbCtl = new DataBaseControl();

            try
            {
                dbCtl.Open();

                IDataReader dataReader = dbCtl.GetReader(strSQL, paraList);
                dataReader.Read();
                string eventKey = dataReader[0].ToString();
                dataReader.Close();

                paraList.Clear();
                strSQL = "update qr_record  set QRC012=0,QRC013=NULL,QRC014=NULL,QRC016=NULL " +
                            "where QRC002=@QRC002;delete from event_user_records where EUR003=@EUR003";
                paraList.Add(new SqlParameter("@QRC002", event_no));
                paraList.Add(new SqlParameter("@EUR003", event_no));

                dbCtl.BeginTransaction();

                string jsonResult = THC_Library.APPCURL.ClearRecordLogActivity(eventKey);
                dynamic resultObj = Newtonsoft.Json.JsonConvert.DeserializeObject(jsonResult);
                if (resultObj.Number != 0)
                {
                    throw new Exception(resultObj.ErrorMessage.ToString());
                }         

                dbCtl.ExecuteCommad(strSQL, paraList);
                dbCtl.CommintTransaction();
            }
            catch (Exception ex)
            {
                dbCtl.RollBackTransaction();
                error = new THC_Library.Error();
                error.Number = THC_Library.THCException.SYSTEM_ERROR;
                error.ErrorMessage = ex.Message;
            }
            finally
            {
                dbCtl.Close();
            }    
        }

        public DataTable getEventRewards(string event_no, out THC_Library.Error error)
        {
            error = null;
            DataTable resultTable = null;

            IList<SqlParameter> paraList = new System.Collections.Generic.List<SqlParameter>();
            string strSQL = "select AEP001,AEP002,AEP003,AEP004,AEP005,AEP006,AEP007,AEP009,AEP011,AEP012,AEP013 " + //(SUBSTRING(AEP013,0,20) + '.....') as AEP013 " +
                            "from activity_rewards where AEP002=@AEP002";
            paraList.Add(new SqlParameter("@AEP002", event_no));

            DataBaseControl dbCtl = new DataBaseControl();

            try
            {
                dbCtl.Open();
                resultTable = dbCtl.GetDataTable(strSQL, paraList);                
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

            return resultTable;
        }

        public DataTable getSingleRewardInfo(string reward_key, out THC_Library.Error error)
        {
            error = null;
            DataTable resultTable = null;

            IList<SqlParameter> paraList = new System.Collections.Generic.List<SqlParameter>();
            string strSQL = "select * from activity_rewards where AEP001=@AEP001";
            paraList.Add(new SqlParameter("@AEP001", reward_key));

            DataBaseControl dbCtl = new DataBaseControl();

            try
            {
                dbCtl.Open();
                resultTable = dbCtl.GetDataTable(strSQL, paraList);
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

            return resultTable;
        }

        public int updateRewardInfo(string reward_key, string name, string memo, string vender, 
                            string img, string vdate, string sms, out THC_Library.Error error)
        {
            error = null;
            int iAffrect = 0;

            IList<SqlParameter> paraList = new System.Collections.Generic.List<SqlParameter>();
            string strSQL = "update activity_rewards set AEP005=@AEP005,AEP007=@AEP007,AEP009=@AEP009,AEP011=@AEP011,AEP012=@AEP012,AEP013=@AEP013 " + 
                            "where AEP001=@AEP001";
            paraList.Add(new SqlParameter("@AEP005", name));
            paraList.Add(new SqlParameter("@AEP007", memo));
            paraList.Add(new SqlParameter("@AEP009", vender));
            paraList.Add(new SqlParameter("@AEP011", img));
            paraList.Add(new SqlParameter("@AEP012", vdate));
            paraList.Add(new SqlParameter("@AEP013", sms));
            paraList.Add(new SqlParameter("@AEP001", reward_key));

            DataBaseControl dbCtl = new DataBaseControl();

            try
            {
                dbCtl.Open();
                iAffrect = dbCtl.ExecuteCommad(strSQL, paraList);
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

            return iAffrect;
        }

        public int updateRewardInfoWithFile(string reward_key, string name, string memo, string vender, string img, 
                    string win_desc, string vdate, string sms, string filepath, HttpPostedFileBase file, out THC_Library.Error error)
        {
            error = null;
            int iAffrect = 0;

            IList<SqlParameter> paraList = new System.Collections.Generic.List<SqlParameter>();
            string strSQL = "";

            if (file == null)
            {
                strSQL = "update activity_rewards set AEP005=@AEP005,AEP007=@AEP007,AEP009=@AEP009,AEP012=@AEP012,AEP013=@AEP013," +
                            "AEP014=@AEP014 where AEP001=@AEP001";
                paraList.Add(new SqlParameter("@AEP005", name));
                paraList.Add(new SqlParameter("@AEP007", memo));
                paraList.Add(new SqlParameter("@AEP009", vender));
                paraList.Add(new SqlParameter("@AEP012", vdate));
                paraList.Add(new SqlParameter("@AEP013", sms));
                paraList.Add(new SqlParameter("@AEP014", win_desc));
                paraList.Add(new SqlParameter("@AEP001", reward_key));
            }
            else
            {
                strSQL = "update activity_rewards set AEP005=@AEP005,AEP007=@AEP007,AEP009=@AEP009,AEP011=@AEP011,AEP012=@AEP012,AEP013=@AEP013," +
                            "AEP014=@AEP014 where AEP001=@AEP001";
                paraList.Add(new SqlParameter("@AEP005", name));
                paraList.Add(new SqlParameter("@AEP007", memo));
                paraList.Add(new SqlParameter("@AEP009", vender));
                paraList.Add(new SqlParameter("@AEP011", file.FileName));
                paraList.Add(new SqlParameter("@AEP012", vdate));
                paraList.Add(new SqlParameter("@AEP013", sms));
                paraList.Add(new SqlParameter("@AEP014", win_desc));
                paraList.Add(new SqlParameter("@AEP001", reward_key));
            }           
           

            DataBaseControl dbCtl = new DataBaseControl();

            try
            {
                //var fileName = System.IO.Path.GetFileName(rwdFile.FileName);
                //var fileExtension = System.IO.Path.GetExtension(rwdFile.FileName);
                if (file != null)
                {
                    var path = System.IO.Path.Combine(filepath, file.FileName);
                    file.SaveAs(path);
                }

                dbCtl.Open();
                iAffrect = dbCtl.ExecuteCommad(strSQL, paraList);

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

            return iAffrect;
        }


        public DataTable getRewardEarnList(string event_no, out THC_Library.Error error)
        {
                //: "QRC001", bVisible: false },
                //{ "title": "序號", "data": "QRC004", bVisible: true },
                //{ "title": "獎項碼", "data": "QRC008", bVisible: true },
                //{ "title": "獎項名稱", "data": "QRC011", bVisible: true },
                //{ "title": "掃描時間", "data": "QRC013", bVisible: true },
                //{ "title": "實際碼", "data": "QRC015", bVisible: true },
                //{ "title": "得獎帳號", "data": "QRC016", bVisible: true }

            error = null;
            DataTable resultTable = null;

            IList<SqlParameter> paraList = new System.Collections.Generic.List<SqlParameter>();
            string strSQL = "select QRC001,QRC004,QRC008,QRC011,QRC013,QRC015,QRC016 from qr_record " + 
                            "where QRC002=@QRC002 and QRC016 IS NOT NULL";
            SqlParameter sqlParam = new SqlParameter("@QRC002", SqlDbType.NVarChar);
            sqlParam.Value = event_no;
            paraList.Add(sqlParam);           

            DataBaseControl dbCtl = new DataBaseControl();

            try
            {
                dbCtl.Open();
                resultTable = dbCtl.GetDataTable(strSQL, paraList);
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

            return resultTable;
        }

        public DataTable exportRewardEarn(string event_no, out THC_Library.Error error)
        {
            error = null;
            DataTable resultTable = null;

            IList<SqlParameter> paraList = new System.Collections.Generic.List<SqlParameter>();
            string strSQL = "select QRC001,QRC004,QRC008,QRC011,QRC013,QRC015,QRC016 from qr_record " + 
                            "where QRC002=@QRC002 and QRC016 IS NOT NULL";
            SqlParameter sqlParam = new SqlParameter("@QRC002", SqlDbType.NVarChar);
            sqlParam.Value = event_no;
            paraList.Add(sqlParam);

            DataBaseControl dbCtl = new DataBaseControl();

            try
            {
                dbCtl.Open();
                resultTable = dbCtl.GetDataTable(strSQL, paraList);
                               
               
                //foreach (DataRow row in resultTable.Rows)
                //{
                    
                //}
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

            return resultTable;
        }

        public DataTable getWeather( out THC_Library.Error error)
        {
            error = null;
            DataTable resultTable = null;

            IList<SqlParameter> paraList = new System.Collections.Generic.List<SqlParameter>();
            string strSQL = "select * from weather";
            
            DataBaseControl dbCtl = new DataBaseControl();
            try
            {
                dbCtl.Open();
                resultTable = dbCtl.GetDataTable(strSQL, paraList);
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

            return resultTable;
        }

        public void getScanRate(string event_no, out float scan, out float total, out float rate, out THC_Library.Error error)
        {
            error = null;

            scan = 0;
            total = 0;
            rate = 0;
            IDataReader dataReader;
            IList<SqlParameter> paraList = new System.Collections.Generic.List<SqlParameter>();
            string strSQL = "select count(*) from qr_record where QRC002=@QRC002 and QRC013 is not NULL";
            paraList.Add(new SqlParameter("@QRC002", event_no));

            DataBaseControl dbCtl = new DataBaseControl();
            try
            {
                dbCtl.Open();
                dataReader = dbCtl.GetReader(strSQL, paraList);
                dataReader.Read();
                scan = float.Parse(dataReader[0].ToString());
                dataReader.Close();

                paraList.Clear();
                strSQL = "select AE007 from activity_event where AE002=@AE002";
                paraList.Add(new SqlParameter("@AE002", event_no));
                dataReader = dbCtl.GetReader(strSQL, paraList);
                dataReader.Read();
                total = float.Parse(dataReader[0].ToString());
                dataReader.Close();

                rate = (float)Math.Round((double)(scan / total), 4);
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
        }

        public DataTable getScanArea(string event_no, out THC_Library.Error error)
        {
            error = null;
            DataTable resultTable = null;

            IList<SqlParameter> paraList = new System.Collections.Generic.List<SqlParameter>();
            string strSQL = "select EUR008 as AREA,count(EUR003) as VALUE from event_user_records " +
                            "where EUR002=@EUR002 group by EUR008";
            paraList.Add(new SqlParameter("@EUR002", event_no));

            DataBaseControl dbCtl = new DataBaseControl();
            try
            {
                dbCtl.Open();
                resultTable = dbCtl.GetDataTable(strSQL, paraList);
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

            return resultTable;
        }

        public DataTable getScanAge(string event_no, out THC_Library.Error error)
        {
            error = null;
            DataTable resultTable = null;

            IList<SqlParameter> paraList = new System.Collections.Generic.List<SqlParameter>();
            string strSQL = "select EUR006 as AGE,count(EUR003) as VALUE from event_user_records where EUR002=@EUR002 group by EUR006";
            paraList.Add(new SqlParameter("@EUR002", event_no));

            DataBaseControl dbCtl = new DataBaseControl();
            try
            {
                dbCtl.Open();
                resultTable = dbCtl.GetDataTable(strSQL, paraList);
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

            return resultTable;
        }

        public DataTable getScanGender(string event_no, out THC_Library.Error error)
        {
            error = null;
            DataTable resultTable = null;

            IList<SqlParameter> paraList = new System.Collections.Generic.List<SqlParameter>();
            string strSQL = "select EUR007 as GENDER,count(EUR003) as VALUE from event_user_records " + 
                            "where EUR002=@EUR002 group by EUR007";
            paraList.Add(new SqlParameter("@EUR002", event_no));

            DataBaseControl dbCtl = new DataBaseControl();
            try
            {
                dbCtl.Open();
                resultTable = dbCtl.GetDataTable(strSQL, paraList);
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

            return resultTable;
        }

        public DataTable getScanCount_InDay_7(string event_no, out THC_Library.Error error)
        {
            error = null;
            DataTable resultTable = null;

            DateTime datNow = DateTime.Now;

            IList<SqlParameter> paraList = new System.Collections.Generic.List<SqlParameter>();
            string strSQL = "select  count(EUR003) as VALUE, CAST(EUR004 AS DATE) as DATE from event_user_records " +
                            "where EUR002=@EUR002 and DATEDIFF(day,EUR004,GETDATE()) < 7 " +
                            "group by CAST(EUR004 AS DATE)";

            paraList.Add(new SqlParameter("@EUR002", event_no));

            DataBaseControl dbCtl = new DataBaseControl();
            try
            {
                dbCtl.Open();
                resultTable = dbCtl.GetDataTable(strSQL, paraList);
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

            return resultTable;
        }

        public DataTable getTimeTemptrue(string event_no, string days, out THC_Library.Error error)
        {
            error = null;
            DataTable resultTable = null;

            IList<SqlParameter> paraList = new System.Collections.Generic.List<SqlParameter>();
            string strSQL = "select CAST(EUR004 AS DATE) as DATE,AVG(EUR009) as TEMP,count(*) as COUNT from event_user_records " +
                            "where EUR002=@EUR002 and DATEDIFF(day,EUR004,GETDATE()) < " + days + " " +
                            "group by CAST(EUR004 AS DATE)";
            paraList.Add(new SqlParameter("@EUR002", event_no));

            DataBaseControl dbCtl = new DataBaseControl();
            try
            {
                dbCtl.Open();
                resultTable = dbCtl.GetDataTable(strSQL, paraList);
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

            return resultTable;
        }

        public DataTable getTimeCountByArea(string event_no, string days, out THC_Library.Error error)
        {
//            select CAST(EUR004 AS DATE) as DATE,count(*),WH004 as COUNT 
//from event_user_records left join weather on EUR008=WH001
// where EUR002=1024 and DATEDIFF(day,EUR004,GETDATE()) < 21 
// group by CAST(EUR004 AS DATE), WH004

            error = null;
            DataTable resultTable = null;

            IList<SqlParameter> paraList = new System.Collections.Generic.List<SqlParameter>();
            string strSQL = "select CAST(EUR004 AS DATE) as DATE,count(*) as COUNT,WH004 as AREA " +
                            "from event_user_records left join weather on EUR008=WH001 " + 
                            "where EUR002=@EUR002 and DATEDIFF(day,EUR004,GETDATE()) < " + days + " " +
                            " group by CAST(EUR004 AS DATE), WH004 order by DATE";

            paraList.Add(new SqlParameter("@EUR002", event_no));

            DataBaseControl dbCtl = new DataBaseControl();
            try
            {
                dbCtl.Open();
                resultTable = dbCtl.GetDataTable(strSQL, paraList);
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

            return resultTable;
        }

        /// <summary>
        /// 參與率次數比例
        /// </summary>       
        public DataTable getScanRate(string event_no, string counter, out string total, out THC_Library.Error error)
        {
            error = null;
            total = "";
            //select EUR005,count(EUR005) as cc from event_user_records
            //where EUR002=1033
            //group by EUR005
            //having count(EUR005) > 2

            error = null;
            DataTable resultTable = null;

            IList<SqlParameter> paraList = new System.Collections.Generic.List<SqlParameter>();
            string strSQL = "select EUR005 as acc,count(EUR005) as cc,CM007 as tel from event_user_records " +
                            "left join consumer_member on EUR005=CM002 " +
                            "where EUR002=@EUR002 group by EUR005,CM007 having count(EUR005) >= @counter " +
                            "order by cc";

            paraList.Add(new SqlParameter("@EUR002", event_no));
            paraList.Add(new SqlParameter("@counter", counter));

            DataBaseControl dbCtl = new DataBaseControl();
            try
            {
                dbCtl.Open();
                resultTable = dbCtl.GetDataTable(strSQL, paraList);

                strSQL = "select count(distinct EUR005) from event_user_records where EUR002=@EUR002";
                paraList.Clear();
                paraList.Add(new SqlParameter("@EUR002", event_no));              
                IDataReader dataReader = dbCtl.GetReader(strSQL, paraList);
                dataReader.Read();
                total = dataReader[0].ToString();
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

            return resultTable;

            return null;
        }

        public void runTimeSetting(HttpPostedFileBase file, out THC_Library.Error error)
        {
            error = null;

            System.IO.StreamReader streamReader = 
                new System.IO.StreamReader(file.InputStream, System.Text.Encoding.Default);

            IList<SqlParameter> paraList = new System.Collections.Generic.List<SqlParameter>();
            SqlParameter sqlParam;
            string strSQL;
            string activityId = "", activityName = "";
            DataBaseControl dbCtl = new DataBaseControl();

            try
            {
                dbCtl.Open();
                string activitySection = streamReader.ReadLine();
                if (activitySection.StartsWith("activity:"))
                {
                    //AE001,AE002,AE003,VM003 as AE004,AE006 as AE005,AE007 as AE006,AE009 as AE007,AE013 as AE008,
                    //AE014 as AE009,AE015 as AE010,AE016 as AE011,AE018 as AE012,AE019 as AE013

                    activitySection = activitySection.Replace("activity:", "");
                    dynamic activityJson = Newtonsoft.Json.JsonConvert.DeserializeObject(activitySection);

                    sqlParam = new SqlParameter("@AE001", SqlDbType.Int);
                    sqlParam.Value = activityJson[0].AE001;
                    paraList.Add(sqlParam);
                    sqlParam = new SqlParameter("@AE002", SqlDbType.NVarChar);
                    sqlParam.Value = activityJson[0].AE002;
                    paraList.Add(sqlParam);
                    sqlParam = new SqlParameter("@AE003", SqlDbType.NVarChar);
                    sqlParam.Value = activityJson[0].AE003;
                    paraList.Add(sqlParam);
                    sqlParam = new SqlParameter("@AE004", SqlDbType.NVarChar);
                    sqlParam.Value = activityJson[0].AE004;
                    paraList.Add(sqlParam);
                    sqlParam = new SqlParameter("@AE005", SqlDbType.VarChar);
                    sqlParam.Value = activityJson[0].AE005;
                    paraList.Add(sqlParam);
                    sqlParam = new SqlParameter("@AE006", SqlDbType.VarChar);
                    sqlParam.Value = activityJson[0].AE006;
                    paraList.Add(sqlParam);
                    sqlParam = new SqlParameter("@AE007", SqlDbType.Int);
                    sqlParam.Value = activityJson[0].AE007;
                    paraList.Add(sqlParam);
                    sqlParam = new SqlParameter("@AE008", SqlDbType.NVarChar);
                    sqlParam.Value = activityJson[0].AE008;
                    paraList.Add(sqlParam);
                    sqlParam = new SqlParameter("@AE009", SqlDbType.VarChar);
                    sqlParam.Value = activityJson[0].AE009;
                    paraList.Add(sqlParam);
                    sqlParam = new SqlParameter("@AE010", SqlDbType.Char);
                    sqlParam.Value = activityJson[0].AE010;
                    paraList.Add(sqlParam);
                    sqlParam = new SqlParameter("@AE011", SqlDbType.NVarChar);
                    sqlParam.Value = activityJson[0].AE011;
                    paraList.Add(sqlParam);
                    sqlParam = new SqlParameter("@AE012", SqlDbType.SmallInt);
                    sqlParam.Value = activityJson[0].AE012;
                    paraList.Add(sqlParam);
                    sqlParam = new SqlParameter("@AE013", SqlDbType.VarChar);
                    sqlParam.Value = activityJson[0].AE013;
                    paraList.Add(sqlParam);

                    strSQL = "insert into activity_event (AE001,AE002,AE003,AE004,AE005,AE006,AE007," +
                             "AE008,AE009,AE010,AE011,AE012,AE013) values (@AE001,@AE002,@AE003,@AE004,@AE005,@AE006,@AE007," +
                             "@AE008,@AE009,@AE010,@AE011,@AE012,@AE013)";
                    dbCtl.ExecuteCommad(strSQL, paraList);

                    activityId = activityJson[0].AE002.ToString();
                    activityName = activityJson[0].AE003.ToString();
                }
                else
                {
                    throw new THC_Library.THCException(9000, "轉檔內容無活動設定資料");
                }

                string rewardSection = streamReader.ReadLine();
                if (rewardSection.StartsWith("reward:"))
                {
                    //AE001,AE002,AE003,VM003 as AE004,AE006 as AE005,AE007 as AE006,AE009 as AE007,AE013 as AE008,
                    //AE014 as AE009,AE015 as AE010,AE016 as AE011,AE018 as AE012,AE019 as AE013

                    rewardSection = rewardSection.Replace("reward:", "");
                    dynamic rewardJson = Newtonsoft.Json.JsonConvert.DeserializeObject(rewardSection);

                    foreach (dynamic reward in rewardJson)
                    {
                        paraList.Clear();
                        sqlParam = new SqlParameter("@AEP001", SqlDbType.Int);
                        sqlParam.Value = reward.AEP001;
                        paraList.Add(sqlParam);
                        sqlParam = new SqlParameter("@AEP002", SqlDbType.Int);
                        sqlParam.Value = reward.AEP002;
                        paraList.Add(sqlParam);
                        sqlParam = new SqlParameter("@AEP003", SqlDbType.Char);
                        sqlParam.Value = reward.AEP003;
                        paraList.Add(sqlParam);
                        sqlParam = new SqlParameter("@AEP004", SqlDbType.TinyInt);
                        sqlParam.Value = reward.AEP004;
                        paraList.Add(sqlParam);
                        sqlParam = new SqlParameter("@AEP005", SqlDbType.NVarChar);
                        sqlParam.Value = reward.AEP005;
                        paraList.Add(sqlParam);
                        sqlParam = new SqlParameter("@AEP006", SqlDbType.Int);
                        sqlParam.Value = reward.AEP006;
                        paraList.Add(sqlParam);
                        sqlParam = new SqlParameter("@AEP007", SqlDbType.NVarChar);
                        sqlParam.Value = reward.AEP007;
                        paraList.Add(sqlParam);
                        sqlParam = new SqlParameter("@AEP008", SqlDbType.DateTime);
                        sqlParam.Value = reward.AEP008;
                        paraList.Add(sqlParam);
                        sqlParam = new SqlParameter("@AEP009", SqlDbType.Int);
                        sqlParam.Value = reward.AEP009;
                        paraList.Add(sqlParam);
                        sqlParam = new SqlParameter("@AEP010", SqlDbType.Int);
                        if (reward.AEP010 == null)
                        {
                            sqlParam.Value = DBNull.Value;
                        }
                        else
                        {
                            sqlParam.Value = reward.AEP010;
                        }

                        paraList.Add(sqlParam);
                        sqlParam = new SqlParameter("@AEP011", SqlDbType.VarChar);
                        sqlParam.Value = reward.AEP011;
                        paraList.Add(sqlParam);
                        sqlParam = new SqlParameter("@AEP012", SqlDbType.VarChar);
                        sqlParam.Value = reward.AEP012;
                        paraList.Add(sqlParam);
                        sqlParam = new SqlParameter("@AEP013", SqlDbType.Text);
                        sqlParam.Value = reward.AEP013;
                        paraList.Add(sqlParam);
                        sqlParam = new SqlParameter("@AEP014", SqlDbType.VarChar);
                        sqlParam.Value = reward.AEP014;
                        paraList.Add(sqlParam);
                        strSQL = "insert into activity_rewards (AEP001,AEP002,AEP003,AEP004,AEP005,AEP006,AEP007," +
                                 "AEP008,AEP009,AEP010,AEP011,AEP012,AEP013,AEP014) values (@AEP001,@AEP002,@AEP003,@AEP004,@AEP005,@AEP006,@AEP007," +
                                 "@AEP008,@AEP009,@AEP010,@AEP011,@AEP012,@AEP013,@AEP014)";
                        dbCtl.ExecuteCommad(strSQL, paraList);
                    }
                    
                }
                else
                {
                    throw new THC_Library.THCException(9000, "轉檔內容無獎項設定資料");
                }
               
                string codeSection = streamReader.ReadLine();
                dynamic codeJson = Newtonsoft.Json.JsonConvert.DeserializeObject(codeSection);

                strSQL = "insert into qr_record (QRC001,QRC002,QRC003,QRC004,QRC005,QRC006,QRC007," +
                              "QRC008,QRC015,QRC018) values (@QRC001,@QRC002,@QRC003," +
                              "@QRC004,@QRC005,@QRC006,@QRC007,@QRC008,@QRC015,@QRC018)";
                foreach (dynamic code in codeJson)
                {

                    paraList.Clear();
                    sqlParam = new SqlParameter("@QRC001", SqlDbType.Int);
                    sqlParam.Value = code.EQC001;
                    paraList.Add(sqlParam);
                    sqlParam = new SqlParameter("@QRC002", SqlDbType.NVarChar);
                    sqlParam.Value = activityId;
                    paraList.Add(sqlParam);
                    sqlParam = new SqlParameter("@QRC003", SqlDbType.NVarChar);
                    sqlParam.Value = activityName;
                    paraList.Add(sqlParam);
                    sqlParam = new SqlParameter("@QRC004", SqlDbType.Int);
                    sqlParam.Value = code.EQC002;
                    paraList.Add(sqlParam);
                    sqlParam = new SqlParameter("@QRC005", SqlDbType.VarChar);
                    sqlParam.Value = code.EQC003;
                    paraList.Add(sqlParam);
                    sqlParam = new SqlParameter("@QRC006", SqlDbType.Char);
                    sqlParam.Value = code.EQC004;
                    paraList.Add(sqlParam);
                    sqlParam = new SqlParameter("@QRC007", SqlDbType.Char);
                    sqlParam.Value = code.EQC005;
                    paraList.Add(sqlParam);
                    sqlParam = new SqlParameter("@QRC008", SqlDbType.VarChar);
                    if (code.EC.ToString().Length > 0)
                    {
                        sqlParam.Value = code.EC;
                    }
                    else
                    {
                        sqlParam.Value = DBNull.Value;
                    }                    
                    paraList.Add(sqlParam);
                    sqlParam = new SqlParameter("@QRC015", SqlDbType.VarChar);
                    sqlParam.Value = string.Format("{0}{1}{2}",
                                        code.EQC003, code.EQC004, code.EQC005);
                    paraList.Add(sqlParam);
                    sqlParam = new SqlParameter("@QRC018", SqlDbType.Int);
                    if (code.EQC007 == null)
                    {
                        sqlParam.Value = DBNull.Value;
                    }
                    else
                    {
                        sqlParam.Value = code.EQC007;
                    }                    
                    paraList.Add(sqlParam);

                    dbCtl.ExecuteCommad(strSQL, paraList);

                    
                    /*
                     * {
"img": "0DFszj05.jpg",
"v_date" : "2017/11/30"
}
                     */
                }
                // EQC001,EQC002,EQC003,EQC004,EQC005,EQC007,EC,AEP003,AEP004,AEP005,AEP011,AEP012,AEP013
                /*
QRC001	Int	PK		1.0
QRC002	nvarchar(20)	事件代碼 專案編號		1.0
QRC003	nvarchar(20)	事件名稱		1.0
QRC004	int	序號		1.0
QRC005	varchar(10)	QR CODE 亂碼		1.0
QRC006	Char(1)	序號補碼		1.0
QRC007	Char(1)	QR補碼		1.0
QRC008	varchar(50)	EC		1.0
QRC009	char(1)	獎項型態	0 虛擬 1 實體	1.0
QRC010	tinyint	獎項層級	0立即中獎/ 1掃描參加遊戲在立即中獎/2非立即中獎	1.0
QRC011	nvarchar(20)	獎項名稱		1.0
#QRC012	int	掃描次數	Default 0	1.0
#QRC013	datetime	掃描時間	null	1.0
#QRC014	int	記錄檔PK	null	1.0
QRC015	varchar(10)	實際碼	QRC005+ QRC006+ QRC007	1.0
#QRC016	varchar(100)	得獎mail帳號	null	1.0
QRC017	Nvarchar(250)	獎項資料 json 格式	Null 包含圖示、有效時間等資料	1.0
QRC018	text	簡訊內容	17/09/09	

                 */

            }
            catch (THC_Library.THCException THCEx)
            {
                error = new THC_Library.Error();
                error.Number = THCEx.Number;
                error.ErrorMessage = THCEx.Message;
            }
            catch (Exception ex)
            {
                error = new THC_Library.Error();
                error.ErrorMessage = ex.ToString();
            }
            finally
            {
                streamReader.Close();
            }
            
        }
        
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace WebHTCBackEnd.Models.Events
{
    public class THC_EventExport : DataEntity
    {
        //select EQC001,EQC002,EQC003,EQC004,EQC005,EC,AEP003,AEP004,AEP005,AEP006,AE003,AE013,AE014
        //from event_qrcode_d as a left join activity_event_prize as b
        //on a.EQC007=b.AEP001 inner join activity_event as c on a.EQC001=c.AE002

        public DataTable getQRRecords(string event_key, out Error.Error error)
        {
            error = null;
            DataTable returnTable = null;
            string strSQL = "select AE002,AE003,b.* from activity_event as a inner join export_log as b " +
                           "on AE001=EPL002 where AE002=@AE002 ";
            try
            {
                dbControl.Open();
                returnTable = dbControl.GetDataTable(strSQL, paraList);
                paraList.Clear();
                if (returnTable.Rows.Count == 0)
                {
                    strSQL = "select AE001 from activity_event where AE002=@AE002";
                    paraList.Add(new SqlParameter("@AE002", event_key));
                    IDataReader dataReader = dbControl.GetReader(strSQL, paraList);
                    dataReader.Read();
                    event_key = dataReader[0].ToString();
                    dataReader.Close();
                }
                else
                {
                    event_key = returnTable.Rows[0]["EPL002"].ToString();
                }
                
                
            }
            catch (Exception ex)
            {
                error = new Error.Error();
                error.Number = 0;
                error.ErrorMessage = ex.ToString();
            }
            finally
            {
                dbControl.Close();
            }
            return returnTable;
        }

        public DataTable queryExportSearch(string event_no, string event_name, out string event_key, out string[] top10_events, out Error.Error error)
        {
            error = null;
            top10_events = null;
            event_key = "";
            paraList.Clear();
            
            //string strSQL = "select AE002,AE003,b.* from activity_event as a inner join export_log as b " +
            //                "on AE001=EPL002 where AE002=@AE002 ";

            //paraList.Add(new SqlParameter("@AE002", event_no));  

            string strSQL = "select AE001,EQC001,EQC002,EQC003,EQC004,EQC005,EC,AEP003,AEP004,AEP005,AEP006,AE003,AE013,AE014 " +
                            "from event_qrcode_d as a left join activity_event_prize as b " +
                            "on a.EQC007 = b.AEP001 inner join activity_event as c on a.EQC001=c.AE001 " +
                            "where AE002=@AE002 and AE003=@AE003 and EC<>''";
            paraList.Add(new SqlParameter("@AE002", event_no));
            paraList.Add(new SqlParameter("@AE003", event_name));

            DataTable returnTable = null;
            try
            {
                dbControl.Open();
                returnTable = dbControl.GetDataTable(strSQL, paraList);                                
                paraList.Clear();
                if (returnTable.Rows.Count == 0)
                {
                    strSQL = "select AE001 from activity_event where AE002=@AE002";
                    paraList.Add(new SqlParameter("@AE002", event_no));
                    IDataReader dataReader = dbControl.GetReader(strSQL, paraList);
                    dataReader.Read();
                    event_key = dataReader[0].ToString();
                    dataReader.Close();
                }
                else
                {
                    event_key = returnTable.Rows[0]["AE001"].ToString();
                }
                paraList.Clear();
                strSQL = "select top 10 AE002,AE003 from activity_event order by AE001 desc";
                DataTable top10Table = dbControl.GetDataTable(strSQL, paraList);
                top10_events = new string[top10Table.Rows.Count];
                for (int i = 0; i < top10Table.Rows.Count; i++)
                {
                    top10_events[i] = string.Format("{0}-{1}",
                                            top10Table.Rows[i][0].ToString(),
                                            top10Table.Rows[i][1].ToString());
                }
            }
            catch (Exception ex)
            {
                error = new Error.Error();
                error.Number = 0;
                error.ErrorMessage = ex.ToString();
            }
            finally
            {
                dbControl.Close();
            }
            return returnTable;
        }

        public DataTable queryExportLog(string event_key, out Error.Error error)
        {
            error = null;    
            paraList.Clear();

            string strSQL = "select AE002,AE003,b.* from activity_event as a inner join export_log as b " +
                            "on a.AE001=EPL002 " +
                            "where EPL002=@EPL002 ";

            paraList.Add(new SqlParameter("@EPL002", event_key));                       
            DataTable returnTable = null;
            try
            {
                dbControl.Open();
                returnTable = dbControl.GetDataTable(strSQL, paraList);   
            }
            catch (Exception ex)
            {
                error = new Error.Error();
                error.Number = 0;
                error.ErrorMessage = ex.ToString();
            }
            finally
            {
                dbControl.Close();
            }
            return returnTable;
        }

        public DataTable addNewExport(string fullfilepath, string event_key, string employee, out int newid, out string now, out Error.Error error)
        {
            error = null;
            //int iNewId = -1;
            DataTable returnTable = null;
            StreamWriter smWriter = null;
            newid = -1;
            
            DateTime datNow = DateTime.Now;
            now = datNow.ToString();
            paraList.Clear();
            string strSQL;
            //string strSQL = "select EQC001,EQC002,EQC003,EQC004,EQC005,EC,AEP003,AEP004,AEP005,AEP006,AEP011,AEP012,AEP013,AEP014,AE002,AE003,AE013,AE014,AE019 " +
            //                "from event_qrcode_d as a left join activity_event_prize as b " +
            //                "on a.EQC007 = b.AEP001 inner join activity_event as c on a.EQC001=c.AE001 " +
            //                "where AE001=@AE001 ";
            //paraList.Add(new SqlParameter("@AE001", event_key));

            //事件key,序號,QR碼,序號補碼,QR補碼,EC,獎項型態,獎項層級,獎項名稱,獎項數量,獎項圖檔檔名,有效時間,簡訊內容,兌獎網址,事件代碼,事件名稱,商品系列,AE014

            try
            {
                smWriter = new StreamWriter(fullfilepath, false, System.Text.Encoding.UTF8);
                //string strLine;

                dbControl.Open();

                //活動
                strSQL = "select AE001,AE002,AE003,VM003 as AE004,AE006 as AE005,AE007 as AE006,AE009 as AE007,AE013 as AE008,AE014 as AE009," +
                            "AE015 as AE010,AE016 as AE011,AE018 as AE012,AE019 as AE013 " +
                            "from activity_event inner join vender_member on AE004=VM002 where AE001=@AE001";
                paraList.Clear();
                paraList.Add(new SqlParameter("@AE001", event_key));
                DataTable evetnTable = dbControl.GetDataTable(strSQL, paraList);
                string eventJson = Newtonsoft.Json.JsonConvert.SerializeObject(evetnTable);
                evetnTable.Dispose();
                evetnTable = null;
                smWriter.WriteLine("activity:" + eventJson);

                //獎項
                strSQL = "select * from activity_event_prize where AEP002=@AEP002";
                paraList.Clear();
                paraList.Add(new SqlParameter("@AEP002", event_key));
                DataTable rewardTable = dbControl.GetDataTable(strSQL, paraList);
                string rewardJson = Newtonsoft.Json.JsonConvert.SerializeObject(rewardTable);
                rewardTable.Dispose();
                rewardTable = null;
                smWriter.WriteLine("reward:" + rewardJson);

                //發碼
                strSQL = "select EQC001,EQC002,EQC003,EQC004,EQC005,EQC007,EC,AEP003,AEP004,AEP005,AEP011,AEP012,AEP013 " +
                         "from event_qrcode_d left join activity_event_prize " + 
                         "on EQC007=AEP001 " +
                         "where EQC001=@EQC001 ";
                paraList.Clear();
                paraList.Add(new SqlParameter("@EQC001", event_key));
                returnTable = dbControl.GetDataTable(strSQL, paraList);
                string qrJson = Newtonsoft.Json.JsonConvert.SerializeObject(returnTable);
                returnTable.Dispose();
                returnTable = null;
                smWriter.WriteLine(qrJson);

                paraList.Clear();
                strSQL = strSQL = "insert into export_log (EPL002,EPL003,EPL004) " +
                            "values (@EPL002,@EPL003,@EPL004);" +
                            "select SCOPE_IDENTITY();";
                paraList.Add(new SqlParameter("@EPL002", event_key));
                paraList.Add(new SqlParameter("@EPL003", employee));
                paraList.Add(new SqlParameter("@EPL004", datNow));

                object objNewId = dbControl.ExecuteScalar(strSQL, paraList);
                newid = int.Parse(objNewId.ToString());


            }
            catch (Exception ex)
            {
                error = new Error.Error();
                error.Number = 0;
                error.ErrorMessage = ex.ToString();
            }
            finally
            {
                dbControl.Close();
                if (smWriter != null)
                {
                    smWriter.Close();
                }
            }

            return returnTable;

        }
    }


}
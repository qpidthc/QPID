using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace WebHTCBackEnd.Models.Events
{
    public class THC_EventReward : DataEntity
    {       
        public DataTable queryRewardByEventNo(string event_no, out Error.Error error)
        {
            error = null;
            paraList.Clear();
            string strSQL = "select a.*,b.AE002 from activity_event_prize as a inner join activity_event as b " +
                            "on a.AEP002=b.AE001 " +
                            "where b.AE002=@AE002";
            paraList.Add(new SqlParameter("@AE002", event_no));
            DataTable returnTable = null;

            try
            {
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

        public DataTable queryRewardByKey(string key, out Error.Error error)
        {
            error = null;
            paraList.Clear();
            string strSQL = "select a.*,b.AE002,b.AE003,c.VM003 from activity_event_prize as a inner join (activity_event as b " +
                            "inner join vender_member as c on AE004=VM002) " +
                            "on AEP002=AE001  " +
                            " where AEP001=@AEP001";
            paraList.Add(new SqlParameter("@AEP001", key));
            DataTable returnTable = null;
            try
            {
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

        public DataTable queryRewardSearch(string event_no, string event_name,
                                    out string eventkey, out string eventno, out string eventname, out string venderno,
                                    out string vendername, out string[] top10_events, out Error.Error error)
        {
            error = null;
            top10_events = null;
            eventkey = "";
            eventno = "";
            eventname = "";
            venderno = "";
            vendername = "";
            paraList.Clear();
            string strAppend = "";

            string strSQL = "select AE001,AE002,AE003,AE004,VM003 from activity_event inner join vender_member " +
                            "on AE004=VM002";

            if (!string.IsNullOrEmpty(event_no))
            {
                strAppend += " and AE002=@AE002";
                paraList.Add(new SqlParameter("@AE002", event_no));
            }
            if (!string.IsNullOrEmpty(event_name))
            {
                strAppend += " and AE003=@AE003";
                paraList.Add(new SqlParameter("@AE003", event_name));
            }
            if (strAppend.Length == 0)
            {
                throw new Exception("No paramters");
            }
            else
            {
                strSQL += " where 1=1" + strAppend;
            }

            DataTable returnTable = null;            
            try
            {
                dbControl.Open();
                IDataReader dataReader = dbControl.GetReader(strSQL, paraList);
                if (dataReader.Read())
                {
                    eventkey = dataReader["AE001"].ToString();
                    eventno = dataReader["AE002"].ToString().Trim();
                    eventname = dataReader["AE003"].ToString().Trim();
                    venderno = dataReader["AE004"].ToString().Trim();
                    vendername = dataReader["VM003"].ToString().Trim();
                }
                dataReader.Close();
                if (eventkey.Length > 0)
                {
                    paraList.Clear();
                    strSQL = "select a.*,b.RT002,c.RDWV002 from (activity_event_prize as a left join reward_type as b " +
                             "on AEP003=RT004) left join reward_vender as c on  AEP009=RDWV004 " +
                             "where AEP002=@AEP002 and RT001=@RT001 and RDWV001=@RDWV001";

                    paraList.Add(new SqlParameter("@AEP002", eventkey));
                    paraList.Add(new SqlParameter("@RT001", Language.LanguageBase.CURRENT_LANGUAGE));
                    paraList.Add(new SqlParameter("@RDWV001", Language.LanguageBase.CURRENT_LANGUAGE));
                    returnTable = dbControl.GetDataTable(strSQL, paraList);
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

       

        public int addNewReward(string event_key, string rwd_type, string rwd_level, string rwd_name,
                        string rwd_number, string rwd_memo, string rwd_vender, string assign, string rwd_image,
                        string rwd_effective, string rwd_sms, out Error.Error error)
        {
            error = null;
            int iNewId = -1;
            paraList.Clear();

            string strSQL = strSQL = "insert into activity_event_prize (AEP002,AEP003,AEP004,AEP005,AEP006,AEP007,AEP009,AEP010,AEP011,AEP012,AEP013) " +
                            "values (@AEP002,@AEP003,@AEP004,@AEP005,@AEP006,@AEP007,@AEP009,@AEP010,@AEP011,@AEP012,@AEP013);" +
                            "select SCOPE_IDENTITY();";
            
            try
            {   
                paraList.Add(new SqlParameter("@AEP002", event_key));
                paraList.Add(new SqlParameter("@AEP003", rwd_type));
                paraList.Add(new SqlParameter("@AEP004", rwd_level));
                paraList.Add(new SqlParameter("@AEP005", rwd_name));
                paraList.Add(new SqlParameter("@AEP006", rwd_number));
                paraList.Add(new SqlParameter("@AEP007", rwd_memo));
                paraList.Add(new SqlParameter("@AEP009", rwd_vender));
                if (assign.Length == 0)
                {
                    paraList.Add(new SqlParameter("@AEP010", DBNull.Value));
                }
                else
                {
                    paraList.Add(new SqlParameter("@AEP010", assign));
                }
                paraList.Add(new SqlParameter("@AEP011", rwd_image));
                paraList.Add(new SqlParameter("@AEP012", rwd_effective));
                paraList.Add(new SqlParameter("@AEP013", rwd_sms));

                dbControl.Open();
                
                object objNewId = dbControl.ExecuteScalar(strSQL, paraList);
                iNewId = int.Parse(objNewId.ToString());
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

            return iNewId;

        }

        public int updateReward(string rwd_key, string rwd_type, string rwd_level, string rwd_name,
                        string rwd_number, string rwd_memo, string rwd_vender, string assign, string rwd_image, 
                        string rwd_effective, string rwd_sms, out Error.Error error)
        {
            error = null;
            int iAffectedCount = -1;
            paraList.Clear();
            string strSQL = "update activity_event_prize set AEP003=@AEP003,AEP004=@AEP004,AEP005=@AEP005 " +
                            ",AEP006=@AEP006,AEP007=@AEP007,AEP009=@AEP009,AEP010=@AEP010,AEP011=@AEP011 " +
                            ",AEP012=@AEP012,AEP013=@AEP013 where AEP001=@AEP001";
            paraList.Add(new SqlParameter("@AEP003", rwd_type));
            paraList.Add(new SqlParameter("@AEP004", rwd_level));
            paraList.Add(new SqlParameter("@AEP005", rwd_name));
            paraList.Add(new SqlParameter("@AEP006", rwd_number));
            paraList.Add(new SqlParameter("@AEP007", rwd_memo));
            paraList.Add(new SqlParameter("@AEP009", rwd_vender));
            if (assign.Length == 0)
            {
                paraList.Add(new SqlParameter("@AEP010", DBNull.Value));
            }
            else
            {
                paraList.Add(new SqlParameter("@AEP010", assign));
            }
            paraList.Add(new SqlParameter("@AEP011", rwd_image));
            paraList.Add(new SqlParameter("@AEP012", rwd_effective));
            paraList.Add(new SqlParameter("@AEP013", rwd_sms));
            paraList.Add(new SqlParameter("@AEP001", rwd_key));
            

            try
            {
                dbControl.Open();
                iAffectedCount = dbControl.ExecuteCommad(strSQL, paraList);
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

            return iAffectedCount;
        }

        public int deleteReward(string key, out Error.Error error)
        {
            error = null;

            string strCompleted = "";
            int iAffectedCount = -1;
            paraList.Clear();
            string strSQL = "select AEP008 from activity_event_prize where AEP001=@AEP001";
            paraList.Add(new SqlParameter("@AEP001", key));

            try
            {
                dbControl.Open();
                IDataReader dataReader = dbControl.GetReader(strSQL, paraList);               
                if (dataReader.Read())
                {
                    strCompleted = dataReader["AEP008"].ToString();
                }
                else
                {
                    dataReader.Close();
                    throw new Exception("找不到所屬資料");
                }
                dataReader.Close();

                if (strCompleted.Length > 0)
                {
                    throw new Exception("已分配獎項，無法刪除該筆資料");
                }
                               
                paraList.Clear();
                strSQL = "delete from activity_event_prize where AEP001=@AEP001;";
                paraList.Add(new SqlParameter("@AEP001", key));
                iAffectedCount = dbControl.ExecuteCommad(strSQL, paraList);               
            }
            catch (Exception ex)
            {
                dbControl.RollBackTransaction();
                error = new Error.Error();
                error.Number = 0;
                error.ErrorMessage = ex.ToString();
            }
            finally
            {
                dbControl.Close();
            }
            return iAffectedCount;
        }

        public void chechVender(string vid, out Error.Error error)
        {
            error = null;

            string strRWD002 = "";
            paraList.Clear();
            string strSQL = "select RDWV002 from reward_vender where RDWV004=@RDWV004 and RDWV001=@RDWV001";
            paraList.Add(new SqlParameter("@RDWV004", vid));
            paraList.Add(new SqlParameter("@RDWV001", Language.LanguageBase.CURRENT_LANGUAGE));

            try
            {
                dbControl.Open();
                IDataReader dataReader = dbControl.GetReader(strSQL, paraList);
                if (dataReader.Read())
                {
                    strRWD002 = dataReader["RDWV002"].ToString();
                }
                else
                {
                    dataReader.Close();
                    throw new Exception("找不到所屬資料");
                }
                dataReader.Close();

                if (strRWD002.Length == 0)
                {
                    throw new Exception("無所屬獎項廠商");
                }
            }
            catch (Exception ex)
            {
                dbControl.RollBackTransaction();
                error = new Error.Error();
                error.Number = 0;
                error.ErrorMessage = ex.ToString();
            }
            finally
            {
                dbControl.Close();
            }           
        }

        public System.Collections.Generic.List<string> getRewardArrayByjobId(string jobfile, out Error.Error error)
        {
            error = null;
            System.Collections.Generic.List<string> list = new List<string>();
            System.IO.StreamReader smReader = null;
            try
            {
                if (!System.IO.File.Exists(jobfile))
                {
                    throw new Exception("job file not exist");
                }
                smReader = new System.IO.StreamReader(jobfile);
                string strLine;
                while ((strLine = smReader.ReadLine()) != null)
                {
                    if (strLine.Length > 0)
                        list.Add(strLine);
                }
            }
            catch (Exception ex)
            {
                error = new Error.Error();
                error.ErrorMessage = ex.Message;
                error.Number = 0x100;
            }
            return list;      
        }

        
    }

   
}
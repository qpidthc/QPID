using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace WebHTCBackEnd.Models.Events
{
    public class THC_Event : DataEntity
    {
        public THC_Event()
        {
            dbControl = new DB.DataBaseControl();
        }

        public DataTable queryEventByEventNo(string event_no, out Error.Error error)
        {
            paraList.Clear();
            string strSQL = "select a.*,b.VM003 from activity_event as a inner join vender_member as b " +
                            "on a.AE004=b.VM002 where AE002=@AE002";
            paraList.Add(new SqlParameter("@AE002", typeof(string)));
            paraList[0].Value = event_no;

            error = null;
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

            return returnTable;
        }

        public DataSet queryEventSearch(string event_no, string event_name, string vender)
        {
            paraList.Clear();           
            string strSQL = "select a.*,b.VM003 from activity_event as a inner join vender_member as b " +
                            "on a.AE004=b.VM002 where 1=1";
            if (!string.IsNullOrEmpty(event_no))
            {
                strSQL += " and AE002=@AE002";
                paraList.Add(new SqlParameter("@AE002", event_no));
            }
            if (!string.IsNullOrEmpty(event_name))
            {
                strSQL += " and AE003=@AE003";
                paraList.Add(new SqlParameter("@AE003", event_name));
            }
            if (!string.IsNullOrEmpty(vender))
            {
                strSQL += " and VM003=@VM003";
                paraList.Add(new SqlParameter("@VM003", vender));
            }

            DataTable eventTable = dbControl.GetDataTable(strSQL, paraList);
            eventTable.TableName = "events";

            paraList.Clear();  
            strSQL = "select VM002,VM003 from vender_member";
            DataTable venderTable = dbControl.GetDataTable(strSQL, paraList);
            venderTable.TableName = "venders";

            DataSet dsSet = new DataSet();
            dsSet.Tables.Add(eventTable);
            dsSet.Tables.Add(venderTable);
            return dsSet;
        }

        public DataTable queryEventList()
        {
            paraList.Clear();           
            string strSQL = "select a.*,b.VM003 from activity_event as a inner join vender_member as b " +
                            "on a.AE004=b.VM002";           
            DataTable returnTable = dbControl.GetDataTable(strSQL, paraList);
            return returnTable;
        }

        public DataSet queryEventListAndVender()
        {
            paraList.Clear();
            string strSQL = "select a.*,b.VM003 from activity_event as a inner join vender_member as b " +
                            "on a.AE004=b.VM002";
            DataTable eventTable = dbControl.GetDataTable(strSQL, paraList);
            eventTable.TableName = "events";

            strSQL = "select VM002,VM003 from vender_member";
            DataTable venderTable = dbControl.GetDataTable(strSQL, paraList);
            venderTable.TableName = "venders";

            DataSet dsSet = new DataSet();
            dsSet.Tables.Add(eventTable);
            dsSet.Tables.Add(venderTable);
            return dsSet;
        }

        public int addNewEvent(string event_no, string event_name, string vender, string lunch, string start_time,
                        string end_time, string code_id, string code_name, string qr_bit, string serial_bit, string com_bit,
                        string production, string web_url, string event_type, string memo, string length, string page, 
                        out Error.Error error)
        {            
            error = null;
            int iNewId = -1;
            paraList.Clear();

            string strSQL = "select count(AE002) from activity_event where AE002=@AE002";
            paraList.Add(new SqlParameter("@AE002", event_no));

            try
            {
                dbControl.Open();
                IDataReader dataReader = dbControl.GetReader(strSQL, paraList);
                dataReader.Read();
                int iCount = int.Parse(dataReader[0].ToString());
                dataReader.Close();

                if (iCount > 0)
                {
                    throw new Exception("專案編號重覆 !");
                }

                paraList.Clear();

                strSQL = "insert into activity_event (AE002,AE003,AE004,AE005,AE006,AE007,AE008,AE009,AE010,AE011,AE012,AE013,AE014,AE015,AE016,AE017,AE018,AE019) " +
                            "values (@AE002,@AE003,@AE004,@AE005,@AE006,@AE007,@AE008,@AE009,@AE010,@AE011,@AE012,@AE013,@AE014,@AE015,@AE016,@AE017,@AE018,@AE019);" +
                            "select SCOPE_IDENTITY();";              
                paraList.Add(new SqlParameter("@AE002", typeof(string)));
                paraList[0].Value = event_no;
                paraList.Add(new SqlParameter("@AE003", typeof(string)));
                paraList[1].Value = event_name;
                paraList.Add(new SqlParameter("@AE004", typeof(string)));
                paraList[2].Value = vender;
                paraList.Add(new SqlParameter("@AE005", typeof(bool)));
                paraList[3].Value = (lunch == "on" ? true : false);
                paraList.Add(new SqlParameter("@AE006", start_time));
                paraList.Add(new SqlParameter("@AE007", end_time));
                paraList.Add(new SqlParameter("@AE008", code_id));
                paraList.Add(new SqlParameter("@AE009", typeof(int)));
                paraList[7].Value = code_name;
                paraList.Add(new SqlParameter("@AE010", typeof(Int16)));
                paraList[8].Value = qr_bit;
                paraList.Add(new SqlParameter("@AE011", typeof(Int16)));
                paraList[9].Value = serial_bit;
                paraList.Add(new SqlParameter("@AE012", typeof(Int16)));
                paraList[10].Value = com_bit;
                paraList.Add(new SqlParameter("@AE013", production));
                paraList.Add(new SqlParameter("@AE014", web_url));
                paraList.Add(new SqlParameter("@AE015", event_type));
                paraList.Add(new SqlParameter("@AE016", memo));
                paraList.Add(new SqlParameter("@AE017", typeof(int)));
                paraList[15].Value =  0;
                paraList.Add(new SqlParameter("@AE018", typeof(short)));
                paraList[16].Value = length;
                paraList.Add(new SqlParameter("@AE019", typeof(string)));
                paraList[17].Value = page;
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

        public int updateEvent(string event_no, string event_name, string vender, string lunch, string start_time,
                        string end_time, string code_id, string code_name, string qr_bit, string serial_bit, string com_bit,
                        string production, string web_url, string event_type, string memo, string length, string page, 
                         out Error.Error error)
        {
            error = null;
            int iAffectedCount = -1;
            paraList.Clear();
            string strSQL = "update activity_event set AE003=@AE003,AE004=@AE004,AE005=@AE005,AE006=@AE006," +
                            "AE007=@AE007,AE008=@AE008,AE009=@AE009,AE010=@AE010,AE011=@AE011,AE012=@AE012, " +
                            "AE013=@AE013,AE014=@AE014,AE015=@AE015,AE016=@AE016,AE018=@AE018,AE019=@AE019 " +
                            "where AE002=@AE002";

            paraList.Add(new SqlParameter("@AE003", event_name));
            paraList.Add(new SqlParameter("@AE004", vender));
            paraList.Add(new SqlParameter("@AE005", typeof(bool)));
            paraList[2].Value = (lunch == "on" ? true : false);
            paraList.Add(new SqlParameter("@AE006", start_time));
            paraList.Add(new SqlParameter("@AE007", end_time));
            paraList.Add(new SqlParameter("@AE008", code_id));
            paraList.Add(new SqlParameter("@AE009", typeof(int)));
            paraList[6].Value = code_name;
            paraList.Add(new SqlParameter("@AE010", typeof(Int16)));
            paraList[7].Value = qr_bit;
            paraList.Add(new SqlParameter("@AE011", typeof(Int16)));
            paraList[8].Value = serial_bit;
            paraList.Add(new SqlParameter("@AE012", typeof(Int16)));
            paraList[9].Value = com_bit;
            paraList.Add(new SqlParameter("@AE013", production));
            paraList.Add(new SqlParameter("@AE014", web_url));
            paraList.Add(new SqlParameter("@AE015", event_type));
            paraList.Add(new SqlParameter("@AE016", memo));
            paraList.Add(new SqlParameter("@AE002", event_no));
            paraList.Add(new SqlParameter("@AE018", typeof(short)));
            paraList[15].Value = length;
            paraList.Add(new SqlParameter("@AE019", typeof(string)));
            paraList[16].Value = page;

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

        public int deleteEvent(string event_no, out Error.Error error)
        {
            error = null;
            int iAffectedCount = -1;
            paraList.Clear();
            string strSQL = "delete from activity_event where AE002=@AE002";
            paraList.Add(new SqlParameter("@AE002", event_no));

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
    }
}
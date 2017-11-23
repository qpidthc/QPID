using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace WebHTCBackEnd.Models.Events
{
    public class THC_EventCodeGen : DataEntity
    {
        public THC_EventCodeGen()
        {
            dbControl = new DB.DataBaseControl();
        }

        public DataTable queryEventCodeGenList(string event_key, out Error.Error error)
        {
            error = null;
            paraList.Clear();
            string strSQL = "select EQCH001,EQCH003,EQCH007,EQCH008,EQCH009 from event_qrcode_h " +
                            " where EQCH002=@EQCH002";
            paraList.Add(new SqlParameter("@EQCH002", event_key));
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

        public DataTable queryEventCodeGenByKey(string key, out Error.Error error)
        {
            error = null;
            paraList.Clear();
            string strSQL = "select a.*,b.AE001,b.AE003,b.AE010,b.AE011,b.AE017,b.AE018,c.VM003 from event_qrcode_h as a inner join (activity_event as b " +
                            "inner join vender_member as c on AE004=VM002) on EQCH002=AE001  " +
                            " where EQCH001=@EQCH001";
            paraList.Add(new SqlParameter("@EQCH001", key));
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

        public DataTable queryEventCodeGenByEventNo(string event_no, out Error.Error error)
        {
            error = null;
            paraList.Clear();
            string strSQL = "select * from event_qrcode_h where EQCH002=@EQCH002";
            paraList.Add(new SqlParameter("@EQCH002", event_no));
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

        public void queryEventCodeGen(out string[] top10_events, out Error.Error error)
        {
            error = null;
            top10_events = null;
            
            paraList.Clear();
            string strSQL = strSQL = "select top 10 AE002,AE003 from activity_event order by AE001 desc";
            
            try
            {
                dbControl.Open();
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
        }


        public DataTable queryEventCodeGenSearch(string event_no, string event_name,
                                    out string event_key, out string eventno, out string eventname, out string venderno,
                                    out string vendername, out string[] top10_events, out Error.Error error)
        {
            error = null;
            top10_events = null;
            event_key = "";
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
                    event_key = dataReader["AE001"].ToString();
                    eventno = dataReader["AE002"].ToString().Trim();
                    eventname = dataReader["AE003"].ToString().Trim();
                    venderno = dataReader["AE004"].ToString().Trim();
                    vendername = dataReader["VM003"].ToString().Trim();
                }
                dataReader.Close();
                if (eventno.Length > 0)
                {
                    paraList.Clear();
                    strSQL = "select a.*,b.PT002 from event_qrcode_h as a left join product_type as b " +
                                "on EQCH004=PT004 " +
                                "where EQCH002=@EQCH002 and PT001=@PT001 " +
                                "order by EQCH001";
                    paraList.Add(new SqlParameter("@EQCH002", event_key));
                    paraList.Add(new SqlParameter("@PT001", THC_Library.Language.LanguageBase.CURRENT_LANGUAGE));
                    returnTable = dbControl.GetDataTable(strSQL, paraList);
                }

                paraList.Clear();
                strSQL = "select top 10 AE002,AE003 from activity_event order by AE001 desc";
                DataTable top10Table = dbControl.GetDataTable(strSQL, paraList);
                top10_events = new string[top10Table.Rows.Count];
                for (int i = 0; i < top10Table.Rows.Count; i++ )
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

        public DataTable queryEventCodeGenList(bool nodata)
        {
            paraList.Clear();

            string strSQL = "select EQCH001,EQCH002,AE003,VM003,EQCH003,EQCH004,EQCH007,EQCH008,EQCH009 " +
                               "from event_qrcode_h as a inner join " +
                               "activity_event as b on a.EQCH002=b.AE001 " +
                               "inner join vender_member as c " +
                               "on b.AE004=c.VM002";

            if (nodata)
            {
                strSQL += " where 1<>1";
            }           
           
            DataTable returnTable = dbControl.GetDataTable(strSQL, paraList);
            return returnTable;
        }

        public int addNewEventCodeGen(string event_no, string event_name, string gen_datetime, string product_type, string po_number,
                        string serial_number, string gen_number, string employee ,out Error.Error error)
        {
             error = null;
            int iNewId = -1;
            paraList.Clear();

            string strSQL = "select AE001,AE005,AE006,AE007,AE010,AE011,AE018 from activity_event where AE002=@AE002 and AE003=@AE003";
            paraList.Add(new SqlParameter("@AE002", event_no));
            paraList.Add(new SqlParameter("@AE003", event_name));

            try
            {
                bool bExist = false;
                bool bValid = false;
                string event_key = "";
                DateTime datBegin = new DateTime(1990, 1, 1);
                DateTime datExpired = new DateTime(1990, 1, 1);
                DateTime datNow = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                dbControl.Open();
                IDataReader dataReader = dbControl.GetReader(strSQL, paraList);
                if (dataReader.Read())
                {
                    bExist = true;
                    event_key = dataReader["AE001"].ToString();
                    bool.TryParse(dataReader["AE005"].ToString(), out bValid);
                    DateTime.TryParse(dataReader["AE006"].ToString(), out datBegin);
                    DateTime.TryParse(dataReader["AE007"].ToString(), out datExpired);
                }                
                dataReader.Close();

                if (!bExist)
                {
                    throw new Exception("Event doest not exist");
                }
                else if (!bValid)
                {
                    throw new Exception("Event is not opened");
                }
                //else if (datNow.Subtract(datBegin).TotalDays < 0)
                //{
                //    throw new Exception("Event Activity not been actived");
                //}
                //else if (datExpired.Subtract(datNow).TotalDays < 0)
                //{
                //    throw new Exception("Event Activity has been closed");
                //}
                   
                paraList.Clear();
                strSQL = "insert into event_qrcode_h (EQCH002,EQCH003,EQCH004,EQCH005,EQCH006,EQCH007,EQCH008) " +
                            "values (@EQCH002,@EQCH003,@EQCH004,@EQCH005,@EQCH006,@EQCH007,@EQCH008);" +
                            "select SCOPE_IDENTITY();";
                paraList.Add(new SqlParameter("@EQCH002", event_key));
                paraList.Add(new SqlParameter("@EQCH003", gen_datetime));
                paraList.Add(new SqlParameter("@EQCH004", product_type));
                paraList.Add(new SqlParameter("@EQCH005", po_number));
                paraList.Add(new SqlParameter("@EQCH006", serial_number));
                paraList.Add(new SqlParameter("@EQCH007", gen_number));
                paraList.Add(new SqlParameter("@EQCH008", employee));
                //paraList.Add(new SqlParameter("@EQCH009", DBNull.Value));
               
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
               
        public int updateEventCodeGen(string key, string product_type, string po_number, string serial_number, out Error.Error error)
        {
            error = null;
            int iAffectedCount = -1;
            paraList.Clear();
            string strSQL = "update event_qrcode_h set EQCH004=@EQCH004,EQCH005=@EQCH005,EQCH006=@EQCH006 " +                            
                            "where EQCH001=@EQCH001";
            paraList.Add(new SqlParameter("@EQCH004", product_type));
            paraList.Add(new SqlParameter("@EQCH005", po_number));
            paraList.Add(new SqlParameter("@EQCH006", serial_number));
            paraList.Add(new SqlParameter("@EQCH001", key));

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

        public int deleteEventCodeGen(string key, out Error.Error error)
        {
            error = null;
            int iAffectedCount = -1;
            paraList.Clear();
            string strSQL = "delete from event_qrcode_h where EQCH001=@EQCH001;";
            paraList.Add(new SqlParameter("@EQCH001", key));

            try
            {
                dbControl.Open();
                dbControl.BeginTransaction();

                //paraList.Clear();
                //strSQL = "delete from event_qrcode_h where EQCH001=@EQCH001;";
                //paraList.Add(new SqlParameter("@EQCH001", key));
                iAffectedCount = dbControl.ExecuteCommad(strSQL, paraList);

                dbControl.CommintTransaction();
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

        private void GenCode()
        {
            string CODES = "axb0GHIJWcdYe1fi2jkl3mKLpMoNBtOTwUVn4qr5ZsPQRSu6Dv7yzA8CghE9FX";  //62

            int strAE010 = 30; //QR
            int strAE011 = 50; //序號
            int strAE018 = 5; //長度

            char[] chCode = new char[strAE018];
            int rNUmber;
            Random r = new Random();

            int iSerial = 1;
            int iEQCH007 = 10000;  //產生數量
            int iMaxLength = iEQCH007.ToString().Length;

            for (int k = 0; k < iEQCH007; k++)
            {
                //產生隨機Code
                for (int i = 0; i < strAE018; i++)
                {
                    rNUmber = r.Next(62);
                    chCode[i] = CODES[rNUmber];
                }

                int iSum = 0;
                foreach (char ch in chCode)
                {
                    iSum += (int)ch;
                }

                //隨機Code 檢查位元
                int iCheck = iSum % strAE010;
                char chChkSum = CODES[iCheck];

                string str = new string(chCode);
                str += chChkSum.ToString();

                //序號 檢查位元
                int iSerialCheckSum = iSerial % strAE011;
                char chSerialChkSum = CODES[iSerialCheckSum];

                //listBox1.Items.Add(iSerial.ToString().PadLeft(iMaxLength, '0') + "-" + chSerialChkSum.ToString() +
                //                str);
                //MessageBox.Show(chChkSum.ToString() + str);

                iSerial++;
            }


        }

        public DataTable downloadCode(string event_key, string id, out string activity_name, out string url, out Error.Error error)
        {
            error = null;
            activity_name = "";
            url = "";
            DataTable codeTable = null;
            paraList.Clear();

            string strSQL = "select EQC003,EQC004,EQC005 from event_qrcode_d where EQC001=@EQC001 and EQC008=@EQC008 order by EQC002";
            paraList.Add(new SqlParameter("@EQC001", event_key));
            paraList.Add(new SqlParameter("@EQC008", id));

            try
            {
                dbControl.Open();
                codeTable = dbControl.GetDataTable(strSQL, paraList);

                paraList.Clear();
                strSQL = "select AE002,AE014 from activity_event where AE001=@AE001";
                paraList.Add(new SqlParameter("@AE001", event_key));
                IDataReader dataReader = dbControl.GetReader(strSQL, paraList);
                dataReader.Read();
                activity_name = dataReader["AE002"].ToString();
                url = dataReader["AE014"].ToString();
                dataReader.Close();
               
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

            return codeTable;
        }

    }
}
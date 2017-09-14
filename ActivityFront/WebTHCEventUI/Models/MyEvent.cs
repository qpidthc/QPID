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
            string strSQL = "select count(*) from qr_record where QRC013 is not NULL";

            DataBaseControl dbCtl = new DataBaseControl();
            try
            {
                dbCtl.Open();
                dataReader = dbCtl.GetReader(strSQL, paraList);
                dataReader.Read();
                scan = float.Parse(dataReader[0].ToString());
                dataReader.Close();

                strSQL = "select AE007 from activity_event where AE002=@AE002";
                paraList.Add(new SqlParameter("@AE002", event_no));
                dataReader = dbCtl.GetReader(strSQL, paraList);
                dataReader.Read();
                total = float.Parse(dataReader[0].ToString());
                dataReader.Close();

                rate = scan / total;
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


    }
}
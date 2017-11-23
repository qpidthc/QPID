using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using THC_Library;

namespace WebTHCAPP.Models
{
    public class Activity
    {
        public DataTable getActivities( out Error error)
        {
            error = null;
            DataTable resultTable = null;
            string strNow = DateTime.Now.ToString("yyyy/MM/dd");
            SqlParameter sqlParam;
            IList<SqlParameter> paraList = new System.Collections.Generic.List<SqlParameter>();
            string strSQL = "select * from activity_event where AE006>=@AE006 order by AE005";

            THC_Library.DataBase.DataBaseControl dbCtl = new THC_Library.DataBase.DataBaseControl();
            sqlParam = new SqlParameter("@AE006", strNow);
            paraList.Add(sqlParam);
            try
            {               
                dbCtl.Open();

                resultTable = dbCtl.GetDataTable(strSQL, paraList);
            }
            catch (Exception ex)
            {
                error = new Error();
                error.Number = 301;
                error.ErrorMessage = ex.ToString();
            }
            return resultTable;
        }

        public void getActivity(int event_no, out string page, out Error error)
        {
            error = null;
            page = "";            
            string strNow = DateTime.Now.ToString("yyyy/MM/dd");
            SqlParameter sqlParam;
            IList<SqlParameter> paraList = new System.Collections.Generic.List<SqlParameter>();
            string strSQL = "select AE013 from activity_event where AE001=@AE001 order by AE005";

            THC_Library.DataBase.DataBaseControl dbCtl = new THC_Library.DataBase.DataBaseControl();
            sqlParam = new SqlParameter("@AE001", event_no);
            paraList.Add(sqlParam);
            try
            {
                dbCtl.Open();
                IDataReader dataReader = dbCtl.GetReader(strSQL, paraList);
                dataReader.Read();
                page = dataReader["AE013"].ToString();
                dataReader.Close();
            }
            catch (Exception ex)
            {
                error = new Error();
                error.Number = 301;
                error.ErrorMessage = ex.ToString();
            }           
        }

        public void asyncActivity(string activity, out Error error)
        {
            error = null;           
            string strSQL = "";
            SqlParameter sqlParam;
            IList<SqlParameter> paraList = new System.Collections.Generic.List<SqlParameter>();
            THC_Library.DataBase.DataBaseControl dbCtl = new THC_Library.DataBase.DataBaseControl();
            try
            {
                dynamic arrayJson = Newtonsoft.Json.JsonConvert.DeserializeObject(activity);
                dynamic activityObj = arrayJson[0];
                dbCtl.Open();
                dbCtl.BeginTransaction();

                paraList.Clear();
                strSQL = "delete from activity_event where AE001=@AE001";
                sqlParam = new SqlParameter("@AE001", SqlDbType.Int);
                sqlParam.Value = activityObj.AE001;
                paraList.Add(sqlParam);
                dbCtl.ExecuteCommad(strSQL, paraList);

                paraList.Clear();
                sqlParam = new SqlParameter("@AE001", SqlDbType.Int);
                sqlParam.Value = activityObj.AE001;
                paraList.Add(sqlParam);
                sqlParam = new SqlParameter("@AE002", SqlDbType.NVarChar);
                sqlParam.Value = activityObj.AE002;
                paraList.Add(sqlParam);
                sqlParam = new SqlParameter("@AE003", SqlDbType.NVarChar);
                sqlParam.Value = activityObj.AE003;
                paraList.Add(sqlParam);
                sqlParam = new SqlParameter("@AE004", SqlDbType.NVarChar);
                sqlParam.Value = activityObj.AE004;
                paraList.Add(sqlParam);
                sqlParam = new SqlParameter("@AE005", SqlDbType.VarChar);
                sqlParam.Value = activityObj.AE005;
                paraList.Add(sqlParam);
                sqlParam = new SqlParameter("@AE006", SqlDbType.VarChar);
                sqlParam.Value = activityObj.AE006;
                paraList.Add(sqlParam);
                sqlParam = new SqlParameter("@AE007", SqlDbType.Int);
                sqlParam.Value = activityObj.AE007;
                paraList.Add(sqlParam);
                sqlParam = new SqlParameter("@AE008", SqlDbType.NVarChar);
                sqlParam.Value = activityObj.AE008;
                paraList.Add(sqlParam);
                sqlParam = new SqlParameter("@AE009", SqlDbType.VarChar);
                sqlParam.Value = activityObj.AE009;
                paraList.Add(sqlParam);
                sqlParam = new SqlParameter("@AE010", SqlDbType.Char);
                sqlParam.Value = activityObj.AE010;
                paraList.Add(sqlParam);
                sqlParam = new SqlParameter("@AE011", SqlDbType.NVarChar);
                sqlParam.Value = activityObj.AE011;
                paraList.Add(sqlParam);
                sqlParam = new SqlParameter("@AE012", SqlDbType.SmallInt);
                sqlParam.Value = activityObj.AE012;
                paraList.Add(sqlParam);
                sqlParam = new SqlParameter("@AE013", SqlDbType.VarChar);
                sqlParam.Value = activityObj.AE013;
                paraList.Add(sqlParam);

                strSQL = "insert into activity_event values (@AE001,@AE002,@AE003,@AE004,@AE005,@AE006," +
                         "@AE007,@AE008,@AE009,@AE010,@AE011,@AE012,@AE013)";
                dbCtl.ExecuteCommad(strSQL, paraList);

                dbCtl.CommintTransaction();
            }
            catch (Exception ex)
            {
                dbCtl.RollBackTransaction();
                error = new Error();
                error.Number = 305;
                error.ErrorMessage = ex.ToString();
            }
            finally
            {
                dbCtl.Close();
            }


        }

        public void clearLogActivity(string activity, out Error error)
        {
            error = null;
            string strSQL = "";
            SqlParameter sqlParam;
            IList<SqlParameter> paraList = new System.Collections.Generic.List<SqlParameter>();
            THC_Library.DataBase.DataBaseControl dbCtl = new THC_Library.DataBase.DataBaseControl();
            try
            {               
                dbCtl.Open();
                dbCtl.BeginTransaction();
                               
                strSQL = "delete from event_user_records where EUR002=@EUR002";
                sqlParam = new SqlParameter("@EUR002", SqlDbType.Int);
                sqlParam.Value = activity;
                paraList.Add(sqlParam);
                dbCtl.ExecuteCommad(strSQL, paraList);                
                dbCtl.CommintTransaction();
            }
            catch (Exception ex)
            {
                dbCtl.RollBackTransaction();
                error = new Error();
                error.Number = 305;
                error.ErrorMessage = ex.ToString();
            }
            finally
            {
                dbCtl.Close();
            }


        }
    }
}
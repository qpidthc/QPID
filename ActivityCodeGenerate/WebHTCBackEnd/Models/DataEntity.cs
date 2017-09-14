using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace WebHTCBackEnd.Models
{
    public class DataEntity
    {
        protected WebHTCBackEnd.DB.DataBaseControl dbControl;
        protected IList<SqlParameter> paraList = new System.Collections.Generic.List<SqlParameter>();

        public DataEntity()
        {
            dbControl = new DB.DataBaseControl();
        }

        public virtual void queryTop10Events(out string[] top10_events, out Error.Error error)
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
    }
}
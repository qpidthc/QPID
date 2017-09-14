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
    }
}
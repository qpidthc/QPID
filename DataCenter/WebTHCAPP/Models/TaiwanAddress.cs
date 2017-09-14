using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using THC_Library;

namespace WebTHCAPP.Models
{
    public class TaiwanAddress
    {
        public DataTable getCities(out THC_Library.Error error)
        {
            error = null;
            DataTable addrTable = null;
            IList<SqlParameter> paraList = new System.Collections.Generic.List<SqlParameter>();
            string strSQL = "select distinct city,sec from taiwan_map order by sec";

            THC_Library.DataBase.DataBaseControl dbCtl = new THC_Library.DataBase.DataBaseControl();
            try
            {
                dbCtl.Open();
                addrTable = dbCtl.GetDataTable(strSQL, paraList);
            }
            catch (Exception ex)
            {
                error = new Error();
                error.Number = 100;
                error.ErrorMessage = "系統錯誤";
            }
            finally
            {
                dbCtl.Close();
            }
            return addrTable;
        }

        public DataTable getTown(string city, out THC_Library.Error error)
        {
            error = null;
            DataTable addrTable = null;
            IList<SqlParameter> paraList = new System.Collections.Generic.List<SqlParameter>();
            string strSQL = "select distinct town from taiwan_map where city=@city order by town";

            THC_Library.DataBase.DataBaseControl dbCtl = new THC_Library.DataBase.DataBaseControl();
            try
            {
                dbCtl.Open();
                paraList.Add(new SqlParameter("@city", city));
                addrTable = dbCtl.GetDataTable(strSQL, paraList);
            }
            catch (Exception ex)
            {
                error = new Error();
                error.Number = 100;
                error.ErrorMessage = "系統錯誤";
            }
            finally
            {
                dbCtl.Close();
            }
            return addrTable;
        }

        public DataTable getRoad(string city, string town, out THC_Library.Error error)
        {
            error = null;
            DataTable addrTable = null;
            IList<SqlParameter> paraList = new System.Collections.Generic.List<SqlParameter>();
            string strSQL = "select road from taiwan_map where city=@city and town=@town order by road";

            THC_Library.DataBase.DataBaseControl dbCtl = new THC_Library.DataBase.DataBaseControl();
            try
            {
                dbCtl.Open();
                paraList.Add(new SqlParameter("@city", city));
                paraList.Add(new SqlParameter("@town", town));
                addrTable = dbCtl.GetDataTable(strSQL, paraList);
            }
            catch (Exception ex)
            {
                error = new Error();
                error.Number = 100;
                error.ErrorMessage = "系統錯誤";
            }
            finally
            {
                dbCtl.Close();
            }
            return addrTable;
        }
    }
}
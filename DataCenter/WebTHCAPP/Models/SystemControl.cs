using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using THC_Library.DataBase;

namespace WebTHCAPP.Models
{
    public class SystemControl
    {
        public DataTable enterVerify(string account, string access_code, out THC_Library.Error error)
        {
            error = null;
            DataTable resultTable = null;

            byte[] pwdBytes = System.Text.Encoding.Default.GetBytes(access_code);
            System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();
            pwdBytes = md5.ComputeHash(pwdBytes);
            string strPwd = Convert.ToBase64String(pwdBytes);

            IList<SqlParameter> paraList = new System.Collections.Generic.List<SqlParameter>();
            string strSQL = "select AU001,AU003 from activity_user where AU001=@AU001";
            paraList.Add(new SqlParameter("@AU001", account));

            DataBaseControl dbCtl = new DataBaseControl();

            try
            {
                dbCtl.Open();
                IDataReader dataReader = dbCtl.GetReader(strSQL, paraList);
                if (dataReader.Read())
                {
                    string PWD = dataReader["AU003"].ToString();
                    dataReader.Close();
                    if (PWD != strPwd)
                    {
                        throw new Exception("請輸入正確的密碼");
                    }
                }
                else
                {
                    dataReader.Close();
                    throw new Exception("請輸入正確的帳號");
                }

                strSQL = "update activity_user set AU004=@AU004 where AU001=@AU001";
                paraList.Clear();
                paraList.Add(new SqlParameter("@AU004", DateTime.Now));
                paraList.Add(new SqlParameter("@AU001", account));
                dbCtl.ExecuteCommad(strSQL, paraList);

                if (account == "root.admin")
                {
                    strSQL = "select AU001,AU002,AU004 from activity_user where AU001!='root.admin'";
                    paraList.Clear();
                    resultTable = dbCtl.GetDataTable(strSQL, paraList);
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

            return resultTable;

        }
    }
}
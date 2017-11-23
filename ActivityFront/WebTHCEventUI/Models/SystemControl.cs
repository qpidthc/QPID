using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using THC_Library.DataBase;

namespace WebTHCEventUI.Models
{
    internal class SystemControl
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

        public DataTable getUserInfo(string account, out THC_Library.Error error)
        {
            error = null;
            DataTable resultTable = null;
                       
            IList<SqlParameter> paraList = new System.Collections.Generic.List<SqlParameter>();
            string strSQL = "select AU001,AU002,AU003,AU004 from activity_user where AU001=@AU001";
            paraList.Add(new SqlParameter("@AU001", account));

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

        public int addNewAccount(string account, string name, string access_code, out THC_Library.Error error)
        {
            error = null;
            int iExcuteCount = -1;

            byte[] pwdBytes = System.Text.Encoding.Default.GetBytes(access_code);
            System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();
            pwdBytes = md5.ComputeHash(pwdBytes);
            string strPwd = Convert.ToBase64String(pwdBytes);

            IList<SqlParameter> paraList = new System.Collections.Generic.List<SqlParameter>();
            string strSQL = "insert into activity_user (AU001,AU002,AU003) values (@AU001,@AU002,@AU003)";
            paraList.Add(new SqlParameter("@AU001", account));
            paraList.Add(new SqlParameter("@AU002", name));
            paraList.Add(new SqlParameter("@AU003", strPwd));
            
            DataBaseControl dbCtl = new DataBaseControl();

            try
            {
                dbCtl.Open();
                iExcuteCount = dbCtl.ExecuteCommad(strSQL, paraList);
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

            return iExcuteCount;
        }

        public int updateUser(string account, string name, string access_code, out THC_Library.Error error)
        {
            error = null;
            int iExcuteCount = -1;

            IList<SqlParameter> paraList = new System.Collections.Generic.List<SqlParameter>();
            string strSQL = "select AU003 from activity_user where AU001=@AU001";
            paraList.Add(new SqlParameter("@AU001", account));
            
            DataBaseControl dbCtl = new DataBaseControl();

            try
            {
                dbCtl.Open();
                IDataReader dataReader = dbCtl.GetReader(strSQL, paraList);
                dataReader.Read();
                string orgPassword = dataReader["AU003"].ToString();
                dataReader.Close();

                if (orgPassword == access_code)
                {
                    strSQL = "update activity_user set AU002=@AU002 where AU001=@AU001";
                    paraList.Clear();
                    paraList.Add(new SqlParameter("@AU002", name));                   
                    paraList.Add(new SqlParameter("@AU001", account));
                }
                else
                {
                    byte[] pwdBytes = System.Text.Encoding.Default.GetBytes(access_code);
                    System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();
                    pwdBytes = md5.ComputeHash(pwdBytes);
                    string strPwd = Convert.ToBase64String(pwdBytes);

                    strSQL = "update activity_user set AU002=@AU002,AU003=@AU003 where AU001=@AU001";
                    paraList.Clear();
                    paraList.Add(new SqlParameter("@AU002", name));
                    paraList.Add(new SqlParameter("@AU003", strPwd));
                    paraList.Add(new SqlParameter("@AU001", account));
                }

                iExcuteCount = dbCtl.ExecuteCommad(strSQL, paraList);
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

            return iExcuteCount;
        }

        public int deleteUser(string account, out THC_Library.Error error)
        {
            error = null;
            int iExcuteCount = -1;
           
            IList<SqlParameter> paraList = new System.Collections.Generic.List<SqlParameter>();
            string strSQL = "delete from activity_user where AU001=@AU001";
            paraList.Add(new SqlParameter("@AU001", account));

            DataBaseControl dbCtl = new DataBaseControl();

            try
            {
                dbCtl.Open();
                iExcuteCount = dbCtl.ExecuteCommad(strSQL, paraList);
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

            return iExcuteCount;

        }

        public void clearLoginTime(string account, out THC_Library.Error error)
        {
            error = null;
                      
            IList<SqlParameter> paraList = new System.Collections.Generic.List<SqlParameter>();
            string strSQL = "update activity_user set AU004=NULL where AU001=@AU001";
            paraList.Add(new SqlParameter("@AU001", account));

            DataBaseControl dbCtl = new DataBaseControl();

            try
            {
                dbCtl.Open();
                dbCtl.ExecuteCommad(strSQL, paraList);            }
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

        public void changePassword(string account, string old, string new1, string new2, out THC_Library.Error error)
        {
            error = null;

            byte[] pwdBytes = System.Text.Encoding.Default.GetBytes(old);
            System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();
            pwdBytes = md5.ComputeHash(pwdBytes);
            string strOldPwd = Convert.ToBase64String(pwdBytes);

            pwdBytes = System.Text.Encoding.Default.GetBytes(new1);
            md5 = System.Security.Cryptography.MD5.Create();
            pwdBytes = md5.ComputeHash(pwdBytes);
            string strNewPwd = Convert.ToBase64String(pwdBytes);

            IList<SqlParameter> paraList = new System.Collections.Generic.List<SqlParameter>();
            string strSQL = "select AU003 from activity_user where AU001=@AU001";           
            paraList.Add(new SqlParameter("@AU001", account));

            DataBaseControl dbCtl = new DataBaseControl();

            try
            {
                if (new1 != new2)
                {
                    throw new Exception("新密碼不相符");
                }

                dbCtl.Open();

                IDataReader dataReader = dbCtl.GetReader(strSQL, paraList);
                dataReader.Read();
                string strOld = dataReader["AU003"].ToString();
                dataReader.Close();

                if (strOldPwd != strOld)
                {
                    throw new Exception("舊密碼輸入錯誤");
                }


                strSQL = "update activity_user set AU003=@AU003 where AU001=@AU001";
                paraList.Clear();
                paraList.Add(new SqlParameter("@AU003", strNewPwd));
                paraList.Add(new SqlParameter("@AU001", account));
                dbCtl.ExecuteCommad(strSQL, paraList);
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

    internal class ControlUser
    {
        public string Account;
        public bool IsAdmin;
    }
}
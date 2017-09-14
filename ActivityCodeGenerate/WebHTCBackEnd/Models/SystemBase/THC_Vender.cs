using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace WebHTCBackEnd.Models.SystemBase
{
    public class THC_Vender : DataEntity
    {
        public THC_Vender()
        {
            dbControl = new DB.DataBaseControl();
        }

        public DataTable queryVenderByAccountNo(string accno)
        {
            paraList.Clear();
            string strSQL = "select * from vender_member where VM002=@VM002";
            paraList.Add(new SqlParameter("@VM002", accno));
                     
            DataTable returnTable = dbControl.GetDataTable(strSQL, paraList);
            return returnTable;
        }

        public DataTable queryVenderSearch(string accno, string name, string bno)
        {
            paraList.Clear();
            string strSQL = "select * from vender_member where 1=1";
            if (!string.IsNullOrEmpty(accno))
            {
                strSQL += " and VM002=@VM002";
                paraList.Add(new SqlParameter("@VM002", accno));
            }
            if (!string.IsNullOrEmpty(name))
            {
                strSQL += " and VM003=@VM003";
                paraList.Add(new SqlParameter("@VM003", name));
            }
            if (!string.IsNullOrEmpty(bno))
            {
                strSQL += " and VM006=@VM006";
                paraList.Add(new SqlParameter("@VM006", bno));
            }          
           
            DataTable returnTable = dbControl.GetDataTable(strSQL, paraList);
            return returnTable;
        }

        public DataTable queryVenderList()
        {
            paraList.Clear();
            string strSQL = "select * from vender_member";
            DataTable returnTable = dbControl.GetDataTable(strSQL, paraList);
            return returnTable;
        }
               
        public int addNewVender(string accno, string name, string fullname, string bno, string addr,
                        string person1, string tel1, string mail1, string person2, string tel2, string mail2,
                        out Error.Error error)
        {
            error = null;
            int iNewId = -1;
            paraList.Clear();

            string strSQL = "select count(VM002) from vender_member where VM002=@VM002";
            paraList.Add(new SqlParameter("@VM002", accno));
                                                
            try
            {
                dbControl.Open();
                IDataReader dataReader = dbControl.GetReader(strSQL, paraList);
                dataReader.Read();
                int iCount = int.Parse(dataReader[0].ToString());
                dataReader.Close();

                if (iCount > 0)
                {
                    throw new Exception("廠商代號重覆 !");
                }

                paraList.Clear();
                strSQL = "insert into vender_member (VM002,VM003,VM004,VM005,VM006,VM007,VM008,VM009,VM010,VM011,VM012) " +
                            "values (@VM002,@VM003,@VM004,@VM005,@VM006,@VM007,@VM008,@VM009,@VM010,@VM011,@VM012);" +
                            "select SCOPE_IDENTITY();";
                paraList.Add(new SqlParameter("@VM002", accno));
                paraList.Add(new SqlParameter("@VM003", name));
                paraList.Add(new SqlParameter("@VM004", fullname));
                paraList.Add(new SqlParameter("@VM005", bno));
                paraList.Add(new SqlParameter("@VM006", addr));
                paraList.Add(new SqlParameter("@VM007", person1));
                paraList.Add(new SqlParameter("@VM008", tel1));
                paraList.Add(new SqlParameter("@VM009", mail1));
                paraList.Add(new SqlParameter("@VM010", person2));
                paraList.Add(new SqlParameter("@VM011", tel2));
                paraList.Add(new SqlParameter("@VM012", mail2));
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

        public int updateVender(string accno, string name, string fullname, string bno, string addr,
                        string person1, string tel1, string mail1, string person2, string tel2, string mail2,
                        out Error.Error error)
        {
            error = null;
            int iAffectedCount = -1;           
            paraList.Clear();
            string strSQL = "update vender_member set VM003=@VM003,VM004=@VM004,VM005=@VM005,VM006=@VM006," +
                            "VM007=@VM007,VM008=@VM008,VM009=@VM009,VM010=@VM010,VM011=@VM011,VM012=@VM012 " +
                            "where VM002=@VM002";                             
            paraList.Add(new SqlParameter("@VM003", name));
            paraList.Add(new SqlParameter("@VM004", fullname));
            paraList.Add(new SqlParameter("@VM005", bno));
            paraList.Add(new SqlParameter("@VM006", addr));
            paraList.Add(new SqlParameter("@VM007", person1));
            paraList.Add(new SqlParameter("@VM008", tel1));
            paraList.Add(new SqlParameter("@VM009", mail1));
            paraList.Add(new SqlParameter("@VM010", person2));
            paraList.Add(new SqlParameter("@VM011", tel2));
            paraList.Add(new SqlParameter("@VM012", mail2));
            paraList.Add(new SqlParameter("@VM002", accno));

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

        public int deleteVender(string accno, out Error.Error error)
        {
            error = null;
            int iAffectedCount = -1;
            paraList.Clear();
            string strSQL = "delete from vender_member where VM002=@VM002";
            paraList.Add(new SqlParameter("@VM002", accno));

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
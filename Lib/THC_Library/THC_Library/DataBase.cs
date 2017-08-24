using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace THC_Library.DataBase
{
    public class DataBaseControl
    {
        private static string ConnectionString = "server=60.251.140.166;database=THC_EVENTS;uid=thc_user;pwd=1qaz@WSX;";

        SqlConnection sqlConnection = null;
        SqlTransaction sqlTransaction = null;

        public DataBaseControl()
        {
            sqlConnection = new SqlConnection(ConnectionString);
        }

        public static  void ResetDatabase(string ip, string database)
        {
            ConnectionString = string.Format(
                                    "server={0};database={1};uid=thc_user;pwd=1qaz@WSX;",
                                    ip, database);
        }

        public void Open()
        {
            sqlConnection.Open();
        }

        public void BeginTransaction()
        {
            sqlTransaction = sqlConnection.BeginTransaction();
        }

        public void BeginTransaction(IsolationLevel iso)
        {
            sqlTransaction = sqlConnection.BeginTransaction(iso);
        }

        public void CommintTransaction()
        {
            sqlTransaction.Commit();
            sqlTransaction = null;
        }

        public void RollBackTransaction()
        {
            if (sqlTransaction != null)
            {
                sqlTransaction.Rollback();
            }
            sqlTransaction = null;
        }

        public void Close()
        {
            sqlConnection.Close();
        }

        public int ExecuteCommad(string sql, System.Collections.Generic.IList<SqlParameter> sqlparams)
        {
            SqlCommand m_Command = new SqlCommand();
            m_Command.CommandText = sql;
            m_Command.Connection = sqlConnection;
            if (sqlTransaction != null)
            {
                m_Command.Transaction = sqlTransaction;
            }
            if (sqlparams != null)
            {
                foreach (SqlParameter @params in sqlparams)
                {
                    m_Command.Parameters.Add(@params);
                }
            }
            return m_Command.ExecuteNonQuery();
        }

        public IDataReader GetReader(string sql, System.Collections.Generic.IList<SqlParameter> sqlparams)
        {
            SqlCommand m_Command = new SqlCommand();
            m_Command.CommandText = sql;
            m_Command.Connection = sqlConnection;
            if (sqlTransaction != null)
            {
                m_Command.Transaction = sqlTransaction;
            }
            if (sqlparams != null)
            {
                foreach (SqlParameter @params in sqlparams)
                {
                    m_Command.Parameters.Add(@params);
                }
            }
            return m_Command.ExecuteReader();
        }

        public DataTable GetDataTable(string sql, System.Collections.Generic.IList<SqlParameter> sqlparams)
        {
            SqlDataAdapter m_DataAdapter = new SqlDataAdapter(sql, sqlConnection);
            if (sqlTransaction != null)
            {
                m_DataAdapter.SelectCommand.Transaction = sqlTransaction;
            }
            if (sqlparams != null)
            {
                foreach (SqlParameter @params in sqlparams)
                {
                    m_DataAdapter.SelectCommand.Parameters.Add(@params);
                }
            }

            DataTable m_DataTable = new DataTable();
            m_DataAdapter.Fill(m_DataTable);
            return m_DataTable;
        }

        public object ExecuteScalar(string sql, System.Collections.Generic.IList<SqlParameter> sqlparams)
        {
            SqlCommand mCommand = new SqlCommand();
            mCommand.CommandText = sql;
            mCommand.Connection = sqlConnection;
            if (sqlTransaction != null)
            {
                mCommand.Transaction = sqlTransaction;
            }
            if (sqlparams != null)
            {
                foreach (SqlParameter @params in sqlparams)
                {
                    mCommand.Parameters.Add(@params);
                }
            }
            return mCommand.ExecuteScalar();
        }
    }
}

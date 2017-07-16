using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
    }
}
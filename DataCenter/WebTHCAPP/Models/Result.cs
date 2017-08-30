using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebTHCAPP.Models
{
    public class ResultBase
    {
        public int Number
        {
            get;
            set;
        }

        public string ErrorMessage
        {
            get;
            set;
        }
    }
    public class Result : ResultBase
    {
        public Result()
        {
            Number = 0;
            Verify = 0;
            Acc = "";
            ErrorMessage = "";
            Ticket = "-1";
            Addition = "";
        }
        
        public int Verify
        {
            get;
            set;
        }

        public string Acc
        {
            get;
            set;
        }

        public string Ticket
        {
            get;
            set;
        }

        public string Addition
        {
            get;
            set;
        }
    }
}
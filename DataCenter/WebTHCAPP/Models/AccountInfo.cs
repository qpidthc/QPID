using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebTHCAPP.Models
{
    public class AccountInfo : ResultBase
    {
        public string FB
        {
            get;
            set;
        }
        public string Mail
        {
            get;
            set;
        }

        public string Mobil
        {
            get;
            set;
        }

        public string Address
        {
            get;
            set;
        }

        public string IId
        {
            get;
            set;
        }

        public string Gender
        {
            get;
            set;
        }

        public string Age
        {
            get;
            set;
        }

        public byte[] Image
        {
            get;
            set;
        }



    }
}
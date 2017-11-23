#define REMOTE
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebTHCEventUI.Models
{
    public class Domain
    {
        #if(REMOTE)
        public const string REMOTE_DOMAIN = "http://oqid.ddns.net/";
        public const string REMOTE_ACCESS_DOMAIN = "http://oqid.ddns.net/WebTHCApp/Members/THC_Member_07";
        #else
        public const string REMOTE_DOMAIN = "http://localhost:51323/";
        public const string REMOTE_ACCESS_DOMAIN = "http://localhost:51323/Members/THC_Member_07";
        #endif

    }
}
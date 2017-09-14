using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebHTCBackEnd.Utility
{
    public class JsonResponeUtility
    {
        public static string ResultOK()
        {
            return "[{ \"result\" : 0 }]";
        }
    }
}
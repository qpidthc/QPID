using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebTHCAPP.Models
{   
    public class RequestChecker 
    {

        public static THC_Library.Error CheckRequest(string tick, string data)
        {
            THC_Library.Error error = null;
            if (tick == null)
            {
                error = new THC_Library.Error();
                error.Number = 401;
                error.ErrorMessage = "無效存取";
            }
            if (data == null)
            {
                error = new THC_Library.Error();
                error.Number = 402;
                error.ErrorMessage = "無效存取";
            }

            tick = THC_Library.APPCURL.Encry(tick);
            if (data != tick)
            {
                error = new THC_Library.Error();
                error.Number = 403;
                error.ErrorMessage = "無效存取";
            }

            return error;
        }
    }
}
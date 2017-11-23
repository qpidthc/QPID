using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace THC_Library
{
    public class Error
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

        public static Error AbortError()
        {
            Error error = new Error();
            error.Number = 1001;
            error.ErrorMessage = "abort";
            return error;
        } 
    }
}

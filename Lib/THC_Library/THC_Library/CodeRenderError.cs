using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace THC_Library
{
    public class THCException : Exception
    {
        public const int SYSTEM_ERROR = 0xFF;
        protected int errNumber;
        public THCException(string error) : base(error)
        {
        }
        public THCException(int number, string error)
            : base(error)
        {
            errNumber = number;
        }
        public int Number
        {
            get
            {
                return errNumber;
            }
        }
    }
    public class CodeRenderException : THCException
    {
        public const int ACTIVITY_NOT_START = 0x01;
        public const int ACTIVITY_FINISHED = 0x02;
        public const int INVAILD_ACTIVITY = 0x03;
        public const int INVAILD_CODE = 0x04;
        public const int REPEAT_SCAN = 0x05;
        public const int LOGIN_INVALID = 0x06;
        
                
        public CodeRenderException(int number,  string error)
            : base(error)
        {
            errNumber = number;
        }

        public string AdditionalMessage
        {
            get;
            set;
        }
       
    }
}

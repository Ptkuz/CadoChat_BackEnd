using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadoChat.Web.AspNetCore.WebResponsies
{   
    public class BaseResponse
    {

        public bool Success { get; set; }

        public string? ErrorCode { get; set; } = null;

        public string? Message { get; set; } 

        public string? StackTrace { get; set; }

        public BaseResponse(string errorCode, string message)
        {
            Success = false;
            ErrorCode = errorCode;
            Message = message;
        }

        public BaseResponse(Exception exception)
        {
            Success = false;
            ErrorCode = exception.GetType().Name;
            Message = exception.Message;
        }

        public BaseResponse(Exception exception, bool getStackTrace )
            : this(exception)
        {
            if (getStackTrace)
            {
                StackTrace = exception.StackTrace;
            }
        }

        public BaseResponse()
        {
            Success = true;
        }

    }
}

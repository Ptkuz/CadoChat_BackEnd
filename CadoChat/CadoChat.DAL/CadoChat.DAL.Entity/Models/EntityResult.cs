using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadoChat.DAL.Entity.Models
{
    public class EntityResult
    {
        public bool Success { get; private set; }

        public string Message { get; private set; }

        public string? StackTrace { get; private set; }

        public Type? ErrorCode { get; private set; }


        public EntityResult()
        {
            Success = true;
            Message = "OK";
        }

        public EntityResult(string message)
        {
            Success = false;
            Message = message;
        }

        public EntityResult(Exception exception)
        {
            Success = false;
            Message = exception.Message;
            StackTrace = exception.StackTrace!;
            ErrorCode = exception.GetType();
        }

    }
}

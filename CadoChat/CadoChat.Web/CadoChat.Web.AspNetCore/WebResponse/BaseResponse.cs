using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadoChat.Web.AspNetCore.WebResponse
{

    /// <summary>
    /// Базовый Web ответ
    /// </summary>
    public class BaseResponse
    {

        /// <summary>
        /// Успешно ли выполнен запрос
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// Код ошибки
        /// </summary>
        public string? ErrorCode { get; set; } = null;

        /// <summary>
        /// Сообщение об ошибке
        /// </summary>
        public string? Message { get; set; }


        /// <summary>
        /// Стек вызовов
        /// </summary>
        public string? StackTrace { get; set; }


        /// <summary>
        /// Инициализатор ошибочного ответа
        /// </summary>
        /// <param name="errorCode">Код ошибки</param>
        /// <param name="message">Сообщение об ошибке</param>
        public BaseResponse(string errorCode, string message)
        {
            Success = false;
            ErrorCode = errorCode;
            Message = message;
        }

        /// <summary>
        /// Инициализатор ошибочного ответа
        /// </summary>
        /// <param name="exception">Исключение, наследованного от <see cref="Exception"/></param>
        public BaseResponse(Exception exception)
        {
            Success = false;
            ErrorCode = exception.GetType().Name;
            Message = exception.Message;
        }

        /// <summary>
        /// Инициализатор ошибочного ответа
        /// </summary>
        /// <param name="exception">Исключение, наследованного от <see cref="Exception"/></param>
        /// <param name="getStackTrace">Отправлять ли стек вызовов</param>
        public BaseResponse(Exception exception, bool getStackTrace )
            : this(exception)
        {
            if (getStackTrace)
            {
                StackTrace = exception.StackTrace;
            }
        }

        /// <summary>
        /// Инициализатор успешного ответа
        /// </summary>
        public BaseResponse()
        {
            Success = true;
        }

    }
}

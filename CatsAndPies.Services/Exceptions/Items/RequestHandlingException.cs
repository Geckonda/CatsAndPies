using CatsAndPies.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatsAndPies.Domain.Exceptions.Items
{
    public class RequestHandlingException : Exception
    {
        public StatusCode StatusCode { get; }
        public string MessageForUser { get; } = string.Empty;
        public object? AdditionalData { get; }
        public RequestHandlingException(StatusCode statusCode, string messageForUser)
            :base($"Ошибка: {statusCode}: {messageForUser}")
        {
            StatusCode = statusCode;
            MessageForUser = messageForUser;
        }
        public RequestHandlingException(StatusCode statusCode, string messageForUser, object additionalData)
            : base($"Ошибка: {statusCode}: {messageForUser}")
        {
            StatusCode = statusCode;
            MessageForUser = messageForUser;
            AdditionalData = additionalData;
        }
    }
}

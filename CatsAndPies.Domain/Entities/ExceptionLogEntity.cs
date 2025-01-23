using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatsAndPies.Domain.Entities
{
    public class ExceptionLogEntity
    {
        public int Id { get; set; }
        public DateTime ExceptionTime { get; set; } // Время исключения
        public string ExceptionType { get; set; } = string.Empty; // Тип исключения
        public string Method { get; set; } = string.Empty; // Метод, где произошло исключение
        public string ExceptionText { get; set; } = string.Empty; // Текст исключения

        public ExceptionLogEntity()
        {
            
        }
        public ExceptionLogEntity(DateTime dateTime, string exceptionType,
            string method, string exceptionText)
        {
            ExceptionTime = dateTime;
            ExceptionType = exceptionType;
            Method = method;
            ExceptionText = exceptionText;
        }

    }
}

using CatsAndPies.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CatsAndPies.Domain.Helpres
{
    public class ExceptionLogHelper
    {
        public static ExceptionLogEntity ParseException(Exception ex, Type middleware)
        {
            var stackTrace = new System.Diagnostics.StackTrace(ex, true);
            var frame = stackTrace.GetFrames()?.FirstOrDefault(f => f.GetMethod()?.DeclaringType != middleware);

            string method = "Неизвестно";
            if (frame != null)
            {
                var methodName = frame.GetMethod()?.Name ?? "Метод неизвестен";
                string pattern = @"<([^>]+)>";
                methodName = Regex.Match(methodName!, pattern).Groups[1].Value;

                var fileName = frame.GetFileName() ?? "Файл неизвестен";
                fileName = fileName!.Substring(fileName.LastIndexOf('\\') + 1);

                var lineNumber = frame.GetFileLineNumber();

                method = $"{methodName} ({fileName} ({lineNumber}))";
            }
            return new()
            {
                ExceptionTime = DateTime.Now,
                ExceptionType = ex.GetType().Name,
                Method = method!,
                ExceptionText = ex.Message,
            };
        }
    }
}

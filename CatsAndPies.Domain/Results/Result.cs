using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatsAndPies.Domain.Responses
{
    public class Result<T>
    {
        public bool IsSuccess { get; private set; }
        public T? Data { get; private set; }

        public static Result<T> SuccessResult(T data) => new() { IsSuccess = true, Data = data };
        public static Result<T> ErrorResult() => new() { IsSuccess = false };
    }
}

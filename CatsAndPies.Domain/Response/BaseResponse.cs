using CatsAndPies.Domain.Abstractions.Responses;
using CatsAndPies.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatsAndPies.Domain.Response
{
    public class BaseResponse<T> : IBaseResponse<T>
    {
        public string? Description { get; set; }
        public StatusCode StatusCode { get; set; }
        public T? Data { get; set; }
        public string? MessageForUser { get; set; }
    }
}

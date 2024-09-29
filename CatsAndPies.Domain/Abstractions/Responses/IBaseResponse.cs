using CatsAndPies.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatsAndPies.Domain.Abstractions.Responses
{
    public interface IBaseResponse<T>
    {
        T? Data { get; set; }
        public StatusCode StatusCode { get; set; }

        public string? Description { get; set; }
        public string? MessageForUser { get; set; }
    }
}

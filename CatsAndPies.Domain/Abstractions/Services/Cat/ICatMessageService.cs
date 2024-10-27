using CatsAndPies.Domain.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatsAndPies.Domain.Abstractions.Services.Cat
{
    public interface ICatMessageService
    {
        Task<BaseResponse<string>> SaySomething(int userId);
    }
}

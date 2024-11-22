using CatsAndPies.Domain.Models.Response;
using CatsAndPies.Domain.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatsAndPies.Domain.Abstractions.Services.Cat
{
    /// <summary>
    /// Отвечает за логику сообщений
    /// </summary>
    public interface ICatMessageService
    {
        Task<Result<string>> TrySaySomething(int userId);
    }
}

using CatsAndPies.Domain.DTO.Response.Cat;
using CatsAndPies.Domain.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatsAndPies.Domain.Abstractions.Services.Cat
{
    /// <summary>
    /// Отвечает за операции чтения данных, такие как получение информации о котах.
    /// </summary>
    public interface ICatQueryService
    {
        public Task<Result<CatResponseWithoutOwnerDTO?>> GetCatWithoutOwnerByUserId(int  userId);
    }
}

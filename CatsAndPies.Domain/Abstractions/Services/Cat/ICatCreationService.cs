using CatsAndPies.Domain.DTO.Response.Cat;
using CatsAndPies.Domain.Models.Response;
using CatsAndPies.Domain.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatsAndPies.Domain.Abstractions.Services.Cat
{
    public interface ICatCreationService
    {
        Task<Result<CatResponseDTO?>> CreateCat(string name, int userId);
    }
}

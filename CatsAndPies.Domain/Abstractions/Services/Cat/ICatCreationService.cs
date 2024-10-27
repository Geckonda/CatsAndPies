using CatsAndPies.Domain.DTO.Response.Cat;
using CatsAndPies.Domain.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatsAndPies.Domain.Abstractions.Services.Cat
{
    public interface ICatCreationService
    {
        Task<BaseResponse<CatResponseDTO>> CreateCat(string name, int userId);
    }
}

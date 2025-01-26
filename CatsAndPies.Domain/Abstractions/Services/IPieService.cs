using CatsAndPies.Domain.DTO.Response.Pie;
using CatsAndPies.Domain.Entities.PiesTables;
using CatsAndPies.Domain.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatsAndPies.Domain.Abstractions.Services
{
    public interface IPieService
    {
        Task<Result<List<PieResponseDTO>>> TryGetAllUserPies(int userId);
        Task<Result<PieResponseDTO>> TryCreatePie(int userId, string pieName);
    }
}

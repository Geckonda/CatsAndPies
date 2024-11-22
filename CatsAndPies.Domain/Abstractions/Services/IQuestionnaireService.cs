using CatsAndPies.Domain.DTO.Request;
using CatsAndPies.Domain.DTO.Response;
using CatsAndPies.Domain.Models.Response;
using CatsAndPies.Domain.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatsAndPies.Domain.Abstractions.Services
{
    public interface IQuestionnaireService
    {
        Task<Result<bool>> TryAdd(QuestionnaireRequestDto model);
        Task<Result<QuestionnaireResponseDto>> TryGetByUserId(int userId);
        Task<Result<QuestionnaireResponseDto>> TryGetById(int id);
        Task<Result<bool>> TryUpdateFull(QuestionnaireRequestDto model);
        Task<Result<bool>> TryDeleteByUserId(int userId);
    }
}

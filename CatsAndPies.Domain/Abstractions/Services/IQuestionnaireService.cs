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
        Task<Result<bool>> Add(QuestionnaireRequestDto model);
        Task<Result<QuestionnaireResponseDto>> GetByUserId(int userId);
        Task<Result<QuestionnaireResponseDto>> GetById(int id);
        Task<Result<bool>> UpdateFull(QuestionnaireRequestDto model);
        Task<Result<bool>> DeleteByUserId(int userId);
    }
}

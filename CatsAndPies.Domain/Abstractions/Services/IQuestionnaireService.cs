using CatsAndPies.Domain.DTO.Request;
using CatsAndPies.Domain.DTO.Response;
using CatsAndPies.Domain.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatsAndPies.Domain.Abstractions.Services
{
    public interface IQuestionnaireService
    {
        Task<BaseResponse<bool>> Add(QuestionnaireRequestDto model);
        Task<BaseResponse<QuestionnaireResponseDto>> GetByUserId(int userId);
        Task<BaseResponse<QuestionnaireResponseDto>> GetById(int id);
        Task<BaseResponse<bool>> UpdateFull(QuestionnaireRequestDto model);
        Task<BaseResponse<bool>> DeleteByUserId(int userId);
    }
}

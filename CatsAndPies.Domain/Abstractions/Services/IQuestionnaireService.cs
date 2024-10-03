using CatsAndPies.Domain.DTO.Request;
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
    }
}

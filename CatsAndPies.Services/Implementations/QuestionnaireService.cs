using AutoMapper;
using CatsAndPies.Domain.Abstractions.Repositories;
using CatsAndPies.Domain.Abstractions.Services;
using CatsAndPies.Domain.DTO.Request;
using CatsAndPies.Domain.Entities;
using CatsAndPies.Domain.Enums;
using CatsAndPies.Domain.Response;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatsAndPies.Services.Implementations
{
    public class QuestionnaireService : IQuestionnaireService
    {
        private readonly ILogger<QuestionnaireService> _logger;
        private readonly IBaseRepository<QuestionnaireEntity> _questionnaireRepository;
        private readonly IMapper _mapper;

        public QuestionnaireService(IBaseRepository<QuestionnaireEntity> repository,
            ILogger<QuestionnaireService> logger,
            IMapper mapper)
        {
            _logger = logger;
            _questionnaireRepository = repository;
            _mapper = mapper;
        }
        public async Task<BaseResponse<bool>> Add(QuestionnaireRequestDto model)
        {
			try
			{
				var questionnaire = _questionnaireRepository
                    .GetAll().Result!
                    .Where(q => q.UserId == model.UserId)
                    .FirstOrDefault();
                if (questionnaire != null)
                {
                    return new()
                    {
                        MessageForUser = "Нельзя добавить новую анкету. Анкета была создана прежде.",
                        Description = "Анкета не добавлена. Анкета уже создана",
                        Data = false,
                    };
                }
                //Заглушка, сделай маппинг!!!
                questionnaire = _mapper.Map<QuestionnaireEntity>(model);
                await _questionnaireRepository.Add(questionnaire);
                return new()
                {
                    StatusCode = StatusCode.Ok,
                    Data = true,
                    MessageForUser = "Анкета успешно добавлена",

                    //Добавить получения пользователя из БД?
                    Description = $"Пользователь добавил анкету"
                };
            }
			catch (Exception ex)
			{
                _logger.LogError(ex, $"[Add questionnaire]: {ex.Message}");
                return new()
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError,
                    MessageForUser = "Не удалось добавить анкету. Что-то пошло не так..."
                };
			}
        }
    }
}

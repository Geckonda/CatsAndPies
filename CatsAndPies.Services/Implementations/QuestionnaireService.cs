using AutoMapper;
using CatsAndPies.Domain.Abstractions.Repositories.Combined;
using CatsAndPies.Domain.Abstractions.Services;
using CatsAndPies.Domain.DTO.Request;
using CatsAndPies.Domain.DTO.Response;
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
        private readonly IQuestionnaireRepository _questionnaireRepository;
        private readonly IMapper _mapper;

        public QuestionnaireService(IQuestionnaireRepository repository,
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

        public async Task<BaseResponse<QuestionnaireResponseDto>> GetById(int id)
        {
            try
            {
                var questionnaire = await _questionnaireRepository.GetOneById(id);
                if (questionnaire == null)
                {
                    return new()
                    {
                        StatusCode = StatusCode.NotFound,
                        MessageForUser = $"Анкета не найдена",
                        Description = $"Анкета не найдена"
                    };
                }
                var model = _mapper.Map<QuestionnaireResponseDto>(questionnaire);
                return new()
                {
                    StatusCode = StatusCode.Ok,
                    Data = model,
                    MessageForUser = "Анкета получена",
                    Description = "Анкета получена"
                };
            }
            catch (Exception ex)
            {
                return new()
                {
                    StatusCode = StatusCode.InternalServerError,
                    MessageForUser = "Не удалось получить анкету, что-то пошло не так...",
                    Description = "Не удалось получить анкету, что-то пошло не так...",
                };
            }
        }

        public async Task<BaseResponse<QuestionnaireResponseDto>> GetByUserId(int userId)
        {
            try
            {
                var questionnaire = await _questionnaireRepository.GetOneByUserId(userId);
                if (questionnaire == null)
                {
                    return new()
                    {
                        StatusCode = StatusCode.NotFound,
                        MessageForUser = "Анкета не найдена",
                        Description = $"Анкета не найдена"
                    };
                }
                var model = _mapper.Map<QuestionnaireResponseDto>(questionnaire);
                return new()
                {
                    StatusCode = StatusCode.Ok,
                    Data = model,
                    MessageForUser = "Анкета получена",
                    Description = "Анкета получена"
                };

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[Get questionnaire]: {ex.Message}");
                return new()
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError,
                    MessageForUser = "Не удалось получить анкету. Что-то пшло не так..."
                };
            }
        }
    }
}

using AutoMapper;
using CatsAndPies.Domain.Abstractions.Repositories.Combined;
using CatsAndPies.Domain.Abstractions.Services;
using CatsAndPies.Domain.DTO.Request;
using CatsAndPies.Domain.DTO.Response;
using CatsAndPies.Domain.Entities;
using CatsAndPies.Domain.Enums;
using CatsAndPies.Domain.Models.Response;
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
                var questionnaire = await _questionnaireRepository.GetOneByUserId(model.UserId);
                if (questionnaire != null)
                {
                    return new()
                    {
                        MessageForUser = "Нельзя добавить новую анкету. Анкета была создана прежде.",
                        Data = false,
                    };
                }
                questionnaire = _mapper.Map<QuestionnaireEntity>(model);
                await _questionnaireRepository.Add(questionnaire);
                return new()
                {
                    StatusCode = StatusCode.Created,
                    Data = true,
                    MessageForUser = "Анкета успешно добавлена",

                    //Добавить получения пользователя из БД?
                };
            }
			catch (Exception ex)
			{
                _logger.LogError(ex, $"[Add questionnaire]: {ex.Message}");
                return new()
                {
                    StatusCode = StatusCode.InternalServerError,
                    MessageForUser = "Не удалось добавить анкету. Что-то пошло не так..."
                };
			}
        }

        public async Task<BaseResponse<bool>> DeleteByUserId(int userId)
        {
            var response = new BaseResponse<bool>();
            try
            {
                var questionnaireId = await _questionnaireRepository.GetOneIdByUserId(userId);
                await _questionnaireRepository.Delete(questionnaireId);
                response.Data = true;
                response.MessageForUser = "Анкета удалена";
                response.StatusCode = StatusCode.Ok;
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[Delete questionnaire]: {ex.Message}");
                return new()
                {
                    Data = false,
                    StatusCode = StatusCode.InternalServerError,
                    MessageForUser = "Не удалось удалить анкету. Что-то пошло не так..."
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
                    };
                }
                var model = _mapper.Map<QuestionnaireResponseDto>(questionnaire);
                return new()
                {
                    StatusCode = StatusCode.Ok,
                    Data = model,
                    MessageForUser = "Анкета получена",
                };
            }
            catch (Exception ex)
            {
                return new()
                {
                    StatusCode = StatusCode.InternalServerError,
                    MessageForUser = "Не удалось получить анкету, что-то пошло не так...",
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
                    };
                }
                var model = _mapper.Map<QuestionnaireResponseDto>(questionnaire);
                return new()
                {
                    StatusCode = StatusCode.Ok,
                    Data = model,
                    MessageForUser = "Анкета получена",
                };

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[Get questionnaire]: {ex.Message}");
                return new()
                {
                    StatusCode = StatusCode.InternalServerError,
                    MessageForUser = "Не удалось получить анкету. Что-то пошло не так..."
                };
            }
        }

        public async Task<BaseResponse<bool>> UpdateFull(QuestionnaireRequestDto model)
        {
            try
            {
                var response = new BaseResponse<bool>();
                var questionnaire = _mapper.Map<QuestionnaireEntity>(model);
                questionnaire.Id = await _questionnaireRepository.GetOneIdByUserId(model.UserId);
                await _questionnaireRepository.Update(questionnaire);
                response.Data = true;
                response.StatusCode = StatusCode.Ok;
                response.MessageForUser = "Анкета обновлена";
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[UpdateFull questionnaire]: {ex.Message}");
                return new()
                {
                    Data = false,
                    StatusCode = StatusCode.InternalServerError,
                    MessageForUser = "Не удалось обновить анкету. Что-то пошло не так..."
                };
            }
        }
    }
}

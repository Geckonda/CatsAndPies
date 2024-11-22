using AutoMapper;
using CatsAndPies.Domain.Abstractions.Repositories.Combined;
using CatsAndPies.Domain.Abstractions.Services;
using CatsAndPies.Domain.DTO.Request;
using CatsAndPies.Domain.DTO.Response;
using CatsAndPies.Domain.Entities;
using CatsAndPies.Domain.Enums;
using CatsAndPies.Domain.Models.Response;
using CatsAndPies.Domain.Responses;
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
        public async Task<Result<bool>> TryAdd(QuestionnaireRequestDto model)
        {
            var questionnaire = await _questionnaireRepository.GetOneByUserId(model.UserId);
            if (questionnaire != null)
                return Result<bool>.ErrorResult();

            questionnaire = _mapper.Map<QuestionnaireEntity>(model);
            await _questionnaireRepository.Add(questionnaire);
            return Result<bool>.SuccessResult(true);
        }

        public async Task<Result<bool>> TryDeleteByUserId(int userId)
        {
            var questionnaireId = await _questionnaireRepository.GetOneIdByUserId(userId);
            if(questionnaireId == 0)
                return Result<bool>.ErrorResult();

            await _questionnaireRepository.Delete(questionnaireId);
            return Result<bool>.SuccessResult(true);
        }

        public async Task<Result<QuestionnaireResponseDto>> TryGetById(int id)
        {
            var questionnaire = await _questionnaireRepository.GetOneById(id);
            if (questionnaire == null)
                return Result<QuestionnaireResponseDto>.ErrorResult();

            var model = _mapper.Map<QuestionnaireResponseDto>(questionnaire);
            return Result<QuestionnaireResponseDto>.SuccessResult(model);
        }

        public async Task<Result<QuestionnaireResponseDto>> TryGetByUserId(int userId)
        {
            var questionnaire = await _questionnaireRepository.GetOneByUserId(userId);
            if (questionnaire == null)
                return Result<QuestionnaireResponseDto>.ErrorResult();

            var model = _mapper.Map<QuestionnaireResponseDto>(questionnaire);
            return Result<QuestionnaireResponseDto>.SuccessResult(model);
        }

        public async Task<Result<bool>> TryUpdateFull(QuestionnaireRequestDto model)
        {
            var questionnaireId = await _questionnaireRepository.GetOneIdByUserId(model.UserId);
            if(questionnaireId == 0)
                return Result<bool>.ErrorResult();

            var questionnaire = _mapper.Map<QuestionnaireEntity>(model);
            questionnaire.Id = questionnaireId;
            await _questionnaireRepository.Update(questionnaire);
            return Result<bool>.SuccessResult(true);
        }
    }
}

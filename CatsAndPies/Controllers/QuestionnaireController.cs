using CatsAndPies.Domain.Abstractions.Services;
using CatsAndPies.Domain.DTO.Request;
using CatsAndPies.Domain.DTO.Response;
using CatsAndPies.Domain.Models.Response;
using CatsAndPies.Domain.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Security.Claims;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CatsAndPies.Controllers
{

    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    [SwaggerTag("Работа с анкетой. (Full Auth)")]
    public class QuestionnaireController : Controller
    {
        private readonly ILogger<QuestionnaireController> _logger;
        private readonly IQuestionnaireService _questionnaireService;
        public QuestionnaireController(IQuestionnaireService questionnaireService,
            ILogger<QuestionnaireController> logger)
        {
            _questionnaireService = questionnaireService;
            _logger = logger;
        }
        private int GetUserId() => Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));

        [HttpPost]
        [SwaggerOperation(Summary = "Добавить анкету", Description = "Возвращает true/false.")]
        public async Task<IActionResult> AddQuestionnaire([FromBody]QuestionnaireRequestDto model)
        {
            _logger.LogInformation("AddQuestionnaire метод вызван {Time}", DateTime.UtcNow);
            BaseResponse<bool> response;
            model.UserId = GetUserId();
            var result = await _questionnaireService.TryAdd(model);
            if(result.IsSuccess)
            {
                response = new()
                {
                    StatusCode = Domain.Enums.StatusCode.Created,
                    Data = result.Data,
                    MessageForUser = "Анкета успешно добавлена.",
                };
                return CreatedAtAction("AddQuestionnaire", response);
            }
            response = new()
            {
                StatusCode = Domain.Enums.StatusCode.Conflict,
                Data = result.Data,
                MessageForUser = "Не удалось добавить анкету, она уже была создана ранее."
            };
            return Conflict(response);
        }
        [HttpGet("GetMyQuestionnaire")]
        [SwaggerOperation(Summary = "Получить анкету", Description = "Возвращает анкету авторизированного пользователя.")]
        public async Task<IActionResult> GetMyQuestionnaire()
        {
            _logger.LogInformation("GetMyQuestionnaire метод вызван {Time}", DateTime.UtcNow);
            BaseResponse<QuestionnaireResponseDto> response;
            var result = await _questionnaireService.TryGetByUserId(GetUserId());
            if(result.IsSuccess)
            {
                response = new()
                {
                    StatusCode = Domain.Enums.StatusCode.Ok,
                    Data = result.Data,
                    MessageForUser = "Анкета получена."
                };
                return Ok(response);
            }
            _logger.LogWarning("GetMyQuestionnaire NotFound {Time}", DateTime.UtcNow);
            response = new()
            {
                StatusCode = Domain.Enums.StatusCode.NotFound,
                Data = result.Data,
                MessageForUser = "Анкета не найдена."
            };
            return NotFound(response);
        }
        [HttpGet("GetUserQuestionnaire")]
        [SwaggerOperation(Summary = "Получить анкету по id", Description = "Возвращает анкету конкретного пользователя по его Id.")]
        public async Task<IActionResult> GetUserQuestionnaire(int id)
        {
            _logger.LogInformation("GetUserQuestionnaire метод вызван {Time}", DateTime.UtcNow);
            BaseResponse<QuestionnaireResponseDto> response;
            var result = await _questionnaireService.TryGetById(id);
            if (result.IsSuccess)
            {
                response = new()
                {
                    StatusCode = Domain.Enums.StatusCode.Ok,
                    Data = result.Data,
                    MessageForUser = "Анкета получена."
                };
                return Ok(response);
            }
            response = new()
            {
                StatusCode = Domain.Enums.StatusCode.NotFound,
                Data = result.Data,
                MessageForUser = "Анкета не найдена."
            };
            return NotFound(response);
        }
        [HttpPut]
        [SwaggerOperation(Summary = "Редактировать анкету (все поля)", Description = "Возвращает true/false.")]
        public async Task<IActionResult> UpdateFullQuestionnaire([FromBody] QuestionnaireRequestDto model)
        {
            _logger.LogInformation("UpdateFullQuestionnaire метод вызван {Time}", DateTime.UtcNow);
            BaseResponse<bool> response;
            model.UserId = GetUserId();
            var result = await _questionnaireService.TryUpdateFull(model);
            if (result.IsSuccess)
            {
                response = new()
                {
                    StatusCode = Domain.Enums.StatusCode.Ok,
                    Data = result.Data,
                    MessageForUser = "Анкета успешно обновлена."
                };
                return Ok(response);
            }
            response = new()
            {
                StatusCode = Domain.Enums.StatusCode.NotFound,
                Data = result.Data,
                MessageForUser = "Анкета не найдена."
            };
            return NotFound(response);
        }
        [HttpDelete]
        [SwaggerOperation(Summary = "Удалить анкету", Description = "Возвращает true/false.")]
        public async Task<IActionResult> DeleteQuestionnaireByUserId()
        {
            _logger.LogInformation("DeleteQuestionnaireByUserId метод вызван {Time}", DateTime.UtcNow);
            BaseResponse<bool> response;
            var result = await _questionnaireService.TryDeleteByUserId(GetUserId());
            if(result.IsSuccess)
            {
                response = new()
                {
                    StatusCode = Domain.Enums.StatusCode.Ok,
                    Data = result.Data,
                    MessageForUser = "Анкета успешно удалена."
                };
                return Ok(response);
            }

            response = new()
            {
                StatusCode = Domain.Enums.StatusCode.NotFound,
                Data = result.Data,
                MessageForUser = "Не удалось найти анкету для удаления."
            };
            return NotFound(response);
        }
    }
}

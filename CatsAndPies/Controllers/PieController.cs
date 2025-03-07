﻿using CatsAndPies.Domain.Abstractions.Services;
using CatsAndPies.Domain.DTO.Response;
using CatsAndPies.Domain.DTO.Response.Pie;
using CatsAndPies.Domain.Entities.PiesTables;
using CatsAndPies.Domain.Models.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Security.Claims;

namespace CatsAndPies.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [SwaggerTag("Работа с пирогами. (Partial Auth)")]
    public class PieController : Controller
    {
        private readonly ILogger<PieController> _logger;
        private readonly IPieService _pieService;
        public PieController(IPieService pieService,
            ILogger<PieController> logger)
        {
            _pieService = pieService;
            _logger = logger;
        }
        private int GetUserId() => Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
        [HttpGet("GetUserPies")]
        [SwaggerOperation(Summary = "Получить все пироги пользователя", Description = "Возвращает список пирогов.")]
        public async Task<IActionResult> GetUserPies()
        {
            _logger.LogInformation("GetUserPies метод вызван {Time}", DateTime.UtcNow);
            BaseResponse<List<PieResponseDTO>> response;
            var result = await _pieService.TryGetAllUserPies(GetUserId());
            if (result.IsSuccess)
            {
                response = new()
                {
                    StatusCode = Domain.Enums.StatusCode.Ok,
                    Data = result.Data,
                    MessageForUser = "Пирожки получены."
                };
                return Ok(response);
            }
            response = new()
            {
                StatusCode = Domain.Enums.StatusCode.NotFound,
                Data = result.Data,
                MessageForUser = "Пирожки не найдены."
            };
            return NotFound(response);
        }
        [Authorize]
        [SwaggerOperation(Summary = "(Auth). Создать пирог", Description = "Возвращает созданный пирог.")]
        [HttpGet("AddPie")]
        public async Task<IActionResult> AddPie(string pieName)
        {
            _logger.LogInformation("AddPie метод вызван {Time}", DateTime.UtcNow);

            var result = await _pieService.TryCreatePie(GetUserId(), pieName);
            var response = new BaseResponse<PieResponseDTO>()
            {
                StatusCode = Domain.Enums.StatusCode.Ok,
                Data = result.Data,
                MessageForUser = "Пирожок создан."
            };
            return Ok(response);
        }
    }
}

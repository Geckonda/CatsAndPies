using CatsAndPies.Domain.Abstractions.Services;
using CatsAndPies.Domain.DTO.Response;
using CatsAndPies.Domain.Entities.PiesTables;
using CatsAndPies.Domain.Models.Response;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace CatsAndPies.Controllers
{
    [Route("api/[controller]")]
    [SwaggerTag("Работа с пирогами.")]
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
        [HttpGet("GetPies")]
        public async Task<IActionResult> GetPies()
        {
            _logger.LogInformation("GetPies метод вызван {Time}", DateTime.UtcNow);
            BaseResponse<List<PieEntity>> response;
            var result = await _pieService.TryGetAllPies();
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
    }
}

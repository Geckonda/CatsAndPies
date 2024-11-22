using CatsAndPies.Domain.Abstractions.Services;
using CatsAndPies.Domain.Abstractions.Services.Cat;
using CatsAndPies.Domain.DTO.Response.Cat;
using CatsAndPies.Domain.Models.Response;
using CatsAndPies.Domain.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR.Protocol;
using Swashbuckle.AspNetCore.Annotations;
using System.Security.Claims;
using System.Xml.Linq;

namespace CatsAndPies.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [SwaggerTag("Работа с котом.")]
    public class CatController : Controller
    {
        private readonly ICatService _catService;
        private int GetUserId() => Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
        public CatController(ICatService catService)
        {
            _catService = catService;
        }
        [HttpGet("SaySomething")]
        [SwaggerOperation(Summary = "Получить случайное сообщение от кота", Description = "Data - содержит строку \"что сказал кот\".")]
        public async Task<IActionResult> SaySomething()
        {
            BaseResponse<string> response;
            try
            {
                var result = await _catService.TrySaySomething(GetUserId());
                if(result.IsSuccess)
                {
                    response = new BaseResponse<string>
                    {
                        StatusCode = Domain.Enums.StatusCode.Ok,
                        Data = result.Data,
                        MessageForUser = "Кот что-то говорит.",
                    };
                    return Ok(response);
                }
                response = new BaseResponse<string>
                {
                    StatusCode = Domain.Enums.StatusCode.NotFound,
                    MessageForUser = "У вас нет кота.",
                };
                return BadRequest(response);
            }
            catch (Exception ex)
            {
                response = new BaseResponse<string>
                {
                    StatusCode = Domain.Enums.StatusCode.InternalServerError,
                    MessageForUser = "Невозможно получить сообщение.",
                };
                return BadRequest(response);
            }
        }

        [HttpPost("CreateCat")]
        [SwaggerOperation(Summary = "Создать кота", Description = "Data - содержит информацю о созданном коте.")]
        public async Task<IActionResult> CreateCat([FromBody] string name)
        {
            BaseResponse<CatResponseDTO> response;
            try
            {
                var result = await _catService.TryCreateCat(name, GetUserId());

                if (result.IsSuccess)
                {
                    response = new BaseResponse<CatResponseDTO>
                    {
                        StatusCode = Domain.Enums.StatusCode.Created,
                        Data = result.Data,
                        MessageForUser = $"Ваш новый кот {name}",
                    };
                    return CreatedAtAction("CreateCat", response);
                }
                response = new BaseResponse<CatResponseDTO>
                {
                    StatusCode = Domain.Enums.StatusCode.Conflict,
                    Data = null,
                    MessageForUser = "У вас уже есть кот",
                };
                return BadRequest(response);
            }
            catch (Exception ex)
            {
                response = new BaseResponse<CatResponseDTO>
                {
                    StatusCode = Domain.Enums.StatusCode.InternalServerError,
                    MessageForUser = "Не найти кота для вас, что-то пошло не так...",
                };
                return BadRequest(response);
            }
        }
    }
}

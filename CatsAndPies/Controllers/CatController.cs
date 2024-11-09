using CatsAndPies.Domain.Abstractions.Services.Cat;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Security.Claims;

namespace CatsAndPies.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [SwaggerTag("Работа с котом.")]
    public class CatController : Controller
    {
        private readonly ICatCreationService _catCreationService;
        private readonly ICatMessageService _catMessageService;
        private int GetUserId() => Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
        public CatController(ICatCreationService catCreationService,
            ICatMessageService catMessageService)
        {
            _catCreationService = catCreationService;
            _catMessageService = catMessageService;
        }
        [HttpGet("SaySomething")]
        [SwaggerOperation(Summary = "Получить случайное сообщение от кота", Description = "Data - содержит строку \"что сказал кот\".")]
        public async Task<IActionResult> SaySomething()
        {
            var response = await _catMessageService.SaySomething(GetUserId());
            if(response.StatusCode == Domain.Enums.StatusCode.Ok)
                return Ok(response);
            return BadRequest(response);
        }
        [HttpPost("CreateCat")]
        [SwaggerOperation(Summary = "Создать кота", Description = "Data - содержит информацю о созданном коте.")]
        public async Task<IActionResult> CreateCat(string name)
        {
            var response = await _catCreationService.CreateCat(name, GetUserId());
            if(response.StatusCode == Domain.Enums.StatusCode.Created)
                return CreatedAtAction("CreateCat", response);
            return BadRequest(response);
        }
    }
}

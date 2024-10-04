using CatsAndPies.Domain.Abstractions.Services;
using CatsAndPies.Domain.DTO.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CatsAndPies.Controllers
{

    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class QuestionnaireController : Controller
    {
        private readonly IQuestionnaireService _questionnaireService;
        public QuestionnaireController(IQuestionnaireService questionnaireService)
        {
            _questionnaireService = questionnaireService;
        }
        private int GetUserId() => Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));

        [HttpPost]
        public async Task<IActionResult> AddQuestionnaire([FromBody]QuestionnaireRequestDto model)
        {
            model.UserId = GetUserId();
            var response = await _questionnaireService.Add(model);
            if(response.StatusCode == Domain.Enums.StatusCode.Ok)
                return Ok(response);
            //Авантюра, не знаю, можно ли так???
            return BadRequest(response);
        }
        [HttpGet("GetMyQuestionnaire")]
        public async Task<IActionResult> GetUserQuestionnaire()
        {
            var response = await _questionnaireService.Get(GetUserId());
            if(response.StatusCode == Domain.Enums.StatusCode.Ok)
                return Ok(response);
            return BadRequest(response);
        }
    }
}

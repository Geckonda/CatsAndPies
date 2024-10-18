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
            if(response.StatusCode == Domain.Enums.StatusCode.Created)
                return CreatedAtAction("AddQuestionnaire", response);
            //Авантюра, не знаю, можно ли так???
            return BadRequest(response);
        }
        [HttpGet("GetMyQuestionnaire")]
        public async Task<IActionResult> GetMyQuestionnaire()
        {
            var response = await _questionnaireService.GetByUserId(GetUserId());
            if(response.StatusCode == Domain.Enums.StatusCode.Ok)
                return Ok(response);
            return BadRequest(response);
        }
        [HttpGet("GetUserQuestionnaire")]
        public async Task<IActionResult> GetUserQuestionnaire(int id)
        {
            var response = await _questionnaireService.GetById(id);
            if (response.StatusCode == Domain.Enums.StatusCode.Ok)
                return Ok(response);
            return BadRequest(response);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateFullQuestionnaire([FromBody] QuestionnaireRequestDto model)
        {
            model.UserId = GetUserId();
            var response = await _questionnaireService.UpdateFull(model);
            if (response.StatusCode == Domain.Enums.StatusCode.Ok)
                return Ok(response);
            return BadRequest(response);

        }
        [HttpDelete]
        public async Task<IActionResult> DeleteQuestionnaireByUserId()
        {
            var response = await _questionnaireService.DeleteByUserId(GetUserId());
            if(response.StatusCode == Domain.Enums.StatusCode.Ok)
                return Ok(response);
            return BadRequest(response);
        }
    }
}

using CatsAndPies.Domain.Abstractions.Services;
using CatsAndPies.Domain.DTO.Request;
using Microsoft.AspNetCore.Mvc;

namespace CatsAndPies.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class QuestionnaireController : Controller
    {
        private readonly IQuestionnaireService _questionnaireService;
        public QuestionnaireController(IQuestionnaireService questionnaireService)
        {
            _questionnaireService = questionnaireService;
        }

        [HttpPost]
        public async Task<IActionResult> AddQuestionnaire([FromBody]QuestionnaireRequestDto model)
        {
            var response = await _questionnaireService.Add(model);
            if(response.StatusCode == Domain.Enums.StatusCode.Ok)
                return Ok(response);
            //Авантюра, не знаю, можно ли так???
            return BadRequest(response);
        }
    }
}

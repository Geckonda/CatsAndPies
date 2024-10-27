using CatsAndPies.Domain.Abstractions.Services.Cat;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CatsAndPies.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
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
        public async Task<IActionResult> SaySomething()
        {
            var response = await _catMessageService.SaySomething(GetUserId());
            if(response.StatusCode == Domain.Enums.StatusCode.Ok)
                return Ok(response);
            return BadRequest(response);
        }
        [HttpPost]
        public async Task<IActionResult> CreateCat(string name)
        {
            var response = await _catCreationService.CreateCat(name, GetUserId());
            if(response.StatusCode == Domain.Enums.StatusCode.Created)
                return CreatedAtAction("CreateCat", response);
            return BadRequest(response);
        }
    }
}

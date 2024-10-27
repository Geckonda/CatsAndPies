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
        private int GetUserId() => Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
        public CatController(ICatCreationService catCreationService)
        {
            _catCreationService = catCreationService;
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

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CatsAndPies.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : Controller
    {
        [Authorize]
        [HttpGet("profile")]
        public IActionResult GetProfile()
        {
            var username = User.Identity.Name;
            return Ok(new { username });
        }
        [HttpGet("hello")]
        public IActionResult GetHello()
        {
            return Ok(new { data =  "Hello" });
        }
    }
}

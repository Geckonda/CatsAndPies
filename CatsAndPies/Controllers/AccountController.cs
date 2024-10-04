using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
        public IActionResult SayHello()
        {
            string role = string.Empty;
            if(User.Identity!.IsAuthenticated) { 
                role = User.FindFirst(ClaimTypes.Role)!.Value;
                return Ok(new { data =  $"Hello, My role is {role}" });
            }
            return Ok(new { data = "Hello, I haven't any role(((" });
        }
        [HttpGet("SayHelloToAdmin")]
        [Authorize(Roles = "Admin")]
        public IActionResult SayHelloToAdmin()
        {
            return Ok(new { data = "Hello" });
        }
    }
}

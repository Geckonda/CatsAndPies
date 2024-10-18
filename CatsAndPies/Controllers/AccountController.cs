using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Security.Claims;

namespace CatsAndPies.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : Controller
    {
        [Authorize]
        [SwaggerIgnore]
        [HttpGet("GetUserName")]
        public IActionResult GetProfile()
        {
            var username = User.Identity!.Name;
            return Ok(new { username });
        }
        [HttpGet("hello")]
        [SwaggerIgnore]
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
        [SwaggerIgnore]
        [Authorize(Roles = "Admin")]
        public IActionResult SayHelloToAdmin()
        {
            return Ok(new { data = "Hello" });
        }

        [HttpGet("SwaggerDevAuth")]
        [SwaggerOperation(Summary = "Страница авторизации Swagger", Description = "Возвращает html."),]
        public IActionResult GetLoginPage()
        {
            var htmlContent = System.IO.File.ReadAllText("wwwroot/swagger-auth.html");
            return Content(htmlContent, "text/html");
        }
    }
}

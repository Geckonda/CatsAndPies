using CatsAndPies.Domain.Abstractions.Services;
using CatsAndPies.Domain.Models.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Security.Claims;

namespace CatsAndPies.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    [SwaggerTag("Работа с анкетой. (Full Auth)")]
    public class WalletController : Controller
    {

        private readonly IWalletService _walletService;
        private int GetUserId() => Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
        public WalletController(IWalletService walletService)
        {
            _walletService = walletService;
        }
        [HttpGet("GetBalance")]
        public async Task<IActionResult> GetBalance()
        {
            BaseResponse<decimal> response;
            var result = await _walletService.GetBalance(GetUserId());
            response = new()
            {
                Data = result,
                StatusCode = Domain.Enums.StatusCode.Ok,
                MessageForUser = $"Ваш баланс: {result}"
            };
            return Ok(response);
        }
    }
}

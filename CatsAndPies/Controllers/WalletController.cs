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
           
            var result = await _walletService.GetBalance(GetUserId());
            BaseResponse<decimal> response = new()
            {
                Data = result,
                StatusCode = Domain.Enums.StatusCode.Ok,
                MessageForUser = $"Ваш баланс: {result}"
            };
            return Ok(response);
        }
        [HttpPost("TransferMoney")]
        public async Task<IActionResult> TransferMoney(int userIdTo, decimal sum)
        {
            var userIdFrom = GetUserId();
            var result = await _walletService.TryTransferMoney(userIdFrom, userIdTo, sum);
            if(result.IsSuccess)
            {
                BaseResponse<bool> response = new()
                {
                    StatusCode = Domain.Enums.StatusCode.Ok,
                    Data = result.Data,
                    MessageForUser = "Средства успешно переведены"
                };
                return Ok(response);
            }
            return BadRequest();
        }
    }
}

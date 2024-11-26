using CatsAndPies.Domain.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatsAndPies.Domain.Abstractions.Services
{
    public interface IWalletService
    {
        Task<decimal> GetBalance(int userId);
        Task<Result<bool>> TryTransferMoney(int userIdFrom, int userIdTo, decimal sum);
    }
}

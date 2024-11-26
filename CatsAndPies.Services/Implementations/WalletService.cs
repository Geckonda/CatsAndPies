using AutoMapper;
using CatsAndPies.Domain.Abstractions.Repositories.Combined;
using CatsAndPies.Domain.Abstractions.Services;
using CatsAndPies.Domain.Entities;
using CatsAndPies.Domain.Enums;
using CatsAndPies.Domain.Exceptions.Items;
using CatsAndPies.Domain.Models.Wallet;
using CatsAndPies.Domain.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatsAndPies.Services.Implementations
{
    public class WalletService : IWalletService
    {
        private readonly IWalletRepository _walletRepository;
        private readonly IMapper _mapper;
        public WalletService(IWalletRepository walletRepository,
            IMapper mapper)
        {
            _walletRepository = walletRepository;
            _mapper = mapper;
        }
        public async Task<decimal> GetBalance(int userId)
        {
            return await _walletRepository.GetBalanceByUserId(userId); ;
        }

        public async Task<Result<bool>> TryTransferMoney(int userIdFrom, int userIdTo, decimal sum)
        {
            var walletToEntity = await _walletRepository.GetOneByUserId(userIdTo);
            if(walletToEntity == null)
                throw new RequestHandlingException(StatusCode.NotFound, "Кошелек получателя не найден.");

            var walletFromEntity = await _walletRepository.GetOneByUserId(userIdFrom);
            if (walletFromEntity == null)
                throw new RequestHandlingException(StatusCode.NotFound, "Кошелек отправителя не найден.");

            if (walletFromEntity.Balance < sum)
                throw new RequestHandlingException(StatusCode.Conflict, "Недостаточно средств для перевода.");

            var walletFrom = _mapper.Map<Wallet>(walletFromEntity);
            var walletTo = _mapper.Map<Wallet>(walletToEntity);


            // Добавить транзакции, а там и Result<T>.Error()
            walletTo.Deposit(walletFrom.Withdraw(sum));
            walletToEntity = _mapper.Map<WalletEntity>(walletTo);
            await _walletRepository.Update(walletToEntity);
            walletFromEntity = _mapper.Map<WalletEntity>(walletFrom);
            await _walletRepository.Update(walletFromEntity);
            return Result<bool>.SuccessResult(true);
        }
    }
}

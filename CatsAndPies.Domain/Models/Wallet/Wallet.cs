using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatsAndPies.Domain.Models.Wallet
{
    public class Wallet
    {
        public int Id { get; set; }
        public decimal Balance { get; private set; }


        public void Deposit(decimal amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Сумма должна быть положительной.");
            Balance += amount;
        }
        public decimal Withdraw(decimal amount)
        {
            if (Balance <= 0)
                throw new InvalidOperationException("Недостаточно средств на кошельке.");
            Balance -= amount;
            return Balance;
        }
    }
}

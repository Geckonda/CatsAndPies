using CatsAndPies.Domain.Models.Wallet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatsAndPies.Tests.Domain.Models
{
    public class WalletTest
    {
        [Fact]
        public void Deposit_ShouldIncreaseBalance_WhenAmountIsPositive()
        {
            // Arrange
            var wallet = new Wallet();
            decimal initialBalance = wallet.Balance;
            decimal depositAmount = 100m;

            // Act
            wallet.Deposit(depositAmount);

            // Assert
            Assert.Equal(initialBalance + depositAmount, wallet.Balance);
        }

        [Fact]
        public void Deposit_ShouldThrowArgumentException_WhenAmountIsNonPositive()
        {
            // Arrange
            var wallet = new Wallet();
            decimal depositAmount = -50m;

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => wallet.Deposit(depositAmount));
            Assert.Equal("Сумма должна быть положительной.", exception.Message);
        }

        [Fact]
        public void Withdraw_ShouldDecreaseBalance_WhenBalanceIsSufficient()
        {
            // Arrange
            var wallet = new Wallet();
            wallet.Deposit(100m);
            decimal withdrawAmount = 50m;

            // Act
            decimal remainingBalance = wallet.Withdraw(withdrawAmount);

            // Assert
            Assert.Equal(50m, remainingBalance);
            Assert.Equal(50m, wallet.Balance);
        }

        [Fact]
        public void Withdraw_ShouldThrowInvalidOperationException_WhenBalanceIsInsufficient()
        {
            // Arrange
            var wallet = new Wallet();

            // Act & Assert
            var exception = Assert.Throws<InvalidOperationException>(() => wallet.Withdraw(10m));
            Assert.Equal("Недостаточно средств на кошельке.", exception.Message);
        }

        [Fact]
        public void Withdraw_ShouldThrowInvalidOperationException_WhenBalanceIsZero()
        {
            // Arrange
            var wallet = new Wallet();

            // Act & Assert
            var exception = Assert.Throws<InvalidOperationException>(() => wallet.Withdraw(0m));
            Assert.Equal("Недостаточно средств на кошельке.", exception.Message);
        }
    }
}

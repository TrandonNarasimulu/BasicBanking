using BasicBanking.Application.Banking.Queires.GetAccountBalance;
using BasicBanking.Application.Common.Exceptions;
using BasicBanking.Application.Common.Interfaces;
using BasicBanking.Domain.Entities;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Xunit;

namespace BasicBanking.Application.UnitTests.Banking.Queries.GetAccountBalance
{
    public class GetAccountBalanceQueryTests
    {
        [Fact]
        public async void Handle_GetAccountBalannce_ShouldSucceed()
        {
            //Arrange
            var command = new AccountBalanceQuery
            {
                AccountNumber = "1234567891"
            };

            var bankingMock = new Mock<IBanking>();

            var bankAccount = new BankAccount { AccountNumber = "123456789", Balance = 100, UserId = 1 };
            var user = new User { FirstName = "John", LastName = "Doe", IDNumber = "1234567891234" };

            bankingMock.Setup(x => x.GetBankAccountDetails(It.IsAny<string>())).ReturnsAsync(bankAccount);
            bankingMock.Setup(x => x.GetUserDetails(It.IsAny<long>())).ReturnsAsync(user);
            
            var handler = new AccountBalanceQueryHandler(bankingMock.Object);

            //Act
            var response = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.True(response.GetType() == typeof(AccountBalanceViewModel));
            bankingMock.Verify(x => x.GetBankAccountDetails(It.IsAny<string>()), Times.Once);
            bankingMock.Verify(x => x.GetUserDetails(It.IsAny<long>()), Times.Once);
        }

        [Fact]
        public async void Handle_GetAccountBalance_ShouldThrowNotFoundExceptionForBankAccount()
        {
            //Arrange
            var command = new AccountBalanceQuery
            {
                AccountNumber = "1234567891"
            };

            var bankingMock = new Mock<IBanking>();

            BankAccount bankAccount = null;
            var user = new User { FirstName = "John", LastName = "Doe", IDNumber = "1234567891234" };

            bankingMock.Setup(x => x.GetBankAccountDetails(It.IsAny<string>())).ReturnsAsync(bankAccount);
            bankingMock.Setup(x => x.GetUserDetails(It.IsAny<long>())).ReturnsAsync(user);

            var handler = new AccountBalanceQueryHandler(bankingMock.Object);

            //Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(command, CancellationToken.None));

            bankingMock.Verify(x => x.GetBankAccountDetails(It.IsAny<string>()), Times.Once);
            bankingMock.Verify(x => x.GetUserDetails(It.IsAny<long>()), Times.Never);
        }

        [Fact]
        public async void Handle_GetAccountBalance_ShouldThrowNotFoundExceptionForUser()
        {
            //Arrange
            var command = new AccountBalanceQuery
            {
                AccountNumber = "1234567891"
            };

            var bankingMock = new Mock<IBanking>();

            var bankAccount = new BankAccount { AccountNumber = "123456789", Balance = 100, UserId = 1 };
            User user = null;

            bankingMock.Setup(x => x.GetBankAccountDetails(It.IsAny<string>())).ReturnsAsync(bankAccount);
            bankingMock.Setup(x => x.GetUserDetails(It.IsAny<long>())).ReturnsAsync(user);

            var handler = new AccountBalanceQueryHandler(bankingMock.Object);

            //Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(command, CancellationToken.None));

            bankingMock.Verify(x => x.GetBankAccountDetails(It.IsAny<string>()), Times.Once);
            bankingMock.Verify(x => x.GetUserDetails(It.IsAny<long>()), Times.Once);
        }
    }
}

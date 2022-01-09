using BasicBanking.Application.Banking.Queires.GetBalanceByIDNumber;
using BasicBanking.Application.Common.Exceptions;
using BasicBanking.Application.Common.Interfaces;
using BasicBanking.Domain.Entities;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Xunit;

namespace BasicBanking.Application.UnitTests.Banking.Queries.GetBalanceByIDNumber
{
    public class GetBalanceByIDNumberQueryTests
    {
        [Fact]
        public async void Handle_GetAccountsBalannce_ShouldSucceed()
        {
            //Arrange
            var command = new BalanceByIDQuery
            {
                IDNumber = "1234567891234"
            };

            var bankingMock = new Mock<IBanking>();

            var bankAccounts = new List<BankAccount> { new BankAccount { AccountNumber = "123456789", Balance = 100, UserId = 1 } };
            var user = new User { FirstName = "John", LastName = "Doe", IDNumber = "1234567891234" };

            bankingMock.Setup(x => x.GetAllUserBankAccounts(It.IsAny<string>())).Returns(bankAccounts);
            bankingMock.Setup(x => x.GetUserDetails(It.IsAny<string>())).ReturnsAsync(user);

            var handler = new BalanceByIDQueryHandler(bankingMock.Object);

            //Act
            var response = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.True(response.GetType() == typeof(BalanceByIDViewModel));
            bankingMock.Verify(x => x.GetAllUserBankAccounts(It.IsAny<string>()), Times.Once);
            bankingMock.Verify(x => x.GetUserDetails(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async void Handle_GetAccountsBalannce_ShouldThrowNotFoundExceptionForUser()
        {
            //Arrange
            var command = new BalanceByIDQuery
            {
                IDNumber = "1234567891234"
            };

            var bankingMock = new Mock<IBanking>();

            var bankAccounts = new List<BankAccount> { new BankAccount { AccountNumber = "123456789", Balance = 100, UserId = 1 } };
            User user = null;

            bankingMock.Setup(x => x.GetAllUserBankAccounts(It.IsAny<string>())).Returns(bankAccounts);
            bankingMock.Setup(x => x.GetUserDetails(It.IsAny<string>())).ReturnsAsync(user);

            var handler = new BalanceByIDQueryHandler(bankingMock.Object);

            //Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(command, CancellationToken.None));

            bankingMock.Verify(x => x.GetAllUserBankAccounts(It.IsAny<string>()), Times.Never);
            bankingMock.Verify(x => x.GetUserDetails(It.IsAny<string>()), Times.Once);
        }
    }
}

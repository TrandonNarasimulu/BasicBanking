using BasicBanking.Application.Banking.Queires.GetTransferHistory;
using BasicBanking.Application.Common.Exceptions;
using BasicBanking.Application.Common.Interfaces;
using BasicBanking.Domain.Entities;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Xunit;

namespace BasicBanking.Application.UnitTests.Banking.Queries.GetTransferHistory
{
    public class GetTransferHistoryQueryTests
    {
        [Fact]
        public async void Handle_GetAccountsBalannce_ShouldSucceed()
        {
            //Arrange
            var command = new TransferHistoryQuery
            {
                AccountNumber = "1234567891"
            };

            var bankingMock = new Mock<IBanking>();

            var bankAccount = new BankAccount { AccountNumber = "123456789", Balance = 100, UserId = 1 };
            var transactions = new List<Transaction> 
            { 
                new Transaction 
                { 
                    Amount = 100, 
                    MainAccountNumber = "1234567891", 
                    OtherAccountNumber = "9876543219", 
                    TransactionDetails = "Deposit" 
                } 
            };

            bankingMock.Setup(x => x.GetBankAccountDetails(It.IsAny<string>())).ReturnsAsync(bankAccount);
            bankingMock.Setup(x => x.GetTransferHistory(It.IsAny<string>())).Returns(transactions);

            var handler = new TransferHistoryQueryHandler(bankingMock.Object);

            //Act
            var response = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.True(response.GetType() == typeof(TransferHistoryViewModel));
            bankingMock.Verify(x => x.GetBankAccountDetails(It.IsAny<string>()), Times.Once);
            bankingMock.Verify(x => x.GetTransferHistory(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async void Handle_GetAccountsBalannce_ShouldSucceedWithNoTransactions()
        {
            //Arrange
            var command = new TransferHistoryQuery
            {
                AccountNumber = "1234567891"
            };

            var bankingMock = new Mock<IBanking>();

            var bankAccount = new BankAccount { AccountNumber = "123456789", Balance = 100, UserId = 1 };
            List<Transaction> transactions = null;

            bankingMock.Setup(x => x.GetBankAccountDetails(It.IsAny<string>())).ReturnsAsync(bankAccount);
            bankingMock.Setup(x => x.GetTransferHistory(It.IsAny<string>())).Returns(transactions);

            var handler = new TransferHistoryQueryHandler(bankingMock.Object);

            //Act
            var response = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.True(response.GetType() == typeof(TransferHistoryViewModel));
            Assert.True(response.TransferHistory.Count == 0);
            bankingMock.Verify(x => x.GetBankAccountDetails(It.IsAny<string>()), Times.Once);
            bankingMock.Verify(x => x.GetTransferHistory(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async void Handle_GetAccountsBalannce_ShouldNotFoundExceptionForBankAccount()
        {
            //Arrange
            var command = new TransferHistoryQuery
            {
                AccountNumber = "1234567891"
            };

            var bankingMock = new Mock<IBanking>();

            BankAccount bankAccount = null;
            List<Transaction> transactions = null;

            bankingMock.Setup(x => x.GetBankAccountDetails(It.IsAny<string>())).ReturnsAsync(bankAccount);
            bankingMock.Setup(x => x.GetTransferHistory(It.IsAny<string>())).Returns(transactions);

            var handler = new TransferHistoryQueryHandler(bankingMock.Object);

            //Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(command, CancellationToken.None));            
            bankingMock.Verify(x => x.GetBankAccountDetails(It.IsAny<string>()), Times.Once);
            bankingMock.Verify(x => x.GetTransferHistory(It.IsAny<string>()), Times.Never);
        }
    }
}

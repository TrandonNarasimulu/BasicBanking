using BasicBanking.Application.Banking.Commands.TransferMoney;
using BasicBanking.Application.Common.Exceptions;
using BasicBanking.Application.Common.Interfaces;
using BasicBanking.Domain.Entities;
using MediatR;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Xunit;

namespace BasicBanking.Application.UnitTests.Banking.Commands.TransferMoney
{
    public class TransferMoneyCommandTests
    {
        [Fact]
        public async void Handle_TransferMoney_ShouldSucceed()
        {
            //Arrange
            var command = new TransferMoneyCommand
            {
                SourceAccountNumber = "1234567891",
                DestinationAccountNumber = "9876543219",
                Amount = 100
            };

            var bankingMock = new Mock<IBanking>();

            var bankAccount = new BankAccount { AccountNumber = "123456789", Balance = 100, UserId = 1 };

            bankingMock.Setup(x => x.GetBankAccountDetails(It.IsAny<string>())).ReturnsAsync(bankAccount);
            bankingMock.Setup(x => x.TransferMoney(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<double>(), It.IsAny<CancellationToken>()));
            bankingMock.Setup(x => x.UpdateTransferHistory(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<double>(), It.IsAny<CancellationToken>()));
            
            var handler = new TransferMoneyCommandHandler(bankingMock.Object);

            //Act
            var response = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.True(response.GetType() == typeof(Unit));
            bankingMock.Verify(x => x.GetBankAccountDetails(It.IsAny<string>()), Times.Exactly(2));
            bankingMock.Verify(x => x.TransferMoney(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<double>(), 
                It.IsAny<CancellationToken>()), Times.Once);
            bankingMock.Verify(x => x.UpdateTransferHistory(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), 
                It.IsAny<double>(), It.IsAny<CancellationToken>()), Times.Exactly(2));
        }

        [Fact]
        public async void Handle_TransferMoney_ShouldThrowNotFoundExceptionForSrcAccount()
        {
            //Arrange
            var command = new TransferMoneyCommand
            {
                SourceAccountNumber = "1234567891",
                DestinationAccountNumber = "9876543219",
                Amount = 100
            };

            var bankingMock = new Mock<IBanking>();

            BankAccount bankAccount = null;

            bankingMock.Setup(x => x.GetBankAccountDetails(It.IsAny<string>())).ReturnsAsync(bankAccount);
            bankingMock.Setup(x => x.TransferMoney(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<double>(), It.IsAny<CancellationToken>()));
            bankingMock.Setup(x => x.UpdateTransferHistory(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<double>(), It.IsAny<CancellationToken>()));

            var handler = new TransferMoneyCommandHandler(bankingMock.Object);

            //Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(command, CancellationToken.None));

            bankingMock.Verify(x => x.GetBankAccountDetails(It.IsAny<string>()), Times.Exactly(1));
            bankingMock.Verify(x => x.TransferMoney(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<double>(),
                It.IsAny<CancellationToken>()), Times.Never);
            bankingMock.Verify(x => x.UpdateTransferHistory(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(),
                It.IsAny<double>(), It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact]
        public async void Handle_TransferMoney_ShouldThrowInsufficientFundsException()
        {
            //Arrange
            var command = new TransferMoneyCommand
            {
                SourceAccountNumber = "1234567891",
                DestinationAccountNumber = "9876543219",
                Amount = 200
            };

            var bankingMock = new Mock<IBanking>();

            var bankAccount = new BankAccount { AccountNumber = "123456789", Balance = 100, UserId = 1 };

            bankingMock.Setup(x => x.GetBankAccountDetails(It.IsAny<string>())).ReturnsAsync(bankAccount);
            bankingMock.Setup(x => x.TransferMoney(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<double>(), It.IsAny<CancellationToken>()));
            bankingMock.Setup(x => x.UpdateTransferHistory(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<double>(), It.IsAny<CancellationToken>()));

            var handler = new TransferMoneyCommandHandler(bankingMock.Object);

            //Act & Assert
            await Assert.ThrowsAsync<InsufficientFundsException>(async () => await handler.Handle(command, CancellationToken.None));

            bankingMock.Verify(x => x.GetBankAccountDetails(It.IsAny<string>()), Times.Exactly(1));
            bankingMock.Verify(x => x.TransferMoney(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<double>(),
                It.IsAny<CancellationToken>()), Times.Never);
            bankingMock.Verify(x => x.UpdateTransferHistory(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(),
                It.IsAny<double>(), It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact]
        public async void Handle_TransferMoney_ShouldThrowNotFoundExceptionForDestAccount()
        {
            //Arrange
            var command = new TransferMoneyCommand
            {
                SourceAccountNumber = "1234567891",
                DestinationAccountNumber = "9876543219",
                Amount = 100
            };

            var bankingMock = new Mock<IBanking>();

            var srcBankAccount = new BankAccount { AccountNumber = "123456789", Balance = 100, UserId = 1 };
            BankAccount destBankAccount = null;

            bankingMock.SetupSequence(x => x.GetBankAccountDetails(It.IsAny<string>()))
                .ReturnsAsync(srcBankAccount)
                .ReturnsAsync(destBankAccount);
            bankingMock.Setup(x => x.TransferMoney(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<double>(), It.IsAny<CancellationToken>()));
            bankingMock.Setup(x => x.UpdateTransferHistory(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<double>(), It.IsAny<CancellationToken>()));

            var handler = new TransferMoneyCommandHandler(bankingMock.Object);

            //Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(command, CancellationToken.None));

            bankingMock.Verify(x => x.GetBankAccountDetails(It.IsAny<string>()), Times.Exactly(2));
            bankingMock.Verify(x => x.TransferMoney(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<double>(),
                It.IsAny<CancellationToken>()), Times.Never);
            bankingMock.Verify(x => x.UpdateTransferHistory(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(),
                It.IsAny<double>(), It.IsAny<CancellationToken>()), Times.Never);
        }
    }
}

using BasicBanking.Application.Banking.Commands.CreateAccount;
using BasicBanking.Application.Common.Interfaces;
using BasicBanking.Domain.Entities;
using Moq;
using System.Threading;
using Xunit;

namespace BasicBanking.Application.UnitTests.Banking.Commands.CreateAccount
{
    public class CreateAccountCommandTests
    {
        [Fact]
        public async void Handle_CreateAccount_ShouldSucceedWithNewUser()
        {
            //Arrange
            var command = new CreateAccountCommand
            {
                FirstName = "SampleName",
                LastName = "SampleLastName",
                IDNumber = "1234567891234",
                InitialDeposit = 100
            };

            var bankingMock = new Mock<IBanking>();
            var mathMock = new Mock<IMath>();

            User nullUser = null;
            User user = new User { FirstName = "Test", LastName = "Test", IDNumber = "1234567891234" };

            bankingMock.SetupSequence(x => x.GetUserDetails(It.IsAny<string>()))
                .ReturnsAsync(nullUser)
                .ReturnsAsync(user);
            bankingMock.Setup(x => x.CreateUser(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()));
            bankingMock.Setup(x => x.CreateAccount(It.IsAny<string>(), It.IsAny<User>(), It.IsAny<double>(), It.IsAny<CancellationToken>()));
            bankingMock.Setup(x => x.UpdateTransferHistory(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<double>(), It.IsAny<CancellationToken>()));
            bankingMock.SetupSequence(x => x.AccountNumberExists(It.IsAny<string>())).ReturnsAsync(false).ReturnsAsync(true);

            mathMock.Setup(x => x.GetRandomNumber(It.IsAny<int>())).Returns(1);

            var handler = new CreateAccountCommandHandler(bankingMock.Object, mathMock.Object);

            //Act
            var response = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.True(response.GetType() == typeof(CreateAccountViewModel));
            mathMock.Verify(x => x.GetRandomNumber(It.IsAny<int>()), Times.AtLeast(10));
            bankingMock.Verify(x => x.GetUserDetails(It.IsAny<string>()), Times.Exactly(2));
            bankingMock.Verify(x => x.CreateUser(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Once);
            bankingMock.Verify(x => x.CreateAccount(It.IsAny<string>(), It.IsAny<User>(), It.IsAny<double>(), It.IsAny<CancellationToken>()), Times.Once);
            bankingMock.Verify(x => x.UpdateTransferHistory(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<double>(), 
                It.IsAny<CancellationToken>()), Times.Once);
            bankingMock.Verify(x => x.AccountNumberExists(It.IsAny<string>()), Times.AtLeast(1));
        }

        [Fact]
        public async void Handle_CreateAccount_ShouldSucceedWithExistingUser()
        {
            //Arrange
            var command = new CreateAccountCommand
            {
                FirstName = "SampleName",
                LastName = "SampleLastName",
                IDNumber = "1234567891234",
                InitialDeposit = 100
            };

            var bankingMock = new Mock<IBanking>();
            var mathMock = new Mock<IMath>();

            User user = new User { FirstName = "Test", LastName = "Test", IDNumber = "1234567891234" };

            bankingMock.Setup(x => x.GetUserDetails(It.IsAny<string>())).ReturnsAsync(user);
            bankingMock.Setup(x => x.CreateUser(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()));
            bankingMock.Setup(x => x.CreateAccount(It.IsAny<string>(), It.IsAny<User>(), It.IsAny<double>(), It.IsAny<CancellationToken>()));
            bankingMock.Setup(x => x.UpdateTransferHistory(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<double>(), It.IsAny<CancellationToken>()));
            bankingMock.SetupSequence(x => x.AccountNumberExists(It.IsAny<string>())).ReturnsAsync(false).ReturnsAsync(true);

            mathMock.Setup(x => x.GetRandomNumber(It.IsAny<int>())).Returns(1);

            var handler = new CreateAccountCommandHandler(bankingMock.Object, mathMock.Object);

            //Act
            var response = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.True(response.GetType() == typeof(CreateAccountViewModel));
            mathMock.Verify(x => x.GetRandomNumber(It.IsAny<int>()), Times.AtLeast(10));
            bankingMock.Verify(x => x.GetUserDetails(It.IsAny<string>()), Times.Exactly(1));
            bankingMock.Verify(x => x.CreateUser(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Never);
            bankingMock.Verify(x => x.CreateAccount(It.IsAny<string>(), It.IsAny<User>(), It.IsAny<double>(), It.IsAny<CancellationToken>()), Times.Once);
            bankingMock.Verify(x => x.UpdateTransferHistory(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<double>(),
                It.IsAny<CancellationToken>()), Times.Once);
            bankingMock.Verify(x => x.AccountNumberExists(It.IsAny<string>()), Times.AtLeast(1));
        }
    }
}

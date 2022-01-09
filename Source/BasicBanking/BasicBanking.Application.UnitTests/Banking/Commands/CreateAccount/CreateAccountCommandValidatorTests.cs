using BasicBanking.Application.Banking.Commands.CreateAccount;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace BasicBanking.Application.UnitTests.Banking.Commands.CreateAccount
{
    public class CreateAccountCommandValidatorTests
    {
        [Fact]
        public void ValidationShouldBeSuccessful()
        {
            //Arrange
            var command = new CreateAccountCommand
            {
                FirstName = "Test",
                LastName = "Test",
                IDNumber = "1234567891234",
                InitialDeposit = 100
            };

            //Actual
            var validationResult = new CreateAccountCommandValidator().Validate(command);

            //Assert
            Assert.True(validationResult.IsValid);
        }

        [Fact]
        public void Validate_EmptyName_ShouldThrowValiadtionException()
        {
            //Arrange
            var command = new CreateAccountCommand
            {
                FirstName = "",
                LastName = "Test",
                IDNumber = "1234567891234",
                InitialDeposit = 100
            };

            //Actual
            var validationResult = new CreateAccountCommandValidator().Validate(command);

            //Assert
            Assert.False(validationResult.IsValid);
        }

        [Fact]
        public void Validate_EmptyLastName_ShouldThrowValiadtionException()
        {
            //Arrange
            var command = new CreateAccountCommand
            {
                FirstName = "Test",
                LastName = "",
                IDNumber = "1234567891234",
                InitialDeposit = 100
            };

            //Actual
            var validationResult = new CreateAccountCommandValidator().Validate(command);

            //Assert
            Assert.False(validationResult.IsValid);
        }

        [Fact]
        public void Validate_EmptyID_ShouldThrowValiadtionException()
        {
            //Arrange
            var command = new CreateAccountCommand
            {
                FirstName = "Test",
                LastName = "Test",
                IDNumber = "",
                InitialDeposit = 100
            };

            //Actual
            var validationResult = new CreateAccountCommandValidator().Validate(command);

            //Assert
            Assert.False(validationResult.IsValid);
        }

        [Fact]
        public void Validate_ZeroDeposit_ShouldThrowValiadtionException()
        {
            //Arrange
            var command = new CreateAccountCommand
            {
                FirstName = "Test",
                LastName = "Test",
                IDNumber = "1234567891234",
                InitialDeposit = 0
            };

            //Actual
            var validationResult = new CreateAccountCommandValidator().Validate(command);

            //Assert
            Assert.False(validationResult.IsValid);
        }

        [Fact]
        public void Validate_NullName_ShouldThrowValiadtionException()
        {
            //Arrange
            var command = new CreateAccountCommand
            {
                FirstName = null,
                LastName = "Test",
                IDNumber = "1234567891234",
                InitialDeposit = 100
            };

            //Actual
            var validationResult = new CreateAccountCommandValidator().Validate(command);

            //Assert
            Assert.False(validationResult.IsValid);
        }

        [Fact]
        public void Validate_NullLastName_ShouldThrowValiadtionException()
        {
            //Arrange
            var command = new CreateAccountCommand
            {
                FirstName = "Test",
                LastName = null,
                IDNumber = "1234567891234",
                InitialDeposit = 100
            };

            //Actual
            var validationResult = new CreateAccountCommandValidator().Validate(command);

            //Assert
            Assert.False(validationResult.IsValid);
        }

        [Fact]
        public void Validate_NulllID_ShouldThrowValiadtionException()
        {
            //Arrange
            var command = new CreateAccountCommand
            {
                FirstName = "Test",
                LastName = "Test",
                IDNumber = null,
                InitialDeposit = 100
            };

            //Actual
            var validationResult = new CreateAccountCommandValidator().Validate(command);

            //Assert
            Assert.False(validationResult.IsValid);
        }

        [Fact]
        public void Validate_EmptyLastNameAndFirstName_ShouldThrowValiadtionException()
        {
            //Arrange
            var command = new CreateAccountCommand
            {
                FirstName = "",
                LastName = "",
                IDNumber = "1234567891234",
                InitialDeposit = 100
            };

            //Actual
            var validationResult = new CreateAccountCommandValidator().Validate(command);

            //Assert
            Assert.False(validationResult.IsValid);
        }

        [Fact]
        public void Validate_EmptyNameAndID_ShouldThrowValiadtionException()
        {
            //Arrange
            var command = new CreateAccountCommand
            {
                FirstName = "",
                LastName = "Test",
                IDNumber = "",
                InitialDeposit = 100
            };

            //Actual
            var validationResult = new CreateAccountCommandValidator().Validate(command);

            //Assert
            Assert.False(validationResult.IsValid);
        }

        [Fact]
        public void Validate_EmptyLastNameAndID_ShouldThrowValiadtionException()
        {
            //Arrange
            var command = new CreateAccountCommand
            {
                FirstName = "Test",
                LastName = "",
                IDNumber = "",
                InitialDeposit = 100
            };

            //Actual
            var validationResult = new CreateAccountCommandValidator().Validate(command);

            //Assert
            Assert.False(validationResult.IsValid);
        }

        [Fact]
        public void Validate_NullFirstNameAndNullLastName_ShouldThrowValiadtionException()
        {
            //Arrange
            var command = new CreateAccountCommand
            {
                FirstName = null,
                LastName = null,
                IDNumber = "1234567891234",
                InitialDeposit = 100
            };

            //Actual
            var validationResult = new CreateAccountCommandValidator().Validate(command);

            //Assert
            Assert.False(validationResult.IsValid);
        }

        [Fact]
        public void Validate_NullFirstNameNullID_ShouldThrowValiadtionException()
        {
            //Arrange
            var command = new CreateAccountCommand
            {
                FirstName = null,
                LastName = "Test",
                IDNumber = null,
                InitialDeposit = 100
            };

            //Actual
            var validationResult = new CreateAccountCommandValidator().Validate(command);

            //Assert
            Assert.False(validationResult.IsValid);
        }

        [Fact]
        public void Validate_NullLastNameNullID_ShouldThrowValiadtionException()
        {
            //Arrange
            var command = new CreateAccountCommand
            {
                FirstName = "Test",
                LastName = null,
                IDNumber = null,
                InitialDeposit = 100
            };

            //Actual
            var validationResult = new CreateAccountCommandValidator().Validate(command);

            //Assert
            Assert.False(validationResult.IsValid);
        }

        [Fact]
        public void Validate_InvalidID_ShouldThrowValiadtionException()
        {
            //Arrange
            var command = new CreateAccountCommand
            {
                FirstName = "Test",
                LastName = "Test",
                IDNumber = "123456",
                InitialDeposit = 100
            };

            //Actual
            var validationResult = new CreateAccountCommandValidator().Validate(command);

            //Assert
            Assert.False(validationResult.IsValid);
        }

        [Fact]
        public void Validate_NegetiveDeposit_ShouldThrowValiadtionException()
        {
            //Arrange
            var command = new CreateAccountCommand
            {
                FirstName = "Test",
                LastName = "Test",
                IDNumber = "1234567891234",
                InitialDeposit = -1
            };

            //Actual
            var validationResult = new CreateAccountCommandValidator().Validate(command);

            //Assert
            Assert.False(validationResult.IsValid);
        }
    }
}

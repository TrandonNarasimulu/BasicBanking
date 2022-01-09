using BasicBanking.Application.Banking.Commands.TransferMoney;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace BasicBanking.Application.UnitTests.Banking.Commands.TransferMoney
{
    public class TransferMoneyCommandValidatorTests
    {
        [Fact]
        public void ValidationShouldBeSuccessful()
        {
            //Arrange
            var command = new TransferMoneyCommand
            {
                SourceAccountNumber = "Test",
                DestinationAccountNumber = "Test",
                Amount = 100
            };

            //Actual
            var validationResult = new TransferMoneyCommandValidator().Validate(command);

            //Assert
            Assert.True(validationResult.IsValid);
        }

        [Fact]
        public void Validate_EmptySrcAccount_ShouldThrowValidationException()
        {
            //Arrange
            var command = new TransferMoneyCommand
            {
                SourceAccountNumber = "",
                DestinationAccountNumber = "Test",
                Amount = 100
            };

            //Actual
            var validationResult = new TransferMoneyCommandValidator().Validate(command);

            //Assert
            Assert.False(validationResult.IsValid);
        }

        [Fact]
        public void Validate_EmptyDestAccount_ShouldThrowValidationException()
        {
            //Arrange
            var command = new TransferMoneyCommand
            {
                SourceAccountNumber = "Test",
                DestinationAccountNumber = "",
                Amount = 100
            };

            //Actual
            var validationResult = new TransferMoneyCommandValidator().Validate(command);

            //Assert
            Assert.False(validationResult.IsValid);
        }

        [Fact]
        public void Validate_ZeroAmount_ShouldThrowValidationException()
        {
            //Arrange
            var command = new TransferMoneyCommand
            {
                SourceAccountNumber = "Test",
                DestinationAccountNumber = "Test",
                Amount = 0
            };

            //Actual
            var validationResult = new TransferMoneyCommandValidator().Validate(command);

            //Assert
            Assert.False(validationResult.IsValid);
        }

        [Fact]
        public void Validate_EmptySrcAccEmptyDestAcc_ShouldThrowValidationException()
        {
            //Arrange
            var command = new TransferMoneyCommand
            {
                SourceAccountNumber = "",
                DestinationAccountNumber = "",
                Amount = 10
            };

            //Actual
            var validationResult = new TransferMoneyCommandValidator().Validate(command);

            //Assert
            Assert.False(validationResult.IsValid);
        }

        [Fact]
        public void Validate_NullSrcAccNullDestAcc_ShouldThrowValidationException()
        {
            //Arrange
            var command = new TransferMoneyCommand
            {
                SourceAccountNumber = null,
                DestinationAccountNumber = null,
                Amount = 10
            };

            //Actual
            var validationResult = new TransferMoneyCommandValidator().Validate(command);

            //Assert
            Assert.False(validationResult.IsValid);
        }

        [Fact]
        public void Validate_NullSrcAccount_ShouldThrowValidationException()
        {
            //Arrange
            var command = new TransferMoneyCommand
            {
                SourceAccountNumber = null,
                DestinationAccountNumber = "Test",
                Amount = 10
            };

            //Actual
            var validationResult = new TransferMoneyCommandValidator().Validate(command);

            //Assert
            Assert.False(validationResult.IsValid);
        }

        [Fact]
        public void Validate_NullDestAccount_ShouldThrowValidationException()
        {
            //Arrange
            var command = new TransferMoneyCommand
            {
                SourceAccountNumber = "Test",
                DestinationAccountNumber = null,
                Amount = 10
            };

            //Actual
            var validationResult = new TransferMoneyCommandValidator().Validate(command);

            //Assert
            Assert.False(validationResult.IsValid);
        }

        [Fact]
        public void Validate_NegetiveAmount_ShouldThrowValidationException()
        {
            //Arrange
            var command = new TransferMoneyCommand
            {
                SourceAccountNumber = "Test",
                DestinationAccountNumber = "Test",
                Amount = -10
            };

            //Actual
            var validationResult = new TransferMoneyCommandValidator().Validate(command);

            //Assert
            Assert.False(validationResult.IsValid);
        }
    }
}

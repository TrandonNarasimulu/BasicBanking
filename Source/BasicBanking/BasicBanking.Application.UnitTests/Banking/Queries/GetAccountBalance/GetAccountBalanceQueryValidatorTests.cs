using BasicBanking.Application.Banking.Queires.GetAccountBalance;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace BasicBanking.Application.UnitTests.Banking.Queries.GetAccountBalance
{
    public class GetAccountBalanceQueryValidatorTests
    {
        [Fact]
        public void ValidationShouldBeSuccessful()
        {
            //Arrange
            var command = new AccountBalanceQuery
            {
                AccountNumber = "1234567891"
            };

            //Actual
            var validationResult = new AccountBalanceQueryValidator().Validate(command);

            //Assert
            Assert.True(validationResult.IsValid);
        }

        [Fact]
        public void Validate_EmptyAccountNumber_ShouldThrowValidationException()
        {
            //Arrange
            var command = new AccountBalanceQuery
            {
                AccountNumber = ""
            };

            //Actual
            var validationResult = new AccountBalanceQueryValidator().Validate(command);

            //Assert
            Assert.False(validationResult.IsValid);
        }

        [Fact]
        public void Validate_NullAccountNumber_ShouldThrowValidationException()
        {
            //Arrange
            var command = new AccountBalanceQuery
            {
                AccountNumber = null
            };

            //Actual
            var validationResult = new AccountBalanceQueryValidator().Validate(command);

            //Assert
            Assert.False(validationResult.IsValid);
        }
    }
}

using BasicBanking.Application.Banking.Queires.GetBalanceByIDNumber;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace BasicBanking.Application.UnitTests.Banking.Queries.GetBalanceByIDNumber
{
    public class GetBalanceByIDNumberQueryValidatorTests
    {
        [Fact]
        public void ValidationShouldBeSuccessful()
        {
            //Arrange
            var command = new BalanceByIDQuery
            {
                IDNumber = "1234567891234"
            };

            //Actual
            var validationResult = new BalanceByIDQueryValidator().Validate(command);

            //Assert
            Assert.True(validationResult.IsValid);
        }

        [Fact]
        public void Validate_EmptyID_ShouldThrowValidationException()
        {
            //Arrange
            var command = new BalanceByIDQuery
            {
                IDNumber = ""
            };

            //Actual
            var validationResult = new BalanceByIDQueryValidator().Validate(command);

            //Assert
            Assert.False(validationResult.IsValid);
        }

        [Fact]
        public void Validate_NullID_ShouldThrowValidationException()
        {
            //Arrange
            var command = new BalanceByIDQuery
            {
                IDNumber = null
            };

            //Actual
            var validationResult = new BalanceByIDQueryValidator().Validate(command);

            //Assert
            Assert.False(validationResult.IsValid);
        }
    }
}

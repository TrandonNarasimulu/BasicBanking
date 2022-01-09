using BasicBanking.Application.Banking.Queires.GetTransferHistory;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace BasicBanking.Application.UnitTests.Banking.Queries.GetTransferHistory
{
    public class GetTransferHistoryQueryValidatorTests
    {
        [Fact]
        public void ValidationShouldBeSuccessful()
        {
            //Arrange
            var command = new TransferHistoryQuery
            {
                AccountNumber = "1234567891"
            };

            //Actual
            var validationResult = new TransferHistoryQueryValidator().Validate(command);

            //Assert
            Assert.True(validationResult.IsValid);
        }

        [Fact]
        public void Validate_EmptyAccountNumber_ShouldThrowValidationException()
        {
            //Arrange
            var command = new TransferHistoryQuery
            {
                AccountNumber = ""
            };

            //Actual
            var validationResult = new TransferHistoryQueryValidator().Validate(command);

            //Assert
            Assert.False(validationResult.IsValid);
        }

        [Fact]
        public void Validate_NullAccountNumber_ShouldThrowValidationException()
        {
            //Arrange
            var command = new TransferHistoryQuery
            {
                AccountNumber = null
            };

            //Actual
            var validationResult = new TransferHistoryQueryValidator().Validate(command);

            //Assert
            Assert.False(validationResult.IsValid);
        }
    }
}

using BasicBanking.Application.Banking.Commands.CreateAccount;
using BasicBanking.Application.Banking.Commands.TransferMoney;
using BasicBanking.Application.Banking.Queires.GetAccountBalance;
using BasicBanking.Application.Banking.Queires.GetBalanceByIDNumber;
using BasicBanking.Application.Banking.Queires.GetTransferHistory;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BasicBanking.API.IntegrationTests.Controllers.BankingController
{
    public class BankingControllerTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;

        public BankingControllerTests(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        // Take note that these tests might need to be run on its own. If it fails you can run it again on its own.
        [Fact]
        public async Task TestCreateAccount_ShouldSucceedAsync()
        {
            var client = _factory.CreateClient();

            var command = new CreateAccountCommand
            {
                FirstName = "John",
                LastName = "Doe",
                IDNumber = "1234567891234",
                InitialDeposit = 100
            };

            var json = JsonConvert.SerializeObject(command);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var httpResponse = await client.PostAsync($"/api/Banking/CreateNewAccount", content);
            httpResponse.EnsureSuccessStatusCode();

            var jsonResult = await httpResponse.Content.ReadAsStringAsync();
            var createdAccount = JsonConvert.DeserializeObject<CreateAccountViewModel>(jsonResult);

            Assert.NotNull(createdAccount.AccountNumber);
            Assert.True(createdAccount.AccountBalance > 0);
        }

        [Fact]
        public async Task TestTransferMoney_ShouldSucceedAsync()
        {
            var client = _factory.CreateClient();

            var command = new TransferMoneyCommand
            {
                SourceAccountNumber = "1235467891",
                DestinationAccountNumber = "9876543219",
                Amount = 10
            };

            var json = JsonConvert.SerializeObject(command);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var httpResponse = await client.PostAsync($"/api/Banking/TransferMoney", content);
            httpResponse.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task TestGetBalanceByAccountNumber_ShouldSucceedAsync()
        {
            var client = _factory.CreateClient();

            string accountNumber = "1235467891";
            var httpResponse = await client.GetAsync($"/api/Banking/BalanceByAccountNumber/{accountNumber}");
            httpResponse.EnsureSuccessStatusCode();

            var jsonResult = await httpResponse.Content.ReadAsStringAsync();
            var accountDetails = JsonConvert.DeserializeObject<AccountBalanceViewModel>(jsonResult);

            Assert.NotNull(accountDetails);
            Assert.True(accountDetails.AccountBalance > 0);
        }

        [Fact]
        public async Task TestGetBalanceByUserIDNumber_ShouldSucceedAsync()
        {
            var client = _factory.CreateClient();

            string idNumber = "1234567891234";
            var httpResponse = await client.GetAsync($"/api/Banking/BalanceByUserIDNumber/{idNumber}");
            httpResponse.EnsureSuccessStatusCode();

            var jsonResult = await httpResponse.Content.ReadAsStringAsync();
            var accountDetails = JsonConvert.DeserializeObject<BalanceByIDViewModel>(jsonResult);

            Assert.NotNull(accountDetails);
            Assert.True(accountDetails.AccountDetails.Count > 0);
        }

        [Fact]
        public async Task TestGetTransferHistory_ShouldSucceedAsync()
        {
            var client = _factory.CreateClient();

            string accountNumber = "1235467891";
            var httpResponse = await client.GetAsync($"/api/Banking/GetTransferHistory/{accountNumber}");
            httpResponse.EnsureSuccessStatusCode();

            var jsonResult = await httpResponse.Content.ReadAsStringAsync();
            var accountDetails = JsonConvert.DeserializeObject<TransferHistoryViewModel>(jsonResult);

            Assert.NotNull(accountDetails);
            Assert.True(accountDetails.TransferHistory.Count > 0);
        }
    }
}

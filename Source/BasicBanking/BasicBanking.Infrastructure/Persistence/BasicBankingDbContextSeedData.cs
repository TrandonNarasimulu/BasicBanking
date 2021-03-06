using BasicBanking.Domain.Entities;

namespace BasicBanking.Infrastructure.Persistence
{
    public static class BasicBankingDbContextSeedData
    {
        public static void SeedSampleDataAsync(BasicBankingDbContext context)
        {
            var user1 = new User { Id = 1, FirstName = "Arisha", LastName = "Barron", IDNumber = "1234567891234" };
            var user2 = new User { Id = 2, FirstName = "Branden", LastName = "Gibson", IDNumber = "5324856236541" };
            var user3 = new User { Id = 3, FirstName = "Rhonda", LastName = "Church", IDNumber = "1546865325412" };
            var user4 = new User { Id = 4, FirstName = "Georgina", LastName = "Hazel", IDNumber = "9865234587523" };

            var bankAccount1 = new BankAccount { Id = 1, UserId = user1.Id, User = user1, Balance = 100.00, AccountNumber = "1235467891" };
            var bankAccount2 = new BankAccount { Id = 2, UserId = user4.Id, User = user4, Balance = 100.00, AccountNumber = "9876543219" };

            var transaction = new Transaction { Id = 1, MainAccountNumber = "1235467891", OtherAccountNumber = "N/A", TransactionDetails = "Deposit", Amount = 100.00 };
            
            context.Users.Add(user1);
            context.Users.Add(user2);
            context.Users.Add(user3);
            context.Users.Add(user4);

            context.BankAccounts.Add(bankAccount1);
            context.BankAccounts.Add(bankAccount2);

            context.TransferHistory.Add(transaction);

            context.SaveChanges();
        }
    }
}

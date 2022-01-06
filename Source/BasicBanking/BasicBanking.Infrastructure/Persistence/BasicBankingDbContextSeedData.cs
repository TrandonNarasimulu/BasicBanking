using BasicBanking.Domain.Entities;

namespace BasicBanking.Infrastructure.Persistence
{
    public static class BasicBankingDbContextSeedData
    {
        public static void SeedSampleDataAsync(BasicBankingDbContext context)
        {
            var user1 = new User { Id = 1, FirstName = "Arisha", LastName = "Barron" };
            var user2 = new User { Id = 2, FirstName = "Brandon", LastName = "Gibson" };
            var user3 = new User { Id = 3, FirstName = "Rhonda", LastName = "Church" };
            var user4 = new User { Id = 4, FirstName = "Georgina", LastName = "Hazel" };

            var bankAccount1 = new BankAccount { Id = 1, UserId = user1.Id, User = user1, Balance = 10.00 };
            
            context.Users.Add(user1);
            context.Users.Add(user2);
            context.Users.Add(user3);
            context.Users.Add(user4);

            context.BankAccounts.Add(bankAccount1);

            context.SaveChanges();
        }
    }
}

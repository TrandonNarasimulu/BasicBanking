using BasicBanking.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace BasicBanking.Application.Common.Interfaces
{
    public interface IBasicBankingDbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<BankAccount> BankAccounts { get; set; }
        public DbSet<Transaction> TransferHistory { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}

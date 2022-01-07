using BasicBanking.Application.Common.Interfaces;
using BasicBanking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BasicBanking.Infrastructure.Services
{
    public class BankingService : IBanking
    {
        private readonly IBasicBankingDbContext _context;
        
        public BankingService(IBasicBankingDbContext context)
        {
            _context = context;
        }

        public async Task CreateAccount(string accountNumber, User user, double initialDeposit, CancellationToken cancellationToken)
        {
            var bankAccount = new BankAccount
            {
                AccountNumber = accountNumber,
                UserId = user.Id,
                User = user,
                Balance = initialDeposit
            };

            _context.BankAccounts.Add(bankAccount);

            await _context.SaveChangesAsync(cancellationToken);
        }

        public bool TransferMoney()
        {
            throw new NotImplementedException();
        }
    }
}

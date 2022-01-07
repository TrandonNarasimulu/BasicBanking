using BasicBanking.Application.Common.Interfaces;
using BasicBanking.Domain.Entities;
using Microsoft.EntityFrameworkCore;
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

        public async Task CreateUser(string firstName, string lastName, CancellationToken cancellationToken)
        {
            var user = new User
            {
                FirstName = firstName,
                LastName = lastName,
            };

            _context.Users.Add(user);

            await _context.SaveChangesAsync(cancellationToken);
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

        public async Task TransferMoney(string srcAccount, string destAccount, double amount, CancellationToken cancellationToken)
        {
            var srcBankAccount = await _context.BankAccounts.SingleOrDefaultAsync(x => x.AccountNumber == srcAccount);
            var destBankAccount = await _context.BankAccounts.SingleOrDefaultAsync(x => x.AccountNumber == destAccount);

            srcBankAccount.Balance -= amount;
            destBankAccount.Balance += amount;

            _context.BankAccounts.Update(srcBankAccount);
            _context.BankAccounts.Update(destBankAccount);

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}

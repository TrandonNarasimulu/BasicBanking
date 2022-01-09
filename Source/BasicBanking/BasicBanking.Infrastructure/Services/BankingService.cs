using BasicBanking.Application.Common.Interfaces;
using BasicBanking.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
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

        public async Task CreateUser(string firstName, string lastName, string idNumber, CancellationToken cancellationToken)
        {
            var user = new User
            {
                FirstName = firstName,
                LastName = lastName,
                IDNumber = idNumber
            };

            _context.Users.Add(user);

            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<bool> AccountNumberExists(string accountNumber)
        {
            var entity = await _context.BankAccounts.SingleOrDefaultAsync(x => x.AccountNumber == accountNumber);

            return entity != null;
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

        public async Task<User> GetUserDetails(string idNumber)
        {
            var userEntity = await _context.Users.SingleOrDefaultAsync(x => x.IDNumber == idNumber);

            return userEntity;
        }

        public async Task<User> GetUserDetails(long userId)
        {
            var userEntity = await _context.Users.SingleOrDefaultAsync(x => x.Id == userId);

            return userEntity;
        }

        public async Task<BankAccount> GetBankAccountDetails(string accountNumber)
        {
            var bankAccount = await _context.BankAccounts.SingleOrDefaultAsync(x => x.AccountNumber == accountNumber);

            return bankAccount;
        }

        public List<BankAccount> GetAllUserBankAccounts(string idNumber)
        {
            return _context.BankAccounts.Where(x => x.User.IDNumber == idNumber).ToList();
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

        public async Task UpdateTransferHistory(string mainAccountNumber, string otherAccountNumber, string transactionDetails, double amount, CancellationToken cancellationToken)
        {
            var transaction = new Transaction
            {
                MainAccountNumber = mainAccountNumber,
                OtherAccountNumber = otherAccountNumber,
                TransactionDetails = transactionDetails,
                Amount = amount
            };

            _context.TransferHistory.Add(transaction);

            await _context.SaveChangesAsync(cancellationToken);
        }

        public List<Transaction> GetTransferHistory(string accountNumber)
        {
            return _context.TransferHistory.Where(x => x.MainAccountNumber == accountNumber).ToList();
        }
    }
}

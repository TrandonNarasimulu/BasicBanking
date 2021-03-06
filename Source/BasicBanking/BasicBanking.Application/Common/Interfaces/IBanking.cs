using BasicBanking.Domain.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BasicBanking.Application.Common.Interfaces
{
    public interface IBanking
    {
        Task CreateUser(string firstName, string lastName, string idNumber, CancellationToken cancellationToken);
        Task<bool> AccountNumberExists(string accountNumber);
        Task CreateAccount(string accountNumber, User user, double initialDeposit, CancellationToken cancellationToken);
        Task<User> GetUserDetails(string idNumber);
        Task<User> GetUserDetails(long userId);
        Task<BankAccount> GetBankAccountDetails(string accountNumber);
        List<BankAccount> GetAllUserBankAccounts(string idNumber);
        Task TransferMoney(string srcAccount, string destAccount, double amount, CancellationToken cancellationToken);
        Task UpdateTransferHistory(string mainAccountNumber, string otherAccountNumber, string transactionDetails, double amount, CancellationToken cancellationToken);
        List<Transaction> GetTransferHistory(string accountNumber);
    }
}

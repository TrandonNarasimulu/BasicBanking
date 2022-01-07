using BasicBanking.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace BasicBanking.Application.Common.Interfaces
{
    public interface IBanking
    {
        Task CreateUser(string firstName, string lastName, CancellationToken cancellationToken);
        Task CreateAccount(string accountNumber, User user, double initialDeposit, CancellationToken cancellationToken);
        Task TransferMoney(string srcAccount, string destAccount, double amount, CancellationToken cancellationToken);
    }
}

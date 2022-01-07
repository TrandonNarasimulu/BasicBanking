
using BasicBanking.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace BasicBanking.Application.Common.Interfaces
{
    public interface IBanking
    {
        Task CreateAccount(string accountNumber, User user, double initialDeposit, CancellationToken cancellationToken);
        bool TransferMoney();
    }
}

using BasicBanking.Application.Common.Exceptions;
using BasicBanking.Application.Common.Interfaces;
using BasicBanking.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BasicBanking.Application.Banking.Queires.GetAccountBalance
{
    public class AccountBalanceQuery : IRequest<AccountBalanceViewModel>
    {
        public string AccountNumber { get; set; }
    }

    public class AccountBalanceQueryHandler : IRequestHandler<AccountBalanceQuery, AccountBalanceViewModel>
    {
        private readonly IBanking _bankingService;

        public AccountBalanceQueryHandler(IBanking bankingService)
        {
            _bankingService = bankingService;
        }

        public async Task<AccountBalanceViewModel> Handle(AccountBalanceQuery request, CancellationToken cancellationToken)
        {
            var bankAccount = await _bankingService.GetBankAccountDetails(request.AccountNumber);
            if (bankAccount == null)
            {
                throw new NotFoundException(nameof(BankAccount), request.AccountNumber);
            }

            var userDetails = await _bankingService.GetUserDetails(bankAccount.UserId);
            if (userDetails == null)
            {
                throw new NotFoundException(nameof(User), $"UserID = '{bankAccount.UserId}'");
            }

            return new AccountBalanceViewModel
            {
                AccountBalance = bankAccount.Balance,
                AccountNumber = bankAccount.AccountNumber,
                AccountHolderFirstName = userDetails.FirstName,
                AccountHolderLastName = userDetails.LastName,
                AccountHolderIDNumber = userDetails.IDNumber
            };
        }
    }
}

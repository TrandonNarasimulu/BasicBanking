using BasicBanking.Application.Common.Exceptions;
using BasicBanking.Application.Common.Interfaces;
using BasicBanking.Domain.Entities;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BasicBanking.Application.Banking.Queires.GetBalanceByIDNumber
{
    public class BalanceByIDQuery : IRequest<BalanceByIDViewModel>
    {
        public string IDNumber { get; set; }
    }

    public class BalanceByIDQueryHandler : IRequestHandler<BalanceByIDQuery, BalanceByIDViewModel>
    {
        private readonly IBanking _bankingService;

        public BalanceByIDQueryHandler(IBanking bankingService)
        {
            _bankingService = bankingService;
        }

        public async Task<BalanceByIDViewModel> Handle(BalanceByIDQuery request, CancellationToken cancellationToken)
        {
            List<AccountDetailsModel> allAccounts = new List<AccountDetailsModel>();

            var userDetails = await _bankingService.GetUserDetails(request.IDNumber);
            if(userDetails == null)
            {
                throw new NotFoundException(nameof(User), request.IDNumber);
            }

            var bankAccounts = _bankingService.GetAllUserBankAccounts(request.IDNumber);
            if (bankAccounts == null)
            {
                return new BalanceByIDViewModel { AccountDetails = allAccounts };
            }

            foreach(var bankAccount in bankAccounts)
            {
                var accountDetailsModel = new AccountDetailsModel
                {
                    AccountBalance = bankAccount.Balance,
                    AccountNumber = bankAccount.AccountNumber,
                    AccountHolderFirstName = userDetails.FirstName,
                    AccountHolderLastName = userDetails.LastName,
                    AccountHolderIDNumber = userDetails.IDNumber
                };

                allAccounts.Add(accountDetailsModel);
            }

            return new BalanceByIDViewModel { AccountDetails = allAccounts };
        }
    }
}

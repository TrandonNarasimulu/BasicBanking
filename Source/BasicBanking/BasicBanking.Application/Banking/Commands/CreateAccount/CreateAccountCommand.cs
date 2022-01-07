using BasicBanking.Application.Common.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BasicBanking.Application.Banking.Commands.CreateAccount
{
    public class CreateAccountCommand : IRequest<CreateAccountViewModel>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string IDNumber { get; set; }
        public double InitialDeposit { get; set; }
    }

    public class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand, CreateAccountViewModel>
    {
        private readonly IBanking _bankingService;
        private readonly IMath _mathService;

        public CreateAccountCommandHandler(IBanking bankingService, IMath mathService)
        {
            _bankingService = bankingService;
            _mathService = mathService;
        }

        public async Task<CreateAccountViewModel> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
        {
            var accountNumber = await GenerateAccountNumber();

            var userEntity = await _bankingService.GetUserDetails(request.IDNumber);
            if(userEntity == null)
            {
                await _bankingService.CreateUser(request.FirstName, request.LastName, request.IDNumber, cancellationToken);
                userEntity = await _bankingService.GetUserDetails(request.IDNumber);
            }

            await _bankingService.CreateAccount(accountNumber, userEntity, request.InitialDeposit, cancellationToken);

            return new CreateAccountViewModel
            {
                AccountNumber = accountNumber,
                AccountBalance = request.InitialDeposit
            };
        }

        private async Task<string> GenerateAccountNumber()
        {
            string accountNumber = "";

            while (string.IsNullOrEmpty(accountNumber) || await _bankingService.AccountNumberExists(accountNumber))
            {
                accountNumber = "";
                for (int i = 0; i < 10; i++)
                {
                    accountNumber += _mathService.GetRandomNumber(9).ToString();
                }
            }
            
            return accountNumber;
        }
    }
}

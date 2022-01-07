using BasicBanking.Application.Common.Exceptions;
using BasicBanking.Application.Common.Interfaces;
using BasicBanking.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BasicBanking.Application.Banking.Commands.TransferMoney
{
    public class TransferMoneyCommand : IRequest<Unit>
    {
        public string SourceAccountNumber { get; set; }
        public string DestinationAccountNumber { get; set; }
        public double Amount { get; set; }
    }

    public class TransferMoneyCommandHandler : IRequestHandler<TransferMoneyCommand, Unit>
    {
        private readonly IBanking _bankingService;

        public TransferMoneyCommandHandler(IBanking bankingService)
        {
            _bankingService = bankingService;
        }

        public async Task<Unit> Handle(TransferMoneyCommand request, CancellationToken cancellationToken)
        {
            var srcAccount = await _bankingService.GetBankAccountDetails(request.SourceAccountNumber);
            if(srcAccount == null)
            {
                throw new NotFoundException(nameof(BankAccount), request.SourceAccountNumber);
            }

            if(srcAccount.Balance < request.Amount)
            {
                throw new InsufficientFundsException($"Insufficient funds to trasnfer in account '{request.SourceAccountNumber}'");
            }

            var destAccount = await _bankingService.GetBankAccountDetails(request.DestinationAccountNumber);
            if (destAccount == null)
            {
                throw new NotFoundException(nameof(BankAccount), request.DestinationAccountNumber);
            }

             await _bankingService.TransferMoney(srcAccount.AccountNumber, destAccount.AccountNumber, request.Amount, cancellationToken);

            return Unit.Value;
        }
    }
}

using BasicBanking.Application.Common.Exceptions;
using BasicBanking.Application.Common.Interfaces;
using BasicBanking.Domain.Entities;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BasicBanking.Application.Banking.Queires.GetTransferHistory
{
    public class TransferHistoryQuery : IRequest<TransferHistoryViewModel>
    {
        public string AccountNumber { get; set; }
    }

    public class TransferHistoryQueryHandler : IRequestHandler<TransferHistoryQuery, TransferHistoryViewModel>
    {
        private readonly IBanking _bankingService;

        public TransferHistoryQueryHandler(IBanking bankingService)
        {
            _bankingService = bankingService;
        }

        public async Task<TransferHistoryViewModel> Handle(TransferHistoryQuery request, CancellationToken cancellationToken)
        {
            List<TransferHistoryItem> transferHistory = new List<TransferHistoryItem>();
            var bankAccount = await _bankingService.GetBankAccountDetails(request.AccountNumber);
            if(bankAccount == null)
            {
                throw new NotFoundException(nameof(BankAccount), request.AccountNumber);
            }

            var transactions = _bankingService.GetTransferHistory(request.AccountNumber);
            if(transactions == null)
            {
                return new TransferHistoryViewModel { TransferHistory = transferHistory };
            }

            foreach(var transaction in transactions)
            {
                var transferHistoryItem = new TransferHistoryItem
                {
                    Amount = transaction.Amount,
                    TransactionDetails = transaction.TransactionDetails,
                    MainAccountNumber = transaction.MainAccountNumber,
                    OtherAccountNumber = transaction.OtherAccountNumber
                };

                transferHistory.Add(transferHistoryItem);
            }

            return new TransferHistoryViewModel { TransferHistory = transferHistory };
        }
    }
}

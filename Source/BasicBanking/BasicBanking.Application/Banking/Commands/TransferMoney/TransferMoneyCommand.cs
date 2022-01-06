using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BasicBanking.Application.Banking.Commands.TransferMoney
{
    public class TransferMoneyCommand : IRequest
    {

    }

    public class TransferMoneyCommandHandler : IRequestHandler<TransferMoneyCommand, Unit>
    {
        public TransferMoneyCommandHandler()
        {

        }

        public async Task<Unit> Handle(TransferMoneyCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}

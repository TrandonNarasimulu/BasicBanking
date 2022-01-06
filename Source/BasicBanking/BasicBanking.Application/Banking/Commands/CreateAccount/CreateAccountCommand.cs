using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BasicBanking.Application.Banking.Commands.CreateAccount
{
    public class CreateAccountCommand : IRequest
    {
    
    }

    public class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand, Unit>
    {
        public CreateAccountCommandHandler()
        {

        }

        public async Task<Unit> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}

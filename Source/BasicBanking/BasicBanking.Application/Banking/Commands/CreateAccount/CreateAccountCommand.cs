using BasicBanking.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BasicBanking.Application.Banking.Commands.CreateAccount
{
    public class CreateAccountCommand : IRequest
    {
        public string AccountHolderName { get; set; }
        public double InitialDeposit { get; set; }
    }

    public class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand, Unit>
    {
        private readonly IBanking _bankingService;
        private readonly IBasicBankingDbContext _context;
        private ILogger<CreateAccountCommandHandler> _logger;

        public CreateAccountCommandHandler(IBanking bankingService, IBasicBankingDbContext context, ILogger<CreateAccountCommandHandler> logger)
        {
            _bankingService = bankingService;
            _context = context;
            _logger = logger;
        }

        public async Task<Unit> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
        {
            //_bankingService.CreateAccount();

            var entity = await _context.Users.SingleOrDefaultAsync(x => x.Id == 1);
            _logger.LogInformation($"Entity Name is: {entity.FirstName}");

            entity.FirstName = "Trandon";

            _context.Users.Update(entity);

            await _context.SaveChangesAsync(cancellationToken);

            var newEntity = await _context.Users.SingleOrDefaultAsync(x => x.Id == 1);
            _logger.LogInformation($"Entity Name is: {newEntity.FirstName}");

            return Unit.Value;
        }
    }
}

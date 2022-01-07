using BasicBanking.Application.Common.Exceptions;
using BasicBanking.Application.Common.Interfaces;
using BasicBanking.Domain.Entities;
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
        public string AccountHolderSurname { get; set; }
        public double InitialDeposit { get; set; }
    }

    public class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand, Unit>
    {
        private readonly IBanking _bankingService;
        private readonly IBasicBankingDbContext _context;
        private readonly IMath _mathService;
        private ILogger<CreateAccountCommandHandler> _logger;

        public CreateAccountCommandHandler(IBanking bankingService, IBasicBankingDbContext context, IMath mathService, ILogger<CreateAccountCommandHandler> logger)
        {
            _bankingService = bankingService;
            _context = context;
            _mathService = mathService;
            _logger = logger;
        }

        public async Task<Unit> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
        {
            var accountNumber = await GenerateAccountNumber();

            var userEntity = await _context.Users.SingleOrDefaultAsync(x => x.FirstName == request.AccountHolderName && 
                                                                            x.LastName == request.AccountHolderSurname);
            if(userEntity == null)
            {
                throw new NotFoundException(nameof(User), $"{request.AccountHolderName} {request.AccountHolderSurname}");
            }

            await _bankingService.CreateAccount(accountNumber, userEntity, request.InitialDeposit, cancellationToken);

            return Unit.Value;
        }

        private async Task<string> GenerateAccountNumber()
        {
            string accountNumber = "";

            while (string.IsNullOrEmpty(accountNumber) || await AccountNumberExists(accountNumber))
            {
                accountNumber = "";
                for (int i = 0; i < 10; i++)
                {
                    accountNumber += _mathService.GetRandomNumber(9).ToString();
                }
            }
            

            return accountNumber;   
        }

        private async Task<bool> AccountNumberExists(string accountNumber)
        {
            var entity = await _context.BankAccounts.SingleOrDefaultAsync(x => x.AccountNumber == accountNumber);

            return entity != null;
        }
    }
}

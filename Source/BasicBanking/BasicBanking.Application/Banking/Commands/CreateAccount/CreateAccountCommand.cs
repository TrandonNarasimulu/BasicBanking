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
    public class CreateAccountCommand : IRequest<CreateAccountViewModel>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public double InitialDeposit { get; set; }
    }

    public class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand, CreateAccountViewModel>
    {
        private readonly IBanking _bankingService;
        private readonly IBasicBankingDbContext _context;
        private readonly IMath _mathService;

        public CreateAccountCommandHandler(IBanking bankingService, IBasicBankingDbContext context, IMath mathService)
        {
            _bankingService = bankingService;
            _context = context;
            _mathService = mathService;
        }

        public async Task<CreateAccountViewModel> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
        {
            var accountNumber = await GenerateAccountNumber();

            var userEntity = await _context.Users.SingleOrDefaultAsync(x => x.FirstName == request.FirstName && 
                                                                            x.LastName == request.LastName);
            if(userEntity == null)
            {
                await _bankingService.CreateUser(request.FirstName, request.LastName, cancellationToken);
                userEntity = await _context.Users.SingleOrDefaultAsync(x => x.FirstName == request.FirstName &&
                                                                            x.LastName == request.LastName);
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

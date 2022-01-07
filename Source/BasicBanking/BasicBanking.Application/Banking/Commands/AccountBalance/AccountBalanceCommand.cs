using BasicBanking.Application.Common.Exceptions;
using BasicBanking.Application.Common.Interfaces;
using BasicBanking.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BasicBanking.Application.Banking.Commands.AccountBalance
{
    public class AccountBalanceCommand : IRequest<AccountBalanceViewModel>
    {
        public string AccountNumber { get; set; }
    }

    public class AccountBalanceCommandHandler : IRequestHandler<AccountBalanceCommand, AccountBalanceViewModel>
    {
        private readonly IBasicBankingDbContext _context;

        public AccountBalanceCommandHandler(IBasicBankingDbContext context)
        {
            _context = context;
        }

        public async Task<AccountBalanceViewModel> Handle(AccountBalanceCommand request, CancellationToken cancellationToken)
        {
            var bankAccount = await _context.BankAccounts.SingleOrDefaultAsync(x => x.AccountNumber == request.AccountNumber);
            if(bankAccount == null)
            {
                throw new NotFoundException(nameof(BankAccount), request.AccountNumber);
            }

            var userEntity = await _context.Users.SingleOrDefaultAsync(x => x.Id  == bankAccount.UserId);
            if (userEntity == null)
            {
                throw new NotFoundException(nameof(User), $"UserID = '{bankAccount.UserId}'");
            }

            return new AccountBalanceViewModel
            {
                AccountBalance = bankAccount.Balance,
                AccountNumber = bankAccount.AccountNumber,
                AccountHolderFirstName = userEntity.FirstName,
                AccountHolderLastName = userEntity.LastName
            };
        }
    }
}

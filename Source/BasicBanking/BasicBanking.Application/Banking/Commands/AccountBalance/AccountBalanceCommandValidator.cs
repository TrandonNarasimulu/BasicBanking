using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace BasicBanking.Application.Banking.Commands.AccountBalance
{
    public class AccountBalanceCommandValidator : AbstractValidator<AccountBalanceCommand>
    {
        public AccountBalanceCommandValidator()
        {
            RuleFor(x => x.AccountNumber).NotEmpty();
        }
    }
}

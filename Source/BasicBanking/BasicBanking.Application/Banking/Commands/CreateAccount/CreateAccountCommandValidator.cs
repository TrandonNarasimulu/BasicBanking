﻿using FluentValidation;

namespace BasicBanking.Application.Banking.Commands.CreateAccount
{
    public class CreateAccountCommandValidator : AbstractValidator<CreateAccountCommand>
    {
        public CreateAccountCommandValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty();
            RuleFor(x => x.LastName).NotEmpty();
            RuleFor(x => x.InitialDeposit).NotEmpty().Must(x => x > 0);
        }
    }
}

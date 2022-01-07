using FluentValidation;

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

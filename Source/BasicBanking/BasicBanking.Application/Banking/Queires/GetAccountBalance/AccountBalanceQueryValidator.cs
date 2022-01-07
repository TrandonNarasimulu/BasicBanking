using FluentValidation;

namespace BasicBanking.Application.Banking.Queires.GetAccountBalance
{
    public class AccountBalanceQueryValidator : AbstractValidator<AccountBalanceQuery>
    {
        public AccountBalanceQueryValidator()
        {
            RuleFor(x => x.AccountNumber).NotEmpty();
        }
    }
}

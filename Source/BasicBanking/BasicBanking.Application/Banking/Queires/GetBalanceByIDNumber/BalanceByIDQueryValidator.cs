using FluentValidation;

namespace BasicBanking.Application.Banking.Queires.GetBalanceByIDNumber
{
    public class BalanceByIDQueryValidator : AbstractValidator<BalanceByIDQuery>
    {
        public BalanceByIDQueryValidator()
        {
            RuleFor(x => x.IDNumber).NotEmpty().Must(x => x.Length == 13);
        }
    }
}

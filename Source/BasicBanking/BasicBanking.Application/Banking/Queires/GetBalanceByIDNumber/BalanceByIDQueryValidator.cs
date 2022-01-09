using FluentValidation;

namespace BasicBanking.Application.Banking.Queires.GetBalanceByIDNumber
{
    public class BalanceByIDQueryValidator : AbstractValidator<BalanceByIDQuery>
    {
        public BalanceByIDQueryValidator()
        {
            RuleFor(x => x.IDNumber).NotEmpty();
            When(x => x.IDNumber != null, () => {
                RuleFor(x => x.IDNumber).Must(x => x.Length == 13);
            });
        }
    }
}

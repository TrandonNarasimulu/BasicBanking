using FluentValidation;

namespace BasicBanking.Application.Banking.Commands.BalanceByIDNumber
{
    public class BalanceByIDCommandValidator : AbstractValidator<BalanceByIDCommand>
    {
        public BalanceByIDCommandValidator()
        {
            RuleFor(x => x.IDNumber).NotEmpty().Must(x => x.Length == 13);
        }
    }
}

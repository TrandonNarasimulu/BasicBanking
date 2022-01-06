using FluentValidation;

namespace BasicBanking.Application.Banking.Commands.TransferMoney
{
    public class TransferMoneyCommandValidator : AbstractValidator<TransferMoneyCommand>
    {
        public TransferMoneyCommandValidator()
        {
            // RuleFor(x => x.).NotEmpty();
        }
    }
}

using FluentValidation;

namespace BasicBanking.Application.Banking.Commands.TransferMoney
{
    public class TransferMoneyCommandValidator : AbstractValidator<TransferMoneyCommand>
    {
        public TransferMoneyCommandValidator()
        {
            RuleFor(x => x.SourceAccountNumber).NotEmpty();
            RuleFor(x => x.DestinationAccountNumber).NotEmpty();
            RuleFor(x => x.Amount).NotEmpty().Must(x => x > 0);
        }
    }
}

using FluentValidation;

namespace BasicBanking.Application.Banking.Queires.GetTransferHistory
{
    public class TransferHistoryQueryValidator : AbstractValidator<TransferHistoryQuery>
    {
        public TransferHistoryQueryValidator()
        {
            RuleFor(x => x.AccountNumber).NotEmpty();
        }
    }
}

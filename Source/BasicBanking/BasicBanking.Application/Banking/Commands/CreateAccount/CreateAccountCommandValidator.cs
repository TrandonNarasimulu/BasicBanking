using FluentValidation;

namespace BasicBanking.Application.Banking.Commands.CreateAccount
{
    public class CreateAccountCommandValidator : AbstractValidator<CreateAccountCommand>
    {
        public CreateAccountCommandValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty();
            RuleFor(x => x.LastName).NotEmpty();
            RuleFor(x => x.IDNumber).NotEmpty();
            When(x => x.IDNumber != null, () => {
                RuleFor(x => x.IDNumber).Must(x => x.Length == 13);
            });
            RuleFor(x => x.InitialDeposit).NotEmpty().Must(x => x > 0);
        }
    }
}

using FluentValidation;

namespace BasicBanking.Application.Banking.Commands.CreateAccount
{
    public class CreateAccountCommandValidator : AbstractValidator<CreateAccountCommand>
    {
        public CreateAccountCommandValidator()
        {
            // RuleFor(x => x.).NotEmpty();
        }
    }
}

using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace BasicBanking.Application.Dummy.Commands.GetText
{
    public class DummyCommandValidator : AbstractValidator<DummyCommand>
    {
        public DummyCommandValidator()
        {
            RuleFor(x => x.InputText).NotNull().NotEmpty();
        }
    }
}

using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace BasicBanking.Application.Dummy.Commands.GetText
{
    public class DummyCommand : IRequest<DummyCommandResult>
    {
        public string InputText { get; set; }
    }
}

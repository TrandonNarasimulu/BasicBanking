using BasicBanking.Application.Common.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace BasicBanking.Application.Dummy.Commands.GetText
{
    public class DummyCommandHandler : IRequestHandler<DummyCommand, DummyCommandResult>
    {
        private readonly ILogger<DummyCommandHandler> _logger;
        private readonly IDummyService _dummyService;

        public DummyCommandHandler(ILogger<DummyCommandHandler> logger, IDummyService dummyService)
        {
            _logger = logger;
            _dummyService = dummyService;
        }

        public async Task<DummyCommandResult> Handle(DummyCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("In the Dummy Command Handler");

            string text = _dummyService.GetText();

            string response = $"Text received from user: {request.InputText}\nSending response: {text}";

            _logger.LogInformation("Exiting the DummyCommandHandler");

            return new DummyCommandResult { Response = response };
        }
    }
}

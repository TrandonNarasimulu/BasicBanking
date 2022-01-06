using BasicBanking.Application.Common.Exceptions;
using BasicBanking.Application.Common.Interfaces;
using BasicBanking.Application.Dummy.Commands.GetText;
using Microsoft.Extensions.Logging;
using Moq;
using Shouldly;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace BasicBanking.Application.UnitTests.Dummy.Commands.GetText
{
    public class DummyCommandTests
    {
        [Fact]
        public async Task Handler_ShouldCaptureBiometrics()
        {
            var dummyService = new Mock<IDummyService>();
            var logger = new Mock<ILogger<DummyCommandHandler>>();

            var command = new DummyCommand
            {
                InputText = "sample text"
            };

            var handler = new DummyCommandHandler(logger.Object, dummyService.Object);

            var result = await handler.Handle(command, CancellationToken.None);

            dummyService.Verify(s => s.GetText());
        }

        [Fact]
        public void Handler_ShouldThrowValidationExceptionForCapture()
        {
            var dummyService = new Mock<IDummyService>();
            var logger = new Mock<ILogger<DummyCommandHandler>>();


            var command = new DummyCommand
            {
                InputText = null
            };

            var handler = new DummyCommandHandler(logger.Object, dummyService.Object);

            Should.ThrowAsync<ValidationException>(() => handler.Handle(command, CancellationToken.None));
        }
    }
}

using BasicBanking.Application.Common.Interfaces;

namespace BasicBanking.Infrastructure.Services
{
    public class DummyService : IDummyService
    {
        public string GetText()
        {
            return "This is some text from your infrastructure layer";
        }
    }
}

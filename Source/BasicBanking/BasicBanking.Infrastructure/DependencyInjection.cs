using BasicBanking.Application.Common.Interfaces;
using BasicBanking.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BasicBanking.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddTransient<IDummyService, DummyService>();

            return services;
        }
    }
}

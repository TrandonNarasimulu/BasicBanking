using BasicBanking.Application.Common.Interfaces;
using BasicBanking.Infrastructure.Persistence;
using BasicBanking.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BasicBanking.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IBanking, BankingService>();
            services.AddTransient<IDateTime, DateTimeService>();

            string dbName = configuration.GetValue<string>("Infrastructure:DatabaseName");
            services.AddDbContext<BasicBankingDbContext>(options => options.UseInMemoryDatabase(dbName));

            services.AddSingleton<IBasicBankingDbContext>(provider => provider.GetService<BasicBankingDbContext>());

            return services;
        }
    }
}

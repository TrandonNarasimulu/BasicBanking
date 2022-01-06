using BasicBanking.Application.Common.Behaviours;
using BasicBanking.Application.Dummy.Commands.GetText;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Security;
using System.Reflection;

namespace BasicBanking.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPerformanceBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));

            services.AddValidatorsFromAssembly(typeof(DummyCommandValidator).Assembly);

            return services;
        }
    }
}

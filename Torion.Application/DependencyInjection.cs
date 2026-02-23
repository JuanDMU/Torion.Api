
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Torion.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {

            var currentAssembly = typeof(DependencyInjection).Assembly;

            services.AddMediatR(config => config.RegisterServicesFromAssembly(currentAssembly));

            services.AddValidatorsFromAssembly(currentAssembly);

            // Register application services here
            // Example: services.AddTransient<IMyService, MyService>();
            return services;
        }
    }
}

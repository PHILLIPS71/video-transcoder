using FluentValidation.AspNetCore;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Giantnodes.Worker.Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddFluentValidation(config =>
            {
                config.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            });

            services.AddMassTransitServices(configuration);

            return services;
        }

        private static IServiceCollection AddMassTransitServices(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddMassTransit(options =>
                {
                    options
                        .SetKebabCaseEndpointNameFormatter();

                    options.UsingRabbitMq((context, config) =>
                    {
                        config.ConfigureEndpoints(context);
                    });

                });

            return services;
        }
    }
}

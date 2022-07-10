using FluentValidation.AspNetCore;
using Giantnodes.Application.Validation;
using Giantnodes.Dashboard.Abstractions.Features.FileExplorer.Queries.GetDirectoryContents;
using Giantnodes.Dashboard.Abstractions.Features.Statistics.Queries.GetDirectoryContainerStatistics;
using Giantnodes.Dashboard.Application.Consumers.FileExplorer.Queries;
using Giantnodes.Dashboard.Application.Consumers.Statistics.Queries;
using Giantnodes.Infrastructure.Storage;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Giantnodes.Dashboard.Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddFluentValidation(config =>
            {
                config.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
                config.RegisterValidatorsFromAssemblyContaining(typeof(GetDirectoryContentsValidator));
                config.RegisterValidatorsFromAssemblyContaining(typeof(GetDirectoryContainerStatisticsValidator));
            });

            services.AddStorageServices(configuration);
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

                    options
                        .AddConsumer<GetDirectoryContentsConsumer>();

                    options
                        .AddConsumer<GetDirectoryContainerStatisticsConsumer>();

                    options.UsingRabbitMq((context, config) =>
                    {
                        config.ConfigureEndpoints(context);
                        config.UseConsumeFilter(typeof(FluentValidationFilter<>), context);
                    });

                });

            return services;
        }
    }
}

﻿using FluentValidation.AspNetCore;
using Giantnodes.Application.Validation;
using Giantnodes.Dashboard.Persistence;
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
            });

            services.AddMassTransitServices();

            return services;
        }

        private static IServiceCollection AddMassTransitServices(this IServiceCollection services)
        {
            services
                .AddMassTransit(options =>
                {
                    options
                        .SetKebabCaseEndpointNameFormatter();

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

using FluentValidation.AspNetCore;
using Giantnodes.Dashboard.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Giantnodes.Dashboard.Application.Tests
{
    public static class ApplicationTestsServiceRegistration
    {
        public static IServiceCollection AddApplicationTestServices(this IServiceCollection services)
        {
            services.AddFluentValidation(config => config.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly()));

            return services;
        }
    }
}

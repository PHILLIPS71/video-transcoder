using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Giantnodes.Dashboard.Application.Tests
{
    public static class ApplicationTestsServiceRegistration
    {
        public static IServiceCollection AddApplicationTestServices(this IServiceCollection services)
        {
            services.AddFluentValidation(config => config.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly()));
            services.AddDistributedMemoryCache();

            return services;
        }
    }
}

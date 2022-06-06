using Giantnodes.Dashboard.Abstractions.Common;
using Giantnodes.Dashboard.Api.Configuration;
using HotChocolate.Types.Descriptors;
using System.Reflection;

namespace Giantnodes.Dashboard.Api
{
    public static class ApiServiceRegistration
    {
        public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services
                .AddGraphQLServer()
                .AddApiTypes()
                .AddQueryType()
                .AddFiltering()
                .AddSorting()
                //.AddConvention<INamingConventions, SnakeCaseNamingConventions>()
                .AddMutationConventions();

            return services;
        }
    }
}

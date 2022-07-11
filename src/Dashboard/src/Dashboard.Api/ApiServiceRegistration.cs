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
            services.AddCors().ConfigureOptions<ConfigureCorsOptions>();

            services
                .AddGraphQLServer()
                .AddApiTypes()
                .AddQueryType()
                .AddFiltering()
                .AddSorting()
                .AddConvention<INamingConventions, SnakeCaseNamingConvention>()
                .AddMutationConventions();

            return services;
        }
    }
}

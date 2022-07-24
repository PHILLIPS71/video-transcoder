using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Giantnodes.Infrastructure.Masstransit
{
    public static class MasstransitRegistration
    {
        public static IServiceCollection AddMasstransitServices(this IServiceCollection services, IConfiguration configuration)
        {
            return services;
        }
    }
}

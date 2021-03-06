using Giantnodes.Infrastructure.Storage.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.IO.Abstractions;

namespace Giantnodes.Infrastructure.Storage
{
    public static class StorageRegistration
    {
        public static IServiceCollection AddStorageServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<StorageSettings>(configuration.GetSection("StorageSettings"));
            services.AddSingleton<IValidateOptions<StorageSettings>, StorageSettingsValidator>();

            services.AddSingleton<IFileSystem, FileSystem>();

            return services;
        }
    }
}

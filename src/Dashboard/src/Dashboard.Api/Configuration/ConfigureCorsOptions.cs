using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.Extensions.Options;

namespace Giantnodes.Dashboard.Api.Configuration
{
    public class ConfigureCorsOptions : IConfigureNamedOptions<CorsOptions>
    {
        private readonly IConfiguration configuration;

        public ConfigureCorsOptions(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public void Configure(string name, CorsOptions options)
        {
            options.AddDefaultPolicy(builder =>
            {
                builder
                    .WithOrigins()
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
            });
        }

        public void Configure(CorsOptions options)
        {
            Configure(options);
        }
    }
}

using Giantnodes.Dashboard.Application;
using Giantnodes.Dashboard.Persistence;
using HotChocolate.AspNetCore;

namespace Giantnodes.Dashboard.Api
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        private readonly IHostEnvironment _environment;

        public Startup(IConfiguration configuration, IHostEnvironment env)
        {
            _configuration = configuration;
            _environment = env;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddPersistenceServices(_configuration);
            services.AddApplicationServices(_configuration);
            services.AddApiServices(_configuration);
        }

        public void Configure(IApplicationBuilder app)
        {
            if (!_environment.IsDevelopment())
            {
                app.UseHttpsRedirection();
            }

            app
                .UseRouting()
                .UseEndpoints(endpoint =>
                    endpoint
                        .MapGraphQL()
                        .WithOptions(new GraphQLServerOptions
                        {
                            Tool = { Enable = _environment.IsDevelopment() }
                        })
                );
        }
    }
}
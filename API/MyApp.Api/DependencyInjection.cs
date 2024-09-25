using MyApp.Application;
using MyApp.Infrastructure;

namespace MyApp.Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddAppDependencyInjection(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddApplicationDependencyInjection()
                .AddInfrastructureDependencyInjection(configuration);

            return services;
        }
    }
}

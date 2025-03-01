using DependencyInjection.Ex;
using Microsoft.Extensions.DependencyInjection;

namespace DependencyInjection
{
    public static class Infrastructure
    {
        public static void AddInfrastructure(this IServiceCollection services)
        {
            services.ConfigureMassTransit();
        }
    }
}

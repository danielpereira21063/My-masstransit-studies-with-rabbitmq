using MassTransit;

namespace OrderService.Extensions
{
    public static class MassTransitEx
    {
        public static void ConfigureMassTransit(this IServiceCollection services)
        {
            services.AddMassTransit((x) =>
            {
                x.UsingRabbitMq((context, config) =>
                {
                    config.Host("localhost", "/", h =>
                    {
                        h.Username("admin");
                        h.Password("admin");
                    });
                });
            });
        }
    }
}

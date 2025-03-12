using MassTransit;
using TrakingService.Consumers;

namespace TrakingService.Extensions
{
    public static class MassTransitEx
    {
        public static void ConfigureMassTransit(this IServiceCollection services)
        {
            services.AddMassTransit((x) =>
            {
                x.AddConsumer<OrderPlacedConsumer>();

                x.UsingRabbitMq((context, config) =>
                {
                    config.Host("localhost", "/", h =>
                    {
                        h.Username("admin");
                        h.Password("admin");
                    });

                    config.ReceiveEndpoint("tracking-order-placed", e =>
                    {
                        e.ConfigureConsumer<OrderPlacedConsumer>(context);

                        e.Bind("order-placed-exchange", x =>
                        {
                            x.RoutingKey = "order.tracking";
                            x.ExchangeType = "direct";
                        });

                    });
                });
            });
        }
    }
}

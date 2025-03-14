using MassTransit;
using ShippingService.Consumers;

namespace ShippingService.Extensions
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

                    config.ReceiveEndpoint("shipping-order-queue", e =>
                    {
                        e.Consumer<OrderPlacedConsumer>();

                        e.Bind("order-placed-exchange", x =>
                        {
                            x.ExchangeType = "fanout";
                        });
                        //e.ConfigureConsumer<OrderPlacedConsumer>(context);

                        //e.Bind("order-placed-exchange", x =>
                        //{
                        //    x.RoutingKey = "order.created";
                        //    x.ExchangeType = "direct";
                        //});
                    });
                });
            });
        }
    }
}

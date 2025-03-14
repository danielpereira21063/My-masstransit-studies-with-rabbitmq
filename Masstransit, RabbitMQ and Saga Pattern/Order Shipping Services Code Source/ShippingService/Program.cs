using ShippingService.Consumers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<OrderPlacedConsumer>();

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("rabbitmq://localhost");

        cfg.ReceiveEndpoint("shipping-order-queue", e =>
        {
            e.ConfigureConsumer<OrderPlacedConsumer>(context);

            e.Bind("order-placed-exchange", x =>
            {
                x.RoutingKey = "order.created";
                x.ExchangeType = "direct";
            });

        });

    });
});

var app = builder.Build();

app.UseHttpsRedirection();
app.Run();

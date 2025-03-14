using MassTransit;
using SharedMessages.Messages;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMassTransit(x =>
{
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("rabbitmq://localhost", h =>
        {
            h.PublisherConfirmation = true; // Enable publisher confirms
        });

        cfg.Message<OrderPlaced>(x => x.SetEntityName("order-placed-exchange"));
        cfg.Publish<OrderPlaced>(x =>
        {
            x.ExchangeType = "direct";
        });
    });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.MapPost("/orders", async (OrderRequest order, IBus bus) =>
{
    var orderPlacedMessage = new OrderPlaced(order.orderId, order.quantity);

    try
    {
        await bus.Publish(orderPlacedMessage, context =>
        {
            context.SetRoutingKey("order.created");
        });

        return Results.Created($"/orders/{order.orderId}", orderPlacedMessage);
    }
    catch (Exception ex)
    {
        // Log the exception and handle it appropriately
        Console.WriteLine($"Error publishing message: {ex.Message}");
        return Results.StatusCode(500); // Return an appropriate HTTP status code
    }

});

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.Run();

public record OrderRequest(Guid orderId, int quantity);

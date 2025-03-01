using MassTransit;
using OrderService.DTOs;
using SharedMessages.Messages;

namespace OrderService.Endpoints
{
    public static class OrderEndpoints
    {
        public static void MapOrderEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapPost("/orders", async (OrderRequest order, IBus bus) =>
            {
                var orderPlacedMessage = new OrderPlaced(order.OrderId, order.Quantity);
                await bus.Publish(orderPlacedMessage);

                return Results.Created($"/orders/{order.OrderId}", orderPlacedMessage);
            });
        }
    }
}

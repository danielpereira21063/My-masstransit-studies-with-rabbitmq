using MassTransit;
using SharedMessages.Messages;

namespace TrakingService.Consumers
{
    public class OrderPlacedConsumer : IConsumer<OrderPlaced>
    {
        public Task Consume(ConsumeContext<OrderPlaced> context)
        {
            Console.WriteLine($"Order received for tracking: {context.Message.OrderOd}");

            return Task.CompletedTask;
        }
    }
}

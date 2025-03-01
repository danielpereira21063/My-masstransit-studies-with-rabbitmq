namespace OrderService.DTOs
{
    public sealed record OrderRequest(Guid OrderId, int Quantity);
}

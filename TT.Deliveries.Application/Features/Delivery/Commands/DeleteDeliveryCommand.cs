using MediatR;

namespace TT.Deliveries.Application.Features.Delivery.Commands
{
    public sealed record DeleteDeliveryCommand(int orderId) : IRequest<int>;
}

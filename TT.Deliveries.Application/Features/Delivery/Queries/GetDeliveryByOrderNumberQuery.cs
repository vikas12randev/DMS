using MediatR;
using TT.Deliveries.Data.Dto.Response;

namespace TT.Deliveries.Application.Features.Delivery.Queries
{
    public record GetDeliveryByOrderIdQuery(int orderId, CancellationToken cancellationToken) : IRequest<DeliveryDto>;
}

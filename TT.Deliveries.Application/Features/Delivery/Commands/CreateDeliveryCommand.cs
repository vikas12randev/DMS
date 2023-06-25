using MediatR;
using TT.Deliveries.Data;
using TT.Deliveries.Data.Dto.Response;

namespace TT.Deliveries.Application.Features.Delivery.Command
{
    public sealed record CreateDeliveryCommand(CreateDeliveryDto deliveryDto) : IRequest<DeliveryDto>;
}

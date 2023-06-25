using MediatR;
using TT.Deliveries.Data.Dto.Response;
using TT.Deliveries.Data;

namespace TT.Deliveries.Application.Features.Delivery.Commands
{
    public sealed record UpdateDeliveryCommand(UpdateDeliveryDto updateDeliveryDto) : IRequest<DeliveryDto>;
}

using MediatR;
using TT.Deliveries.Application.Features.Delivery.Queries;
using TT.Deliveries.Data.Dto.Response;
using TT.Deliveries.Persistence.DataStore;

namespace TT.Deliveries.Application.Handlers
{
    public class GetDeliveryByOrderIdQueryHandler : IRequestHandler<GetDeliveryByOrderIdQuery, DeliveryDto>
    {
        private readonly FakeDataStore _fakeDataStore;

        public GetDeliveryByOrderIdQueryHandler(FakeDataStore fakeDataStore) => _fakeDataStore = fakeDataStore;

        public async Task<DeliveryDto> Handle(GetDeliveryByOrderIdQuery request, CancellationToken cancellationToken) =>
            await _fakeDataStore.GetDeliveryByOrderId(request.orderId);

    }
}

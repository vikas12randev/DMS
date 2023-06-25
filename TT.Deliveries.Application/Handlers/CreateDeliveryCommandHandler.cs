using MediatR;
using TT.Delieveries.Persistence;
using TT.Deliveries.Data.Dto.Response;
using TT.Deliveries.Persistence.DataStore;

namespace TT.Deliveries.Application.Features.Delivery.Command
{
    public sealed class CreateDeliveryCommandHandler : IRequestHandler<CreateDeliveryCommand, DeliveryDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDeliveryRepository _deliveryRepository;
        private FakeDataStore _fakeDataStore;

        public CreateDeliveryCommandHandler(IUnitOfWork unitOfWork, IDeliveryRepository deliveryRepository, FakeDataStore fakeDataStore)
        {
            _unitOfWork = unitOfWork;
            _deliveryRepository = deliveryRepository;
            _fakeDataStore = fakeDataStore;
        }

        public async Task<DeliveryDto> Handle(CreateDeliveryCommand request, CancellationToken cancellationToken)
        {
            var delivery = _fakeDataStore.GetDeliveryByOrderId(request.deliveryDto.Order.OrderId);

            if (delivery.Exception != null) 
                return await _deliveryRepository.CreateDeliveryAsync(request.deliveryDto, cancellationToken);

            //await _unitOfWork.Save(cancellationToken);

            throw new Exception($"Delivery already exists for order Id {request.deliveryDto.Order.OrderId}");
        }
    }
}


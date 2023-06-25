using MediatR;
using TT.Delieveries.Persistence;
using TT.Deliveries.Application.Features.Delivery.Command;
using TT.Deliveries.Application.Features.Delivery.Commands;
using TT.Deliveries.Persistence.DataStore;

namespace TT.Deliveries.Application.Handlers
{
    public sealed class DeleteDeliveryCommandHandler : IRequestHandler<DeleteDeliveryCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDeliveryRepository _deliveryRepository;
        private FakeDataStore _fakeDataStore;

        public DeleteDeliveryCommandHandler(IUnitOfWork unitOfWork, IDeliveryRepository deliveryRepository, FakeDataStore fakeDataStore)
        {
            _unitOfWork = unitOfWork;
            _deliveryRepository = deliveryRepository;
            _fakeDataStore = fakeDataStore;
        }

        public async Task<int> Handle(DeleteDeliveryCommand request, CancellationToken cancellationToken)
        {
            var delivery = _fakeDataStore.GetDeliveryByOrderId(request.orderId);

            if (delivery != null)
                return await _deliveryRepository.DeleteDeliveryByOrderIdAsync(request.orderId, cancellationToken);

             throw new Exception($"Order Id {request.orderId} not found");

            //await _unitOfWork.Save(cancellationToken);
        }
    }
}

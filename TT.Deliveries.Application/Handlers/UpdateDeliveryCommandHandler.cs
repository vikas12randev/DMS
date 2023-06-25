using MediatR;
using TT.Delieveries.Persistence;
using TT.Deliveries.Application.Features.Delivery.Commands;
using TT.Deliveries.Data.Dto.Response;
using TT.Deliveries.Data.Enums;
using TT.Deliveries.Data.Models;
using TT.Deliveries.Persistence.DataStore;

namespace TT.Deliveries.Application.Handlers
{
    public sealed class UpdateDeliveryCommandHandler : IRequestHandler<UpdateDeliveryCommand, DeliveryDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDeliveryRepository _deliveryRepository;
        private FakeDataStore _fakeDataStore;

        public UpdateDeliveryCommandHandler(IUnitOfWork unitOfWork, IDeliveryRepository deliveryRepository, FakeDataStore fakeDataStore)
        {
            _unitOfWork = unitOfWork;
            _deliveryRepository = deliveryRepository;
            _fakeDataStore = fakeDataStore;
        }

        public async Task<DeliveryDto> Handle(UpdateDeliveryCommand request, CancellationToken cancellationToken)
        {
            var delivery = await _fakeDataStore.GetDeliveryByOrderId(request.updateDeliveryDto.OrderId);

            if (((DeliveryState)request.updateDeliveryDto.State).ToString() == delivery.State)
                throw new InvalidOperationException($"Order Id {request.updateDeliveryDto.OrderId} is already set to {delivery.State} state");


            if (delivery != null)
            {

                switch (request.updateDeliveryDto.State)
                {
                    case DeliveryState.Created:
                        throw new InvalidOperationException($"Order state can not be changed to created");

                    case DeliveryState.Approved:
                    case DeliveryState.Cancelled:
                        //User has the permission to approve or cancel the delivery
                        if (request.updateDeliveryDto.Roles == Roles.User)
                            return await _deliveryRepository.UpdateDeliveryAsync(request.updateDeliveryDto, cancellationToken);
                        break;

                    case DeliveryState.Completed:
                        //Partner can complete the delivery if its in approved state
                        if (request.updateDeliveryDto.Roles == Roles.Partner)
                            return await _deliveryRepository.UpdateDeliveryAsync(request.updateDeliveryDto, cancellationToken);
                        else
                            throw new InvalidOperationException($"You can only change the state to Completed if you are a partner or the existing state is approved");
                };
            }

            //await _unitOfWork.Save(cancellationToken);

            return delivery;
        }
    }
}

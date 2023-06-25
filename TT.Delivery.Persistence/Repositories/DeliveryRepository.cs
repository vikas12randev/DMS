using Azure.Core;
using LinqToDB;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Threading;
using System.Threading.Tasks;
using TT.Deliveries.Data;
using TT.Deliveries.Data.Dto.Response;
using TT.Deliveries.Data.Dtos;
using TT.Deliveries.Data.Enums;
using TT.Deliveries.Data.Models;
using TT.Deliveries.Persistence.DataStore;
using DataContext = TT.Delieveries.Persistence.Context.DataContext;

namespace TT.Delieveries.Persistence.Repositories
{
    public class DeliveryRepository : BaseRepository<Delivery>, IDeliveryRepository
    {
        private FakeDataStore _fakeDataStore;

        public DeliveryRepository(DataContext context, FakeDataStore fakeDataStore) : base(context)
        {
            _fakeDataStore = fakeDataStore;
        }

        public async Task<DeliveryDto> CreateDeliveryAsync(CreateDeliveryDto createDeliveryDto, CancellationToken cancellation)
        {
            DeliveryDto deliveryDto = new DeliveryDto()
            {
                AccessWindow = new AccessWindow(DateTime.Now, DateTime.Now.AddHours(2)),
                Order = new OrderDto() 
                {
                    OrderNumber = createDeliveryDto.Order.OrderNumber,
                    Sender = createDeliveryDto.Order.Sender
                },
                Recipient = createDeliveryDto.Recipient,
                State = ((DeliveryState)DeliveryState.Created).ToString(),
            };

            DeliveryDataSource deliveryDataSource = new DeliveryDataSource()
            {
                AccessWindow = new AccessWindow(DateTime.Now, DateTime.Now.AddHours(2)),
                Order = new Order(createDeliveryDto.Order.OrderNumber, createDeliveryDto.Order.Sender),
                Recipient = createDeliveryDto.Recipient,
                State = DeliveryState.Created
            };

            await _fakeDataStore.AddDelivery(deliveryDataSource);

            return await Task.FromResult(deliveryDto);
        }

        public async Task<DeliveryDto> GetDeliveryByOrderIdAsync(int orderId, CancellationToken cancellationToken)
        {
            return await _fakeDataStore.GetDeliveryByOrderId(orderId);
        }

        public async Task<DeliveryDto> UpdateDeliveryAsync(UpdateDeliveryDto updateDeliveryDto, CancellationToken cancellation)
        {
            return await _fakeDataStore.UpdateDeliveryByOrderId(updateDeliveryDto);
        }

        public async Task<int> DeleteDeliveryByOrderIdAsync(int orderId, CancellationToken cancellation)
        {
            return await _fakeDataStore.DeleteDelivery(orderId);
        }
    }
}


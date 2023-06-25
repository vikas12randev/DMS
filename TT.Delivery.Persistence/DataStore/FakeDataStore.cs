using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TT.Deliveries.Data;
using TT.Deliveries.Data.Dto.Response;
using TT.Deliveries.Data.Dtos;
using TT.Deliveries.Data.Enums;
using TT.Deliveries.Data.Models;

namespace TT.Deliveries.Persistence.DataStore
{
    public class FakeDataStore
    {
        private static List<DeliveryDataSource> _deliveries;

        public FakeDataStore()
        {
            _deliveries = new List<DeliveryDataSource>
            {
               new DeliveryDataSource {
                    DateCreated = DateTime.Now, 
                    AccessWindow = new AccessWindow(DateTime.Now, DateTime.Now.AddHours(1)),
                    Order = new Order(1, "User1"),
                    Recipient = new Recipient("Receiver1", "London", "recepient1@gmail.com", "11111xxxxxx"),
                    State = DeliveryState.Created
               },
               new DeliveryDataSource {
                    DateCreated = DateTime.Now,
                    AccessWindow = new AccessWindow(DateTime.Now, DateTime.Now.AddHours(1)),
                    Order = new Order(2, "User2"),
                    Recipient = new Recipient("Receiver2", "London", "recepient2@gmail.com", "2222xxxxxx"),
                    State = DeliveryState.Created
               },
               new DeliveryDataSource {
                    DateCreated = DateTime.Now,
                    AccessWindow = new AccessWindow(DateTime.Now, DateTime.Now.AddHours(1)),
                    Order = new Order(3, "User3"),
                    Recipient = new Recipient("Receiver3", "London", "recepient3@gmail.com", "3333xxxxxxx"),
                    State = DeliveryState.Created
               },
            };
        }

        public async Task AddDelivery(DeliveryDataSource delivery)
        {
            _deliveries.Add(delivery);
            await Task.CompletedTask;
        }

        public async Task<int> DeleteDelivery(int orderId)
        {
            _deliveries.RemoveAt(_deliveries.FindIndex(x=> x.Order.Id == orderId));
            return await Task.FromResult(orderId);
        }

        public async Task<IEnumerable<DeliveryDataSource>> GetAllDeliveries() => await Task.FromResult(_deliveries);

        public async Task<DeliveryDto> GetDeliveryByOrderId(int orderId)
        {
            var delivertyDto = await Task.FromResult(_deliveries.Where(o => o.Order.Id == orderId).FirstOrDefault());

            if (delivertyDto == null)
                throw new Exception($"Order Id {orderId} not found");

            return new DeliveryDto()
            {
                State = ((DeliveryState)delivertyDto.State).ToString(),
                AccessWindow = delivertyDto.AccessWindow,
                Order = new OrderDto()
                {
                    OrderId = delivertyDto.Order.Id,
                    OrderNumber = delivertyDto.Order.OrderNumber,
                    Sender = delivertyDto.Order.Sender
                },
                Recipient = delivertyDto.Recipient
            };
        }            

        public async Task<DeliveryDto> UpdateDeliveryByOrderId(UpdateDeliveryDto updateDeliveryDto)
        {
            DeliveryDataSource deliveryDto = await Task.FromResult(_deliveries.Where(o => o.Order.Id == updateDeliveryDto.OrderId).FirstOrDefault());

            deliveryDto.State = updateDeliveryDto.State;

            return new DeliveryDto()
            {
                State = ((DeliveryState)updateDeliveryDto.State).ToString(),
                AccessWindow = deliveryDto.AccessWindow,
                Order = new OrderDto()
                {
                    OrderId = deliveryDto.Order.Id,
                    OrderNumber = deliveryDto.Order.OrderNumber,
                    Sender = deliveryDto.Order.Sender
                },
                Recipient = deliveryDto.Recipient
            };
        }      
    }
}

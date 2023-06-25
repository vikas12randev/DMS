using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using TT.Delieveries.Persistence.Context;
using TT.Deliveries.Data.Models;

namespace TT.Deliveries.Persistence.Context
{
    public class DataContextIntializer
    {
        private static DataContext context;

        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var serviceScope = serviceProvider.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<DataContext>();
                InitializeDeliveries(context);
            }
        }

        private static void InitializeDeliveries(DataContext context)
        {
            if (!context.Deliveries.Any())
            {
                Delivery delivery_01 = new Delivery {
                    DateCreated = new DateTime(),
                    AccessWindow = new AccessWindow(new DateTime(), new DateTime().AddHours(1)),
                    Order = new Order(1, "User1"),
                    Recipient = new Recipient("Receiver1", "London", "recepient1@gmail.com", "111111111111"),
                    State = Data.Enums.DeliveryState.Created

                };                

                context.Deliveries.Add(delivery_01);
                context.SaveChanges();
            }

            context.SaveChanges();
        }
    }
}

using System;
using System.Data.Common;
using TT.Delieveries.Application;

namespace TT.Deliveries.Data.Models
{
    public class Order : BaseEntity
    {
        public Order(int orderNumber, string sender)
        {
            Id = orderNumber;
            this.OrderNumber = orderNumber;
            this.Sender = sender;
            DateCreated = DateTime.Now;            
        }

        public int OrderNumber { get; set; }
        public string Sender { get; set; }
    }
}

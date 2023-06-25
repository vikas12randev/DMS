using System;
using TT.Deliveries.Data.Enums;
using TT.Deliveries.Data.Models;

namespace TT.Deliveries.Data.Dtos
{
    public class DeliveryDataSource
    {
        public DateTime? DateCreated { get; set; }

        public DeliveryState State { get; set; }

        public AccessWindow AccessWindow { get; set; }

        public Recipient Recipient { get; set; }

        public Order Order { get; set; }
    }
}

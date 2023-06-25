namespace TT.Deliveries.Data.Models
{
    using TT.Delieveries.Application;
    using TT.Deliveries.Data.Enums;

    public sealed class Delivery : BaseEntity
    {
        public DeliveryState State { get; set; }

        public AccessWindow AccessWindow { get; set; }

        public Recipient Recipient { get; set; }

        public Order Order { get; set; }
    }
}

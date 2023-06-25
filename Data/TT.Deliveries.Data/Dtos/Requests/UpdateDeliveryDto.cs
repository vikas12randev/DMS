using TT.Deliveries.Data.Enums;

namespace TT.Deliveries.Data
{
    public class UpdateDeliveryDto
    {
        public int OrderId { get; set; } 

        public DeliveryState State { get; set; }

        public Roles Roles { get; set; }
    }
}

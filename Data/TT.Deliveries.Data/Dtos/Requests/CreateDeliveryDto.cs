using TT.Deliveries.Data.Dtos;
using TT.Deliveries.Data.Models;

namespace TT.Deliveries.Data
{
    public class CreateDeliveryDto
    {
        public Recipient Recipient { get; set; }

        public OrderDto Order { get; set; }
    }
}

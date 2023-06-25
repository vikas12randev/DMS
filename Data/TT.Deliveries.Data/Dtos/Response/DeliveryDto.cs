using TT.Deliveries.Data.Dtos;
using TT.Deliveries.Data.Models;

namespace TT.Deliveries.Data.Dto.Response
{
    public class DeliveryDto
    {
        public string State { get; set; }

        public AccessWindow AccessWindow { get; set; }

        public Recipient Recipient { get; set; }

        public OrderDto Order { get; set; }
    }
}

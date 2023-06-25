using System.Threading;
using System.Threading.Tasks;
using TT.Deliveries.Data;
using TT.Deliveries.Data.Dto.Response;
using TT.Deliveries.Data.Models;

namespace TT.Delieveries.Persistence
{
    public interface IDeliveryRepository : IBaseRepository<Delivery>
    {
        Task<DeliveryDto> GetDeliveryByOrderIdAsync(int orderId, CancellationToken cancellationToken);

        Task<DeliveryDto> CreateDeliveryAsync(CreateDeliveryDto createDeliveryDto, CancellationToken cancellation);

        Task<DeliveryDto> UpdateDeliveryAsync(UpdateDeliveryDto updateDeliveryDto, CancellationToken cancellation);

        Task<int> DeleteDeliveryByOrderIdAsync(int orderId, CancellationToken cancellationToken);
    }
}


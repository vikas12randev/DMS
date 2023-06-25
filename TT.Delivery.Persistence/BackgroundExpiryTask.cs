using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TT.Deliveries.Data;
using TT.Deliveries.Data.Dtos;
using TT.Deliveries.Persistence.DataStore;

namespace TT.Deliveries.Persistence
{
    /// <summary>
    /// Background service wakes up every 60 minutes 
    /// and sets the state to expire where access window has gone past an hour
    /// </summary>
    public class BackgroundExpiryTask : BackgroundService
    {
        private readonly ILogger<BackgroundExpiryTask> _logger;

        private FakeDataStore _fakeDataStore;

        public BackgroundExpiryTask(FakeDataStore fakeDataStore, 
                                    ILogger<BackgroundExpiryTask> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _fakeDataStore = fakeDataStore;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogDebug("BackgroundExpiryTask is starting.");

            stoppingToken.Register(() => _logger.LogDebug("#1 BackgroundExpiryTask background task is stopping."));

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogDebug("BackgroundExpiryTask background task is doing background work.");

                //Waits for an hour before it checks for expiry window access
                await Task.Delay(3600000);

                UpdateExpiryAccessWindowDeliveries();               
            }

            _logger.LogDebug("BackgroundExpiryTask background task is stopping.");
        }

        private void UpdateExpiryAccessWindowDeliveries()
        {
            _logger.LogDebug("Checking deliveries for expiry access windows");

            var deliveryDtos = GetExpiryAccessWindowsDeliveries();

            foreach (var delivery in deliveryDtos)
            {
                UpdateDeliveryDto updateDeliveryDto = new UpdateDeliveryDto()
                {
                    OrderId = delivery.Order.Id,
                    Roles = Data.Enums.Roles.Partner,
                    State = Data.Enums.DeliveryState.Expired
                };
                _ = _fakeDataStore.UpdateDeliveryByOrderId(updateDeliveryDto);
            }
        }

        private IEnumerable<DeliveryDataSource> GetExpiryAccessWindowsDeliveries()
        {
            IEnumerable<int> orderIds = new List<int>();
            return _fakeDataStore.GetAllDeliveries()
                   .Result.Where(x=> x.AccessWindow.StartTime < DateTime.Now.AddSeconds(10)).ToList();
        }
    }
}

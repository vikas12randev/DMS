using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TT.Delieveries.Persistence.Repositories;
using TT.Delieveries.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using TT.Deliveries.Persistence.DataStore;
using TT.Deliveries.Persistence;

namespace TT.Delieveries.Persistence
{
    public static class ServiceConfigurations
    {
        public static void AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("Sqlite");
            services.AddDbContext<DataContext>(opt => opt.UseSqlServer(connectionString));
            services.AddHostedService<BackgroundExpiryTask>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IDeliveryRepository, DeliveryRepository>();
            services.AddSingleton<FakeDataStore>();
        }
    }
}


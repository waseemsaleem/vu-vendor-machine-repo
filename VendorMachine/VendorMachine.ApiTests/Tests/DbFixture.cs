#nullable disable
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using VendorMachine.ApiTests.Helpers;
using VendorMachine.Core.Data;
using VendorMachine.Core.DTOs;
using VendorMachine.Core.Services;
using VendorMachine.Core.Services.Interfaces;

namespace VendorMachine.ApiTests.Tests
{
    public class DbFixture
    {
        public DbFixture()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection
                .AddDbContext<ApiDbContext>(options =>
                    {
                        options.UseInMemoryDatabase("VendorMachineDatabase");
                        options.EnableSensitiveDataLogging(true);
                    },
                    ServiceLifetime.Singleton);
       
            serviceCollection.AddScoped<IUserService, UserService>();
            serviceCollection.AddScoped<IProductService, ProductService>();
            serviceCollection.AddSingleton<IUserAdapter, UserAdapter>();
            serviceCollection.AddSingleton<IProductAdapter, ProductAdapter>();
            serviceCollection.AddSingleton<IMockData, MockData>();
            ServiceProvider = serviceCollection.BuildServiceProvider();
            var mockData = ServiceProvider.GetService<IMockData>();
            mockData.GetContextWithData(ServiceProvider);
        }

        public ServiceProvider ServiceProvider { get; private set; }
    }
}

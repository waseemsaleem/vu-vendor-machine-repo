#nullable disable
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using VendorMachine.Core.Services;
using VendorMachine.Core.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using VendorMachine.Core.DTOs;
using VendorMachine.ApiTests.Helpers;
using VendorMachine.Core.Services.Interfaces;
using VendorMachine.Core.ViewModels;

namespace VendorMachine.ApiTests.Controllers
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
                    ServiceLifetime.Scoped);
            serviceCollection.AddScoped<IUserService, UserService>();
            serviceCollection.AddScoped<IProductService, ProductService>();
            serviceCollection.AddSingleton<IUserAdapter, UserAdapter>();
            serviceCollection.AddSingleton<IProductAdapter, ProductAdapter>();

            ServiceProvider = serviceCollection.BuildServiceProvider();
        }

        public ServiceProvider ServiceProvider { get; private set; }
    }

    public class ProductControllerTests : IClassFixture<DbFixture>
    {
        private readonly ServiceProvider _serviceProvider;


        private readonly IProductService _productServiceService;

        public ProductControllerTests(DbFixture fixture)
        {
            _serviceProvider = fixture.ServiceProvider;

            _productServiceService = _serviceProvider.GetService<IProductService>();
            //var options = new DbContextOptionsBuilder<ApiDbContext>()
            //    .UseInMemoryDatabase(databaseName: "VendorMachineDatabase")
            //    .Options;
            //ApiDbContext context = new ApiDbContext(options);
            //UserService userService = new UserService(context, UserAdapter.Instance);
            //_productServiceService = new ProductService(context, ProductAdapter.Instance, userService);
            //_productServiceService.AddProducts(ProductMockData.GetProducts()).Wait();
        }

        [Fact()]
        public async Task AddProducts_AddsProducts_ShouldReturnListOfAddedProducts()
        {
            /// Arrange

            /// Act
            GenericResponse result = await _productServiceService.AddProduct(ProductMockData.GetProduct());


            // /// Assert
            Assert.True(result.Success);
        }

        [Fact()]
        public async Task GetProducts_GetProducts_ShouldReturnListOfProducts()
        {
            /// Arrange
            await _productServiceService.AddProduct(ProductMockData.GetProduct());

            /// Act
            GenericResponse result = await _productServiceService.GetProducts();


            // /// Assert
            Assert.True(result.Success);
            Assert.True(((List<ProductVM>)result.Reponse).Count > 0);
        }

        [Fact()]
        public async Task DeleteProducts_DeleteProducts_ShouldReturnDeletedProduct()
        {
            // Arrange
            await _productServiceService.AddProduct(ProductMockData.GetProduct());
            string productId = "2b368c67-51f1-4580-b0de-d8ba71b9768d";

            // Act
            GenericResponse result = await _productServiceService.DeleteProduct(productId);


            // Assert
            Assert.True(result.Success);
        }

        [Fact()]
        public async Task UpdateProducts_UpdateProducts_ShouldReturnUpdatedProduct()
        {
            // Arrange
            await _productServiceService.AddProduct(ProductMockData.GetProduct());
            var product = new ProductVM()
            {
                ProductName = "UpdatedProduct",
                Cost = 50,
                AmountAvailable = 100,
                ProductId = "2b368c67-51f1-4580-b0de-d8ba71b9768d"
            };
            // Act
            GenericResponse result = await _productServiceService.UpdateProduct(product.ProductId, product);


            //Assert
            Assert.True(result.Success);
        }
    }
}

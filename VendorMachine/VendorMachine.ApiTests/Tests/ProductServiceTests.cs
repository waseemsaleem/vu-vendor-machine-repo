using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using VendorMachine.Core.Services.Interfaces;
using VendorMachine.Core.ViewModels;
using Xunit;

namespace VendorMachine.ApiTests.Tests
{
    public class ProductServiceTests : IClassFixture<DbFixture>
    {
        private readonly ServiceProvider _serviceProvider;


        private readonly IProductService _productService;
        

        public ProductServiceTests(DbFixture fixture)
        {
            _serviceProvider = fixture.ServiceProvider;

            _productService = _serviceProvider.GetService<IProductService>();
            
        }

        [Fact()]
        public async Task AddProducts_AddsProducts_ShouldReturnListOfAddedProducts()
        {
            // Arrange
            var newProductId = Guid.NewGuid().ToString();
            var product = new ProductVM
            {
                ProductId = newProductId,
                ProductName = $"Product {newProductId}",
                AmountAvailable = 100,
                Cost = 500,
                SellerId = "2b368c67-51f1-4580-b0de-d8ba71b5468d"
            };

            // Act
            GenericResponse result = await _productService.AddProduct(product);


            // Assert
            Assert.True(result.Success);
        }

        [Fact()]
        public async Task GetProducts_GetProducts_ShouldReturnListOfProducts()
        {
            // Arrange
            
            // Act
            var result = await _productService.GetProducts();


            // Assert
            Assert.True(result.Success);
            Assert.NotNull((List<ProductVM>)result.Reponse);
            Assert.True(((List<ProductVM>)result.Reponse).Count > 0);
        }

        [Fact()]
        public async Task DeleteProducts_DeleteProducts_ShouldReturnDeletedProduct()
        {
            // Arrange
            string productId = "2b368c67-51f1-4580-b0de-d8ba71b9768d";

            // Act
            GenericResponse result = await _productService.DeleteProduct(productId);


            // Assert
            Assert.True(result.Success);
        }

        [Fact()]
        public async Task UpdateProducts_UpdateProducts_ShouldReturnUpdatedProduct()
        {
            // Arrange
            var product = new ProductVM()
            {
                ProductName = "UpdatedProduct",
                Cost = 20,
                AmountAvailable = 20,
                ProductId = "2b368c67-51f1-4580-b0de-d8ba71b9768d",
                SellerId = "2b368c67-51f1-4580-b0de-d8ba71a5468d",
            };
            // Act
            GenericResponse result = await _productService.UpdateProduct(product.ProductId, product);


            //Assert
            Assert.True(result.Success);
        }
        [Fact()]
        public async Task BuyProducts_ProcessBuyProducts_ShouldReturnSuccess()
        {
            // Arrange
            var productId = "efdb929d-2998-443a-b15c-e27b9715b09f";
            var quantity = 2;
            var userId = "2b368c67-51f1-4580-b0de-d8ba71b5468d";

            // Act
            GenericResponse result = await _productService.BuyProduct(productId, quantity, userId);

            // Assert
            Assert.True(result.Success);
        }
        [Fact()]
        public async Task DepositProducts_ProcessDepositAmount_ShouldReturnSuccess()
        {
            // Arrange
            var amount = 100;
            
            var claims = new List<Claim>()
            {
                new Claim("UserId","2b368c67-51f1-4580-b0de-d8ba71b5468d")
            };
            var userService = _serviceProvider.GetService<IUserService>();
            // Act
            GenericResponse result = await userService.Deposit(amount, claims);

            // Assert
            Assert.True(result.Success);
        }
    }
}
#nullable disable

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using VendorMachine.Core.Data;
using VendorMachine.Core.Models;
using VendorMachine.Core.Services.Interfaces;
using VendorMachine.Core.ViewModels;

namespace VendorMachine.ApiTests.Helpers
{
    public class MockData : IMockData
    {
       public ApiDbContext GetContextWithData(ServiceProvider _serviceProvider)
        {
            var options = new DbContextOptionsBuilder<ApiDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var context  =_serviceProvider.GetService<ApiDbContext>();

            var roles = new List<RoleModel>()
            {
                new RoleModel(){
                    RoleId = "2b368c67-51f1-4580-b0de-d8ba71a9768d",
                    RoleName = "Buyer"
                },
                new RoleModel(){
                    RoleId = "efdb929d-2998-443a-b15c-e27b9715b09f",
                    RoleName = "Seller"
                }
            };

            var users = new List<UserModel>()
            {
                new UserModel()
                {
                    UserId = "2b368c67-51f1-4580-b0de-d8ba71a5468d",
                    Deposit = 20,
                    IsLoggedIn = false,
                    Password = "Test@123",
                    UserName = "Seller"
                },
                new UserModel(){
                    UserId = "2b368c67-51f1-4580-b0de-d8ba71b5468d",
                    Deposit = 100,
                    IsLoggedIn = false,
                    Password = "Test@123",
                    UserName = "Buyer"
                }
            };
            context.Roles.AddRange(roles);
            context.Users.AddRange(users);
            context.Products.Add(new ProductModel()
            {
                ProductId = "2b368c67-51f1-4580-b0de-d8ba71b9768d",
                ProductName = "La Trappe Isid'or",
                SellerId = "2b368c67-51f1-4580-b0de-d8ba71a5468d",
                AmountAvailable = 100,
                Cost = 5
            });
            context.Products.Add(new ProductModel()
            {
                ProductId = "2b368c67-51f1-4580-b0de-d8ba71b8768d",
                ProductName = "La Trappe Isid'or",
                SellerId = "2b368c67-51f1-4580-b0de-d8ba71a5468d",
                AmountAvailable = 100,
                Cost = 5
            });
            context.Products.Add(new ProductModel()
            {
                ProductId = "efdb929d-2998-443a-b15c-e27b9715b09f",
                ProductName = "Product 2",
                AmountAvailable = 200,
                Cost = 10,
                SellerId = "2b368c67-51f1-4580-b0de-d8ba715468d"
            });

            context.SaveChanges();

            return context;
        }
    }
}

#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VendorMachine.Core.Data;
using VendorMachine.Core.Helpers;
using VendorMachine.Core.Models;
using VendorMachine.Core.Services.Interfaces;
using VendorMachine.Core.ViewModels;
using static VendorMachine.Core.Helpers.GlobalHelpers;

namespace VendorMachine.Core.Services
{
    public class ProductService : IProductService
    {
        private readonly ApiDbContext _context;
        private readonly IUserService _userService;
        private readonly IProductAdapter _productAdapter;

        public ProductService(ApiDbContext context, IProductAdapter productAdapter, IUserService userService)
        {
            _context = context;
            _productAdapter = productAdapter;
            _userService = userService;
        }

        public async Task<GenericResponse> AddProducts(List<ProductVM> products)
        {
            _context.Products.AddRange(_productAdapter.ToProductModel(products));
            try
            {
                await _context.SaveChangesAsync();
                return ResponseHelper.SuccessResponse("Products saved successfully", products);
            }
            catch (Exception ex)
            {
                return ResponseHelper.FailResponse(ex.Message, null);
                // log the ex
                //throw new Exception("There is a conflict while adding the productService entity");
            }
        }

        public async Task<GenericResponse> AddProduct(ProductVM product)
        {

            if (string.IsNullOrEmpty(product.ProductId))
            {
                product.ProductId = Guid.NewGuid().ToString();
            }

            _context.Products.Add(_productAdapter.ToProductModel(product));
            try
            {
                await _context.SaveChangesAsync();
                return ResponseHelper.SuccessResponse("Product saved successfully", product);

            }
            catch (Exception ex)
            {
                return ResponseHelper.FailResponse(ex.Message, null);
                // logging
                // return error response
            }
        }

        public async Task<GenericResponse> BuyProduct(string productId, int quantity, string userId)
        {
            try
            {
                UserVM user = (UserVM)(await _userService.GetUser(userId)).Reponse;
                if (!Constants.amounts.Any(x => x.Equals(user.Deposit)))
                {
                    throw new Exception("User must have some deposit money to purchase products");
                }

                var product= await _context.Products.FindAsync(productId);
                if (quantity > product.AmountAvailable)
                {
                    throw new Exception("Requested productService amount is not available to buy.");
                }

                int productCost = quantity * product.Cost;
                if (productCost > user.Deposit)
                {
                    throw new Exception("User does not have enough deposit to make this purchase");
                }

                user.Deposit = user.Deposit - productCost;
                product.AmountAvailable = product.AmountAvailable - quantity;
                await _userService.UpdateUser(user.UserId, user);
                _context.Products.Update(product);
                await _context.SaveChangesAsync();

                var response = new BuyProductVM
                {
                    Product = new ProductVM
                    {
                        ProductId = product.ProductId,
                        ProductName = product.ProductName
                    },
                    TotalSpent = productCost,
                    Change = Constants.amounts.Any(x => x.Equals(user.Deposit)) ? user.Deposit : 0
                };
                return ResponseHelper.SuccessResponse("Buy Product successfully", response);
            }
            catch (Exception ex)
            {
                return ResponseHelper.FailResponse("BuyProduct is not successfully", null);
                // logging ex
                // return error response
            }
        }

        public async Task<GenericResponse> DeleteProduct(string id)
        {
            var response = new GenericResponse();
            var productModel = await _context.Products.FindAsync(id);
            try
            {
                if (productModel == null)
                {
                    throw new Exception("Selected productService does not exists.");
                }

                var trackedModel = _context.Products.Remove(productModel);
                await _context.SaveChangesAsync();
                return ResponseHelper.SuccessResponse("Product removed successfully", _productAdapter.ToProductVM(productModel));
            }
            catch (Exception ex)
            {
                return ResponseHelper.FailResponse("Product not removed successfully", null);
                // logging ex
                // return error response
            }
            return response;
        }

        public async Task<GenericResponse> GetProduct(string id)
        {
            var productModel = await _context.Products.FindAsync(id);

            if (productModel == null)
            {
                return ResponseHelper.FailResponse($"No Product found with id = '{id}'", null);
            }
            return ResponseHelper.SuccessResponse("Get Product successful", _productAdapter.ToProductVM(productModel));
        }

        public async Task<GenericResponse> GetProducts()
        {
            if (_context.Products == null)
            {
                return ResponseHelper.FailResponse($"No Product found", null);
            }
            return ResponseHelper.SuccessResponse("Get Product successful", _productAdapter.ToProductVM(await _context.Products.ToListAsync()));
        }

        public async Task<GenericResponse> UpdateProduct(string id, ProductVM productModel)
        {
            try
            {
                var product = _context.Products.Find(productModel.ProductId);
                var sellerId = product.SellerId;
                _context.Entry(product).State = EntityState.Detached;
                product = _productAdapter.ToProductModel(productModel);
                product.SellerId = sellerId;
                _context.Products.Remove(product);
                _context.Products.Update(product);
                await _context.SaveChangesAsync();
                return ResponseHelper.SuccessResponse("Product Update Successfully", product);
            }
            catch (Exception ex)
            {
                return ResponseHelper.FailResponse(ex.Message, null);
                // logging
                // return error response
            }
        }
    }
}

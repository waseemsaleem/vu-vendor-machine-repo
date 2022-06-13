using System.Collections.Generic;
using VendorMachine.Core.Models;
using VendorMachine.Core.Services.Interfaces;
using VendorMachine.Core.ViewModels;

namespace VendorMachine.Core.DTOs
{
    public class ProductAdapter : IProductAdapter
    {
        public static readonly ProductAdapter Instance = new ProductAdapter();
        public ProductAdapter() { }

        public ProductModel ToProductModel(ProductVM product)
        {
            ProductModel productModel = new ProductModel
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                AmountAvailable = product.AmountAvailable,
                Cost = product.Cost,
                SellerId = product.SellerId
            };
            return productModel;
        }

        public List<ProductModel> ToProductModel(List<ProductVM> products)
        {
            List<ProductModel> result = new List<ProductModel>();
            foreach (ProductVM product in products)
            {
                result.Add(ToProductModel(product));
            }
            return result;
        }

        public ProductVM ToProductVM(ProductModel product)
        {
            return new ProductVM
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                AmountAvailable = product.AmountAvailable,
                Cost = product.Cost,
            };
        }

        public List<ProductVM> ToProductVM(List<ProductModel> products)
        {
            List<ProductVM> result = new List<ProductVM>();
            foreach (ProductModel product in products)
            {
                result.Add(ToProductVM(product));
            }
            return result;
        }
    }
}

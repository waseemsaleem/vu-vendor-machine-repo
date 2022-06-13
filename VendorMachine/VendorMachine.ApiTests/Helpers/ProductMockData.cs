#nullable disable

using System.Collections.Generic;
using System.Linq;
using VendorMachine.Core.ViewModels;

namespace VendorMachine.ApiTests.Helpers
{
    public class ProductMockData
    {
        static ProductVM _product = new ProductVM
        {
            ProductId = "2b368c67-51f1-4580-b0de-d8ba71b9768d",
            ProductName = "Product 1",
            AmountAvailable = 100,
            Cost = 500,
            SellerId = "2b368c67-51f1-4580-b0de-d8ba71b5468d"
        };
        static List<ProductVM> _products = new List<ProductVM>()
            {
                new ProductVM{
                    ProductId = "2b368c67-51f1-4580-b0de-d8ba71a9768d",
                    ProductName = "Product 1",
                    AmountAvailable = 100,
                    Cost = 500,
                    SellerId = "2b368c67-51f1-4580-b0de-d8ba71a5468d"
                },
                new ProductVM{
                    ProductId = "efdb929d-2998-443a-b15c-e27b9715b09f",
                    ProductName = "Product 2",
                    AmountAvailable = 200,
                    Cost = 1000,
                    SellerId = "2b368c67-51f1-4580-b0de-d8ba71a5468d"
                }
            };

        public static List<ProductVM> GetProducts()
        {
            return _products;
        }

        public static ProductVM GetProduct()
        {
            return _product;
        }

        public static ProductVM GetProduct(string id)
        {
            return _products.FirstOrDefault(x => x.ProductId.Equals(id));
        }
    }
}

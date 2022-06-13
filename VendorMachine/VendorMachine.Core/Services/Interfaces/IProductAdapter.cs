using System.Collections.Generic;
using VendorMachine.Core.Models;
using VendorMachine.Core.ViewModels;

namespace VendorMachine.Core.Services.Interfaces
{
    public interface IProductAdapter
    {
        ProductVM ToProductVM(ProductModel product);
        ProductModel ToProductModel(ProductVM product);
        List<ProductModel> ToProductModel(List<ProductVM> products);
        List<ProductVM> ToProductVM(List<ProductModel> products);
    }
}

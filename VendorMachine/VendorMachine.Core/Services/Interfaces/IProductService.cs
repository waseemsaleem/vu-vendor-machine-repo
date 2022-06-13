using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using VendorMachine.Core.ViewModels;

namespace VendorMachine.Core.Services.Interfaces
{
    public interface IProductService
    {
        Task<GenericResponse> GetProducts();
        Task<GenericResponse> GetProduct(string id);
        Task<GenericResponse> UpdateProduct(string id, ProductVM productModel);
        Task<GenericResponse> AddProduct(ProductVM product);
        Task<GenericResponse> AddProducts(List<ProductVM> product);
        Task<GenericResponse> DeleteProduct(string id);
        Task<GenericResponse> BuyProduct(string productId, int quantity, string userId);
    }
}

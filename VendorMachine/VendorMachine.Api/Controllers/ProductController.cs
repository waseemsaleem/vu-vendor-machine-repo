using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VendorMachine.Core.Helpers;
using VendorMachine.Core.Models;
using VendorMachine.Core.Services.Interfaces;
using VendorMachine.Core.ViewModels;

namespace VendorMachine.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        // GET: api/Product
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetProducts()
        {
            GenericResponse products = await _productService.GetProducts();

            if (products.Reponse == null)
            {
                return NotFound();
            }
            return Ok(products);
        }

        // GET: api/Product/5
        [HttpGet("{id}")]
        public async Task<ActionResult> GetProduct(string id)
        {
            var roleNameVm = (UserRoleNameVM)HttpContext.Items["User"];
            if (roleNameVm != null && roleNameVm.RoleName != RoleConstants.Seller)
                return Unauthorized(new { message = "Unauthorized" });
            var productModel = await _productService.GetProduct(id);

            if (productModel.Reponse == null)
            {
                return NotFound();
            }

            return Ok(productModel);
        }

        // PUT: api/Product/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(string id, ProductVM productModel)
        {
            var roleNameVm = (UserRoleNameVM)HttpContext.Items["User"];
            if (roleNameVm != null && roleNameVm.RoleName != RoleConstants.Seller)
                return Unauthorized(new { message = "Unauthorized" });
            if (id != productModel.ProductId)
            {
                return BadRequest();
            }

            var result = await _productService.UpdateProduct(id, productModel);
            return Ok(result);
        }

        // POST: api/Product
        [HttpPost]
        public async Task<ActionResult> PostProduct(ProductVM productModel)
        {
            var roleNameVm = (UserRoleNameVM)HttpContext.Items["User"];
            if (roleNameVm != null && roleNameVm.RoleName != RoleConstants.Seller)
                return Unauthorized(new { message = "Unauthorized" });
            var result = await _productService.AddProduct(productModel);
            return Ok(result);
        }

        // DELETE: api/Product/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(string id)
        {
            var roleNameVm = (UserRoleNameVM)HttpContext.Items["User"];
            if (roleNameVm != null && roleNameVm.RoleName != RoleConstants.Seller)
                return Unauthorized(new { message = "Unauthorized" });

            var result = await _productService.DeleteProduct(id);
            return Ok(result);
        }

        [HttpPost("buy")]
        public async Task<IActionResult> Buy(string productId, int quantity)
        {
            var roleNameVm = (UserRoleNameVM)HttpContext.Items["User"];
            if (roleNameVm != null && roleNameVm.RoleName != RoleConstants.Buyer)
                return Unauthorized(new { message = "Unauthorized" });

            string userId = User.Claims.First(x => x.Type.Equals(GlobalHelpers.Constants.c_userId)).Value;
            GenericResponse result = await _productService.BuyProduct(productId, quantity, userId);
            return Ok(result);
        }
    }
}

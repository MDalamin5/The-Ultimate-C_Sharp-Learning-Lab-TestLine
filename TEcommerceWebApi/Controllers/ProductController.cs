using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TEcommerceWebApi.data;
using TEcommerceWebApi.DTOs;
using TEcommerceWebApi.Models;
using TEcommerceWebApi.Services;

namespace TEcommerceWebApi.Controllers
{
    [ApiController]
    [Route("/api/v2/products")]
    public class ProductController: ControllerBase
    {

        private readonly AppDbContext _appDbContext;
        private readonly ProductService _productService;

        public ProductController(ProductService productService, AppDbContext appDbContext)
        {
            _productService = productService;
            _appDbContext = appDbContext;
        }

        

        // Create a Product

        [HttpPost]
        public async Task<IActionResult> CreateProduct(ProductCreateDto productData)
        {
            var category = await _productService.CreateProduct(productData);
            if (category == null)
            {
                return NotFound(ApiResponse<object>.ErrorResponse(
                    new List<string> { $"Category with ID '{productData.CategoryId}' does not exist." }, 
                    404, 
                    "Validation Failed."
                ));
            }


            return Ok(ApiResponse<ProductReadDto>.SuccessResponse(category, 201, "Product created successfully."));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var allProducts = await _productService.GetAllProducts();
            
            if (allProducts == null)
                return Ok(ApiResponse<object>.SuccessResponse(default, 200, "No product founded"));
                
            return Ok(ApiResponse<List<ProductReadDto>>.SuccessResponse(allProducts, 200, "All Product are return."));
        }
    }
}


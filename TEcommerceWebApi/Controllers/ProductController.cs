using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TEcommerceWebApi.data;
using TEcommerceWebApi.DTOs;
using TEcommerceWebApi.Models;

namespace TEcommerceWebApi.Controllers
{
    [ApiController]
    [Route("/api/v2/products")]
    public class ProductController: ControllerBase
    {

        private readonly AppDbContext _appDbContext;

        public ProductController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        

        // Create a Product

        [HttpPost]
        public async Task<IActionResult> CreateProduct(ProductCreateDto productData)
        {
            var category = await _appDbContext.Categories.FindAsync(productData.CategoryId);
            
            if (category == null)
            {
                return NotFound(ApiResponse<object>.ErrorResponse(
                    new List<string> { $"Category with ID '{productData.CategoryId}' does not exist." }, 
                    404, 
                    "Validation Failed."
                ));
            }

            var newProduct = new Product
            {
                ProductId = Guid.NewGuid(),
                Name = productData.Name,
                Price = productData.Price,
                CategoryId = productData.CategoryId
            };

            await _appDbContext.Products.AddAsync(newProduct);
            await _appDbContext.SaveChangesAsync();

            var responseProduct = new ProductReadDto
            {
                ProductId = newProduct.ProductId,
                Name = newProduct.Name,
                Price = newProduct.Price,
                CategoryId = newProduct.CategoryId,
                CategoryName = category.Name
            };

            return Ok(ApiResponse<ProductReadDto>.SuccessResponse(responseProduct, 201, "Product created successfully."));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var allProducts = await _appDbContext.Products
            .AsNoTracking()
            .Select(p => new ProductReadDto
            {
                ProductId = p.ProductId,
                Name = p.Name,
                Price = p.Price,
                CategoryId = p.CategoryId,
                CategoryName = p.Category != null ? p.Category.Name : string.Empty
            }).ToListAsync();
            
            return Ok(ApiResponse<List<ProductReadDto>>.SuccessResponse(allProducts, 200, "All Product are return."));
        }
    }
}


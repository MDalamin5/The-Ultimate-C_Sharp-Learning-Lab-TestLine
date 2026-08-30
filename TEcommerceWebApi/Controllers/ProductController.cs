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
using TEcommerceWebApi.Interfaces;

namespace TEcommerceWebApi.Controllers
{
    [ApiController]
    [Route("/api/v2/products")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        // Injected Interface
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<ProductReadDto>>> CreateProduct([FromBody] ProductCreateDto productData)
        {
            var createdProduct = await _productService.CreateProduct(productData);

            if (createdProduct == null)
            {
                return NotFound(ApiResponse<object>.ErrorResponse(
                    new List<string> { $"Category with ID '{productData.CategoryId}' does not exist." }, 
                    404, 
                    "Validation Failed."
                ));
            }

            return StatusCode(201, ApiResponse<ProductReadDto>.SuccessResponse(createdProduct, 201, "Product created successfully."));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts([FromQuery] int pageNumber=1, [FromQuery] int pageSize=4)
        {
            var allProducts = await _productService.GetAllProducts(pageNumber, pageSize);
            return Ok(ApiResponse<PaginatedResult<ProductReadDto>>.SuccessResponse(allProducts, 200, "All Products returned successfully."));
        }
    }
}


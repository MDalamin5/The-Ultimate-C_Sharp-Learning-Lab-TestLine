using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EcommerceWebApi.Models;
using EcommerceWebApi.DTOs;
using EcommerceWebApi.Services;
using EcommerceWebApi.Interfaces;
using EcommerceWebApi.Helpers;

namespace EcommerceWebApi.Controllers
{   
    [ApiController]
    [Route("/api/v1/categories")]
    public class CategoryController:ControllerBase
    {
        private ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        // TO read teh category => api/v1/categories
        [HttpGet]
        public async Task<IActionResult> GetCategories([FromQuery] QueryParameters queryParameters)
        {
            queryParameters.Validate();
            var categoryList = await _categoryService.GetAllCategories(queryParameters);

            return Ok(ApiResponse<PaginatedRecord<CategoryReadDto>>.SuccessResponse(categoryList, "Categories returned", 200));
        }


        // Get categories by ID:
        [HttpGet("{categoryId:guid}")]
        public async Task<IActionResult> GetCategoryById(Guid categoryId)
        {
            var foundCategory = await _categoryService.GetCategoryById(categoryId);
            if(foundCategory == null)
                return NotFound(ApiResponse<object>.ErrorResponse(new List<string> {"Category Name is Required"}, 404, "Invalid Request."));
            else
            {
            
                return Ok(ApiResponse<CategoryReadDto>.SuccessResponse(foundCategory, "Category Found Successful", 200));
            }
            
            
        }

        // To Create a categories => POST: api/v1/categories
        [HttpPost]
        public async Task<IActionResult> CreateCategories([FromBody] CategoryCreateDto categoryData)
        {
            var newCategory = await _categoryService.CreateCategory(categoryData);

            return Created($"/api/v1/categories/{newCategory.CategoryId}", ApiResponse<CategoryReadDto>.SuccessResponse(newCategory, "Categories Created Successful", 201));
        }


        // update the categories value: Delete: api/v1/categories/{categoryId}
        [HttpDelete("{categoryId:guid}")]
        public async Task<IActionResult> DeleteCategoryById(Guid categoryId)
        {
            var foundCategory = await _categoryService.DeleteCategoryById(categoryId);
            if(foundCategory)  
            {
                return Ok(ApiResponse<object>.SuccessResponse(null, "Deleted Successful.", 204));
            }
            else
                return NotFound(ApiResponse<object>.ErrorResponse(new List<string> {"Category is not found with this id"}, 404, "Validations Failed."));
        }

        // update category data PUT: api/v1/categories/{categoryId}
        [HttpPut("{categoryId:guid}")]
        public async Task<IActionResult> UpdateCategoryById(Guid categoryId, [FromBody] CategoryUpdateDto categoryData)
        {
            var foundCategory = await _categoryService.UpdateCategory(categoryId, categoryData);

            if (foundCategory == null)
                return NotFound(ApiResponse<object>.ErrorResponse(new List<string> {"Category is not found with this id"}, 400, "Validations Failed."));
            
            
            return Ok(ApiResponse<CategoryReadDto>.SuccessResponse(foundCategory, "Update successful", 204));
        }   

    }
}
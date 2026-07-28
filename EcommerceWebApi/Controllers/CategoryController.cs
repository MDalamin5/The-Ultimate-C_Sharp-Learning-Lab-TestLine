using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EcommerceWebApi.Models;
using EcommerceWebApi.DTOs;
using EcommerceWebApi.Services;

namespace EcommerceWebApi.Controllers
{   
    [ApiController]
    [Route("/api/v1/categories")]
    public class CategoryController:ControllerBase
    {
        // private static List<Category> categories = new List<Category>();
        private CategoryService _categoryService;
        public CategoryController(CategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        // TO read teh category => api/v1/categories
        [HttpGet]
        public IActionResult GetCategories([FromQuery] string searchValue = "")
        {
            var categoryList = _categoryService.GetAllCategories();

            return Ok(ApiResponse<List<CategoryReadDto>>.SuccessResponse(categoryList, "Categories returned", 200));
        }


        // Get categories by ID:
        [HttpGet("{categoryId:guid}")]
        public IActionResult GetCategoryById(Guid categoryId)
        {
            var foundCategory = _categoryService.GetCategoryById(categoryId);
            if(foundCategory == null)
                return NotFound(ApiResponse<object>.ErrorResponse(new List<string> {"Category Name is Required"}, 404, "Invalid Request."));
            else
            {
            
                return Ok(ApiResponse<CategoryReadDto>.SuccessResponse(foundCategory, "Category Found Successful", 200));
            }
            
            
        }

        // To Create a categories => POST: api/v1/categories
        [HttpPost]
        public IActionResult CreateCategories([FromBody] CategoryCreateDto categoryData)
        {
            var newCategory = _categoryService.CreateCategory(categoryData);

            return Created($"/api/v1/categories/{newCategory.CategoryId}", ApiResponse<CategoryReadDto>.SuccessResponse(newCategory, "Categories Created Successful", 201));
        }


        // // update the categories value: Delete: api/v1/categories/{categoryId}
        // [HttpDelete("{categoryId:guid}")]
        // public IActionResult DeleteCategoryById(Guid categoryId)
        // {
        //     var foundCategory = categories.FirstOrDefault(c => c.CategoryId == categoryId);
        //     if(foundCategory != null)
        //     {
        //         categories.Remove(foundCategory);
        //         return Ok(ApiResponse<object>.SuccessResponse(null, "Update successful", 204));
        //     }
        //     else
        //         return NotFound(ApiResponse<object>.ErrorResponse(new List<string> {"Category is not found with this id"}, 404, "Validations Failed."));
        // }

        // // update category data PUT: api/v1/categories/{categoryId}
        // [HttpPut("{categoryId:guid}")]
        // public IActionResult UpdateCategoryById(Guid categoryId, [FromBody] CategoryUpdateDto categoryData)
        // {
        //     var foundCategory = categories.FirstOrDefault(c => c.CategoryId == categoryId);

        //     if (foundCategory == null)
        //         return NotFound(ApiResponse<object>.ErrorResponse(new List<string> {"Category is not found with this id"}, 400, "Validations Failed."));
            
            
        //     foundCategory.Name = categoryData.Name;
        //     foundCategory.Description = categoryData.Description;
        //     return Ok(ApiResponse<object>.SuccessResponse(null, "Update successful", 204));
        // }   

    }
}
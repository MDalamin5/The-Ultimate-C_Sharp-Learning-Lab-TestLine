using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TEcommerceWebApi.DTOs;
using TEcommerceWebApi.Interfaces;

namespace TEcommerceWebApi.Controllers
{
    [ApiController]
    [Route("/api/v2/categories")]
    public class CategoryController: ControllerBase
    {
        public ICategoryService _categoryService;
        
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }


        // Read all categories
        [HttpGet]
        public async Task<IActionResult> GetCategories([FromQuery] string searchVale = "")
        {
            // Data Binding With Read Dto
            var responseCategory = await _categoryService.GetAllCategory();

            return Ok(ApiResponse<List<CategoryReadDto>>.SuccessResponse(responseCategory, 200, "Category Returned Successfully."));
        }

        //Read a category byId
        
        [HttpGet("{categoryId:guid}")]
        public async Task<IActionResult> GetCategoryById(Guid categoryId)
        {
            
            var responseCategory = await _categoryService.GetCategoryById(categoryId);

            if(responseCategory == null)
                return NotFound(ApiResponse<object>.ErrorResponse(new List<string>{"Category not found with this id."}, 404, "Validation Invalid."));

            return Ok(ApiResponse<CategoryReadDto>.SuccessResponse(responseCategory, 200, "Category founded."));
        }

        // Create Category
        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryCreateDto categoryData)
        {
            //Return Data followed by CategoryReadDto
            var responseCreateCategory = await _categoryService.CreateCategory(categoryData);

            return Created(nameof(GetCategoryById), ApiResponse<CategoryReadDto>.SuccessResponse(responseCreateCategory, 201, "Category Created Successfully."));
        }


        // update a Category
        [HttpPut("{categoryId:guid}")]
        public async Task<IActionResult> UpdateCategoryById(Guid categoryId, [FromBody] CategoryUpdateDto categoryData)
        {
            var foundCategory = await _categoryService.UpdateCategory(categoryId, categoryData);

            if(foundCategory == null)
                return NotFound(ApiResponse<object>.ErrorResponse(new List<string>{"category is not found with this id."}, 400, "Validation Failed."));
    
            return Ok(ApiResponse<CategoryReadDto>.SuccessResponse(foundCategory, 204, "Category Updated successfully."));
            
        }




        // delete category by ID
        [HttpDelete("{categoryId:guid}")]
        public async Task<IActionResult> DeleteCategoryById(Guid categoryId)
        {
            bool response = await _categoryService.DeleteCategoryById(categoryId);
            if(response == false)
                return NotFound(ApiResponse<object>.ErrorResponse(new List<string>{"category is not found with this id."}, 404, "Validation Failed."));
            
            return Ok(ApiResponse<object>.SuccessResponse(null, 204, "Category Deleted Successfully."));
        }
    
    }
}
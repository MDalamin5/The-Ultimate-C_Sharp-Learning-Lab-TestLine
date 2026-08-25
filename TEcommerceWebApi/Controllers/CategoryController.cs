using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TEcommerceWebApi.DTOs;
using TEcommerceWebApi.Models;
using TEcommerceWebApi.Services;

namespace TEcommerceWebApi.Controllers
{
    [ApiController]
    [Route("/api/v2/categories")]
    public class CategoryController: ControllerBase
    {
        public CategoryService _categoryService;
        
        public CategoryController(CategoryService categoryService)
        {
            _categoryService = categoryService;
        }


        // Read all categories
        [HttpGet]
        public IActionResult GetCategories([FromQuery] string searchVale = "")
        {
            // Data Binding With Read Dto
            var responseCategory = _categoryService.GetAllCategory();

            return Ok(ApiResponse<List<CategoryReadDto>>.SuccessResponse(responseCategory, 200, "Category Returned Successfully."));
        }

        //Read a category byId
        /*
        [HttpGet("{categoryId:guid}")]
        public IActionResult GetCategoryById(Guid categoryId)
        {
            var foundCategory = categories.FirstOrDefault(category => category.CategoryId == categoryId);
            if(foundCategory == null)
                return NotFound(ApiResponse<object>.ErrorResponse(new List<string>{"Category not found with this id."}, 400, "Validation Invalid."));

            var responseCategory = new CategoryReadDto
            {
                CategoryId = foundCategory.CategoryId,
                Name = foundCategory.Name,
                Description = foundCategory.Description,
                CreatedAt = foundCategory.CreatedAt
            };

            return Ok(ApiResponse<CategoryReadDto>.SuccessResponse(responseCategory, 200, "Category founded."));
        }

        // Create Category
        [HttpPost]
        public IActionResult CreateCategory([FromBody] CategoryCreateDto categoryData)
        {

            var newCategory = new Category
            {
                CategoryId = Guid.NewGuid(),
                Name = categoryData.Name,
                Description = categoryData.Description,
                CreatedAt = DateTime.UtcNow
            };

            categories.Add(newCategory);

            //Return Data followed by CategoryReadDto
            var responseCreateCategory = new CategoryReadDto
            {
                CategoryId = newCategory.CategoryId,
                Name = newCategory.Name,
                Description = newCategory.Description,
                CreatedAt = newCategory.CreatedAt
            };

            return Created(nameof(GetCategoryById), ApiResponse<CategoryReadDto>.SuccessResponse(responseCreateCategory, 201, "Category Created Successfully."));
        }


        // update a Category
        [HttpPut("{categoryId:guid}")]
        public IActionResult UpdateCategoryById(Guid categoryId, [FromBody] CategoryUpdateDto categoryData)
        {
            var foundCategory = categories.FirstOrDefault(category => category.CategoryId == categoryId);

            if(foundCategory == null)
                return NotFound(ApiResponse<object>.ErrorResponse(new List<string>{"category is not found with this id."}, 400, "Validation Failed."));
            
            
            //Assuming the Name is not empty and the descriptions must gater then 10 char.
            foundCategory.Name = categoryData.Name;
            foundCategory.Description = categoryData.Description;

            return Ok(ApiResponse<object>.SuccessResponse(null, 204, "Category Updated successfully."));
            

        }






        // delete category by ID
        [HttpDelete("{categoryId:guid}")]
        public IActionResult DeleteCategoryById(Guid categoryId)
        {
            var foundCategory = categories.FirstOrDefault(category => category.CategoryId == categoryId);
            if(foundCategory == null)
            {
                return NotFound($"This: {categoryId} is not Exist.");
            }
            categories.Remove(foundCategory);
            return Ok(ApiResponse<object>.SuccessResponse(null, 204, "Category Deleted Successfully."));
        }
        */
    }
}
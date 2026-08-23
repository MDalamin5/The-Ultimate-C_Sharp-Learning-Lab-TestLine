using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TEcommerceWebApi.DTOs;
using TEcommerceWebApi.Models;

namespace TEcommerceWebApi.Controllers
{
    [ApiController]
    [Route("/api/v2/categories")]
    public class CategoryController: ControllerBase
    {
        private static List<Category> categories = new List<Category>();


        // Read all categories
        [HttpGet]
        public IActionResult GetCategories([FromQuery] string searchVale = "")
        {
            if (!string.IsNullOrEmpty(searchVale))
            {
                var srcCategory = categories.Where(c => c.Name.Contains(searchVale, StringComparison.OrdinalIgnoreCase)).ToList();
                return Ok(srcCategory);
            }

            // Data Binding With Read Dto
            var responseCategory = categories.Select(c => new CategoryReadDto
            {
                CategoryId = c.CategoryId,
                Name = c.Name,
                Description = c.Description,
                CreatedAt = c.CreatedAt
            }).ToList();

            return Ok(responseCategory);
        }


        // Create Category
        [HttpPost]
        public IActionResult CreateCategory([FromBody] CategoryCreateDto categoryData)
        {
            if (string.IsNullOrEmpty(categoryData.Name))
            {
                return BadRequest("Category Name is Required.");
            }
            if(categoryData.Name.Length < 2)
            {
                return BadRequest("Category name must be Gater then 2 Char.");
            }

            var newCategory = new Category
            {
                CategoryId = Guid.NewGuid(),
                Name = categoryData.Name,
                Description = categoryData.Description,
                CreatedAt = DateTime.UtcNow
            };

            categories.Add(newCategory);

            return Created($"/api/v2/categories/{newCategory.CategoryId}", newCategory);
        }


        // update a Category
        [HttpPut("{categoryId:guid}")]
        public IActionResult UpdateCategoryById(Guid categoryId, [FromBody] CategoryUpdateDto categoryData)
        {
            var foundCategory = categories.FirstOrDefault(category => category.CategoryId == categoryId);

            if(foundCategory == null)
                return NotFound($"This {categoryId} is not exists.");
            
            
            //Assuming the Name is not empty and the descriptions must gater then 10 char.
            foundCategory.Name = categoryData.Name;
            foundCategory.Description = categoryData.Description;

            return NoContent();
            

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
            return NoContent();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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
            return Ok(categories);
        }


        // Create Category
        [HttpPost]
        public IActionResult CreateCategory([FromBody] Category categoryData)
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
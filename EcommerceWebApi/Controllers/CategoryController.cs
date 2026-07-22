using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceWebApi.Controllers
{   
    [ApiController]
    [Route("/api/v1/categories/")]
    public class CategoryController: ControllerBase
    {
        private static List<Category> categories = new List<Category>();
        // TO read teh category => api/v1/categories
        [HttpGet]
        public IActionResult GetCategories([FromQuery] string searchValue = "")
        {
            if (!string.IsNullOrEmpty(searchValue))
            {
                var searchCategories = categories.Where(c => c.Name.Contains(searchValue, StringComparison.OrdinalIgnoreCase)).ToList();

                return Ok(searchCategories);
            }
            else
                return Ok(categories);
        }


        // To Create a categories => POST: api/v1/categories
        [HttpPost]
        public IActionResult CreateCategories([FromBody] Category categoryData)
        {
            if(string.IsNullOrEmpty(categoryData.Name))
                return BadRequest("Categories Name is required.");

            var newCategory = new Category
            {
                CategoryId = Guid.NewGuid(),
                Name = categoryData.Name,
                Description = categoryData.Description,
                CreatedAt = DateTime.UtcNow
            };

            categories.Add(newCategory);
            return Created($"/api/v1/categories/{newCategory.CategoryId}", newCategory);
        }


        // update the categories value: PUT: api/v1/categories/{categoryId}
        [HttpPut("{categoryId: guid}")]
        public IActionResult UpdateCategoryById(Guid categoryId)
        {
            var foundCategory = categories.FirstOrDefault(c => c.CategoryId == categoryId);
            if(foundCategory != null)
            {
                categories.Remove(foundCategory);
                return NoContent();
            }
            else
                return NotFound($"This {categoryId} not Found!!");
        }

    }
}
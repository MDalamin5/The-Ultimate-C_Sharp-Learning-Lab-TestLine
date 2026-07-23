using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EcommerceWebApi.Models;
using EcommerceWebApi.DTOs;

namespace EcommerceWebApi.Controllers
{   
    [ApiController]
    [Route("/api/v1/categories")]
    public class CategoryController:ControllerBase
    {
        private static List<Category> categories = new List<Category>();
        // TO read teh category => api/v1/categories
        [HttpGet]
        public IActionResult GetCategories([FromQuery] string searchValue = "")
        {
            // if (!string.IsNullOrEmpty(searchValue))
            // {
            //     var searchCategories = categories.Where(c => c.Name.Contains(searchValue, StringComparison.OrdinalIgnoreCase)).ToList();

            //     return Ok(searchCategories);
            // }
            // else
            //     return Ok(categories);

            var categoryList = categories.Select(c => new CategoryReadDto
            {
                Name = c.Name,
                Description = c.Description,
                CreatedAt = c.CreatedAt
            }).ToList();

            return Ok(categoryList);
        }


        // To Create a categories => POST: api/v1/categories
        [HttpPost]
        public IActionResult CreateCategories([FromBody] CategoryCreateDto categoryData)
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


        // update the categories value: Delete: api/v1/categories/{categoryId}
        [HttpDelete("{categoryId:guid}")]
        public IActionResult DeleteCategoryById(Guid categoryId)
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

        // update category data PUT: api/v1/categories/{categoryId}
        [HttpPut("{categoryId:guid}")]
        public IActionResult UpdateCategoryById(Guid categoryId, [FromBody] CategoryUpdateDto categoryData)
        {
            var foundCategory = categories.FirstOrDefault(c => c.CategoryId == categoryId);

            if (foundCategory != null)
            {
                if(!string.IsNullOrEmpty(categoryData.Name))
                    foundCategory.Name = categoryData.Name;
                if(!string.IsNullOrEmpty(categoryData.Description))
                    foundCategory.Description = categoryData.Description;

                return NoContent();
            }
            else
                return NotFound("Data not Found");
        }

    }
}
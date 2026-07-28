using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EcommerceWebApi.DTOs;
using EcommerceWebApi.Models;

namespace EcommerceWebApi.Services
{
    public class CategoryService
    {
        private static readonly List<Category> categories = new List<Category>();


        public List<CategoryReadDto> GetAllCategories()
        {
            return categories.Select(c => new CategoryReadDto
            {
                CategoryId = c.CategoryId,
                Name = c.Name,
                Description = c.Description,
                CreatedAt = c.CreatedAt
            }).ToList();
        }

        public CategoryReadDto GetCategoryById(Guid categoryId)
        {
            var foundCategory = categories.FirstOrDefault(c => c.CategoryId == categoryId);
            if(foundCategory == null)
                return null;
            else
            {
                var FoundCategory = new CategoryReadDto
                {
                    CategoryId = foundCategory.CategoryId,
                    Name = foundCategory.Name,
                    Description = foundCategory.Description,
                    CreatedAt = foundCategory.CreatedAt
                };
                return FoundCategory;
            }
        }
    }
}
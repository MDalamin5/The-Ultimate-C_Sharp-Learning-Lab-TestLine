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
        private static readonly List<Category> _categories = new List<Category>();


        public List<CategoryReadDto> GetAllCategories()
        {
            return _categories.Select(c => new CategoryReadDto
            {
                CategoryId = c.CategoryId,
                Name = c.Name,
                Description = c.Description,
                CreatedAt = c.CreatedAt
            }).ToList();
        }

        public CategoryReadDto? GetCategoryById(Guid categoryId)
        {
            var foundCategory = _categories.FirstOrDefault(c => c.CategoryId == categoryId);
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

        public CategoryReadDto CreateCategory(CategoryCreateDto categoryData)
        {
            
            var newCategory = new Category
            {
                CategoryId = Guid.NewGuid(),
                Name = categoryData.Name,
                Description = categoryData.Description,
                CreatedAt = DateTime.UtcNow
            };

            _categories.Add(newCategory);

            var categoryReadDto = new CategoryReadDto
            {
              CategoryId = newCategory.CategoryId,
              Name = newCategory.Name,
              Description = newCategory.Description,
              CreatedAt = newCategory.CreatedAt  
            };
            return categoryReadDto;
        }

        public CategoryReadDto? UpdateCategory(Guid categoryId, CategoryUpdateDto categoryData)
        {
            var foundCategory = _categories.FirstOrDefault(c => c.CategoryId == categoryId);

            if (foundCategory == null)
                return null;
            
            
            return new CategoryReadDto
            {
                CategoryId = foundCategory.CategoryId,
                Name = foundCategory.Name,
                Description = foundCategory.Description,
                CreatedAt = foundCategory.CreatedAt
            };
        }
    }
}
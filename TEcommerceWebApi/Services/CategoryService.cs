using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TEcommerceWebApi.DTOs;
using TEcommerceWebApi.Models;

namespace TEcommerceWebApi.Services
{
    public class CategoryService
    {

        private static readonly List<Category> categories = new List<Category>();
        public List<CategoryReadDto> GetAllCategory()
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
            var foundCategory = categories.FirstOrDefault(category => category.CategoryId == categoryId);
            if(foundCategory == null)
                return null;

            return new CategoryReadDto
                {
                    CategoryId = foundCategory.CategoryId,
                    Name = foundCategory.Name,
                    Description = foundCategory.Description,
                    CreatedAt = foundCategory.CreatedAt
                };
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

            categories.Add(newCategory);

            //Return Data followed by CategoryReadDto
            return new CategoryReadDto
            {
                CategoryId = newCategory.CategoryId,
                Name = newCategory.Name,
                Description = newCategory.Description,
                CreatedAt = newCategory.CreatedAt
            };
        }


        public bool UpdateCategory(Guid categoryId, CategoryUpdateDto categoryData)
        {
            var foundCategory = categories.FirstOrDefault(category => category.CategoryId == categoryId);

            if(foundCategory == null)
                return false;
            
            
            //Assuming the Name is not empty and the descriptions must gater then 10 char.
            foundCategory.Name = categoryData.Name;
            foundCategory.Description = categoryData.Description;
            return true;
        }

        public bool DeleteCategoryById(Guid categoryId)
        {
            var foundCategory = categories.FirstOrDefault(category => category.CategoryId == categoryId);
            if(foundCategory == null)
                return false;
            
            categories.Remove(foundCategory);
            return true;
        }
    }
}
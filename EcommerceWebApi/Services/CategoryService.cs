using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EcommerceWebApi.DTOs;
using EcommerceWebApi.Interfaces;
using EcommerceWebApi.Models;
using EcommerceWebApi.Profies;

namespace EcommerceWebApi.Services
{
    public class CategoryService: ICategoryService
    {
        private static readonly List<Category> _categories = new List<Category>();
        private readonly IMapper _mapper;

        public CategoryService(IMapper mapper)
        {
            _mapper = mapper;
        }


        public List<CategoryReadDto> GetAllCategories()
        {
            // Before Mapping.
            // return _categories.Select(c => new CategoryReadDto
            // {
            //     CategoryId = c.CategoryId,
            //     Name = c.Name,
            //     Description = c.Description,
            //     CreatedAt = c.CreatedAt
            // }).ToList();

            //After Mapping. All category data map to CategoryReadDto and return.

            return _mapper.Map<List<CategoryReadDto>>(_categories);
        }

        public CategoryReadDto? GetCategoryById(Guid categoryId)
        {
            var foundCategory = _categories.FirstOrDefault(c => c.CategoryId == categoryId);
            if(foundCategory == null)
                return null;
            else
            {
                // var FoundCategory = new CategoryReadDto
                // {
                //     CategoryId = foundCategory.CategoryId,
                //     Name = foundCategory.Name,
                //     Description = foundCategory.Description,
                //     CreatedAt = foundCategory.CreatedAt
                // };
                // return FoundCategory;

                return _mapper.Map<CategoryReadDto>(_categories);
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
            
            foundCategory.Name = categoryData.Name;
            foundCategory.Description = categoryData.Description;
            
            return new CategoryReadDto
            {
                CategoryId = foundCategory.CategoryId,
                Name = foundCategory.Name,
                Description = foundCategory.Description,
                CreatedAt = foundCategory.CreatedAt
            };
        }

        public bool DeleteCategoryById(Guid categoryId)
        {
            var foundCategory = _categories.FirstOrDefault(c => c.CategoryId == categoryId);

            if (foundCategory == null)
                return false;

            _categories.Remove(foundCategory);
            return true;
        }
    }
}
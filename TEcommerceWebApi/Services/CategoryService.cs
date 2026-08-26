using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TEcommerceWebApi.DTOs;
using TEcommerceWebApi.Models;
using TEcommerceWebApi.Interfaces;
using TEcommerceWebApi.Profiles;
using AutoMapper;

namespace TEcommerceWebApi.Services
{
    public class CategoryService: ICategoryService
    {

        private readonly IMapper _mapper;

        public CategoryService(IMapper mapper)
        {
            _mapper = mapper;
        }

        private static readonly List<Category> _categories = new List<Category>();
        public List<CategoryReadDto> GetAllCategory()
        {

            return _mapper.Map<List<CategoryReadDto>>(_categories);

            // using without mapper.
            /*
            return categories.Select(c => new CategoryReadDto
            {
                CategoryId = c.CategoryId,
                Name = c.Name,
                Description = c.Description,
                CreatedAt = c.CreatedAt
            }).ToList();
            */
        }

        public CategoryReadDto? GetCategoryById(Guid categoryId)
        {
            var foundCategory = _categories.FirstOrDefault(category => category.CategoryId == categoryId);
            if(foundCategory == null)
                return null;

            return _mapper.Map<CategoryReadDto>(foundCategory);

            // without using the Mapper.
            // return new CategoryReadDto
            //     {
            //         CategoryId = foundCategory.CategoryId,
            //         Name = foundCategory.Name,
            //         Description = foundCategory.Description,
            //         CreatedAt = foundCategory.CreatedAt
            //     };
        }

        public CategoryReadDto CreateCategory(CategoryCreateDto categoryData)
        {
            // var newCategory = new Category
            // {
            //     CategoryId = Guid.NewGuid(),
            //     Name = categoryData.Name,
            //     Description = categoryData.Description,
            //     CreatedAt = DateTime.UtcNow
            // };

            var newCategory = _mapper.Map<Category>(categoryData);
            newCategory.CategoryId = Guid.NewGuid();
            newCategory.CreatedAt = DateTime.UtcNow;

            _categories.Add(newCategory);

            //return via mapper
            return _mapper.Map<CategoryReadDto>(newCategory);

            //Return Data followed by CategoryReadDto
            // return new CategoryReadDto
            // {
            //     CategoryId = newCategory.CategoryId,
            //     Name = newCategory.Name,
            //     Description = newCategory.Description,
            //     CreatedAt = newCategory.CreatedAt
            // };

        }


        public CategoryReadDto? UpdateCategory(Guid categoryId, CategoryUpdateDto categoryData)
        {
            var foundCategory = _categories.FirstOrDefault(category => category.CategoryId == categoryId);

            if(foundCategory == null)
                return null;
            
            
            //Assuming the Name is not empty and the descriptions must gater then 10 char.
            // foundCategory.Name = categoryData.Name;
            // foundCategory.Description = categoryData.Description;

            //using mapper categoryUpdateDto -> category
            _mapper.Map(categoryData, foundCategory);
            return _mapper.Map<CategoryReadDto>(foundCategory);
        }

        public bool DeleteCategoryById(Guid categoryId)
        {
            var foundCategory = _categories.FirstOrDefault(category => category.CategoryId == categoryId);
            if(foundCategory == null)
                return false;
            
            _categories.Remove(foundCategory);
            return true;
        }
    }
}
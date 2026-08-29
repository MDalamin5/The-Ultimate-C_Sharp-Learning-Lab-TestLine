using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TEcommerceWebApi.DTOs;
using TEcommerceWebApi.data;
using AutoMapper.Configuration.Conventions;
using Microsoft.AspNetCore.Http.HttpResults;
using TEcommerceWebApi.Models;

namespace TEcommerceWebApi.Services
{
    public class ProductService
    {
        private readonly AppDbContext _appDbContext;

        public ProductService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<ProductReadDto> CreateProduct(ProductCreateDto productData)
        {
            var category = await _appDbContext.Categories.FindAsync(productData.CategoryId);
            
            if (category == null)
            {
                return null;
            }

            var newProduct = new Product
            {
                ProductId = Guid.NewGuid(),
                Name = productData.Name,
                Price = productData.Price,
                CategoryId = productData.CategoryId
            };

            await _appDbContext.Products.AddAsync(newProduct);
            await _appDbContext.SaveChangesAsync();

            var responseProduct = new ProductReadDto
            {
                ProductId = newProduct.ProductId,
                Name = newProduct.Name,
                Price = newProduct.Price,
                CategoryId = newProduct.CategoryId,
                CategoryName = category.Name
            };

            return responseProduct;
        }
    }
}
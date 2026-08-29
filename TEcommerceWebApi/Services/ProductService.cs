using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TEcommerceWebApi.DTOs;
using TEcommerceWebApi.data;
using AutoMapper.Configuration.Conventions;
using Microsoft.AspNetCore.Http.HttpResults;
using TEcommerceWebApi.Models;
using Microsoft.EntityFrameworkCore;
using TEcommerceWebApi.Interfaces;



namespace TEcommerceWebApi.Services
{
    public class ProductService: IProductService
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

        public async Task<List<ProductReadDto>> GetAllProducts()
        {
            var allProducts = await _appDbContext.Products
            .AsNoTracking()
            .Select(p => new ProductReadDto
            {
                ProductId = p.ProductId,
                Name = p.Name,
                Price = p.Price,
                CategoryId = p.CategoryId,
                CategoryName = p.Category != null ? p.Category.Name : string.Empty
            }).ToListAsync();

            if (allProducts == null){
                return null;
            }
            
            return allProducts;
        }
    }
}
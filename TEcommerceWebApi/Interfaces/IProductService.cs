using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TEcommerceWebApi.DTOs;
using TEcommerceWebApi.Controllers;

namespace TEcommerceWebApi.Interfaces
{
    public interface IProductService
    {
        Task<ProductReadDto?> CreateProduct(ProductCreateDto productData);
        Task<PaginatedResult<ProductReadDto>> GetAllProducts(int pageNumber, int pageSize, string? searchValue = null);
    }
}
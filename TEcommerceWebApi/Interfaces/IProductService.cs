using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TEcommerceWebApi.DTOs;
using TEcommerceWebApi.Controllers;
using TEcommerceWebApi.Helpers;

namespace TEcommerceWebApi.Interfaces
{
    public interface IProductService
    {
        Task<ProductReadDto?> CreateProduct(ProductCreateDto productData);
        Task<PaginatedResult<ProductReadDto>> GetAllProducts(QueryParameters queryParameters);
    }
}
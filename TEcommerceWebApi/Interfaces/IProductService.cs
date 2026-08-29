using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TEcommerceWebApi.DTOs;

namespace TEcommerceWebApi.Interfaces
{
    public interface IProductService
    {
        Task<ProductReadDto> CreateProduct(ProductCreateDto productData);
        Task<List<ProductReadDto>> GetAllProducts();
    }
}
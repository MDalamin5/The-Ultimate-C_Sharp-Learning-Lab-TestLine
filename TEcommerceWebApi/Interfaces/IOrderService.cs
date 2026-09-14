using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TEcommerceWebApi.DTOs;
using TEcommerceWebApi.Enums;

namespace TEcommerceWebApi.Interfaces
{
    public interface IOrderService
    {
        Task<List<OrderReadDto>?> GetOrdersByUserIdAsync(Guid userId, OrderStatus? status = null);
    }
}
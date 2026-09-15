using System;
using System.Threading.Tasks;
using TEcommerceWebApi.DTOs;
using TEcommerceWebApi.Helpers;
using TEcommerceWebApi.Controllers;

namespace TEcommerceWebApi.Interfaces
{
    public interface IUserService
    {
        Task<PaginatedResult<UserReadDto>> GetAllUsersAsync(QueryParameters queryParameters);
        Task<UserReadDto?> GetUserByIdAsync(Guid userId);
        Task<UserReadDto?> CreateUserAsync(UserCreateDto userData);
        Task<UserReadDto?> UpdateUserAsync(Guid userId, UserUpdateDto updateData);
        Task<bool> DeleteUserAsync(Guid userId);
        Task<bool> IsEmailTakenAsync(string email, Guid? excludeUserId = null);
    }
}
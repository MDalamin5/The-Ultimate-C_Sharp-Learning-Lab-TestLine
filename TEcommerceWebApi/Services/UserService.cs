using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TEcommerceWebApi.data;
using TEcommerceWebApi.DTOs;
using TEcommerceWebApi.Helpers;
using TEcommerceWebApi.Interfaces;
using TEcommerceWebApi.Models;
using TEcommerceWebApi.Controllers;

namespace TEcommerceWebApi.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public UserService(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        // Helper to check email uniqueness (case-insensitive)
        public async Task<bool> IsEmailTakenAsync(string email, Guid? excludeUserId = null)
        {
            var emailLower = email.Trim().ToLower();
            var query = _appDbContext.Users.AsNoTracking();

            if (excludeUserId.HasValue)
            {
                query = query.Where(u => u.UserId != excludeUserId.Value);
            }

            return await query.AnyAsync(u => u.Email.ToLower() == emailLower);
        }

        public async Task<UserReadDto?> CreateUserAsync(UserCreateDto userData)
        {
            // 1. Check if Email already exists
            if (await IsEmailTakenAsync(userData.Email))
            {
                return null; // Signals duplicate email to controller
            }

            // 2. Map & Create User
            var newUser = _mapper.Map<User>(userData);
            newUser.UserId = Guid.NewGuid();
            newUser.CreatedAt = DateTime.UtcNow;

            await _appDbContext.Users.AddAsync(newUser);
            await _appDbContext.SaveChangesAsync();

            return _mapper.Map<UserReadDto>(newUser);
        }

        public async Task<UserReadDto?> GetUserByIdAsync(Guid userId)
        {
            return await _appDbContext.Users
                .AsNoTracking()
                .Where(u => u.UserId == userId)
                .Select(u => new UserReadDto
                {
                    UserId = u.UserId,
                    Email = u.Email,
                    FullName = u.FullName,
                    CreatedAt = u.CreatedAt,
                    TotalOrdersCount = u.Orders.Count()
                })
                .FirstOrDefaultAsync();
        }

        public async Task<PaginatedResult<UserReadDto>> GetAllUsersAsync(QueryParameters queryParameters)
        {
            var query = _appDbContext.Users
                .AsNoTracking()
                .AsQueryable();

            // Search by Name or Email
            if (!string.IsNullOrWhiteSpace(queryParameters.SearchValue))
            {
                var search = $"%{queryParameters.SearchValue.Trim()}%";
                query = query.Where(u => EF.Functions.ILike(u.FullName, search) || EF.Functions.ILike(u.Email, search));
            }

            // Default Sort by CreatedAt Descending
            query = query.OrderByDescending(u => u.CreatedAt);

            var totalCount = await query.CountAsync();

            var users = await query
                .Skip((queryParameters.PageNumber - 1) * queryParameters.PageSize)
                .Take(queryParameters.PageSize)
                .Select(u => new UserReadDto
                {
                    UserId = u.UserId,
                    Email = u.Email,
                    FullName = u.FullName,
                    CreatedAt = u.CreatedAt,
                    TotalOrdersCount = u.Orders.Count()
                })
                .ToListAsync();

            return new PaginatedResult<UserReadDto>
            {
                Items = users,
                TotalCount = totalCount,
                PageNumber = queryParameters.PageNumber,
                PageSize = queryParameters.PageSize
            };
        }

        public async Task<UserReadDto?> UpdateUserAsync(Guid userId, UserUpdateDto updateData)
        {
            var user = await _appDbContext.Users.FindAsync(userId);
            if (user == null) return null;

            // Check if email is taken by another user
            if (await IsEmailTakenAsync(updateData.Email, userId))
            {
                throw new InvalidOperationException($"The email '{updateData.Email}' is already in use by another account.");
            }

            user.FullName = updateData.FullName;
            user.Email = updateData.Email;

            await _appDbContext.SaveChangesAsync();

            return _mapper.Map<UserReadDto>(user);
        }

        public async Task<bool> DeleteUserAsync(Guid userId)
        {
            var user = await _appDbContext.Users.FindAsync(userId);
            if (user == null) return false;

            // Delete will be restricted by PostgreSQL if Orders exist for this user!
            _appDbContext.Users.Remove(user);
            await _appDbContext.SaveChangesAsync();
            return true;
        }
    }
}
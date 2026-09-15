using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TEcommerceWebApi.DTOs;
using TEcommerceWebApi.Helpers;
using TEcommerceWebApi.Interfaces;

namespace TEcommerceWebApi.Controllers
{
    [ApiController]
    [Route("/api/v2/users")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<UserReadDto>>> RegisterUser([FromBody] UserCreateDto userData)
        {
            var createdUser = await _userService.CreateUserAsync(userData);

            if (createdUser == null)
            {
                return Conflict(ApiResponse<object>.ErrorResponse(
                    new List<string> { $"User with email '{userData.Email}' already exists." },
                    409,
                    "Duplicate Email"
                ));
            }

            return CreatedAtAction(
                nameof(GetUserById), 
                new { userId = createdUser.UserId }, 
                ApiResponse<UserReadDto>.SuccessResponse(createdUser, 201, "User registered successfully.")
            );
        }

        [HttpGet("{userId:guid}")]
        public async Task<ActionResult<ApiResponse<UserReadDto>>> GetUserById(Guid userId)
        {
            var user = await _userService.GetUserByIdAsync(userId);

            if (user == null)
            {
                return NotFound(ApiResponse<object>.ErrorResponse(
                    new List<string> { $"User with ID '{userId}' was not found." },
                    404,
                    "User Not Found"
                ));
            }

            return Ok(ApiResponse<UserReadDto>.SuccessResponse(user, 200, "User profile retrieved."));
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<PaginatedResult<UserReadDto>>>> GetAllUsers([FromQuery] QueryParameters queryParameters)
        {
            queryParameters.Validate();
            var users = await _userService.GetAllUsersAsync(queryParameters);
            return Ok(ApiResponse<PaginatedResult<UserReadDto>>.SuccessResponse(users, 200, "Users retrieved successfully."));
        }

        [HttpPut("{userId:guid}")]
        public async Task<ActionResult<ApiResponse<UserReadDto>>> UpdateUser(Guid userId, [FromBody] UserUpdateDto updateData)
        {
            try
            {
                var updatedUser = await _userService.UpdateUserAsync(userId, updateData);
                if (updatedUser == null)
                {
                    return NotFound(ApiResponse<object>.ErrorResponse(
                        new List<string> { $"User with ID '{userId}' was not found." },
                        404,
                        "Update Failed"
                    ));
                }

                return Ok(ApiResponse<UserReadDto>.SuccessResponse(updatedUser, 200, "User updated successfully."));
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ApiResponse<object>.ErrorResponse(
                    new List<string> { ex.Message },
                    409,
                    "Email Conflict"
                ));
            }
        }

        [HttpDelete("{userId:guid}")]
        public async Task<ActionResult<ApiResponse<object>>> DeleteUser(Guid userId)
        {
            var deleted = await _userService.DeleteUserAsync(userId);
            if (!deleted)
            {
                return NotFound(ApiResponse<object>.ErrorResponse(
                    new List<string> { $"User with ID '{userId}' was not found." },
                    404,
                    "Delete Failed"
                ));
            }

            return Ok(ApiResponse<object>.SuccessResponse(null, 200, "User deleted successfully."));
        }
    }
}
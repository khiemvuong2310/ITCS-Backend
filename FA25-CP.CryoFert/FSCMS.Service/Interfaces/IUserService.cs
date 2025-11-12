using FSCMS.Service.ReponseModel;
using FSCMS.Service.RequestModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSCMS.Service.Interfaces
{
    public interface IUserService
    {
        /// <summary>
        /// Get user by ID
        /// </summary>
        Task<BaseResponse<UserResponse>> GetUserByIdAsync(Guid userId);

        /// <summary>
        /// Get user by email
        /// </summary>
        /// <param name="email">The email address of the user</param>
        /// <returns>BaseResponse containing user information</returns>
        Task<BaseResponse<UserResponse>> GetUserByEmailAsync(string email);

        /// <summary>
        /// Get users by username (search by name)
        /// </summary>
        Task<BaseResponse<List<UserResponse>>> GetUsersByNameAsync(string userName);

        /// <summary>
        /// Get detailed user information by ID
        /// </summary>
        Task<BaseResponse<UserDetailResponse>> GetUserDetailByIdAsync(Guid userId);

        /// <summary>
        /// Get all users with pagination and filtering
        /// </summary>
        Task<DynamicResponse<UserResponse>> GetAllUsersAsync(GetUsersRequest request);

        /// <summary>
        /// Create new user
        /// </summary>
        Task<BaseResponse<UserResponse>> CreateUserAsync(CreateUserRequest request);

        /// <summary>
        /// Update existing user
        /// </summary>
        Task<BaseResponse<UserResponse>> UpdateUserAsync(Guid userId, UpdateUserRequest request);

        /// <summary>
        /// Delete user (soft delete)
        /// </summary>
        Task<BaseResponse> DeleteUserAsync(Guid userId);

        /// <summary>
        /// Check if email exists
        /// </summary>
        Task<bool> EmailExistsAsync(string email);

        /// <summary>
        /// Verify user email
        /// </summary>
        Task<BaseResponse> VerifyUserEmailAsync(Guid userId);

        /// <summary>
        /// Update user status
        /// </summary>
        Task<BaseResponse> UpdateUserStatusAsync(Guid userId, bool status);
    }
}

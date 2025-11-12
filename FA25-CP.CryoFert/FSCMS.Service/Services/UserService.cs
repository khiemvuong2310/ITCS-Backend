using AutoMapper;
using FSCMS.Core.Entities;
using FSCMS.Core.Enum;
using FSCMS.Data.UnitOfWork;
using FSCMS.Service.Interfaces;
using FSCMS.Service.ReponseModel;
using FSCMS.Service.RequestModel;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSCMS.Service.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<UserService> _logger;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<UserService> logger)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Get user by ID
        /// </summary>
        /// <param name="userId">The unique identifier of the user</param>
        /// <returns>BaseResponse containing user information</returns>
        public async Task<BaseResponse<UserResponse>> GetUserByIdAsync(Guid userId)
        {
            const string methodName = nameof(GetUserByIdAsync);
            _logger.LogInformation("{MethodName} called with userId: {UserId}", methodName, userId);

            try
            {
                // Input validation
                if (userId == Guid.Empty)
                {
                    _logger.LogWarning("{MethodName}: Invalid user ID provided - {UserId}", methodName, userId);
                    return new BaseResponse<UserResponse>
                    {
                        Code = StatusCodes.Status400BadRequest,
                        SystemCode = "INVALID_USER_ID",
                        Message = "User ID cannot be empty or invalid",
                        Data = null
                    };
                }

                var account = await _unitOfWork.Repository<Account>()
                    .AsQueryable()
                    .AsNoTracking()
                    .Include(u => u.Role)
                    .Where(u => u.Id == userId && !u.IsDeleted)
                    .FirstOrDefaultAsync();

                if (account == null)
                {
                    _logger.LogWarning("{MethodName}: User not found with ID: {UserId}", methodName, userId);
                    return new BaseResponse<UserResponse>
                    {
                        Code = StatusCodes.Status404NotFound,
                        SystemCode = "USER_NOT_FOUND",
                        Message = "User not found",
                        Data = null
                    };
                }

                var userResponse = _mapper.Map<UserResponse>(account);
                _logger.LogInformation("{MethodName}: Successfully retrieved user {UserId}", methodName, userId);

                return new BaseResponse<UserResponse>
                {
                    Code = StatusCodes.Status200OK,
                    SystemCode = "SUCCESS",
                    Message = "User retrieved successfully",
                    Data = userResponse
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName}: Error retrieving user {UserId}", methodName, userId);
                return new BaseResponse<UserResponse>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    SystemCode = "INTERNAL_ERROR",
                    Message = "An internal error occurred while retrieving the user",
                    Data = null
                };
            }
        }

        /// <summary>
        /// Get user by email
        /// </summary>
        /// <param name="email">The email address of the user</param>
        /// <returns>BaseResponse containing user information</returns>
        public async Task<BaseResponse<UserResponse>> GetUserByEmailAsync(string email)
        {
            const string methodName = nameof(GetUserByEmailAsync);
            _logger.LogInformation("{MethodName} called with email: {Email}", methodName, email);

            try
            {
                // Input validation
                if (string.IsNullOrWhiteSpace(email))
                {
                    _logger.LogWarning("{MethodName}: Email is null or empty", methodName);
                    return new BaseResponse<UserResponse>
                    {
                        Code = StatusCodes.Status400BadRequest,
                        SystemCode = "INVALID_EMAIL",
                        Message = "Email address is required",
                        Data = null
                    };
                }

                var normalizedEmail = email.Trim().ToLowerInvariant();

                var account = await _unitOfWork.Repository<Account>()
                    .AsQueryable()
                    .AsNoTracking()
                    .Include(u => u.Role)
                    .Where(u => !u.IsDeleted && u.Email.ToLower() == normalizedEmail)
                    .FirstOrDefaultAsync();

                if (account == null)
                {
                    _logger.LogWarning("{MethodName}: User not found with email: {Email}", methodName, email);
                    return new BaseResponse<UserResponse>
                    {
                        Code = StatusCodes.Status404NotFound,
                        SystemCode = "USER_NOT_FOUND",
                        Message = "User not found",
                        Data = null
                    };
                }

                var userResponse = _mapper.Map<UserResponse>(account);
                _logger.LogInformation("{MethodName}: Successfully retrieved user by email", methodName);

                return new BaseResponse<UserResponse>
                {
                    Code = StatusCodes.Status200OK,
                    SystemCode = "SUCCESS",
                    Message = "User retrieved successfully",
                    Data = userResponse
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName}: Error retrieving user by email: {Email}", methodName, email);
                return new BaseResponse<UserResponse>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    SystemCode = "INTERNAL_ERROR",
                    Message = "An internal error occurred while retrieving the user",
                    Data = null
                };
            }
        }

        /// <summary>
        /// Get users by username (search by name) - Currently not applicable as Account doesn't have UserName
        /// </summary>
        public async Task<BaseResponse<List<UserResponse>>> GetUsersByNameAsync(string userName)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(userName))
                {
                    return new BaseResponse<List<UserResponse>>
                    {
                        Code = StatusCodes.Status400BadRequest,
                        Message = "Username is required",
                        Data = null
                    };
                }

                // Search by email instead since Account doesn't have UserName
                var accounts = await _unitOfWork.Repository<Account>()
                    .AsQueryable()
                    .Include(u => u.Role)
                    .Where(u => u.Email.Contains(userName) && !u.IsDeleted)
                    .ToListAsync();

                if (accounts == null || !accounts.Any())
                {
                    return new BaseResponse<List<UserResponse>>
                    {
                        Code = StatusCodes.Status404NotFound,
                        Message = "No users found with the specified name",
                        Data = new List<UserResponse>()
                    };
                }

                return new BaseResponse<List<UserResponse>>
                {
                    Code = StatusCodes.Status200OK,
                    Message = $"Found {accounts.Count} user(s)",
                    Data = _mapper.Map<List<UserResponse>>(accounts)
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<List<UserResponse>>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Message = $"An error occurred: {ex.Message}",
                    Data = null
                };
            }
        }

        /// <summary>
        /// Get detailed user information by ID
        /// </summary>
        public async Task<BaseResponse<UserDetailResponse>> GetUserDetailByIdAsync(Guid userId)
        {
            try
            {
                if (userId == null)
                {
                    return new BaseResponse<UserDetailResponse>
                    {
                        Code = StatusCodes.Status400BadRequest,
                        Message = "Invalid user ID",
                        Data = null
                    };
                }

                var account = await _unitOfWork.Repository<Account>()
                    .AsQueryable()
                    .Include(u => u.Role)
                    .Include(u => u.Patient)
                    .Include(u => u.Doctor)
                    .Where(u => u.Id == userId && !u.IsDeleted)
                    .FirstOrDefaultAsync();

                if (account == null)
                {
                    return new BaseResponse<UserDetailResponse>
                    {
                        Code = StatusCodes.Status404NotFound,
                        Message = "User not found",
                        Data = null
                    };
                }

                return new BaseResponse<UserDetailResponse>
                {
                    Code = StatusCodes.Status200OK,
                    Message = "User details retrieved successfully",
                    Data = _mapper.Map<UserDetailResponse>(account)
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<UserDetailResponse>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Message = $"An error occurred: {ex.Message}",
                    Data = null
                };
            }
        }

        /// <summary>
        /// Get all users with pagination and filtering
        /// </summary>
        public async Task<DynamicResponse<UserResponse>> GetAllUsersAsync(GetUsersRequest request)
        {
            try
            {
                var query = _unitOfWork.Repository<Account>()
                    .AsQueryable()
                    .Include(u => u.Role)
                    .Where(u => !u.IsDeleted);

                // Apply filters
                if (!string.IsNullOrWhiteSpace(request.SearchTerm))
                {
                    query = query.Where(u =>
                        u.Email.Contains(request.SearchTerm) ||
                        (u.Phone != null && u.Phone.Contains(request.SearchTerm)));
                }

                if (request.RoleId.HasValue)
                {
                    query = query.Where(u => u.RoleId == request.RoleId.Value);
                }

                if (request.Status.HasValue)
                {
                    query = query.Where(u => u.IsActive == request.Status.Value);
                }

                if (request.EmailVerified.HasValue)
                {
                    query = query.Where(u => u.IsVerified == request.EmailVerified.Value);
                }

                // Get total count
                var totalCount = await query.CountAsync();

                // Apply sorting
                if (!string.IsNullOrWhiteSpace(request.Sort))
                {
                    var isDescending = request.Order?.ToLower() == "desc";

                    query = request.Sort.ToLower() switch
                    {
                        "email" => isDescending ? query.OrderByDescending(u => u.Email) : query.OrderBy(u => u.Email),
                        "createdat" => isDescending ? query.OrderByDescending(u => u.CreatedAt) : query.OrderBy(u => u.CreatedAt),
                        _ => isDescending ? query.OrderByDescending(u => u.Id) : query.OrderBy(u => u.Id)
                    };
                }
                else
                {
                    query = query.OrderByDescending(u => u.CreatedAt);
                }

                // Apply pagination
                var accounts = await query
                    .Skip((request.Page - 1) * request.Size)
                    .Take(request.Size)
                    .ToListAsync();

                return new DynamicResponse<UserResponse>
                {
                    Code = StatusCodes.Status200OK,
                    Message = "Users retrieved successfully",
                    MetaData = new PagingMetaData
                    {
                        Page = request.Page,
                        Size = request.Size,
                        Total = totalCount
                    },
                    Data = _mapper.Map<List<UserResponse>>(accounts)
                };
            }
            catch (Exception ex)
            {
                return new DynamicResponse<UserResponse>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Message = $"An error occurred: {ex.Message}",
                    MetaData = new PagingMetaData(),
                    Data = new List<UserResponse>()
                };
            }
        }

        /// <summary>
        /// Create new user
        /// </summary>
        public async Task<BaseResponse<UserResponse>> CreateUserAsync(CreateUserRequest request)
        {
            try
            {
                // Check if email already exists
                var existingAccount = await _unitOfWork.Repository<Account>()
                    .AsQueryable()
                    .Where(u => u.Email == request.Email && !u.IsDeleted)
                    .FirstOrDefaultAsync();

                if (existingAccount != null)
                {
                    return new BaseResponse<UserResponse>
                    {
                        Code = StatusCodes.Status400BadRequest,
                        Message = "Email already exists",
                        Data = null
                    };
                }

                // Check if role exists
                var roleExists = await _unitOfWork.Repository<Role>()
                    .AsQueryable()
                    .AnyAsync(r => r.Id == request.RoleId && !r.IsDeleted);

                if (!roleExists)
                {
                    return new BaseResponse<UserResponse>
                    {
                        Code = StatusCodes.Status400BadRequest,
                        Message = "Invalid role ID",
                        Data = null
                    };
                }

                // Create account entity
                var newAccount = _mapper.Map<Account>(request);
                
                // Hash password
                newAccount.PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

                // Save to database
                await _unitOfWork.Repository<Account>().InsertAsync(newAccount);
                await _unitOfWork.CommitAsync();

                // Reload with role information
                var createdAccount = await _unitOfWork.Repository<Account>()
                    .AsQueryable()
                    .Include(u => u.Role)
                    .FirstOrDefaultAsync(u => u.Id == newAccount.Id);

                return new BaseResponse<UserResponse>
                {
                    Code = StatusCodes.Status201Created,
                    Message = "User created successfully",
                    Data = _mapper.Map<UserResponse>(createdAccount)
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<UserResponse>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Message = $"An error occurred: {ex.Message}",
                    Data = null
                };
            }
        }

        /// <summary>
        /// Update existing user
        /// </summary>
        public async Task<BaseResponse<UserResponse>> UpdateUserAsync(Guid userId, UpdateUserRequest request)
        {
            try
            {
                if (userId == null)
                {
                    return new BaseResponse<UserResponse>
                    {
                        Code = StatusCodes.Status400BadRequest,
                        Message = "Invalid user ID",
                        Data = null
                    };
                }

                var account = await _unitOfWork.Repository<Account>()
                    .AsQueryable()
                    .Where(u => u.Id == userId && !u.IsDeleted)
                    .FirstOrDefaultAsync();

                if (account == null)
                {
                    return new BaseResponse<UserResponse>
                    {
                        Code = StatusCodes.Status404NotFound,
                        Message = "User not found",
                        Data = null
                    };
                }

                // Check if new role exists (if role is being updated)
                if (request.RoleId.HasValue)
                {
                    var roleExists = await _unitOfWork.Repository<Role>()
                        .AsQueryable()
                        .AnyAsync(r => r.Id == request.RoleId.Value && !r.IsDeleted);

                    if (!roleExists)
                    {
                        return new BaseResponse<UserResponse>
                        {
                            Code = StatusCodes.Status400BadRequest,
                            Message = "Invalid role ID",
                            Data = null
                        };
                    }
                }

                // Update account
                _mapper.Map(request, account);

                // Save changes
                await _unitOfWork.Repository<Account>().UpdateGuid(account, account.Id);
                await _unitOfWork.CommitAsync();

                // Reload with role information
                var updatedAccount = await _unitOfWork.Repository<Account>()
                    .AsQueryable()
                    .Include(u => u.Role)
                    .FirstOrDefaultAsync(u => u.Id == userId);

                return new BaseResponse<UserResponse>
                {
                    Code = StatusCodes.Status200OK,
                    Message = "User updated successfully",
                    Data = _mapper.Map<UserResponse>(updatedAccount)
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<UserResponse>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Message = $"An error occurred: {ex.Message}",
                    Data = null
                };
            }
        }

        /// <summary>
        /// Delete user (soft delete)
        /// </summary>
        public async Task<BaseResponse> DeleteUserAsync(Guid userId)
        {
            try
            {
                if (userId == null)
                {
                    return new BaseResponse
                    {
                        Code = StatusCodes.Status400BadRequest,
                        Message = "Invalid user ID"
                    };
                }

                var account = await _unitOfWork.Repository<Account>()
                    .AsQueryable()
                    .Where(u => u.Id == userId && !u.IsDeleted)
                    .FirstOrDefaultAsync();

                if (account == null)
                {
                    return new BaseResponse
                    {
                        Code = StatusCodes.Status404NotFound,
                        Message = "User not found"
                    };
                }

                // Soft delete
                account.IsDeleted = true;
                account.UpdatedAt = DateTime.UtcNow;

                await _unitOfWork.Repository<Account>().UpdateGuid(account, account.Id);
                await _unitOfWork.CommitAsync();

                return new BaseResponse
                {
                    Code = StatusCodes.Status200OK,
                    Message = "User deleted successfully"
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Message = $"An error occurred: {ex.Message}"
                };
            }
        }

        /// <summary>
        /// Check if email exists
        /// </summary>
        public async Task<bool> EmailExistsAsync(string email)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(email))
                    return false;

                return await _unitOfWork.Repository<Account>()
                    .AsQueryable()
                    .AnyAsync(u => u.Email == email && !u.IsDeleted);
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Verify user email
        /// </summary>
        public async Task<BaseResponse> VerifyUserEmailAsync(Guid userId)
        {
            try
            {
                if (userId == null)
                {
                    return new BaseResponse
                    {
                        Code = StatusCodes.Status400BadRequest,
                        Message = "Invalid user ID"
                    };
                }

                var account = await _unitOfWork.Repository<Account>()
                    .AsQueryable()
                    .Where(u => u.Id == userId && !u.IsDeleted)
                    .FirstOrDefaultAsync();

                if (account == null)
                {
                    return new BaseResponse
                    {
                        Code = StatusCodes.Status404NotFound,
                        Message = "User not found"
                    };
                }

                if (account.IsVerified)
                {
                    return new BaseResponse
                    {
                        Code = StatusCodes.Status400BadRequest,
                        Message = "Email is already verified"
                    };
                }

                account.IsVerified = true;
                account.UpdatedAt = DateTime.UtcNow;

                await _unitOfWork.Repository<Account>().UpdateGuid(account, account.Id);
                await _unitOfWork.CommitAsync();

                return new BaseResponse
                {
                    Code = StatusCodes.Status200OK,
                    Message = "Email verified successfully"
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Message = $"An error occurred: {ex.Message}"
                };
            }
        }

        /// <summary>
        /// Update user status
        /// </summary>
        public async Task<BaseResponse> UpdateUserStatusAsync(Guid userId, bool status)
        {
            try
            {
                if (userId == null)
                {
                    return new BaseResponse
                    {
                        Code = StatusCodes.Status400BadRequest,
                        Message = "Invalid user ID"
                    };
                }

                var account = await _unitOfWork.Repository<Account>()
                    .AsQueryable()
                    .Where(u => u.Id == userId && !u.IsDeleted)
                    .FirstOrDefaultAsync();

                if (account == null)
                {
                    return new BaseResponse
                    {
                        Code = StatusCodes.Status404NotFound,
                        Message = "User not found"
                    };
                }

                account.IsActive = status;
                account.UpdatedAt = DateTime.UtcNow;

                await _unitOfWork.Repository<Account>().UpdateGuid(account, account.Id);
                await _unitOfWork.CommitAsync();

                return new BaseResponse
                {
                    Code = StatusCodes.Status200OK,
                    Message = $"User status updated to {(status ? "active" : "inactive")} successfully"
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Message = $"An error occurred: {ex.Message}"
                };
            }
        }
    }
}

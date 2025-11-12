using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using FSCMS.Service.Services;
using FSCMS.Service.RequestModel;
using FSCMS.Service.ReponseModel;
using System.Security.Claims;
using FSCMS.Service.Interfaces;

namespace FA25_CP.CryoFert_BE.Controllers
{
    /// <summary>
    /// User Management Controller - Handles user CRUD operations, user details, and user management
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    [Authorize] 
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Get user by ID - Users can only view their own profile, Admin/Doctor can view any profile
        /// </summary>
        /// <param name="userId">User ID</param>
        /// <returns>User information</returns>
        [HttpGet("{userId}")]
        [ProducesResponseType(typeof(BaseResponse<UserResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseResponse<UserResponse>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseResponse<UserResponse>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BaseResponse<UserResponse>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(BaseResponse<UserResponse>), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(BaseResponse<UserResponse>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetUserById(Guid userId)
        {
            if (userId == null)
            {
                return BadRequest(new BaseResponse<UserResponse>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Message = "Invalid user ID"
                });
            }

            // Get current user ID and role from JWT token
            var currentUserIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var currentUserRole = User.FindFirst(ClaimTypes.Role)?.Value;
            
            if (string.IsNullOrEmpty(currentUserIdClaim) || !Guid.TryParse(currentUserIdClaim, out Guid currentUserId))
            {
                return Unauthorized(new BaseResponse<UserResponse>
                {
                    Code = StatusCodes.Status401Unauthorized,
                    Message = "Invalid user token"
                });
            }

            // Authorization check: Users can only view their own profile
            // Admin and Doctor can view any profile
            if (currentUserId != userId && currentUserRole != "Admin" && currentUserRole != "Doctor")
            {
                return StatusCode(StatusCodes.Status403Forbidden, new BaseResponse<UserResponse>
                {
                    Code = StatusCodes.Status403Forbidden,
                    Message = "You are not authorized to view this user's information",
                    Data = null
                });
            }

            var result = await _userService.GetUserByIdAsync(userId);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        /// <summary>
        /// Get user by email
        /// </summary>
        /// <param name="email">Email address</param>
        /// <returns>User information</returns>
        [HttpGet("by-email")]
        [ProducesResponseType(typeof(BaseResponse<UserResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseResponse<UserResponse>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseResponse<UserResponse>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BaseResponse<UserResponse>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(BaseResponse<UserResponse>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetUserByEmail([FromQuery] string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return BadRequest(new BaseResponse<UserResponse>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Message = "Email is required"
                });
            }

            var userResponse = await _userService.GetUserByEmailAsync(email);
            return StatusCode(userResponse.Code ?? StatusCodes.Status500InternalServerError, userResponse);
        }

        /// <summary>
        /// Get detailed user information by ID
        /// </summary>
        /// <param name="userId">User ID</param>
        /// <returns>Detailed user information</returns>
        [HttpGet("{userId}/details")]
        [ProducesResponseType(typeof(BaseResponse<UserDetailResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseResponse<UserDetailResponse>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseResponse<UserDetailResponse>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BaseResponse<UserDetailResponse>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(BaseResponse<UserDetailResponse>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetUserDetailById(Guid userId)
        {
            if (userId == null)
            {
                return BadRequest(new BaseResponse<UserDetailResponse>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Message = "Invalid user ID"
                });
            }

            var result = await _userService.GetUserDetailByIdAsync(userId);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        /// <summary>
        /// Get all users with pagination and filtering
        /// </summary>
        /// <param name="request">Pagination and filtering parameters</param>
        /// <returns>Paginated list of users</returns>
        [HttpGet]
        [Authorize(Roles = "Admin,Doctor")] // Only Admin and Doctor can view all users
        [ProducesResponseType(typeof(DynamicResponse<UserResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(DynamicResponse<UserResponse>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(DynamicResponse<UserResponse>), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(DynamicResponse<UserResponse>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllUsers([FromQuery] GetUsersRequest request)
        {
            var result = await _userService.GetAllUsersAsync(request);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        /// <summary>
        /// Search users by name/email
        /// </summary>
        /// <param name="userName">Search term</param>
        /// <returns>List of matching users</returns>
        [HttpGet("search")]
        [Authorize(Roles = "Admin,Doctor")] // Only Admin and Doctor can search users
        [ProducesResponseType(typeof(BaseResponse<List<UserResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseResponse<List<UserResponse>>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseResponse<List<UserResponse>>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(BaseResponse<List<UserResponse>>), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(BaseResponse<List<UserResponse>>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> SearchUsers([FromQuery] string userName)
        {
            if (string.IsNullOrWhiteSpace(userName))
            {
                return BadRequest(new BaseResponse<List<UserResponse>>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Message = "Search term is required"
                });
            }

            var result = await _userService.GetUsersByNameAsync(userName);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        /// <summary>
        /// Create new user
        /// </summary>
        /// <param name="createUserRequest">User creation details</param>
        /// <returns>Created user information</returns>
        [HttpPost]
        [Authorize(Roles = "Admin")] // Only Admin can create users
        [ProducesResponseType(typeof(BaseResponse<UserResponse>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(BaseResponse<UserResponse>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseResponse<UserResponse>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(BaseResponse<UserResponse>), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(BaseResponse<UserResponse>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest createUserRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new BaseResponse<UserResponse>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Message = "Invalid input data"
                });
            }

            var result = await _userService.CreateUserAsync(createUserRequest);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        /// <summary>
        /// Update existing user
        /// </summary>
        /// <param name="userId">User ID</param>
        /// <param name="updateUserRequest">Updated user information</param>
        /// <returns>Updated user information</returns>
        [HttpPut("{userId}")]
        [ProducesResponseType(typeof(BaseResponse<UserResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseResponse<UserResponse>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseResponse<UserResponse>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BaseResponse<UserResponse>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(BaseResponse<UserResponse>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateUser(Guid userId, [FromBody] UpdateUserRequest updateUserRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new BaseResponse<UserResponse>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Message = "Invalid input data"
                });
            }

            // Check if user is updating their own profile or is Admin
            var currentUserIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var currentUserRole = User.FindFirst(ClaimTypes.Role)?.Value;
            
            if (string.IsNullOrEmpty(currentUserIdClaim) || !Guid.TryParse(currentUserIdClaim, out Guid currentUserId))
            {
                return Unauthorized(new BaseResponse<UserResponse>
                {
                    Code = StatusCodes.Status401Unauthorized,
                    Message = "Invalid user token"
                });
            }

            // Allow users to update their own profile or Admin to update any profile
            if (currentUserId != userId && currentUserRole != "Admin")
            {
                return Forbid();
            }

            var result = await _userService.UpdateUserAsync(userId, updateUserRequest);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        /// <summary>
        /// Delete user (soft delete)
        /// </summary>
        /// <param name="userId">User ID</param>
        /// <returns>Deletion result</returns>
        [HttpDelete("{userId}")]
        [Authorize(Roles = "Admin")] // Only Admin can delete users
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteUser(Guid userId)
        {
            if (userId == null)
            {
                return BadRequest(new BaseResponse
                {
                    Code = StatusCodes.Status400BadRequest,
                    Message = "Invalid user ID"
                });
            }

            var result = await _userService.DeleteUserAsync(userId);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        /// <summary>
        /// Check if email exists
        /// </summary>
        /// <param name="email">Email address</param>
        /// <returns>Email existence status</returns>
        [HttpGet("email-exists")]
        [ProducesResponseType(typeof(BaseResponse<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseResponse<bool>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseResponse<bool>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(BaseResponse<bool>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CheckEmailExists([FromQuery] string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return BadRequest(new BaseResponse<bool>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Message = "Email is required"
                });
            }

            var exists = await _userService.EmailExistsAsync(email);
            return Ok(new BaseResponse<bool>
            {
                Code = StatusCodes.Status200OK,
                Message = "Email check completed",
                Data = exists
            });
        }

        /// <summary>
        /// Verify user email
        /// </summary>
        /// <param name="userId">User ID</param>
        /// <returns>Email verification result</returns>
        [HttpPost("{userId}/verify-email")]
        [Authorize(Roles = "Admin")] // Only Admin can verify emails
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> VerifyUserEmail(Guid userId)
        {
            if (userId == null)
            {
                return BadRequest(new BaseResponse
                {
                    Code = StatusCodes.Status400BadRequest,
                    Message = "Invalid user ID"
                });
            }

            var result = await _userService.VerifyUserEmailAsync(userId);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        /// <summary>
        /// Update user status (active/inactive)
        /// </summary>
        /// <param name="userId">User ID</param>
        /// <param name="status">New status</param>
        /// <returns>Status update result</returns>
        [HttpPut("{userId}/status")]
        [Authorize(Roles = "Admin")] // Only Admin can change user status
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateUserStatus(Guid userId, [FromBody] bool status)
        {
            if (userId == null)
            {
                return BadRequest(new BaseResponse
                {
                    Code = StatusCodes.Status400BadRequest,
                    Message = "Invalid user ID"
                });
            }

            var result = await _userService.UpdateUserStatusAsync(userId, status);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        /// <summary>
        /// Get current user profile
        /// </summary>
        /// <returns>Current user information</returns>
        [HttpGet("profile")]
        [ProducesResponseType(typeof(BaseResponse<UserResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseResponse<UserResponse>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(BaseResponse<UserResponse>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCurrentUserProfile()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out Guid userId))
            {
                return Unauthorized(new BaseResponse<UserResponse>
                {
                    Code = StatusCodes.Status401Unauthorized,
                    Message = "Invalid user token"
                });
            }

            var result = await _userService.GetUserByIdAsync(userId);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        /// <summary>
        /// Update current user profile
        /// </summary>
        /// <param name="updateUserRequest">Updated profile information</param>
        /// <returns>Updated user information</returns>
        [HttpPut("profile")]
        [ProducesResponseType(typeof(BaseResponse<UserResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseResponse<UserResponse>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseResponse<UserResponse>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(BaseResponse<UserResponse>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateCurrentUserProfile([FromBody] UpdateUserRequest updateUserRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new BaseResponse<UserResponse>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Message = "Invalid input data"
                });
            }

            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out Guid userId))
            {
                return Unauthorized(new BaseResponse<UserResponse>
                {
                    Code = StatusCodes.Status401Unauthorized,
                    Message = "Invalid user token"
                });
            }

            var result = await _userService.UpdateUserAsync(userId, updateUserRequest);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }
    }
}

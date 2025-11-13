using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using FSCMS.Service.Services;
using FSCMS.Service.RequestModel;
using FSCMS.Service.ReponseModel;
using System.Security.Claims;
using FSCMS.Service.Interfaces;
using FA25_CP.CryoFert_BE.AppStarts;

namespace FA25_CP.CryoFert_BE.Controllers
{
    /// <summary>
    /// Authentication Controller - Handles login, registration, password management, and email verification
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        /// <summary>
        /// User login with email and password
        /// </summary>
        [HttpPost("login")]
        [ApiDefaultResponse(typeof(LoginResponseModel), UseDynamicWrapper = false)]
        public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new BaseResponseForLogin<LoginResponseModel>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Message = "Invalid input data",
                    Data = null,
                    IsBanned = false
                });
            }

            var result = await _authService.AuthenticateAsync(loginModel.Email, loginModel.Password, loginModel.Mobile);
            
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        /// <summary>
        /// User registration
        /// </summary>
        [HttpPost("register")]
        [ApiDefaultResponse(typeof(TokenModel), UseDynamicWrapper = false)]
        public async Task<IActionResult> Register([FromBody] RegisterRequestModel registerModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new BaseResponse<TokenModel>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Message = "Invalid input data"
                });
            }

            var result = await _authService.RegisterAsync(registerModel);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        /// <summary>
        /// Refresh JWT token using refresh token
        /// </summary>
        [HttpPost("refresh-token")]
        [ApiDefaultResponse(typeof(TokenModel), UseDynamicWrapper = false)]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest refreshTokenRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new BaseResponse<TokenModel>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Message = "Invalid input data"
                });
            }

            var result = await _authService.RefreshTokenAsync(refreshTokenRequest.RefreshToken);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        /// <summary>
        /// Send verification email
        /// </summary>
        [HttpPost("send-verification-email")]
        [ApiDefaultResponse(typeof(object), UseDynamicWrapper = false)]
        public async Task<IActionResult> SendVerificationEmail([FromBody] EmailRequest emailRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new BaseResponse
                {
                    Code = StatusCodes.Status400BadRequest,
                    Message = "Invalid input data"
                });
            }

            var result = await _authService.SendVerificationEmailAsync(emailRequest.Email);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        /// <summary>
        /// Verify email with verification code
        /// </summary>
        [HttpPost("verify-email")]
        [ApiDefaultResponse(typeof(TokenModel), UseDynamicWrapper = false)]
        public async Task<IActionResult> VerifyEmail([FromBody] EmailVerificationModel verificationModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new BaseResponse<TokenModel>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Message = "Invalid input data"
                });
            }

            var result = await _authService.VerifyAccountAsync(verificationModel);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        /// <summary>
        /// Forgot password - send reset password email
        /// </summary>
        [HttpPost("forgot-password")]
        [ApiDefaultResponse(typeof(object), UseDynamicWrapper = false)]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequest forgotPasswordRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new BaseResponse
                {
                    Code = StatusCodes.Status400BadRequest,
                    Message = "Invalid input data"
                });
            }

            var result = await _authService.ForgotPassword(forgotPasswordRequest);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        /// <summary>
        /// Change password for authenticated user
        /// </summary>
        [HttpPost("change-password")]
        [Authorize]
        [ApiDefaultResponse(typeof(object), UseDynamicWrapper = false)]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest changePasswordRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new BaseResponse
                {
                    Code = StatusCodes.Status400BadRequest,
                    Message = "Invalid input data"
                });
            }

            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out Guid userId))
            {
                return Unauthorized(new BaseResponse
                {
                    Code = StatusCodes.Status401Unauthorized,
                    Message = "Invalid user token"
                });
            }

            var result = await _authService.ChangePasswordAsync(userId, changePasswordRequest);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        /// <summary>
        /// Logout - Clear refresh token for authenticated user
        /// </summary>
        [HttpPost("logout")]
        [Authorize]
        [ApiDefaultResponse(typeof(object), UseDynamicWrapper = false)]
        public async Task<IActionResult> Logout()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out Guid userId))
            {
                return Unauthorized(new BaseResponse
                {
                    Code = StatusCodes.Status401Unauthorized,
                    Message = "Invalid user token"
                });
            }

            var result = await _authService.LogoutAsync(userId);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        /// <summary>
        /// Admin creates new account
        /// </summary>
        [HttpPost("admin/create-account")]
        [Authorize(Roles = "Admin")]
        [ApiDefaultResponse(typeof(TokenModel), UseDynamicWrapper = false)]
        public async Task<IActionResult> AdminCreateAccount([FromBody] AdminCreateAccountModel adminCreateAccountModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new BaseResponse<TokenModel>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Message = "Invalid input data"
                });
            }

            var result = await _authService.AdminGenAcc(adminCreateAccountModel);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        /// <summary>
        /// Admin sends account credentials to user
        /// </summary>
        [HttpPost("admin/send-account/{userId}")]
        [Authorize(Roles = "Admin")]
        [ApiDefaultResponse(typeof(object), UseDynamicWrapper = false)]
        public async Task<IActionResult> SendAccount(Guid userId)
        {
            if (userId == null)
            {
                return BadRequest(new BaseResponse
                {
                    Code = StatusCodes.Status400BadRequest,
                    Message = "Invalid user ID"
                });
            }

            var result = await _authService.SendAccount(userId);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        /// <summary>
        /// Set email as verified (Admin only)
        /// </summary>
        [HttpPost("admin/set-email-verified")]
        [Authorize(Roles = "Admin")]
        [ApiDefaultResponse(typeof(object), UseDynamicWrapper = false)]
        public async Task<IActionResult> SetEmailVerified([FromBody] EmailRequest emailRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new BaseResponse
                {
                    Code = StatusCodes.Status400BadRequest,
                    Message = "Invalid input data"
                });
            }

            var result = await _authService.SetEmailVerified(emailRequest.Email);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }
    }
}

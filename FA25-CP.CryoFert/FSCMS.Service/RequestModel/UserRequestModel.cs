using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSCMS.Service.RequestModel
{
    public class CreateUserRequest
    {
        [RegularExpression(@"^[\p{L}0-9\s]+$", ErrorMessage = "Username cannot contain special characters.")]
        [StringLength(50, ErrorMessage = "Username cannot exceed 50 characters.")]
        public string? UserName { get; set; }

        [Range(1, 150, ErrorMessage = "Age must be between 1 and 150.")]
        public int? Age { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters.")]
        public string Password { get; set; }

        [RegularExpression(@"^\+?[0-9]{10,15}$", ErrorMessage = "Phone number must be between 10 and 15 digits and may start with a '+' sign.")]
        public string? Phone { get; set; }

        [StringLength(100, ErrorMessage = "Location cannot exceed 100 characters.")]
        public string? Location { get; set; }

        [StringLength(100, ErrorMessage = "Country cannot exceed 100 characters.")]
        public string? Country { get; set; }

        public string? Image { get; set; }

        [Required(ErrorMessage = "RoleId is required.")]
        public int RoleId { get; set; }

        public bool? Status { get; set; } = true;
    }

    public class UpdateUserRequest
    {
        [RegularExpression(@"^[\p{L}0-9\s]+$", ErrorMessage = "Username cannot contain special characters.")]
        [StringLength(50, ErrorMessage = "Username cannot exceed 50 characters.")]
        public string? UserName { get; set; }

        [Range(1, 150, ErrorMessage = "Age must be between 1 and 150.")]
        public int? Age { get; set; }

        [RegularExpression(@"^\+?[0-9]{10,15}$", ErrorMessage = "Phone number must be between 10 and 15 digits and may start with a '+' sign.")]
        public string? Phone { get; set; }

        [StringLength(100, ErrorMessage = "Location cannot exceed 100 characters.")]
        public string? Location { get; set; }

        [StringLength(100, ErrorMessage = "Country cannot exceed 100 characters.")]
        public string? Country { get; set; }

        public string? Image { get; set; }

        public int? RoleId { get; set; }

        public bool? Status { get; set; }
    }

    public class GetUserByIdRequest
    {
        [Required(ErrorMessage = "User ID is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "User ID must be greater than 0.")]
        public int UserId { get; set; }
    }

    public class GetUserByEmailRequest
    {
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }
    }

    public class GetUserByNameRequest
    {
        [Required(ErrorMessage = "Username is required.")]
        public string UserName { get; set; }
    }

    public class GetUsersRequest : PagingModel
    {
        public string? SearchTerm { get; set; }
        public int? RoleId { get; set; }
        public bool? Status { get; set; }
        public bool? EmailVerified { get; set; }
    }

    public class PagingModel
    {
        public int Page { get; set; } = 1;
        public int Size { get; set; } = 50;
        public string? Sort { get; set; }
        public string? Order { get; set; }
    }
}

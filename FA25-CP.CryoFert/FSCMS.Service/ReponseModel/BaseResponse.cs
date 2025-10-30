using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FSCMS.Service.ReponseModel
{
    /// <summary>
    /// Standard API response wrapper with generic data type
    /// </summary>
    /// <typeparam name="T">The type of data being returned</typeparam>
    public class BaseResponse<T>
    {
        /// <summary>
        /// HTTP status code
        /// </summary>
        [JsonPropertyName("code")]
        public int? Code { get; set; }

        /// <summary>
        /// System-specific error code for debugging and error handling
        /// </summary>
        [JsonPropertyName("systemCode")]
        public string? SystemCode { get; set; }

        /// <summary>
        /// Human-readable message describing the result
        /// </summary>
        [JsonPropertyName("message")]
        public string? Message { get; set; }

        /// <summary>
        /// The actual data payload
        /// </summary>
        [JsonPropertyName("data")]
        public T? Data { get; set; }

        /// <summary>
        /// Timestamp when the response was generated
        /// </summary>
        [JsonPropertyName("timestamp")]
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Indicates if the operation was successful
        /// </summary>
        [JsonPropertyName("success")]
        public bool Success => Code >= 200 && Code < 300;

        /// <summary>
        /// Creates a successful response
        /// </summary>
        public static BaseResponse<T> CreateSuccess(T data, string message = "Operation completed successfully", int code = 200)
        {
            return new BaseResponse<T>
            {
                Code = code,
                SystemCode = "SUCCESS",
                Message = message,
                Data = data
            };
        }

        /// <summary>
        /// Creates an error response
        /// </summary>
        public static BaseResponse<T> CreateError(string message, int code = 500, string? systemCode = null)
        {
            return new BaseResponse<T>
            {
                Code = code,
                SystemCode = systemCode ?? "ERROR",
                Message = message,
                Data = default(T)
            };
        }
    }

    /// <summary>
    /// Specialized response for login operations
    /// </summary>
    /// <typeparam name="T">The type of login data being returned</typeparam>
    public class BaseResponseForLogin<T> : BaseResponse<T>
    {
        /// <summary>
        /// Indicates if the user account is banned
        /// </summary>
        [JsonPropertyName("isBanned")]
        public bool IsBanned { get; set; }

        /// <summary>
        /// Indicates if email verification is required
        /// </summary>
        [JsonPropertyName("requiresVerification")]
        public bool RequiresVerification { get; set; }

        /// <summary>
        /// ID of the banned account (if applicable)
        /// </summary>
        [JsonPropertyName("bannedAccountId")]
        public int BannedAccountId { get; set; }
    }

    /// <summary>
    /// Standard API response without generic data type
    /// </summary>
    public class BaseResponse
    {
        /// <summary>
        /// HTTP status code
        /// </summary>
        [JsonPropertyName("code")]
        public int? Code { get; set; }

        /// <summary>
        /// System-specific error code for debugging and error handling
        /// </summary>
        [JsonPropertyName("systemCode")]
        public string? SystemCode { get; set; }

        /// <summary>
        /// Human-readable message describing the result
        /// </summary>
        [JsonPropertyName("message")]
        public string? Message { get; set; }

        /// <summary>
        /// Timestamp when the response was generated
        /// </summary>
        [JsonPropertyName("timestamp")]
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Indicates if the operation was successful
        /// </summary>
        [JsonPropertyName("success")]
        public bool Success => Code >= 200 && Code < 300;

        /// <summary>
        /// Creates a successful response
        /// </summary>
        public static BaseResponse CreateSuccess(string message = "Operation completed successfully", int code = 200)
        {
            return new BaseResponse
            {
                Code = code,
                SystemCode = "SUCCESS",
                Message = message
            };
        }

        /// <summary>
        /// Creates an error response
        /// </summary>
        public static BaseResponse CreateError(string message, int code = 500, string? systemCode = null)
        {
            return new BaseResponse
            {
                Code = code,
                SystemCode = systemCode ?? "ERROR",
                Message = message
            };
        }
    }

    /// <summary>
    /// Response wrapper for paginated data
    /// </summary>
    /// <typeparam name="T">The type of data in the collection</typeparam>
    public class DynamicResponse<T>
    {
        /// <summary>
        /// HTTP status code
        /// </summary>
        [JsonPropertyName("code")]
        public int? Code { get; set; }

        /// <summary>
        /// System-specific error code for debugging and error handling
        /// </summary>
        [JsonPropertyName("systemCode")]
        public string? SystemCode { get; set; }

        /// <summary>
        /// Human-readable message describing the result
        /// </summary>
        [JsonPropertyName("message")]
        public string? Message { get; set; }

        /// <summary>
        /// Pagination metadata
        /// </summary>
        [JsonPropertyName("metaData")]
        public PagingMetaData MetaData { get; set; } = new PagingMetaData();

        /// <summary>
        /// The collection of data items
        /// </summary>
        [JsonPropertyName("data")]
        public List<T> Data { get; set; } = new List<T>();

        /// <summary>
        /// Timestamp when the response was generated
        /// </summary>
        [JsonPropertyName("timestamp")]
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Indicates if the operation was successful
        /// </summary>
        [JsonPropertyName("success")]
        public bool Success => Code >= 200 && Code < 300;
    }

    /// <summary>
    /// Pagination metadata for paginated responses
    /// </summary>
    public class PagingMetaData
    {
        /// <summary>
        /// Current page number (1-based)
        /// </summary>
        [JsonPropertyName("page")]
        public int Page { get; set; } = 1;

        /// <summary>
        /// Number of items per page
        /// </summary>
        [JsonPropertyName("size")]
        public int Size { get; set; } = 50;

        /// <summary>
        /// Total number of items across all pages
        /// </summary>
        [JsonPropertyName("total")]
        public int Total { get; set; } = 0;

        /// <summary>
        /// Total number of pages
        /// </summary>
        [JsonPropertyName("totalPages")]
        public int TotalPages => Size > 0 ? (int)Math.Ceiling((double)Total / Size) : 0;

        /// <summary>
        /// Indicates if there is a next page
        /// </summary>
        [JsonPropertyName("hasNext")]
        public bool HasNext => Page < TotalPages;

        /// <summary>
        /// Indicates if there is a previous page
        /// </summary>
        [JsonPropertyName("hasPrevious")]
        public bool HasPrevious => Page > 1;

        /// <summary>
        /// Number of items in the current page
        /// </summary>
        [JsonPropertyName("currentPageSize")]
        public int CurrentPageSize { get; set; }
    }

    /// <summary>
    /// Base model for pagination requests
    /// </summary>
    public class PagingModel
    {
        /// <summary>
        /// Page number (1-based, default: 1)
        /// </summary>
        [JsonPropertyName("page")]
        public int Page { get; set; } = 1;

        /// <summary>
        /// Number of items per page (default: 50, max: 100)
        /// </summary>
        [JsonPropertyName("size")]
        public int Size { get; set; } = 50;

        /// <summary>
        /// Field to sort by
        /// </summary>
        [JsonPropertyName("sort")]
        public string? Sort { get; set; }

        /// <summary>
        /// Sort order: "asc" or "desc" (default: "asc")
        /// </summary>
        [JsonPropertyName("order")]
        public string? Order { get; set; } = "asc";

        /// <summary>
        /// Validates and normalizes pagination parameters
        /// </summary>
        public void Normalize()
        {
            if (Page < 1) Page = 1;
            if (Size < 1) Size = 50;
            if (Size > 100) Size = 100;
            Order = Order?.ToLowerInvariant() == "desc" ? "desc" : "asc";
        }
    }
}

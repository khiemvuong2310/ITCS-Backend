using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json;

namespace FA25_CP.CryoFert_BE.AppStarts
{
    public static class AuthConfig
    {
        public static void ConfigureAuthService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            var secrectKey = configuration["Jwt:Key"];

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    RequireExpirationTime = true,
                    ClockSkew = TimeSpan.Zero,
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidAudience = configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secrectKey))
                };

                // Comprehensive JWT Bearer Events handling
                o.Events = new JwtBearerEvents
                {
                    // Called when a message is received
                    OnMessageReceived = context =>
                    {
                        // Extract token from Authorization header or query string
                        var token = context.Request.Headers["Authorization"]
                            .FirstOrDefault()?.Split(" ").Last();

                        if (string.IsNullOrEmpty(token))
                        {
                            // Try to get token from query string (for SignalR or similar scenarios)
                            token = context.Request.Query["access_token"];
                        }

                        if (!string.IsNullOrEmpty(token))
                        {
                            context.Token = token;
                        }

                        return Task.CompletedTask;
                    },

                    // Called when token validation begins
                    OnTokenValidated = context =>
                    {
                        // Additional custom validation logic can be added here
                        var claimsIdentity = context.Principal?.Identity as System.Security.Claims.ClaimsIdentity;
                        
                        if (claimsIdentity != null)
                        {
                            // Log successful token validation
                            var userId = claimsIdentity.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
                            var userEmail = claimsIdentity.FindFirst(System.Security.Claims.ClaimTypes.Name)?.Value;
                            
                            // You can add logging here if needed
                            // Console.WriteLine($"Token validated for user: {userEmail} (ID: {userId})");
                        }

                        return Task.CompletedTask;
                    },

                    // Called when authentication fails
                    OnAuthenticationFailed = context =>
                    {
                        // Log authentication failure
                        var exception = context.Exception;
                        
                        // Handle specific token validation errors
                        string errorMessage = "Authentication failed.";
                        
                        if (exception is SecurityTokenExpiredException)
                        {
                            errorMessage = "Token has expired. Please login again.";
                            context.Response.Headers.Add("Token-Expired", "true");
                        }
                        else if (exception is SecurityTokenInvalidSignatureException)
                        {
                            errorMessage = "Invalid token signature.";
                        }
                        else if (exception is SecurityTokenInvalidIssuerException)
                        {
                            errorMessage = "Invalid token issuer.";
                        }
                        else if (exception is SecurityTokenInvalidAudienceException)
                        {
                            errorMessage = "Invalid token audience.";
                        }
                        else if (exception is SecurityTokenNotYetValidException)
                        {
                            errorMessage = "Token is not yet valid.";
                        }
                        else if (exception is SecurityTokenException)
                        {
                            errorMessage = "Invalid token format.";
                        }

                        // Store error message for use in OnChallenge
                        context.HttpContext.Items["AuthError"] = errorMessage;
                        
                        return Task.CompletedTask;
                    },

                    // Called when authentication challenge is issued (401)
                    OnChallenge = context =>
                    {
                        context.HandleResponse();
                        context.Response.StatusCode = 401;
                        context.Response.ContentType = "application/json";

                        // Get specific error message if available
                        var errorMessage = context.HttpContext.Items["AuthError"]?.ToString() 
                            ?? "Authentication required. Please provide a valid JWT token.";

                        var response = new
                        {
                            code = 401,
                            systemCode = (string?)null,
                            message = errorMessage,
                            data = (object?)null
                        };

                        return context.Response.WriteAsync(JsonSerializer.Serialize(response, new JsonSerializerOptions
                        {
                            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                        }));
                    },

                    // Called when access is forbidden (403)
                    OnForbidden = context =>
                    {
                        context.Response.StatusCode = 403;
                        context.Response.ContentType = "application/json";

                        var response = new
                        {
                            code = 403,
                            systemCode = (string?)null,
                            message = "Access denied. You don't have permission to access this resource. Admin role required.",
                            data = (object?)null
                        };

                        return context.Response.WriteAsync(JsonSerializer.Serialize(response, new JsonSerializerOptions
                        {
                            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                        }));
                    }
                };
            })
            //.AddCookie()
            //.AddGoogle(GoogleDefaults.AuthenticationScheme, googleOptions =>
            //{
            //    IConfigurationSection googleAuthNSection = configuration.GetSection("GoogleKeys");

            //    googleOptions.ClientId = googleAuthNSection["ClientId"];
            //    googleOptions.ClientSecret = googleAuthNSection["ClientSecret"];

            //    // Optional: Set a custom callback path if needed
            //    googleOptions.CallbackPath = "/dang-nhap-tu-google";
            //}) 
            ;
            services.AddAuthorization();
        }
    }
}

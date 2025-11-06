using System.Text.Json.Serialization;
using DotNetEnv;
using FA25_CP.CryoFert_BE.AppStarts;
using FSCMS.Core; // namespace chứa AppDbContext
using FSCMS.Core.Models;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Pomelo.EntityFrameworkCore.MySql;

namespace FA25_CP.CryoFert_BE
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // 1. Load environment variables từ .env
            Env.Load();

            var builder = WebApplication.CreateBuilder(args);

            // 2. Add services to the container
            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = null;
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });

            builder.Services.AddHttpContextAccessor();

            // Memory Cache for performance optimization
            builder.Services.AddMemoryCache();

            // Exception handler 
            builder.Services.AddProblemDetails();

            builder.Services.AddControllers().AddJsonOptions(opts =>
            {
                opts.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });


            // 3. DbContext config (MySQL)
            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                var connectionString =
                    Environment.GetEnvironmentVariable("DB_CONNECTION_STRING")
                    ?? builder.Configuration.GetConnectionString("DefaultConnection")
                    ?? builder.Configuration["ConnectionStrings:DefaultConnection"]
                    ?? builder.Configuration["DB_CONNECTION_STRING"];

                if (string.IsNullOrWhiteSpace(connectionString))
                    throw new InvalidOperationException("Missing DB_CONNECTION_STRING (.env) or ConnectionStrings:DefaultConnection (appsettings)");

                var serverVersion = ServerVersion.AutoDetect(connectionString);
                options.UseMySql(connectionString, serverVersion,
                    mysqlOptions => mysqlOptions.MigrationsAssembly(typeof(AppDbContext).Assembly.GetName().Name));
            });

            builder.Services.Configure<CloudinarySettings>(
                builder.Configuration.GetSection("CloudinarySettings")
            );

            // 4. CORS config
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowReactApp", policy =>
                {
                    policy.WithOrigins(
                            "http://localhost:5173", 
                            "https://fscms.pages.dev",
                            "https://cryo.devnguyen.xyz",
                            "https://cryofert.runasp.net"
                          )
                          .AllowAnyHeader()
                          .AllowAnyMethod()
                          .AllowCredentials();
                });
                
                // Public CORS policy for broader access (without credentials)
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });

            // 5. Upload config (giới hạn 100 MB)
            builder.Services.Configure<FormOptions>(options =>
            {
                options.MultipartBodyLengthLimit = 104857600; // 100 MB
            });

            // 6. Install Dependency Injection Services
            builder.Services.InstallService(builder.Configuration);

            // 7. Configure Authentication & Authorization (JWT)
            builder.Services.ConfigureAuthService(builder.Configuration);

            // 8. Swagger & OpenAPI + JWT Auth
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "FSCMS API",
                    Version = "v1"
                });

                var securitySchema = new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                };

                c.AddSecurityDefinition("Bearer", securitySchema);
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    { securitySchema, new[] { "Bearer" } }
                });
            });

            // 9. Build application
            var app = builder.Build();

            // 10. Configure middleware
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "FSCMS API v1");
                });
            }
            else
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseExceptionHandler();

            app.UseHttpsRedirection();
            app.UseCors("AllowReactApp");
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();

            // 9. Run application
            app.Run();
        }
    }
}

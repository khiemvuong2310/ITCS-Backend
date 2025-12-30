using DotNetEnv;
using EFCoreSecondLevelCacheInterceptor;
using FA25_CP.CryoFert_BE.AppStarts;
using FA25_CP.CryoFert_BE.Common.Filters;
using FSCMS.Core;
using FSCMS.Core.Models;
using FSCMS.Core.Models.Options;
using FSCMS.Service.SignalR;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Pomelo.EntityFrameworkCore.MySql;
using System.Reflection;
using System.Text.Json.Serialization;

namespace FA25_CP.CryoFert_BE
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // 1. Load environment variables từ .env file
            Env.Load();

            var builder = WebApplication.CreateBuilder(args);

            // 2. Configure core services - Controllers với JSON options
            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = null;
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });

            // 3. Add HTTP context accessor để truy cập HttpContext trong services
            builder.Services.AddHttpContextAccessor();

            // 4. Add MVC services (Controllers với Views và Razor Pages)
            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages();

            // 5. Add memory cache để tối ưu hiệu suất
            builder.Services.AddMemoryCache();

            // 6. Add problem details cho exception handling
            builder.Services.AddProblemDetails();

            // 7. Configure SignalR cho real-time communication
            builder.Services.AddSignalR();

            // 8. Configure Cloudinary settings cho image upload
            builder.Services.Configure<CloudinarySettings>(options =>
            {
                options.CloudName = Environment.GetEnvironmentVariable("CLOUDINARY_CLOUDNAME") ?? "";
                options.ApiKey = Environment.GetEnvironmentVariable("CLOUDINARY_APIKEY") ?? "";
                options.ApiSecret = Environment.GetEnvironmentVariable("CLOUDINARY_APISECRET") ?? "";
            });

            // 9. Configure VnPay settings cho payment gateway
            builder.Services.Configure<VnPayOptions>(options =>
            {
                options.vnp_Url = Environment.GetEnvironmentVariable("VNPAY_URL") ?? "";
                options.vnp_Api = Environment.GetEnvironmentVariable("VNPAY_API") ?? "";
                options.vnp_TmnCode = Environment.GetEnvironmentVariable("VNPAY_TMNCODE") ?? "";
                options.vnp_HashSecret = Environment.GetEnvironmentVariable("VNPAY_HASHSECRET") ?? "";
                options.vnp_Returnurl = Environment.GetEnvironmentVariable("VNPAY_RETURNURL") ?? "";
            });

            // 10. Configure PayOS settings cho payment gateway
            builder.Services.Configure<PayOSOptions>(options =>
            {
                options.pos_ClientId = Environment.GetEnvironmentVariable("PAYOS_CLIENT_ID") ?? "";
                options.pos_ApiKey = Environment.GetEnvironmentVariable("PAYOS_API_KEY") ?? "";
                options.pos_ChecksumKey = Environment.GetEnvironmentVariable("PAYOS_CHECKSUM_KEY") ?? "";
                options.pos_ReturnUrl = Environment.GetEnvironmentVariable("PAYOS_RETURN_URL") ?? "";
                options.pos_CancelUrl = Environment.GetEnvironmentVariable("PAYOS_CANCEL_URL") ?? "";
                options.pos_WebhookUrl = Environment.GetEnvironmentVariable("PAYOS_WEBHOOK_URL") ?? "";
            });

            // 11. Configure CORS policy để cho phép frontend truy cập API
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.WithOrigins(
                            "http://localhost:5173",
                            "https://localhost:5173",
                            "http://localhost:3000",
                            "https://localhost:3000",
                            "http://localhost:3001",
                            "https://localhost:3001",
                            "https://localhost",
                            "https://fscms.pages.dev",
                            "https://cryo.devnguyen.xyz",
                            "https://cryofert.runasp.net",
                            "http://localhost:5174",
                            "https://cryofert-mobile-preview.pages.dev"
                          )
                          .AllowAnyHeader()
                          .AllowAnyMethod()
                          .AllowCredentials();
                });
            });

            // 12. Configure form options - giới hạn upload file 100 MB
            builder.Services.Configure<FormOptions>(options =>
            {
                options.MultipartBodyLengthLimit = 104857600; // 100 MB
            });

            // 13. Configure database context - MySQL với connection pooling và second level cache
            builder.Services.AddDbContextPool<AppDbContext>((serviceProvider, options) =>
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

                // Thêm second level cache interceptor để tự động cache queries
                options.AddInterceptors(serviceProvider.GetRequiredService<SecondLevelCacheInterceptor>());
            });
            //builder.Services.AddDbContext<AppDbContext>(options =>
            //{
            //    var connectionString =
            //        Environment.GetEnvironmentVariable("DB_CONNECTION_STRING")
            //        ?? builder.Configuration.GetConnectionString("DefaultConnection")
            //        ?? builder.Configuration["ConnectionStrings:DefaultConnection"]
            //        ?? builder.Configuration["DB_CONNECTION_STRING"];

            //    if (string.IsNullOrWhiteSpace(connectionString))
            //        throw new InvalidOperationException("Missing DB_CONNECTION_STRING (.env) or ConnectionStrings:DefaultConnection (appsettings)");

            //    var serverVersion = ServerVersion.AutoDetect(connectionString);
            //    options.UseMySql(connectionString, serverVersion,
            //        mysqlOptions => mysqlOptions.MigrationsAssembly(typeof(AppDbContext).Assembly.GetName().Name));
            //});

            // 14. Install custom dependency injection services (bao gồm caching services)
            builder.Services.InstallService(builder.Configuration);

            // 15. Configure Authentication & Authorization với JWT
            builder.Services.ConfigureAuthService(builder.Configuration);

            // 16. Configure Swagger/OpenAPI documentation với JWT authentication
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.EnableAnnotations();

                // Đăng ký operation filter cho default responses
                c.OperationFilter<ApiDefaultResponseOperationFilter>();

                // Include XML comments nếu file tồn tại
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                if (File.Exists(xmlPath)) c.IncludeXmlComments(xmlPath);

                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "FSCMS API",
                    Version = "v1"
                });

                // Configure JWT Bearer authentication cho Swagger
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

            // 17. Build application
            var app = builder.Build();

            // 18. Configure middleware pipeline
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

            app.UseRouting();
            app.UseExceptionHandler();
            app.UseHttpsRedirection();
            app.UseCors("AllowAll");
            app.UseAuthentication();
            app.UseAuthorization();

            // 19. Map SignalR hubs cho real-time communication
            app.MapHub<TransactionHub>("/transactionHub");
            app.MapHub<NotificationHub>("/notificationHub");

            app.MapControllers();

            // 20. Run application
            app.Run();
        }
    }
}

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
            try
            {
                // 1. Load environment variables
                try
                {
                    var envPath = Path.Combine(Directory.GetCurrentDirectory(), ".env");
                    if (File.Exists(envPath))
                    {
                        DotNetEnv.Env.Load(envPath);
                    }
                }
                catch (Exception) { }

                var builder = WebApplication.CreateBuilder(args);

                // --- Logging để debug lỗi 500 trên Azure ---
                builder.Logging.ClearProviders();
                builder.Logging.AddConsole();
                builder.Logging.AddDebug();

                // 2. Configure core services
                builder.Services.AddControllers().AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.ReferenceHandler = null;
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                });

                // 3. Add HTTP context accessor
                builder.Services.AddHttpContextAccessor();

                // 4. Add MVC services
                builder.Services.AddControllersWithViews();
                builder.Services.AddRazorPages();

                // 5. Add memory cache
                builder.Services.AddMemoryCache();

                // 6. Add problem details
                builder.Services.AddProblemDetails();

                // 7. Configure SignalR
                builder.Services.AddSignalR();

                // 8. Configure Cloudinary settings cho image upload
                builder.Services.Configure<CloudinarySettings>(options =>
                {
                    options.CloudName = Environment.GetEnvironmentVariable("CLOUDINARY_CLOUDNAME") ?? "";
                    options.ApiKey = Environment.GetEnvironmentVariable("CLOUDINARY_APIKEY") ?? "";
                    options.ApiSecret = Environment.GetEnvironmentVariable("CLOUDINARY_APISECRET") ?? "";
                });

                // 9. Configure VnPay
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

                // 11. Configure CORS policy
                builder.Services.AddCors(options =>
                {
                    options.AddPolicy("AllowAll", policy =>
                    {
                        policy.SetIsOriginAllowed(origin =>
                        {
                            // Nếu origin bị null (ví dụ server gọi server), cho qua luôn
                            if (string.IsNullOrWhiteSpace(origin)) return true;

                            // Normalize origin để so sánh (lowercase)
                            var normalizedOrigin = origin.ToLowerInvariant();

                            // Cách 1: Cho phép tất cả các thể loại Localhost 
                            if (normalizedOrigin.StartsWith("http://localhost") || normalizedOrigin.StartsWith("https://localhost"))
                                return true;

                            // Cách 2: Cho phép theo đuôi tên miền (Production)
                            if (normalizedOrigin.EndsWith(".pages.dev")) return true;
                            if (normalizedOrigin.EndsWith(".azurewebsites.net")) return true;
                            if (normalizedOrigin.EndsWith(".devnguyen.xyz")) return true;
                            if (normalizedOrigin.EndsWith(".runasp.net")) return true;

                            // Mặc định chặn các cái khác
                            return false;
                        })
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials()
                        .SetPreflightMaxAge(TimeSpan.FromHours(24)); // Cache preflight requests
                    });
                });

                //builder.Services.AddCors(options =>
                //{
                //    options.AddPolicy("AllowAll", policy =>
                //    {
                //        policy.SetIsOriginAllowed(origin => true)
                //              .AllowAnyHeader()
                //              .AllowAnyMethod()
                //              .AllowCredentials();
                //    });
                //});

                // 12. Configure form options
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
                        Console.WriteLine("WARNING: Connection String is NULL!");

                    //var serverVersion = ServerVersion.AutoDetect(connectionString ?? "server=localhost;");
                    var serverVersion = new MySqlServerVersion(new Version(8, 0, 36));
                    options.UseMySql(connectionString, serverVersion,
                        mysqlOptions => mysqlOptions.MigrationsAssembly(typeof(AppDbContext).Assembly.GetName().Name));

                    options.AddInterceptors(serviceProvider.GetRequiredService<SecondLevelCacheInterceptor>());
                });

                // 14. Install custom services
                builder.Services.InstallService(builder.Configuration);

                // 15. Configure Auth
                builder.Services.ConfigureAuthService(builder.Configuration);

                // 16. Configure Swagger
                builder.Services.AddEndpointsApiExplorer();
                builder.Services.AddSwaggerGen(c =>
                {
                    c.EnableAnnotations();
                    c.OperationFilter<ApiDefaultResponseOperationFilter>();
                    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                    if (File.Exists(xmlPath)) c.IncludeXmlComments(xmlPath);
                    c.SwaggerDoc("v1", new OpenApiInfo { Title = "FSCMS API", Version = "v1" });

                    // JWT Setup
                    var securitySchema = new OpenApiSecurityScheme
                    {
                        Description = "JWT Authorization header using the Bearer scheme.",
                        Name = "Authorization",
                        In = ParameterLocation.Header,
                        Type = SecuritySchemeType.Http,
                        Scheme = "bearer",
                        Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
                    };
                    c.AddSecurityDefinition("Bearer", securitySchema);
                    c.AddSecurityRequirement(new OpenApiSecurityRequirement { { securitySchema, new[] { "Bearer" } } });
                });

                // 17. Build application
                var app = builder.Build();

                // 18. Configure middleware pipeline

                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "FSCMS API v1");
                    c.RoutePrefix = string.Empty;
                });

                // Sử dụng Developer Exception Page ở mọi nơi tạm thời để debug lỗi 500 Cors Preflight
                app.UseDeveloperExceptionPage();

                // CORS PHẢI ĐẶT TRƯỚC ROUTING ĐỂ XỬ LÝ PREFLIGHT REQUESTS
                app.UseCors("AllowAll");

                app.UseRouting();

                app.UseHttpsRedirection();

                app.UseAuthentication();
                app.UseAuthorization();

                // 19. Map Hubs
                app.MapHub<TransactionHub>("/transactionHub");
                app.MapHub<NotificationHub>("/notificationHub");

                app.MapControllers();

                // 20. Run
                app.Run();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Fatal error during application startup:");
                Console.WriteLine(ex.ToString());
                throw;
            }
        }
    }
}

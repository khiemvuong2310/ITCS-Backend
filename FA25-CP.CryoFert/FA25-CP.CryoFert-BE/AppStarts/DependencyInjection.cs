using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DinkToPdf;
using DinkToPdf.Contracts;
using FSCMS.Core.Interfaces;
using FSCMS.Core.Models.Options;
using FSCMS.Core.Services;
using FSCMS.Data.UnitOfWork;
using FSCMS.Service.Interfaces;
using FSCMS.Service.Mapping;
using FSCMS.Service.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.StackExchangeRedis;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using StackExchange.Redis;
using Twilio.Clients;

namespace FA25_CP.CryoFert_BE.AppStarts
{
    public static class DependencyInjection
    {
        public static void InstallService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddRouting(options =>
            {
                options.LowercaseUrls = true;
                options.LowercaseQueryStrings = true;
            });

            // Twilio Client (optional)
            services.AddSingleton<ITwilioRestClient>(new TwilioRestClient("ACCOUNT_SID", "AUTH_TOKEN"));
            // using DinkToPdf; using DinkToPdf.Contracts;
            services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));

            // Add AutoMapper
            services.AddAutoMapper(typeof(UserMappingProfile), typeof(DoctorMappingProfile), typeof(PatientMappingProfile));

            // Add UnitOfWork and Repository Pattern
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Configure MailServiceOptions from configuration
            services.Configure<MailServiceOptions>(options =>
            {
                configuration.GetSection("Email").Bind(options);

                // Map Email section to MailServiceOptions
                options.SmtpServer = configuration["Email:SmtpHost"] ?? "smtp.gmail.com";
                options.SmtpPort = int.Parse(configuration["Email:SmtpPort"] ?? "587");
                options.UseSsl = true;
                options.SenderEmail = configuration["Email:Sender"] ?? string.Empty;
                options.SenderName = configuration["Email:SenderName"] ?? "CryoFert - Fertility Management System";
                options.Username = configuration["Email:Sender"] ?? string.Empty;
                options.Password = configuration["Email:Password"] ?? string.Empty;
                options.TemplatesPath = "Templates"; // Templates folder path
            });

            // Register Mail Service
            services.AddScoped<IMailService, SmtpMailService>();

            // Add Service Layer
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IEmailTemplateService, EmailTemplateService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ILabSampleService, LabSampleService>();
            services.AddScoped<IRoleService, RoleService>(); // Role service with memory cache
            services.AddScoped<IDoctorService, DoctorService>(); // Doctor service with comprehensive CRUD operations
            services.AddScoped<IPatientService, PatientService>(); // Patient service with comprehensive CRUD operations
            services.AddScoped<IAppointmentService, AppointmentService>(); // Appointment service with comprehensive CRUD operations
            services.AddScoped<ICryoLocationService, CryoLocationService>();
            services.AddScoped<INotificationService, NotificationService>();
            services.AddScoped<ICryoExportService, CryoExportService>();
            services.AddScoped<ICryoImportService, CryoImportService>();
            services.AddScoped<IMediaService, MediaService>();
            services.AddScoped<ITransactionService, TransactionService>();
            services.AddScoped<ICryoStorageContractService, CryoStorageContractService>();
            services.AddScoped<ICryoPackageService, CryoPackageService>();
            services.AddScoped<IAppointmentDoctorService, AppointmentDoctorService>();
            services.AddScoped<IAgreementService, AgreementService>();
            services.AddScoped<IPrescriptionService, PrescriptionService>();
            services.AddScoped<IPrescriptionDetailService, PrescriptionDetailService>();
            services.AddScoped<IMedicineService, MedicineService>();
            services.AddScoped<IMedicalRecordService, MedicalRecordService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IOTPService, OTPService>();

            services.Configure<RedisOptions>(configuration.GetSection(RedisOptions.KeyName));
            var redisOptions = configuration.GetSection(RedisOptions.KeyName).Get<RedisOptions>();

            services.AddSingleton<IConnectionMultiplexer>(sp =>
            {
                var logger = sp.GetRequiredService<ILogger<IConnectionMultiplexer>>();

                // 1. Lấy Connection String (Ưu tiên biến môi trường)
                var connectionString = Environment.GetEnvironmentVariable("REDIS_CONNECTION_STRING") ?? redisOptions.ConnectionString;

                if (string.IsNullOrEmpty(connectionString))
                {
                    logger.LogWarning("⚠️ Không tìm thấy Redis Connection String. Bỏ qua Cache.");
                    return null;
                }

                try
                {
                    // 2. Parse cấu hình từ chuỗi
                    var config = ConfigurationOptions.Parse(connectionString, true);

                    // 3. 🔥 CẤU HÌNH BẮT BUỘC CHO REDIS CLOUD (Ghi đè cấu hình cũ) 🔥
                    config.Ssl = true;
                    config.SslProtocols = SslProtocols.Tls12; // Ép dùng TLS 1.2
                    config.AbortOnConnectFail = false;        // Giữ app sống
                    config.ConnectTimeout = 10000;            // 10 giây timeout

                    // Bỏ qua lỗi chứng chỉ (Fix lỗi Self-signed certificate)
                    config.CertificateValidation += (sender, cert, chain, errors) => true;

                    var multiplexer = ConnectionMultiplexer.Connect(config);

                    // 4. Đăng ký sự kiện để bắt LỖI NGẦM (Quan trọng)
                    multiplexer.ConnectionFailed += (sender, e) =>
                        logger.LogError($"❌ REDIS KẾT NỐI THẤT BẠI: {e.FailureType} - {e.Exception?.Message}");

                    multiplexer.ErrorMessage += (sender, e) =>
                        logger.LogError($"❌ REDIS LỖI SERVER: {e.Message}");

                    multiplexer.ConnectionRestored += (sender, e) =>
                        logger.LogInformation("✅ REDIS ĐÃ KẾT NỐI LẠI!");

                    // 5. Kiểm tra trạng thái thực tế ngay lúc này
                    if (multiplexer.IsConnected)
                    {
                        logger.LogInformation("✅ [REDIS ALIVE] Kết nối thành công & Sẵn sàng!");
                    }
                    else
                    {
                        logger.LogWarning("⚠️ [REDIS WAITING] Object đã tạo nhưng chưa thông mạng. Đang chờ handshake...");
                    }

                    return multiplexer;
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "🔥 Lỗi crash khi khởi tạo Redis.");
                    return null;
                }
            });

            services.AddSingleton<IRedisService, RedisService>();

            // CryoRequest Services - Service Management System
            services.AddScoped<IServiceCategoryService, ServiceCategoryService>(); // Service category management
            services.AddScoped<IServiceService, ServiceService>(); // Service management with category filtering
            services.AddScoped<IServiceRequestService, ServiceRequestService>(); // Service request workflow management
            services.AddScoped<IServiceRequestDetailsService, ServiceRequestDetailsService>(); // Service request details management 
            services.AddSingleton<IFileStorageService, CloudinaryStorageService>();
            services.AddScoped<PaymentGatewayService>();

            // Treatment Servicess - Fertility Treatment Management
            services.AddScoped<ITreatmentService, TreatmentService>();
            services.AddScoped<ITreatmentIVFService, TreatmentIVFService>();
            services.AddScoped<ITreatmentIUIService, TreatmentIUIService>();
            services.AddScoped<ITreatmentCycleService, TreatmentCycleService>();
        }

        /// <summary>
        /// Creates a dummy connection multiplexer that will fail gracefully when Redis is not available
        /// </summary>
        private static IConnectionMultiplexer CreateDummyConnectionMultiplexer()
        {
            // Create a configuration that will never connect
            var config = new ConfigurationOptions
            {
                EndPoints = { "localhost:0" }, // Invalid endpoint
                AbortOnConnectFail = false,
                ConnectRetry = 0,
                ConnectTimeout = 1
            };
            
            try
            {
                return ConnectionMultiplexer.Connect(config);
            }
            catch
            {
                // If even this fails, we'll handle it in RedisService
                // Return null and let RedisService handle it
                return null!;
            }
        }
    }
}

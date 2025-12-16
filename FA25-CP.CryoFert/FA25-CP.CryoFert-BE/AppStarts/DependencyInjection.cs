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
                var connectionString = redisOptions.ConnectionString;

                // Ưu tiên biến môi trường nếu có
                var envConn = Environment.GetEnvironmentVariable("REDIS_CONNECTION_STRING");
                if (!string.IsNullOrEmpty(envConn)) connectionString = envConn;

                if (string.IsNullOrEmpty(connectionString))
                {
                    logger.LogWarning("⚠️ Không tìm thấy Redis Connection String.");
                    return null;
                }

                try
                {
                    // Parse config từ chuỗi kết nối
                    var config = ConfigurationOptions.Parse(connectionString, true);

                    // Fix lỗi SSL cho Redis Cloud
                    config.CertificateValidation += (sender, cert, chain, errors) => true;

                    var multiplexer = ConnectionMultiplexer.Connect(config);
                    logger.LogInformation("✅ Kết nối Redis thành công!");
                    return multiplexer;
                }
                catch (Exception ex)
                {
                    // In lỗi chi tiết ra console để bạn đọc
                    logger.LogError($"❌ Lỗi kết nối Redis: {ex.Message}");
                    // Quan trọng: Ném lỗi ra để app dừng lại -> Bạn mới nhìn thấy lỗi.
                    // Khi chạy thật thì comment dòng throw này lại.
                    throw;
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

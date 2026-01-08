using AutoMapper;
using DinkToPdf;
using DinkToPdf.Contracts;
using EFCoreSecondLevelCacheInterceptor;
using FSCMS.Core.Extensions;
using FSCMS.Core.Interfaces;
using FSCMS.Core.Models.Options;
using FSCMS.Core.Services;
using FSCMS.Data.UnitOfWork;
using FSCMS.Service.Interfaces;
using FSCMS.Service.Mapping;
using FSCMS.Service.Payments;
using FSCMS.Service.Services;
using Google;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.StackExchangeRedis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Twilio.Clients;
using ZiggyCreatures.Caching.Fusion;

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
            services.AddScoped<PayOSService>();


            // Add AutoMapper
            services.AddAutoMapper(typeof(UserMappingProfile), typeof(DoctorMappingProfile), typeof(PatientMappingProfile));

            // Add UnitOfWork and Repository Pattern
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Configure MailServiceOptions from environment variables or configuration
            services.Configure<MailServiceOptions>(options =>
            {
                // Map Email section to MailServiceOptions (prioritize environment variables)
                options.SmtpServer = Environment.GetEnvironmentVariable("EMAIL_SMTP_HOST")
                    ?? configuration["Email:SmtpHost"]
                    ?? "smtp.gmail.com";
                options.SmtpPort = int.Parse(Environment.GetEnvironmentVariable("EMAIL_SMTP_PORT")
                    ?? configuration["Email:SmtpPort"]
                    ?? "587");
                options.UseSsl = true;
                options.SenderEmail = Environment.GetEnvironmentVariable("EMAIL_SENDER")
                    ?? configuration["Email:Sender"]
                    ?? string.Empty;
                options.SenderName = Environment.GetEnvironmentVariable("EMAIL_SENDER_NAME")
                    ?? configuration["Email:SenderName"]
                    ?? "CryoFert - Fertility Management System";
                options.Username = Environment.GetEnvironmentVariable("EMAIL_SENDER")
                    ?? configuration["Email:Sender"]
                    ?? string.Empty;
                options.Password = Environment.GetEnvironmentVariable("EMAIL_PASSWORD")
                    ?? configuration["Email:Password"]
                    ?? string.Empty;
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
            services.AddHostedService<ExpiredContractBackgroundService>();


            // Get Redis connection string from environment variable (.env) or configuration as fallback
            string redisConnectionString =
                Environment.GetEnvironmentVariable("REDIS_CONNECTION_STRING")
                ?? configuration.GetConnectionStringOrThrow("redis");

            // Configure FusionCache with Redis
            services.AddCaching(redisConnectionString);

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
        /// Configures caching system using FusionCache with Redis backend.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <param name="redisConnectionString">The Redis connection string.</param>
        /// <returns>The service collection for chaining.</returns>
        public static IServiceCollection AddCaching(this IServiceCollection services, string redisConnectionString)
        {
            // 1. Configure StackExchange Redis Cache
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = redisConnectionString;
            });

            // 2. FusionCache Serializer
            services.AddFusionCacheNewtonsoftJsonSerializer();

            // 3. FusionCache Redis Backplane
            services.AddFusionCacheStackExchangeRedisBackplane(options =>
            {
                options.Configuration = redisConnectionString;
            });

            // 4. Configure FusionCache with default options
            services.AddFusionCache()
                .WithDefaultEntryOptions(new FusionCacheEntryOptions
                {
                    Duration = TimeSpan.FromMinutes(30),
                    DistributedCacheDuration = TimeSpan.FromMinutes(30),
                    IsFailSafeEnabled = true,
                    FailSafeMaxDuration = TimeSpan.FromDays(1),
                    FailSafeThrottleDuration = TimeSpan.FromSeconds(30),

                    EagerRefreshThreshold = 0.9f,

                    FactorySoftTimeout = TimeSpan.FromMilliseconds(100),
                    FactoryHardTimeout = TimeSpan.FromMilliseconds(1500)
                })
                .WithRegisteredDistributedCache()
                .WithRegisteredSerializer()
                .WithRegisteredBackplane()
                .AsHybridCache();

            // 5. Configure EF Core Second Level Cache with Hybrid Cache Provider
            services.AddEFSecondLevelCache(options =>
            {
                options.UseHybridCacheProvider().ConfigureLogging(false).UseCacheKeyPrefix("EF_")
                       .UseDbCallsIfCachingProviderIsDown(TimeSpan.FromMinutes(1));
                options.CacheAllQueries(CacheExpirationMode.Absolute, TimeSpan.FromSeconds(30));
            });

            return services;
        }
    }
}

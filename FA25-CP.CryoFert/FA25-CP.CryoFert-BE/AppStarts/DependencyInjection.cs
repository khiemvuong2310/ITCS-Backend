using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twilio.Clients;
using FSCMS.Data.UnitOfWork;
using FSCMS.Service.Services;
using FSCMS.Service.Interfaces;
using FSCMS.Core.Interfaces;
using FSCMS.Core.Services;
using FSCMS.Core.Models.Options;
using AutoMapper;
using FSCMS.Service.Mapping;
using StackExchange.Redis;

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
            services.AddScoped<IMedicalRecordService, MedicalRecordService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IOTPService, OTPService>();

            services.Configure<RedisOptions>(options =>
            {
                configuration.GetSection(RedisOptions.KeyName).Bind(options);

                var envConnectionString = Environment.GetEnvironmentVariable("REDIS_CONNECTION_STRING");
                var envHost = Environment.GetEnvironmentVariable("REDIS_HOST");
                var envPort = Environment.GetEnvironmentVariable("REDIS_PORT");
                var envUser = Environment.GetEnvironmentVariable("REDIS_USER");
                var envPassword = Environment.GetEnvironmentVariable("REDIS_PASSWORD");
                var envSsl = Environment.GetEnvironmentVariable("REDIS_SSL");

                if (!string.IsNullOrWhiteSpace(envConnectionString))
                {
                    options.ConnectionString = envConnectionString;
                }

                if (!string.IsNullOrWhiteSpace(envHost))
                {
                    options.Host = envHost;
                }

                if (int.TryParse(envPort, out var parsedPort))
                {
                    options.Port = parsedPort;
                }

                if (!string.IsNullOrWhiteSpace(envUser))
                {
                    options.User = envUser;
                }

                if (!string.IsNullOrWhiteSpace(envPassword))
                {
                    options.Password = envPassword;
                }

                if (bool.TryParse(envSsl, out var parsedSsl))
                {
                    options.UseSsl = parsedSsl;
                }

                options.ConnectionString ??= configuration.GetConnectionString("RedisConnection");

                if (string.IsNullOrWhiteSpace(options.ConnectionString) &&
                    (string.IsNullOrWhiteSpace(options.Host) || !options.Port.HasValue))
                {
                    throw new InvalidOperationException("Redis is not configured. Provide REDIS_CONNECTION_STRING or REDIS_HOST/REDIS_PORT in environment variables or appsettings.");
                }
            });

            services.AddSingleton<IConnectionMultiplexer>(sp =>
            {
                var redisOptions = sp.GetRequiredService<IOptions<RedisOptions>>().Value;

                ConfigurationOptions configurationOptions;

                if (!string.IsNullOrWhiteSpace(redisOptions.ConnectionString))
                {
                    configurationOptions = ConfigurationOptions.Parse(redisOptions.ConnectionString, true);
                }
                else
                {
                    if (string.IsNullOrWhiteSpace(redisOptions.Host) || !redisOptions.Port.HasValue)
                    {
                        throw new InvalidOperationException("Redis host/port configuration is missing.");
                    }

                    configurationOptions = new ConfigurationOptions();
                    configurationOptions.EndPoints.Add(redisOptions.Host, redisOptions.Port.Value);
                    if (!string.IsNullOrWhiteSpace(redisOptions.User))
                    {
                        configurationOptions.User = redisOptions.User;
                    }

                    if (!string.IsNullOrWhiteSpace(redisOptions.Password))
                    {
                        configurationOptions.Password = redisOptions.Password;
                    }

                    configurationOptions.Ssl = redisOptions.UseSsl;
                }

                configurationOptions.AbortOnConnectFail = redisOptions.AbortOnConnectFail;
                configurationOptions.ConnectRetry = redisOptions.ConnectRetry;
                configurationOptions.ConnectTimeout = redisOptions.ConnectTimeout;

                return ConnectionMultiplexer.Connect(configurationOptions);
            });

            services.AddScoped<IRedisService, RedisService>();

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
    }
}

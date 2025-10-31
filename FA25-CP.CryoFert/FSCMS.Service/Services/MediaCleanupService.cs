using FSCMS.Core.Entities;
using FSCMS.Data.UnitOfWork;
using FSCMS.Service.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace FSCMS.Service.Services
{
    public class MediaCleanupService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<MediaCleanupService> _logger;

        public MediaCleanupService(IServiceProvider serviceProvider, ILogger<MediaCleanupService> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("MediaCleanupService started.");

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using var scope = _serviceProvider.CreateScope();
                    var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
                    var firebaseService = scope.ServiceProvider.GetRequiredService<IFileStorageService>();

                    var expired = unitOfWork.Repository<Media>()
                        .AsQueryable()
                        .Where(m => m.IsDeleted && m.DeletedAt < DateTime.UtcNow.AddDays(-30))
                        .ToList();

                    foreach (var file in expired)
                    {
                        await firebaseService.DeleteFileAsync(file.FilePath);
                        await unitOfWork.Repository<Media>().DeleteAsync(file);
                    }

                    await unitOfWork.SaveChangesAsync();
                    _logger.LogInformation($"Cleaned up {expired.Count} expired media files.");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error while cleaning up expired media files.");
                }

                // Chờ 24h trước khi chạy lại
                await Task.Delay(TimeSpan.FromHours(24), stoppingToken);
            }
        }
    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FSCMS.Core.Entities;
using FSCMS.Core.Enum;
using FSCMS.Core.Enums;
using FSCMS.Data.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace FSCMS.Service.Services
{
    public class ExpiredContractBackgroundService : BackgroundService
    {
        private readonly ILogger<ExpiredContractBackgroundService> _logger;
        private readonly IServiceProvider _serviceProvider;
        private readonly TimeSpan _interval = TimeSpan.FromDays(1);

        public ExpiredContractBackgroundService(
            ILogger<ExpiredContractBackgroundService> logger,
            IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("ExpiredContractBackgroundService started.");

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using (var scope = _serviceProvider.CreateScope())
                    {
                        var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
                        await ProcessExpiredContracts(unitOfWork);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error processing expired contracts.");
                }

                await Task.Delay(_interval, stoppingToken);
            }
        }

        // Thay đổi ProcessExpiredContracts để nhận IUnitOfWork
        private async Task ProcessExpiredContracts(IUnitOfWork unitOfWork)
        {
            var now = DateTime.UtcNow;

            // Lấy hợp đồng chưa hết hạn
            var contracts = await unitOfWork.Repository<CryoStorageContract>()
                .AsQueryable()
                .Where(c => (c.Status == ContractStatus.Active || c.Status == ContractStatus.Renewed) &&
                            !c.IsDeleted)
                .Include(c => c.CPSDetails)
                .ToListAsync();

            foreach (var contract in contracts)
            {
                bool isExpired = contract.EndDate < now;
                if (isExpired)
                {
                    contract.Status = ContractStatus.Expired;
                    contract.UpdatedAt = now;
                    foreach (var detail in contract.CPSDetails)
                    {
                        detail.Status = "Expired";
                        detail.UpdatedAt = now;
                        await unitOfWork.Repository<CPSDetail>().UpdateGuid(detail, detail.Id);
                        var sample = await unitOfWork.Repository<LabSample>()
                        .AsQueryable()
                        .FirstOrDefaultAsync(p => p.Id == detail.LabSampleId && !p.IsDeleted);
                        if (sample != null)
                        {
                            sample.Status = SpecimenStatus.Expired;
                            sample.UpdatedAt = now;
                            await unitOfWork.Repository<LabSample>().UpdateGuid(sample, sample.Id);
                        }
                    }
                }
                else
                {
                    var dayToExpired = contract.EndDate.Value - now;
                    if (dayToExpired.TotalDays < 30)
                    {
                        var noti = new Notification(
            Guid.NewGuid(),
            "Warning Expired Contract",
            $"Contract {contract.ContractNumber} has {dayToExpired.Days} days left to expire. Please check!",
            NotificationType.CryoStorageContract);
                        noti.Status = NotificationStatus.Sent;
                        noti.PatientId = contract.PatientId;
                        noti.SentTime = DateTime.UtcNow;
                        noti.Channel = "Contract";
                        noti.RelatedEntityType = "CryoStorageContract";
                        noti.RelatedEntityId = contract.Id;
                        await unitOfWork.Repository<Notification>().InsertAsync(noti);
                    }
                }
            }

            if (contracts.Any())
            {
                await unitOfWork.CommitAsync();
                var expiredCount = contracts.Count(c => c.Status == ContractStatus.Expired);
                _logger.LogInformation($"{expiredCount} contracts expired and updated.");
            }
        }
    }
}

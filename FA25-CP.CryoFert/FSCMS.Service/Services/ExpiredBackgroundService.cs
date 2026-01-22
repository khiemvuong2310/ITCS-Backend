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
    public class ExpiredBackgroundService : BackgroundService
    {
        private readonly ILogger<ExpiredBackgroundService> _logger;
        private readonly IServiceProvider _serviceProvider;
        private readonly TimeSpan _interval = TimeSpan.FromDays(1);

        public ExpiredBackgroundService(
            ILogger<ExpiredBackgroundService> logger,
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
                        await ProcessExpiredAppoinments(unitOfWork);
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

        private async Task ProcessExpiredAppoinments(IUnitOfWork unitOfWork)
        {
            var now = DateTime.UtcNow;
            var today = DateOnly.FromDateTime(now);

            var appointments = await unitOfWork.Repository<Appointment>()
                .AsQueryable()
                .Where(c => c.Status == AppointmentStatus.Scheduled &&
                            !c.IsDeleted)
                .ToListAsync();

            foreach (var appointment in appointments)
            {
                bool isExpired = appointment.AppointmentDate < today;
                if (isExpired)
                {
                    appointment.Status = AppointmentStatus.Cancelled;
                    appointment.UpdatedAt = now;
                }
                else
                {
                    var daysToAppointment = appointment.AppointmentDate.DayNumber - today.DayNumber;

                    if (daysToAppointment < 3 && daysToAppointment >= 0)
                    {
                        var noti = new Notification(
            Guid.NewGuid(),
            "Upcoming Appointment",
            $"Your appointment is in {daysToAppointment} day(s) on {appointment.AppointmentDate:dd/MM/yyyy}. Please prepare accordingly.",
            NotificationType.Appointment);
                        noti.Status = NotificationStatus.Sent;
                        noti.PatientId = appointment.PatientId;
                        noti.SentTime = DateTime.UtcNow;
                        noti.Channel = "Appointment";
                        noti.RelatedEntityType = "Appointment";
                        noti.RelatedEntityId = appointment.Id;
                        await unitOfWork.Repository<Notification>().InsertAsync(noti);
                    }
                }
            }

            if (appointments.Any())
            {
                await unitOfWork.CommitAsync();
                var expiredCount = appointments.Count(c => c.Status == AppointmentStatus.Cancelled);
                _logger.LogInformation($"{expiredCount} appointments expired and updated.");
            }
        }
    }
}

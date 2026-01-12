using System;
using System.ComponentModel.DataAnnotations;
using FSCMS.Service.ReponseModel;
using FSCMS.Core.Enum;

namespace FSCMS.Service.RequestModel
{
    public class CreateNotificationRequest
    {
        [Required(ErrorMessage = "Title is required.")]
        [StringLength(200, ErrorMessage = "Title cannot exceed 200 characters.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Content is required.")]
        [StringLength(2000, ErrorMessage = "Content cannot exceed 2000 characters.")]
        public string Content { get; set; }

        [Required(ErrorMessage = "Notification type is required.")]
        public NotificationType Type { get; set; }
        public DateTime? ScheduledTime { get; set; }

        [StringLength(50, ErrorMessage = "Channel cannot exceed 50 characters.")]
        public string? Channel { get; set; }

        [Required(ErrorMessage = "RelatedEntityId is required.")]
        public Guid RelatedEntityId { get; set; } // ID của MedicalRecord, Patient, Treatment, etc.
        [Required(ErrorMessage = "RelatedEntityType is required.")]
        public EntityTypeNotification RelatedEntityType { get; set; } // "MedicalRecord", "Patient", "Treatment", "LabTest"

        public bool? IsImportant { get; set; } = false;

        [StringLength(500, ErrorMessage = "Notes cannot exceed 500 characters.")]
        public string? Notes { get; set; }
    }

    public class UpdateNotificationRequest
    {
        [StringLength(200, ErrorMessage = "Title cannot exceed 200 characters.")]
        public string? Title { get; set; }

        [StringLength(2000, ErrorMessage = "Content cannot exceed 2000 characters.")]
        public string? Content { get; set; }

        public NotificationType? Type { get; set; }

        public Guid? PatientId { get; set; }

        public Guid? UserId { get; set; }

        public NotificationStatus? Status { get; set; }

        public DateTime? ScheduledTime { get; set; }

        public DateTime? SentTime { get; set; }

        public DateTime? ReadTime { get; set; }

        [StringLength(50, ErrorMessage = "Channel cannot exceed 50 characters.")]
        public string? Channel { get; set; }

        [StringLength(100, ErrorMessage = "RelatedEntityType cannot exceed 100 characters.")]
        public string? RelatedEntityType { get; set; }

        public Guid? RelatedEntityId { get; set; }

        public bool? IsImportant { get; set; }

        [StringLength(500, ErrorMessage = "Notes cannot exceed 500 characters.")]
        public string? Notes { get; set; }
    }

    public class GetNotificationByIdRequest
    {
        [Required(ErrorMessage = "Notification ID is required.")]
        public Guid Id { get; set; }
    }

    public class GetNotificationsRequest : PagingModel
    {
        public string? SearchTerm { get; set; }
        public Guid? PatientId { get; set; }
        public Guid? UserId { get; set; }
        public NotificationType? Type { get; set; }
        public NotificationStatus? Status { get; set; }
        public bool? IsImportant { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }

    public enum EntityTypeNotification
    {
        MedicalRecord = 0,
        TreatmentCycle = 1,
        Account = 2,
        Agreement = 3,
        CryoStorageContract = 4,
        Treatment = 5,
        CryoImport = 6,
        CryoExport = 7,
        ServiceRequest = 8,
        Appointment = 9,
        Relationship = 10
    }
}

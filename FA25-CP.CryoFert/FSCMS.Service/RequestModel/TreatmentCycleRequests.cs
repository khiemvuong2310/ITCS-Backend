using System.ComponentModel.DataAnnotations;
using FSCMS.Core.Enum;

namespace FSCMS.Service.RequestModel
{
    public class CreateTreatmentCycleRequest
    {
        [Required]
        public Guid TreatmentId { get; set; }

        [Required]
        [MaxLength(200)]
        public string CycleName { get; set; } = string.Empty;

        [Required]
        [Range(1, 100)]
        public int CycleNumber { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }
        public string? Protocol { get; set; }
        public string? Notes { get; set; }
        public decimal? Cost { get; set; }
    }

    public class UpdateTreatmentCycleRequest
    {
        [MaxLength(200)]
        public string? CycleName { get; set; }
        [Range(1, 100)]
        public int? CycleNumber { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public TreatmentStatus? Status { get; set; }
        public string? Protocol { get; set; }
        public string? Notes { get; set; }
        public decimal? Cost { get; set; }

        // Allow admin override to edit Completed
        public bool IsAdminOverride { get; set; }
    }

    public class StartTreatmentCycleRequest
    {
        public DateTime? StartDate { get; set; }
        public string? Notes { get; set; }
    }

    public class CompleteTreatmentCycleRequest
    {
        public DateTime? EndDate { get; set; }
        [Required]
        public string Outcome { get; set; } = string.Empty;
        public string? Notes { get; set; }
    }

    public class CancelTreatmentCycleRequest
    {
        [Required]
        public string Reason { get; set; } = string.Empty;
        public string? Notes { get; set; }
    }

    // Samples
    public class AddCycleSampleRequest
    {
        [Required]
        public string SampleCode { get; set; } = string.Empty;
        [Required]
        public string SampleType { get; set; } = string.Empty; // mapped in service
        [Required]
        public DateTime CollectionDate { get; set; }
        public string? Notes { get; set; }
    }

    // Appointments
    public class AddCycleAppointmentRequest
    {
        [Required]
        public DateOnly AppointmentDate { get; set; }
        [Required]
        public string Type { get; set; } = string.Empty; // AppointmentType
        public string? Reason { get; set; }
        public string? Instructions { get; set; }
        public string? Notes { get; set; }
        public Guid? SlotId { get; set; }
    }

    // Documents
    public class UploadCycleDocumentRequest
    {
        [Required]
        public string FileName { get; set; } = string.Empty;
        [Required]
        public string FilePath { get; set; } = string.Empty;
        [Required]
        public string FileType { get; set; } = string.Empty;
        [Required]
        public long FileSize { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Category { get; set; }
    }
}



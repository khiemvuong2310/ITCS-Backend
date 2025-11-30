using FSCMS.Core.Enum;

namespace FSCMS.Service.ReponseModel
{
    public class TreatmentCycleResponseModel
    {
        public Guid Id { get; set; }
        public Guid TreatmentId { get; set; }
        public string CycleName { get; set; } = string.Empty;
        public int CycleNumber { get; set; }
        public TreatmentStepType StepType { get; set; }
        public int ExpectedDurationDays { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public TreatmentStatus Status { get; set; }
        public string? Protocol { get; set; }
        public string? Notes { get; set; }
        public decimal? Cost { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }

    public class TreatmentCycleDetailResponseModel : TreatmentCycleResponseModel
    {
        public Guid PatientId { get; set; }
        public Guid DoctorId { get; set; }
        public string? TreatmentName { get; set; }
        public List<AppointmentSummary> Appointments { get; set; } = new();
        public List<DocumentSummary> Documents { get; set; } = new();
    }

    public class AppointmentSummary
    {
        public Guid Id { get; set; }
        public DateOnly AppointmentDate { get; set; }
        public string Type { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
    }

    public class DocumentSummary
    {
        public Guid Id { get; set; }
        public string FileName { get; set; } = string.Empty;
        public string FileType { get; set; } = string.Empty;
        public long FileSize { get; set; }
        public string? Category { get; set; }
        public DateTime? UploadDate { get; set; }
    }

    public class TreatmentCycleBillingResponse
    {
        public Guid TreatmentCycleId { get; set; }
        public decimal? EstimatedCost { get; set; }
        public decimal TotalPaid { get; set; }
        public decimal Outstanding => Math.Max(0, (EstimatedCost ?? 0) - TotalPaid);
        public List<BillingItem> Items { get; set; } = new();
    }

    public class BillingItem
    {
        public string Description { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public DateTime? Date { get; set; }
        public string? Reference { get; set; }
    }
}



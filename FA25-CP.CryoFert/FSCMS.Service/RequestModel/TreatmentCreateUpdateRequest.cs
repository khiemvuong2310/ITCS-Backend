using FSCMS.Core.Enum;
using System.ComponentModel.DataAnnotations;

namespace FSCMS.Service.RequestModel
{
    public class TreatmentCreateUpdateRequest
    {
        [Required]
        public Guid PatientId { get; set; }

        [Required]
        public Guid DoctorId { get; set; }

        [Required]
        [MaxLength(200)]
        public string TreatmentName { get; set; } = string.Empty;

        [Required]
        public TreatmentType TreatmentType { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }
        public TreatmentStatus? Status { get; set; }
        public string? Diagnosis { get; set; }
        public string? Goals { get; set; }
        public string? Notes { get; set; }
        public decimal? EstimatedCost { get; set; }
        public decimal? ActualCost { get; set; }
    }
}



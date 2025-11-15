using FSCMS.Core.Enum;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FSCMS.Service.RequestModel
{
    /// <summary>
    /// Request model for updating Treatment - all fields are optional for partial updates
    /// </summary>
    public class TreatmentUpdateRequest
    {
        public Guid? PatientId { get; set; }

        public Guid? DoctorId { get; set; }

        [MaxLength(200)]
        public string? TreatmentName { get; set; }

        public TreatmentType? TreatmentType { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }
        public TreatmentStatus? Status { get; set; }
        public string? Diagnosis { get; set; }
        public string? Goals { get; set; }
        public string? Notes { get; set; }
        public decimal? EstimatedCost { get; set; }
        public decimal? ActualCost { get; set; }

        // Optional IUI data - used when TreatmentType is IUI
        public TreatmentIUICreateUpdateRequest? IUI { get; set; }

        // Optional IVF data - used when TreatmentType is IVF
        public TreatmentIVFCreateUpdateRequest? IVF { get; set; }
    }

    /// <summary>
    /// Request model for updating Treatment status only
    /// </summary>
    public class UpdateTreatmentStatusRequest
    {
        [Required(ErrorMessage = "Status is required.")]
        [JsonPropertyName("status")]
        public TreatmentStatus Status { get; set; }
    }
}


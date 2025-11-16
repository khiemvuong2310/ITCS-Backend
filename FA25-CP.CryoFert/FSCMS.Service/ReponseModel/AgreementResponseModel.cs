using System;
using System.Text.Json.Serialization;
using FSCMS.Core.Enum;

namespace FSCMS.Service.ReponseModel
{
    /// <summary>
    /// Response model for agreement information
    /// </summary>
    public class AgreementResponse
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("agreementCode")]
        public string AgreementCode { get; set; } = string.Empty;

        [JsonPropertyName("treatmentId")]
        public Guid TreatmentId { get; set; }

        [JsonPropertyName("treatmentName")]
        public string? TreatmentName { get; set; }

        [JsonPropertyName("patientId")]
        public Guid PatientId { get; set; }

        [JsonPropertyName("patientName")]
        public string? PatientName { get; set; }

        [JsonPropertyName("startDate")]
        public DateTime StartDate { get; set; }

        [JsonPropertyName("endDate")]
        public DateTime? EndDate { get; set; }

        [JsonPropertyName("totalAmount")]
        public decimal TotalAmount { get; set; }

        [JsonPropertyName("status")]
        public AgreementStatus Status { get; set; }

        [JsonPropertyName("statusName")]
        public string StatusName => Status.ToString();

        [JsonPropertyName("signedByPatient")]
        public bool SignedByPatient { get; set; }

        [JsonPropertyName("signedByDoctor")]
        public bool SignedByDoctor { get; set; }

        [JsonPropertyName("fileUrl")]
        public string? FileUrl { get; set; }

        [JsonPropertyName("createdAt")]
        public DateTime CreatedAt { get; set; }

        [JsonPropertyName("updatedAt")]
        public DateTime? UpdatedAt { get; set; }
    }

    /// <summary>
    /// Detailed response model for agreement with full information
    /// </summary>
    public class AgreementDetailResponse : AgreementResponse
    {
        [JsonPropertyName("treatment")]
        public TreatmentBasicInfo? Treatment { get; set; }

        [JsonPropertyName("patient")]
        public PatientBasicInfo? Patient { get; set; }
    }
}


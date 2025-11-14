using System;
using System.Collections.Generic;

namespace FSCMS.Service.ReponseModel
{
    public class PrescriptionResponse
    {
        public Guid Id { get; set; }

        public Guid MedicalRecordId { get; set; }
        public string? MedicalRecordDiagnosis { get; set; }

        public DateTime PrescriptionDate { get; set; }
        public string? Diagnosis { get; set; }
        public string? Instructions { get; set; }
        public string? Notes { get; set; }

        public bool IsFilled { get; set; }
        public DateTime? FilledDate { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
    public class PrescriptionDetailResponse : PrescriptionResponse
    {
        public List<PrescriptionItemResponse>? PrescriptionDetails { get; set; }
    }
    public class PrescriptionItemResponse
    {
        public Guid Id { get; set; }

        public Guid MedicineId { get; set; }
        public string? MedicineName { get; set; }
        public string? Dosage { get; set; }
        public string? Form { get; set; }

        public int Quantity { get; set; }
        public string? Frequency { get; set; }
        public int? DurationDays { get; set; }
        public string? Instructions { get; set; }
        public string? Notes { get; set; }
    }
}

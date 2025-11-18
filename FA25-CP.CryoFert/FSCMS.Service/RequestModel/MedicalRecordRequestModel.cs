using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FSCMS.Service.ReponseModel;

namespace FSCMS.Service.RequestModel
{
    public class CreateMedicalRecordRequest
    {
        [Required(ErrorMessage = "AppointmentId is required.")]
        public Guid AppointmentId { get; set; }

        [StringLength(500, ErrorMessage = "ChiefComplaint cannot exceed 500 characters.")]
        public string? ChiefComplaint { get; set; }

        [StringLength(2000, ErrorMessage = "History cannot exceed 2000 characters.")]
        public string? History { get; set; }

        [StringLength(2000, ErrorMessage = "PhysicalExamination cannot exceed 2000 characters.")]
        public string? PhysicalExamination { get; set; }

        [StringLength(1000, ErrorMessage = "Diagnosis cannot exceed 1000 characters.")]
        public string? Diagnosis { get; set; }

        [StringLength(2000, ErrorMessage = "TreatmentPlan cannot exceed 2000 characters.")]
        public string? TreatmentPlan { get; set; }

        [StringLength(2000, ErrorMessage = "FollowUpInstructions cannot exceed 2000 characters.")]
        public string? FollowUpInstructions { get; set; }

        [StringLength(500, ErrorMessage = "VitalSigns cannot exceed 500 characters.")]
        public string? VitalSigns { get; set; }

        [StringLength(2000, ErrorMessage = "LabResults cannot exceed 2000 characters.")]
        public string? LabResults { get; set; }

        [StringLength(2000, ErrorMessage = "ImagingResults cannot exceed 2000 characters.")]
        public string? ImagingResults { get; set; }

        [StringLength(2000, ErrorMessage = "Notes cannot exceed 2000 characters.")]
        public string? Notes { get; set; }
    }

    public class UpdateMedicalRecordRequest
    {
        [Required(ErrorMessage = "MedicalRecordId is required.")]
        public Guid MedicalRecordId { get; set; }

        [StringLength(500, ErrorMessage = "ChiefComplaint cannot exceed 500 characters.")]
        public string? ChiefComplaint { get; set; }

        [StringLength(2000, ErrorMessage = "History cannot exceed 2000 characters.")]
        public string? History { get; set; }

        [StringLength(2000, ErrorMessage = "PhysicalExamination cannot exceed 2000 characters.")]
        public string? PhysicalExamination { get; set; }

        [StringLength(1000, ErrorMessage = "Diagnosis cannot exceed 1000 characters.")]
        public string? Diagnosis { get; set; }

        [StringLength(2000, ErrorMessage = "TreatmentPlan cannot exceed 2000 characters.")]
        public string? TreatmentPlan { get; set; }

        [StringLength(2000, ErrorMessage = "FollowUpInstructions cannot exceed 2000 characters.")]
        public string? FollowUpInstructions { get; set; }

        [StringLength(500, ErrorMessage = "VitalSigns cannot exceed 500 characters.")]
        public string? VitalSigns { get; set; }

        [StringLength(2000, ErrorMessage = "LabResults cannot exceed 2000 characters.")]
        public string? LabResults { get; set; }

        [StringLength(2000, ErrorMessage = "ImagingResults cannot exceed 2000 characters.")]
        public string? ImagingResults { get; set; }

        [StringLength(2000, ErrorMessage = "Notes cannot exceed 2000 characters.")]
        public string? Notes { get; set; }
    }

    public class SearchMedicalRecordRequest : PagingModel
    {
        public Guid? PatientId { get; set; }
        public Guid? AppointmentId { get; set; }

        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }

        public string? Diagnosis { get; set; }
        public string? TreatmentPlan { get; set; }
        public string? SearchTerm { get; set; } // Tìm chung: diagnosis, notes, complaint...

        public bool? HasPrescription { get; set; }
    }
}

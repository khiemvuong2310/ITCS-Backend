using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FSCMS.Service.ReponseModel;

namespace FSCMS.Service.RequestModel
{
    // Request tạo mới Prescription
    public class CreatePrescriptionRequest
    {
        [Required(ErrorMessage = "MedicalRecordId is required.")]
        public Guid MedicalRecordId { get; set; }

        [Required(ErrorMessage = "PrescriptionDate is required.")]
        public DateTime PrescriptionDate { get; set; }

        [StringLength(500, ErrorMessage = "Diagnosis cannot exceed 500 characters.")]
        public string? Diagnosis { get; set; }

        [StringLength(500, ErrorMessage = "Instructions cannot exceed 500 characters.")]
        public string? Instructions { get; set; }

        [StringLength(500, ErrorMessage = "Notes cannot exceed 500 characters.")]
        public string? Notes { get; set; }

        public List<CreatePrescriptionDetailRequest>? PrescriptionDetails { get; set; }
    }

    // Request cập nhật Prescription
    public class UpdatePrescriptionRequest
    {
        [StringLength(500, ErrorMessage = "Diagnosis cannot exceed 500 characters.")]
        public string? Diagnosis { get; set; }

        [StringLength(500, ErrorMessage = "Instructions cannot exceed 500 characters.")]
        public string? Instructions { get; set; }

        [StringLength(500, ErrorMessage = "Notes cannot exceed 500 characters.")]
        public string? Notes { get; set; }

        public List<CreatePrescriptionDetailRequest>? PrescriptionDetails { get; set; }
    }

    // Request lấy danh sách Prescription với paging/filter
    public class GetPrescriptionsRequest : PagingModel
    {
        public Guid? MedicalRecordId { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public string? SearchTerm { get; set; }
    }

    // Request tạo chi tiết PrescriptionDetail
    public class CreatePrescriptionDetailRequest
    {
        [Required(ErrorMessage = "MedicineId is required.")]
        public Guid MedicineId { get; set; }

        [Required(ErrorMessage = "Quantity is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than 0.")]
        public int Quantity { get; set; }

        [StringLength(100, ErrorMessage = "Dosage cannot exceed 100 characters.")]
        public string? Dosage { get; set; }

        [StringLength(100, ErrorMessage = "Frequency cannot exceed 100 characters.")]
        public string? Frequency { get; set; }

        [Range(0, 365, ErrorMessage = "DurationDays must be between 0 and 365.")]
        public int? DurationDays { get; set; }

        [StringLength(500, ErrorMessage = "Instructions cannot exceed 500 characters.")]
        public string? Instructions { get; set; }

        [StringLength(500, ErrorMessage = "Notes cannot exceed 500 characters.")]
        public string? Notes { get; set; }
    }
}

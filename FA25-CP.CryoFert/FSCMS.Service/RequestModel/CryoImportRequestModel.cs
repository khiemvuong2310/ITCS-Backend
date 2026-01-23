using System;
using System.ComponentModel.DataAnnotations;
using FSCMS.Service.ReponseModel;

namespace FSCMS.Service.RequestModel
{
    public class CreateCryoImportRequest
    {
        [Required(ErrorMessage = "LabSampleId is required.")]
        public Guid LabSampleId { get; set; }

        [Required(ErrorMessage = "CryoLocationId is required.")]
        public Guid CryoLocationId { get; set; }

        [Required(ErrorMessage = "ImportDate is required.")]
        public DateTime ImportDate { get; set; }

        public Guid? ImportedBy { get; set; }
        public Guid? WitnessedBy { get; set; }

        [Range(-200, 0, ErrorMessage = "Temperature must be between -200 and 0.")]
        public decimal? Temperature { get; set; }

        [StringLength(500, ErrorMessage = "Reason cannot exceed 500 characters.")]
        public string? Reason { get; set; }

        [StringLength(1000, ErrorMessage = "Notes cannot exceed 1000 characters.")]
        public string? Notes { get; set; }
    }

    public class UpdateCryoImportRequest
    {
        public DateTime? ImportDate { get; set; }
        public Guid? ImportedBy { get; set; }
        public Guid? WitnessedBy { get; set; }

        [Range(-100, 100, ErrorMessage = "Temperature must be between -100 and 100.")]
        public decimal? Temperature { get; set; }

        [StringLength(500, ErrorMessage = "Reason cannot exceed 500 characters.")]
        public string? Reason { get; set; }

        [StringLength(1000, ErrorMessage = "Notes cannot exceed 1000 characters.")]
        public string? Notes { get; set; }
    }

    public class GetCryoImportsRequest : PagingModel
    {
        public Guid? LabSampleId { get; set; }
        public Guid? CryoLocationId { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }
}

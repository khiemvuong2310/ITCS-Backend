using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FSCMS.Service.ReponseModel;

namespace FSCMS.Service.RequestModel
{
    public class CreateCryoExportRequest
    {
        [Required(ErrorMessage = "LabSampleId is required.")]
        public Guid LabSampleId { get; set; }

        [Required(ErrorMessage = "CryoLocationId is required.")]
        public Guid CryoLocationId { get; set; }

        [Required(ErrorMessage = "ExportDate is required.")]
        public DateTime ExportDate { get; set; }

        public Guid? ExportedBy { get; set; }
        public Guid? WitnessedBy { get; set; }

        [StringLength(500, ErrorMessage = "Reason cannot exceed 500 characters.")]
        public string? Reason { get; set; }

        [StringLength(200, ErrorMessage = "Destination cannot exceed 200 characters.")]
        public string? Destination { get; set; }

        [StringLength(1000, ErrorMessage = "Notes cannot exceed 1000 characters.")]
        public string? Notes { get; set; }

        public bool? IsThawed { get; set; } = false;
        public DateTime? ThawingDate { get; set; }

        [StringLength(500, ErrorMessage = "ThawingResult cannot exceed 500 characters.")]
        public string? ThawingResult { get; set; }
    }

    public class UpdateCryoExportRequest
    {
        public DateTime? ExportDate { get; set; }
        public Guid? ExportedBy { get; set; }
        public Guid? WitnessedBy { get; set; }

        [StringLength(500, ErrorMessage = "Reason cannot exceed 500 characters.")]
        public string? Reason { get; set; }

        [StringLength(200, ErrorMessage = "Destination cannot exceed 200 characters.")]
        public string? Destination { get; set; }

        [StringLength(1000, ErrorMessage = "Notes cannot exceed 1000 characters.")]
        public string? Notes { get; set; }

        public bool? IsThawed { get; set; }
        public DateTime? ThawingDate { get; set; }

        [StringLength(500, ErrorMessage = "ThawingResult cannot exceed 500 characters.")]
        public string? ThawingResult { get; set; }
    }

    public class GetCryoExportsRequest : PagingModel
    {
        public Guid? LabSampleId { get; set; }
        public Guid? CryoLocationId { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public bool? IsThawed { get; set; }
    }
}

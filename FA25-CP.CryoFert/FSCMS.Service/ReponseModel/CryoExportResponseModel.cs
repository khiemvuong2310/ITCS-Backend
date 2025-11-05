using System;

namespace FSCMS.Service.ReponseModel
{    public class CryoExportResponse
    {
        public Guid Id { get; set; }
        public Guid LabSampleId { get; set; }
        public Guid CryoLocationId { get; set; }
        public DateTime ExportDate { get; set; }
        public Guid? ExportedBy { get; set; }
        public Guid? WitnessedBy { get; set; }
        public string? Reason { get; set; }
        public string? Destination { get; set; }
        public string? Notes { get; set; }
        public bool IsThawed { get; set; }
        public DateTime? ThawingDate { get; set; }
        public string? ThawingResult { get; set; }
        public LabSampleResponse? LabSample { get; set; }
        public CryoLocationResponse? CryoLocation { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}

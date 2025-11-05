using System;

namespace FSCMS.Service.ReponseModel
{
    public class CryoImportResponse
    {
        public Guid Id { get; set; }
        public Guid LabSampleId { get; set; }
        public Guid CryoLocationId { get; set; }
        public DateTime ImportDate { get; set; }
        public Guid? ImportedBy { get; set; }
        public Guid? WitnessedBy { get; set; }
        public decimal? Temperature { get; set; }
        public string? Reason { get; set; }
        public string? Notes { get; set; }
        public LabSampleResponse? LabSample { get; set; }
        public CryoLocationResponse? CryoLocation { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}

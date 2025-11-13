using System;
using FSCMS.Core.Models.Bases;

namespace FSCMS.Core.Entities
{
    // Bảng LabSampleSperm: Thông tin chi tiết cho mẫu tinh trùng.
    // Quan hệ:
    // - 1-1 tới LabSample (LabSampleId)
    public class LabSampleSperm : BaseEntity<Guid>
    {
        public LabSampleSperm() : base() { }

        public LabSampleSperm(
            Guid id
        )
        {
            Id = id;
        }
        public LabSampleSperm(
            Guid id,
            Guid labSampleId,
            decimal? volume,
            decimal? concentration
        )
        {
            Id = id;
            LabSampleId = labSampleId;
            Volume = volume;
            Concentration = concentration;
        }
        public Guid LabSampleId { get; set; }
        public decimal? Volume { get; set; }
        public decimal? Concentration { get; set; }
        public decimal? Motility { get; set; }
        public decimal? ProgressiveMotility { get; set; }
        public decimal? Morphology { get; set; }
        public decimal? PH { get; set; }
        public string? Viscosity { get; set; }
        public string? Liquefaction { get; set; }
        public string? Color { get; set; }
        public int? TotalSpermCount { get; set; }
        public string? Notes { get; set; }
        public virtual LabSample? LabSample { get; set; }
    }
}

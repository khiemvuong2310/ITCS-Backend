using System;
using FSCMS.Core.Models.Bases;

namespace FSCMS.Core.Entities
{
    public class LabSampleOocyte : BaseEntity<Guid>
    {
        protected LabSampleOocyte() : base() { }
        public LabSampleOocyte(
            Guid id,
            Guid labSampleId,
            string maturityStage,
            string? quality
        )
        {
            Id = id;
            LabSampleId = labSampleId;
            MaturityStage = maturityStage;
            Quality = quality;
        }
        public Guid LabSampleId { get; set; }
        public string MaturityStage { get; set; } = default!;
        public string? Quality { get; set; }
        public bool IsMature { get; set; }
        public DateTime? RetrievalDate { get; set; }
        public string? CumulusCells { get; set; }
        public string? CytoplasmAppearance { get; set; }
        public bool IsVitrified { get; set; } = false;
        public DateTime? VitrificationDate { get; set; }
        public string? Notes { get; set; }
        public virtual LabSample? LabSample { get; set; }
    }
}

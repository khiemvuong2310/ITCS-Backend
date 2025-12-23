using System;
using FSCMS.Core.Models.Bases;

namespace FSCMS.Core.Entities
{
    // Bảng LabSampleEmbryo: Thông tin chi tiết cho mẫu phôi.
    // Quan hệ:
    // - 1-1 tới LabSample (LabSampleId)
    public class LabSampleEmbryo : BaseEntity<Guid>
    {
        protected LabSampleEmbryo() : base() { }
        public LabSampleEmbryo(
            Guid id
        )
        {
            Id = id;
        }
        public LabSampleEmbryo(
            Guid id,
            Guid labSampleId,
            Guid labSampleOocyteId,
            Guid labSampleSpermId,
            string? grade,
            string? fertilizationMethod
        )
        {
            Id = id;
            LabSampleId = labSampleId;
            LabSampleOocyteId = labSampleOocyteId;
            LabSampleSpermId = labSampleSpermId;
            Grade = grade;
            FertilizationMethod = fertilizationMethod;
        }
        public Guid LabSampleId { get; set; }
        public Guid LabSampleOocyteId { get; set; }
        public Guid LabSampleSpermId { get; set; }
        public string? Grade { get; set; }
        public int? CellCount { get; set; }
        public string? Morphology { get; set; }
        public bool IsBiopsied { get; set; } = false;
        public bool IsPGTTested { get; set; } = false;
        public string? PGTResult { get; set; }
        public string? FertilizationMethod { get; set; }
        public string? Notes { get; set; }
        public DateTime FertilizationDate { get; set; }
        public virtual LabSample? LabSample { get; set; }
        public virtual LabSampleOocyte? LabSampleOocyte { get; set; }
        public virtual LabSampleSperm? LabSampleSperm { get; set; }
    }
}

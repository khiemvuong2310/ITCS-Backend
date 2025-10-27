using System;
using FSCMS.Core.Models.Bases;

namespace FSCMS.Core.Entities
{
    public class LabSampleEmbryo : BaseEntity<Guid>
    {
        protected LabSampleEmbryo() : base() { }
        public LabSampleEmbryo(
            Guid id,
            Guid labSampleId,
            int dayOfDevelopment,
            string? grade,
            string? fertilizationMethod
        )
        {
            Id = id;
            LabSampleId = labSampleId;
            DayOfDevelopment = dayOfDevelopment;
            Grade = grade;
            FertilizationMethod = fertilizationMethod;
        }
        public Guid LabSampleId { get; set; }
        public int DayOfDevelopment { get; set; }
        public string? Grade { get; set; }
        public int? CellCount { get; set; }
        public string? Morphology { get; set; }
        public bool IsBiopsied { get; set; } = false;
        public bool IsPGTTested { get; set; } = false;
        public string? PGTResult { get; set; }
        public string? FertilizationMethod { get; set; }
        public string? Notes { get; set; }
        public virtual LabSample? LabSample { get; set; }
    }
}

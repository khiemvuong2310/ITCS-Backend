using System;

namespace FSCMS.Core.Entities
{
    /// <summary>
    /// Lưu thông tin bác sĩ chuyển phôi, thời gian và liên kết timeline
    /// </summary>
    public class EmbryoTransfer : BaseEntity
    {
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public DateTime TransferDate { get; set; }
        public int? TreatmentTimelineId { get; set; }
        public int? SpecimenId { get; set; } // Embryo specimen
        public int NumberOfEmbryos { get; set; }
        public string? CatheterType { get; set; }
        public string? UltrasoundGuided { get; set; }
        public string? Notes { get; set; }

        public virtual Patient? Patient { get; set; }
        public virtual Doctor? Doctor { get; set; }
        public virtual TreatmentTimeline? TreatmentTimeline { get; set; }
        public virtual Specimen? Specimen { get; set; }
    }
}



using System;
using FSCMS.Core.Models.Bases;

namespace FSCMS.Core.Entities
{
    public class PrescriptionDetail : BaseEntity<Guid>
    {
        protected PrescriptionDetail() : base() { }
        public PrescriptionDetail(Guid id, Guid prescriptionId, Guid medicineId, int quantity)
        {
            Id = id;
            PrescriptionId = prescriptionId;
            MedicineId = medicineId;
            Quantity = quantity;
        }
        public Guid PrescriptionId { get; set; }
        public Guid MedicineId { get; set; }
        public int Quantity { get; set; }
        public string Dosage { get; set; } = string.Empty;
        public string Frequency { get; set; } = string.Empty;
        public int DurationDays { get; set; }
        public string? Instructions { get; set; }
        public string? Notes { get; set; }
        public virtual Prescription? Prescription { get; set; }
        public virtual Medicine? Medicine { get; set; }
    }
}

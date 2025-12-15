using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using FSCMS.Core.Models.Bases;

namespace FSCMS.Core.Entities
{
    // Bảng Medicine: Danh mục thuốc/sản phẩm y tế.
    // Quan hệ:
    // - 1-n tới PrescriptionDetail (thuốc xuất hiện trong nhiều chi tiết đơn)
    public class Medicine : BaseEntity<Guid>
    {
        protected Medicine() : base() { }
        public Medicine(Guid id, string name, string? dosage, string? form)
        {
            Id = id;
            Name = name;
            Dosage = dosage;
            Form = form;
        }
        public string Name { get; set; } = default!;
        public string? GenericName { get; set; }
        public string? Dosage { get; set; }
        public string? Form { get; set; }
        public string? Indication { get; set; }
        public string? Contraindication { get; set; }
        public string? SideEffects { get; set; }
        public bool IsActive { get; set; } = true;
        public string? Notes { get; set; }
        [JsonIgnore]
        public virtual ICollection<PrescriptionDetail> PrescriptionDetails { get; set; } = new List<PrescriptionDetail>();
    }
}

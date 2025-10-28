using System;
using System.Collections.Generic;
using FSCMS.Core.Models.Bases;

namespace FSCMS.Core.Entities
{
    // Bảng ServiceCategory: Danh mục nhóm dịch vụ (khám, xét nghiệm, cryo...).
    // Quan hệ:
    // - 1-n tới Service (một nhóm chứa nhiều dịch vụ)
    public class ServiceCategory : BaseEntity<Guid>
    {
        protected ServiceCategory() : base() { }
        public ServiceCategory(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? Code { get; set; }
        public bool IsActive { get; set; } = true;
        public int DisplayOrder { get; set; }
        public virtual ICollection<Service> Services { get; set; } = new List<Service>();
    }
}

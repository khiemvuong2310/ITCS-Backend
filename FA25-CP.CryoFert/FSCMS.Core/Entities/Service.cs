using System;
using System.Collections.Generic;
using FSCMS.Core.Models.Bases;

namespace FSCMS.Core.Entities
{
    public class Service : BaseEntity<Guid>
    {
        protected Service() : base() { }
        public Service(Guid id, string name, decimal price, Guid serviceCategoryId)
        {
            Id = id;
            Name = name;
            Price = price;
            ServiceCategoryId = serviceCategoryId;
        }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public string? Code { get; set; }
        public string? Unit { get; set; }
        public int? Duration { get; set; }
        public bool IsActive { get; set; } = true;
        public string? Notes { get; set; }
        public Guid ServiceCategoryId { get; set; }
        public virtual ServiceCategory? ServiceCategory { get; set; }
        public virtual ICollection<ServiceRequestDetails> ServiceRequestDetails { get; set; } = new List<ServiceRequestDetails>();
    }
}

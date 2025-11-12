using System;
using System.Collections.Generic;
using FSCMS.Core.Models.Bases;

namespace FSCMS.Core.Entities
{
    // Bảng Role: Danh mục vai trò trong hệ thống (Admin, Doctor, ...)
    // Quan hệ:
    // - 1 Role có nhiều Account (One-to-Many) thông qua thuộc tính Users
    public class Role : BaseEntity<Guid>
    {
        protected Role() : base() { }
        public Role(Guid id, string roleName, string roleCode)
        {
            Id = id;
            RoleName = roleName;
            RoleCode = roleCode;
        }
        public string RoleName { get; set; } = string.Empty;
        public string RoleCode { get; set; } = string.Empty;
        public string? Description { get; set; }
        public virtual ICollection<Account> Users { get; set; } = new List<Account>();
    }
}

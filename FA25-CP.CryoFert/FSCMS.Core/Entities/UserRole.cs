using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSCMS.Core.Entities
{
    public class UserRole : BaseEntity
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public bool Status { get; set; }

        public virtual User? User { get; set; }
        public virtual Role? Role { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSCMS.Core.Enum
{
    /// <summary>
    /// Trạng thái của relationship
    /// </summary>
    public enum RelationshipStatus
    {
        Pending = 0,
        Approved = 1,
        Rejected = 2,
        Expired = 3
    }
}

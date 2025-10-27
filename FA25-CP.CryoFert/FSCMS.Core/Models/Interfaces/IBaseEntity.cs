using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSCMS.Core.Models.Interfaces
{
    /// <summary>
    ///     Base interface for all entities in the system.
    /// </summary>
    public interface IBaseEntity : IAuditableEntity, IDeletableEntity
    {
    }
}

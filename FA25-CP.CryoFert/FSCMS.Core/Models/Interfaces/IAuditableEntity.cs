using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSCMS.Core.Models.Interfaces
{
    /// <summary>
    /// Defines properties for entities that support auditing (timestamps) and soft deletion.
    /// </summary>
    public interface IAuditableEntity
    {
        /// <summary>
        /// Gets the date and time when the entity was created.
        /// </summary>
        DateTime CreatedAt { get; }

        /// <summary>
        /// Gets or sets the date and time when the entity was last updated.
        /// Nullable if the entity has not been updated yet.
        /// </summary>
        DateTime? UpdatedAt { get; set; }
    }
}

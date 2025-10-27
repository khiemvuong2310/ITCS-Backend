using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSCMS.Core.Models.Interfaces
{
    /// <summary>
    /// Defines properties for entities that support soft deletion.
    /// </summary>
    public interface IDeletableEntity
    {
        /// <summary>
        /// Gets or sets a value indicating whether the entity is marked as deleted (soft delete).
        /// </summary>
        bool IsDeleted { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the entity was marked as deleted.
        /// Nullable if the entity has not been deleted.
        /// </summary>
        DateTime? DeletedAt { get; set; }
    }
}

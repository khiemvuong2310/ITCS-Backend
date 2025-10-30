using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FSCMS.Core.Models.Interfaces;

namespace FSCMS.Core.Models.Bases
{
    /// <summary>
    /// Base class for entities with a single identifier, timestamps, and soft delete functionality.
    /// Supports lazy loading of navigation properties.
    /// </summary>
    /// <typeparam name="TId">The type of the entity's identifier.</typeparam>
    public abstract class BaseEntity<TId> : IBaseEntity where TId : IEquatable<TId> // Implements IAuditableEntity
    {
        private Action<object, string>? _lazyLoader;

        public TId Id { get; protected init; } = default!;
        public DateTime CreatedAt { get; protected set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedAt { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseEntity{TId}"/> class.
        /// Sets the creation timestamp and default soft delete status.
        /// </summary>
        protected BaseEntity()
        {
            CreatedAt = DateTime.UtcNow.AddHours(7);
            IsDeleted = false;
        }

        /// <summary>
        /// Constructor that accepts a lazy loader action for EF Core to inject its lazy loading service.
        /// This constructor is used by EF Core to enable lazy loading.
        /// </summary>
        /// <param name="lazyLoader">The lazy loading action.</param>
        protected BaseEntity(Action<object, string> lazyLoader)
        {
            _lazyLoader = lazyLoader;
            CreatedAt = DateTime.UtcNow.AddHours(7);
            IsDeleted = false;
        }

        /// <summary>
        /// Protected method to load a navigation property lazily.
        /// Usage in derived classes:
        /// private YourEntityType _navigationProperty;
        /// public YourEntityType NavigationProperty
        /// {
        ///     get => LazyLoad(ref _navigationProperty, nameof(NavigationProperty));
        ///     set => _navigationProperty = value;
        /// }
        /// </summary>
        /// <typeparam name="TRelated">The type of navigation property.</typeparam>
        /// <param name="navigationProperty">Reference to the backing field.</param>
        /// <param name="navigationPropertyName">Name of the navigation property.</param>
        /// <returns>The loaded navigation property.</returns>
        protected TRelated LazyLoad<TRelated>(ref TRelated navigationProperty, string navigationPropertyName)
            where TRelated : class
        {
            // If the lazy loader is available and the navigation property is null, load it
            if (_lazyLoader != null && navigationProperty == null)
            {
                _lazyLoader.Invoke(this, navigationPropertyName);
            }

            return navigationProperty!;
        }
    }
}

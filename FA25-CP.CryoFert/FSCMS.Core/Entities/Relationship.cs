using System;
using FSCMS.Core.Enum;
using FSCMS.Core.Models.Bases;

namespace FSCMS.Core.Entities
{
    /// <summary>
    /// Represents a relationship between two patients in the system.
    /// This entity acts as a linking table that defines various relationship types
    /// (e.g., spouse, donor, parent, child) between patients.
    /// </summary>
    public class Relationship : BaseEntity<Guid>
    {
        /// <summary>
        /// Default constructor for EF Core.
        /// </summary>
        protected Relationship() : base() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Relationship"/> class.
        /// </summary>
        /// <param name="id">The unique identifier of the relationship.</param>
        /// <param name="patient1Id">The ID of the first patient.</param>
        /// <param name="patient2Id">The ID of the second patient.</param>
        /// <param name="relationshipType">The type of relationship.</param>
        public Relationship(Guid id, Guid patient1Id, Guid patient2Id, RelationshipType relationshipType)
        {
            Id = id;
            Patient1Id = patient1Id;
            Patient2Id = patient2Id;
            RelationshipType = relationshipType;
        }

        // ────────────────────────────────
        // Relationship Information
        // ────────────────────────────────

        /// <summary>
        /// Gets or sets the ID of the first patient.
        /// </summary>
        public Guid Patient1Id { get; set; }

        /// <summary>
        /// Gets or sets the ID of the second patient.
        /// </summary>
        public Guid Patient2Id { get; set; }

        /// <summary>
        /// Gets or sets the type of relationship (e.g., Wife, Husband).
        /// </summary>
        public RelationshipType RelationshipType { get; set; }

        /// <summary>
        /// Gets or sets the date when this relationship was established.
        /// </summary>
        public DateTime? EstablishedDate { get; set; }

        /// <summary>
        /// Gets or sets additional notes or remarks about the relationship.
        /// </summary>
        public string? Notes { get; set; }

        /// <summary>
        /// Indicates whether this relationship is currently active.
        /// </summary>
        public bool IsActive { get; set; } = true;

        // ────────────────────────────────
        // Navigation Properties
        // ────────────────────────────────

        /// <summary>
        /// The first patient involved in this relationship.
        /// </summary>
        public virtual Patient? Patient1 { get; set; }

        /// <summary>
        /// The second patient involved in this relationship.
        /// </summary>
        public virtual Patient? Patient2 { get; set; }
    }
}

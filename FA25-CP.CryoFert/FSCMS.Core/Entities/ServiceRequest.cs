using System;
using System.Collections.Generic;
using FSCMS.Core.Enum;
using FSCMS.Core.Models.Bases;

namespace FSCMS.Core.Entities
{
    /// <summary>
    /// Represents a request for one or more medical services within the fertility center system.
    /// Each service request may be linked to an appointment and contains one or more service request details.
    /// </summary>
    public class ServiceRequest : BaseEntity<Guid>
    {
        /// <summary>
        /// Default constructor for EF Core.
        /// </summary>
        protected ServiceRequest() : base() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceRequest"/> class.
        /// </summary>
        /// <param name="id">The unique identifier of the service request.</param>
        /// <param name="requestDate">The date when the request was created.</param>
        /// <param name="requestType">The type of request (e.g., treatment, lab test).</param>
        public ServiceRequest(Guid id, DateTime requestDate)
        {
            Id = id;
            RequestDate = requestDate;
        }

        // ────────────────────────────────
        // Request Information
        // ────────────────────────────────

        /// <summary>
        /// Gets or sets the ID of the related appointment, if any.
        /// </summary>
        public Guid? AppointmentId { get; set; }

        /// <summary>
        /// Gets or sets the date when the service request was created.
        /// </summary>
        public DateTime RequestDate { get; set; }

        /// <summary>
        /// Gets or sets the current status of the service request.
        /// </summary>
        public ServiceRequestStatus Status { get; set; } = ServiceRequestStatus.Pending;

        /// <summary>
        /// Gets or sets the total calculated cost of all requested services.
        /// </summary>
        public decimal? TotalAmount { get; set; }

        /// <summary>
        /// Gets or sets optional notes or remarks related to this request.
        /// </summary>
        public string? Notes { get; set; }

        /// <summary>
        /// Gets or sets the date when the request was approved.
        /// </summary>
        public DateTime? ApprovedDate { get; set; }

        /// <summary>
        /// Gets or sets the name or identifier of the staff who approved the request.
        /// </summary>
        public string? ApprovedBy { get; set; }

        // ────────────────────────────────
        // Navigation Properties
        // ────────────────────────────────

        /// <summary>
        /// The appointment associated with this service request.
        /// </summary>
        public virtual Appointment? Appointment { get; set; }

        /// <summary>
        /// The list of detailed services requested within this request.
        /// </summary>
        public virtual ICollection<ServiceRequestDetails> ServiceRequestDetails { get; set; } = new List<ServiceRequestDetails>();
    }
}
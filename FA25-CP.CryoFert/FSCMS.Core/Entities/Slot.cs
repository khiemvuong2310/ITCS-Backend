using System;
using FSCMS.Core.Models.Bases;

namespace FSCMS.Core.Entities
{
    /// <summary>
    /// Represents a specific time slot within a doctor's work schedule.
    /// - Many-to-One relationship with <see cref="DoctorSchedule"/>.
    /// - One-to-One relationship with <see cref="Appointment"/>.
    /// </summary>
    public class Slot : BaseEntity<Guid>
    {
        /// <summary>
        /// Default constructor for EF Core.
        /// </summary>
        protected Slot() : base() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Slot"/> class.
        /// </summary>
        /// <param name="id">The unique identifier of the slot.</param>
        /// <param name="doctorScheduleId">The identifier of the related doctor schedule.</param>
        /// <param name="startTime">The start time of the slot.</param>
        /// <param name="endTime">The end time of the slot.</param>
        /// <param name="isBooked">Indicates whether the slot has been booked.</param>
        public Slot(
            Guid id,
            Guid doctorScheduleId,
            TimeSpan startTime,
            TimeSpan endTime,
            bool isBooked = false
        )
        {
            Id = id;
            DoctorScheduleId = doctorScheduleId;
            StartTime = startTime;
            EndTime = endTime;
            IsBooked = isBooked;
        }

        // ────────────────────────────────
        // Slot Information
        // ────────────────────────────────

        /// <summary>
        /// Gets or sets the identifier of the related doctor schedule.
        /// </summary>
        public Guid DoctorScheduleId { get; set; }

        /// <summary>
        /// Gets or sets the start time of the slot.
        /// </summary>
        public TimeSpan StartTime { get; set; }

        /// <summary>
        /// Gets or sets the end time of the slot.
        /// </summary>
        public TimeSpan EndTime { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the slot has been booked.
        /// </summary>
        public bool IsBooked { get; set; } = false;

        /// <summary>
        /// Gets or sets any additional notes related to the slot.
        /// </summary>
        public string? Notes { get; set; }

        // ────────────────────────────────
        // Navigation Properties
        // ────────────────────────────────

        /// <summary>
        /// Gets or sets the doctor schedule associated with this slot (Many-to-One).
        /// </summary>
        public virtual DoctorSchedule? DoctorSchedule { get; set; }

        /// <summary>
        /// Gets or sets the appointment associated with this slot (One-to-One).
        /// </summary>
        public virtual Appointment? Appointment { get; set; }
    }
}

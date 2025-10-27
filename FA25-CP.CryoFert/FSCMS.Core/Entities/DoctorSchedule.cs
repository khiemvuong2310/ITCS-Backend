using System;
using System.Collections.Generic;
using FSCMS.Core.Models.Bases;

namespace FSCMS.Core.Entities
{
    /// <summary>
    /// Represents a doctor's work schedule within the system.
    /// Defines working date, time range, and availability for patient appointments.
    /// Many-to-One relationship with <see cref="Doctor"/>.
    /// One-to-Many relationship with <see cref="Slot"/>.
    /// </summary>
    public class DoctorSchedule : BaseEntity<Guid>
    {
        /// <summary>
        /// Default constructor for EF Core.
        /// </summary>
        protected DoctorSchedule() : base() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="DoctorSchedule"/> class.
        /// </summary>
        /// <param name="id">The unique identifier of the schedule.</param>
        /// <param name="doctorId">The identifier of the doctor associated with this schedule.</param>
        /// <param name="workDate">The date of the working schedule.</param>
        /// <param name="startTime">The start time of the work shift.</param>
        /// <param name="endTime">The end time of the work shift.</param>
        /// <param name="isAvailable">Indicates whether the doctor is available on this date.</param>
        public DoctorSchedule(
            Guid id,
            Guid doctorId,
            DateTime workDate,
            TimeSpan startTime,
            TimeSpan endTime,
            bool isAvailable = true
        )
        {
            Id = id;
            DoctorId = doctorId;
            WorkDate = workDate;
            StartTime = startTime;
            EndTime = endTime;
            IsAvailable = isAvailable;
        }

        // ────────────────────────────────
        // Schedule Information
        // ────────────────────────────────

        /// <summary>
        /// Gets or sets the identifier of the doctor who owns this schedule.
        /// </summary>
        public Guid DoctorId { get; set; }

        /// <summary>
        /// Gets or sets the working date for this schedule.
        /// </summary>
        public DateTime WorkDate { get; set; }

        /// <summary>
        /// Gets or sets the start time of the working shift.
        /// </summary>
        public TimeSpan StartTime { get; set; }

        /// <summary>
        /// Gets or sets the end time of the working shift.
        /// </summary>
        public TimeSpan EndTime { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the doctor is available during this schedule.
        /// </summary>
        public bool IsAvailable { get; set; } = true;

        /// <summary>
        /// Gets or sets the location or room where the doctor will work (optional).
        /// </summary>
        public string? Location { get; set; }

        /// <summary>
        /// Gets or sets additional notes or remarks for this schedule.
        /// </summary>
        public string? Notes { get; set; }

        // ────────────────────────────────
        // Navigation Properties
        // ────────────────────────────────

        /// <summary>
        /// Gets or sets the doctor associated with this schedule.
        /// </summary>
        public virtual Doctor? Doctor { get; set; }

        /// <summary>
        /// Gets or sets the collection of appointment slots under this schedule.
        /// </summary>
        public virtual ICollection<Slot> Slots { get; set; } = new List<Slot>();
    }
}

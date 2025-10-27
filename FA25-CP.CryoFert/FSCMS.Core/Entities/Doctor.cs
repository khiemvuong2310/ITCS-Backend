using System;
using System.Collections.Generic;
using FSCMS.Core.Models.Bases;

namespace FSCMS.Core.Entities
{
    /// <summary>
    /// Represents a doctor within the healthcare system.
    /// Contains professional qualifications, work history, and related schedules or treatments.
    /// One-to-One relationship with <see cref="Account"/>.
    /// </summary>
    public class Doctor : BaseEntity<Guid>
    {
        /// <summary>
        /// Default constructor for EF Core.
        /// </summary>
        protected Doctor() : base() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Doctor"/> class.
        /// </summary>
        /// <param name="id">The unique identifier of the doctor.</param>
        /// <param name="badgeId">The badge or internal code of the doctor.</param>
        /// <param name="specialty">The doctor's medical specialty (e.g., IVF, IUI, Embryology).</param>
        /// <param name="yearsOfExperience">The total years of experience in the field.</param>
        /// <param name="joinDate">The date the doctor joined the organization.</param>
        /// <param name="isActive">Indicates whether the doctor is currently active.</param>
        public Doctor(
            Guid id,
            string badgeId,
            string specialty,
            int yearsOfExperience,
            DateTime joinDate,
            bool isActive = true
        )
        {
            Id = id;
            BadgeId = badgeId;
            Specialty = specialty;
            YearsOfExperience = yearsOfExperience;
            JoinDate = joinDate;
            IsActive = isActive;
        }

        // ────────────────────────────────
        // Doctor Information
        // ────────────────────────────────

        /// <summary>
        /// Gets or sets the unique badge ID assigned to the doctor.
        /// </summary>
        public string BadgeId { get; set; } = default!;

        /// <summary>
        /// Gets or sets the medical specialty of the doctor (e.g., IVF, IUI, Embryology).
        /// </summary>
        public string Specialty { get; set; } = default!;

        /// <summary>
        /// Gets or sets the list of certifications or qualifications held by the doctor (stored as text or JSON).
        /// </summary>
        public string? Certificates { get; set; }

        /// <summary>
        /// Gets or sets the professional medical license number of the doctor.
        /// </summary>
        public string? LicenseNumber { get; set; }

        /// <summary>
        /// Gets or sets the total number of years of medical experience.
        /// </summary>
        public int YearsOfExperience { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the doctor is currently active.
        /// </summary>
        public bool IsActive { get; set; } = true;

        /// <summary>
        /// Gets or sets the doctor's biography or professional summary.
        /// </summary>
        public string? Biography { get; set; }

        /// <summary>
        /// Gets or sets the date when the doctor joined the organization.
        /// </summary>
        public DateTime JoinDate { get; set; }

        /// <summary>
        /// Gets or sets the date when the doctor left the organization. Null if still active.
        /// </summary>
        public DateTime? LeaveDate { get; set; }

        // ────────────────────────────────
        // Navigation Properties
        // ────────────────────────────────

        /// <summary>
        /// Gets or sets the associated account of the doctor (One-to-One).
        /// </summary>
        public Guid AccountId { get; set; }
        public virtual Account? Account { get; set; }

        /// <summary>
        /// Gets or sets the collection of schedules assigned to the doctor.
        /// </summary>
        public virtual ICollection<DoctorSchedule> DoctorSchedules { get; set; } = new List<DoctorSchedule>();

        /// <summary>
        /// Gets or sets the collection of treatments managed or performed by the doctor.
        /// </summary>
        public virtual ICollection<Treatment> Treatments { get; set; } = new List<Treatment>();
    }
}

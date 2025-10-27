using System;
using FSCMS.Core.Enum;
using FSCMS.Core.Models.Bases;

namespace FSCMS.Core.Entities;

/// <summary>
/// Represents an appointment within a treatment cycle.  
/// Many-to-One with <see cref="TreatmentCycle"/>,  
/// One-to-One with <see cref="Slot"/> and <see cref="MedicalRecord"/>.
/// </summary>
public class Appointment : BaseEntity<Guid>
{
    /// <summary>
    /// Protected constructor for EF Core.
    /// </summary>
    protected Appointment() : base()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Appointment"/> class with specified values.
    /// </summary>
    /// <param name="id">The unique identifier of the appointment.</param>
    /// <param name="treatmentCycleId">The ID of the related treatment cycle.</param>
    /// <param name="appointmentDate">The date and time of the appointment.</param>
    /// <param name="type">The appointment type.</param>
    /// <param name="status">The appointment status.</param>
    /// <param name="reason">The reason for the appointment.</param>
    /// <param name="instructions">Special instructions for the patient.</param>
    /// <param name="notes">Additional notes about the appointment.</param>
    /// <param name="slotId">The assigned slot ID.</param>
    /// <param name="checkInTime">The check-in time when the patient arrives.</param>
    /// <param name="checkOutTime">The check-out time when the appointment is completed.</param>
    /// <param name="isReminderSent">Indicates whether a reminder has been sent.</param>
    public Appointment(
        Guid id,
        Guid treatmentCycleId,
        DateTime appointmentDate,
        AppointmentType type,
        AppointmentStatus status,
        string? reason = null,
        string? instructions = null,
        string? notes = null,
        Guid? slotId = null,
        DateTime? checkInTime = null,
        DateTime? checkOutTime = null,
        bool isReminderSent = false
    )
    {
        Id = id;
        TreatmentCycleId = treatmentCycleId;
        AppointmentDate = appointmentDate;
        Type = type;
        Status = status;
        Reason = reason;
        Instructions = instructions;
        Notes = notes;
        SlotId = slotId;
        CheckInTime = checkInTime;
        CheckOutTime = checkOutTime;
        IsReminderSent = isReminderSent;
    }

    // ────────────────────────────────
    // Appointment Details
    // ────────────────────────────────

    /// <summary>
    /// Gets or sets the ID of the associated treatment cycle.
    /// </summary>
    public Guid TreatmentCycleId { get; set; }

    /// <summary>
    /// Gets or sets the assigned slot ID (if applicable).
    /// </summary>
    public Guid? SlotId { get; set; }

    /// <summary>
    /// Gets or sets the appointment type.
    /// </summary>
    public AppointmentType Type { get; set; }

    /// <summary>
    /// Gets or sets the appointment status.
    /// </summary>
    public AppointmentStatus Status { get; set; }

    /// <summary>
    /// Gets or sets the scheduled date and time of the appointment.
    /// </summary>
    public DateTime AppointmentDate { get; set; }

    /// <summary>
    /// Gets or sets the reason for the appointment.
    /// </summary>
    public string? Reason { get; set; }

    /// <summary>
    /// Gets or sets any special instructions related to the appointment.
    /// </summary>
    public string? Instructions { get; set; }

    /// <summary>
    /// Gets or sets any additional notes.
    /// </summary>
    public string? Notes { get; set; }

    /// <summary>
    /// Gets or sets the check-in time when the patient arrives.
    /// </summary>
    public DateTime? CheckInTime { get; set; }

    /// <summary>
    /// Gets or sets the check-out time when the appointment is completed.
    /// </summary>
    public DateTime? CheckOutTime { get; set; }

    /// <summary>
    /// Indicates whether a reminder notification has been sent.
    /// </summary>
    public bool IsReminderSent { get; set; } = false;

    // ────────────────────────────────
    // Navigation Properties
    // ────────────────────────────────

    /// <summary>
    /// Gets or sets the related treatment cycle.
    /// </summary>
    public virtual TreatmentCycle? TreatmentCycle { get; set; }

    /// <summary>
    /// Gets or sets the assigned slot.
    /// </summary>
    public virtual Slot? Slot { get; set; }

    /// <summary>
    /// Gets or sets the related medical record for this appointment.
    /// </summary>
    public virtual MedicalRecord? MedicalRecord { get; set; }
}

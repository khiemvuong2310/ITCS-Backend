using System;
using FSCMS.Core.Enum;
using FSCMS.Core.Models.Bases;

namespace FSCMS.Core.Entities;
// Bảng Appointment: Cuộc hẹn trong một chu kỳ điều trị.
// Quan hệ:
// - n-1 tới TreatmentCycle (TreatmentCycleId)
// - 1-1 với Slot (SlotId tuỳ chọn)
// - 1-1 với MedicalRecord (bệnh án của cuộc hẹn)
public class Appointment : BaseEntity<Guid>
{
    protected Appointment() : base()
    {
    }
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
    public Guid TreatmentCycleId { get; set; }
    public Guid? SlotId { get; set; }
    public AppointmentType Type { get; set; }
    public AppointmentStatus Status { get; set; }
    public DateTime AppointmentDate { get; set; }
    public string? Reason { get; set; }
    public string? Instructions { get; set; }
    public string? Notes { get; set; }
    public DateTime? CheckInTime { get; set; }
    public DateTime? CheckOutTime { get; set; }
    public bool IsReminderSent { get; set; } = false;

    //Navigate properties
    public virtual TreatmentCycle? TreatmentCycle { get; set; }
    public virtual Slot? Slot { get; set; }
    public virtual MedicalRecord? MedicalRecord { get; set; }
}

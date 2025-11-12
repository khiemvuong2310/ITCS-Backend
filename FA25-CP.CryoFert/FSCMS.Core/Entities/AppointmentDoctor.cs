using FSCMS.Core.Models.Bases;

namespace FSCMS.Core.Entities;
// Bảng AppointmentDoctor: Join table cho quan hệ nhiều-nhiều giữa Appointment và Doctor.
// Quan hệ:
// - n-1 tới Appointment (AppointmentId)
// - n-1 tới Doctor (DoctorId)
public class AppointmentDoctor : BaseEntity<Guid>
{
    protected AppointmentDoctor() : base() { }
    
    public AppointmentDoctor(
        Guid id,
        Guid appointmentId,
        Guid doctorId,
        string? role = null,
        string? notes = null
    )
    {
        Id = id;
        AppointmentId = appointmentId;
        DoctorId = doctorId;
        Role = role;
        Notes = notes;
    }

    public Guid AppointmentId { get; set; }
    public Guid DoctorId { get; set; }

    //Vai trò doctor trong cuộc hẹn
    public string? Role { get; set; } // Ví dụ: "Primary", "Assisting", "Consultant"
    public string? Notes { get; set; }

    // Navigation properties
    public virtual Appointment? Appointment { get; set; }
    public virtual Doctor? Doctor { get; set; }
}


using System;

namespace FSCMS.Core.Entities
{
    /// <summary>
    /// Ghi nhận bệnh nhân đến khám
    /// </summary>
    public class CheckIn : BaseEntity
    {
        public int PatientId { get; set; }
        public int? AppointmentId { get; set; }
        public DateTime CheckInTime { get; set; } = DateTime.UtcNow;
        public string? Desk { get; set; }
        public string? Notes { get; set; }

        public virtual Patient? Patient { get; set; }
        public virtual Appointment? Appointment { get; set; }
    }
}



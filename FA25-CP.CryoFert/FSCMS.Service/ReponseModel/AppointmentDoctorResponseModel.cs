using System;
using System.Text.Json.Serialization;

namespace FSCMS.Service.ReponseModel
{
    public class AppointmentDoctorResponse
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("appointmentId")]
        public Guid AppointmentId { get; set; }

        [JsonPropertyName("doctorId")]
        public Guid DoctorId { get; set; }

        [JsonPropertyName("role")]
        public string? Role { get; set; }

        [JsonPropertyName("notes")]
        public string? Notes { get; set; }

        [JsonPropertyName("createdAt")]
        public DateTime CreatedAt { get; set; }

        [JsonPropertyName("updatedAt")]
        public DateTime? UpdatedAt { get; set; }

        // Basic doctor info
        [JsonPropertyName("doctor")]
        public AppointmentDoctorDoctorInfo? Doctor { get; set; }

        // Basic appointment info
        [JsonPropertyName("appointment")]
        public AppointmentDoctorAppointmentInfo? Appointment { get; set; }
    }

    public class AppointmentDoctorDoctorInfo
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("badgeId")]
        public string? BadgeId { get; set; }

        [JsonPropertyName("specialty")]
        public string? Specialty { get; set; }

        [JsonPropertyName("fullName")]
        public string? FullName { get; set; }
    }

    public class AppointmentDoctorAppointmentInfo
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("appointmentDate")]
        public DateOnly AppointmentDate { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; } = string.Empty;
    }
}



using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using FSCMS.Service.ReponseModel;

namespace FSCMS.Service.RequestModel
{
    public class CreateAppointmentDoctorRequest
    {
        [Required]
        [JsonPropertyName("appointmentId")]
        public Guid AppointmentId { get; set; }

        [Required]
        [JsonPropertyName("doctorId")]
        public Guid DoctorId { get; set; }

        [StringLength(100)]
        [JsonPropertyName("role")]
        public string? Role { get; set; }

        [StringLength(1000)]
        [JsonPropertyName("notes")]
        public string? Notes { get; set; }
    }

    public class UpdateAppointmentDoctorRequest
    {
        [StringLength(100)]
        [JsonPropertyName("role")]
        public string? Role { get; set; }

        [StringLength(1000)]
        [JsonPropertyName("notes")]
        public string? Notes { get; set; }
    }

    public class GetAppointmentDoctorsRequest : PagingModel
    {
        [JsonPropertyName("appointmentId")]
        public Guid? AppointmentId { get; set; }

        [JsonPropertyName("doctorId")]
        public Guid? DoctorId { get; set; }

        [JsonPropertyName("role")]
        public string? Role { get; set; }

        [JsonPropertyName("searchTerm")]
        public string? SearchTerm { get; set; }
    }
}



using FSCMS.Core.Enum;
using FSCMS.Service.ReponseModel;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FSCMS.Service.RequestModel
{
    /// <summary>
    /// Request model for creating an appointment
    /// </summary>
    public class CreateAppointmentRequest
    {
        [Required(ErrorMessage = "Patient ID is required.")]
        [JsonPropertyName("patientId")]
        public Guid PatientId { get; set; }

        [JsonPropertyName("treatmentCycleId")]
        public Guid? TreatmentCycleId { get; set; }

        [Required(ErrorMessage = "Appointment date is required.")]
        [JsonPropertyName("appointmentDate")]
        public DateTime AppointmentDate { get; set; }

        [Required(ErrorMessage = "Appointment type is required.")]
        [JsonPropertyName("type")]
        public AppointmentType Type { get; set; }

        [JsonPropertyName("status")]
        public AppointmentStatus Status { get; set; } = AppointmentStatus.Scheduled;

        [StringLength(500, ErrorMessage = "Reason cannot exceed 500 characters.")]
        [JsonPropertyName("reason")]
        public string? Reason { get; set; }

        [StringLength(1000, ErrorMessage = "Instructions cannot exceed 1000 characters.")]
        [JsonPropertyName("instructions")]
        public string? Instructions { get; set; }

        [StringLength(1000, ErrorMessage = "Notes cannot exceed 1000 characters.")]
        [JsonPropertyName("notes")]
        public string? Notes { get; set; }

        [JsonPropertyName("slotId")]
        public Guid? SlotId { get; set; }

        /// <summary>
        /// List of doctor IDs to assign to this appointment
        /// </summary>
        [JsonPropertyName("doctorIds")]
        public List<Guid>? DoctorIds { get; set; }

        /// <summary>
        /// Doctor roles corresponding to doctorIds (optional, defaults to null)
        /// </summary>
        [JsonPropertyName("doctorRoles")]
        public List<string>? DoctorRoles { get; set; }

        [JsonPropertyName("checkInTime")]
        public DateTime? CheckInTime { get; set; }

        [JsonPropertyName("checkOutTime")]
        public DateTime? CheckOutTime { get; set; }
    }

    /// <summary>
    /// Request model for updating an appointment
    /// </summary>
    public class UpdateAppointmentRequest
    {
        [JsonPropertyName("appointmentDate")]
        public DateTime? AppointmentDate { get; set; }

        [JsonPropertyName("type")]
        public AppointmentType? Type { get; set; }

        [JsonPropertyName("status")]
        public AppointmentStatus? Status { get; set; }

        [StringLength(500, ErrorMessage = "Reason cannot exceed 500 characters.")]
        [JsonPropertyName("reason")]
        public string? Reason { get; set; }

        [StringLength(1000, ErrorMessage = "Instructions cannot exceed 1000 characters.")]
        [JsonPropertyName("instructions")]
        public string? Instructions { get; set; }

        [StringLength(1000, ErrorMessage = "Notes cannot exceed 1000 characters.")]
        [JsonPropertyName("notes")]
        public string? Notes { get; set; }

        [JsonPropertyName("slotId")]
        public Guid? SlotId { get; set; }

        [JsonPropertyName("checkInTime")]
        public DateTime? CheckInTime { get; set; }

        [JsonPropertyName("checkOutTime")]
        public DateTime? CheckOutTime { get; set; }

        [JsonPropertyName("isReminderSent")]
        public bool? IsReminderSent { get; set; }
    }

    /// <summary>
    /// Request model for getting appointments with filtering
    /// </summary>
    public class GetAppointmentsRequest : PagingModel
    {
        [JsonPropertyName("treatmentCycleId")]
        public Guid? TreatmentCycleId { get; set; }

        [JsonPropertyName("doctorId")]
        public Guid? DoctorId { get; set; }

        [JsonPropertyName("slotId")]
        public Guid? SlotId { get; set; }

        [JsonPropertyName("type")]
        public AppointmentType? Type { get; set; }

        [JsonPropertyName("status")]
        public AppointmentStatus? Status { get; set; }

        [JsonPropertyName("appointmentDateFrom")]
        public DateTime? AppointmentDateFrom { get; set; }

        [JsonPropertyName("appointmentDateTo")]
        public DateTime? AppointmentDateTo { get; set; }

        [JsonPropertyName("searchTerm")]
        public string? SearchTerm { get; set; }
    }
}


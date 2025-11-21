using FSCMS.Core.Enum;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace FSCMS.Service.ReponseModel
{
    /// <summary>
    /// Response model for appointment information
    /// </summary>
    public class AppointmentResponse
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("treatmentCycleId")]
        public Guid? TreatmentCycleId { get; set; }

        [JsonPropertyName("slotId")]
        public Guid? SlotId { get; set; }

        [JsonPropertyName("type")]
        public AppointmentType Type { get; set; }

        [JsonPropertyName("typeName")]
        public string TypeName { get; set; } = string.Empty;

        [JsonPropertyName("status")]
        public AppointmentStatus Status { get; set; }

        [JsonPropertyName("statusName")]
        public string StatusName { get; set; } = string.Empty;

        [JsonPropertyName("appointmentDate")]
        public DateOnly AppointmentDate { get; set; }

        [JsonPropertyName("reason")]
        public string? Reason { get; set; }

        [JsonPropertyName("instructions")]
        public string? Instructions { get; set; }

        [JsonPropertyName("notes")]
        public string? Notes { get; set; }

        [JsonPropertyName("checkInTime")]
        public DateTime? CheckInTime { get; set; }

        [JsonPropertyName("checkOutTime")]
        public DateTime? CheckOutTime { get; set; }

        [JsonPropertyName("isReminderSent")]
        public bool IsReminderSent { get; set; }

        [JsonPropertyName("createdAt")]
        public DateTime CreatedAt { get; set; }

        [JsonPropertyName("updatedAt")]
        public DateTime? UpdatedAt { get; set; }

        // Related information
        [JsonPropertyName("treatmentCycle")]
        public AppointmentTreatmentCycleInfo? TreatmentCycle { get; set; }

        [JsonPropertyName("slot")]
        public AppointmentSlotInfo? Slot { get; set; }

        [JsonPropertyName("patient")]
        public AppointmentPatientInfo? Patient { get; set; }

        [JsonPropertyName("doctors")]
        public List<AppointmentDoctorInfo> Doctors { get; set; } = new List<AppointmentDoctorInfo>();

        [JsonPropertyName("doctorCount")]
        public int DoctorCount { get; set; }

        /// <summary>
        /// Transactions linked to this appointment (e.g. booking deposits, payments)
        /// </summary>
        [JsonPropertyName("transactions")]
        public List<TransactionResponseModel> Transactions { get; set; } = new List<TransactionResponseModel>();
    }

    /// <summary>
    /// Detailed appointment response with all related data
    /// </summary>
    public class AppointmentDetailResponse : AppointmentResponse
    {
        [JsonPropertyName("medicalRecord")]
        public AppointmentMedicalRecordInfo? MedicalRecord { get; set; }

        [JsonPropertyName("serviceRequests")]
        public List<AppointmentServiceRequestInfo> ServiceRequests { get; set; } = new List<AppointmentServiceRequestInfo>();
    }

    /// <summary>
    /// Treatment cycle information for appointment response
    /// </summary>
    public class AppointmentTreatmentCycleInfo
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("cycleName")]
        public string CycleName { get; set; } = string.Empty;

        [JsonPropertyName("cycleNumber")]
        public int CycleNumber { get; set; }

        [JsonPropertyName("startDate")]
        public DateTime? StartDate { get; set; }

        [JsonPropertyName("endDate")]
        public DateTime? EndDate { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; } = string.Empty;

        [JsonPropertyName("treatment")]
        public AppointmentTreatmentInfo? Treatment { get; set; }
    }

    /// <summary>
    /// Treatment information for appointment response
    /// </summary>
    public class AppointmentTreatmentInfo
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("treatmentType")]
        public string TreatmentType { get; set; } = string.Empty;

        [JsonPropertyName("patient")]
        public AppointmentPatientInfo? Patient { get; set; }
    }

    /// <summary>
    /// Patient information for appointment response
    /// </summary>
    public class AppointmentPatientInfo
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("patientCode")]
        public string PatientCode { get; set; } = string.Empty;

        [JsonPropertyName("fullName")]
        public string FullName { get; set; } = string.Empty;

        [JsonPropertyName("phone")]
        public string? Phone { get; set; }

        [JsonPropertyName("email")]
        public string? Email { get; set; }
    }

    /// <summary>
    /// Slot information for appointment response
    /// </summary>
    public class AppointmentSlotInfo
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("startTime")]
        public TimeSpan StartTime { get; set; }

        [JsonPropertyName("endTime")]
        public TimeSpan EndTime { get; set; }

        [JsonPropertyName("isBooked")]
        public bool IsBooked { get; set; }

        [JsonPropertyName("schedule")]
        public AppointmentScheduleInfo? Schedule { get; set; }
    }

    /// <summary>
    /// Schedule information for appointment response
    /// </summary>
    public class AppointmentScheduleInfo
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("workDate")]
        public DateTime WorkDate { get; set; }

        [JsonPropertyName("location")]
        public string? Location { get; set; }

        [JsonPropertyName("doctor")]
        public AppointmentDoctorBasicInfo? Doctor { get; set; }
    }

    /// <summary>
    /// Doctor information for appointment response
    /// </summary>
    public class AppointmentDoctorInfo
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("doctorId")]
        public Guid DoctorId { get; set; }

        [JsonPropertyName("badgeId")]
        public string BadgeId { get; set; } = string.Empty;

        [JsonPropertyName("specialty")]
        public string Specialty { get; set; } = string.Empty;

        [JsonPropertyName("fullName")]
        public string FullName { get; set; } = string.Empty;

        [JsonPropertyName("role")]
        public string? Role { get; set; }

        [JsonPropertyName("notes")]
        public string? Notes { get; set; }
    }

    /// <summary>
    /// Basic doctor information for appointment response
    /// </summary>
    public class AppointmentDoctorBasicInfo
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("badgeId")]
        public string BadgeId { get; set; } = string.Empty;

        [JsonPropertyName("specialty")]
        public string Specialty { get; set; } = string.Empty;

        [JsonPropertyName("fullName")]
        public string FullName { get; set; } = string.Empty;
    }

    /// <summary>
    /// Medical record information for appointment response
    /// </summary>
    public class AppointmentMedicalRecordInfo
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("recordDate")]
        public DateTime RecordDate { get; set; }

        [JsonPropertyName("chiefComplaint")]
        public string? ChiefComplaint { get; set; }

        [JsonPropertyName("diagnosis")]
        public string? Diagnosis { get; set; }
    }

    /// <summary>
    /// Service request information for appointment response
    /// </summary>
    public class AppointmentServiceRequestInfo
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("requestDate")]
        public DateTime RequestDate { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; } = string.Empty;

        [JsonPropertyName("totalAmount")]
        public decimal? TotalAmount { get; set; }
    }
}


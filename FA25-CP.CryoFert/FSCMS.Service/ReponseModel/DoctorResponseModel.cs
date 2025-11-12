using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FSCMS.Service.ReponseModel
{
    /// <summary>
    /// Response model for doctor information
    /// </summary>
    public class DoctorResponse
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("accountId")]
        public Guid AccountId { get; set; }

        [JsonPropertyName("badgeId")]
        public string BadgeId { get; set; } = string.Empty;

        [JsonPropertyName("specialty")]
        public string Specialty { get; set; } = string.Empty;

        [JsonPropertyName("certificates")]
        public string? Certificates { get; set; }

        [JsonPropertyName("licenseNumber")]
        public string? LicenseNumber { get; set; }

        [JsonPropertyName("yearsOfExperience")]
        public int YearsOfExperience { get; set; }

        [JsonPropertyName("biography")]
        public string? Biography { get; set; }

        [JsonPropertyName("joinDate")]
        public DateTime JoinDate { get; set; }

        [JsonPropertyName("leaveDate")]
        public DateTime? LeaveDate { get; set; }

        [JsonPropertyName("isActive")]
        public bool IsActive { get; set; }

        [JsonPropertyName("createdAt")]
        public DateTime CreatedAt { get; set; }

        [JsonPropertyName("updatedAt")]
        public DateTime? UpdatedAt { get; set; }

        // Account information
        [JsonPropertyName("account")]
        public DoctorAccountInfo? Account { get; set; }
    }

    /// <summary>
    /// Detailed doctor response with additional information
    /// </summary>
    public class DoctorDetailResponse : DoctorResponse
    {
        [JsonPropertyName("totalSchedules")]
        public int TotalSchedules { get; set; }

        [JsonPropertyName("totalTreatments")]
        public int TotalTreatments { get; set; }

        [JsonPropertyName("upcomingSchedules")]
        public List<DoctorScheduleResponse> UpcomingSchedules { get; set; } = new List<DoctorScheduleResponse>();

        [JsonPropertyName("recentTreatments")]
        public int RecentTreatments { get; set; }
    }

    /// <summary>
    /// Account information for doctor response
    /// </summary>
    public class DoctorAccountInfo
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("firstName")]
        public string FirstName { get; set; } = string.Empty;

        [JsonPropertyName("lastName")]
        public string LastName { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; } = string.Empty;

        [JsonPropertyName("phone")]
        public string Phone { get; set; } = string.Empty;

        [JsonPropertyName("username")]
        public string Username { get; set; } = string.Empty;

        [JsonPropertyName("isActive")]
        public bool IsActive { get; set; }

        [JsonPropertyName("isVerified")]
        public bool IsVerified { get; set; }
    }

    /// <summary>
    /// Response model for doctor schedule information
    /// </summary>
    public class DoctorScheduleResponse
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("doctorId")]
        public Guid DoctorId { get; set; }

        [JsonPropertyName("slotId")]
        public Guid SlotId { get; set; }

        [JsonPropertyName("workDate")]
        public DateTime WorkDate { get; set; }

        [JsonPropertyName("startTime")]
        public TimeSpan StartTime { get; set; }

        [JsonPropertyName("endTime")]
        public TimeSpan EndTime { get; set; }

        [JsonPropertyName("location")]
        public string? Location { get; set; }

        [JsonPropertyName("notes")]
        public string? Notes { get; set; }

        [JsonPropertyName("isAvailable")]
        public bool IsAvailable { get; set; }

        [JsonPropertyName("createdAt")]
        public DateTime CreatedAt { get; set; }

        [JsonPropertyName("updatedAt")]
        public DateTime? UpdatedAt { get; set; }

        // Doctor information
        [JsonPropertyName("doctor")]
        public DoctorBasicInfo? Doctor { get; set; }

        // Slots information
        [JsonPropertyName("totalSlots")]
        public int TotalSlots { get; set; }

        [JsonPropertyName("availableSlots")]
        public int AvailableSlots { get; set; }

        [JsonPropertyName("bookedSlots")]
        public int BookedSlots { get; set; }
    }

    /// <summary>
    /// Detailed doctor schedule response with slots
    /// </summary>
    public class DoctorScheduleDetailResponse : DoctorScheduleResponse
    {
        [JsonPropertyName("slots")]
        public List<SlotResponse> Slots { get; set; } = new List<SlotResponse>();
    }

    /// <summary>
    /// Basic doctor information for references
    /// </summary>
    public class DoctorBasicInfo
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("badgeId")]
        public string BadgeId { get; set; } = string.Empty;

        [JsonPropertyName("specialty")]
        public string Specialty { get; set; } = string.Empty;

        [JsonPropertyName("yearsOfExperience")]
        public int YearsOfExperience { get; set; }

        [JsonPropertyName("fullName")]
        public string FullName { get; set; } = string.Empty;
    }

    /// <summary>
    /// Response model for slot information
    /// </summary>
    public class SlotResponse
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("doctorScheduleId")]
        public Guid DoctorScheduleId { get; set; }

        [JsonPropertyName("startTime")]
        public TimeSpan StartTime { get; set; }

        [JsonPropertyName("endTime")]
        public TimeSpan EndTime { get; set; }

        [JsonPropertyName("notes")]
        public string? Notes { get; set; }

        [JsonPropertyName("isBooked")]
        public bool IsBooked { get; set; }

        [JsonPropertyName("createdAt")]
        public DateTime CreatedAt { get; set; }

        [JsonPropertyName("updatedAt")]
        public DateTime? UpdatedAt { get; set; }

        // Schedule information
        [JsonPropertyName("schedule")]
        public SlotScheduleInfo? Schedule { get; set; }
    }

    /// <summary>
    /// Detailed slot response with appointment information
    /// </summary>
    public class SlotDetailResponse : SlotResponse
    {
        [JsonPropertyName("appointment")]
        public SlotAppointmentInfo? Appointment { get; set; }
    }

    /// <summary>
    /// Schedule information for slot response
    /// </summary>
    public class SlotScheduleInfo
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("workDate")]
        public DateTime WorkDate { get; set; }

        [JsonPropertyName("location")]
        public string? Location { get; set; }

        [JsonPropertyName("doctor")]
        public DoctorBasicInfo? Doctor { get; set; }
    }

    /// <summary>
    /// Appointment information for slot response
    /// </summary>
    public class SlotAppointmentInfo
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("appointmentDate")]
        public DateTime AppointmentDate { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; } = string.Empty;

        [JsonPropertyName("patientName")]
        public string PatientName { get; set; } = string.Empty;
    }

    /// <summary>
    /// Summary statistics for doctor dashboard
    /// </summary>
    public class DoctorStatisticsResponse
    {
        [JsonPropertyName("totalDoctors")]
        public int TotalDoctors { get; set; }

        [JsonPropertyName("activeDoctors")]
        public int ActiveDoctors { get; set; }

        [JsonPropertyName("inactiveDoctors")]
        public int InactiveDoctors { get; set; }

        [JsonPropertyName("totalSchedulesToday")]
        public int TotalSchedulesToday { get; set; }

        [JsonPropertyName("totalSlotsToday")]
        public int TotalSlotsToday { get; set; }

        [JsonPropertyName("bookedSlotsToday")]
        public int BookedSlotsToday { get; set; }

        [JsonPropertyName("availableSlotsToday")]
        public int AvailableSlotsToday { get; set; }

        [JsonPropertyName("specialties")]
        public List<SpecialtyStatistic> Specialties { get; set; } = new List<SpecialtyStatistic>();
    }

    /// <summary>
    /// Specialty statistics
    /// </summary>
    public class SpecialtyStatistic
    {
        [JsonPropertyName("specialty")]
        public string Specialty { get; set; } = string.Empty;

        [JsonPropertyName("doctorCount")]
        public int DoctorCount { get; set; }

        [JsonPropertyName("averageExperience")]
        public double AverageExperience { get; set; }
    }
}

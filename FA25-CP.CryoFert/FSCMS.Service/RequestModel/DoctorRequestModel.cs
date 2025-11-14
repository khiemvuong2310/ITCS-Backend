using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using FSCMS.Service.ReponseModel;

namespace FSCMS.Service.RequestModel
{
    /// <summary>
    /// Request model for creating a new doctor
    /// </summary>
    public class CreateDoctorRequest
    {
        [Required(ErrorMessage = "Account ID is required.")]
        [JsonPropertyName("accountId")]
        public Guid AccountId { get; set; }

        [Required(ErrorMessage = "Badge ID is required.")]
        [StringLength(50, ErrorMessage = "Badge ID cannot exceed 50 characters.")]
        [JsonPropertyName("badgeId")]
        public string BadgeId { get; set; } = null!;

        [Required(ErrorMessage = "Specialty is required.")]
        [StringLength(100, ErrorMessage = "Specialty cannot exceed 100 characters.")]
        [JsonPropertyName("specialty")]
        public string Specialty { get; set; } = null!;

        [StringLength(500, ErrorMessage = "Certificates cannot exceed 500 characters.")]
        [JsonPropertyName("certificates")]
        public string? Certificates { get; set; }

        [StringLength(50, ErrorMessage = "License number cannot exceed 50 characters.")]
        [JsonPropertyName("licenseNumber")]
        public string? LicenseNumber { get; set; }

        [Range(0, 50, ErrorMessage = "Years of experience must be between 0 and 50.")]
        [JsonPropertyName("yearsOfExperience")]
        public int YearsOfExperience { get; set; }

        [StringLength(1000, ErrorMessage = "Biography cannot exceed 1000 characters.")]
        [JsonPropertyName("biography")]
        public string? Biography { get; set; }

        [Required(ErrorMessage = "Join date is required.")]
        [JsonPropertyName("joinDate")]
        public DateTime JoinDate { get; set; }

        [JsonPropertyName("isActive")]
        public bool IsActive { get; set; } = true;
    }

    /// <summary>
    /// Request model for updating an existing doctor
    /// </summary>
    public class UpdateDoctorRequest
    {
        [StringLength(50, ErrorMessage = "Badge ID cannot exceed 50 characters.")]
        [JsonPropertyName("badgeId")]
        public string? BadgeId { get; set; }

        [StringLength(100, ErrorMessage = "Specialty cannot exceed 100 characters.")]
        [JsonPropertyName("specialty")]
        public string? Specialty { get; set; }

        [StringLength(500, ErrorMessage = "Certificates cannot exceed 500 characters.")]
        [JsonPropertyName("certificates")]
        public string? Certificates { get; set; }

        [StringLength(50, ErrorMessage = "License number cannot exceed 50 characters.")]
        [JsonPropertyName("licenseNumber")]
        public string? LicenseNumber { get; set; }

        [Range(0, 50, ErrorMessage = "Years of experience must be between 0 and 50.")]
        [JsonPropertyName("yearsOfExperience")]
        public int? YearsOfExperience { get; set; }

        [StringLength(1000, ErrorMessage = "Biography cannot exceed 1000 characters.")]
        [JsonPropertyName("biography")]
        public string? Biography { get; set; }

        [JsonPropertyName("joinDate")]
        public DateTime? JoinDate { get; set; }

        [JsonPropertyName("leaveDate")]
        public DateTime? LeaveDate { get; set; }

        [JsonPropertyName("isActive")]
        public bool? IsActive { get; set; }
    }

    /// <summary>
    /// Request model for getting doctors with pagination and filtering
    /// </summary>
    public class GetDoctorsRequest : PagingModel
    {

        [JsonPropertyName("searchTerm")]
        public string? SearchTerm { get; set; }

        [JsonPropertyName("specialty")]
        public string? Specialty { get; set; }

        [JsonPropertyName("isActive")]
        public bool? IsActive { get; set; }

        [JsonPropertyName("minExperience")]
        public int? MinExperience { get; set; }

        [JsonPropertyName("maxExperience")]
        public int? MaxExperience { get; set; }

        [JsonPropertyName("joinDateFrom")]
        public DateTime? JoinDateFrom { get; set; }

        [JsonPropertyName("joinDateTo")]
        public DateTime? JoinDateTo { get; set; }
    }

    /// <summary>
    /// Request model for querying doctors by availability
    /// </summary>
    public class GetAvailableDoctorsRequest : PagingModel
    {
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("workDate")]
        public DateOnly? WorkDate { get; set; }

        [JsonPropertyName("slotId")]
        public Guid? SlotId { get; set; }

        [JsonPropertyName("includeInactive")]
        public bool IncludeInactive { get; set; } = false;
    }

    /// <summary>
    /// Request model for creating a doctor schedule
    /// </summary>
    public class CreateDoctorScheduleRequest
    {
        [Required(ErrorMessage = "Doctor ID is required.")]
        [JsonPropertyName("doctorId")]
        public Guid DoctorId { get; set; }

        [Required(ErrorMessage = "Slot ID is required.")]
        [JsonPropertyName("slotId")]
        public Guid SlotId { get; set; }

        [Required(ErrorMessage = "Work date is required.")]
        [JsonPropertyName("workDate")]
        public DateTime WorkDate { get; set; }

        [StringLength(100, ErrorMessage = "Location cannot exceed 100 characters.")]
        [JsonPropertyName("location")]
        public string? Location { get; set; }

        [StringLength(500, ErrorMessage = "Notes cannot exceed 500 characters.")]
        [JsonPropertyName("notes")]
        public string? Notes { get; set; }

        [JsonPropertyName("isAvailable")]
        public bool IsAvailable { get; set; } = true;
    }

    /// <summary>
    /// Request model for updating a doctor schedule
    /// </summary>
    public class UpdateDoctorScheduleRequest
    {
        [JsonPropertyName("slotId")]
        public Guid? SlotId { get; set; }

        [JsonPropertyName("workDate")]
        public DateTime? WorkDate { get; set; }

        [StringLength(100, ErrorMessage = "Location cannot exceed 100 characters.")]
        [JsonPropertyName("location")]
        public string? Location { get; set; }

        [StringLength(500, ErrorMessage = "Notes cannot exceed 500 characters.")]
        [JsonPropertyName("notes")]
        public string? Notes { get; set; }

        [JsonPropertyName("isAvailable")]
        public bool? IsAvailable { get; set; }
    }

    /// <summary>
    /// Request model for getting doctor schedules with filtering
    /// </summary>
    public class GetDoctorSchedulesRequest : PagingModel
    {
        [JsonPropertyName("doctorId")]
        public Guid? DoctorId { get; set; }

        [JsonPropertyName("workDateFrom")]
        public DateTime? WorkDateFrom { get; set; }

        [JsonPropertyName("workDateTo")]
        public DateTime? WorkDateTo { get; set; }

        [JsonPropertyName("isAvailable")]
        public bool? IsAvailable { get; set; }

        [JsonPropertyName("location")]
        public string? Location { get; set; }
    }

    /// <summary>
    /// Request model for creating a slot
    /// </summary>
    public class CreateSlotRequest
    {
        [Required(ErrorMessage = "Doctor schedule ID is required.")]
        [JsonPropertyName("doctorScheduleId")]
        public Guid DoctorScheduleId { get; set; }

        [Required(ErrorMessage = "Start time is required.")]
        [JsonPropertyName("startTime")]
        public TimeSpan StartTime { get; set; }

        [Required(ErrorMessage = "End time is required.")]
        [JsonPropertyName("endTime")]
        public TimeSpan EndTime { get; set; }

        [StringLength(500, ErrorMessage = "Notes cannot exceed 500 characters.")]
        [JsonPropertyName("notes")]
        public string? Notes { get; set; }

        [JsonPropertyName("isBooked")]
        public bool IsBooked { get; set; } = false;
    }

    /// <summary>
    /// Request model for updating a slot
    /// </summary>
    public class UpdateSlotRequest
    {
        [JsonPropertyName("startTime")]
        public TimeSpan? StartTime { get; set; }

        [JsonPropertyName("endTime")]
        public TimeSpan? EndTime { get; set; }

        [StringLength(500, ErrorMessage = "Notes cannot exceed 500 characters.")]
        [JsonPropertyName("notes")]
        public string? Notes { get; set; }

        [JsonPropertyName("isBooked")]
        public bool? IsBooked { get; set; }
    }

    /// <summary>
    /// Request model for getting slots with filtering
    /// </summary>
    public class GetSlotsRequest : PagingModel
    {
        [JsonPropertyName("doctorScheduleId")]
        public Guid? DoctorScheduleId { get; set; }

        [JsonPropertyName("doctorId")]
        public Guid? DoctorId { get; set; }

        [JsonPropertyName("isBooked")]
        public bool? IsBooked { get; set; }

        [JsonPropertyName("dateFrom")]
        public DateTime? DateFrom { get; set; }

        [JsonPropertyName("dateTo")]
        public DateTime? DateTo { get; set; }

        [JsonPropertyName("timeFrom")]
        public TimeSpan? TimeFrom { get; set; }

        [JsonPropertyName("timeTo")]
        public TimeSpan? TimeTo { get; set; }
    }

    /// <summary>
    /// Request model for getting busy schedule dates for a doctor
    /// </summary>
    public class GetBusyScheduleDateRequest
    {
        [Required(ErrorMessage = "Doctor ID is required.")]
        [JsonPropertyName("doctorId")]
        public Guid DoctorId { get; set; }

        [JsonPropertyName("fromDate")]
        public DateTime? FromDate { get; set; }

        [JsonPropertyName("toDate")]
        public DateTime? ToDate { get; set; }
    }
}

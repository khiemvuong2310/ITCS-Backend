using FSCMS.Service.ReponseModel;
using FSCMS.Service.RequestModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSCMS.Service.Interfaces
{
    /// <summary>
    /// Interface for doctor management services
    /// </summary>
    public interface IDoctorService
    {
        #region Doctor CRUD Operations

        /// <summary>
        /// Get doctor by ID
        /// </summary>
        /// <param name="doctorId">The unique identifier of the doctor</param>
        /// <returns>BaseResponse containing doctor information</returns>
        Task<BaseResponse<DoctorResponse>> GetDoctorByIdAsync(Guid doctorId);

        /// <summary>
        /// Get detailed doctor information by ID
        /// </summary>
        /// <param name="doctorId">The unique identifier of the doctor</param>
        /// <returns>BaseResponse containing detailed doctor information</returns>
        Task<BaseResponse<DoctorDetailResponse>> GetDoctorDetailByIdAsync(Guid doctorId);

        /// <summary>
        /// Get doctor by account ID
        /// </summary>
        /// <param name="accountId">The account ID associated with the doctor</param>
        /// <returns>BaseResponse containing doctor information</returns>
        Task<BaseResponse<DoctorResponse>> GetDoctorByAccountIdAsync(Guid accountId);

        /// <summary>
        /// Get all doctors with pagination and filtering
        /// </summary>
        /// <param name="request">Request parameters for filtering and pagination</param>
        /// <returns>DynamicResponse containing paginated doctor list</returns>
        Task<DynamicResponse<DoctorResponse>> GetAllDoctorsAsync(GetDoctorsRequest request);

        /// <summary>
        /// Get doctors who are available based on optional filters
        /// </summary>
        /// <param name="request">Request parameters for availability filtering and pagination</param>
        /// <returns>DynamicResponse containing paginated doctor list</returns>
        Task<DynamicResponse<DoctorResponse>> GetAvailableDoctorsAsync(GetAvailableDoctorsRequest request);

        /// <summary>
        /// Create new doctor
        /// </summary>
        /// <param name="request">Doctor creation request</param>
        /// <returns>BaseResponse containing created doctor information</returns>
        Task<BaseResponse<DoctorResponse>> CreateDoctorAsync(CreateDoctorRequest request);

        /// <summary>
        /// Update existing doctor
        /// </summary>
        /// <param name="doctorId">The unique identifier of the doctor</param>
        /// <param name="request">Doctor update request</param>
        /// <returns>BaseResponse containing updated doctor information</returns>
        Task<BaseResponse<DoctorResponse>> UpdateDoctorAsync(Guid doctorId, UpdateDoctorRequest request);

        /// <summary>
        /// Delete doctor (soft delete)
        /// </summary>
        /// <param name="doctorId">The unique identifier of the doctor</param>
        /// <returns>BaseResponse indicating success or failure</returns>
        Task<BaseResponse> DeleteDoctorAsync(Guid doctorId);

        /// <summary>
        /// Update doctor status (active/inactive)
        /// </summary>
        /// <param name="doctorId">The unique identifier of the doctor</param>
        /// <param name="isActive">The new status</param>
        /// <returns>BaseResponse indicating success or failure</returns>
        Task<BaseResponse> UpdateDoctorStatusAsync(Guid doctorId, bool isActive);

        #endregion

        #region Doctor Schedule CRUD Operations

        /// <summary>
        /// Get doctor schedule by ID
        /// </summary>
        /// <param name="scheduleId">The unique identifier of the schedule</param>
        /// <returns>BaseResponse containing schedule information</returns>
        Task<BaseResponse<DoctorScheduleResponse>> GetDoctorScheduleByIdAsync(Guid scheduleId);

        /// <summary>
        /// Get detailed doctor schedule by ID with slots
        /// </summary>
        /// <param name="scheduleId">The unique identifier of the schedule</param>
        /// <returns>BaseResponse containing detailed schedule information</returns>
        Task<BaseResponse<DoctorScheduleDetailResponse>> GetDoctorScheduleDetailByIdAsync(Guid scheduleId);

        /// <summary>
        /// Get all doctor schedules with pagination and filtering
        /// </summary>
        /// <param name="request">Request parameters for filtering and pagination</param>
        /// <returns>DynamicResponse containing paginated schedule list</returns>
        Task<DynamicResponse<DoctorScheduleResponse>> GetAllDoctorSchedulesAsync(GetDoctorSchedulesRequest request);

        /// <summary>
        /// Get schedules for a specific doctor
        /// </summary>
        /// <param name="doctorId">The unique identifier of the doctor</param>
        /// <param name="request">Request parameters for filtering and pagination</param>
        /// <returns>DynamicResponse containing paginated schedule list</returns>
        Task<DynamicResponse<DoctorScheduleResponse>> GetDoctorSchedulesByDoctorIdAsync(Guid doctorId, GetDoctorSchedulesRequest request);

        /// <summary>
        /// Create new doctor schedule
        /// </summary>
        /// <param name="request">Schedule creation request</param>
        /// <returns>BaseResponse containing created schedule information</returns>
        Task<BaseResponse<DoctorScheduleResponse>> CreateDoctorScheduleAsync(CreateDoctorScheduleRequest request);

        /// <summary>
        /// Update existing doctor schedule
        /// </summary>
        /// <param name="scheduleId">The unique identifier of the schedule</param>
        /// <param name="request">Schedule update request</param>
        /// <returns>BaseResponse containing updated schedule information</returns>
        Task<BaseResponse<DoctorScheduleResponse>> UpdateDoctorScheduleAsync(Guid scheduleId, UpdateDoctorScheduleRequest request);

        /// <summary>
        /// Delete doctor schedule (soft delete)
        /// </summary>
        /// <param name="scheduleId">The unique identifier of the schedule</param>
        /// <returns>BaseResponse indicating success or failure</returns>
        Task<BaseResponse> DeleteDoctorScheduleAsync(Guid scheduleId);

        /// <summary>
        /// Update schedule availability
        /// </summary>
        /// <param name="scheduleId">The unique identifier of the schedule</param>
        /// <param name="isAvailable">The new availability status</param>
        /// <returns>BaseResponse indicating success or failure</returns>
        Task<BaseResponse> UpdateScheduleAvailabilityAsync(Guid scheduleId, bool isAvailable);

        #endregion

        #region Slot CRUD Operations

        /// <summary>
        /// Get slot by ID
        /// </summary>
        /// <param name="slotId">The unique identifier of the slot</param>
        /// <returns>BaseResponse containing slot information</returns>
        Task<BaseResponse<SlotResponse>> GetSlotByIdAsync(Guid slotId);

        /// <summary>
        /// Get detailed slot by ID with appointment information
        /// </summary>
        /// <param name="slotId">The unique identifier of the slot</param>
        /// <returns>BaseResponse containing detailed slot information</returns>
        Task<BaseResponse<SlotDetailResponse>> GetSlotDetailByIdAsync(Guid slotId);

        /// <summary>
        /// Get all slots with pagination and filtering
        /// </summary>
        /// <param name="request">Request parameters for filtering and pagination</param>
        /// <returns>DynamicResponse containing paginated slot list</returns>
        Task<DynamicResponse<SlotResponse>> GetAllSlotsAsync(GetSlotsRequest request);

        /// <summary>
        /// Get slots for a specific doctor schedule
        /// </summary>
        /// <param name="scheduleId">The unique identifier of the doctor schedule</param>
        /// <param name="request">Request parameters for filtering and pagination</param>
        /// <returns>DynamicResponse containing paginated slot list</returns>
        Task<DynamicResponse<SlotResponse>> GetSlotsByScheduleIdAsync(Guid scheduleId, GetSlotsRequest request);

        /// <summary>
        /// Get available slots for a specific doctor and date range
        /// </summary>
        /// <param name="doctorId">The unique identifier of the doctor</param>
        /// <param name="dateFrom">Start date for search</param>
        /// <param name="dateTo">End date for search</param>
        /// <returns>DynamicResponse containing available slots</returns>
        Task<DynamicResponse<SlotResponse>> GetAvailableSlotsAsync(Guid doctorId, DateTime dateFrom, DateTime dateTo);

        /// <summary>
        /// Create new slot
        /// </summary>
        /// <param name="request">Slot creation request</param>
        /// <returns>BaseResponse containing created slot information</returns>
        Task<BaseResponse<SlotResponse>> CreateSlotAsync(CreateSlotRequest request);

        /// <summary>
        /// Create multiple slots for a schedule
        /// </summary>
        /// <param name="scheduleId">The unique identifier of the doctor schedule</param>
        /// <param name="slotDuration">Duration of each slot in minutes</param>
        /// <returns>BaseResponse containing number of created slots</returns>
        Task<BaseResponse<int>> CreateSlotsForScheduleAsync(Guid scheduleId, int slotDuration = 30);

        /// <summary>
        /// Update existing slot
        /// </summary>
        /// <param name="slotId">The unique identifier of the slot</param>
        /// <param name="request">Slot update request</param>
        /// <returns>BaseResponse containing updated slot information</returns>
        Task<BaseResponse<SlotResponse>> UpdateSlotAsync(Guid slotId, UpdateSlotRequest request);

        /// <summary>
        /// Delete slot (soft delete)
        /// </summary>
        /// <param name="slotId">The unique identifier of the slot</param>
        /// <returns>BaseResponse indicating success or failure</returns>
        Task<BaseResponse> DeleteSlotAsync(Guid slotId);

        /// <summary>
        /// Update slot booking status
        /// </summary>
        /// <param name="slotId">The unique identifier of the slot</param>
        /// <param name="isBooked">The new booking status</param>
        /// <returns>BaseResponse indicating success or failure</returns>
        Task<BaseResponse> UpdateSlotBookingStatusAsync(Guid slotId, bool isBooked);

        #endregion

        #region Utility Methods

        /// <summary>
        /// Check if doctor exists
        /// </summary>
        /// <param name="doctorId">The unique identifier of the doctor</param>
        /// <returns>True if doctor exists, false otherwise</returns>
        Task<bool> DoctorExistsAsync(Guid doctorId);

        /// <summary>
        /// Check if badge ID is unique
        /// </summary>
        /// <param name="badgeId">The badge ID to check</param>
        /// <param name="excludeDoctorId">Doctor ID to exclude from check (for updates)</param>
        /// <returns>True if badge ID is unique, false otherwise</returns>
        Task<bool> IsBadgeIdUniqueAsync(string badgeId, Guid? excludeDoctorId = null);

        /// <summary>
        /// Get doctor statistics
        /// </summary>
        /// <returns>BaseResponse containing doctor statistics</returns>
        Task<BaseResponse<DoctorStatisticsResponse>> GetDoctorStatisticsAsync();

        /// <summary>
        /// Get available specialties
        /// </summary>
        /// <returns>List of available specialties</returns>
        Task<BaseResponse<List<string>>> GetAvailableSpecialtiesAsync();

        #endregion
    }
}

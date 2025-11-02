using FSCMS.Core.Enum;
using FSCMS.Service.ReponseModel;
using FSCMS.Service.RequestModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FSCMS.Service.Interfaces
{
    /// <summary>
    /// Interface for appointment management operations
    /// </summary>
    public interface IAppointmentService
    {
        #region Appointment CRUD Operations

        /// <summary>
        /// Get appointment by ID
        /// </summary>
        /// <param name="appointmentId">The unique identifier of the appointment</param>
        /// <returns>BaseResponse containing appointment information</returns>
        Task<BaseResponse<AppointmentResponse>> GetAppointmentByIdAsync(Guid appointmentId);

        /// <summary>
        /// Get detailed appointment by ID with related data
        /// </summary>
        /// <param name="appointmentId">The unique identifier of the appointment</param>
        /// <returns>BaseResponse containing detailed appointment information</returns>
        Task<BaseResponse<AppointmentDetailResponse>> GetAppointmentDetailByIdAsync(Guid appointmentId);

        /// <summary>
        /// Get all appointments with pagination and filtering
        /// </summary>
        /// <param name="request">Request parameters for filtering and pagination</param>
        /// <returns>DynamicResponse containing paginated appointment list</returns>
        Task<DynamicResponse<AppointmentResponse>> GetAllAppointmentsAsync(GetAppointmentsRequest request);

        /// <summary>
        /// Get appointments for a specific treatment cycle
        /// </summary>
        /// <param name="treatmentCycleId">The unique identifier of the treatment cycle</param>
        /// <param name="request">Request parameters for filtering and pagination</param>
        /// <returns>DynamicResponse containing paginated appointment list</returns>
        Task<DynamicResponse<AppointmentResponse>> GetAppointmentsByTreatmentCycleIdAsync(Guid treatmentCycleId, GetAppointmentsRequest request);

        /// <summary>
        /// Get appointments for a specific doctor
        /// </summary>
        /// <param name="doctorId">The unique identifier of the doctor</param>
        /// <param name="request">Request parameters for filtering and pagination</param>
        /// <returns>DynamicResponse containing paginated appointment list</returns>
        Task<DynamicResponse<AppointmentResponse>> GetAppointmentsByDoctorIdAsync(Guid doctorId, GetAppointmentsRequest request);

        /// <summary>
        /// Get appointments for a specific slot
        /// </summary>
        /// <param name="slotId">The unique identifier of the slot</param>
        /// <returns>BaseResponse containing appointment information</returns>
        Task<BaseResponse<AppointmentResponse>> GetAppointmentBySlotIdAsync(Guid slotId);

        /// <summary>
        /// Create new appointment
        /// </summary>
        /// <param name="request">Appointment creation request</param>
        /// <returns>BaseResponse containing created appointment information</returns>
        Task<BaseResponse<AppointmentResponse>> CreateAppointmentAsync(CreateAppointmentRequest request);

        /// <summary>
        /// Update existing appointment
        /// </summary>
        /// <param name="appointmentId">The unique identifier of the appointment</param>
        /// <param name="request">Appointment update request</param>
        /// <returns>BaseResponse containing updated appointment information</returns>
        Task<BaseResponse<AppointmentResponse>> UpdateAppointmentAsync(Guid appointmentId, UpdateAppointmentRequest request);

        /// <summary>
        /// Delete appointment (soft delete)
        /// </summary>
        /// <param name="appointmentId">The unique identifier of the appointment</param>
        /// <returns>BaseResponse indicating success or failure</returns>
        Task<BaseResponse> DeleteAppointmentAsync(Guid appointmentId);

        #endregion

        #region Appointment Status Operations

        /// <summary>
        /// Update appointment status
        /// </summary>
        /// <param name="appointmentId">The unique identifier of the appointment</param>
        /// <param name="status">The new status</param>
        /// <returns>BaseResponse indicating success or failure</returns>
        Task<BaseResponse> UpdateAppointmentStatusAsync(Guid appointmentId, AppointmentStatus status);

        /// <summary>
        /// Check in for an appointment
        /// </summary>
        /// <param name="appointmentId">The unique identifier of the appointment</param>
        /// <returns>BaseResponse indicating success or failure</returns>
        Task<BaseResponse> CheckInAppointmentAsync(Guid appointmentId);

        /// <summary>
        /// Check out for an appointment
        /// </summary>
        /// <param name="appointmentId">The unique identifier of the appointment</param>
        /// <returns>BaseResponse indicating success or failure</returns>
        Task<BaseResponse> CheckOutAppointmentAsync(Guid appointmentId);

        /// <summary>
        /// Cancel an appointment
        /// </summary>
        /// <param name="appointmentId">The unique identifier of the appointment</param>
        /// <param name="cancellationReason">Reason for cancellation</param>
        /// <returns>BaseResponse indicating success or failure</returns>
        Task<BaseResponse> CancelAppointmentAsync(Guid appointmentId, string? cancellationReason = null);

        #endregion

        #region Appointment Doctor Management

        /// <summary>
        /// Add doctor to appointment
        /// </summary>
        /// <param name="appointmentId">The unique identifier of the appointment</param>
        /// <param name="doctorId">The unique identifier of the doctor</param>
        /// <param name="role">Role of the doctor in the appointment</param>
        /// <param name="notes">Additional notes</param>
        /// <returns>BaseResponse indicating success or failure</returns>
        Task<BaseResponse> AddDoctorToAppointmentAsync(Guid appointmentId, Guid doctorId, string? role = null, string? notes = null);

        /// <summary>
        /// Remove doctor from appointment
        /// </summary>
        /// <param name="appointmentId">The unique identifier of the appointment</param>
        /// <param name="doctorId">The unique identifier of the doctor</param>
        /// <returns>BaseResponse indicating success or failure</returns>
        Task<BaseResponse> RemoveDoctorFromAppointmentAsync(Guid appointmentId, Guid doctorId);

        /// <summary>
        /// Update doctor role in appointment
        /// </summary>
        /// <param name="appointmentId">The unique identifier of the appointment</param>
        /// <param name="doctorId">The unique identifier of the doctor</param>
        /// <param name="role">New role of the doctor</param>
        /// <param name="notes">Additional notes</param>
        /// <returns>BaseResponse indicating success or failure</returns>
        Task<BaseResponse> UpdateDoctorRoleInAppointmentAsync(Guid appointmentId, Guid doctorId, string? role, string? notes = null);

        #endregion
    }
}


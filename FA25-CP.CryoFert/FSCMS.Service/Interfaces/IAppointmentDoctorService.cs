using FSCMS.Service.ReponseModel;
using FSCMS.Service.RequestModel;
using System;
using System.Threading.Tasks;

namespace FSCMS.Service.Interfaces
{
    /// <summary>
    /// Service interface for managing AppointmentDoctor relationships (Appointment <-> Doctor).
    /// </summary>
    public interface IAppointmentDoctorService
    {
        Task<BaseResponse<AppointmentDoctorResponse>> GetByIdAsync(Guid appointmentDoctorId);
        Task<DynamicResponse<AppointmentDoctorResponse>> GetAllAsync(GetAppointmentDoctorsRequest request);
        Task<DynamicResponse<AppointmentDoctorResponse>> GetByAppointmentIdAsync(Guid appointmentId, GetAppointmentDoctorsRequest request);
        Task<DynamicResponse<AppointmentDoctorResponse>> GetByDoctorIdAsync(Guid doctorId, GetAppointmentDoctorsRequest request);

        Task<BaseResponse<AppointmentDoctorResponse>> CreateAsync(CreateAppointmentDoctorRequest request);
        Task<BaseResponse<AppointmentDoctorResponse>> UpdateAsync(Guid appointmentDoctorId, UpdateAppointmentDoctorRequest request);
        Task<BaseResponse> DeleteAsync(Guid appointmentDoctorId);
    }
}



using AutoMapper;
using FSCMS.Core.Entities;
using FSCMS.Service.ReponseModel;
using FSCMS.Service.RequestModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSCMS.Service.Mapping
{
    /// <summary>
    /// AutoMapper profile for Doctor-related entities
    /// </summary>
    public class DoctorMappingProfile : Profile
    {
        public DoctorMappingProfile()
        {
            #region Doctor Mappings

            // Map Doctor entity to DoctorResponse
            CreateMap<Doctor, DoctorResponse>()
                .ForMember(dest => dest.Account, opt => opt.MapFrom(src => src.Account));

            // Map Doctor entity to DoctorDetailResponse
            CreateMap<Doctor, DoctorDetailResponse>()
                .IncludeBase<Doctor, DoctorResponse>()
                .ForMember(dest => dest.TotalSchedules, opt => opt.MapFrom(src => src.DoctorSchedules.Count))
                .ForMember(dest => dest.TotalTreatments, opt => opt.MapFrom(src => src.Treatments.Count))
                .ForMember(dest => dest.UpcomingSchedules, opt => opt.MapFrom(src => 
                    src.DoctorSchedules
                        .Where(s => s.WorkDate >= DateTime.Today && !s.IsDeleted)
                        .OrderBy(s => s.WorkDate)
                        .Take(5)))
                .ForMember(dest => dest.RecentTreatments, opt => opt.MapFrom(src => 
                    src.Treatments.Count(t => t.CreatedAt >= DateTime.Today.AddDays(-30) && !t.IsDeleted)));

            // Map Doctor entity to DoctorBasicInfo
            CreateMap<Doctor, DoctorBasicInfo>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => 
                    src.Account != null ? $"{src.Account.FirstName} {src.Account.LastName}".Trim() : string.Empty));

            // Map Account entity to DoctorAccountInfo
            CreateMap<Account, DoctorAccountInfo>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName));

            // Map CreateDoctorRequest to Doctor entity
            CreateMap<CreateDoctorRequest, Doctor>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => false))
                .ForMember(dest => dest.DeletedAt, opt => opt.Ignore())
                .ForMember(dest => dest.Account, opt => opt.Ignore())
                .ForMember(dest => dest.DoctorSchedules, opt => opt.Ignore())
                .ForMember(dest => dest.Treatments, opt => opt.Ignore());

            // Map UpdateDoctorRequest to Doctor entity
            CreateMap<UpdateDoctorRequest, Doctor>()
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.AccountId, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
                .ForMember(dest => dest.DeletedAt, opt => opt.Ignore())
                .ForMember(dest => dest.Account, opt => opt.Ignore())
                .ForMember(dest => dest.DoctorSchedules, opt => opt.Ignore())
                .ForMember(dest => dest.Treatments, opt => opt.Ignore())
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            #endregion

            #region DoctorSchedule Mappings

            // Map DoctorSchedule entity to DoctorScheduleResponse
            CreateMap<DoctorSchedule, DoctorScheduleResponse>()
                .ForMember(dest => dest.Doctor, opt => opt.MapFrom(src => src.Doctor))
                .ForMember(dest => dest.TotalSlots, opt => opt.MapFrom(src => src.Slots.Count))
                .ForMember(dest => dest.AvailableSlots, opt => opt.MapFrom(src => src.Slots.Count(s => !s.IsBooked && !s.IsDeleted)))
                .ForMember(dest => dest.BookedSlots, opt => opt.MapFrom(src => src.Slots.Count(s => s.IsBooked && !s.IsDeleted)));

            // Map DoctorSchedule entity to DoctorScheduleDetailResponse
            CreateMap<DoctorSchedule, DoctorScheduleDetailResponse>()
                .IncludeBase<DoctorSchedule, DoctorScheduleResponse>()
                .ForMember(dest => dest.Slots, opt => opt.MapFrom(src => 
                    src.Slots.Where(s => !s.IsDeleted).OrderBy(s => s.StartTime)));

            // Map CreateDoctorScheduleRequest to DoctorSchedule entity
            CreateMap<CreateDoctorScheduleRequest, DoctorSchedule>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => false))
                .ForMember(dest => dest.DeletedAt, opt => opt.Ignore())
                .ForMember(dest => dest.Doctor, opt => opt.Ignore())
                .ForMember(dest => dest.Slots, opt => opt.Ignore());

            // Map UpdateDoctorScheduleRequest to DoctorSchedule entity
            CreateMap<UpdateDoctorScheduleRequest, DoctorSchedule>()
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.DoctorId, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
                .ForMember(dest => dest.DeletedAt, opt => opt.Ignore())
                .ForMember(dest => dest.Doctor, opt => opt.Ignore())
                .ForMember(dest => dest.Slots, opt => opt.Ignore())
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            #endregion

            #region Slot Mappings

            // Map Slot entity to SlotResponse
            CreateMap<Slot, SlotResponse>()
                .ForMember(dest => dest.Schedule, opt => opt.MapFrom(src => src.DoctorSchedule));

            // Map Slot entity to SlotDetailResponse
            CreateMap<Slot, SlotDetailResponse>()
                .IncludeBase<Slot, SlotResponse>()
                .ForMember(dest => dest.Appointment, opt => opt.MapFrom(src => src.Appointment));

            // Map DoctorSchedule entity to SlotScheduleInfo
            CreateMap<DoctorSchedule, SlotScheduleInfo>()
                .ForMember(dest => dest.Doctor, opt => opt.MapFrom(src => src.Doctor));

            // Map Appointment entity to SlotAppointmentInfo (if Appointment entity exists)
            // CreateMap<Appointment, SlotAppointmentInfo>()
            //     .ForMember(dest => dest.PatientName, opt => opt.MapFrom(src => 
            //         src.Patient != null && src.Patient.Account != null 
            //             ? $"{src.Patient.Account.FirstName} {src.Patient.Account.LastName}".Trim() 
            //             : string.Empty));

            // Map CreateSlotRequest to Slot entity
            CreateMap<CreateSlotRequest, Slot>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => false))
                .ForMember(dest => dest.DeletedAt, opt => opt.Ignore())
                .ForMember(dest => dest.DoctorSchedule, opt => opt.Ignore())
                .ForMember(dest => dest.Appointment, opt => opt.Ignore());

            // Map UpdateSlotRequest to Slot entity
            CreateMap<UpdateSlotRequest, Slot>()
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.DoctorScheduleId, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
                .ForMember(dest => dest.DeletedAt, opt => opt.Ignore())
                .ForMember(dest => dest.DoctorSchedule, opt => opt.Ignore())
                .ForMember(dest => dest.Appointment, opt => opt.Ignore())
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            #endregion
        }
    }
}

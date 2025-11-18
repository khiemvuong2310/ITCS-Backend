using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FSCMS.Core.Entities;
using FSCMS.Service.ReponseModel;
using FSCMS.Service.RequestModel;

namespace FSCMS.Service.Mapping
{
    public class MedicalRecordMappingProfile : Profile
    {
        public MedicalRecordMappingProfile()
        {
            // ============================
            //  ENTITY → RESPONSE
            // ============================

            // Map MedicalRecord → MedicalRecordResponse
            CreateMap<MedicalRecord, MedicalRecordResponse>()
                .ForMember(dest => dest.AppointmentDate,
                    opt => opt.MapFrom(src =>src.Appointment.AppointmentDate))
                .ForMember(dest => dest.PatientId,
                    opt => opt.MapFrom(src =>src.Appointment.PatientId))
                .ForMember(dest => dest.PatientName,
                    opt => opt.MapFrom(src =>$"{src.Appointment.Patient.Account.FirstName} {src.Appointment.Patient.Account.LastName}"))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt));

            // Map MedicalRecord → MedicalRecordDetailResponse (inherit from MedicalRecordResponse)
            CreateMap<MedicalRecord, MedicalRecordDetailResponse>()
                .IncludeBase<MedicalRecord, MedicalRecordResponse>()
                .ForMember(dest => dest.Prescriptions, opt => opt.MapFrom(src => src.Prescriptions));

            // ============================
            //  REQUEST → ENTITY (CREATE)
            // ============================

            CreateMap<CreateMedicalRecordRequest, MedicalRecord>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                //.ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.Appointment, opt => opt.Ignore())
                .ForMember(dest => dest.Prescriptions, opt => opt.Ignore());

            // ============================
            //  REQUEST → ENTITY (UPDATE)
            // ============================

            CreateMap<UpdateMedicalRecordRequest, MedicalRecord>()
                //.ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.Id, opt => opt.Ignore())                 
                .ForMember(dest => dest.AppointmentId, opt => opt.Ignore())     
                .ForMember(dest => dest.Appointment, opt => opt.Ignore())
                .ForMember(dest => dest.Prescriptions, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForAllMembers(opt => opt.Condition((src, dest, member) => member != null));
        }
    }
}

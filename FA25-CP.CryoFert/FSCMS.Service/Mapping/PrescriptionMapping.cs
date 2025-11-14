using AutoMapper;
using FSCMS.Core.Entities;
using FSCMS.Service.ReponseModel;
using FSCMS.Service.RequestModel;
using System;
using System.Collections.Generic;

namespace FSCMS.Service.Mapping
{
    public class PrescriptionMappingProfile : Profile
    {
        public PrescriptionMappingProfile()
        {
            // Map Prescription -> PrescriptionResponse
            CreateMap<Prescription, PrescriptionResponse>()
                .ForMember(dest => dest.MedicalRecordDiagnosis, opt => opt.MapFrom(src => src.MedicalRecord != null ? src.MedicalRecord.Diagnosis : null))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt));

            // Map Prescription -> PrescriptionDetailResponse (include PrescriptionDetails)
            CreateMap<Prescription, PrescriptionDetailResponse>()
                .IncludeBase<Prescription, PrescriptionResponse>()
                .ForMember(dest => dest.PrescriptionDetails, opt => opt.MapFrom(src => src.PrescriptionDetails));

            // Map PrescriptionDetail -> PrescriptionItemResponse
            CreateMap<PrescriptionDetail, PrescriptionItemResponse>()
                .ForMember(dest => dest.MedicineName, opt => opt.MapFrom(src => src.Medicine != null ? src.Medicine.Name : null))
                .ForMember(dest => dest.Dosage, opt => opt.MapFrom(src => src.Medicine != null ? src.Medicine.Dosage : src.Dosage))
                .ForMember(dest => dest.Form, opt => opt.MapFrom(src => src.Medicine != null ? src.Medicine.Form : null));

            // Map CreatePrescriptionRequest -> Prescription
            CreateMap<CreatePrescriptionRequest, Prescription>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow))
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.PrescriptionDetails, opt => opt.Ignore())
                .ForMember(dest => dest.MedicalRecord, opt => opt.Ignore());

            // Map UpdatePrescriptionRequest -> Prescription (only non-null)
            CreateMap<UpdatePrescriptionRequest, Prescription>()
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow))
                .ForMember(dest => dest.MedicalRecordId, opt => opt.Ignore())
                .ForMember(dest => dest.PrescriptionDetails, opt => opt.Ignore())
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));

            // Map CreatePrescriptionDetailRequest -> PrescriptionDetail
            CreateMap<CreatePrescriptionDetailRequest, PrescriptionDetail>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Medicine, opt => opt.Ignore())
                .ForMember(dest => dest.Prescription, opt => opt.Ignore());
        }
    }
}

using AutoMapper;
using FSCMS.Core.Entities;
using FSCMS.Service.ReponseModel;
using FSCMS.Service.RequestModel;
using System;

namespace FSCMS.Service.Mapping
{
    /// <summary>
    /// AutoMapper profile for Agreement entity mappings
    /// </summary>
    public class AgreementMappingProfile : Profile
    {
        public AgreementMappingProfile()
        {
            // Map Agreement -> AgreementResponse
            CreateMap<Agreement, AgreementResponse>()
                .ForMember(dest => dest.TreatmentName, opt => opt.MapFrom(src => src.Treatment != null ? src.Treatment.TreatmentName : null))
                .ForMember(dest => dest.PatientName, opt => opt.MapFrom(src => 
                    src.Patient != null && src.Patient.Account != null 
                        ? $"{src.Patient.Account.FirstName} {src.Patient.Account.LastName}".Trim() 
                        : null))
                .ForMember(dest => dest.StatusName, opt => opt.MapFrom(src => src.Status.ToString()))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt));

            // Map Agreement -> AgreementDetailResponse (include full Treatment and Patient info)
            CreateMap<Agreement, AgreementDetailResponse>()
                .IncludeBase<Agreement, AgreementResponse>()
                .ForMember(dest => dest.Treatment, opt => opt.MapFrom(src => src.Treatment != null ? new TreatmentBasicInfo
                {
                    Id = src.Treatment.Id,
                    Name = src.Treatment.TreatmentName,
                    Description = src.Treatment.Diagnosis ?? src.Treatment.Notes
                } : null))
                .ForMember(dest => dest.Patient, opt => opt.MapFrom(src => src.Patient != null && src.Patient.Account != null ? new PatientBasicInfo
                {
                    Id = src.Patient.Id,
                    FullName = $"{src.Patient.Account.FirstName} {src.Patient.Account.LastName}".Trim(),
                    Email = src.Patient.Account.Email,
                    PhoneNumber = src.Patient.Account.Phone
                } : null));

            // Map CreateAgreementRequest -> Agreement
            // Note: Agreement entity uses constructor, so we'll map manually in service
            CreateMap<CreateAgreementRequest, Agreement>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.AgreementCode, opt => opt.Ignore()) // Generated in service
                .ForMember(dest => dest.Status, opt => opt.Ignore()) // Set to Pending in service
                .ForMember(dest => dest.SignedByPatient, opt => opt.Ignore()) // Set to false in service
                .ForMember(dest => dest.SignedByDoctor, opt => opt.Ignore()) // Set to false in service
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
                .ForMember(dest => dest.Treatment, opt => opt.Ignore())
                .ForMember(dest => dest.Patient, opt => opt.Ignore());

            // Map UpdateAgreementRequest -> Agreement (only non-null properties)
            CreateMap<UpdateAgreementRequest, Agreement>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.AgreementCode, opt => opt.Ignore())
                .ForMember(dest => dest.TreatmentId, opt => opt.Ignore())
                .ForMember(dest => dest.PatientId, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
                .ForMember(dest => dest.Treatment, opt => opt.Ignore())
                .ForMember(dest => dest.Patient, opt => opt.Ignore())
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}


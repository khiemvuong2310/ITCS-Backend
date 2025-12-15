using AutoMapper;
using FSCMS.Core.Entities;
using FSCMS.Core.Enum;
using FSCMS.Service.RequestModel;
using FSCMS.Service.ReponseModel;
using System;

namespace FSCMS.Service.Mapping
{
    public class LabSampleMappingProfile : Profile
    {
        public LabSampleMappingProfile()
        {
            // =========================================================
            // ENTITY → RESPONSE
            // =========================================================
            CreateMap<LabSample, LabSampleResponse>()
                .ForMember(dest => dest.IsStoraged, opt => opt.MapFrom(src => src.StorageDate.HasValue))
                .ForMember(dest => dest.IsAvailable, opt => opt.MapFrom(src => src.IsAvailable))
                .ForMember(dest => dest.Quality, opt => opt.MapFrom(src => src.Quality))
                .ForMember(dest => dest.Notes, opt => opt.MapFrom(src => src.Notes))
                .ForMember(dest => dest.StorageDate, opt => opt.MapFrom(src => src.StorageDate))
                .ForMember(dest => dest.ExpiryDate, opt => opt.MapFrom(src => src.ExpiryDate))
                .ForMember(dest => dest.SampleType, opt => opt.MapFrom(src => src.SampleType))
                .ForMember(dest => dest.CollectionDate, opt => opt.MapFrom(src => src.CollectionDate))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status));

            CreateMap<LabSample, LabSampleDetailResponse>()
                .IncludeBase<LabSample, LabSampleResponse>()
                .ForMember(dest => dest.Patient, opt => opt.MapFrom(src => src.Patient))
                .ForMember(dest => dest.Sperm, opt => opt.MapFrom(src => src.LabSampleSperm))
                .ForMember(dest => dest.Oocyte, opt => opt.MapFrom(src => src.LabSampleOocyte))
                .ForMember(dest => dest.Embryo, opt => opt.MapFrom(src => src.LabSampleEmbryo));

            // Basic patient info
            CreateMap<Patient, PatientBasicInfo>();

            // Type-specific entity → response
            CreateMap<LabSampleSperm, LabSampleSpermDto>();
            CreateMap<LabSampleOocyte, LabSampleOocyteDto>();
            CreateMap<LabSampleEmbryo, LabSampleEmbryoDto>();


            // =========================================================
            // REQUEST → ENTITY
            // =========================================================

            // ======= CREATE: SPERM =======
            CreateMap<CreateLabSampleSpermRequest, LabSample>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.SampleType, opt => opt.MapFrom(_ => SampleType.Sperm))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(_ => SpecimenStatus.Collected))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow))
                .ForMember(dest => dest.CollectionDate, opt => opt.MapFrom(_ => DateTime.UtcNow))
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.LabSampleSperm, opt => opt.MapFrom(src => src))
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));

            // ======= CREATE: OOCYTE =======
            CreateMap<CreateLabSampleOocyteRequest, LabSample>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.SampleType, opt => opt.MapFrom(_ => SampleType.Oocyte))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(_ => SpecimenStatus.Collected))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow))
                .ForMember(dest => dest.CollectionDate, opt => opt.MapFrom(_ => DateTime.UtcNow))
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.LabSampleOocyte, opt => opt.MapFrom(src => src))
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));

            // ======= CREATE: EMBRYO =======
            CreateMap<CreateLabSampleEmbryoRequest, LabSample>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.SampleType, opt => opt.MapFrom(_ => SampleType.Embryo))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(_ => SpecimenStatus.Collected))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow))
                .ForMember(dest => dest.CollectionDate, opt => opt.MapFrom(_ => DateTime.UtcNow))
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.LabSampleEmbryo, opt => opt.MapFrom(src => src))
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));

            // ======= UPDATE: SPERM =======
            CreateMap<UpdateLabSampleSpermRequest, LabSample>()
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow))
                .ForMember(dest => dest.LabSampleSperm, opt => opt.MapFrom(src => src))
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));

            // ======= UPDATE: OOCYTE =======
            CreateMap<UpdateLabSampleOocyteRequest, LabSample>()
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow))
                .ForMember(dest => dest.LabSampleOocyte, opt => opt.MapFrom(src => src))
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));

            // ======= UPDATE: EMBRYO =======
            CreateMap<UpdateLabSampleEmbryoRequest, LabSample>()
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow))
                .ForMember(dest => dest.LabSampleEmbryo, opt => opt.MapFrom(src => src))
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));


            // ======= SUB ENTITY MAPPING =======
            CreateMap<CreateLabSampleSpermRequest, LabSampleSperm>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<CreateLabSampleOocyteRequest, LabSampleOocyte>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<CreateLabSampleEmbryoRequest, LabSampleEmbryo>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            // Update type-specific
            CreateMap<UpdateLabSampleSpermRequest, LabSampleSperm>()
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<UpdateLabSampleOocyteRequest, LabSampleOocyte>()
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<UpdateLabSampleEmbryoRequest, LabSampleEmbryo>()
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}

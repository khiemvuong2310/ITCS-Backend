using AutoMapper;
using FSCMS.Core.Entities;
using FSCMS.Service.ReponseModel;
using FSCMS.Service.RequestModel;
using System;

namespace FSCMS.Service.Mapping
{
    public class LabSampleMappingProfile : Profile
    {
        public LabSampleMappingProfile()
        {
            // =============================
            // Entity → Response
            // =============================
            CreateMap<LabSample, LabSampleResponse>()
                .ForMember(dest => dest.Patient, opt => opt.MapFrom(src => src.Patient))
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
                .ForMember(dest => dest.Sperm, opt => opt.MapFrom(src => src.LabSampleSperm))
                .ForMember(dest => dest.Oocyte, opt => opt.MapFrom(src => src.LabSampleOocyte))
                .ForMember(dest => dest.Embryo, opt => opt.MapFrom(src => src.LabSampleEmbryo));

            // Patient basic info mapping
            CreateMap<Patient, PatientBasicInfo>();

            // Type-specific: entity → response DTOs
            CreateMap<LabSampleSperm, LabSampleSpermDto>();
            CreateMap<LabSampleOocyte, LabSampleOocyteDto>();
            CreateMap<LabSampleEmbryo, LabSampleEmbryoDto>();

            // =============================
            // Request → Entity
            // =============================

            // Create base lab sample
            CreateMap<CreateLabSampleRequest, LabSample>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow))
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.StorageDate, opt => opt.Ignore())
                .ForMember(dest => dest.ExpiryDate, opt => opt.Ignore())
                .ForMember(dest => dest.Status, opt => opt.MapFrom(_ => Core.Enum.SpecimenStatus.Donated))
                .ForMember(dest => dest.LabSampleSperm, opt => opt.MapFrom(src => src.Sperm))
                .ForMember(dest => dest.LabSampleOocyte, opt => opt.MapFrom(src => src.Oocyte))
                .ForMember(dest => dest.LabSampleEmbryo, opt => opt.MapFrom(src => src.Embryo))
                .ForMember(dest => dest.Patient, opt => opt.Ignore());

            // Update base lab sample
            CreateMap<UpdateLabSampleRequest, LabSample>()
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow))
                .ForMember(dest => dest.LabSampleSperm, opt => opt.MapFrom(src => src.Sperm))
                .ForMember(dest => dest.LabSampleOocyte, opt => opt.MapFrom(src => src.Oocyte))
                .ForMember(dest => dest.LabSampleEmbryo, opt => opt.MapFrom(src => src.Embryo))
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            // Type-specific: request → entity
            CreateMap<CreateLabSampleSpermRequest, LabSampleSperm>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<CreateLabSampleOocyteRequest, LabSampleOocyte>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<CreateLabSampleEmbryoRequest, LabSampleEmbryo>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            // Update
            CreateMap<UpdateLabSampleSpermRequest, LabSampleSperm>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<UpdateLabSampleOocyteRequest, LabSampleOocyte>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<UpdateLabSampleEmbryoRequest, LabSampleEmbryo>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}

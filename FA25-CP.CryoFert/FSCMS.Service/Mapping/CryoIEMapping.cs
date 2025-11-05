using AutoMapper;
using FSCMS.Core.Entities;
using FSCMS.Service.ReponseModel;
using FSCMS.Service.RequestModel;
using System;

namespace FSCMS.Service.Mapping
{
    public class CryoIEMapping : Profile
    {
        public CryoIEMapping()
        {

            // Map CryoImport entity to CryoImportResponse
            CreateMap<CryoImport, CryoImportResponse>()
                .ForMember(dest => dest.LabSample, opt => opt.MapFrom(src => src.LabSample))
                .ForMember(dest => dest.CryoLocation, opt => opt.MapFrom(src => src.CryoLocation));

            // Map CreateCryoImportRequest to CryoImport entity
            CreateMap<CreateCryoImportRequest, CryoImport>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => false))
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));

            // Map UpdateCryoImportRequest to CryoImport entity
            CreateMap<UpdateCryoImportRequest, CryoImport>()
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));

            // Map CryoExport entity to CryoExportResponse
            CreateMap<CryoExport, CryoExportResponse>()
                .ForMember(dest => dest.LabSample, opt => opt.MapFrom(src => src.LabSample))
                .ForMember(dest => dest.CryoLocation, opt => opt.MapFrom(src => src.CryoLocation));

            // Map CreateCryoExportRequest to CryoExport entity
            CreateMap<CreateCryoExportRequest, CryoExport>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => false))
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));

            // Map UpdateCryoExportRequest to CryoExport entity
            CreateMap<UpdateCryoExportRequest, CryoExport>()
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}

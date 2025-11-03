using AutoMapper;
using FSCMS.Core.Entities;
using FSCMS.Service.RequestModel;
using FSCMS.Service.ReponseModel;

namespace FSCMS.Service.Mapping;

public class CryoLocationMapping : Profile
{
    public CryoLocationMapping()
    {
        // Request → Entity
        CreateMap<CryoLocationCreateRequest, CryoLocation>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Children, opt => opt.Ignore())
            .ForMember(dest => dest.LabSamples, opt => opt.Ignore())
            .ForMember(dest => dest.CryoImports, opt => opt.Ignore())
            .ForMember(dest => dest.CryoExports, opt => opt.Ignore());

        CreateMap<CryoLocationUpdateRequest, CryoLocation>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Children, opt => opt.Ignore())
            .ForMember(dest => dest.LabSamples, opt => opt.Ignore())
            .ForMember(dest => dest.CryoImports, opt => opt.Ignore())
            .ForMember(dest => dest.CryoExports, opt => opt.Ignore());

        // Entity → Response
        CreateMap<CryoLocation, CryoLocationResponse>()
            .ForMember(dest => dest.Children, opt => opt.MapFrom(src => src.Children));
    }
}

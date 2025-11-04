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

        // CryoLocation -> CryoLocationResponse
        CreateMap<CryoLocation, CryoLocationResponse>()
            .ForMember(dest => dest.AvailableCapacity,
                       opt => opt.MapFrom(src => (src.Capacity ?? 0) - src.SampleCount));

        // CryoLocation -> CryoLocationSummaryResponse
        CreateMap<CryoLocation, CryoLocationSummaryResponse>();


        // CryoLocation -> CryoLocationFullTreeResponse (recursive)
        CreateMap<CryoLocation, CryoLocationFullTreeResponse>()
            .ForMember(dest => dest.AvailableCapacity,
                       opt => opt.MapFrom(src => (src.Capacity ?? 0) - src.SampleCount))
            .ForMember(dest => dest.Children,
                       opt => opt.MapFrom(src => src.Children != null
                                                 ? src.Children.Where(c => !c.IsDeleted).ToList()
                                                 : new List<CryoLocation>()));

    }
}

using AutoMapper;
using FSCMS.Core.Entities;
using FSCMS.Service.ReponseModel;
using FSCMS.Service.RequestModel;
using System;
using System.Collections.Generic;

namespace FSCMS.Service.Mapping
{
    public class CryoPackageMappingProfile : Profile
    {
        public CryoPackageMappingProfile()
        {
            // Map CryoPackage entity -> CryoPackageResponse
            CreateMap<CryoPackage, CryoPackageResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.PackageName, opt => opt.MapFrom(src => src.PackageName))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
                .ForMember(dest => dest.DurationMonths, opt => opt.MapFrom(src => src.DurationMonths))
                .ForMember(dest => dest.MaxSamples, opt => opt.MapFrom(src => src.MaxSamples))
                .ForMember(dest => dest.SampleType, opt => opt.MapFrom(src => src.SampleType))
                .ForMember(dest => dest.IncludesInsurance, opt => opt.MapFrom(src => src.IncludesInsurance))
                .ForMember(dest => dest.InsuranceAmount, opt => opt.MapFrom(src => src.InsuranceAmount))
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.IsActive))
                .ForMember(dest => dest.Benefits, opt => opt.MapFrom(src => src.Benefits))
                .ForMember(dest => dest.Notes, opt => opt.MapFrom(src => src.Notes))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt));

            // Map CryoPackage entity -> CryoPackageDetailResponse (include contracts info)
            CreateMap<CryoPackage, CryoPackageDetailResponse>()
                .IncludeBase<CryoPackage, CryoPackageResponse>()
                .ForMember(dest => dest.TotalContracts, opt => opt.MapFrom(src => src.CryoStorageContracts != null ? src.CryoStorageContracts.Count : 0))
                .ForMember(dest => dest.Contracts, opt => opt.MapFrom(src => src.CryoStorageContracts));
            
            // Map CreateCryoPackageRequest -> CryoPackage
            CreateMap<CreateCryoPackageRequest, CryoPackage>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.IsActive))
                .ForMember(dest => dest.CryoStorageContracts, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore());

            // Map UpdateCryoPackageRequest -> CryoPackage
            CreateMap<UpdateCryoPackageRequest, CryoPackage>()
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}

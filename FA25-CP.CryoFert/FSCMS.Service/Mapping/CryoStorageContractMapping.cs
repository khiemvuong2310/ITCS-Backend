using AutoMapper;
using FSCMS.Core.Entities;
using FSCMS.Service.ReponseModel;
using FSCMS.Service.RequestModel;
using System;

namespace FSCMS.Service.Mapping
{
    public class CryoStorageContractMappingProfile : Profile
    {
        public CryoStorageContractMappingProfile()
        {
            // Map CryoStorageContract -> CryoStorageContractResponse
            CreateMap<CryoStorageContract, CryoStorageContractResponse>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
                .ForMember(dest => dest.PatientName, opt => opt.Ignore())
                .ForMember(dest => dest.CryoPackageName, opt => opt.MapFrom(src => src.CryoPackage != null ? src.CryoPackage.PackageName : null))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt));

            // Map CryoStorageContract -> CryoStorageContractDetailResponse (include Samples)
            CreateMap<CryoStorageContract, CryoStorageContractDetailResponse>()
                .IncludeBase<CryoStorageContract, CryoStorageContractResponse>()
                .ForMember(dest => dest.Samples, opt => opt.MapFrom(src => src.CPSDetails));

            // Map CPSDetail -> CPSDetailResponse
            CreateMap<CPSDetail, CPSDetailResponse>()
                .ForMember(dest => dest.SampleCode, opt => opt.MapFrom(src => src.LabSample != null ? src.LabSample.SampleCode : null))
                .ForMember(dest => dest.SampleType, opt => opt.MapFrom(src => src.LabSample != null ? src.LabSample.SampleType.ToString() : null))
                .ForMember(dest => dest.StorageLocation, opt => opt.Ignore())
                .ForMember(dest => dest.IsActive, opt => opt.Ignore());

            // Map CreateCryoStorageContractRequest -> CryoStorageContract
            CreateMap<CreateCryoStorageContractRequest, CryoStorageContract>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.ContractNumber, opt => opt.Ignore()) // sinh tự động trong service
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow))
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.PaidAmount, opt => opt.MapFrom(_ => 0))
                .ForMember(dest => dest.CPSDetails, opt => opt.Ignore())
                .ForMember(dest => dest.Patient, opt => opt.Ignore())
                .ForMember(dest => dest.CryoPackage, opt => opt.Ignore());

            CreateMap<RenewCryoStorageContractRequest, CryoStorageContract>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.ContractNumber, opt => opt.Ignore()) // sinh tự động trong service
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow))
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.PaidAmount, opt => opt.MapFrom(_ => 0))
                .ForMember(dest => dest.CPSDetails, opt => opt.Ignore())
                .ForMember(dest => dest.Patient, opt => opt.Ignore())
                .ForMember(dest => dest.CryoPackage, opt => opt.Ignore());

            // Map UpdateCryoStorageContractRequest -> CryoStorageContract (only non-null)
            CreateMap<UpdateCryoStorageContractRequest, CryoStorageContract>()
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow))
                .ForMember(dest => dest.PatientId, opt => opt.Ignore())
                .ForMember(dest => dest.CryoPackageId, opt => opt.Ignore())
                .ForMember(dest => dest.CPSDetails, opt => opt.Ignore())
                .ForMember(dest => dest.ContractNumber, opt => opt.Ignore())
                .ForMember(dest => dest.Patient, opt => opt.Ignore())
                .ForMember(dest => dest.CryoPackage, opt => opt.Ignore())
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}

using AutoMapper;
using FSCMS.Core.Entities;
using FSCMS.Service.ReponseModel;
using FSCMS.Service.RequestModel;

namespace FSCMS.Service.Mapping
{
    /// <summary>
    /// AutoMapper profile for Patient and Relationship entities
    /// </summary>
    public class PatientMappingProfile : Profile
    {
        public PatientMappingProfile()
        {
            CreatePatientMappings();
            CreateRelationshipMappings();
        }

        private void CreatePatientMappings()
        {
            // Patient Entity to Response mappings
            CreateMap<Patient, PatientResponse>()
                .ForMember(dest => dest.TreatmentCount, opt => opt.MapFrom(src => src.Treatments.Count))
                .ForMember(dest => dest.LabSampleCount, opt => opt.MapFrom(src => src.LabSamples.Count))
                .ForMember(dest => dest.RelationshipCount, opt => opt.MapFrom(src => 
                    src.RelationshipsAsPatient1.Count + src.RelationshipsAsPatient2.Count))
                .ForMember(dest => dest.AccountInfo, opt => opt.MapFrom(src => src.Account));

            CreateMap<Patient, PatientDetailResponse>()
                .IncludeBase<Patient, PatientResponse>()
                .ForMember(dest => dest.Relationships, opt => opt.MapFrom(src => 
                    src.RelationshipsAsPatient1.Concat(src.RelationshipsAsPatient2)))
                .ForMember(dest => dest.Treatments, opt => opt.MapFrom(src => src.Treatments))
                .ForMember(dest => dest.LabSamples, opt => opt.MapFrom(src => src.LabSamples));

            CreateMap<Patient, PatientSearchResult>()
                .IncludeBase<Patient, PatientResponse>()
                .ForMember(dest => dest.MatchedFields, opt => opt.Ignore())
                .ForMember(dest => dest.RelevanceScore, opt => opt.Ignore());

            CreateMap<Patient, RelatedPatientInfo>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.Account != null ? src.Account.Username : null))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Account != null ? src.Account.Email : null))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Account != null ? src.Account.Phone : null));

            // Account to PatientAccountInfo mapping
            CreateMap<Account, PatientAccountInfo>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.BirthDate, opt => opt.MapFrom(src => ConvertToDateTime(src.BirthDate)))
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender))
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Username))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Phone))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
                .ForMember(dest => dest.AvatarId, opt => opt.MapFrom(src => src.AvatarId))
                .ForMember(dest => dest.LastLogin, opt => opt.MapFrom(src => src.LastLogin))
                .ForMember(dest => dest.RoleId, opt => opt.MapFrom(src => src.RoleId))
                .ForMember(dest => dest.IsVerified, opt => opt.MapFrom(src => src.IsVerified))
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.IsActive))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt));

            // Request to Entity mappings
            CreateMap<CreatePatientRequest, Patient>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => false))
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.Account, opt => opt.Ignore())
                .ForMember(dest => dest.Treatments, opt => opt.Ignore())
                .ForMember(dest => dest.LabSamples, opt => opt.Ignore())
                .ForMember(dest => dest.CryoStorageContracts, opt => opt.Ignore())
                .ForMember(dest => dest.RelationshipsAsPatient1, opt => opt.Ignore())
                .ForMember(dest => dest.RelationshipsAsPatient2, opt => opt.Ignore());

            CreateMap<UpdatePatientRequest, Patient>()
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
                .ForMember(dest => dest.Account, opt => opt.Ignore())
                .ForMember(dest => dest.Treatments, opt => opt.Ignore())
                .ForMember(dest => dest.LabSamples, opt => opt.Ignore())
                .ForMember(dest => dest.CryoStorageContracts, opt => opt.Ignore())
                .ForMember(dest => dest.RelationshipsAsPatient1, opt => opt.Ignore())
                .ForMember(dest => dest.RelationshipsAsPatient2, opt => opt.Ignore())
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            // Treatment summary mappings (assuming Treatment entity exists)
            CreateMap<Treatment, PatientTreatmentSummary>()
                .ForMember(dest => dest.TreatmentCode, opt => opt.Ignore()) // Map from actual Treatment properties
                .ForMember(dest => dest.StartDate, opt => opt.Ignore())
                .ForMember(dest => dest.EndDate, opt => opt.Ignore())
                .ForMember(dest => dest.Status, opt => opt.Ignore())
                .ForMember(dest => dest.Notes, opt => opt.Ignore());

            // Lab sample summary mappings (assuming LabSample entity exists)
            CreateMap<LabSample, PatientLabSampleSummary>()
                .ForMember(dest => dest.SampleCode, opt => opt.Ignore()) // Map from actual LabSample properties
                .ForMember(dest => dest.SampleType, opt => opt.Ignore())
                .ForMember(dest => dest.CollectionDate, opt => opt.Ignore())
                .ForMember(dest => dest.Status, opt => opt.Ignore())
                .ForMember(dest => dest.Notes, opt => opt.Ignore());
        }

        private void CreateRelationshipMappings()
        {
            // Relationship Entity to Response mappings
            CreateMap<Relationship, RelationshipResponse>()
                .ForMember(dest => dest.Patient1Info, opt => opt.MapFrom(src => src.Patient1))
                .ForMember(dest => dest.Patient2Info, opt => opt.MapFrom(src => src.Patient2));

            // Request to Entity mappings
            CreateMap<CreateRelationshipRequest, Relationship>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => false))
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.Patient1, opt => opt.Ignore())
                .ForMember(dest => dest.Patient2, opt => opt.Ignore());

            CreateMap<UpdateRelationshipRequest, Relationship>()
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
                .ForMember(dest => dest.Patient1Id, opt => opt.Ignore())
                .ForMember(dest => dest.Patient2Id, opt => opt.Ignore())
                .ForMember(dest => dest.Patient1, opt => opt.Ignore())
                .ForMember(dest => dest.Patient2, opt => opt.Ignore())
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }

        private static DateTime? ConvertToDateTime(DateOnly? birthDate)
            => birthDate?.ToDateTime(TimeOnly.MinValue);
    }
}

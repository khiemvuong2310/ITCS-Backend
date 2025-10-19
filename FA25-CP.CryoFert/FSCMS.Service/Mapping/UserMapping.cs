using AutoMapper;
using FSCMS.Core.Entities;
using FSCMS.Service.ReponseModel;
using FSCMS.Service.RequestModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSCMS.Service.Mapping
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            // Map Account entity to UserResponse
            CreateMap<Account, UserResponse>()
                .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.Role != null ? src.Role.RoleName : null))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedDate))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedDate))
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Avatar))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.IsActive))
                .ForMember(dest => dest.UserName, opt => opt.Ignore()) // Account doesn't have UserName
                .ForMember(dest => dest.Age, opt => opt.Ignore()) // Account doesn't have Age
                .ForMember(dest => dest.Location, opt => opt.Ignore()) // Account doesn't have Location
                .ForMember(dest => dest.Country, opt => opt.Ignore()); // Account doesn't have Country

            // Map Account entity to UserDetailResponse (includes base mapping from UserResponse)
            CreateMap<Account, UserDetailResponse>()
                .IncludeBase<Account, UserResponse>()
                .ForMember(dest => dest.TotalAppointments, opt => opt.Ignore()) // Not directly accessible from Account
                .ForMember(dest => dest.TotalPayments, opt => opt.Ignore()) // Not directly accessible from Account
                .ForMember(dest => dest.TotalFeedbacks, opt => opt.Ignore()) // Not directly accessible from Account
                .ForMember(dest => dest.DoctorSpecialization, opt => opt.MapFrom(src => src.Doctor != null ? src.Doctor.Specialty : null));

            // Map CreateUserRequest to Account entity
            CreateMap<CreateUserRequest, Account>()
                .ForMember(dest => dest.EmailVerified, opt => opt.MapFrom(src => false))
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.IsDelete, opt => opt.MapFrom(src => false))
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.Status ?? true))
                .ForMember(dest => dest.Avatar, opt => opt.MapFrom(src => src.Image))
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedDate, opt => opt.Ignore())
                .ForMember(dest => dest.Token, opt => opt.Ignore())
                .ForMember(dest => dest.Role, opt => opt.Ignore())
                .ForMember(dest => dest.Patient, opt => opt.Ignore())
                .ForMember(dest => dest.Doctor, opt => opt.Ignore())
                .ForMember(dest => dest.LastLogin, opt => opt.Ignore());

            // Map UpdateUserRequest to Account entity (only map non-null properties)
            CreateMap<UpdateUserRequest, Account>()
                .ForMember(dest => dest.UpdatedDate, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.Avatar, opt => opt.MapFrom(src => src.Image))
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Email, opt => opt.Ignore())
                .ForMember(dest => dest.Password, opt => opt.Ignore())
                .ForMember(dest => dest.Token, opt => opt.Ignore())
                .ForMember(dest => dest.EmailVerified, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
                .ForMember(dest => dest.IsDelete, opt => opt.Ignore())
                .ForMember(dest => dest.Role, opt => opt.Ignore())
                .ForMember(dest => dest.Patient, opt => opt.Ignore())
                .ForMember(dest => dest.Doctor, opt => opt.Ignore())
                .ForMember(dest => dest.LastLogin, opt => opt.Ignore())
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}

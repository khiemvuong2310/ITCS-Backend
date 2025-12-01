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
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Username))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Phone))
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender))
                .ForMember(dest => dest.DOB, opt => opt.MapFrom(src => ConvertToDateTime(src.BirthDate)))
                .ForMember(dest => dest.Age, opt => opt.MapFrom(src => CalculateAge(src.BirthDate)))
                .ForMember(dest => dest.Location, opt => opt.MapFrom(src => src.Address))
                .ForMember(dest => dest.AvatarId, opt => opt.MapFrom(src => src.AvatarId))
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.AvatarId))
                .ForMember(dest => dest.LastLogin, opt => opt.MapFrom(src => src.LastLogin))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.IsActive))
                .ForMember(dest => dest.EmailVerified, opt => opt.MapFrom(src => src.IsVerified))
                .ForMember(dest => dest.RoleId, opt => opt.MapFrom(src => src.RoleId))
                .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.Role != null ? src.Role.RoleName : null))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt))
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
                .ForMember(dest => dest.IsVerified, opt => opt.MapFrom(src => false))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => false))
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.Status ?? true))
                .ForMember(dest => dest.AvatarId, opt => opt.MapFrom(src => src.Image))
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.RefreshToken, opt => opt.Ignore())
                .ForMember(dest => dest.Role, opt => opt.Ignore())
                .ForMember(dest => dest.Patient, opt => opt.Ignore())
                .ForMember(dest => dest.Doctor, opt => opt.Ignore())
                .ForMember(dest => dest.LastLogin, opt => opt.Ignore());

            // Map UpdateUserRequest to Account entity (only map non-null properties)
            CreateMap<UpdateUserRequest, Account>()
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.AvatarId, opt => opt.MapFrom(src => src.Image))
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Email, opt => opt.Ignore())
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
                .ForMember(dest => dest.RefreshToken, opt => opt.Ignore())
                .ForMember(dest => dest.IsVerified, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
                .ForMember(dest => dest.Role, opt => opt.Ignore())
                .ForMember(dest => dest.Patient, opt => opt.Ignore())
                .ForMember(dest => dest.Doctor, opt => opt.Ignore())
                .ForMember(dest => dest.LastLogin, opt => opt.Ignore())
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }

        private static DateTime? ConvertToDateTime(DateOnly? birthDate)
            => birthDate?.ToDateTime(TimeOnly.MinValue);

        private static int? CalculateAge(DateOnly? birthDate)
        {
            if (!birthDate.HasValue)
            {
                return null;
            }

            var today = DateOnly.FromDateTime(DateTime.UtcNow);
            var age = today.Year - birthDate.Value.Year;
            if (today < birthDate.Value.AddYears(age))
            {
                age--;
            }

            return age;
        }
    }
}

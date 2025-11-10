using System;
using AutoMapper;
using FSCMS.Core.Entities;
using FSCMS.Core.Enum;
using FSCMS.Service.ReponseModel;
using FSCMS.Service.RequestModel;

namespace FSCMS.Service.Mapping
{
    public class TransactionMappingProfile : Profile
    {
        public TransactionMappingProfile()
        {
            // Map Transaction entity => TransactionResponseModel
            CreateMap<Transaction, TransactionResponseModel>()
                .ForMember(dest => dest.PaymentUrl, opt => opt.Ignore()) // PaymentUrl được set riêng trong service
                .ForMember(dest => dest.ProcessedBy, opt => opt.MapFrom(src => src.ProcessedBy))
                .ForMember(dest => dest.ProcessedDate, opt => opt.MapFrom(src => src.ProcessedDate))
                .ForMember(dest => dest.PatientId, opt => opt.MapFrom(src => src.PatientId))
                .ForMember(dest => dest.PatientName, opt => opt.MapFrom(src => src.PatientName))
                .ForMember(dest => dest.RelatedEntityType, opt => opt.MapFrom(src => src.RelatedEntityType))
                .ForMember(dest => dest.RelatedEntityId, opt => opt.MapFrom(src => src.RelatedEntityId))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.PaymentMethod, opt => opt.MapFrom(src => src.PaymentMethod))
                .ForMember(dest => dest.PaymentGateway, opt => opt.MapFrom(src => src.PaymentGateway))
                .ForMember(dest => dest.ReferenceNumber, opt => opt.MapFrom(src => src.ReferenceNumber))
                .ForMember(dest => dest.Amount, opt => opt.MapFrom(src => src.Amount))
                .ForMember(dest => dest.Currency, opt => opt.MapFrom(src => src.Currency))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.TransactionCode, opt => opt.MapFrom(src => src.TransactionCode))
                .ForMember(dest => dest.TransactionType, opt => opt.MapFrom(src => src.TransactionType))
                .ForMember(dest => dest.TransactionDate, opt => opt.MapFrom(src => src.TransactionDate))
                .ForMember(dest => dest.Notes, opt => opt.MapFrom(src => src.Notes));

            // Map CreateTransactionRequest => Transaction entity
            CreateMap<CreateTransactionRequest, Transaction>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.TransactionCode, opt => opt.Ignore())
                .ForMember(dest => dest.TransactionType, opt => opt.MapFrom(src => TransactionType.Payment)) // default Payment
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => TransactionStatus.Pending))
                .ForMember(dest => dest.Currency, opt => opt.MapFrom(src => "VND"))
                .ForMember(dest => dest.TransactionDate, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            // Map UpdateTransactionRequest => Transaction entity
            CreateMap<UpdateTransactionRequest, Transaction>()
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}

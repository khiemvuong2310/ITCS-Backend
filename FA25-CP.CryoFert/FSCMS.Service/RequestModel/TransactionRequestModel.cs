using System;
using System.ComponentModel.DataAnnotations;
using CloudinaryDotNet.Core;
using FSCMS.Core.Enum;
using FSCMS.Service.ReponseModel;

namespace FSCMS.Service.RequestModel
{
    public class CancelltransactionRequest
    {
        [Required(ErrorMessage = "RelatedEntityType is required.")]
        public EntityTypeTransaction RelatedEntityType { get; set; }
        [Required(ErrorMessage = "RelatedEntityId is required.")]
        public Guid RelatedEntityId { get; set; }
    }
        public class CreateUrlPaymentRequest
    {
        [Required(ErrorMessage = "RelatedEntityType is required.")]
        public EntityTypeTransaction RelatedEntityType { get; set; }
        [Required(ErrorMessage = "RelatedEntityId is required.")]
        public Guid RelatedEntityId { get; set; }
        [Required(ErrorMessage = "PatientId is required.")]
        public Guid PatientId { get; set; }
    }
        public class CreateTransactionRequest
    {
        [Required(ErrorMessage = "Amount is required.")]
        [Range(1, double.MaxValue, ErrorMessage = "Amount must be greater than 0.")]
        public decimal Amount { get; set; }

        // [Required(ErrorMessage = "Currency is required.")]
        // [StringLength(5)]
        // public string Currency { get; set; } = "VND";
        //[Required(ErrorMessage = "PatientId is required.")]
        //public Guid PatientId { get; set; }

        //[StringLength(1000)]
        //public string? Description { get; set; }
        [Required(ErrorMessage = "RelatedEntityType is required.")]
        public EntityTypeTransaction RelatedEntityType { get; set; }
        [Required(ErrorMessage = "RelatedEntityId is required.")]
        public Guid RelatedEntityId { get; set; }
    }

    public class UpdateTransactionRequest
    {
        public decimal? Amount { get; set; }

        [StringLength(1000)]
        public string? Description { get; set; }
        public TransactionStatus? Status { get; set; }
    }

    public class GetTransactionsRequest : PagingModel
    {
        public Guid PatientId { get; set; }
        public EntityTypeTransaction? RelatedEntityType { get; set; } = null;
        public Guid? RelatedEntityId { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public TransactionStatus? Status { get; set; }
    }

    public enum EntityTypeTransaction

    {
        ServiceRequest = 0,
        Appointment = 1,
        CryoStorageContract = 2,
        //Patient = 3
    }
}

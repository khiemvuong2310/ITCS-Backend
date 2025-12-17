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

    public class CashPaymentRequest
    {
        [Required(ErrorMessage = "RelatedEntityType is required.")]
        public EntityTypeTransaction RelatedEntityType { get; set; }
        [Required(ErrorMessage = "RelatedEntityId is required.")]
        public Guid RelatedEntityId { get; set; }
        //[Required(ErrorMessage = "PatientId is required.")]
        //public Guid PatientId { get; set; }
    }
    public class CreateUrlPaymentRequest
    {
        [Required(ErrorMessage = "PaymentGateway is required.")]
        public PaymentGateway PaymentGateway { get; set; }
        [Required(ErrorMessage = "RelatedEntityType is required.")]
        public EntityTypeTransaction RelatedEntityType { get; set; }
        [Required(ErrorMessage = "RelatedEntityId is required.")]
        public Guid RelatedEntityId { get; set; }
        //[Required(ErrorMessage = "PatientId is required.")]
        //public Guid PatientId { get; set; }
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
        public Guid TransactionId { get; set; }
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

    public enum PaymentGateway

    {
        VnPay = 0,
        PayOS = 1,
    }

    public class PayOSWebhookData
    {
        public string Code { get; set; }
        public string Desc { get; set; }
        public bool Success { get; set; }
        public PayOSPaymentData Data { get; set; }
        public string Signature { get; set; }
    }

    public class PayOSPaymentData
    {
        public long OrderCode { get; set; }
        public long Amount { get; set; }
        public string Description { get; set; }
        public string AccountNumber { get; set; }
        public string Reference { get; set; }
        public string TransactionDateTime { get; set; }
        public string Currency { get; set; }
        public string PaymentLinkId { get; set; }
        public string Code { get; set; }
        public string Desc { get; set; }
        public string CounterAccountBankId { get; set; }
        public string CounterAccountBankName { get; set; }
        public string CounterAccountName { get; set; }
        public string CounterAccountNumber { get; set; }
        public string VirtualAccountName { get; set; }
        public string VirtualAccountNumber { get; set; }
    }


}

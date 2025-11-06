using System;
using System.ComponentModel.DataAnnotations;
using FSCMS.Core.Enum;
using FSCMS.Service.ReponseModel;

namespace FSCMS.Service.RequestModel
{
    public class CreateTransactionRequest
    {
        [Required(ErrorMessage = "Amount is required.")]
        [Range(1, double.MaxValue, ErrorMessage = "Amount must be greater than 0.")]
        public decimal Amount { get; set; }

        [Required(ErrorMessage = "Currency is required.")]
        [StringLength(5)]
        public string Currency { get; set; } = "VND";

        public Guid PatientId { get; set; }

        [StringLength(1000)]
        public string? Description { get; set; }

        public string? RelatedEntityType { get; set; }
        public Guid RelatedEntityId { get; set; }
    }

    public class UpdateTransactionRequest
    {
        public decimal? Amount { get; set; }

        [StringLength(1000)]
        public string? Description { get; set; }

        public string? RelatedEntityType { get; set; }
        public int? RelatedEntityId { get; set; }

        public TransactionStatus? Status { get; set; }
    }

    public class GetTransactionsRequest : PagingModel
    {
        public Guid PatientId { get; set; }
        public string? RelatedEntityType { get; set; }
        public Guid RelatedEntityId { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public TransactionStatus? Status { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FSCMS.Core.Enums;
using FSCMS.Service.ReponseModel;

namespace FSCMS.Service.RequestModel
{
    public class CreateCryoStorageContractRequest
    {
        [Required(ErrorMessage = "PatientId is required.")]
        public Guid PatientId { get; set; }

        [Required(ErrorMessage = "CryoPackageId is required.")]
        public Guid CryoPackageId { get; set; }

        public bool IsAutoRenew { get; set; } = false;

        public ContractStatus Status { get; set; } = ContractStatus.Active;

        [StringLength(200, ErrorMessage = "Notes cannot exceed 200 characters.")]
        public string? Notes { get; set; }

        public List<CreateCPSDetailRequest>? Samples { get; set; }
    }

    public class UpdateCryoStorageContractRequest
    {
        public bool? IsAutoRenew { get; set; }
        public ContractStatus? Status { get; set; }
        [StringLength(200, ErrorMessage = "Notes cannot exceed 200 characters.")]
        public string? Notes { get; set; }
    }

    public class GetCryoStorageContractsRequest : PagingModel
    {
        public Guid? PatientId { get; set; }
        public Guid? CryoPackageId { get; set; }
        public ContractStatus? Status { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public string? SearchTerm { get; set; }
    }

    public class CreateCPSDetailRequest
    {
        [Required(ErrorMessage = "LabSampleId is required.")]
        public Guid LabSampleId { get; set; }
        [StringLength(200, ErrorMessage = "Notes cannot exceed 200 characters.")]
        public string? Notes { get; set; }
    }
}

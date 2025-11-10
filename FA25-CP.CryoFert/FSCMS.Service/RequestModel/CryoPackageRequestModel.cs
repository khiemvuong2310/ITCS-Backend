using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FSCMS.Core.Enum;
using FSCMS.Service.ReponseModel;

namespace FSCMS.Service.RequestModel
{
    public class CreateCryoPackageRequest
    {
        [Required(ErrorMessage = "PackageName is required.")]
        [StringLength(100, ErrorMessage = "PackageName cannot exceed 100 characters.")]
        public string PackageName { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Price must be greater than or equal 0.")]
        public decimal Price { get; set; }

        [Range(1, 120, ErrorMessage = "DurationMonths must be between 1 and 120.")]
        public int DurationMonths { get; set; }

        [Range(1, 100, ErrorMessage = "MaxSamples must be between 1 and 100.")]
        public int MaxSamples { get; set; }

        public SampleType SampleType { get; set; } = SampleType.Sperm;

        public bool IncludesInsurance { get; set; } = false;

        [Range(0, double.MaxValue, ErrorMessage = "InsuranceAmount must be greater than or equal 0.")]
        public decimal? InsuranceAmount { get; set; }

        public bool IsActive { get; set; } = true;

        [StringLength(500, ErrorMessage = "Benefits cannot exceed 500 characters.")]
        public string? Benefits { get; set; }

        [StringLength(500, ErrorMessage = "Notes cannot exceed 500 characters.")]
        public string? Notes { get; set; }

        [StringLength(1000, ErrorMessage = "Description cannot exceed 1000 characters.")]
        public string? Description { get; set; }
    }

    public class UpdateCryoPackageRequest
    {
        [StringLength(100, ErrorMessage = "PackageName cannot exceed 100 characters.")]
        public string? PackageName { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Price must be greater than or equal 0.")]
        public decimal? Price { get; set; }

        [Range(1, 120, ErrorMessage = "DurationMonths must be between 1 and 120.")]
        public int? DurationMonths { get; set; }

        [Range(1, 100, ErrorMessage = "MaxSamples must be between 1 and 100.")]
        public int? MaxSamples { get; set; }

        public SampleType? SampleType { get; set; }

        public bool? IncludesInsurance { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "InsuranceAmount must be greater than or equal 0.")]
        public decimal? InsuranceAmount { get; set; }

        public bool? IsActive { get; set; }

        [StringLength(500, ErrorMessage = "Benefits cannot exceed 500 characters.")]
        public string? Benefits { get; set; }

        [StringLength(500, ErrorMessage = "Notes cannot exceed 500 characters.")]
        public string? Notes { get; set; }

        [StringLength(1000, ErrorMessage = "Description cannot exceed 1000 characters.")]
        public string? Description { get; set; }
    }

    public class GetCryoPackageByIdRequest
    {
        [Required(ErrorMessage = "PackageId is required.")]
        public Guid PackageId { get; set; }
    }

    public class GetCryoPackagesRequest : PagingModel
    {
        public string? SearchTerm { get; set; }
        public SampleType? SampleType { get; set; }
        public bool? IsActive { get; set; }
    }
}

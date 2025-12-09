using System;
using System.ComponentModel.DataAnnotations;
using FSCMS.Core.Enum;
using FSCMS.Service.ReponseModel;
using Microsoft.AspNetCore.Http;

namespace FSCMS.Service.RequestModel
{
    /// <summary>
    /// Request model for creating a new agreement
    /// </summary>
    public class CreateAgreementRequest
    {
        [Required(ErrorMessage = "TreatmentId is required.")]
        public Guid TreatmentId { get; set; }

        [Required(ErrorMessage = "PatientId is required.")]
        public Guid PatientId { get; set; }

        [Required(ErrorMessage = "StartDate is required.")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "TotalAmount is required.")]
        [Range(0, double.MaxValue, ErrorMessage = "TotalAmount must be greater than or equal to 0.")]
        public decimal TotalAmount { get; set; }

        public DateTime? EndDate { get; set; }

        //[StringLength(500, ErrorMessage = "FileUrl cannot exceed 500 characters.")]
        //public string? FileUrl { get; set; }
    }

    /// <summary>
    /// Request model for updating an existing agreement
    /// </summary>
    public class UpdateAgreementRequest
    {
        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "TotalAmount must be greater than or equal to 0.")]
        public decimal? TotalAmount { get; set; }

        public AgreementStatus? Status { get; set; }

        public bool? SignedByPatient { get; set; }

        public bool? SignedByDoctor { get; set; }

        //[StringLength(500, ErrorMessage = "FileUrl cannot exceed 500 characters.")]
        //public string? FileUrl { get; set; }
    }

    /// <summary>
    /// Request model for getting list of agreements with pagination and filtering
    /// </summary>
    public class GetAgreementsRequest : PagingModel
    {
        public Guid? TreatmentId { get; set; }

        public Guid? PatientId { get; set; }

        public AgreementStatus? Status { get; set; }

        public DateTime? FromStartDate { get; set; }

        public DateTime? ToStartDate { get; set; }

        public DateTime? FromEndDate { get; set; }

        public DateTime? ToEndDate { get; set; }

        public bool? SignedByPatient { get; set; }

        public bool? SignedByDoctor { get; set; }

        public string? SearchTerm { get; set; }
    }

    /// <summary>
    /// Request model for signing an agreement
    /// </summary>
    public class SignAgreementRequest
    {
        [Required(ErrorMessage = "SignedByPatient or SignedByDoctor must be true.")]
        public bool? SignedByPatient { get; set; }

        public bool? SignedByDoctor { get; set; }
    }

    /// <summary>
    /// Request model for verifying signature with OTP
    /// </summary>
    public class VerifySignatureRequest
    {
        [Required(ErrorMessage = "OTP code is required.")]
        [StringLength(6, MinimumLength = 6, ErrorMessage = "OTP code must be 6 digits.")]
        public string OtpCode { get; set; } = string.Empty;
    }
}


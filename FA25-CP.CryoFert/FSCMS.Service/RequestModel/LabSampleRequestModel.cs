using System;
using System.ComponentModel.DataAnnotations;
using FSCMS.Core.Enum;
using FSCMS.Service.ReponseModel;

namespace FSCMS.Service.RequestModel
{
    #region ===== BASE REQUEST CLASSES =====

    /// <summary>
    /// Base request for creating lab samples (common fields)
    /// </summary>
    public abstract class CreateLabSampleBaseRequest
    {
        [Required(ErrorMessage = "Patient ID is required.")]
        public Guid PatientId { get; set; }

        [StringLength(500, ErrorMessage = "Notes cannot exceed 500 characters.")]
        public string? Notes { get; set; }
        [StringLength(100)]
        public string? Quality { get; set; }
        public bool IsAvailable { get; set; } = true;
        public bool IsQualityCheck { get; set; } = false;
    }

    public class UpdateLabSampleFrozenRequest
    {
        [Required(ErrorMessage = "CanFrozen is required.")]
        public bool CanFrozen { get; set; }
    }

    public class UpdateLabSampleFertilizeRequest
    {
        [Required(ErrorMessage = "CanFertilize is required.")]
        public bool CanFertilize { get; set; }
    }

    /// <summary>
    /// Base request for updating lab samples (common fields)
    /// </summary>
    public abstract class UpdateLabSampleBaseRequest
    {
        public SpecimenStatus? Status { get; set; }

        [StringLength(500, ErrorMessage = "Notes cannot exceed 500 characters.")]
        public string? Notes { get; set; }
        [StringLength(100)]
        public string? Quality { get; set; }
        public bool? IsAvailable { get; set; }
    }

    #endregion

    #region ===== SPERM SAMPLE REQUEST =====

    /// <summary>
    /// Create sperm sample request
    /// </summary>
    public class CreateLabSampleSpermRequest : CreateLabSampleBaseRequest
    {
        [Range(0, 10, ErrorMessage = "Volume must be between 0 and 10 mL.")]
        public decimal? Volume { get; set; }

        [Range(0, 500, ErrorMessage = "Concentration must be between 0 and 500 million/mL.")]
        public decimal? Concentration { get; set; }

        [Range(0, 100, ErrorMessage = "Motility must be between 0 and 100%.")]
        public decimal? Motility { get; set; }

        [Range(0, 100, ErrorMessage = "Progressive motility must be between 0 and 100%.")]
        public decimal? ProgressiveMotility { get; set; }

        [Range(0, 100, ErrorMessage = "Morphology must be between 0 and 100%.")]
        public decimal? Morphology { get; set; }

        [Range(6, 9, ErrorMessage = "pH must be between 6.0 and 9.0.")]
        public decimal? PH { get; set; }

        [StringLength(50)]
        public string? Viscosity { get; set; }

        [StringLength(50)]
        public string? Liquefaction { get; set; }

        [StringLength(30)]
        public string? Color { get; set; }

        public int? TotalSpermCount { get; set; }
    }

    /// <summary>
    /// Update sperm sample request
    /// </summary>
    public class UpdateLabSampleSpermRequest : UpdateLabSampleBaseRequest
    {
        [Range(0, 10)]
        public decimal? Volume { get; set; }
        [Range(0, 500)]
        public decimal? Concentration { get; set; }
        [Range(0, 100)]
        public decimal? Motility { get; set; }
        [Range(0, 100)]
        public decimal? ProgressiveMotility { get; set; }
        [Range(0, 100)]
        public decimal? Morphology { get; set; }
        [Range(6, 9)]
        public decimal? PH { get; set; }

        [StringLength(50)]
        public string? Viscosity { get; set; }
        [StringLength(50)]
        public string? Liquefaction { get; set; }
        [StringLength(30)]
        public string? Color { get; set; }
        public int? TotalSpermCount { get; set; }
    }

    #endregion

    #region ===== OOCYTE SAMPLE REQUEST =====

    /// <summary>
    /// Create oocyte sample request
    /// </summary>
    public class CreateLabSampleOocyteRequest : CreateLabSampleBaseRequest
    {
        [StringLength(100)]
        public string? MaturityStage { get; set; }

        public bool IsMature { get; set; }

        [StringLength(100)]
        public string? CumulusCells { get; set; }

        [StringLength(100)]
        public string? CytoplasmAppearance { get; set; }

        public bool IsVitrified { get; set; }
    }

    /// <summary>
    /// Update oocyte sample request
    /// </summary>
    public class UpdateLabSampleOocyteRequest : UpdateLabSampleBaseRequest
    {
        [StringLength(100)]
        public string? MaturityStage { get; set; }

        public bool? IsMature { get; set; }

        public DateTime? RetrievalDate { get; set; }

        [StringLength(100)]
        public string? CumulusCells { get; set; }

        [StringLength(100)]
        public string? CytoplasmAppearance { get; set; }

        public bool? IsVitrified { get; set; }

        public DateTime? VitrificationDate { get; set; }
    }

    #endregion

    #region ===== EMBRYO SAMPLE REQUEST =====

    /// <summary>
    /// Create embryo sample request
    /// </summary>
    public class CreateLabSampleEmbryoRequest : CreateLabSampleBaseRequest
    {
        [Required(ErrorMessage = "Lab Sample Oocyte ID is required.")]
        public Guid LabSampleOocyteId { get; set; }
        [Required(ErrorMessage = "Lab Sample Sperm ID is required.")]
        public Guid LabSampleSpermId { get; set; }
        [Range(1, 10, ErrorMessage = "Day of development must be between 1 and 10.")]
        public int DayOfDevelopment { get; set; }

        [StringLength(20)]
        public string? Grade { get; set; }

        [Range(1, 200)]
        public int? CellCount { get; set; }

        [StringLength(100)]
        public string? Morphology { get; set; }

        public bool IsBiopsied { get; set; }

        public bool IsPGTTested { get; set; }

        [StringLength(255)]
        public string? PGTResult { get; set; }

        [StringLength(100)]
        public string? FertilizationMethod { get; set; }
    }

    /// <summary>
    /// Update embryo sample request
    /// </summary>
    public class UpdateLabSampleEmbryoRequest : UpdateLabSampleBaseRequest
    {
        [Range(1, 10)]
        public int? DayOfDevelopment { get; set; }

        [StringLength(20)]
        public string? Grade { get; set; }

        [Range(1, 200)]
        public int? CellCount { get; set; }

        [StringLength(100)]
        public string? Morphology { get; set; }

        public bool? IsBiopsied { get; set; }

        public bool? IsPGTTested { get; set; }

        [StringLength(255)]
        public string? PGTResult { get; set; }

        [StringLength(100)]
        public string? FertilizationMethod { get; set; }
    }

    #endregion

    #region ===== GET FILTER REQUEST =====

    public class GetLabSamplesRequest : PagingModel
    {
        public SampleType? SampleType { get; set; }
        public SpecimenStatus? Status { get; set; }
        public bool? CanFrozen { get; set; }
        public string? SearchTerm { get; set; }
        public Guid? PatientId { get; set; }
    }

    public class GetLabSamplesRequestDetail : PagingModel
    {
        [Required(ErrorMessage = "SampleType is required.")]
        public SampleType SampleType { get; set; }
        public SpecimenStatus? Status { get; set; }
        public bool? CanFrozen { get; set; }
        public string? SearchTerm { get; set; }
        public Guid? PatientId { get; set; }
    }

    public class GetEligibleLabSamplesRequest : PagingModel
    {
        [Required(ErrorMessage = "PatientId is required.")]
        public Guid PatientId { get; set; }
        public SampleType? SampleType { get; set; }
        public string? SearchTerm { get; set; }
    }

    #endregion
}

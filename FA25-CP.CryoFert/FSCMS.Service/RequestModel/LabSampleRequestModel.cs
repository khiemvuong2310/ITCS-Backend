using System;
using System.ComponentModel.DataAnnotations;
using FSCMS.Core.Enum;
using FSCMS.Service.ReponseModel;

namespace FSCMS.Service.RequestModel
{
    /// <summary>
    /// Request to create a new lab sample (generic base)
    /// </summary>
    public class CreateLabSampleRequest
    {
        [Required(ErrorMessage = "Patient ID is required.")]
        public Guid PatientId { get; set; }

        [Required(ErrorMessage = "Sample type is required.")]
        public SampleType SampleType { get; set; }

        [StringLength(500, ErrorMessage = "Notes cannot exceed 500 characters.")]
        public string? Notes { get; set; }
        public bool IsAvailable { get; set; } = true;
        // Optional — for type-specific details
        public CreateLabSampleSpermRequest? Sperm { get; set; }

        public CreateLabSampleOocyteRequest? Oocyte { get; set; }

        public CreateLabSampleEmbryoRequest? Embryo { get; set; }
    }

    /// <summary>
    /// Request to update existing lab sample
    /// </summary>
    public class UpdateLabSampleRequest
    {
        public SpecimenStatus? Status { get; set; }
        [StringLength(500, ErrorMessage = "Notes cannot exceed 500 characters.")]
        public string? Notes { get; set; }
        public bool? IsAvailable { get; set; }
        // Optional — type-specific update
        public UpdateLabSampleSpermRequest? Sperm { get; set; }

        public UpdateLabSampleOocyteRequest? Oocyte { get; set; }

        public UpdateLabSampleEmbryoRequest? Embryo { get; set; }
    }

    #region Sperm Request DTOs
    public class CreateLabSampleSpermRequest
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

        [StringLength(500)]
        public string? Notes { get; set; }
    }

    public class UpdateLabSampleSpermRequest : CreateLabSampleSpermRequest { }
    #endregion

    #region Oocyte Request DTOs
    public class CreateLabSampleOocyteRequest
    {
        [Required(ErrorMessage = "Maturity stage is required.")]
        [StringLength(100)]
        public string MaturityStage { get; set; } = default!;

        [StringLength(100)]
        public string? Quality { get; set; }

        public bool IsMature { get; set; }

        public DateTime? RetrievalDate { get; set; }

        [StringLength(100)]
        public string? CumulusCells { get; set; }

        [StringLength(100)]
        public string? CytoplasmAppearance { get; set; }

        public bool IsVitrified { get; set; }

        public DateTime? VitrificationDate { get; set; }

        [StringLength(500)]
        public string? Notes { get; set; }
    }

    public class UpdateLabSampleOocyteRequest : CreateLabSampleOocyteRequest { }
    #endregion

    #region Embryo Request DTOs
    public class CreateLabSampleEmbryoRequest
    {
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

        [StringLength(500)]
        public string? Notes { get; set; }
    }

    public class GetLabSamplesRequest : PagingModel
    {
        public SampleType? SampleType { get; set; }
        public SpecimenStatus? Status { get; set; }
        public string? SearchTerm { get; set; }
        public Guid? PatientId { get; set; }
    }

    public class UpdateLabSampleEmbryoRequest : CreateLabSampleEmbryoRequest { }
    #endregion
}

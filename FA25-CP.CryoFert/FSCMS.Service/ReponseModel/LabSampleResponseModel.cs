using System;
using System.Text.Json.Serialization;
using FSCMS.Core.Entities;
using FSCMS.Core.Enum;

namespace FSCMS.Service.ReponseModel
{
    /// <summary>
    /// Basic lab sample response
    /// </summary>
    public class LabSampleResponse
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("patientId")]
        public Guid PatientId { get; set; }

        [JsonPropertyName("sampleCode")]
        public string SampleCode { get; set; } = string.Empty;

        [JsonPropertyName("sampleType")]
        public SampleType SampleType { get; set; }

        [JsonPropertyName("status")]
        public SpecimenStatus Status { get; set; }

        [JsonPropertyName("collectionDate")]
        public DateTime CollectionDate { get; set; }

        [JsonPropertyName("isAvailable")]
        public bool IsAvailable { get; set; }

        [JsonPropertyName("isStoraged")]
        public bool IsStoraged { get; set; } = false;

        [JsonPropertyName("storageDate")]
        public DateTime? StorageDate { get; set; }

        [JsonPropertyName("expiryDate")]
        public DateTime? ExpiryDate { get; set; }

        [JsonPropertyName("quality")]
        public string? Quality { get; set; }

        [JsonPropertyName("notes")]
        public string? Notes { get; set; }
    }

    /// <summary>
    /// Detailed lab sample response with type-specific info
    /// </summary>
    public class LabSampleDetailResponse : LabSampleResponse
    {
        [JsonPropertyName("sperm")]
        public LabSampleSpermDto? Sperm { get; set; }

        [JsonPropertyName("oocyte")]
        public LabSampleOocyteDto? Oocyte { get; set; }

        [JsonPropertyName("embryo")]
        public LabSampleEmbryoDto? Embryo { get; set; }

        [JsonPropertyName("patient")]
        public PatientBasicInfo? Patient { get; set; }
    }

    /// <summary>
    /// Patient info for reference
    /// </summary>
    public class PatientBasicInfo
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("fullName")]
        public string FullName { get; set; } = string.Empty;

        [JsonPropertyName("dob")]
        public DateTime? DOB { get; set; }

        [JsonPropertyName("gender")]
        public string? Gender { get; set; }
    }

    /// <summary>
    /// Sperm-specific details
    /// </summary>
    public class LabSampleSpermDto
    {
        [JsonPropertyName("volume")]
        public decimal? Volume { get; set; }

        [JsonPropertyName("concentration")]
        public decimal? Concentration { get; set; }

        [JsonPropertyName("motility")]
        public decimal? Motility { get; set; }

        [JsonPropertyName("progressiveMotility")]
        public decimal? ProgressiveMotility { get; set; }

        [JsonPropertyName("morphology")]
        public decimal? Morphology { get; set; }

        [JsonPropertyName("ph")]
        public decimal? PH { get; set; }

        [JsonPropertyName("viscosity")]
        public string? Viscosity { get; set; }

        [JsonPropertyName("liquefaction")]
        public string? Liquefaction { get; set; }

        [JsonPropertyName("color")]
        public string? Color { get; set; }

        [JsonPropertyName("totalSpermCount")]
        public int? TotalSpermCount { get; set; }

        [JsonPropertyName("notes")]
        public string? Notes { get; set; }
    }

    /// <summary>
    /// Oocyte-specific details
    /// </summary>
    public class LabSampleOocyteDto
    {
        [JsonPropertyName("maturityStage")]
        public string MaturityStage { get; set; } = default!;

        [JsonPropertyName("quality")]
        public string? Quality { get; set; }

        [JsonPropertyName("isMature")]
        public bool IsMature { get; set; }

        [JsonPropertyName("retrievalDate")]
        public DateTime? RetrievalDate { get; set; }

        [JsonPropertyName("cumulusCells")]
        public string? CumulusCells { get; set; }

        [JsonPropertyName("cytoplasmAppearance")]
        public string? CytoplasmAppearance { get; set; }

        [JsonPropertyName("isVitrified")]
        public bool IsVitrified { get; set; } = false;

        [JsonPropertyName("vitrificationDate")]
        public DateTime? VitrificationDate { get; set; }

        [JsonPropertyName("notes")]
        public string? Notes { get; set; }
    }

    /// <summary>
    /// Embryo-specific details
    /// </summary>
    public class LabSampleEmbryoDto
    {
        [JsonPropertyName("dayOfDevelopment")]
        public int DayOfDevelopment { get; set; }

        [JsonPropertyName("grade")]
        public string? Grade { get; set; }

        [JsonPropertyName("cellCount")]
        public int? CellCount { get; set; }

        [JsonPropertyName("morphology")]
        public string? Morphology { get; set; }

        [JsonPropertyName("isBiopsied")]
        public bool IsBiopsied { get; set; } = false;

        [JsonPropertyName("isPgtTested")]
        public bool IsPGTTested { get; set; } = false;

        [JsonPropertyName("pgtResult")]
        public string? PGTResult { get; set; }

        [JsonPropertyName("fertilizationMethod")]
        public string? FertilizationMethod { get; set; }

        [JsonPropertyName("notes")]
        public string? Notes { get; set; }

        [JsonPropertyName("spermId")]
        public Guid LabSampleSpermId { get; set; }

        [JsonPropertyName("oocyteId")]
        public Guid LabSampleOocyteId { get; set; }
    }
}

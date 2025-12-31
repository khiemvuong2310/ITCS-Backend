using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using FSCMS.Core.Enum;

namespace FSCMS.Service.ReponseModel
{
    /// <summary>
    /// Response model for patient information
    /// </summary>
    public class PatientResponse
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("patientCode")]
        public string PatientCode { get; set; } = default!;

        [JsonPropertyName("nationalId")]
        public string NationalID { get; set; } = string.Empty;

        [JsonPropertyName("emergencyContact")]
        public string? EmergencyContact { get; set; }

        [JsonPropertyName("emergencyPhone")]
        public string? EmergencyPhone { get; set; }

        [JsonPropertyName("insurance")]
        public string? Insurance { get; set; }

        [JsonPropertyName("occupation")]
        public string? Occupation { get; set; }

        [JsonPropertyName("medicalHistory")]
        public string? MedicalHistory { get; set; }

        [JsonPropertyName("allergies")]
        public string? Allergies { get; set; }

        [JsonPropertyName("bloodType")]
        public string? BloodType { get; set; }

        [JsonPropertyName("height")]
        public decimal? Height { get; set; }

        [JsonPropertyName("weight")]
        public decimal? Weight { get; set; }

        [JsonPropertyName("bmi")]
        public decimal? BMI => Height.HasValue && Weight.HasValue && Height > 0 
            ? Math.Round(Weight.Value / (Height.Value / 100 * Height.Value / 100), 2) 
            : null;

        [JsonPropertyName("isActive")]
        public bool IsActive { get; set; }

        [JsonPropertyName("notes")]
        public string? Notes { get; set; }

        [JsonPropertyName("accountId")]
        public Guid AccountId { get; set; }

        [JsonPropertyName("createdAt")]
        public DateTime CreatedAt { get; set; }

        [JsonPropertyName("updatedAt")]
        public DateTime? UpdatedAt { get; set; }

        // Account information
        [JsonPropertyName("accountInfo")]
        public PatientAccountInfo? AccountInfo { get; set; }

        // Statistics
        [JsonPropertyName("treatmentCount")]
        public int TreatmentCount { get; set; }

        [JsonPropertyName("labSampleCount")]
        public int LabSampleCount { get; set; }

        [JsonPropertyName("relationshipCount")]
        public int RelationshipCount { get; set; }
    }

    /// <summary>
    /// Detailed patient response with related information
    /// </summary>
    public class PatientDetailResponse : PatientResponse
    {
        [JsonPropertyName("relationships")]
        public List<RelationshipResponse> Relationships { get; set; } = new();

        [JsonPropertyName("treatments")]
        public List<PatientTreatmentSummary> Treatments { get; set; } = new();

        [JsonPropertyName("labSamples")]
        public List<PatientLabSampleSummary> LabSamples { get; set; } = new();
    }

    /// <summary>
    /// Account information for patient
    /// </summary>
    public class PatientAccountInfo
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("firstName")]
        public string FirstName { get; set; } = default!;

        [JsonPropertyName("lastName")]
        public string LastName { get; set; } = default!;

        [JsonPropertyName("birthDate")]
        public DateTime? BirthDate { get; set; }

        [JsonPropertyName("age")]
        public int? Age => BirthDate.HasValue 
            ? DateTime.UtcNow.Year - BirthDate.Value.Year - (DateTime.UtcNow.DayOfYear < BirthDate.Value.DayOfYear ? 1 : 0)
            : null;

        [JsonPropertyName("gender")]
        public bool? Gender { get; set; }

        [JsonPropertyName("username")]
        public string Username { get; set; } = default!;

        [JsonPropertyName("email")]
        public string Email { get; set; } = default!;

        [JsonPropertyName("phone")]
        public string Phone { get; set; } = default!;

        [JsonPropertyName("address")]
        public string? Address { get; set; }

        [JsonPropertyName("avatarId")]
        public Guid? AvatarId { get; set; }

        [JsonPropertyName("lastLogin")]
        public DateTime? LastLogin { get; set; }

        [JsonPropertyName("roleId")]
        public Guid RoleId { get; set; }

        [JsonPropertyName("isVerified")]
        public bool IsVerified { get; set; }

        [JsonPropertyName("isActive")]
        public bool IsActive { get; set; }

        [JsonPropertyName("createdAt")]
        public DateTime CreatedAt { get; set; }

        [JsonPropertyName("updatedAt")]
        public DateTime? UpdatedAt { get; set; }
    }

    /// <summary>
    /// Response model for relationship information
    /// </summary>
    public class RelationshipResponse
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("patient1Id")]
        public Guid Patient1Id { get; set; }

        [JsonPropertyName("patient2Id")]
        public Guid Patient2Id { get; set; }

        [JsonPropertyName("relationshipType")]
        public RelationshipType RelationshipType { get; set; }

        [JsonPropertyName("relationshipTypeName")]
        public string RelationshipTypeName => RelationshipType.ToString();

        [JsonPropertyName("status")]
        public RelationshipStatus Status { get; set; }

        [JsonPropertyName("statusName")]
        public string StatusName => Status.ToString();

        [JsonPropertyName("establishedDate")]
        public DateTime? EstablishedDate { get; set; }

        [JsonPropertyName("notes")]
        public string? Notes { get; set; }

        [JsonPropertyName("isActive")]
        public bool IsActive { get; set; }

        [JsonPropertyName("requestedBy")]
        public Guid? RequestedBy { get; set; }

        [JsonPropertyName("respondedBy")]
        public Guid? RespondedBy { get; set; }

        [JsonPropertyName("respondedAt")]
        public DateTime? RespondedAt { get; set; }

        [JsonPropertyName("expiresAt")]
        public DateTime? ExpiresAt { get; set; }

        [JsonPropertyName("rejectionReason")]
        public string? RejectionReason { get; set; }

        [JsonPropertyName("createdAt")]
        public DateTime CreatedAt { get; set; }

        [JsonPropertyName("updatedAt")]
        public DateTime? UpdatedAt { get; set; }

        // Related patient information
        [JsonPropertyName("patient1Info")]
        public RelatedPatientInfo? Patient1Info { get; set; }

        [JsonPropertyName("patient2Info")]
        public RelatedPatientInfo? Patient2Info { get; set; }
    }

    /// <summary>
    /// Related patient information in relationships
    /// </summary>
    public class RelatedPatientInfo
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("patientCode")]
        public string PatientCode { get; set; } = default!;

        [JsonPropertyName("nationalId")]
        public string NationalID { get; set; } = string.Empty;

        [JsonPropertyName("fullName")]
        public string? FullName { get; set; }

        [JsonPropertyName("email")]
        public string? Email { get; set; }

        [JsonPropertyName("phone")]
        public string? Phone { get; set; }

        [JsonPropertyName("isActive")]
        public bool IsActive { get; set; }
    }

    /// <summary>
    /// Treatment summary for patient
    /// </summary>
    public class PatientTreatmentSummary
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("treatmentCode")]
        public string? TreatmentCode { get; set; }

        [JsonPropertyName("startDate")]
        public DateTime? StartDate { get; set; }

        [JsonPropertyName("endDate")]
        public DateTime? EndDate { get; set; }

        [JsonPropertyName("status")]
        public string? Status { get; set; }

        [JsonPropertyName("notes")]
        public string? Notes { get; set; }
    }

    /// <summary>
    /// Lab sample summary for patient
    /// </summary>
    public class PatientLabSampleSummary
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("sampleCode")]
        public string? SampleCode { get; set; }

        [JsonPropertyName("sampleType")]
        public string? SampleType { get; set; }

        [JsonPropertyName("collectionDate")]
        public DateTime? CollectionDate { get; set; }

        [JsonPropertyName("status")]
        public string? Status { get; set; }

        [JsonPropertyName("notes")]
        public string? Notes { get; set; }
    }

    /// <summary>
    /// Patient statistics response
    /// </summary>
    public class PatientStatisticsResponse
    {
        [JsonPropertyName("totalPatients")]
        public int TotalPatients { get; set; }

        [JsonPropertyName("activePatients")]
        public int ActivePatients { get; set; }

        [JsonPropertyName("inactivePatients")]
        public int InactivePatients { get; set; }

        [JsonPropertyName("patientsWithInsurance")]
        public int PatientsWithInsurance { get; set; }

        [JsonPropertyName("patientsWithoutInsurance")]
        public int PatientsWithoutInsurance { get; set; }

        [JsonPropertyName("totalRelationships")]
        public int TotalRelationships { get; set; }

        [JsonPropertyName("activeRelationships")]
        public int ActiveRelationships { get; set; }

        [JsonPropertyName("relationshipsByType")]
        public Dictionary<string, int> RelationshipsByType { get; set; } = new();

        [JsonPropertyName("bloodTypeDistribution")]
        public Dictionary<string, int> BloodTypeDistribution { get; set; } = new();

        [JsonPropertyName("averageAge")]
        public double? AverageAge { get; set; }

        [JsonPropertyName("averageBMI")]
        public double? AverageBMI { get; set; }

        [JsonPropertyName("patientsCreatedThisMonth")]
        public int PatientsCreatedThisMonth { get; set; }

        [JsonPropertyName("patientsCreatedThisYear")]
        public int PatientsCreatedThisYear { get; set; }
    }

    /// <summary>
    /// Patient search result with highlighting
    /// </summary>
    public class PatientSearchResult : PatientResponse
    {
        [JsonPropertyName("matchedFields")]
        public List<string> MatchedFields { get; set; } = new();

        [JsonPropertyName("relevanceScore")]
        public double RelevanceScore { get; set; }
    }
}

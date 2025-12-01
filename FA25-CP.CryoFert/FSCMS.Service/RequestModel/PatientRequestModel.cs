using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using FSCMS.Core.Enum;
using FSCMS.Service.ReponseModel;

namespace FSCMS.Service.RequestModel
{
    /// <summary>
    /// Request model for creating a new patient
    /// </summary>
    public class CreatePatientRequest
    {
        [JsonPropertyName("patientCode")]
        [Required(ErrorMessage = "Patient code is required.")]
        [StringLength(50, ErrorMessage = "Patient code cannot exceed 50 characters.")]
        public string PatientCode { get; set; } = default!;

        [JsonPropertyName("nationalId")]
        [Required(ErrorMessage = "National ID is required.")]
        [StringLength(20, ErrorMessage = "National ID cannot exceed 20 characters.")]
        public string NationalID { get; set; } = default!;

        [JsonPropertyName("emergencyContact")]
        [StringLength(100, ErrorMessage = "Emergency contact cannot exceed 100 characters.")]
        public string? EmergencyContact { get; set; }

        [JsonPropertyName("emergencyPhone")]
        [StringLength(20, ErrorMessage = "Emergency phone cannot exceed 20 characters.")]
        [Phone(ErrorMessage = "Invalid phone number format.")]
        public string? EmergencyPhone { get; set; }

        [JsonPropertyName("insurance")]
        [StringLength(100, ErrorMessage = "Insurance cannot exceed 100 characters.")]
        public string? Insurance { get; set; }

        [JsonPropertyName("occupation")]
        [StringLength(100, ErrorMessage = "Occupation cannot exceed 100 characters.")]
        public string? Occupation { get; set; }

        [JsonPropertyName("medicalHistory")]
        [StringLength(2000, ErrorMessage = "Medical history cannot exceed 2000 characters.")]
        public string? MedicalHistory { get; set; }

        [JsonPropertyName("allergies")]
        [StringLength(1000, ErrorMessage = "Allergies cannot exceed 1000 characters.")]
        public string? Allergies { get; set; }

        [JsonPropertyName("bloodType")]
        [StringLength(10, ErrorMessage = "Blood type cannot exceed 10 characters.")]
        public string? BloodType { get; set; }

        [JsonPropertyName("height")]
        [Range(0, 300, ErrorMessage = "Height must be between 0 and 300 cm.")]
        public decimal? Height { get; set; }

        [JsonPropertyName("weight")]
        [Range(0, 500, ErrorMessage = "Weight must be between 0 and 500 kg.")]
        public decimal? Weight { get; set; }

        [JsonPropertyName("notes")]
        [StringLength(1000, ErrorMessage = "Notes cannot exceed 1000 characters.")]
        public string? Notes { get; set; }

        [JsonPropertyName("accountId")]
        [Required(ErrorMessage = "Account ID is required.")]
        public Guid AccountId { get; set; }

        [JsonPropertyName("isActive")]
        public bool IsActive { get; set; } = true;
    }

    /// <summary>
    /// Request model for updating an existing patient
    /// </summary>
    public class UpdatePatientRequest
    {
        [JsonPropertyName("patientCode")]
        [StringLength(50, ErrorMessage = "Patient code cannot exceed 50 characters.")]
        public string? PatientCode { get; set; }

        [JsonPropertyName("nationalId")]
        [StringLength(20, ErrorMessage = "National ID cannot exceed 20 characters.")]
        public string? NationalID { get; set; }

        [JsonPropertyName("emergencyContact")]
        [StringLength(100, ErrorMessage = "Emergency contact cannot exceed 100 characters.")]
        public string? EmergencyContact { get; set; }

        [JsonPropertyName("emergencyPhone")]
        [StringLength(20, ErrorMessage = "Emergency phone cannot exceed 20 characters.")]
        [Phone(ErrorMessage = "Invalid phone number format.")]
        public string? EmergencyPhone { get; set; }

        [JsonPropertyName("insurance")]
        [StringLength(100, ErrorMessage = "Insurance cannot exceed 100 characters.")]
        public string? Insurance { get; set; }

        [JsonPropertyName("occupation")]
        [StringLength(100, ErrorMessage = "Occupation cannot exceed 100 characters.")]
        public string? Occupation { get; set; }

        [JsonPropertyName("medicalHistory")]
        [StringLength(2000, ErrorMessage = "Medical history cannot exceed 2000 characters.")]
        public string? MedicalHistory { get; set; }

        [JsonPropertyName("allergies")]
        [StringLength(1000, ErrorMessage = "Allergies cannot exceed 1000 characters.")]
        public string? Allergies { get; set; }

        [JsonPropertyName("bloodType")]
        [StringLength(10, ErrorMessage = "Blood type cannot exceed 10 characters.")]
        public string? BloodType { get; set; }

        [JsonPropertyName("height")]
        [Range(0, 300, ErrorMessage = "Height must be between 0 and 300 cm.")]
        public decimal? Height { get; set; }

        [JsonPropertyName("weight")]
        [Range(0, 500, ErrorMessage = "Weight must be between 0 and 500 kg.")]
        public decimal? Weight { get; set; }

        [JsonPropertyName("notes")]
        [StringLength(1000, ErrorMessage = "Notes cannot exceed 1000 characters.")]
        public string? Notes { get; set; }

        [JsonPropertyName("isActive")]
        public bool? IsActive { get; set; }
    }

    /// <summary>
    /// Request model for getting patients with pagination and filtering
    /// </summary>
    public class GetPatientsRequest : PagingModel
    {
        [JsonPropertyName("searchTerm")]
        public string? SearchTerm { get; set; }

        [JsonPropertyName("patientCode")]
        public string? PatientCode { get; set; }

        [JsonPropertyName("nationalId")]
        public string? NationalID { get; set; }

        [JsonPropertyName("bloodType")]
        public string? BloodType { get; set; }

        [JsonPropertyName("isActive")]
        public bool? IsActive { get; set; }

        [JsonPropertyName("hasInsurance")]
        public bool? HasInsurance { get; set; }

        [JsonPropertyName("minHeight")]
        public decimal? MinHeight { get; set; }

        [JsonPropertyName("maxHeight")]
        public decimal? MaxHeight { get; set; }

        [JsonPropertyName("minWeight")]
        public decimal? MinWeight { get; set; }

        [JsonPropertyName("maxWeight")]
        public decimal? MaxWeight { get; set; }

        [JsonPropertyName("createdFrom")]
        public DateTime? CreatedFrom { get; set; }

        [JsonPropertyName("createdTo")]
        public DateTime? CreatedTo { get; set; }
    }

    /// <summary>
    /// Request model for creating a new relationship between patients
    /// </summary>
    public class CreateRelationshipRequest
    {
        [JsonPropertyName("patient1Id")]
        [Required(ErrorMessage = "Patient 1 ID is required.")]
        public Guid Patient1Id { get; set; }

        [JsonPropertyName("patient2Id")]
        [Required(ErrorMessage = "Patient 2 ID is required.")]
        public Guid Patient2Id { get; set; }

        [JsonPropertyName("relationshipType")]
        [Required(ErrorMessage = "Relationship type is required.")]
        public RelationshipType RelationshipType { get; set; }

        [JsonPropertyName("establishedDate")]
        public DateTime? EstablishedDate { get; set; }

        [JsonPropertyName("notes")]
        [StringLength(1000, ErrorMessage = "Notes cannot exceed 1000 characters.")]
        public string? Notes { get; set; }

        [JsonPropertyName("isActive")]
        public bool IsActive { get; set; } = true;
    }

    /// <summary>
    /// Request model for updating an existing relationship
    /// </summary>
    public class UpdateRelationshipRequest
    {
        [JsonPropertyName("relationshipType")]
        public RelationshipType? RelationshipType { get; set; }

        [JsonPropertyName("establishedDate")]
        public DateTime? EstablishedDate { get; set; }

        [JsonPropertyName("notes")]
        [StringLength(1000, ErrorMessage = "Notes cannot exceed 1000 characters.")]
        public string? Notes { get; set; }

        [JsonPropertyName("isActive")]
        public bool? IsActive { get; set; }
    }

    /// <summary>
    /// Request model for getting relationships with pagination and filtering
    /// </summary>
    public class GetRelationshipsRequest : PagingModel
    {
        [JsonPropertyName("patientId")]
        public Guid? PatientId { get; set; }

        [JsonPropertyName("relationshipType")]
        public RelationshipType? RelationshipType { get; set; }

        [JsonPropertyName("isActive")]
        public bool? IsActive { get; set; }

        [JsonPropertyName("establishedFrom")]
        public DateTime? EstablishedFrom { get; set; }

        [JsonPropertyName("establishedTo")]
        public DateTime? EstablishedTo { get; set; }

        [JsonPropertyName("createdFrom")]
        public DateTime? CreatedFrom { get; set; }

        [JsonPropertyName("createdTo")]
        public DateTime? CreatedTo { get; set; }
    }

    /// <summary>
    /// Request model for approving a relationship request
    /// </summary>
    public class ApproveRelationshipRequest
    {
        [JsonPropertyName("relationshipId")]
        [Required(ErrorMessage = "Relationship ID is required.")]
        public Guid RelationshipId { get; set; }
    }

    /// <summary>
    /// Request model for rejecting a relationship request
    /// </summary>
    public class RejectRelationshipRequest
    {
        [JsonPropertyName("relationshipId")]
        [Required(ErrorMessage = "Relationship ID is required.")]
        public Guid RelationshipId { get; set; }

        [JsonPropertyName("rejectionReason")]
        [StringLength(500, ErrorMessage = "Rejection reason cannot exceed 500 characters.")]
        public string? RejectionReason { get; set; }
    }

    /// <summary>
    /// Request model for cancelling a relationship request
    /// </summary>
    public class CancelRelationshipRequest
    {
        [JsonPropertyName("relationshipId")]
        [Required(ErrorMessage = "Relationship ID is required.")]
        public Guid RelationshipId { get; set; }

        [JsonPropertyName("cancellationReason")]
        [StringLength(500, ErrorMessage = "Cancellation reason cannot exceed 500 characters.")]
        public string? CancellationReason { get; set; }
    }

    /// <summary>
    /// Request model for updating patient status
    /// </summary>
    public class UpdatePatientStatusRequest
    {
        [JsonPropertyName("isActive")]
        [Required(ErrorMessage = "Status is required.")]
        public bool IsActive { get; set; }

        [JsonPropertyName("reason")]
        [StringLength(500, ErrorMessage = "Reason cannot exceed 500 characters.")]
        public string? Reason { get; set; }
    }
}

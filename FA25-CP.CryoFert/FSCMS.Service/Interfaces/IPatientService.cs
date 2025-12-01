using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FSCMS.Service.ReponseModel;
using FSCMS.Service.RequestModel;

namespace FSCMS.Service.Interfaces
{
    /// <summary>
    /// Interface for patient management operations
    /// </summary>
    public interface IPatientService
    {
        #region Patient CRUD Operations

        /// <summary>
        /// Creates a new patient
        /// </summary>
        /// <param name="request">Patient creation request</param>
        /// <returns>Created patient response</returns>
        Task<BaseResponse<PatientResponse>> CreatePatientAsync(CreatePatientRequest request);

        /// <summary>
        /// Gets a patient by ID
        /// </summary>
        /// <param name="patientId">Patient ID</param>
        /// <returns>Patient response</returns>
        Task<BaseResponse<PatientResponse>> GetPatientByIdAsync(Guid patientId);

        /// <summary>
        /// Gets detailed patient information by ID including related data
        /// </summary>
        /// <param name="patientId">Patient ID</param>
        /// <returns>Detailed patient response</returns>
        Task<BaseResponse<PatientDetailResponse>> GetPatientDetailsByIdAsync(Guid patientId);

        /// <summary>
        /// Gets a patient by patient code
        /// </summary>
        /// <param name="patientCode">Patient code</param>
        /// <returns>Patient response</returns>
        Task<BaseResponse<PatientResponse>> GetPatientByCodeAsync(string patientCode);

        /// <summary>
        /// Gets a patient by national ID
        /// </summary>
        /// <param name="nationalId">National ID</param>
        /// <returns>Patient response</returns>
        Task<BaseResponse<PatientResponse>> GetPatientByNationalIdAsync(string nationalId);

        /// <summary>
        /// Gets a patient by account ID
        /// </summary>
        /// <param name="accountId">Account ID</param>
        /// <returns>Patient response</returns>
        Task<BaseResponse<PatientResponse>> GetPatientByAccountIdAsync(Guid accountId);

        /// <summary>
        /// Gets all patients with pagination and filtering
        /// </summary>
        /// <param name="request">Get patients request</param>
        /// <returns>Paginated patient responses</returns>
        Task<DynamicResponse<PatientResponse>> GetAllPatientsAsync(GetPatientsRequest request);

        /// <summary>
        /// Updates an existing patient
        /// </summary>
        /// <param name="patientId">Patient ID</param>
        /// <param name="request">Patient update request</param>
        /// <returns>Updated patient response</returns>
        Task<BaseResponse<PatientResponse>> UpdatePatientAsync(Guid patientId, UpdatePatientRequest request);

        /// <summary>
        /// Updates patient status (active/inactive)
        /// </summary>
        /// <param name="patientId">Patient ID</param>
        /// <param name="request">Status update request</param>
        /// <returns>Updated patient response</returns>
        Task<BaseResponse<PatientResponse>> UpdatePatientStatusAsync(Guid patientId, UpdatePatientStatusRequest request);

        /// <summary>
        /// Soft deletes a patient
        /// </summary>
        /// <param name="patientId">Patient ID</param>
        /// <returns>Operation result</returns>
        Task<BaseResponse> DeletePatientAsync(Guid patientId);

        #endregion

        #region Relationship CRUD Operations

        /// <summary>
        /// Creates a new relationship between patients
        /// </summary>
        /// <param name="request">Relationship creation request</param>
        /// <returns>Created relationship response</returns>
        Task<BaseResponse<RelationshipResponse>> CreateRelationshipAsync(CreateRelationshipRequest request);

        /// <summary>
        /// Gets a relationship by ID
        /// </summary>
        /// <param name="relationshipId">Relationship ID</param>
        /// <returns>Relationship response</returns>
        Task<BaseResponse<RelationshipResponse>> GetRelationshipByIdAsync(Guid relationshipId);

        /// <summary>
        /// Gets all relationships with pagination and filtering
        /// </summary>
        /// <param name="request">Get relationships request</param>
        /// <returns>Paginated relationship responses</returns>
        Task<DynamicResponse<RelationshipResponse>> GetAllRelationshipsAsync(GetRelationshipsRequest request);

        /// <summary>
        /// Gets relationships for a specific patient
        /// </summary>
        /// <param name="patientId">Patient ID</param>
        /// <param name="request">Pagination request</param>
        /// <returns>Paginated relationship responses</returns>
        Task<DynamicResponse<RelationshipResponse>> GetPatientRelationshipsAsync(Guid patientId, GetRelationshipsRequest request);

        /// <summary>
        /// Updates an existing relationship
        /// </summary>
        /// <param name="relationshipId">Relationship ID</param>
        /// <param name="request">Relationship update request</param>
        /// <returns>Updated relationship response</returns>
        Task<BaseResponse<RelationshipResponse>> UpdateRelationshipAsync(Guid relationshipId, UpdateRelationshipRequest request);

        /// <summary>
        /// Soft deletes a relationship
        /// </summary>
        /// <param name="relationshipId">Relationship ID</param>
        /// <returns>Operation result</returns>
        Task<BaseResponse> DeleteRelationshipAsync(Guid relationshipId);

        /// <summary>
        /// Approves a pending relationship request
        /// </summary>
        /// <param name="request">Approve relationship request</param>
        /// <returns>Updated relationship response</returns>
        Task<BaseResponse<RelationshipResponse>> ApproveRelationshipAsync(ApproveRelationshipRequest request);

        /// <summary>
        /// Rejects a pending relationship request
        /// </summary>
        /// <param name="request">Reject relationship request</param>
        /// <returns>Updated relationship response</returns>
        Task<BaseResponse<RelationshipResponse>> RejectRelationshipAsync(RejectRelationshipRequest request);

        /// <summary>
        /// Cancels a pending relationship request initiated by the current patient
        /// </summary>
        /// <param name="request">Cancel relationship request</param>
        /// <returns>Updated relationship response</returns>
        Task<BaseResponse<RelationshipResponse>> CancelRelationshipAsync(CancelRelationshipRequest request);

        /// <summary>
        /// Approves a relationship request via email link with token verification
        /// </summary>
        /// <param name="relationshipId">Relationship ID</param>
        /// <param name="token">Approval token from email</param>
        /// <returns>Updated relationship response</returns>
        Task<BaseResponse<RelationshipResponse>> ApproveRelationshipByTokenAsync(Guid relationshipId, string token);

        /// <summary>
        /// Rejects a relationship request via email link with token verification
        /// </summary>
        /// <param name="relationshipId">Relationship ID</param>
        /// <param name="token">Approval token from email</param>
        /// <param name="rejectionReason">Optional rejection reason</param>
        /// <returns>Updated relationship response</returns>
        Task<BaseResponse<RelationshipResponse>> RejectRelationshipByTokenAsync(Guid relationshipId, string token, string? rejectionReason = null);

        #endregion

        #region Search and Utility Operations

        /// <summary>
        /// Searches patients by various criteria
        /// </summary>
        /// <param name="searchTerm">Search term</param>
        /// <param name="request">Pagination request</param>
        /// <returns>Paginated patient search results</returns>
        Task<DynamicResponse<PatientSearchResult>> SearchPatientsAsync(string searchTerm, GetPatientsRequest request);

        /// <summary>
        /// Gets patient statistics
        /// </summary>
        /// <returns>Patient statistics</returns>
        Task<BaseResponse<PatientStatisticsResponse>> GetPatientStatisticsAsync();

        /// <summary>
        /// Validates if a patient code is unique
        /// </summary>
        /// <param name="patientCode">Patient code to validate</param>
        /// <param name="excludePatientId">Patient ID to exclude from validation (for updates)</param>
        /// <returns>Validation result</returns>
        Task<BaseResponse<bool>> ValidatePatientCodeAsync(string patientCode, Guid? excludePatientId = null);

        /// <summary>
        /// Validates if a national ID is unique
        /// </summary>
        /// <param name="nationalId">National ID to validate</param>
        /// <param name="excludePatientId">Patient ID to exclude from validation (for updates)</param>
        /// <returns>Validation result</returns>
        Task<BaseResponse<bool>> ValidateNationalIdAsync(string nationalId, Guid? excludePatientId = null);

        /// <summary>
        /// Gets available blood types
        /// </summary>
        /// <returns>List of blood types</returns>
        Task<BaseResponse<List<string>>> GetAvailableBloodTypesAsync();

        /// <summary>
        /// Gets relationship type options
        /// </summary>
        /// <returns>List of relationship types</returns>
        Task<BaseResponse<Dictionary<int, string>>> GetRelationshipTypesAsync();

        /// <summary>
        /// Checks if two patients can have a relationship
        /// </summary>
        /// <param name="patient1Id">First patient ID</param>
        /// <param name="patient2Id">Second patient ID</param>
        /// <returns>Validation result</returns>
        Task<BaseResponse<bool>> CanCreateRelationshipAsync(Guid patient1Id, Guid patient2Id);

        /// <summary>
        /// Gets patients by relationship type
        /// </summary>
        /// <param name="patientId">Patient ID</param>
        /// <param name="relationshipType">Relationship type</param>
        /// <returns>List of related patients</returns>
        Task<BaseResponse<List<RelatedPatientInfo>>> GetRelatedPatientsAsync(Guid patientId, Core.Enum.RelationshipType relationshipType);

        #endregion

        #region Bulk Operations

        /// <summary>
        /// Bulk updates patient status
        /// </summary>
        /// <param name="patientIds">List of patient IDs</param>
        /// <param name="isActive">New status</param>
        /// <param name="reason">Reason for status change</param>
        /// <returns>Operation result</returns>
        Task<BaseResponse<int>> BulkUpdatePatientStatusAsync(List<Guid> patientIds, bool isActive, string? reason = null);

        /// <summary>
        /// Bulk deletes patients
        /// </summary>
        /// <param name="patientIds">List of patient IDs</param>
        /// <returns>Operation result</returns>
        Task<BaseResponse<int>> BulkDeletePatientsAsync(List<Guid> patientIds);

        #endregion
    }
}

using System;
using System.Text.Json.Serialization;

namespace FSCMS.Service.ReponseModel
{
    /// <summary>
    /// Common shared response models used across multiple services
    /// </summary>
    
    /// <summary>
    /// Basic patient information for reference in various responses
    /// </summary>
    public class PatientBasicInfo
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("fullName")]
        public string? FullName { get; set; }

        [JsonPropertyName("email")]
        public string? Email { get; set; }

        [JsonPropertyName("phoneNumber")]
        public string? PhoneNumber { get; set; }

        [JsonPropertyName("dob")]
        public DateOnly? DOB { get; set; }

        [JsonPropertyName("gender")]
        public string? Gender { get; set; }
    }

    /// <summary>
    /// Basic treatment information for reference in various responses
    /// </summary>
    public class TreatmentBasicInfo
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("description")]
        public string? Description { get; set; }
    }
}


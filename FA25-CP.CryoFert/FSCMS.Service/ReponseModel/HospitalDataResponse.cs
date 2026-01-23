using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FSCMS.Service.ReponseModel
{
    /// <summary>
    /// Response model for hospital data operations
    /// </summary>
    public class HospitalDataResponse
    {
        /// <summary>
        /// Unique identifier of the hospital data
        /// </summary>
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        /// <summary>
        /// The hospital data value
        /// </summary>
        [JsonPropertyName("value")]
        public decimal Value { get; set; }

        /// <summary>
        /// Creation timestamp
        /// </summary>
        [JsonPropertyName("createdAt")]
        public DateTime? CreatedAt { get; set; }

        /// <summary>
        /// Last update timestamp
        /// </summary>
        [JsonPropertyName("updatedAt")]
        public DateTime? UpdatedAt { get; set; }
    }
}

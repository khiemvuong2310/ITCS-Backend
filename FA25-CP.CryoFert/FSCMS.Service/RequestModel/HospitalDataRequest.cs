using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSCMS.Service.RequestModel
{
    /// <summary>
    /// Request model for creating new hospital data
    /// </summary>
    public class CreateHospitalDataRequest
    {
        [Required(ErrorMessage = "Value is required")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Value must be greater than 0")]
        public decimal Value { get; set; }
    }

    /// <summary>
    /// Request model for updating existing hospital data (supports partial updates)
    /// </summary>
    public class UpdateHospitalDataRequest
    {
        /// <summary>
        /// The ID of the hospital data to update
        /// </summary>
        [Required(ErrorMessage = "ID is required")]
        public Guid Id { get; set; }

        /// <summary>
        /// The new hospital data value (optional - if null, keeps existing value)
        /// </summary>
        [Range(0.01, double.MaxValue, ErrorMessage = "Value must be greater than 0")]
        public decimal? Value { get; set; }
    }
}

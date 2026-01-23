using FSCMS.Service.ReponseModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSCMS.Service.RequestModel
{
    /// <summary>
    /// Request model for getting hospital data with pagination and filtering
    /// </summary>
    public class GetHospitalDataRequest : PagingModel
    {
        /// <summary>
        /// Optional filter by minimum value
        /// </summary>
        public decimal? MinValue { get; set; }

        /// <summary>
        /// Optional filter by maximum value
        /// </summary>
        public decimal? MaxValue { get; set; }

        /// <summary>
        /// Optional search term for filtering
        /// </summary>
        public string? SearchTerm { get; set; }
    }
}

using FSCMS.Core.Enum;
using FSCMS.Service.ReponseModel;
using System.ComponentModel.DataAnnotations;

namespace FSCMS.Service.RequestModel
{
    public class GetTreatmentsRequest : PagingModel
    {
        public string? SearchTerm { get; set; }
        public Guid? PatientId { get; set; }
        public Guid? DoctorId { get; set; }
        public TreatmentType? TreatmentType { get; set; }
        public TreatmentStatus? Status { get; set; }

        [DataType(DataType.Date)]
        public DateTime? StartDateFrom { get; set; }

        [DataType(DataType.Date)]
        public DateTime? StartDateTo { get; set; }

        public void Normalize()
        {
            if (Page < 1) Page = 1;
            if (Size < 1) Size = 10;
            if (Size > 100) Size = 100;

            SearchTerm = string.IsNullOrWhiteSpace(SearchTerm) ? null : SearchTerm.Trim();
            Sort = string.IsNullOrWhiteSpace(Sort) ? "startdate" : Sort.Trim().ToLower();
            Order = string.IsNullOrWhiteSpace(Order) ? "desc" : Order.Trim().ToLower();

            if (StartDateFrom.HasValue && StartDateTo.HasValue && StartDateFrom > StartDateTo)
            {
                var tmp = StartDateFrom;
                StartDateFrom = StartDateTo;
                StartDateTo = tmp;
            }
        }
    }
}



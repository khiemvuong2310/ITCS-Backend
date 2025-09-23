using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSCMS.Core.Entities
{
    /// <summary>
    /// Entity đại diện cho phản hồi của bệnh nhân
    /// Ghi lại đánh giá và phản hồi của bệnh nhân về dịch vụ
    /// </summary>
    public class Feedback : BaseEntity
    {
        public int PatientId { get; set; }
        public int ServiceId { get; set; }
        public int Rating { get; set; }
        public string Content { get; set; }
        public string Status { get; set; }

        public virtual Patient? Patient { get; set; }
        public virtual Service? Service { get; set; }
    }
}

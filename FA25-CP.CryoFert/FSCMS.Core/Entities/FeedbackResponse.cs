using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSCMS.Core.Entities
{
    /// <summary>
    /// Entity đại diện cho phản hồi của admin
    /// Ghi lại phản hồi của quản trị viên đối với phản hồi của bệnh nhân
    /// </summary>
    public class FeedbackResponse : BaseEntity
    {
        public int FeedbackId { get; set; }
        public int AdminId { get; set; }
        public string Notes { get; set; }
        public string Status { get; set; }

        public virtual Feedback? Feedback { get; set; }
        public virtual User? Admin { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSCMS.Core.Entities
{
    public class FeedbackResponse : BaseEntity
    {
        public int FeedbackID { get; set; }
        public int AdminID { get; set; }
        public string Notes { get; set; }
        public string Status { get; set; }
    }
}

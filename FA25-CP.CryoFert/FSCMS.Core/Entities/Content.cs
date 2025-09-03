using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSCMS.Core.Entities
{
    public class Content : BaseEntity
    {
        public string Title { get; set; }
        public string Body { get; set; }
        public string Category { get; set; } // Service/Doctor/Blog/Notice
        public string Author { get; set; }
        public string Media { get; set; }
        public string Status { get; set; }

        public int? CreatedByUserId { get; set; }
        public virtual User? CreatedByUser { get; set; }
    }
}
